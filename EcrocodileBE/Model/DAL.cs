using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EcrocodileBE.Model
{
    public class DAL
    {
        public Response register(Users user, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID",0);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName == null ? " ": user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName == null ? " " : user.LastName);
            cmd.Parameters.AddWithValue("@Password", user.Password == null ? " " : user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email == null ? " " : user.Email);
            cmd.Parameters.AddWithValue("@Fund", 0 );
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", 0);
            cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User Registered Successfully..!!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User Registeratin Failed..!!";
            }

            return response;
        }
        public Response login(Users users, SqlConnection connection)
        {
            SqlDataAdapter sda = new SqlDataAdapter("sp_login", connection);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            sda.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);

                response.StatusCode = 200;
                response.StatusMessage = "User Is Valid..!!";
                response.user = null;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User Is Inalid..!!";
            }
            return response;
        }

        public Response viewUser(Users users, SqlConnection connection)
        {
            SqlDataAdapter sda = new SqlDataAdapter("p_viewUser", connection);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@ID", users.Email);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                response.StatusCode = 200;
                response.StatusMessage = "User Exists..!!";
                response.user = null;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exist..!!";
                response.user = user;
            }
            return response;
        }

        public Response updateProfile(Users user, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Record Upadated Successsfully...!!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Same error occured.Try after sometime..!!";
            }

            return response;
        }

        public Response addToCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", cart.UserId);
            cmd.Parameters.AddWithValue("@CrocodileId", cart.CrocodileId);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quentity", cart.Quentity);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
            
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Added Successfylly..!!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "could Not be added..!!";
            }
            return response;
        }

        public Response placeOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "order has been  placeed successfully..!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "order could not be placeed successfully..!";
            }
            return response;
        }

        public Response orderList(Users users, SqlConnection connection)
        {
            Response response = new Response();
            List<Orders> listOrder = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    listOrder.Add(order);

                }
                if (listOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order Details Fetch..!";
                    response.listOrders = listOrder;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order Detaols are not avaolable..!";
                    response.listOrders = null;
                }
                
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order Detaols are not avaolable..!";
                response.listOrders = null;
            }
            return response;
        }

        public Response addupdateCrocodile(Crocodils crocodils, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddUpdateCrocodile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", crocodils.Name);
            cmd.Parameters.AddWithValue("@Menufacturer", crocodils.Menufacturer);
            cmd.Parameters.AddWithValue("@UnitPrice", crocodils.UnitPrice);
            cmd.Parameters.AddWithValue("@Quentity", crocodils.Quentity);
            cmd.Parameters.AddWithValue("@ExpDate", crocodils.ExpDate);
            cmd.Parameters.AddWithValue("@ImageUrl", crocodils.ImageUrl);
            cmd.Parameters.AddWithValue("@Status", crocodils.Status);
            cmd.Parameters.AddWithValue("@Type", crocodils.Type);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Product Inserted Successfully..!!";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Product Insert Failed..!!";
            }


            return response;
        }

        public Response userList(SqlConnection connection)
        {
            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
           
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user = new Users();
                    user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    user.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.Fund = Convert.ToDecimal(dt.Rows[i]["Fund"]);
                    user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                    user.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                    listUsers.Add(user);

                }
                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User Details Fetch..!";
                    response.listUsers = listUsers;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "User Details are not avaolable..!";
                    response.listUsers = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User Detaols are not avaolable..!";
                response.listUsers = null;
            }
            return response;
        }
    }
}

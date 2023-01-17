using EcrocodileBE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EcrocodileBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public  AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("addupdateCrocodile")]
        public Response addupdateCrocodile(Crocodils crocodils)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECrocCS").ToString());
            Response response = dal.addupdateCrocodile(crocodils,connection);
            return response;
        }


        [HttpGet]
        [Route("userList")]
        public Response userList()
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECrocCS").ToString());
            Response response = dal.userList(connection);
            return response;
        }
    }
}

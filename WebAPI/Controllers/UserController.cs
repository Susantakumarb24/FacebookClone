using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebAPI.Constants;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public UserController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            //string query = @"select UserId,FirstName,LastName,DOB,Email,Mobile, Password,Gender,ProfilePicture from dbo.Users";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(Query.getQuery, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult(table);
        }

        

        [Route("LoginData")]
        [HttpPost]
        public JsonResult LoginData(Login data)
        {
            //string query = @"select * from dbo.Users where Password='" + data.password +"'";
            //string query = @"select * from dbo.Users where Password='" + data.Password + "' and (Mobile='" + data.Username + "' or Email='" + data.Username + "')";
            DataTable table = new DataTable();
            
            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
            SqlDataReader myReader;
            //bool flag = false;
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(Query.loginQuery, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Email", data.Username);
                        myCommand.Parameters.AddWithValue("@Mobile", data.Username);
                        myCommand.Parameters.AddWithValue("@Password", data.Password);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return new JsonResult("falsedfghjk");

            }
            return new JsonResult(table.Rows.Count);

        }


        [Route("RstPwd")]
        [HttpPost]

        public JsonResult RstPwd(Password dt)
        {
            string query = @"update dbo.Users set Password='"+dt.password+"' where Email='" + dt.email + "'";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
            SqlDataReader myReader;
            //bool flag = false;
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                       
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine();
            }
            return new JsonResult("Success");
        }


        [Route("EmailVerify")]
        [HttpPost]
        public JsonResult EmailVerify(EmailVar data)
        {
            string query = @"select * from dbo.Users where Email='" + data.email + "'";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
            SqlDataReader myReader;
            //bool flag = false;
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                       
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return new JsonResult("falsedfghjk");

            }
            return new JsonResult(table.Rows.Count);

        }


        [HttpPost]
        public JsonResult Post(User user)
        {
            //string query = @"
            //        insert into dbo.Users values 
            //        ('" + user.FirstName + @"','" + user.LastName + @"','" + user.DOB + @"','" + user.Email + @"','" + user.Mobile + @"','" + user.Password + @"','" + user.Gender + @"','" + user.ProfilePicture + @"')
            //        ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
            SqlDataReader myReader;
            bool flag = false;
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(Query.registerQuery, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                        myCommand.Parameters.AddWithValue("@LastName", user.LastName);
                        myCommand.Parameters.AddWithValue("@DOB", user.DOB);
                        myCommand.Parameters.AddWithValue("@Email", user.Email);
                        myCommand.Parameters.AddWithValue("@Mobile", user.Mobile);
                        myCommand.Parameters.AddWithValue("@Password", user.Password);
                        myCommand.Parameters.AddWithValue("@Gender", user.Gender);
                        myCommand.Parameters.AddWithValue("@ProfilePicture", user.ProfilePicture);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return ""
            }
            

            return new JsonResult(table.Rows.Count);
        }

        [Route("UserData")]
        [HttpPost]
        public JsonResult UserData(Login em)
        {
            string query = @"select FirstName,LastName,DOB,Email,Mobile,Gender,ProfilePicture from dbo.Users where Email='" + em.Username + "'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult(table);
        }


        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }


     


        [HttpDelete("{id}")]
        public JsonResult Delete(string id)
        {
            //string query = @"
            //        delete from dbo.Users
            //        where Mobile = '" + id + @"' 
            //        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(Query.deleteQuery, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id",id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
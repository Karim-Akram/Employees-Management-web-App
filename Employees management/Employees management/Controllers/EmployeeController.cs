using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Employees_management.Models;
using System.Text;
using System.Runtime.Intrinsics.Arm;

namespace Employees_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configration;

        public EmployeeController(IConfiguration configration)
        {
            _configration = configration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select EmployeeID, EmployeeName, EmployeeEmail, EmployeePhone, EmployeeAge, Department
From dbo.Employee;                   

";
            DataTable table = new DataTable();
            string sqlDataSource = _configration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();


                }

            }
            return new JsonResult(table);

        }
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"
         insert into dbo.Employee
(EmployeeName,EmployeeEmail,EmployeePhone,EmployeeAge,Department) 
                    values (@EmployeeName,@EmployeeEmail,@EmployeePhone,@EmployeeAge,@Department)                  

";
            DataTable table = new DataTable();
            string sqlDataSource = _configration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName",emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@EmployeeEmail", emp.EmployeeEmail);
                    myCommand.Parameters.AddWithValue("@EmployeePhone", emp.EmployeePhone);
                    myCommand.Parameters.AddWithValue("@EmployeeAge", emp.EmployeeAge);
                    myCommand.Parameters.AddWithValue("@Department", emp.EmployeeDepartment);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();


                }

            }
            return new JsonResult("Added Succussfully");

        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"
                update dbo.Employee
                    set EmployeeName =@EmployeeName,
                    EmployeeEmail=@EmployeeEmail,
                    EmployeePhone=@EmployeePhone,
                    EmployeeAge=@EmployeeAge,
                    Department=@Department

                    where EmployeeID=@EmployeeID

";
            DataTable table = new DataTable();
            string sqlDataSource = _configration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeID",emp.EmployeeId);
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@EmployeeEmail", emp.EmployeeEmail);
                    myCommand.Parameters.AddWithValue("@EmployeePhone", emp.EmployeePhone);
                    myCommand.Parameters.AddWithValue("@EmployeeAge", emp.EmployeeAge);
                    myCommand.Parameters.AddWithValue("@Department", emp.EmployeeDepartment);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();


                }

            }
            return new JsonResult("Updated Successfully");

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from dbo.Employee
                    where EmployeeID=@EmployeeID

";
            DataTable table = new DataTable();
            string sqlDataSource = _configration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();


                }

            }
            return new JsonResult("Deleted Successfully");

        }







    }
}

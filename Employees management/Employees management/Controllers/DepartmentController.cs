using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Employees_management.Models;
using System.Text;

namespace Employees_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configration;

        public DepartmentController(IConfiguration configration)
        {
            _configration = configration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select DepartmentID, DepartmentName From dbo.Department;                    

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
        public JsonResult Post(Department dep)
        {
            string query = @"
                insert into dbo.Department
                    values (@DepartmentName)                  

";
            DataTable table = new DataTable();
            string sqlDataSource = _configration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();


                }

            }
            return new JsonResult("Added Succussfully");

        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"
                update dbo.Department
                    set DepartmentName =@DepartmentName
                    where DepartmentID=@DepartmentID

";
            DataTable table = new DataTable();
            string sqlDataSource = _configration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentID", dep.DepartmentID);
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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
                delete from dbo.Department
                    where DepartmentID=@DepartmentID

";
            DataTable table = new DataTable();
            string sqlDataSource = _configration.GetConnectionString("EmployeeAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentID",id);
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

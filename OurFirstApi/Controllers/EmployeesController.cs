using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using OurFirstApi.Models;

namespace OurFirstApi.Controllers
{
    //api/employees
    [HttpGet, Route("api/employee")] //-> route prefix, adds the prefix to the route attributes going forward
    public class EmployeesController : ApiController
    {
        //api/employees
        public HttpResponseMessage Get()
        {
            using (var connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    var result = connection.Query<EmployeeListResult>("select * " +
                                                                      "from Employee");


                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Query blew up");
                }
            }
        }

        //api/employees/3000
        [HttpGet, Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            using (var connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    var result =
                        connection.Query<EmployeeListResult>("Select * From Employee where EmployeeId = @id",
                            new {id = id}).FirstOrDefault();

                    if (result == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,$"Employee with the Id {id} was not found");
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        // POST api/employees/
        public HttpResponseMessage Post(EmployeeListResult employee)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    var results = connection.Execute("Insert into Employee(FirstName, LastName) " +
                                                          "Values(@FirstName, @LastName)",
                        new { FirstName = employee.FirstName, LastName = employee.LastName});
                    return Request.CreateResponse(HttpStatusCode.Created);

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        // PUT api/employees/5
        public HttpResponseMessage Put(int id, EmployeeListResult employee)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {

                try
                {
                    connection.Open();

                    var rowsAffected = connection.Execute("Update Employee " +
                                                            "SET LastName = @NewLastName " +
                                                          "Where EmployeeId = @EmployeeId",
                        new { EmployeeId = id, NewLastName = employee.LastName });
                    return Request.CreateResponse(HttpStatusCode.Accepted);


                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
        }
        // DELETE api/values/5

        public HttpResponseMessage Delete(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                connection.Open();

                try
                {
                    var result = connection.Execute("Delete From Employee " +
                                                          "Where EmployeeId = @EmployeeId",
                                                            new { EmployeeId = id });
                    return Request.CreateResponse(HttpStatusCode.Accepted);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
        }
        [HttpGet, Route("{firstname}")]
        public HttpResponseMessage Get(string firstname)
        {
            using (var connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    var result =
                        connection.Query<EmployeeListResult>("Select * From Employee where FirstName = @firstname",
                            new {firstname }).FirstOrDefault();

                    if (result == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with the first name {firstname} was not found");
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
        }
    }
}

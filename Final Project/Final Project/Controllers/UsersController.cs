using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_Project.Models;


namespace Final_Project.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public List<User> Get()
        {
            User user = new User();
            return user.ReadAllUsers();
        }

        // GET api/<controller>  ///Right match between questions and rights table 
        public List<Right> Put([FromBody] User user)
        {

            return user.ReadUserRights();
        }

        // GET api/<controller>/5
        public List<User> Get(string phone, string password)
        {
            User user = new User();
            return user.ReadUser(phone, password);
        }

        // POST api/<controller>
        public int Post([FromBody] User user)
        {
            return user.InsertUser();
        }

        // PUT api/<controller>/5
 
        public string Put(string pass, string phone) 
        {
            User a = new User();
            return a.UpdateUserPass(pass, phone);
        }

        // DELETE api/<controller>/5
        public string Delete([FromBody] User u)
        {
            return u.UpdateStatus();
        }
    }
}
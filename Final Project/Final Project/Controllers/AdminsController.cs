using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    public class AdminsController : ApiController
    {

        // GET api/<controller>
        public List<Admin> Get(string phone, string password)
        {
            Admin admin = new Admin();
            return admin.ReadAdmin(phone, password);
        }

        public List<Admin> Get() // to data table
        {
            Admin admin = new Admin();
            return admin.ReadAllAdmins();
        }

        [HttpPut]
        [Route("api/Admins/messi")]
        public void f1 ()
        {
            int i = 8;
            i++;
        }

        // POST api/<controller>
        public int Post([FromBody] Admin admin)
        {
            return admin.InserAdmin();
        }

        //PUT api/<controller>/5
        public string Put([FromBody] Admin a)
        {
            return a.Update();
        }

        //PUT api/<controller>/5
        public string Put(string pass, string phone) ///////add
        {
            Admin a = new Admin();
            return a.UpdatePass(pass, phone);
        }

        // DELETE api/<controller>/5
        public string Delete([FromBody] Admin a)
        {
            return a.UpdateStatus();
        }
    }
}
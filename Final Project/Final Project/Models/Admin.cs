using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project.Models.DAL;


namespace Final_Project.Models
{
    public class Admin
    {
        int id;
        string first_name;
        string last_name;
        string email;
        string password;
        string phone;
        string role;

        public int Id { get => id; set => id = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Role { get => role; set => role = value; }

        public Admin(string first_name, string last_name, string email, string password, string phone, string role,int id)
        {
            this.id = id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.password = password;
            this.phone = phone;
            this.role = role;
        }

        public Admin() { }

        public List<Admin> ReadAdmin(string phone,string password)//מביא את פרטי האדמין למסך התחברות
        {
            Data ds = new Data();
            return ds.ReadAdmin(phone, password);
        }

        public List<Admin> ReadAllAdmins()//admins to datatable
        {
            Data ds = new Data();
            return ds.ReadAllAdmins();
        }

        public int InserAdmin()
        {
            Data ds = new Data();
            return ds.InserAdmin(this);
        }

        public string UpdateStatus()
        {
            Data ds = new Data();
            return ds.UpdateStatus(this, 0);
        }

        public string Update()
        {
            Data ds = new Data();
            return ds.UpdateData(this);
        }

        public string UpdatePass(string pass, string phone)
        {
            Data ds = new Data();
            return ds.UpdatePass(pass, phone);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project.Models.DAL;

namespace Final_Project.Models
{
    public class User
    {
        int id;
        string name;
        string phone;
        string password;
        string bdate;
        int gender;
        string familyStatus;
        int disP;
        int income;
        int isres;
        int workDis;
        string signdate;
        int accId;
        string age;
        int[] med_stat;

        public User() { }
        
        public User(int id,string name, string phone, string password, string bdate, int gender, string familyStatus, int disP, int income, int isres, int workDis, int accId, string age, int [] medstat)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.password = password;
            this.bdate = bdate;
            this.gender = gender;
            this.familyStatus = familyStatus;
            this.disP = disP;
            this.income = income;
            this.isres = isres;
            this.workDis = workDis;
            this.signdate = DateTime.Now.ToString("dd/MM/yyyy");
            this.accId = accId;
            this.age = age;
            this.med_stat = medstat;
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Password { get => password; set => password = value; }
        public string Bdate { get => bdate; set => bdate = value; }
        public int Gender { get => gender; set => gender = value; }
        public string FamilyStatus { get => familyStatus; set => familyStatus = value; }
        public int DisP { get => disP; set => disP = value; }
        public int Income { get => income; set => income = value; }
        public int Isres { get => isres; set => isres = value; }
        public int WorkDis { get => workDis; set => workDis = value; }
        public string Signdate { get => signdate; set => signdate = value; }
        public int AccId { get => accId; set => accId = value; }
        public string Age { get => age; set => age = value;}
        public int[] Medstat { get => med_stat; set => med_stat = value; }

        //methods
        public List<User> ReadAllUsers()//users to data table
        {
            Data ds = new Data();
            return ds.ReadAllUsers();
        }

        public List<User> ReadUser(string phone, string password)//מביא את פרטי היוזר למסך התחברות
        {
            Data ds = new Data();
            return ds.ReadUser(phone, password);
        }

        public string UpdateStatus()
        {
            Data ds = new Data();
            return ds.UpdateUserStatus(this, 0);
        }

        public int InsertUser() //new user
        {
            Data ds = new Data();
            return ds.InsertUser(this);
        }

        public string UpdateUserPass(string pass, string phone)
        {
            Data ds = new Data();
            return ds.UpdateUserPass(pass, phone);
        }

        public List<Right> ReadUserRights ()
        {
            Data ds = new Data();
            return ds.ReadUserRights(this);

        }
    }
}
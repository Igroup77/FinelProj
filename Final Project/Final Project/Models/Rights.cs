using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project.Models.DAL;
using System.Collections.Specialized;

namespace Final_Project.Models
{
    public class Right
    {
        ListDictionary medstatwithstring;
        int[] med_stat;
        int id;
        string name;
        string st1;
        string st2;
        string st3;
        string description;
        string source;
        string accident_type;
        int residance;
        int fstatus;
        int min_disap;
        int max_disap;
        int minInc;
        int maxInc;
        int workdis;
        int minAge;
        int maxAge;
        string czUrl;
        string date;
        string insertAdmin;
        string gender;
        int rstatus;


        public Right() { }

        public Right(int id,string name, string st1, string st2, string st3, string description, string source, string accident_type, int residance, int fstatus, int min_disap, int max_disap, int minInc, int maxInc, int workdis, int minAge, int maxAge, string czUrl, string date, string insertAdmin,string gender, ListDictionary medstatwithstring,int rstatus)
        {
            this.id = id;
            this.Name = name;
            this.St1 = st1;
            this.St2 = st2;
            this.St3 = st3;
            this.Description = description;
            this.Source = source;
            this.Accident_type = accident_type;
            this.Residance = residance;
            this.Fstatus = fstatus;
            this.Min_disap = min_disap;
            this.Max_disap = max_disap;
            this.MinInc = minInc;
            this.MaxInc = maxInc;
            this.Workdis = workdis;
            this.MinAge = minAge;
            this.MaxAge = maxAge;
            this.CzUrl = czUrl;
            this.Date = date;
            this.insertAdmin = insertAdmin;
            this.gender = gender;
            this.medstatwithstring = medstatwithstring;
            this.rstatus = rstatus;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string St1 { get => st1; set => st1 = value; }
        public string St2 { get => st2; set => st2 = value; }
        public string St3 { get => st3; set => st3 = value; }
        public string Description { get => description; set => description = value; }
        public string Source { get => source; set => source = value; }
        public string Accident_type { get => accident_type; set => accident_type = value; }
        public int Residance { get => residance; set => residance = value; }
        public int Fstatus { get => fstatus; set => fstatus = value; }
        public int Min_disap { get => min_disap; set => min_disap = value; }
        public int Max_disap { get => max_disap; set => max_disap = value; }
        public int MinInc { get => minInc; set => minInc = value; }
        public int MaxInc { get => maxInc; set => maxInc = value; }
        public int Workdis { get => workdis; set => workdis = value; }
        public int MinAge { get => minAge; set => minAge = value; }
        public int MaxAge { get => maxAge; set => maxAge = value; }
        public string CzUrl { get => czUrl; set => czUrl = value; }
        public string Date { get => date; set => date = value; }
        public string InsertAdmin { get => insertAdmin; set => insertAdmin = value; }
        public string Gender { get => gender; set => gender = value; }
        public int[] Medstat { get => med_stat; set => med_stat = value; }
        public ListDictionary Medstatwithstring { get => medstatwithstring; set => medstatwithstring = value; }
        public int Rstatus { get => rstatus; set => rstatus = value; }

        //methods
        public List<Right> ReadAllRights()//rights to datatable
        {
            Data ds = new Data();
            return ds.ReadAllRights();
        }
        public List<Right> ReadRights4User(int id)
        {
            Data ds = new Data();
            return ds.ReadRights4User(id);
        }

        public int ReadcountRights()
        {
            Data ds = new Data();
            return ds.ReadcountRights();
        }

        public string UpdateStatus()
        {
            Data ds = new Data();
            return ds.UpdateRightStatus(this, 0);
        }

        public object InsertRight()
        {
            Data ds = new Data();
            return ds.InsertRight(this);
        }

        public string Update()
        {
            Data ds = new Data();
            return ds.UpdateRightData(this);
        }

    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project.Models.DAL;

namespace Final_Project.Models
{
    public class DashBoard
    {
        int rights;
        int patient;
        int exrights;
        int percent;
        List<int> quantity;
        List<int> months;
        List<string> log;
        List<string> sourceList;
        List<string> monthNameList;
        List<int> userpermonth;
        List<int> times;

        public DashBoard(int rights, int patient, int exrights, int percent, List<int> quantity, List<int> months, List<string> log, List<string> sourceList, List<string> monthNameList, List<int> userpermonth, List<int> times)
        {
            this.rights = rights;
            this.patient = patient;
            this.exrights = exrights;
            this.percent = percent;
            this.quantity = quantity;
            this.months = months;
            this.log = log;
            this.sourceList = sourceList;
            this.monthNameList = monthNameList;
            this.userpermonth = userpermonth;
            this.times = times;
        }
        public DashBoard() { }

        public int Rights { get => rights; set => rights = value; }
        public int Patient { get => patient; set => patient = value; }
        public int Exrights { get => exrights; set => exrights = value; }
        public int Percent { get => percent; set => percent = value; }
        public List<int> Quantity { get => quantity; set => quantity = value; }
        public List<int> Months { get => months; set => months = value; }
        public List<string> Log { get => log; set => log = value; }
        public List<string> SourceList { get => sourceList; set => sourceList = value; }
        public List<string> MonthNameList { get => monthNameList; set => monthNameList = value; }
        public List<int> Times { get => times; set => times = value; }

        public List<int> Userpermonth { get => userpermonth; set => userpermonth = value; }
        public DashBoard ReadDashBoard()
        {
            Data ds = new Data();
            return ds.ReadDashBoard();
        }

        public DashBoard ReadDashBoard(int id)
        {
            Data ds = new Data();
            return ds.ReadDashBoard(id);
        }
        public int changeStatus(int uid, int rid, int status)
        {
            Data ds = new Data();
            return ds.changeStatus(uid, rid, status);

        }

        public int addRighttoUser(int uid, int rid)
        {
            Data ds = new Data();
            return ds.addRighttoUser(uid, rid);

        }
    }

}
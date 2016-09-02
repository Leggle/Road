using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Road.Models.DB;

namespace Road.Models.ViewModel
{
    public class TransactionViewModel
    {
        public int AutoNumber { set; get; }
        public Nullable<DateTime>  Trans_Date { set; get; }
        public Nullable<int> Emp_No { set; get; }
        public string Emp_Name { get; set; }//
        public Nullable<int> Mach_No { set; get; }
        public string Mach_Desc { get; set; }//
        public Nullable<int> BIA_No { set; get; }
        public string Road_Name { get; set; }//
        public Nullable<int> Activity_Code { set; get; }
        public string Activity_Desc { get; set; }//
        public Nullable<double> Hours { set; get; }
        public Nullable<double> Lease_Chg { set; get; } 

        public List<tblTransaction> Trans { set; get; }
        public List<tblEmp> Emps { set; get; }
        public List<tblMach> Maches { set; get; }
        public List<tblRoad> Roads { set; get; }
        public List<tblAct> Acts { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Road.Models.DB;
using System.ComponentModel.DataAnnotations;

namespace Road.Models.ViewModel
{
    public class MachineViewModel
    {
        public int Mach_No { set; get; }
        [Required]
        public string Mach_Desc { set; get; }
        public Nullable<double> Lease_Rate { set; get; }
        public Nullable<int> Owner_Num { set; get; }
        public Nullable<bool> Active { set; get; }

        public IEnumerable<tblMach> Maches { set; get; }
        public IEnumerable<tblOwner> Owners { set; get; }

        
        }

    public class ModifyMach
    {
        RoadsEntities dbContext = new RoadsEntities();

        public tblMach GetMach(int Mach_no)
        {
            return dbContext.tblMaches.Where(m => m.Mach_No == Mach_no).FirstOrDefault();
        }

        public bool UpdateMach(tblMach tMach)
        {

            tblMach data = dbContext.tblMaches.Where(m => m.Mach_No == tMach.Mach_No).FirstOrDefault();

            data.Mach_No = tMach.Mach_No;
            data.Mach_Desc = tMach.Mach_Desc;
            data.Lease_Rate = tMach.Lease_Rate;
            data.Owner_Num = tMach.Owner_Num;
            data.Active = tMach.Active;

            dbContext.SaveChanges();

            return true;

        }

        public bool isMachNoExists(int mach_no)
        {
            return dbContext.tblMaches.Where(m => m.Mach_No.Equals(mach_no)).Any();
        }
    }
}
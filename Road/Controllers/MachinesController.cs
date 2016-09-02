using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Road.Models.DB;
using Road.Models.ViewModel;

namespace Road.Controllers
{
    public class MachinesController : Controller
    {
        RoadsEntities dbContext = new RoadsEntities();
        ModifyMach ms = new ModifyMach();

        public ActionResult Index()
        {
            return View("machineList");
        }

        public ActionResult populateList()
        {

            ViewBag.DropdownResult = dbContext.tblOwners.ToList();

            return View("machineList");
               
        }

        public ActionResult webGrid()
        {
            MachineViewModel vm = new MachineViewModel();

            vm.Maches = dbContext.tblMaches.ToList();
            vm.Owners = dbContext.tblOwners.ToList();

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddNew(tblMach mach)
        {
            if (!ms.isMachNoExists(mach.Mach_No))
            {
                dbContext.tblMaches.Add(mach);
                dbContext.SaveChanges();

                ModelState.Clear();
                return RedirectToAction("populateList");
            }
            else
            {
                ModelState.AddModelError("", "The Machine Number is already existed. Please enter another one.");
            }

            return populateList();
           
        }

        public PartialViewResult ShowEdit(int id)
        {
            var data = ms.GetMach(id);
            MachineViewModel mvm = new MachineViewModel();

            mvm.Maches = dbContext.tblMaches.ToList();
            mvm.Owners = dbContext.tblOwners.ToList();

            mvm.Mach_No = data.Mach_No;
            mvm.Mach_Desc = data.Mach_Desc;
            mvm.Lease_Rate = data.Lease_Rate;
            mvm.Owner_Num = data.Owner_Num;
            mvm.Active = data.Active;

            return PartialView(mvm);
        }

        [HttpPost]
        public ActionResult UpdateMach(tblMach data)
        {
            tblMach tm = new tblMach();
            tm.Mach_No = data.Mach_No;
            tm.Mach_Desc = data.Mach_Desc;
            tm.Lease_Rate = data.Lease_Rate;
            tm.Owner_Num = data.Owner_Num;
            tm.Active = data.Active;
            bool check = ms.UpdateMach(tm);

            if (check)
            {
                ModelState.Clear();
                return RedirectToAction("populateList", "Machines");
            }


            return PartialView("Edit");

        }
    }
}
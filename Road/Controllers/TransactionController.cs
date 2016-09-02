using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Road.Models.ViewModel;
using Road.Models.DB;
using Road.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.Entity;

namespace Road.Controllers
{
    public class TransactionController : Controller
    {
        RoadsEntities dbContext = new RoadsEntities();

        // GET: Transaction
        public ActionResult Index()
        {
            TransactionViewModel tvm = new TransactionViewModel();
            tvm.Emps = dbContext.tblEmps.ToList();
            tvm.Acts = dbContext.tblActs.ToList();
            tvm.Maches = dbContext.tblMaches.ToList();
            tvm.Roads = dbContext.tblRoads.ToList();
            tvm.Trans = dbContext.tblTransactions.ToList();

            return View("Transaction",tvm);
        }

        [HttpPost]
        public ActionResult addTransaction(TransactionViewModel tbt)
        {
            tblTransaction tvm = new tblTransaction();
            tvm.Trans_Date = tbt.Trans_Date;
            tvm.Emp_No = tbt.Emp_No;
            tvm.BIA_No = tbt.BIA_No;
            tvm.Activity_Code = tbt.Activity_Code;
            tvm.Mach_No = tbt.Mach_No;
            tvm.Hours = tbt.Hours;
            tvm.Lease_Chg = tbt.Lease_Chg;

            dbContext.tblTransactions.Add(tvm);
            dbContext.SaveChanges();

            return PartialView("kendoGrid",tvm);
        }

        public PartialViewResult showGrid()
        {
            return PartialView("kendoGrid");
        }

        public static IEnumerable<TransactionViewModel> Read()
        {
            RoadsEntities entities = new RoadsEntities();

            return entities.tblTransactions.Select(tvm => new TransactionViewModel
            {
                AutoNumber = tvm.AutoNumber,
                Trans_Date = tvm.Trans_Date,
                Emp_Name = tvm.tblEmp.Emp_Name,
                Mach_Desc = tvm.tblMach.Mach_Desc,
                Road_Name = tvm.tblRoad.Road_Name,
                Activity_Desc = tvm.tblAct.Activity_Desc,
                Hours = tvm.Hours,
                Lease_Chg = tvm.Lease_Chg
            });
        }

        public ActionResult kendoGrid([DataSourceRequest] DataSourceRequest request)
        {
            return Json(Read().ToDataSourceResult(request,ModelState),JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult update([DataSourceRequest] DataSourceRequest request, TransactionViewModel tvm)
        {
            RoadsEntities db = new RoadsEntities();

            var newrecord = db.tblTransactions.Where(t => t.AutoNumber.Equals(tvm.AutoNumber)).FirstOrDefault();

            newrecord.Hours = tvm.Hours;
            newrecord.Lease_Chg = tvm.Lease_Chg;

            db.SaveChanges();

            return Json(new[] { tvm }.ToDataSourceResult(request, ModelState));
        }

    }
}
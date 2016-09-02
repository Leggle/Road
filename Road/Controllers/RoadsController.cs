using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Road.Models.DB;
using Road.Models.ViewModel;

namespace Road.Controllers
{
    public class RoadsController : Controller
    {
        RoadsEntities re = new RoadsEntities();
        ModifyRoad mr = new ModifyRoad();

        // GET: Roads
        public ActionResult Index()
        {
            return View("Roads");
        }

        public ActionResult fillPage()
        {
            ViewBag.roadNames = re.tblTypes.ToList();

            return View("Roads");
        }

        public ActionResult webGrid()
        {
            RoadViewModel rvm = new RoadViewModel();
            rvm.Roadss = re.tblRoads.ToList();
            rvm.Typess = re.tblTypes.ToList();

            return View(rvm);
        }
        [HttpPost]
        public ActionResult AddRow(tblRoad tbl)
        {
            if (!mr.isRoadNoExists(tbl.BIA_No))
            {
                re.tblRoads.Add(tbl);
                re.SaveChanges();
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "The Road Code is already existed. Please enter another one.");
            }

            return fillPage();
        }

        public PartialViewResult Edit(int id)
        {
            var rd = mr.GetRoad(id);
            RoadViewModel rrvm = new RoadViewModel();
            rrvm.Roadss = re.tblRoads.ToList();
            rrvm.Typess = re.tblTypes.ToList();

            rrvm.BIA_No = rd.BIA_No;
            rrvm.Road_Name = rd.Road_Name;
            rrvm.Type_Id = (int)rd.Type_Id;
            rrvm.Miles = (double)rd.Miles;
            return PartialView(rrvm);
        }

        [HttpPost]
        public ActionResult UpdateRoad(tblRoad data)
        {
            tblRoad tr = new tblRoad();
            tr.BIA_No = data.BIA_No;
            tr.Road_Name = data.Road_Name;
            tr.Miles = data.Miles;
            tr.Type_Id = data.Type_Id;
            bool chk = mr.UpdateRoad(tr);

            if (chk)
            {
                ModelState.Clear();
                return RedirectToAction("fillPage", "Roads");
            }
            return PartialView("Edit");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Road.Models.DB;

namespace Road.Models.ViewModel
{
    public class RoadViewModel
    {
        public int BIA_No { get; set; }
        public string Road_Name { get; set; }
        public int Type_Id { get; set; }
        public double Miles { get; set; }

        public IEnumerable<tblRoad> Roadss { get; set; }
        public IEnumerable<tblType> Typess { get; set; }

    }

    public class ModifyRoad
    {
        RoadsEntities re = new RoadsEntities();
        public tblRoad GetRoad(int BIA_no)
        {
            return re.tblRoads.Where(m => m.BIA_No == BIA_no).FirstOrDefault();
        }
        public bool UpdateRoad(tblRoad tRoad)
        {
            tblRoad data = re.tblRoads.Where(m => m.BIA_No == tRoad.BIA_No).FirstOrDefault();

            data.BIA_No = tRoad.BIA_No;
            data.Road_Name = tRoad.Road_Name;
            data.Type_Id = tRoad.Type_Id;
            data.Miles = tRoad.Miles;
            re.SaveChanges();
            return true;
        }

        public bool isRoadNoExists(int road_no)
        {
            return re.tblRoads.Where(m => m.BIA_No.Equals(road_no)).Any();
        }

    }
}
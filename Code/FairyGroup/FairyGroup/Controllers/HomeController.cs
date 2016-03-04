using FairyGroup.Models.DBFairyGroup;
using FairyGroup.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FairyGroup.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult Post()
        {

            return View();
        }
        public ActionResult Regist()
        {

            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Profile()
        {

            return View();
        }

        public JsonResult getAreaThailand()
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = new ThaiAreaModel();
                    md.mdProvinceGroup = (from p in db.ProvinceGroups
                                          select p).ToList();
                    md.mdProvince = (from p in db.Provinces
                                     select p).ToList();

                    md.mdDistrict = (from d in db.Districts
                                     select d).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult getProvince()
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = (from p in db.Provinces
                              select p).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public JsonResult getDistrict(int ProvinceID)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = (from d in db.Districts
                              where d.ProvinceID == ProvinceID
                              select d).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public JsonResult getBuildingType()
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = (from b in db.BuildingTypes
                              select b).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public JsonResult getPostBuilding(int BuildingTypeID, int priceID, int ProvinceID, int DistrictID, string keySearch)
        //{ 
            
        //}
    }
}
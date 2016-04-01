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
        public ActionResult PostBuilding(int owner)
        {
            ViewBag.Owner = owner;
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
                    md.mdSubDistrict = (from d in db.SubDistricts
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
        public JsonResult getSubDistrict(int DistrictID)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = (from d in db.SubDistricts
                              where d.DistrictID == DistrictID
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
        public JsonResult getPriceSelect()
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = (from b in db.PriceSelections
                              select b).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult getCompany()
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = (from c in db.CompanyProfiles
                              select c).OrderBy(x => x.OrderID).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult getPostBuilding(int BuildingTypeID, int priceID, int ProvinceID, int DistrictID, string keySearch)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    DateTime mindate = DateTime.Now.AddMonths(-3);
                    var md = db.spPostBuilding_List(BuildingTypeID, priceID, ProvinceID, DistrictID, keySearch, mindate.Year.ToString() + mindate.Month.ToString().PadLeft(2, '0') + mindate.Day.ToString().PadLeft(2, '0')).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult CheckLogin(string UserName, string Password)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var login = (from u in db.Users
                                 where u.UserName == UserName
                                 select u).ToList();
                    if (login.Count == 1)
                    {
                        if (login[0].Password == Password)
                        {
                            login[0].Password = "";
                            return Json(login[0], JsonRequestBehavior.AllowGet);
                        }
                        else throw new Exception("Please Check User Or Password");
                    }
                    else { throw new Exception("Find not found User :" + UserName); }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public JsonResult SendContact(string fname, string lname, string email, string phone, string subject, string body)
        {
            try
            {
                body = "From : " + fname + " " + lname
                    + "<br/>Email : " + email
                    + "<br/>Phone : " + phone + "<br/>" + body.Replace("[[|]]", "<");
                using (var db = new FairyGroupEntities())
                { 
                    var ToMail = (from u in db.SystemConfigs
                                    where u.ConfigCode == "ContactToEmail"
                                    select u).SingleOrDefault().ConfigValue;
                    var FromMail = (from u in db.SystemConfigs
                                    where u.ConfigCode == "SendFrom"
                                    select u).SingleOrDefault().ConfigValue;
                    var Pass = (from u in db.SystemConfigs
                                where u.ConfigCode == "PassSendFrom"
                                select u).SingleOrDefault().ConfigValue;
                    var SMTPMail = (from u in db.SystemConfigs
                                    where u.ConfigCode == "ServerSMTPMail"
                                    select u).SingleOrDefault().ConfigValue;
                    var PortSendMail = (from u in db.SystemConfigs
                                        where u.ConfigCode == "PortSendMail"
                                        select u).SingleOrDefault().ConfigValue;



                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();

                    try
                    {
                        System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(FromMail);
                        message.From = fromAddress;
                        message.To.Add(ToMail);

                        message.Subject = subject;
                        message.IsBodyHtml = true;
                        message.Body = body;
                        // We use gmail as our smtp client
                        smtpClient.Host = SMTPMail;
                        smtpClient.Port = Convert.ToInt32(PortSendMail);
                        smtpClient.EnableSsl = false;
                        smtpClient.UseDefaultCredentials = true;
                        smtpClient.Credentials = new System.Net.NetworkCredential(FromMail, Pass);

                        smtpClient.Send(message);
                        

                        var mds = new LogMail {
                            SendFrom=FromMail,
                            SendTo = ToMail,
                            Fname = fname,
                            Lname=lname,
                            uEmail = email,
                            mSubject = subject,
                            mBody = body,
                            SendDate = DateTime.Now,
                            CreateDate = DateTime.Now,
                            LogMSG = "Success",
                            SendStatus = "Success"
                        };
                        db.LogMails.Add(mds);
                    }
                    catch (Exception ex)
                    {
                        var mds = new LogMail
                        {
                            SendFrom = FromMail,
                            SendTo = ToMail,
                            Fname = fname,
                            Lname = lname,
                            uEmail = email,
                            mSubject = subject,
                            mBody = body,                            
                            CreateDate = DateTime.Now,
                            LogMSG = ex.Message,
                            SendStatus = "Error"
                        };
                        db.LogMails.Add(mds);
                    }
                    int num = db.SaveChanges();

                }
               
                


                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region PostBuilding
        public JsonResult getPostType()
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = (from b in db.PostTypes
                              select b).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult getPostBuildingDetail(int BuildingTypeID, int PostBuildingID)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = db.spGetPostDetail(BuildingTypeID, PostBuildingID).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion PostBuilding
    }
}
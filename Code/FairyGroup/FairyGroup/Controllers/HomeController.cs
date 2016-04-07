using FairyGroup.Models;
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
        int p_PostBuildingID = 0;
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
        public ActionResult MaintenancePage()
        {
            return View();
        }
        public ActionResult Profile()
        {

            return View();
        }
        public ActionResult PostBuilding(int owner = 0, int PostBuildingID=0)
        {
            ViewBag.Owner = owner;
            //if (ViewBag.PostBuildingID == null) 
            p_PostBuildingID = PostBuildingID;
            ViewBag.PostBuildingID = PostBuildingID;
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
                    var md = db.spPostBuilding_List(BuildingTypeID, priceID, ProvinceID, DistrictID, keySearch, mindate.Year.ToString() + mindate.Month.ToString().PadLeft(2, '0') + mindate.Day.ToString().PadLeft(2, '0')).Where(x => x.PostStatus != null && x.PostStatus != "Draft" && x.PostStatus != "Cancel").ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult getPostBuildingByUser(int BuildingTypeID, int priceID, int ProvinceID, int DistrictID, string keySearch,int UserID)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    DateTime mindate = DateTime.Now.AddMonths(-3);
                    var md = db.spPostBuilding_ListByUser(BuildingTypeID, priceID, ProvinceID, DistrictID
                        , keySearch, mindate.Year.ToString() + mindate.Month.ToString().PadLeft(2, '0') + mindate.Day.ToString().PadLeft(2, '0'), UserID).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JsonResult getPostBuildingExclusive()
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    DateTime mindate = DateTime.Now.AddMonths(-3);
                    var md = db.spPostBuilding_List(0, 0, 0, 0, "", mindate.Year.ToString() + mindate.Month.ToString().PadLeft(2, '0') + mindate.Day.ToString().PadLeft(2, '0')).Where(x => x.PostStatus != null && x.PostStatus != "Draft" && x.PostStatus != "Cancel" && x.PriorityID < 5).Take(5).ToList();
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
                    PostBuildingDetailModel md = new PostBuildingDetailModel();
                    md.mdPostGroupDetail = db.spGetPostGroupDetail(BuildingTypeID, PostBuildingID).ToList();
                    md.mdPostDetail = db.spGetPostDetail(BuildingTypeID, PostBuildingID).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult getInitDataNewPost()
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    PostBuildingModel md = new PostBuildingModel();
                    if (ViewBag.PostBuildingID != null)
                    {
                        int id = ViewBag.PostBuildingID;
                        md.mdPost = db.PostBuildings.FirstOrDefault(x => x.PostBuildingID == id); 
                    }
                    else md.mdPost = new PostBuilding();
                    md.mdPostType = (from b in db.PostTypes
                                     select b).ToList();

                    md.mdBuildingType = (from b in db.BuildingTypes
                                        select b).ToList();

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
        public JsonResult getImagePost(int PostBuildingID)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = (from i in db.PostBuildingImgs
                                  where i.PostBuildingID ==  PostBuildingID
                                  select i).ToList();
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult SavePostHeader(PostBuilding mdH)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    if (mdH.PostBuildingID == 0)
                    {
                        var u = (from s in db.Users 
                              join c in db.UserTypes on s.UserTypeID  equals c.UserTypeID
                             where s.UserID == mdH.UserID
                            select c).SingleOrDefault();
                        mdH.CreateDate = DateTime.Now;
                        mdH.UpdateDate = DateTime.Now;
                        mdH.PriorityID = u.PriorityID;
                        var md = db.PostBuildings.Add(mdH);         
                        db.SaveChanges();
                        ViewBag.PostBuildingID = md.PostBuildingID;
                        return Json(md, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var md = db.PostBuildings.FirstOrDefault(x => x.PostBuildingID == mdH.PostBuildingID);
                        md.BuildingTypeID = mdH.BuildingTypeID;
                        md.PostTypeID = mdH.PostTypeID;
                        md.SalePrice = mdH.SalePrice;
                        md.ProvinceID = mdH.ProvinceID;
                        md.DistrictID = mdH.DistrictID;
                        md.SubDistrictID = mdH.SubDistrictID;                        
                        md.Mobile = mdH.Mobile;
                        md.Telephone = mdH.Telephone;
                        md.Description = mdH.Description;
                        md.PriorityID = mdH.PriorityID;
                        md.UpdateDate = mdH.UpdateDate;
                        md.DescriptionEn = mdH.DescriptionEn;
                        md.PostTitle = mdH.PostTitle;
                        md.PostTitleEn = mdH.PostTitleEn;
                        md.isOwner = mdH.isOwner;
                        md.AddressNo = mdH.AddressNo;
                        md.RoadName = mdH.RoadName;
                        md.LatitudeMap = mdH.LatitudeMap;
                        md.LongitudeMap = mdH.LongitudeMap;
                        md.UpdateDate = DateTime.Now;
                        db.SaveChanges();
                        ViewBag.PostBuildingID = md.PostBuildingID;
                        return Json(md, JsonRequestBehavior.AllowGet);
                    }
                    
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult SavePostDetail(spGetPostDetail_Result mdD)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    if (mdD.PostBuildingDetailValue == null) mdD.PostBuildingDetailValue = "";
                    if (mdD.PostBuildingDetailID == 0)
                    {
                        PostBuildingDetail md = new PostBuildingDetail();
                        md.PostBuildingID = mdD.PostBuildingID.Value;
                        md.BuildingDetailID = mdD.BuildingDetailID;
                        md.PostBuildingDetailValue = mdD.PostBuildingDetailValue.ToString();
                        var mds = db.PostBuildingDetails.Add(md);
                        db.SaveChanges();
                        mdD.PostBuildingID = md.PostBuildingID;
                        mdD.BuildingDetailID = md.BuildingDetailID;
                        mdD.PostBuildingDetailValue = md.PostBuildingDetailValue;
                        mdD.PostBuildingDetailID = md.PostBuildingDetailID;
                    }
                    else
                    {
                        var md = db.PostBuildingDetails.FirstOrDefault(x => x.PostBuildingDetailID == mdD.PostBuildingDetailID);
                        md.PostBuildingDetailValue = mdD.PostBuildingDetailValue.ToString();
                        db.SaveChanges();
                    }
                    return Json(mdD, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public JsonResult PostBuldingUpload(HttpPostedFileBase[] Postfiles,int id)
        {
            try
            {
                if (Postfiles != null)
                {

                    foreach (var file in Postfiles)
                    {
                        // Some browsers send file names with full path.
                        // We are only interested in the file name.
                        string dtf = DateTime.Now.ToString("yyMMddHHmmss") + "_";
                        var fileName = dtf + System.IO.Path.GetFileName(file.FileName);
                        var directoryPath = "";// /BuildingImg/2016/000001/
                        var savePath = "/BuildingImg/" + DateTime.Now.Year.ToString() + "/" + id.ToString();
                        if (!System.IO.Directory.Exists(Server.MapPath("~"+savePath)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("~" + savePath + "/"));
                        }
                        directoryPath = Server.MapPath("~" + savePath);
                        var physicalPath = System.IO.Path.Combine(directoryPath, fileName);

                        // The files are not actually saved in this demo
                        file.SaveAs(physicalPath);

                        using (var db = new FairyGroupEntities())
                        {
                            PostBuildingImg md = new PostBuildingImg();
                            md.PostBuildingID = id;
                            md.PathName = savePath + "/";
                            md.FileName = fileName;
                            md.CreateDate = DateTime.Now;
                            db.PostBuildingImgs.Add(md);
                            db.SaveChanges();
                        }
                    }
                }



                //if (ModelState.IsValid)
                //{
                //    var newProduct = Mapper.Map<ProductModel, Product>(model);
                //    _productRepository.CreateProduct(newProduct);
                //    _productRepository.SaveChanges();
                //}
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PostBuldingConfirm(int id)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {

                    var md = db.PostBuildings.FirstOrDefault(x => x.PostBuildingID == id);
                    md.PostStatus = "Sale";
                    db.SaveChanges();
                    ViewBag.PostBuildingID = md.PostBuildingID;
                    return Json(md, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult GetPostImg(int id)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    var md = (from im in db.PostBuildingImgs
                              where im.PostBuildingID == id
                              select im).ToList();
                   
                    return Json(md, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult DeleteImg(int ImgID)
        {

            try
            {
                using (var db = new FairyGroupEntities())
                {

                    var md = db.PostBuildingImgs.FirstOrDefault(x => x.PostBuildingImgID == ImgID);
                    db.PostBuildingImgs.Remove(md);
                    db.SaveChanges();
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion PostBuilding

        #region Regis
        public JsonResult SaveUser(User mdUser)
        {
            try
            {
                using (var db = new FairyGroupEntities())
                {
                    if (mdUser.UserID == 0)
                    {
                        var u = new User();
                        u.UserName = mdUser.UserName;
                        u.UserTypeID = mdUser.UserTypeID;
                        u.EmailAccount = mdUser.EmailAccount;
                        u.FirstNameEN = mdUser.FirstNameEN;
                        u.FirstNameTH = mdUser.FirstNameTH;
                        u.LastNameEN = mdUser.LastNameEN;
                        u.LastNameTH = mdUser.LastNameTH;
                        u.Mobile = mdUser.Mobile;
                        u.NickName = mdUser.NickName;
                        u.Password = mdUser.Password;
                        u.Phone = mdUser.Phone;
                        u.Sex = mdUser.Sex;
                        
                        var md = db.Users.Add(u);
                        db.SaveChanges();
                        return Json(md, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var md = db.Users.FirstOrDefault(x => x.UserID == mdUser.UserID);
                        md.UserName = mdUser.UserName;
                        md.UserTypeID = mdUser.UserTypeID;
                        md.EmailAccount = mdUser.EmailAccount;
                        md.FirstNameEN = mdUser.FirstNameEN;
                        md.FirstNameTH = mdUser.FirstNameTH;
                        md.LastNameEN = mdUser.LastNameEN;
                        md.LastNameTH = mdUser.LastNameTH;
                        md.Mobile = mdUser.Mobile;
                        md.NickName = mdUser.NickName;
                        //u.Password = mdUser.Password;
                        md.Phone = mdUser.Phone;
                        md.Sex = mdUser.Sex;                        
                        db.SaveChanges();
                        return Json(md, JsonRequestBehavior.AllowGet);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Regis

    }
}
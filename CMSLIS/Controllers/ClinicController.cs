using CMS_Core.Entity;
using CMSLIS.Controllers;
using CMSLIS.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Globalization;
using CMS_Core.Implementtion;
using CMSLIS.Common;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.IO;
using CMSNEW.Models;
using System.Web;

namespace CMSNEW.Controllers
{
    [CheckAuthorization]
    public class ClinicController : BaseController
    {
        ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");
        private ImpCMS_PATIENTINFO_APPOINTMENT impCMS_PATIENTINFO_APPOINTMENT;
        private ImpCMS_PATIENTINFO impCMS_PATIENTINFO;
        private ImpCMS_SERVICE_GROUP impCMS_SERVICE_GROUP;
        private ImpCMS_PATIENT impCMS_PATIENT;
        private IMPCMS_PATIENTINFO_TOTAL _PATIENTINFO_TOTAL;
        private ImpCMS_PATIENT_HISTORY impCMS_PATIENT_HISTORY;
        private ImpCMS_PATIENT_ALLERGY impCMS_PATIENT_ALLERGY;
        private ImpCMS_PATIENT_SERVICE_RESULT impCMS_PATIENT_SERVICE_RESULT;
        private ImpCMS_PATIENT_SERVICE impCMS_PATIENT_SERVICE;
        private ImpCMS_SERVICE impCMS_SERVICE;
        private ImpCMS_MEDICINE impCMS_MEDICINE;
        private ImpCMS_PATIENT_PRESCRIPTION impCMS_PATIENT_PRESCRIPTION;
        private ImpCMS_PATIENT_IMAGES impCMS_PATIENT_IMAGES;
        private ImpCMS_PATIENTINFO_QUEUE impCMS_PATIENTINFO_QUEUE;
        private ImpCMS_ROOM_CLINIC impCMS_ROOM_CLINIC;
        private ImpCMS_SERVICE_TYPE impCMS_SERVICE_TYPE;
        private ImpCMS_TEMPLATERESULTEXAMINATION impCMS_TEMPLATERESULTEXAMINATION;
        private ImpCMS_PATIENT_SERVICE_REFUND impCMS_PATIENT_SERVICE_REFUND;
        public ClinicController()
        {
            impCMS_PATIENTINFO_APPOINTMENT = new ImpCMS_PATIENTINFO_APPOINTMENT();
            impCMS_PATIENTINFO = new ImpCMS_PATIENTINFO();
            impCMS_SERVICE_GROUP = new ImpCMS_SERVICE_GROUP();
            impCMS_PATIENT = new ImpCMS_PATIENT();
            _PATIENTINFO_TOTAL = new IMPCMS_PATIENTINFO_TOTAL();
            impCMS_PATIENT_HISTORY = new ImpCMS_PATIENT_HISTORY();
            impCMS_PATIENT_ALLERGY = new ImpCMS_PATIENT_ALLERGY();
            impCMS_PATIENT_SERVICE_RESULT = new ImpCMS_PATIENT_SERVICE_RESULT();
            impCMS_PATIENT_SERVICE = new ImpCMS_PATIENT_SERVICE();
            impCMS_SERVICE = new ImpCMS_SERVICE();
            impCMS_PATIENT_PRESCRIPTION = new ImpCMS_PATIENT_PRESCRIPTION();
            impCMS_MEDICINE = new ImpCMS_MEDICINE();
            impCMS_PATIENT_IMAGES = new ImpCMS_PATIENT_IMAGES();
            impCMS_PATIENTINFO_QUEUE = new ImpCMS_PATIENTINFO_QUEUE();
            impCMS_ROOM_CLINIC = new ImpCMS_ROOM_CLINIC();
            impCMS_SERVICE_TYPE = new ImpCMS_SERVICE_TYPE();
            impCMS_TEMPLATERESULTEXAMINATION = new ImpCMS_TEMPLATERESULTEXAMINATION();
            impCMS_PATIENT_SERVICE_REFUND = new ImpCMS_PATIENT_SERVICE_REFUND();
        }


        // GET: Clinic
        public ActionResult Index()
        {
            return View();
        }

        #region --> Danh sách bệnh nhân   
        public ActionResult ListPatients()
        {
            // Initialization.  
            AddPageHeader("Danh sách bệnh nhân", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatients");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            //string a = "sdfsdf";
            //TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            //a = myTI.ToTitleCase(a);

            CMS_Core.Models.PatientViewModel patientViewModel = new CMS_Core.Models.PatientViewModel();
            patientViewModel.tungay = DateTime.Now.ToString("dd/MM/yyyy");
            patientViewModel.denngay = DateTime.Now.ToString("dd/MM/yyyy");
            patientViewModel.keyword = string.Empty;
            patientViewModel.TypeKeyword = 0;
            patientViewModel.partnerid = UserInfo.PARTNERID;

            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeyword();
                List<CMS_PATIENTINFO_TOTAL> _PATIENTINFO_TOTALS = _PATIENTINFO_TOTAL.GetCMS_PATIENTINFO_TOTALByALL(DateTime.Today, DateTime.Now.AddDays(1), string.Empty, 1, UserInfo.PARTNERID);
                ViewBag.PATIENTINFO_TOTALS = _PATIENTINFO_TOTALS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatients: " + ex.ToString());
            }

            // Info.  
            return View(patientViewModel);
        }

        [HttpPost]
        public ActionResult ListPatients(CMS_Core.Models.PatientViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách bệnh nhân", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatients");
            var UserInfo = ((cms_Account)Session["UserInfo"]);



            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();

            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(obj.tungay.Trim()))
                    Tungay = DateTime.ParseExact(obj.tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(obj.denngay.Trim()))
                    Denngay = DateTime.ParseExact(obj.denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

                if (Tungay > Denngay)
                {
                    TextErr = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày";
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                }
            }
            catch
            {
                TextErr = "Dữ liệu ngày tháng không đúng định dạng";
            }
            #endregion


            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeyword();
                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchListPatients":
                            if (string.IsNullOrEmpty(obj.keyword))
                            {
                                obj.keyword = string.Empty;
                            }
                            List<CMS_PATIENTINFO_TOTAL> _PATIENTINFO_TOTALS = _PATIENTINFO_TOTAL.GetCMS_PATIENTINFO_TOTALByALL(Tungay, Denngay.AddDays(1), obj.keyword.Trim(), obj.TypeKeyword, UserInfo.PARTNERID);
                            ViewBag.PATIENTINFO_TOTALS = _PATIENTINFO_TOTALS;
                            break;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = TextErr;
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatients: " + ex.ToString());
            }

            // Info.  
            return View(obj);
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListPatientDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/Clinic/ListPatientDelete");
            string listID = string.Empty;
            string listIDError = string.Empty;
            string result = string.Empty;
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        result = impCMS_PATIENTINFO.DeleteCMS_PATIENTINFO(Convert.ToInt32(customerID), UserInfo.PARTNERID);

                        if (!string.IsNullOrEmpty(result))
                        {
                            if (result.Equals(CMS_Core.Common.Constant.typeORAForeign.ToString()))
                            {
                                listIDError += customerID + ",";
                            }
                            else
                            {
                                AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                                listID += customerID + ",";
                            }
                        }
                        else
                        {
                            AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                            listID += customerID + ",";
                        }

                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }
                    if (listIDError.IndexOf(",") != -1)
                    {
                        listIDError = listIDError.Substring(0, listIDError.Length - 1);
                    }

                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListPatientDelete: " + ex.ToString());
                return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
            }

            if (listIDError.Length > 0)
            {
                return Json("Xóa thành công bản ghi có pid là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do bản thông tin bệnh nhân đã được sử dụng khám rồi!");
            }
            else
            {
                return Json("Xóa thành công bản ghi có pid là: " + listID);
            }


            // Info.  
            //return View();
        }


        #endregion

        #region --> Thêm hồ sơ bệnh nhân

        public ActionResult ListPatientsAdd(string pid)
        {
            // Initialization.             
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatients");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            CMS_Core.Models.PatientAddViewModel obj = new CMS_Core.Models.PatientAddViewModel();
            Session["SaveHISTORIE"] = null;
            List<CMS_PATIENTINFO> PATIENTINFOS = null;
            try
            {
                if (string.IsNullOrEmpty(pid))
                {
                    AddPageHeader("Thêm mới hồ sơ bệnh nhân", "");
                    AddBreadcrumb("Thêm mới hồ sơ bệnh nhân", "/Clinic/ListPatientsAdd");
                    ViewBag.Title = "Thêm mới hồ sơ bệnh nhân";
                    obj.PATIENTINFO = new CMS_PATIENTINFO();
                  
                    obj.PATIENTINFO.SEX = 0;
                    obj.PATIENTINFO.CITY = 377; //Tỉnh thành Hà Nội
                    obj.PATIENTINFO.DISTRICT = 0; //Tỉnh thành Hà Nội
                    obj.PATIENTINFO.STATUS = 2;
                    //obj.PATIENTINFO.IDENTIFICATION_DATEVIEW = DateTime.Now.ToString("dd/MM/yyyy");
                    //obj.PATIENTINFO.BIRTHDAYVIEW = DateTime.Now.ToString("dd/MM/yyyy");
                    //obj.PATIENTINFO.PASSPORT_DATEVIEW = DateTime.Now.ToString("dd/MM/yyyy");
                  
                    obj.PATIENTINFO.PID = 0;

                    obj.ALLERGGY = new CMS_PATIENT_ALLERGY();
                    obj.ALLERGGYS = new List<CMS_PATIENT_ALLERGY>();

                    obj.HISTORIE = new CMS_PATIENT_HISTORY();
                    obj.HISTORIE.SICKNAME = string.Empty;
                    obj.HISTORIES = new List<CMS_PATIENT_HISTORY>();


                }
                else
                {
                    AddPageHeader("Chỉnh sửa hồ sơ bệnh nhân", "");
                    AddBreadcrumb("Chỉnh sửa hồ sơ bệnh nhân", "/Clinic/ListPatientsAdd?PID=" + pid);
                    ViewBag.Title = "Chỉnh sửa hồ sơ bệnh nhân";

                    obj.HISTORIES = impCMS_PATIENT_HISTORY.GetCMS_PATIENT_HISTORYByPID(Convert.ToInt32(pid), UserInfo.PARTNERID);
                    obj.ALLERGGYS = impCMS_PATIENT_ALLERGY.GetCMS_PATIENT_ALLERGYByPID(Convert.ToInt32(pid), UserInfo.PARTNERID);
                    obj.ALLERGGY = new CMS_PATIENT_ALLERGY();
                    obj.ALLERGGY.ALLERGY_TYPE = 0;
                    obj.HISTORIE = new CMS_PATIENT_HISTORY();
                    obj.HISTORIE.SICKNAME = string.Empty;
                    obj.PATIENTS = impCMS_PATIENT.GetCMS_PATIENTByPID(pid, UserInfo.PARTNERID);


                    PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);

                    if (PATIENTINFOS != null)
                    {
                        if (PATIENTINFOS.Count > 0)
                        {
                            obj.PATIENTINFO = PATIENTINFOS[0];
                            // return View(PATIENTINFOS[0]);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }



                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListMedicine: " + ex.ToString());
            }

            // Info.  
            return View(obj);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListPatientsAdd(CMS_Core.Models.PatientAddViewModel obj, string submit)
        {
            try
            {
                // Initialization.     
                if (obj.PATIENTINFO.PID == 0)
                {
                    AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatients");
                    AddPageHeader("Thêm mới hồ sơ bệnh nhân", "");
                    AddBreadcrumb("Thêm mới hồ sơ bệnh nhân", "/Clinic/ListPatientsAdd");
                    ViewBag.Title = "Thêm mới hồ sơ bệnh nhân";
                }
                else
                {
                    AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatients");
                    AddPageHeader("Chỉnh sửa hồ sơ bệnh nhân", "");
                    AddBreadcrumb("Chỉnh sửa hồ sơ bệnh nhân", "/Clinic/ListPatientsAdd?pid=" + obj.PATIENTINFO.PID);
                    ViewBag.Title = "Chỉnh sửa hồ sơ bệnh nhân";
                }

                var errors = obj.PATIENTINFO.Validate(new ValidationContext(obj.PATIENTINFO));
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                if (errors.ToList().Count == 0)
                {
                    TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                    obj.PATIENTINFO.PATIENTNAME = myTI.ToTitleCase(obj.PATIENTINFO.PATIENTNAME);

                    if (!string.IsNullOrEmpty(obj.PATIENTIMAGE))
                    {
                        //String path = ControllerContext.HttpContext.Server.MapPath(@"~/Uploads/Patients/");
                        //path = path + DateTime.Now.ToString("yyyyMMddHHmm") + "_" + CMSLIS.Common.Common.getRandom() + "_" + CMSLIS.Common.Common.getNiceUrl(obj.PATIENTINFO.PATIENTNAME) + ".jpg";
                        //if (CMSLIS.Common.Common.Base64ToImage(obj.PATIENTINFO.PATIENTPATH, path))
                        //{
                        //    path = path.Substring(path.IndexOf("Uploads"));
                        //    path = path.Replace("\\", "/");
                        //    obj.PATIENTINFO.PATIENTIMAGE = path;
                        //}

                        String path = obj.PATIENTIMAGE;
                        // path = path.Substring(path.IndexOf("Uploads"));
                        path = path.Replace("\\", "/");
                        path = path.Replace("\"", "");
                        obj.PATIENTINFO.PATIENTIMAGE = path;

                    }


                    string result = string.Empty;
                    if (obj.PATIENTINFO.PID == 0)
                    {


                       
                        obj.PATIENTINFO.CREATEDATE = DateTime.Now;
                       
                      

                        result = impCMS_PATIENTINFO.InsertCMS_PATIENTINFO(obj.PATIENTINFO);
                        if (!string.IsNullOrEmpty(result))
                        {
                            List<CMS_PATIENT_HISTORY> _PATIENT_HISTORYs = new List<CMS_PATIENT_HISTORY>();

                            // Cập nhật tiền sử bệnh
                            if (Session["SaveHISTORIE"] != null)
                            {
                                _PATIENT_HISTORYs = (List<CMS_PATIENT_HISTORY>)Session["SaveHISTORIE"];
                                if (_PATIENT_HISTORYs != null)
                                {
                                    foreach (var data in _PATIENT_HISTORYs)
                                    {
                                        data.PARTNERID = UserInfo.PARTNERID;
                                        data.CREATEBY = UserInfo.accountid;
                                        data.CREATEDATE = DateTime.Now;
                                        data.PID = Convert.ToInt32(result);
                                        var errorsHistory = data.Validate(new ValidationContext(data));
                                        if (errorsHistory.ToList().Count == 0)
                                        {
                                            impCMS_PATIENT_HISTORY.InsertCMS_PATIENT_HISTORY(data);
                                        }
                                    }
                                }
                                Session["SaveHISTORIE"] = null;

                            }

                            // Cập nhật dị ứng
                            List<CMS_PATIENT_ALLERGY> _PATIENT_ALLERGYS = new List<CMS_PATIENT_ALLERGY>();


                            if (Session["SaveALLERGGY"] != null)
                            {
                                _PATIENT_ALLERGYS = (List<CMS_PATIENT_ALLERGY>)Session["SaveALLERGGY"];
                                if (_PATIENT_ALLERGYS != null)
                                {
                                    foreach (var data in _PATIENT_ALLERGYS)
                                    {
                                        data.PARTNERID = UserInfo.PARTNERID;
                                        data.CREATEBY = UserInfo.accountid;
                                        data.CREATEDATE = DateTime.Now;
                                        data.PID = Convert.ToInt32(result);
                                        var errorsAllergy = data.Validate(new ValidationContext(data));
                                        if (errorsAllergy.ToList().Count == 0)
                                        {
                                            impCMS_PATIENT_ALLERGY.InsertCMS_PATIENT_ALLERGY(data);
                                        }
                                    }
                                }
                                Session["SaveALLERGGY"] = null;
                            }



                            ViewBag.TitleSuccsess = "Thêm mới thông tin bệnh nhân thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                            Response.Redirect("/Clinic/ListPatients", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới thông tin bệnh nhân không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {


                        obj.PATIENTINFO.UPDATEBY = (Int16)UserInfo.accountid;
                        obj.PATIENTINFO.UPATEDATE = DateTime.Now;

                        result = impCMS_PATIENTINFO.UpdateCMS_PATIENTINFO(obj.PATIENTINFO);
                        if (!string.IsNullOrEmpty(result))
                        {
                            List<CMS_PATIENT_HISTORY> _PATIENT_HISTORYs = new List<CMS_PATIENT_HISTORY>();

                            // Cập nhật tiền sử bệnh
                            if (Session["SaveHISTORIE"] != null)
                            {
                                _PATIENT_HISTORYs = (List<CMS_PATIENT_HISTORY>)Session["SaveHISTORIE"];
                                if (_PATIENT_HISTORYs != null)
                                {
                                    foreach (var data in _PATIENT_HISTORYs)
                                    {
                                        if (data.ID == 0)
                                        {
                                            data.PARTNERID = UserInfo.PARTNERID;
                                            data.CREATEBY = UserInfo.accountid;
                                            data.CREATEDATE = DateTime.Now;
                                            data.PID = (int)obj.PATIENTINFO.PID;
                                            var errorsHistory = data.Validate(new ValidationContext(data));
                                            if (errorsHistory.ToList().Count == 0)
                                            {
                                                impCMS_PATIENT_HISTORY.InsertCMS_PATIENT_HISTORY(data);
                                            }
                                        }
                                        else
                                        {
                                            data.PARTNERID = UserInfo.PARTNERID;
                                            data.UPDATEBY = UserInfo.accountid;
                                            data.UPDATEDATE = DateTime.Now;
                                            data.PID = (int)obj.PATIENTINFO.PID;
                                            var errorsHistory = data.Validate(new ValidationContext(data));
                                            if (errorsHistory.ToList().Count == 0)
                                            {
                                                impCMS_PATIENT_HISTORY.UpdateCMS_PATIENT_HISTORY(data);
                                            }
                                        }
                                    }
                                }
                                Session["SaveHISTORIE"] = null;

                            }

                            // Cập nhật dị ứng
                            List<CMS_PATIENT_ALLERGY> _PATIENT_ALLERGYS = new List<CMS_PATIENT_ALLERGY>();

                            if (Session["SaveALLERGGY"] != null)
                            {
                                _PATIENT_ALLERGYS = (List<CMS_PATIENT_ALLERGY>)Session["SaveALLERGGY"];
                                if (_PATIENT_ALLERGYS != null)
                                {
                                    foreach (var data in _PATIENT_ALLERGYS)
                                    {
                                        if (data.ID == 0)
                                        {
                                            data.PARTNERID = UserInfo.PARTNERID;
                                            data.CREATEBY = UserInfo.accountid;
                                            data.CREATEDATE = DateTime.Now;
                                            data.PID = (int)obj.PATIENTINFO.PID;
                                            var errorsAllergy = data.Validate(new ValidationContext(data));
                                            if (errorsAllergy.ToList().Count == 0)
                                            {
                                                impCMS_PATIENT_ALLERGY.InsertCMS_PATIENT_ALLERGY(data);
                                            }
                                        }
                                        else
                                        {
                                            data.PARTNERID = UserInfo.PARTNERID;
                                            data.UPDATEBY = UserInfo.accountid;
                                            data.UPDATEDATE = DateTime.Now;
                                            data.PID = (int)obj.PATIENTINFO.PID;
                                            var errorsAllergy = data.Validate(new ValidationContext(data));
                                            if (errorsAllergy.ToList().Count == 0)
                                            {
                                                impCMS_PATIENT_ALLERGY.UpdateCMS_PATIENT_ALLERGY(data);
                                            }
                                        }
                                    }
                                }
                                Session["SaveALLERGGY"] = null;
                            }

                            ViewBag.TitleSuccsess = "Cập nhật thông tin bệnh nhân thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/Clinic/ListPatients", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin bệnh nhân không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = errors.ToList()[0].ErrorMessage;
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientsAdd: " + ex.ToString());
            }
            return View(obj);
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult SaveHISTORIE(string SICKNAME, string SICKYEAR, string NOTE, int id)
        {
            try
            {
                CMS_PATIENT_HISTORY _PATIENT_HISTORY = new CMS_PATIENT_HISTORY();
                List<CMS_PATIENT_HISTORY> _PATIENT_HISTORYs = new List<CMS_PATIENT_HISTORY>();

                if (Session["SaveHISTORIE"] != null)
                {
                    _PATIENT_HISTORYs = (List<CMS_PATIENT_HISTORY>)Session["SaveHISTORIE"];
                }

                if (!string.IsNullOrEmpty(SICKNAME))
                {
                    _PATIENT_HISTORY.SICKNAME = SICKNAME;
                    _PATIENT_HISTORY.SICKYEAR = Convert.ToInt32(SICKYEAR);
                    _PATIENT_HISTORY.NOTE = NOTE;
                    if (id > 0)
                    {
                        _PATIENT_HISTORY.ID = id;
                    }
                    if (_PATIENT_HISTORYs.Find(x => x.SICKNAME.Equals(SICKNAME) && x.SICKYEAR == Convert.ToInt32(SICKYEAR) && id == 0) != null)
                    {
                        return Json("2");
                    }
                    else
                    {
                        _PATIENT_HISTORYs.Add(_PATIENT_HISTORY);
                        Session["SaveHISTORIE"] = _PATIENT_HISTORYs;
                        return Json("1");
                    }
                }
                // Info.  
                return Json("0");
            }
            catch
            {
                return Json("0");
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult SaveALLERGGY(string ALLERGY_NAME, string ALLERGY_TYPE, string ALLERGY_NOTE, int id)
        {
            try
            {
                CMS_PATIENT_ALLERGY _PATIENT_ALLERGY = new CMS_PATIENT_ALLERGY();

                List<CMS_PATIENT_ALLERGY> _PATIENT_ALLERGYS = new List<CMS_PATIENT_ALLERGY>();

                if (Session["SaveALLERGGY"] != null)
                {
                    _PATIENT_ALLERGYS = (List<CMS_PATIENT_ALLERGY>)Session["SaveALLERGGY"];
                }

                if (!string.IsNullOrEmpty(ALLERGY_NAME))
                {
                    _PATIENT_ALLERGY.ALLERGY_NAME = ALLERGY_NAME;
                    _PATIENT_ALLERGY.ALLERGY_TYPE = Convert.ToInt32(ALLERGY_TYPE);
                    _PATIENT_ALLERGY.ALLERGY_NOTE = ALLERGY_NOTE;
                    if (id > 0)
                    {
                        _PATIENT_ALLERGY.ID = id;
                    }
                    if (_PATIENT_ALLERGYS.Find(x => x.ALLERGY_NAME.Equals(ALLERGY_NAME) && x.ALLERGY_TYPE == Convert.ToInt32(ALLERGY_TYPE)) != null && id == 0)
                    {
                        return Json("2");
                    }
                    else
                    {
                        _PATIENT_ALLERGYS.Add(_PATIENT_ALLERGY);
                        Session["SaveALLERGGY"] = _PATIENT_ALLERGYS;
                        return Json("1");
                    }
                }
                // Info.  
                return Json("0");
            }
            catch
            {
                return Json("0");
            }
        }



        [HttpPost]
        public ActionResult Capture(string pid, string sid, FormCollection form)
        {
            string Pic = string.Empty;
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                bool checkExitPid = false;
                if (string.IsNullOrEmpty(pid))
                    pid = "0";

                List<CMS_PATIENTINFO> PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);
                if (PATIENTINFOS != null)
                {
                    if (PATIENTINFOS.Count > 0)
                    {
                        checkExitPid = true;
                    }
                }

                if (checkExitPid)
                {
                    if (string.IsNullOrEmpty(sid))
                        sid = "0";

                    var file = Request.Files[0];

                    if (file.ContentLength > 0)
                    {
                        Pic = string.Format("~/Uploads/Patients/{0}", pid + "_" + sid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
                        file.SaveAs(HttpContext.Server.MapPath(Pic));

                    }
                }
                else
                {
                    return Json("0");
                }
            }
            catch { }

            return Json(Pic.Replace("~/", "/"));


        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult CaptureImage(string base64image, string pid, string sid, string serviceid)
        {
            string Pic = string.Empty;
            //  IEnumerable<CMS_PATIENT_IMAGES> modelList = new List<CMS_PATIENT_IMAGES>();

            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                bool checkExitPid = false;
                if (string.IsNullOrEmpty(pid))
                    pid = "0";

                List<CMS_PATIENTINFO> PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);
                if (PATIENTINFOS != null)
                {
                    if (PATIENTINFOS.Count > 0)
                    {
                        checkExitPid = true;
                    }
                }

                if (checkExitPid)
                {
                    if (string.IsNullOrEmpty(sid))
                        sid = "0";
                    if (string.IsNullOrEmpty(serviceid))
                        serviceid = "0";


                    CMS_PATIENT_IMAGES _PATIENT_IMAGES = new CMS_PATIENT_IMAGES();
                    _PATIENT_IMAGES.CREATDATE = DateTime.Now;
                    _PATIENT_IMAGES.CREATEBY = UserInfo.accountid;
                    _PATIENT_IMAGES.PARTNERID = UserInfo.PARTNERID;
                    _PATIENT_IMAGES.PID = Convert.ToInt32(pid);
                    _PATIENT_IMAGES.PATIENT_ID = Convert.ToInt32(sid);
                    _PATIENT_IMAGES.SERVICE_ID = Convert.ToInt32(serviceid);


                    if (!string.IsNullOrEmpty(base64image))
                    {
                        Pic = string.Format("~/Uploads/Patients/{0}", pid + "_" + sid + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
                        CMSLIS.Common.Common.Base64ToImage(base64image, HttpContext.Server.MapPath(Pic));
                    }
                    _PATIENT_IMAGES.PATHIMAGE = Pic.Replace("~/", "/");
                    var errors = _PATIENT_IMAGES.Validate(new ValidationContext(_PATIENT_IMAGES));
                    if (errors.ToList().Count == 0)
                    {
                        string result = impCMS_PATIENT_IMAGES.Add(_PATIENT_IMAGES, UserInfo.PARTNERID);
                        List<CMS_PATIENT_IMAGES> _IMAGES = new List<CMS_PATIENT_IMAGES>();
                        _IMAGES.Add(impCMS_PATIENT_IMAGES.Get(Convert.ToInt32(result), UserInfo.PARTNERID));
                        return Json(_IMAGES, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                logError.Info("CaptureImage:" + ex.ToString());
                return Json("0");
            }

            return Json("0");


        }


        public ActionResult ListPatientsPrint(string pid, string Token)
        {
            // Initialization.             
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Thông tin bệnh nhân", "/Clinic/ListPatientsPrint");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            CMS_PATIENTINFO PATIENTINFO = new CMS_PATIENTINFO();
            ImpCMS_TEMPLATEPRINT impCMS_TEMPLATEPRINT = new ImpCMS_TEMPLATEPRINT();


            try
            {
                if (CMSLIS.Common.Common.CheckKeyPrivate(pid, Token))
                {
                    //Lấy thông tin header
                    List<CMS_TEMPLATEPRINT> _TEMPLATEPRINTS = impCMS_TEMPLATEPRINT.GetCMS_TEMPLATEPRINT_BYTYPE(1, UserInfo.PARTNERID);

                    if (_TEMPLATEPRINTS != null)
                    {
                        if (_TEMPLATEPRINTS.Count > 0)
                        {
                            ViewBag.TemplateHeader = _TEMPLATEPRINTS[0].TEMPLATE;
                        }
                    }

                    //Lấy thông tin footer
                    _TEMPLATEPRINTS = impCMS_TEMPLATEPRINT.GetCMS_TEMPLATEPRINT_BYTYPE(2, UserInfo.PARTNERID);

                    if (_TEMPLATEPRINTS != null)
                    {
                        if (_TEMPLATEPRINTS.Count > 0)
                        {
                            ViewBag.TemplateFooter = _TEMPLATEPRINTS[0].TEMPLATE;
                        }
                    }

                    List<CMS_PATIENTINFO> PATIENTINFOS = null;

                    PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);

                    if (PATIENTINFOS != null)
                    {
                        if (PATIENTINFOS.Count > 0)
                        {
                            // return View(PATIENTINFOS[0]);

                            string footer = "--footer-right \" [page]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
                            return new Rotativa.MVC.PartialViewAsPdf(PATIENTINFOS[0])
                            {
                                RotativaOptions = new Rotativa.Core.DriverOptions()
                                {
                                    // PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                                    PageOrientation = Rotativa.Core.Options.Orientation.Portrait,
                                    PageSize = Rotativa.Core.Options.Size.A4,
                                    // PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                                    CustomSwitches = footer
                                },

                            };
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Dữ liệu nhập vào không chính xác, Mời bạn kiểm tra lại!";
                }

            }
            catch (Exception ex)
            {
                logError.Info("ListPatientsPrint:" + ex.ToString());
                throw;
            }

            return View(PATIENTINFO);

        }

        //public ActionResult ListPatientsHistory(string pid)
        //{
        //    // Initialization.             
        //    AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
        //    AddBreadcrumb("Lịch sử khám bệnh", "/Clinic/ListPatientsHistory");

        //    List<CMS_PATIENT> _PATIENTS  = null;
        //    try
        //    {
        //        var UserInfo = ((cms_Account)Session["UserInfo"]);

        //       Imp

        //        _PATIENTS = impCMS_PARTNER.

        //        if (PATIENTINFOS != null)
        //        {
        //            if (PATIENTINFOS.Count > 0)
        //            {
        //                string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
        //                return new Rotativa.MVC.PartialViewAsPdf(PATIENTINFOS[0])
        //                {
        //                    RotativaOptions = new Rotativa.Core.DriverOptions()
        //                    {
        //                        PageOrientation = Rotativa.Core.Options.Orientation.Landscape,
        //                        CustomSwitches = footer
        //                    },

        //                };
        //            }
        //            else
        //            {
        //                ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        logError.Info("ThongKeBanHangPrint:" + ex.ToString());
        //        throw;
        //    }

        //    return View(PATIENTINFO);

        //}




        #endregion

        #region --> Đăng ký khám bệnh

        public ActionResult ListPatientsCreateOrder(string pid, string mid, string sid) // pid là id bệnh nhân, mid là id đặt lịch
        {
            // Initialization.       
            AddPageHeader("Đăng ký khám", "");
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách đặt lịch", "/Clinic/MedicalAppointment");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            CMS_Core.Models.PatientAddViewModel obj = new CMS_Core.Models.PatientAddViewModel();

            try
            {
                Session["SaveALLERGGY1"] = null;
                Session["SaveHISTORIE1"] = null;

                if (!string.IsNullOrEmpty(pid))
                {
                    obj = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_CreateOrderBYPID(Convert.ToInt64(pid), UserInfo.PARTNERID);


                    // List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);
                    if (obj.PATIENTINFO != null)
                    {
                        if (obj.PATIENTINFO.PID > 0)
                        {
                            // obj.PATIENTINFO = _PATIENTINFOS[0];

                            // obj.HISTORIES = impCMS_PATIENT_HISTORY.GetCMS_PATIENT_HISTORYByPID(Convert.ToInt64(pid), UserInfo.PARTNERID);
                            //  obj.ALLERGGYS = impCMS_PATIENT_ALLERGY.GetCMS_PATIENT_ALLERGYByPID(Convert.ToInt64(pid), UserInfo.PARTNERID);
                            obj.ALLERGGY = new CMS_PATIENT_ALLERGY();
                            obj.ALLERGGY.ALLERGY_TYPE = 0;

                            obj.HISTORIE = new CMS_PATIENT_HISTORY();
                            obj.HISTORIE.SICKNAME = string.Empty;
                            // obj.PATIENTS = impCMS_PATIENT.GetCMS_PATIENTByPID(pid, UserInfo.PARTNERID);
                            obj.PATIENT = new CMS_PATIENT();
                            obj.PATIENT.OBJECTID = 0;
                            obj.PATIENT.LOCATIONID = 0;
                            obj.PATIENT.DEPARTMENTSID = 0;
                            obj.PATIENT.TYPECUSTOMERID = 0;
                            obj.PATIENT.SERVICE_GROUPID = 0;
                            obj.PATIENT.SERVICEID = string.Empty;
                            obj.PATIENT.DISCOUNT = 0;
                            obj.SERVICES = new List<CMS_SERVICE>();
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân, Mời bạn thực hiện lại";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân, Mời bạn thực hiện lại";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else if (!string.IsNullOrEmpty(mid))
                {

                    CMS_PATIENTINFO_APPOINTMENT _PATIENTINFO_APPOINTMENT = impCMS_PATIENTINFO_APPOINTMENT.Get(Convert.ToInt32(mid), UserInfo.PARTNERID);

                    if (_PATIENTINFO_APPOINTMENT != null)
                    {
                        //Nếu có thông tin PID bệnh nhân thì lấy thông tin the PID bệnh nhân
                        try
                        {
                            if (_PATIENTINFO_APPOINTMENT.PATIENTINFO_ID > 0)
                            {
                                obj = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_CreateOrderBYPID(Convert.ToInt64(_PATIENTINFO_APPOINTMENT.PATIENTINFO_ID), UserInfo.PARTNERID);
                                if (obj.PATIENTINFO != null)
                                {
                                    if (obj.PATIENTINFO.PID > 0)
                                    {

                                        obj.ALLERGGY = new CMS_PATIENT_ALLERGY();
                                        obj.ALLERGGY.ALLERGY_TYPE = 0;

                                        obj.HISTORIE = new CMS_PATIENT_HISTORY();
                                        obj.HISTORIE.SICKNAME = string.Empty;

                                        obj.PATIENT = new CMS_PATIENT();
                                        obj.PATIENT.OBJECTID = 0;
                                        obj.PATIENT.LOCATIONID = 0;
                                        obj.PATIENT.DEPARTMENTSID = 0;
                                        obj.PATIENT.TYPECUSTOMERID = 0;
                                        obj.PATIENT.SERVICE_GROUPID = 0;
                                        obj.PATIENT.SERVICEID = string.Empty;
                                        obj.PATIENT.DISCOUNT = 0;
                                        obj.SERVICES = new List<CMS_SERVICE>();
                                    }
                                    else
                                    {
                                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân, Mời bạn thực hiện lại";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    }
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân, Mời bạn thực hiện lại";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                obj.PATIENTINFO = impCMS_PATIENTINFO_APPOINTMENT.CONVERT_APPOINTMENT_TO_PATIENTINFO(_PATIENTINFO_APPOINTMENT);

                                obj.HISTORIES = new List<CMS_PATIENT_HISTORY>();
                                obj.ALLERGGYS = new List<CMS_PATIENT_ALLERGY>();
                                obj.ALLERGGY = new CMS_PATIENT_ALLERGY();
                                obj.ALLERGGY.ALLERGY_TYPE = 0;


                                obj.HISTORIE = new CMS_PATIENT_HISTORY();
                                obj.HISTORIE.SICKNAME = string.Empty;
                                obj.PATIENTS = new List<CMS_PATIENT>();
                                obj.PATIENT = new CMS_PATIENT();
                                obj.PATIENT.OBJECTID = 0;
                                obj.PATIENT.LOCATIONID = 0;

                                obj.PATIENT.DEPARTMENTSID = _PATIENTINFO_APPOINTMENT.DEPARTMENTSID;
                                obj.PATIENT.TYPECUSTOMERID = 0;
                                obj.PATIENT.SERVICE_GROUPID = 0;
                                obj.PATIENT.SERVICEID = string.Empty;
                                obj.PATIENT.DISCOUNT = 0;
                                obj.SERVICES = new List<CMS_SERVICE>();
                            }

                        }
                        catch { }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân, Mời bạn thực hiện lại";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }


                }
                else if (!string.IsNullOrEmpty(sid))
                {

                    obj = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_CreateOrderBYSID(Convert.ToInt64(sid), UserInfo.PARTNERID);


                    // List<CMS_PATIENT> _PATIENTS = impCMS_PATIENT.GetCMS_PATIENT_BYSID(sid, UserInfo.PARTNERID);
                    if (obj.PATIENT != null)
                    {
                        if (obj.PATIENT.SID > 0)
                        {
                            //obj.PATIENT.SUMMONEY = obj.SERVICES[0].SERVICE_GROUP_PRICE;
                            obj.PATIENT.MONEYGIVE = 0;
                            obj.PATIENT.MONEYREFUNDS = 0;

                            //if(obj.PATIENT.SERVICE_GROUPID != 0)
                            //{
                            //    obj.PATIENT.SUMMONEY = obj.SERVICES
                            //}

                            //  obj.PATIENT.SUMMONEY = obj.SERVICES[0].SERVICE_GROUP_PRICE;
                            //   obj.PATIENT.SUMMONEYPAY = obj.SERVICES[0].SERVICE_GROUP_PRICE;


                            //if ((_PATIENTS[0].to ?? 0) > 0)
                            //{
                            //    ViewBag.TitleSuccsess = "Mã KCB này đã thanh toán rồi, không thể sửa được. Mời bạn kiểm tra lại!";
                            //}
                            //else
                            //{
                            // obj.PATIENT = _PATIENTS[0];
                            if (string.IsNullOrEmpty(obj.PATIENT.SERVICEID))
                            {
                                obj.PATIENT.SERVICEID = string.Empty;
                            }
                            // List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(_PATIENTS[0].PID.ToString(), UserInfo.PARTNERID);
                            if (obj.PATIENTINFO != null)
                            {
                                if (obj.PATIENTINFO.PID > 0)
                                {
                                    // obj.PATIENTINFO = _PATIENTINFOS[0];

                                    // obj.HISTORIES = impCMS_PATIENT_HISTORY.GetCMS_PATIENT_HISTORYByPID(obj.PATIENTINFO.PID ?? 0, UserInfo.PARTNERID);
                                    // obj.ALLERGGYS = impCMS_PATIENT_ALLERGY.GetCMS_PATIENT_ALLERGYByPID(obj.PATIENTINFO.PID ?? 0, UserInfo.PARTNERID);
                                    obj.ALLERGGY = new CMS_PATIENT_ALLERGY();
                                    obj.ALLERGGY.ALLERGY_TYPE = 0;
                                    obj.HISTORIE = new CMS_PATIENT_HISTORY();
                                    obj.HISTORIE.SICKNAME = string.Empty;
                                    // obj.PATIENTS = impCMS_PATIENT.GetCMS_PATIENTByPID(obj.PATIENTINFO.PID.ToString(), UserInfo.PARTNERID);


                                    // obj.SERVICES = new List<CMS_SERVICE>();
                                    // obj.SERVICES = impCMS_SERVICE.GetCMS_SERVICEBySID(obj.PATIENT.SID, UserInfo.PARTNERID);


                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân, Mời bạn thực hiện lại";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                obj.PATIENTINFO.PID = 0;
                                ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân, Mời bạn thực hiện lại";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                            // }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám, Mời bạn thực hiện lại";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám, Mời bạn thực hiện lại";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }

                }
                else
                {
                    obj.PATIENTINFO = new CMS_PATIENTINFO();
                    obj.PATIENTINFO.PARTNERID = UserInfo.PARTNERID;
                    obj.PATIENTINFO.SEX = 0;
                    obj.PATIENTINFO.CITY = 377; //Tỉnh thành Hà Nội
                    obj.PATIENTINFO.DISTRICT = 0; //Tỉnh thành Hà Nội
                    obj.PATIENTINFO.STATUS = 2;
                    // obj.PATIENTINFO.IDENTIFICATION_DATEVIEW = DateTime.Now.ToString("dd/MM/yyyy");
                    // obj.PATIENTINFO.BIRTHDAYVIEW = DateTime.Now.ToString("dd/MM/yyyy");
                    // obj.PATIENTINFO.PASSPORT_DATEVIEW = DateTime.Now.ToString("dd/MM/yyyy");
                 
                    obj.PATIENTINFO.PID = 0;

                    obj.ALLERGGY = new CMS_PATIENT_ALLERGY();
                    obj.ALLERGGYS = new List<CMS_PATIENT_ALLERGY>();

                    obj.HISTORIE = new CMS_PATIENT_HISTORY();
                    obj.HISTORIE.SICKNAME = string.Empty;
                    obj.HISTORIES = new List<CMS_PATIENT_HISTORY>();

                    obj.PATIENT = new CMS_PATIENT();
                    obj.PATIENT.OBJECTID = 0;
                    obj.PATIENT.LOCATIONID = 0;
                    obj.PATIENT.DEPARTMENTSID = 0;
                    obj.PATIENT.TYPECUSTOMERID = 0;
                    obj.PATIENT.SERVICE_GROUPID = 0;
                    obj.PATIENT.SERVICEID = string.Empty;
                    obj.PATIENT.DISCOUNT = 0;
                    obj.SERVICES = new List<CMS_SERVICE>();
                }


            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientsCreateOrder: " + ex.ToString());
            }
            // Xóa chọn dịch vụ
            Session["ListPatientsByService0"] = null;
            return View(obj);

        }

        [HttpPost]
        public ActionResult ListPatientsCreateOrder(CMS_Core.Models.PatientAddViewModel obj, string submit) // pid là id bệnh nhân, mid là id đặt lịch
        {

            // Initialization.     
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách đặt lịch", "/Clinic/MedicalAppointment");
            AddPageHeader("Đăng ký khám bệnh", "");
            ViewBag.Title = "Đăng ký khám bệnh";
            string SERVICEID = string.Empty;
            bool checkLocation = false;
            string textCheckLocation = string.Empty;
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {
                string serviceGroupID = obj.PATIENT.SERVICE_GROUPID.ToString();
                if (obj.PATIENT.SERVICE_GROUPID != 0)
                {
                    // lấy danh sách dịch vụ trong nhóm dịch vụ
                    obj.SERVICES = impCMS_SERVICE.GetAllCMS_SERVICE_CHECKBYGROUPSEVICEID(obj.PATIENT.SERVICE_GROUPID, UserInfo.PARTNERID);
                }
                else
                if (obj.PATIENT.mulSERVICEID != null) // trường hợp chọn dịch vụ riêng lẻ
                {
                    List<CMS_SERVICE> _SERVICESChoi = new List<CMS_SERVICE>();
                    string[] SERVICES = obj.PATIENT.mulSERVICEID;
                    foreach (string service in SERVICES)
                    {
                        SERVICEID = SERVICEID + service + ",";
                        List<CMS_SERVICE> datas = impCMS_SERVICE.GetCMS_SERVICEByID(Convert.ToInt32(service), UserInfo.PARTNERID);
                        if (datas != null)
                        {
                            if (datas.Count > 0)
                            {
                                _SERVICESChoi.Add(datas[0]);
                            }
                        }
                    }
                    obj.SERVICES = _SERVICESChoi;
                }

                //Check LISTLOCATIONID
                if (obj.PATIENT.LISTLOCATIONID != null)
                {
                    string[] LOCATIONIDS = obj.PATIENT.LISTLOCATIONID;
                    foreach (string LOCATIONID in LOCATIONIDS)
                    {
                        obj.PATIENT.LOCATIONIDNEW = obj.PATIENT.LOCATIONIDNEW + LOCATIONID + ",";
                        checkLocation = false;
                        foreach (var service in obj.SERVICES)
                        {
                            if (impCMS_ROOM_CLINIC.CheckRoomAndService(Convert.ToInt32(LOCATIONID), service.ID, service.SERVICETYPE, UserInfo.PARTNERID))
                            {
                                checkLocation = true;
                                break;
                            }
                        }
                        if (!checkLocation)
                        {
                            textCheckLocation = textCheckLocation + " Mời bạn dịch vụ cho phòng: " + impCMS_ROOM_CLINIC.Get(Convert.ToInt32(LOCATIONID), UserInfo.PARTNERID).ROOM_NAME + ",";
                        }

                    }

                    if (textCheckLocation.Length > 0)
                    {
                        textCheckLocation = textCheckLocation.Substring(0, textCheckLocation.Length - 1);
                    }


                    if (obj.PATIENT.LOCATIONIDNEW.Length > 0)
                    {
                        obj.PATIENT.LOCATIONIDNEW = obj.PATIENT.LOCATIONIDNEW.Substring(0, obj.PATIENT.LOCATIONIDNEW.Length - 1);
                    }

                }
                else
                {
                    obj.PATIENT.LOCATIONIDNEW = string.Empty;
                }

                if (SERVICEID.Length > 0)
                {
                    SERVICEID = SERVICEID.Substring(0, SERVICEID.Length - 1);
                }

                //Kiểm tra xem đã chọn phòng chưa?
                if (obj.PATIENT.LOCATIONIDNEW.Length > 0)
                {
                    //Kiểm tra xem có dịch vụ chưa?
                    if (serviceGroupID != "0" || SERVICEID.Length > 0)
                    {
                        // Dịch vù và phòng khớp nhau thì tiến hành đăng ký khám bệnh
                        if (textCheckLocation.Length == 0)
                        {
                            string result = string.Empty;
                            if (obj.PATIENTINFO.PID == null)
                                obj.PATIENTINFO.PID = 0;
                            // nếu chưa có pid bệnh nhân thì tạo mới pid cho bệnh nhân
                            if (obj.PATIENTINFO.PID == 0)
                            {
                                var errorsSave = obj.PATIENTINFO.Validate(new ValidationContext(obj.PATIENTINFO));
                                if (errorsSave.ToList().Count == 0)
                                {

                                    obj.PATIENTINFO.CREATEBY = (Int16)UserInfo.accountid;
                                    obj.PATIENTINFO.CREATEDATE = DateTime.Now;
                                    obj.PATIENTINFO.PARTNERID = UserInfo.PARTNERID;
                                    obj.PATIENTINFO.partnerid_Code = UserInfo.PARTNER_CODE;
                                    result = impCMS_PATIENTINFO.InsertCMS_PATIENTINFO(obj.PATIENTINFO);

                                    //Nhập lịch sử bệnh cho bệnh nhân mới
                                    if (Session["SaveHISTORIE1"] != null)
                                    {
                                        List<CMS_PATIENT_HISTORY> _PATIENT_HISTORYs = (List<CMS_PATIENT_HISTORY>)Session["SaveHISTORIE"];
                                        if (_PATIENT_HISTORYs != null)
                                        {
                                            foreach (var data in _PATIENT_HISTORYs)
                                            {
                                                data.PARTNERID = UserInfo.PARTNERID;
                                                data.CREATEBY = UserInfo.accountid;
                                                data.CREATEDATE = DateTime.Now;
                                                data.PID = Convert.ToInt32(result);
                                                var errorsHistory = data.Validate(new ValidationContext(data));
                                                if (errorsHistory.ToList().Count == 0)
                                                {
                                                    impCMS_PATIENT_HISTORY.InsertCMS_PATIENT_HISTORY(data);
                                                }
                                            }
                                        }
                                        Session["SaveHISTORIE1"] = null;

                                    }

                                    // Cập nhật dị ứng
                                    List<CMS_PATIENT_ALLERGY> _PATIENT_ALLERGYS = new List<CMS_PATIENT_ALLERGY>();

                                    if (Session["SaveALLERGGY1"] != null)
                                    {
                                        _PATIENT_ALLERGYS = (List<CMS_PATIENT_ALLERGY>)Session["SaveALLERGGY1"];
                                        if (_PATIENT_ALLERGYS != null)
                                        {
                                            foreach (var data in _PATIENT_ALLERGYS)
                                            {
                                                if (data.ID == 0)
                                                {
                                                    data.PARTNERID = UserInfo.PARTNERID;
                                                    data.CREATEBY = UserInfo.accountid;
                                                    data.CREATEDATE = DateTime.Now;
                                                    data.PID = (int)obj.PATIENTINFO.PID;
                                                    var errorsAllergy = data.Validate(new ValidationContext(data));
                                                    if (errorsAllergy.ToList().Count == 0)
                                                    {
                                                        impCMS_PATIENT_ALLERGY.InsertCMS_PATIENT_ALLERGY(data);
                                                    }
                                                }
                                                else
                                                {
                                                    data.PARTNERID = UserInfo.PARTNERID;
                                                    data.UPDATEBY = UserInfo.accountid;
                                                    data.UPDATEDATE = DateTime.Now;
                                                    data.PID = (int)obj.PATIENTINFO.PID;
                                                    var errorsAllergy = data.Validate(new ValidationContext(data));
                                                    if (errorsAllergy.ToList().Count == 0)
                                                    {
                                                        impCMS_PATIENT_ALLERGY.UpdateCMS_PATIENT_ALLERGY(data);
                                                    }
                                                }
                                            }
                                        }
                                        Session["SaveALLERGGY1"] = null;
                                    }



                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = errorsSave.ToList()[0].ErrorMessage;
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    return View(obj);
                                }
                            }
                            else
                            {
                                result = obj.PATIENTINFO.PID.ToString();
                            }

                            if (!string.IsNullOrEmpty(result))
                            {
                                string SIDPATIENT = string.Empty;

                                // Nếu chưa có phiếu khám thì tạo phiếu mới
                                if (obj.PATIENT.SID == 0)
                                {
                                    var errorsSave = obj.PATIENT.Validate(new ValidationContext(obj.PATIENT));
                                    if (errorsSave.ToList().Count == 0)
                                    {
                                        obj.PATIENT.PARTNERID = UserInfo.PARTNERID;
                                        obj.PATIENT.PID = Convert.ToInt32(result);
                                        obj.PATIENT.DATEIN = DateTime.Now;
                                        obj.PATIENT.RETURNRESULT = 0;
                                        obj.PATIENT.RETURNTYPE = 0;
                                        obj.PATIENT.PRINTOUT = 0;
                                        obj.PATIENT.SERVICEID = SERVICEID;
                                        obj.PATIENT.DEPARTMENTSID = 0;
                                        obj.PATIENT.LOCATIONID = 0;
                                        if ((obj.PATIENT.SPOTCASH > 0 && obj.PATIENT.MONEYPOS > 0) || obj.PATIENT.MONEYGIVE > 0)
                                        {
                                            obj.PATIENT.INVOICE = UserInfo.Username;
                                            obj.PATIENT.INVOICETIME = DateTime.Now;
                                        }
                                        SIDPATIENT = impCMS_PATIENT.InsertCMS_PATIENT(obj.PATIENT);
                                    }
                                    else
                                    {
                                        ViewBag.TitleSuccsess = errorsSave.ToList()[0].ErrorMessage;
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                        return View(obj);
                                    }
                                }
                                else  //Cập nhật phiếu cũ
                                {
                                    obj.PATIENT.PARTNERID = UserInfo.PARTNERID;
                                    obj.PATIENT.PID = Convert.ToInt32(result);
                                    obj.PATIENT.DATEIN = DateTime.Now;
                                    obj.PATIENT.RETURNRESULT = 0;
                                    obj.PATIENT.RETURNTYPE = 0;
                                    obj.PATIENT.PRINTOUT = 0;
                                    obj.PATIENT.SERVICEID = SERVICEID;
                                    obj.PATIENT.DEPARTMENTSID = 0;
                                    obj.PATIENT.LOCATIONID = 0;


                                    SIDPATIENT = obj.PATIENT.SID.ToString();
                                    if ((obj.PATIENT.SPOTCASH > 0 && obj.PATIENT.MONEYPOS > 0) || obj.PATIENT.MONEYGIVE > 0)
                                    {
                                        obj.PATIENT.INVOICE = UserInfo.Username;
                                        obj.PATIENT.INVOICETIME = DateTime.Now;
                                    }
                                    var errorsSave = obj.PATIENT.Validate(new ValidationContext(obj.PATIENT));
                                    if (errorsSave.ToList().Count == 0)
                                    {
                                        impCMS_PATIENT.UpdateCMS_PATIENT(obj.PATIENT);
                                    }

                                    AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                }

                                if (!string.IsNullOrEmpty(SIDPATIENT))
                                {
                                    if (obj.PATIENT.SERVICE_GROUPID != 0)
                                    {
                                        //Xóa dịch vụ không sử dụng nữa
                                        impCMS_PATIENT_SERVICE.DeleteBySID(obj.PATIENT.SID, obj.PATIENTINFO.PID ?? 0, UserInfo.PARTNERID);

                                        // lấy danh sách dịch vụ trong nhóm dịch vụ
                                        List<CMS_SERVICE> _SERVICES = impCMS_SERVICE.GetAllCMS_SERVICE_CHECKBYGROUPSEVICEID(obj.PATIENT.SERVICE_GROUPID, UserInfo.PARTNERID);
                                        if (_SERVICES != null)
                                        {

                                            foreach (var service in _SERVICES)
                                            {


                                                CMS_PATIENT_SERVICE _PATIENT_SERVICE = new CMS_PATIENT_SERVICE();
                                                if (obj.PATIENT.SID == 0)
                                                {
                                                    _PATIENT_SERVICE.PID = Convert.ToInt32(result); // Giá trị pid của bệnh nhân
                                                    _PATIENT_SERVICE.PATIENT_ID = Convert.ToInt32(SIDPATIENT);
                                                    _PATIENT_SERVICE.SERVICE_ID = service.ID;
                                                    _PATIENT_SERVICE.SERVICE_NAME = service.SERVICE_NAME;
                                                    _PATIENT_SERVICE.SERVICE_PRICE = service.SERVICE_PRICE ?? 0;
                                                    _PATIENT_SERVICE.CREATEBY = UserInfo.accountid;
                                                    _PATIENT_SERVICE.CREATE_DATE = DateTime.Now;
                                                    _PATIENT_SERVICE.IS_PRINT = 0;
                                                    _PATIENT_SERVICE.IS_PRINT_RESULT = 0;
                                                    _PATIENT_SERVICE.SERVICETYPE = service.SERVICETYPE;
                                                    _PATIENT_SERVICE.VISIT_PATIENT_ID = service.VISIT_PATIENT;
                                                    if ((obj.PATIENT.SPOTCASH > 0 && obj.PATIENT.MONEYPOS > 0) || obj.PATIENT.MONEYGIVE > 0)
                                                    {
                                                        _PATIENT_SERVICE.IS_PAY = 1;
                                                    }
                                                    else
                                                    {
                                                        _PATIENT_SERVICE.IS_PAY = 0;
                                                    }
                                                    _PATIENT_SERVICE.COUNT = 1;
                                                    _PATIENT_SERVICE.SERVICE_GROUP_ID = obj.PATIENT.SERVICE_GROUPID;
                                                    var errorsSave = _PATIENT_SERVICE.Validate(new ValidationContext(_PATIENT_SERVICE));
                                                    if (errorsSave.ToList().Count == 0)
                                                    {
                                                        impCMS_PATIENT_SERVICE.Add(_PATIENT_SERVICE, UserInfo.PARTNERID);
                                                    }
                                                }
                                                else  // Trường hợp cập nhật lại dữ liệu
                                                {


                                                    _PATIENT_SERVICE.PID = Convert.ToInt32(result); // Giá trị pid của bệnh nhân
                                                    _PATIENT_SERVICE.PATIENT_ID = Convert.ToInt32(SIDPATIENT);
                                                    _PATIENT_SERVICE.SERVICE_ID = service.ID;
                                                    _PATIENT_SERVICE.SERVICE_NAME = service.SERVICE_NAME;
                                                    _PATIENT_SERVICE.SERVICE_PRICE = service.SERVICE_PRICE ?? 0;
                                                    _PATIENT_SERVICE.CREATEBY = UserInfo.accountid;
                                                    _PATIENT_SERVICE.CREATE_DATE = DateTime.Now;
                                                    _PATIENT_SERVICE.IS_PRINT = 0;
                                                    _PATIENT_SERVICE.IS_PRINT_RESULT = 0;
                                                    _PATIENT_SERVICE.SERVICETYPE = service.SERVICETYPE;
                                                    _PATIENT_SERVICE.VISIT_PATIENT_ID = service.VISIT_PATIENT;
                                                    _PATIENT_SERVICE.SERVICE_GROUP_ID = obj.PATIENT.SERVICE_GROUPID;
                                                    if ((obj.PATIENT.SPOTCASH > 0 && obj.PATIENT.MONEYPOS > 0) || obj.PATIENT.MONEYGIVE > 0)
                                                    {
                                                        _PATIENT_SERVICE.IS_PAY = 1;
                                                    }
                                                    else
                                                    {
                                                        _PATIENT_SERVICE.IS_PAY = 0;
                                                    }
                                                    _PATIENT_SERVICE.COUNT = 1;
                                                    _PATIENT_SERVICE.CREATEBY = UserInfo.accountid;
                                                    _PATIENT_SERVICE.CREATE_DATE = DateTime.Now;


                                                    var errorsSave = _PATIENT_SERVICE.Validate(new ValidationContext(_PATIENT_SERVICE));
                                                    if (errorsSave.ToList().Count == 0)
                                                    {
                                                        impCMS_PATIENT_SERVICE.Add(_PATIENT_SERVICE, UserInfo.PARTNERID);
                                                    }



                                                }
                                                //  obj.SERVICES = _SERVICES;

                                            }

                                            // Cập nhật phòng khám
                                            impCMS_PATIENTINFO_QUEUE.DeleteSID(Convert.ToInt64(SIDPATIENT), Convert.ToInt64(result), UserInfo.PARTNERID);
                                            string[] LOCATIONIDS = obj.PATIENT.LISTLOCATIONID;
                                            foreach (string LOCATIONID in LOCATIONIDS)
                                            {
                                                impCMS_PATIENTINFO_QUEUE.Add(Convert.ToInt64(SIDPATIENT), Convert.ToInt64(result), Convert.ToInt32(LOCATIONID), UserInfo.PARTNERID);
                                            }



                                            ViewBag.TitleSuccsess = "Đăng ký khám thành công!";
                                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;

                                        }
                                        else
                                        {
                                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin nhóm dịch vụ. Mời bạn kiểm tra lại!";
                                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                        }
                                    }
                                    else if (obj.PATIENT.mulSERVICEID != null) // trường hợp chọn dịch vụ riêng lẻ
                                    {
                                        string[] SERVICES = obj.PATIENT.mulSERVICEID;//  obj.PATIENT.SERVICEID.Split(',');

                                        obj.SERVICES = impCMS_SERVICE.GetCMS_SERVICEBySID(obj.PATIENT.SID, UserInfo.PARTNERID);

                                        //xóa dịch vụ không có trong danh sách
                                        foreach (var service in obj.SERVICES)
                                        {
                                            if (SERVICES.Where(x => x == service.ID.ToString()).ToList().Count == 0)
                                            {
                                                impCMS_PATIENT_SERVICE.DeleteBySIDAndServiceID(obj.PATIENT.SID, obj.PATIENTINFO.PID ?? 0, service.ID, UserInfo.PARTNERID);
                                            }
                                        }


                                        //Xóa dịch vụ không sử dụng nữa
                                        // impCMS_PATIENT_SERVICE.DeleteBySID(obj.PATIENT.SID, obj.PATIENTINFO.PID ?? 0, UserInfo.PARTNERID);

                                        // cập nhật các dịch vụ không có trong danh sách
                                        foreach (string service in SERVICES)
                                        {
                                            // kiêm tra dịch vụ đã tồn tại chưa? nếu chưa thì chèn
                                            if (obj.SERVICES.Where(x => x.ID.ToString() == service).ToList().Count == 0)
                                            {

                                                //  obj.SERVICES = new List<CMS_SERVICE>();
                                                List<CMS_SERVICE> datas = impCMS_SERVICE.GetCMS_SERVICEByID(Convert.ToInt32(service), UserInfo.PARTNERID);
                                                if (datas != null)
                                                {
                                                    if (datas.Count > 0)
                                                    {
                                                        CMS_PATIENT_SERVICE _PATIENT_SERVICE = new CMS_PATIENT_SERVICE();

                                                        if (obj.PATIENT.SID == 0)
                                                        {
                                                            _PATIENT_SERVICE.PID = Convert.ToInt32(result); // Giá trị pid của bệnh nhân
                                                            _PATIENT_SERVICE.PATIENT_ID = Convert.ToInt32(SIDPATIENT);
                                                            _PATIENT_SERVICE.SERVICE_ID = datas[0].ID;
                                                            _PATIENT_SERVICE.SERVICE_NAME = datas[0].SERVICE_NAME;
                                                            _PATIENT_SERVICE.SERVICE_PRICE = datas[0].SERVICE_PRICE ?? 0;
                                                            _PATIENT_SERVICE.CREATEBY = UserInfo.accountid;
                                                            _PATIENT_SERVICE.CREATE_DATE = DateTime.Now;
                                                            _PATIENT_SERVICE.IS_PRINT = 0;
                                                            _PATIENT_SERVICE.IS_PRINT_RESULT = 0;
                                                            _PATIENT_SERVICE.COUNT = 1;
                                                            _PATIENT_SERVICE.SERVICETYPE = datas[0].SERVICETYPE;
                                                            _PATIENT_SERVICE.VISIT_PATIENT_ID = 1;
                                                            _PATIENT_SERVICE.SERVICE_GROUP_ID = 0;
                                                            if ((obj.PATIENT.SPOTCASH > 0 && obj.PATIENT.MONEYPOS > 0) || obj.PATIENT.MONEYGIVE > 0)
                                                            {
                                                                _PATIENT_SERVICE.IS_PAY = 1;
                                                            }
                                                            else
                                                            {
                                                                _PATIENT_SERVICE.IS_PAY = 0;
                                                            }
                                                            var errorsSave = _PATIENT_SERVICE.Validate(new ValidationContext(_PATIENT_SERVICE));
                                                            if (errorsSave.ToList().Count == 0)
                                                            {
                                                                impCMS_PATIENT_SERVICE.Add(_PATIENT_SERVICE, UserInfo.PARTNERID);
                                                            }
                                                            // obj.SERVICES.Add(datas[0]);
                                                        }
                                                        else
                                                        {

                                                            _PATIENT_SERVICE.PID = Convert.ToInt32(result); // Giá trị pid của bệnh nhân
                                                            _PATIENT_SERVICE.PATIENT_ID = Convert.ToInt32(SIDPATIENT);
                                                            _PATIENT_SERVICE.SERVICE_ID = datas[0].ID;
                                                            _PATIENT_SERVICE.SERVICE_NAME = datas[0].SERVICE_NAME;
                                                            _PATIENT_SERVICE.SERVICE_PRICE = datas[0].SERVICE_PRICE ?? 0;
                                                            _PATIENT_SERVICE.CREATEBY = UserInfo.accountid;
                                                            _PATIENT_SERVICE.CREATE_DATE = DateTime.Now;
                                                            _PATIENT_SERVICE.IS_PRINT = 0;
                                                            _PATIENT_SERVICE.IS_PRINT_RESULT = 0;
                                                            _PATIENT_SERVICE.COUNT = 1;
                                                            _PATIENT_SERVICE.SERVICETYPE = datas[0].SERVICETYPE;
                                                            _PATIENT_SERVICE.VISIT_PATIENT_ID = 1;
                                                            _PATIENT_SERVICE.SERVICE_GROUP_ID = 0;
                                                            if ((obj.PATIENT.SPOTCASH > 0 && obj.PATIENT.MONEYPOS > 0) || obj.PATIENT.MONEYGIVE > 0)
                                                            {
                                                                _PATIENT_SERVICE.IS_PAY = 1;
                                                            }
                                                            else
                                                            {
                                                                _PATIENT_SERVICE.IS_PAY = 0;
                                                            }
                                                            var errorsSave = _PATIENT_SERVICE.Validate(new ValidationContext(_PATIENT_SERVICE));
                                                            if (errorsSave.ToList().Count == 0)
                                                            {
                                                                impCMS_PATIENT_SERVICE.Add(_PATIENT_SERVICE, UserInfo.PARTNERID);
                                                            }
                                                            // obj.SERVICES.Add(datas[0]);

                                                        }

                                                    }
                                                    else
                                                    {
                                                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin dịch vụ. Mời bạn kiểm tra lại!";
                                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                                    }
                                                }
                                                else
                                                {
                                                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin dịch vụ. Mời bạn kiểm tra lại!";
                                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                                }
                                            }
                                            else // cập nhật trạng thái dịch vụ cho bệnh nhân
                                            {
                                                List<CMS_SERVICE> datas = impCMS_SERVICE.GetCMS_SERVICEByID(Convert.ToInt32(service), UserInfo.PARTNERID);
                                                //impCMS_PATIENT_SERVICE.Get()

                                                if (datas != null)
                                                {
                                                    if (datas.Count > 0)
                                                    {
                                                        CMS_PATIENT_SERVICE _PATIENT_SERVICE = new CMS_PATIENT_SERVICE();
                                                        _PATIENT_SERVICE.PID = Convert.ToInt32(result); // Giá trị pid của bệnh nhân
                                                        _PATIENT_SERVICE.PATIENT_ID = Convert.ToInt32(SIDPATIENT);
                                                        _PATIENT_SERVICE.SERVICE_ID = datas[0].ID;
                                                        _PATIENT_SERVICE.SERVICE_NAME = datas[0].SERVICE_NAME;
                                                        _PATIENT_SERVICE.SERVICE_PRICE = datas[0].SERVICE_PRICE ?? 0;
                                                        _PATIENT_SERVICE.UPDATEBY = UserInfo.accountid;
                                                        _PATIENT_SERVICE.UPDATE_DATE = DateTime.Now;
                                                        _PATIENT_SERVICE.IS_PRINT = 0;
                                                        _PATIENT_SERVICE.IS_PRINT_RESULT = 0;
                                                        _PATIENT_SERVICE.COUNT = 1;
                                                        _PATIENT_SERVICE.SERVICETYPE = datas[0].SERVICETYPE;
                                                        _PATIENT_SERVICE.VISIT_PATIENT_ID = 1;
                                                        _PATIENT_SERVICE.SERVICE_GROUP_ID = 0;

                                                        //lấy danh sách dịch vụ đã sử dụng
                                                        List<CMS_PATIENT_SERVICE> CMS_PATIENT_SERVICES= impCMS_PATIENT_SERVICE.GetCMS_PATIENT_SERVICE_BYSIDALL(Convert.ToInt32(SIDPATIENT), UserInfo.PARTNERID);
                                                        if (CMS_PATIENT_SERVICES != null)
                                                        {
                                                            _PATIENT_SERVICE.ID = CMS_PATIENT_SERVICES.Where(x => x.SERVICE_ID.ToString() == service).ToList()[0].ID;
                                                        }

                                                        if ((obj.PATIENT.SPOTCASH > 0 && obj.PATIENT.MONEYPOS > 0) || obj.PATIENT.MONEYGIVE > 0)
                                                        {
                                                            _PATIENT_SERVICE.IS_PAY = 1;
                                                        }
                                                        else
                                                        {
                                                            _PATIENT_SERVICE.IS_PAY = 0;
                                                        }
                                                        var errorsSave = _PATIENT_SERVICE.Validate(new ValidationContext(_PATIENT_SERVICE));
                                                        if (errorsSave.ToList().Count == 0)
                                                        {
                                                            impCMS_PATIENT_SERVICE.Update(_PATIENT_SERVICE, UserInfo.PARTNERID);
                                                        }

                                                    }
                                                }
                                            }
                                        }



                                        // Cập nhật phòng khám
                                        impCMS_PATIENTINFO_QUEUE.DeleteSID(Convert.ToInt64(SIDPATIENT), Convert.ToInt64(result), UserInfo.PARTNERID);
                                        string[] LOCATIONIDS = obj.PATIENT.LISTLOCATIONID;
                                        foreach (string LOCATIONID in LOCATIONIDS)
                                        {
                                            impCMS_PATIENTINFO_QUEUE.Add(Convert.ToInt64(SIDPATIENT), Convert.ToInt64(result), Convert.ToInt32(LOCATIONID), UserInfo.PARTNERID);
                                        }


                                        ViewBag.TitleSuccsess = "Đăng ký khám thành công!";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                    }
                                }

                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Có lỗi trong quá trình cập nhật. Mời bạn cập nhật lại!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                        }
                        else

                        {
                            ViewBag.TitleSuccsess = textCheckLocation;
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Bạn chưa chọn dịch vụ khám. Mời bạn chọn dịch vụ!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        //obj.PATIENT.SERVICEID = string.Empty;
                    }

                }
                else
                {
                    ViewBag.TitleSuccsess = "Bạn chưa chọn phòng khám. Mời bạn chọn phòng khám!";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
                //if (string.IsNullOrWhiteSpace(obj.PATIENT.SERVICEID))
                //{
                //    obj.PATIENT.SERVICEID = string.Empty;
                //}

            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientsCreateOrder: " + ex.ToString());
            }
            obj.SERVICES = impCMS_SERVICE.GetCMS_SERVICEBySID(obj.PATIENT.SID, UserInfo.PARTNERID);
          

            return View(obj);

        }



        public ActionResult Patient_Order(string sid, string pid)
        {
            // Initialization.             
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Thông tin hóa đơn", "/Clinic/Patient_Order");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            CMS_PATIENTINFO PATIENTINFO = new CMS_PATIENTINFO();
            ImpCMS_TEMPLATEPRINT impCMS_TEMPLATEPRINT = new ImpCMS_TEMPLATEPRINT();



            try
            {
                //Lấy thông tin header
                List<CMS_TEMPLATEPRINT> _TEMPLATEPRINTS = impCMS_TEMPLATEPRINT.GetCMS_TEMPLATEPRINT_BYTYPE(1, UserInfo.PARTNERID);

                if (_TEMPLATEPRINTS != null)
                {
                    if (_TEMPLATEPRINTS.Count > 0)
                    {
                        ViewBag.TemplateHeader = _TEMPLATEPRINTS[0].TEMPLATE;
                    }
                }

                //Lấy thông tin footer
                _TEMPLATEPRINTS = impCMS_TEMPLATEPRINT.GetCMS_TEMPLATEPRINT_BYTYPE(2, UserInfo.PARTNERID);

                if (_TEMPLATEPRINTS != null)
                {
                    if (_TEMPLATEPRINTS.Count > 0)
                    {
                        ViewBag.TemplateFooter = _TEMPLATEPRINTS[0].TEMPLATE;
                    }
                }

                CMS_Core.Models.MedicalExaminationViewModel medicalExamination = new CMS_Core.Models.MedicalExaminationViewModel();


                List<CMS_PATIENTINFO> PATIENTINFOS = null;

                PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);
                List<CMS_PATIENT> CMS_PATIENTS = impCMS_PATIENT.GetCMS_PATIENT_BYSID(sid, UserInfo.PARTNERID);


                if (PATIENTINFOS != null)
                {
                    if (PATIENTINFOS.Count > 0)
                    {
                        medicalExamination.PATIENTINFO = PATIENTINFOS[0];
                    }
                }

                if (CMS_PATIENTS != null)
                {
                    if (CMS_PATIENTS.Count > 0)
                    {
                        medicalExamination.PATIENT = CMS_PATIENTS[0];
                    }
                }

                medicalExamination.CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetCMS_PATIENT_SERVICE_BYSID_NOPAY(Convert.ToInt64(sid), UserInfo.PARTNERID);


                if (PATIENTINFOS != null)
                {
                    if (PATIENTINFOS.Count > 0)
                    {
                        // return View(PATIENTINFOS[0]);

                        string footer = "--footer-right \" [page]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
                        return new Rotativa.MVC.PartialViewAsPdf(medicalExamination)
                        {
                            RotativaOptions = new Rotativa.Core.DriverOptions()
                            {
                                // PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                                PageOrientation = Rotativa.Core.Options.Orientation.Portrait,
                                PageSize = Rotativa.Core.Options.Size.A5,
                                // PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                                CustomSwitches = footer
                            },

                        };
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                }


            }
            catch (Exception ex)
            {
                logError.Info("ListPatientsPrint:" + ex.ToString());
                throw;
            }

            return View(PATIENTINFO);

        }

        public ActionResult ListPatientsByService(string SID, string PatientName)
        {
            // Initialization.             
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Chọn dịch vụ cho khác hàng", "/Clinic/ListPatientsByService");
            var UserInfo = ((cms_Account)Session["UserInfo"]);


            CMS_Core.Models.ListServiceChoiseViewModel listServiceChoiseViewModel = new CMS_Core.Models.ListServiceChoiseViewModel();
            try
            {
                if (string.IsNullOrEmpty(SID))
                {
                    SID = "0";
                }
                else
                {
                    Session["ListPatientsByService" + SID] = null;
                }

                if (string.IsNullOrEmpty(PatientName))
                {
                    PatientName = string.Empty;
                }


                listServiceChoiseViewModel.PARTNERID = UserInfo.PARTNERID;
                listServiceChoiseViewModel.SID = SID;
                listServiceChoiseViewModel.PatientName = PatientName;

                List<CMS_SERVICE> _SERVICES = impCMS_SERVICE.GetAllCMS_SERVICE(0, UserInfo.PARTNERID);
                ViewBag.SERVICES = _SERVICES;
                ViewBag.SERVICESCHOISE = null;

            }
            catch (Exception ex)
            {
                logError.Info("ListPatientsByService:" + ex.ToString());
            }
            // Info.  
            return View(listServiceChoiseViewModel);



        }



        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult ListPatientsByTinh(string IDTinh)
        {
            try
            {
                IEnumerable<ClassComboBox> modelList = new List<ClassComboBox>();
                if (Session["Tinh" + IDTinh] != null)
                {
                    modelList = (List<ClassComboBox>)Session["Tinh" + IDTinh];
                }
                else
                {
                    ImpClassComboBox impClassComboBox = new ImpClassComboBox();
                    modelList = impClassComboBox.GetDataBySQL("select id as idfield, category_desc as namefield from DM_QUANHUYEN where categorys_status = 2 and TINHTHANH_ID =  " + Convert.ToInt32(IDTinh) + " order by category_desc asc");
                    Session["Tinh" + IDTinh] = modelList;
                }
                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("ListPatientsByTinh:" + IDTinh + "  " + ex.ToString());
                return Json(string.Empty);
            }
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult ListPatientsByKhoaKham(string KhoaKhamID)
        {
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);

                IEnumerable<ClassComboBox> modelList = new List<ClassComboBox>();
                if (Session["KhoaKham" + KhoaKhamID] != null)
                {
                    modelList = (List<ClassComboBox>)Session["KhoaKham" + KhoaKhamID];
                }
                else
                {
                    ImpClassComboBox impClassComboBox = new ImpClassComboBox();
                    modelList = impClassComboBox.GetDataBySQL("SELECT   id  as idfield,    ROOM_NAME as namefield FROM  CMS_ROOM_CLINIC where STATUS = 2 and DEPARTMENT_ID =  " + Convert.ToInt32(KhoaKhamID) + " and PARTNERID = " + UserInfo.PARTNERID + "    order by ROOM_NAME asc");
                    Session["KhoaKham" + KhoaKhamID] = modelList;
                }
                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("ListPatientsByTinh:" + KhoaKhamID + "  " + ex.ToString());
                return Json(string.Empty);
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult ListPatientsByGroupService(string GroupID, string sid)
        {
            try
            {
                if (string.IsNullOrEmpty(sid))
                {
                    sid = "0";
                }

                var UserInfo = ((cms_Account)Session["UserInfo"]);
                IEnumerable<CMS_SERVICE> modelList = new List<CMS_SERVICE>();
                if (Session["GroupService" + GroupID] != null)
                {
                    modelList = (List<CMS_SERVICE>)Session["GroupService" + GroupID];
                }
                else
                {
                    modelList = impCMS_SERVICE.GetAllCMS_SERVICE_GROUPSEVICEIDANDSID(Convert.ToInt32(GroupID), Convert.ToInt64(sid), UserInfo.PARTNERID);
                    Session["GroupService" + GroupID] = modelList;
                }
                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("ListPatientsByGroupService:" + GroupID + "  " + ex.ToString());
                return Json(string.Empty);
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult ListPatientsByListServiceID(string ListServiceID, string sid)
        {
            try
            {

                if (string.IsNullOrEmpty(sid))
                {
                    sid = "0";
                }

                var UserInfo = ((cms_Account)Session["UserInfo"]);
                IEnumerable<CMS_SERVICE> modelList = new List<CMS_SERVICE>();
                //if (Session["ListServiceID" + ListServiceID] != null)
                //{
                //    modelList = (List<CMS_SERVICE>)Session["ListServiceID" + ListServiceID];
                //}
                //else
                //{

                modelList = impCMS_SERVICE.GetAllCMS_SERVICE_BYLISTSEVICEID(ListServiceID, Convert.ToInt64(sid), UserInfo.PARTNERID);
                //    Session["ListServiceID" + ListServiceID] = modelList;
                //}
                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("ListPatientsByGroupService:" + ListServiceID + "  " + ex.ToString());
                return Json(string.Empty);
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult ListPatientsByPID(string pid)
        {
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                IEnumerable<CMS_PATIENTINFO> modelList = new List<CMS_PATIENTINFO>();

                modelList = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);

                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("ListPatientsByGroupService:" + pid + "  " + ex.ToString());
                return Json(string.Empty);
            }
        }


        #endregion

        #region --> Lịch hẹn khám

        public ActionResult MedicalAppointment()
        {
            // Initialization.  
            AddPageHeader("Danh sách lịch hẹn khám", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách lịch hẹn khám", "/Clinic/MedicalAppointment");
            var UserInfo = ((cms_Account)Session["UserInfo"]);



            CMS_Core.Models.PatientViewModel patientViewModel = new CMS_Core.Models.PatientViewModel();
            patientViewModel.tungay = DateTime.Now.ToString("dd/MM/yyyy");
            patientViewModel.denngay = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy");
            patientViewModel.keyword = string.Empty;
            patientViewModel.TypeKeyword = 0;
            patientViewModel.partnerid = UserInfo.PARTNERID;

            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordMedicalAppointment();


                List<CMS_PATIENTINFO_APPOINTMENT> _PATIENTINFO_APPOINTMENTS = impCMS_PATIENTINFO_APPOINTMENT.GetCMS_PATIENTINFO_APPOINTMENT_ByALL(DateTime.Now, DateTime.Now.AddDays(4), string.Empty, 1, UserInfo.PARTNERID);
                ViewBag.PATIENTINFO_APPOINTMENTS = _PATIENTINFO_APPOINTMENTS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("MedicalAppointment:" + ex.ToString());
            }

            // Info.  
            return View(patientViewModel);
        }

        [HttpPost]
        public ActionResult MedicalAppointment(CMS_Core.Models.PatientViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách lịch hẹn khám", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách lịch hẹn khám", "/Clinic/MedicalAppointment");
            var UserInfo = ((cms_Account)Session["UserInfo"]);



            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();

            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(obj.tungay.Trim()))
                    Tungay = DateTime.ParseExact(obj.tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(obj.denngay.Trim()))
                    Denngay = DateTime.ParseExact(obj.denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

                if (Tungay > Denngay)
                {
                    TextErr = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày";
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                }
            }
            catch
            {
                TextErr = "Dữ liệu ngày tháng không đúng định dạng";
            }
            #endregion


            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeyword();
                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchMedicalAppointment":
                            if (string.IsNullOrEmpty(obj.keyword))
                            {
                                obj.keyword = string.Empty;
                            }
                            List<CMS_PATIENTINFO_APPOINTMENT> _PATIENTINFO_APPOINTMENTS = impCMS_PATIENTINFO_APPOINTMENT.GetCMS_PATIENTINFO_APPOINTMENT_ByALL(Tungay, Denngay.AddDays(1), obj.keyword.Trim(), obj.TypeKeyword, UserInfo.PARTNERID);
                            ViewBag.PATIENTINFO_APPOINTMENTS = _PATIENTINFO_APPOINTMENTS;

                            break;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = TextErr;
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("MedicalAppointment:" + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        #endregion

        #region --> Đặt lịch khám bệnh

        public ActionResult MedicalAppointmentAdd(string pid, string PATIENTID)
        {
            // Initialization.             
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/MedicalAppointment");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            CMS_PATIENTINFO_APPOINTMENT obj = new CMS_PATIENTINFO_APPOINTMENT();
            try
            {
                if (string.IsNullOrEmpty(pid))
                {
                    AddPageHeader("Đặt lịch khám", "");
                    AddBreadcrumb("Đặt lịch khám", "/Clinic/MedicalAppointmentAdd");
                    ViewBag.Title = "Đặt lịch khám";

                    if (!string.IsNullOrEmpty(PATIENTID))
                    {

                        List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(PATIENTID, UserInfo.PARTNERID);
                        if (_PATIENTINFOS != null)
                        {
                            if (_PATIENTINFOS.Count > 0)
                            {
                                obj = impCMS_PATIENTINFO_APPOINTMENT.CONVERT_PATIENTINFO_TO_APPOINTMENT(_PATIENTINFOS[0]);
                            }
                        }
                        obj.PATIENTINFO_ID = Convert.ToInt64(PATIENTID);
                    }
                    else
                    {
                        obj.PARTNERID = UserInfo.PARTNERID;
                        obj.SEX = 2;
                        obj.CITY = 112; //Tỉnh thành Hà Nội
                        obj.DISTRICT = 0; //Tỉnh thành Hà Nội
                        obj.STATUS = 2;
                        obj.partnerid_Code = UserInfo.PARTNER_CODE;
                        obj.PID = 0;

                    }
                }
                else
                {
                    AddPageHeader("Chỉnh sửa đặt lịch khám", "");
                    AddBreadcrumb("Chỉnh sửa đặt lịch khám", "/Clinic/MedicalAppointmentAdd");
                    ViewBag.Title = "Chỉnh sửa đặt lịch khám";

                    obj = impCMS_PATIENTINFO_APPOINTMENT.Get(Convert.ToInt32(pid), UserInfo.PARTNERID);

                }
            }
            catch (Exception ex)
            {
                logError.Info("MedicalAppointmentAdd: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(obj);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MedicalAppointmentAdd(CMS_PATIENTINFO_APPOINTMENT obj)
        {
            try
            {
                // Initialization.     
                if (obj.PID == 0)
                {
                    AddBreadcrumb("Phòng khám", "");
                    AddPageHeader("Thêm mới lịch hẹn khám", "");
                    AddBreadcrumb("TThêm mới lịch hẹn khám", "/Clinic/MedicalAppointmentAdd");
                    ViewBag.Title = "Thêm mới lịch hẹn khám";
                }
                else
                {
                    AddBreadcrumb("Phòng khám", "");
                    AddPageHeader("Chỉnh sửa lịch hẹn khám", "");
                    AddBreadcrumb("Chỉnh sửa lịch hẹn khám", "/Clinic/MedicalAppointmentAdd");
                    ViewBag.Title = "Chỉnh sửa lịch hẹn khám";
                }

                var errors = obj.Validate(new ValidationContext(obj));
                var UserInfo = ((cms_Account)Session["UserInfo"]);

                if (errors.ToList().Count == 0)
                {
                    TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                    obj.PATIENTNAME = myTI.ToTitleCase(obj.PATIENTNAME);
                    string result = string.Empty;
                    if (obj.PID == 0)
                    {
                        obj.CREATEBY = (Int16)UserInfo.accountid;
                        obj.CREATEDATE = DateTime.Now;

                        result = impCMS_PATIENTINFO_APPOINTMENT.Add(obj, UserInfo.PARTNERID);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới đặt lịch thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới đặt lịch không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        obj.UPDATEBY = (Int16)UserInfo.accountid;
                        obj.UPATEDATE = DateTime.Now;

                        result = impCMS_PATIENTINFO_APPOINTMENT.Update(obj, UserInfo.PARTNERID);
                        if (!string.IsNullOrEmpty(result))
                        {
                            List<CMS_PATIENT_HISTORY> _PATIENT_HISTORYs = new List<CMS_PATIENT_HISTORY>();
                            ViewBag.TitleSuccsess = "Cập nhật thông tin lịch hẹn thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin lịch hẹn không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = errors.ToList()[0].ErrorMessage;
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }




            }
            catch (Exception ex)
            {
                logError.Info("MedicalAppointmentAdd: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                logError.Info("MedicalAppointmentAdd: " + ex.ToString());
            }
            return View(obj);
        }

        #endregion

        #region --> Danh sách chờ khám bệnh

        public ActionResult ListPatientWaitingForMedical(string channel)
        {
            // Initialization.  

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            if (!string.IsNullOrEmpty(channel))
            {
                if (!CMSLIS.Common.Common.IsNumeric(channel))
                    channel = "0";

                CMS_SERVICE_TYPE _SERVICE_TYPE = impCMS_SERVICE_TYPE.Get(Convert.ToInt32(channel), UserInfo.PARTNERID);

                if (_SERVICE_TYPE != null)
                {
                    if (_SERVICE_TYPE.NAME.Length > 0)
                    {
                        AddPageHeader("Danh sách " + _SERVICE_TYPE.NAME, "");
                        AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
                        AddBreadcrumb("Danh sách " + _SERVICE_TYPE.NAME, "/Clinic/ListPatientWaitingForMedical?channel=" + channel);
                    }
                    else
                    {
                        // channel = "0";
                        AddPageHeader("Danh sách chờ khám ", "");
                        AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
                        AddBreadcrumb("Danh sách chờ khám ", "/Clinic/ListPatientWaitingForMedical");
                    }
                }
                else
                {
                    //  channel = "0";
                    AddPageHeader("Danh sách chờ khám ", "");
                    AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
                    AddBreadcrumb("Danh sách chờ khám ", "/Clinic/ListPatientWaitingForMedical");
                }
            }
            else
            {
                channel = "0";
                AddPageHeader("Danh sách chờ khám ", "");
                AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
                AddBreadcrumb("Danh sách chờ khám ", "/Clinic/ListPatientWaitingForMedical");
            }



            CMS_Core.Models.PatientViewModel patientViewModel = new CMS_Core.Models.PatientViewModel();
            patientViewModel.tungay = DateTime.Now.ToString("dd/MM/yyyy");
            patientViewModel.denngay = DateTime.Now.ToString("dd/MM/yyyy");
            patientViewModel.keyword = string.Empty;
            patientViewModel.TypeKeyword = 0;
            patientViewModel.partnerid = UserInfo.PARTNERID;
            patientViewModel.channel = Convert.ToInt32(channel);

            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("LOCATIONID");
                if (cookie.Value != null)
                {
                    if (!string.IsNullOrEmpty(cookie.Value))
                    {
                        patientViewModel.LOCATIONID = Convert.ToInt32(cookie.Value);
                    }
                    else
                    {
                        patientViewModel.LOCATIONID = 0;
                    }
                }
            }
            catch
            {
                patientViewModel.LOCATIONID = 0;
            }


            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordWaitingForMedical();
                List<CMS_PATIENTINFO_TOTAL> CMS_PATIENTINFO_TOTALS = impCMS_PATIENT.GetWaitingForMedical_BYALL(DateTime.Now, DateTime.Now.AddDays(1), string.Empty, 1, Convert.ToInt32(channel), Convert.ToInt32(patientViewModel.LOCATIONID), UserInfo.PARTNERID);
                ViewBag.PATIENTINFO_TOTALS = CMS_PATIENTINFO_TOTALS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientWaitingForMedical:" + ex.ToString());
            }

            // Info.  
            return View(patientViewModel);
        }

        [HttpPost]
        public ActionResult ListPatientWaitingForMedical(CMS_Core.Models.PatientViewModel obj, string submit)
        {
            // Initialization.  
            try
            {
                HttpCookie cookieq = HttpContext.Request.Cookies.Get("LOCATIONID");


                if (cookieq.Value != null)
                {
                    if (!string.IsNullOrEmpty(cookieq.Value))
                    {
                        if (obj.LOCATIONID != Convert.ToInt32(cookieq.Value))
                        {
                            cookieq.Value = obj.LOCATIONID.ToString();
                            cookieq.Expires = DateTime.Now.AddDays(100);
                            Response.Cookies.Add(cookieq);
                        }
                    }
                    else
                    {
                        cookieq.Value = obj.LOCATIONID.ToString();
                        cookieq.Expires = DateTime.Now.AddDays(100);
                        Response.Cookies.Add(cookieq);
                    }
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("LOCATIONID");
                    cookie.Value = obj.LOCATIONID.ToString();
                    cookie.Expires = DateTime.Now.AddDays(100);
                    Response.Cookies.Add(cookie);
                }

            }
            catch
            {
                HttpCookie cookie = new HttpCookie("LOCATIONID", obj.LOCATIONID.ToString());
                cookie.Expires = DateTime.Now.AddDays(100);
                Response.Cookies.Add(cookie);

            }

            //ImpCMS_GROUP_MEDICINE impCMS_GROUP_MEDICINE = new ImpCMS_GROUP_MEDICINE();
            //List<CMS_GROUP_MEDICINE> dfsd = impCMS_GROUP_MEDICINE.GetCMS_GROUP_MEDICINENew();

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            if (obj.channel != 0)
            {

                CMS_SERVICE_TYPE _SERVICE_TYPE = impCMS_SERVICE_TYPE.Get(Convert.ToInt32(obj.channel), UserInfo.PARTNERID);

                if (_SERVICE_TYPE != null)
                {
                    if (_SERVICE_TYPE.NAME.Length > 0)
                    {
                        AddPageHeader("Danh sách " + _SERVICE_TYPE.NAME, "");
                        AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
                        AddBreadcrumb("Danh sách " + _SERVICE_TYPE.NAME, "/Clinic/ListPatientWaitingForMedical?channel=" + obj.channel);
                    }
                    else
                    {
                        // channel = "0";
                        AddPageHeader("Danh sách chờ khám ", "");
                        AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
                        AddBreadcrumb("Danh sách chờ khám ", "/Clinic/ListPatientWaitingForMedical");
                    }
                }
                else
                {
                    //  channel = "0";
                    AddPageHeader("Danh sách chờ khám ", "");
                    AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
                    AddBreadcrumb("Danh sách chờ khám ", "/Clinic/ListPatientWaitingForMedical");
                }


            }
            else
            {
                obj.channel = 0;
                AddPageHeader("Danh sách chờ khám ", "");
                AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
                AddBreadcrumb("Danh sách chờ khám ", "/Clinic/ListPatientWaitingForMedical");
            }
            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();

            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(obj.tungay.Trim()))
                    Tungay = DateTime.ParseExact(obj.tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(obj.denngay.Trim()))
                    Denngay = DateTime.ParseExact(obj.denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

                if (Tungay > Denngay)
                {
                    TextErr = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày";
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                }
            }
            catch
            {
                TextErr = "Dữ liệu ngày tháng không đúng định dạng";
            }
            #endregion


            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordWaitingForMedical();
                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchListPatientWaitingForMedical":
                            if (string.IsNullOrEmpty(obj.keyword))
                            {
                                obj.keyword = string.Empty;
                            }
                            List<CMS_PATIENTINFO_TOTAL> CMS_PATIENTINFO_TOTALS = impCMS_PATIENT.GetWaitingForMedical_BYALL(Tungay, Denngay.AddDays(1), obj.keyword.Trim(), obj.TypeKeyword, Convert.ToInt32(obj.channel), Convert.ToInt32(obj.LOCATIONID), UserInfo.PARTNERID);
                            ViewBag.PATIENTINFO_TOTALS = CMS_PATIENTINFO_TOTALS;
                            break;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = TextErr;
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientWaitingForMedical: " + ex.ToString());

            }

            // Info.  
            return View(obj);
        }


        public ActionResult ListPatientPrint(string sid, string pid, string Token)
        {
            // Initialization.             

            AddPageHeader("In phiếu in", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("In phiếu in", "/Clinic/ListPatientPrint");


            CMS_Core.Models.PatientPrintViewModel obj = new CMS_Core.Models.PatientPrintViewModel();


            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (!string.IsNullOrEmpty(sid) && !string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(Token))
                {
                    if (CMSLIS.Common.Common.CheckKeyPrivate(sid, pid, Token))
                    {
                        List<CMS_PATIENTINFO_TOTAL> _PATIENTINFO_TOTALS = impCMS_PATIENT.GetWaitingForMedical_BYSID(Convert.ToInt32(sid), UserInfo.PARTNERID);

                        if (_PATIENTINFO_TOTALS != null)
                        {
                            if (_PATIENTINFO_TOTALS.Count > 0)
                            {
                                obj.PATIENTINFO = _PATIENTINFO_TOTALS[0];
                                obj.CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetCMS_PATIENT_SERVICE_BYSID(Convert.ToInt32(sid), UserInfo.PARTNERID);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thông tin phiếu không chính xác, Mời bạn kiểm tra lại!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }

            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ExportStoreAdd: " + ex.ToString());
            }

            // Info.  
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(obj)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A5
                },
            };


        }

        public ActionResult ListPatientNOPayPrint(string sid, string pid, string Token)
        {
            // Initialization.             

            AddPageHeader("In phiếu in", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("In phiếu in", "/Clinic/ListPatientNOPayPrint");


            CMS_Core.Models.PatientPrintViewModel obj = new CMS_Core.Models.PatientPrintViewModel();


            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (!string.IsNullOrEmpty(sid) && !string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(Token))
                {
                    if (CMSLIS.Common.Common.CheckKeyPrivate(sid, pid, Token))
                    {
                        List<CMS_PATIENTINFO_TOTAL> _PATIENTINFO_TOTALS = impCMS_PATIENT.GetWaitingForMedical_BYSID(Convert.ToInt32(sid), UserInfo.PARTNERID);

                        if (_PATIENTINFO_TOTALS != null)
                        {
                            if (_PATIENTINFO_TOTALS.Count > 0)
                            {
                                obj.PATIENTINFO = _PATIENTINFO_TOTALS[0];


                                obj.CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetCMS_PATIENT_SERVICE_BYSID_NOPAY(Convert.ToInt32(sid), UserInfo.PARTNERID);

                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }

                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thông tin phiếu không chính xác, Mời bạn kiểm tra lại!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }

            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ExportStoreAdd: " + ex.ToString());
            }

            // Info.  
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(obj)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A5
                },
            };


        }


        public ActionResult ListPatientNOPricePrint(string sid, string pid, string Token)
        {
            // Initialization.             

            AddPageHeader("In phiếu in", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("In phiếu in", "/Clinic/ListPatientNOPricePrint");


            CMS_Core.Models.PatientPrintViewModel obj = new CMS_Core.Models.PatientPrintViewModel();


            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (!string.IsNullOrEmpty(sid) && !string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(Token))
                {
                    if (CMSLIS.Common.Common.CheckKeyPrivate(sid, pid, Token))
                    {
                        List<CMS_PATIENTINFO_TOTAL> _PATIENTINFO_TOTALS = impCMS_PATIENT.GetWaitingForMedical_BYSID(Convert.ToInt32(sid), UserInfo.PARTNERID);

                        if (_PATIENTINFO_TOTALS != null)
                        {
                            if (_PATIENTINFO_TOTALS.Count > 0)
                            {
                                obj.PATIENTINFO = _PATIENTINFO_TOTALS[0];


                                obj.CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetCMS_PATIENT_SERVICE_BYSID_NOPAY(Convert.ToInt32(sid), UserInfo.PARTNERID);

                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }

                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thông tin phiếu không chính xác, Mời bạn kiểm tra lại!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }

            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ExportStoreAdd: " + ex.ToString());
            }

            // Info.  
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(obj)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A5
                },
            };


        }


        public ActionResult ListPatientWaitingForMedicaGroupService()
        {
            // Initialization.  

            var UserInfo = ((cms_Account)Session["UserInfo"]);

            AddPageHeader("Danh sách bệnh nhân khám theo gói ", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách bệnh nhân khám theo gói", "/Clinic/ListPatientWaitingForMedicaGroupService");

            CMS_Core.Models.PatientViewModel patientViewModel = new CMS_Core.Models.PatientViewModel();
            patientViewModel.tungay = DateTime.Now.ToString("dd/MM/yyyy");
            patientViewModel.denngay = DateTime.Now.ToString("dd/MM/yyyy");
            patientViewModel.keyword = string.Empty;
            patientViewModel.TypeKeyword = 0;
            patientViewModel.partnerid = UserInfo.PARTNERID;

            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordWaitingForMedical();
                List<CMS_PATIENTINFO_TOTAL> CMS_PATIENTINFO_TOTALS = impCMS_PATIENT.GetGroupService_BYALL(DateTime.Now, DateTime.Now.AddDays(1), string.Empty, 1, 0, UserInfo.PARTNERID);
                ViewBag.PATIENTINFO_TOTALS = CMS_PATIENTINFO_TOTALS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientWaitingForMedicaGroupService:" + ex.ToString());
            }

            // Info.  
            return View(patientViewModel);
        }

        [HttpPost]
        public ActionResult ListPatientWaitingForMedicaGroupService(CMS_Core.Models.PatientViewModel obj, string submit)
        {
            // Initialization.  


            var UserInfo = ((cms_Account)Session["UserInfo"]);


            AddPageHeader("Danh sách bệnh nhân khám theo gói ", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách bệnh nhân khám theo gói", "/Clinic/ListPatientWaitingForMedicaGroupService");

            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();

            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(obj.tungay.Trim()))
                    Tungay = DateTime.ParseExact(obj.tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(obj.denngay.Trim()))
                    Denngay = DateTime.ParseExact(obj.denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

                if (Tungay > Denngay)
                {
                    TextErr = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày";
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                }
            }
            catch
            {
                TextErr = "Dữ liệu ngày tháng không đúng định dạng";
            }
            #endregion


            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordWaitingForMedical();
                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchListPatientWaitingForMedicaGroupService":
                            if (string.IsNullOrEmpty(obj.keyword))
                            {
                                obj.keyword = string.Empty;
                            }
                            List<CMS_PATIENTINFO_TOTAL> CMS_PATIENTINFO_TOTALS = impCMS_PATIENT.GetGroupService_BYALL(Tungay, Denngay.AddDays(1), obj.keyword, obj.TypeKeyword, obj.SERVICE_GROUPID, UserInfo.PARTNERID);
                            ViewBag.PATIENTINFO_TOTALS = CMS_PATIENTINFO_TOTALS;
                            break;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = TextErr;
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientWaitingForMedicaGroupService: " + ex.ToString());

            }

            // Info.  
            return View(obj);
        }


        #endregion


        #region --> Khám bệnh

        public ActionResult medicalExamination(string sid, string channel, string queueid)
        {
            // Initialization.  
            AddPageHeader("Khám bệnh ", "");
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatientWaitingForMedical");
            AddBreadcrumb("Khám bệnh", "/Clinic/medicalExamination");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            CMS_Core.Models.MedicalExaminationViewModel medicalExamination = new CMS_Core.Models.MedicalExaminationViewModel();
            Session["SaveALLERGGY1"] = null;
            Session["SaveHISTORIE1"] = null;
            Session["medicalExamination"] = null;
            Session["PATIENT_PRESCRIPTION"] = null;

            try
            {
                if (string.IsNullOrWhiteSpace(channel))
                {
                    channel = "0";
                }

                if (string.IsNullOrWhiteSpace(queueid))
                {
                    queueid = "0";
                }

                if (!string.IsNullOrWhiteSpace(sid))
                {
                    medicalExamination.SID = Convert.ToInt32(sid);

                    medicalExamination = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_ALLBYSID(Convert.ToInt64(sid), Convert.ToInt32(queueid), UserInfo.PARTNERID);


                    // List<CMS_PATIENT> patients = impCMS_PATIENT.GetCMS_PATIENT_BYSID(sid, UserInfo.PARTNERID);
                    if (medicalExamination.PATIENT != null)
                    {
                        if (medicalExamination.PATIENT.SID > 0)
                        {
                            //  medicalExamination.PATIENT = patients[0];
                            // List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(medicalExamination.PATIENT.PID.ToString(), UserInfo.PARTNERID);
                            if (medicalExamination.PATIENTINFO != null)
                            {
                                if (medicalExamination.PATIENTINFO.PID > 0)
                                {
                                    // lọc các dịch vụ khám theo nhóm vào từng lần khám
                                    if (medicalExamination.PATIENT.SERVICE_GROUPID > 0 && medicalExamination.PATIENT.VISIT_PATIENT_ID != 1)
                                    {
                                        medicalExamination.CMS_PATIENT_SERVICES = medicalExamination.CMS_PATIENT_SERVICES.Where(x => x.VISIT_PATIENT_ID == medicalExamination.PATIENT.VISIT_PATIENT_ID).ToList();
                                    }
                                    if (medicalExamination.PATIENT.VISIT_PATIENT_ID < 1)
                                    {
                                        medicalExamination.PATIENT.VISIT_PATIENT_ID = 1;
                                    }


                                    Session["SaveALLERGGY1"] = medicalExamination.ALLERGGYS;
                                    Session["SaveHISTORIE1"] = medicalExamination.HISTORIES;

                                    medicalExamination.HISTORIE = new CMS_PATIENT_HISTORY();
                                    medicalExamination.HISTORIE.SICKNAME = string.Empty;
                                    medicalExamination.ALLERGGY = new CMS_PATIENT_ALLERGY();
                                    medicalExamination.PATIENT_SERVICE_RESULT = new CMS_PATIENT_SERVICE_RESULT();
                                    medicalExamination.PATIENT_SERVICE_RESULT.SECONDARY_SICK = string.Empty;
                                    ///  medicalExamination.CMS_PATIENT_SERVICE_RESULTS = impCMS_PATIENT_SERVICE_RESULT.GetCMS_PATIENT_SERVICE_BYSID(Convert.ToInt64(sid), UserInfo.PARTNERID);
                                    medicalExamination.PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                                    medicalExamination.PATIENT_PRESCRIPTION.MEDICINE_UNIT = 0;
                                    medicalExamination.CMS_PATIENT_SERVICE_RESULTS_INPUT = new CMS_PATIENT_SERVICE_RESULT();

                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                        }
                        else
                        {
                            medicalExamination.PATIENTINFO = new CMS_PATIENTINFO();
                            medicalExamination.ALLERGGY = new CMS_PATIENT_ALLERGY();
                            medicalExamination.HISTORIE = new CMS_PATIENT_HISTORY();
                            medicalExamination.HISTORIE.SICKNAME = string.Empty;
                            medicalExamination.PATIENT_SERVICE_RESULT = new CMS_PATIENT_SERVICE_RESULT();
                            medicalExamination.PATIENT_SERVICE_RESULT.SECONDARY_SICK = string.Empty;
                            medicalExamination.PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                            medicalExamination.PATIENT_PRESCRIPTION.MEDICINE_UNIT = 0;
                            medicalExamination.CMS_PATIENT_SERVICE_RESULTS_INPUT = new CMS_PATIENT_SERVICE_RESULT();
                            medicalExamination.PATIENT_IMAGES = new List<CMS_PATIENT_IMAGES>();
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        medicalExamination.PATIENTINFO = new CMS_PATIENTINFO();
                        medicalExamination.ALLERGGY = new CMS_PATIENT_ALLERGY();
                        medicalExamination.HISTORIE = new CMS_PATIENT_HISTORY();
                        medicalExamination.HISTORIE.SICKNAME = string.Empty;
                        medicalExamination.PATIENT_SERVICE_RESULT = new CMS_PATIENT_SERVICE_RESULT();
                        medicalExamination.PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                        medicalExamination.PATIENT_PRESCRIPTION.MEDICINE_UNIT = 0;
                        medicalExamination.PATIENT_SERVICE_RESULT.SECONDARY_SICK = string.Empty;
                        medicalExamination.CMS_PATIENT_SERVICE_RESULTS_INPUT = new CMS_PATIENT_SERVICE_RESULT();
                        medicalExamination.PATIENT_IMAGES = new List<CMS_PATIENT_IMAGES>();
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }

                }
                else
                {
                    medicalExamination.PATIENTINFO = new CMS_PATIENTINFO();
                    medicalExamination.ALLERGGY = new CMS_PATIENT_ALLERGY();
                    medicalExamination.HISTORIE = new CMS_PATIENT_HISTORY();
                    medicalExamination.HISTORIE.SICKNAME = string.Empty;
                    medicalExamination.PATIENT_SERVICE_RESULT = new CMS_PATIENT_SERVICE_RESULT();
                    medicalExamination.PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                    medicalExamination.PATIENT_PRESCRIPTION.MEDICINE_UNIT = 0;
                    medicalExamination.PATIENT_SERVICE_RESULT.SECONDARY_SICK = string.Empty;
                    medicalExamination.CMS_PATIENT_SERVICE_RESULTS_INPUT = new CMS_PATIENT_SERVICE_RESULT();
                    medicalExamination.PATIENT_IMAGES = new List<CMS_PATIENT_IMAGES>();
                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }

                //Lấy thông tin phòng khám
                CMS_SERVICE_TYPE _SERVICE_TYPE = impCMS_SERVICE_TYPE.Get(Convert.ToInt32(channel), UserInfo.PARTNERID);
                if (_SERVICE_TYPE != null)
                {
                    if (!string.IsNullOrEmpty(_SERVICE_TYPE.NAME))
                    {
                        medicalExamination.channelName = _SERVICE_TYPE.NAME;
                    }
                    else
                    {
                        medicalExamination.channelName = "Khám bệnh";
                    }
                }
                else
                {
                    medicalExamination.channelName = "Khám bệnh";
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListPatientWaitingForMedical:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            medicalExamination.SERVICEID = string.Empty;
            medicalExamination.SERVICE_GROUPID = 0;
            medicalExamination.KeyPrivate = CMSLIS.Common.Common.generalKeyPrivate(sid, medicalExamination.PATIENTINFO.PID.ToString());
            medicalExamination.channel = channel;


            medicalExamination.idTemplate = 0;
            return View(medicalExamination);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult medicalExamination(CMS_Core.Models.MedicalExaminationViewModel obj, string submit)
        {
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {
                AddPageHeader("Khám bệnh ", "");
                AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
                AddBreadcrumb("Khám bệnh", "/Clinic/medicalExamination");


                string result = string.Empty;
                if (obj.CMS_PATIENT_SERVICE_RESULTS_INPUT != null)
                {
                    if (string.IsNullOrEmpty(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_RESULT))
                    {
                        obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_RESULT = string.Empty;
                    }
                    //foreach (var data in obj.CMS_PATIENT_SERVICE_RESULTS_INPUT)
                    //{
                    if (!string.IsNullOrEmpty(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.EXAMINATION_CONTENT))
                    {
                        if (obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_ID > 0)
                        {
                            List<CMS_SERVICE> _SERVICES = impCMS_SERVICE.GetCMS_SERVICEByID(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_ID, UserInfo.PARTNERID);
                            if (_SERVICES != null)
                            {
                                if (_SERVICES.Count > 0)
                                {
                                    obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_NAME = _SERVICES[0].SERVICE_NAME;
                                    obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_PRICE = _SERVICES[0].SERVICE_PRICE ?? 0;
                                    obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICETYPE = _SERVICES[0].SERVICETYPE;
                                }
                            }
                        }
                        var errors = obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.Validate(new ValidationContext(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT));
                        if (errors.ToList().Count == 0)
                        {
                            if (obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.ID == 0)
                            {

                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.CREATEBY = UserInfo.accountid;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.CREATE_DATE = DateTime.Now;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.PARTNERID = UserInfo.PARTNERID;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.PID = obj.PATIENTINFO.PID ?? 0;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.PATIENT_ID = obj.SID;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.DOCTOR_NAME = UserInfo.Hoten;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.DOCTOR_ID = UserInfo.accountid;

                                result = impCMS_PATIENT_SERVICE_RESULT.Add(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT, UserInfo.PARTNERID);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật thông tin kết quả chẩn đoán thành công";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                    AddLogAction(result, Constant.ActionInsertOK.ToString());
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật thông tin kết quả chẩn đoán không thành công";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    AddLogAction(result, Constant.ActionInsertNOK.ToString());
                                }
                            }
                            else
                            {
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.UPDATEBY = UserInfo.accountid;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.UPDATE_DATE = DateTime.Now;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.DOCTOR_NAME = UserInfo.Hoten;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.DOCTOR_ID = UserInfo.accountid;
                                result = impCMS_PATIENT_SERVICE_RESULT.Update(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT, UserInfo.PARTNERID);
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = errors.ToList()[0].ErrorMessage;
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Mời bạn nhập nội dung chuẩn đoán!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }

                    // xóa ảnh chụp siêu âm và xét nghiệm không dùng
                    if (!string.IsNullOrEmpty(obj.ListImageDelete))
                    {
                        string[] ImageDeletes = obj.ListImageDelete.Split(',');
                        foreach (string ImageDelete in ImageDeletes)
                        {
                            if (ImageDelete.Length > 0)
                            {
                                try
                                {
                                    impCMS_PATIENT_IMAGES.Delete(Convert.ToInt32(ImageDelete), UserInfo.PARTNERID);
                                }
                                catch { }
                            }
                        }

                    }

                }

                CMS_Core.Models.MedicalExaminationViewModel medicalExamination = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_ALLBYSID(Convert.ToInt64(obj.SID), 0, UserInfo.PARTNERID);



                if (medicalExamination.PATIENT != null)
                {
                    if (medicalExamination.PATIENT.SID > 0)
                    {

                        obj.PATIENT = medicalExamination.PATIENT;
                        obj.PATIENTINFO = medicalExamination.PATIENTINFO;
                        obj.CMS_PATIENT_SERVICES = medicalExamination.CMS_PATIENT_SERVICES;
                        obj.HISTORIES = medicalExamination.HISTORIES;
                        obj.ALLERGGYS = medicalExamination.ALLERGGYS;
                        obj.PATIENTS = medicalExamination.PATIENTS;
                        obj.CMS_PATIENT_SERVICE_RESULTS = medicalExamination.CMS_PATIENT_SERVICE_RESULTS;
                        obj.CMS_PATIENT_PRESCRIPTIONS = medicalExamination.CMS_PATIENT_PRESCRIPTIONS;
                        obj.PATIENT_IMAGES = medicalExamination.PATIENT_IMAGES;


                        obj.HISTORIE = new CMS_PATIENT_HISTORY();
                        obj.HISTORIE.SICKNAME = string.Empty;
                        obj.ALLERGGY = new CMS_PATIENT_ALLERGY();
                        obj.PATIENT_SERVICE_RESULT = new CMS_PATIENT_SERVICE_RESULT();
                        obj.PATIENT_SERVICE_RESULT.SECONDARY_SICK = string.Empty;
                        //  obj.CMS_PATIENT_SERVICE_RESULTS = impCMS_PATIENT_SERVICE_RESULT.GetCMS_PATIENT_SERVICE_BYSID(Convert.ToInt64(obj.SID), UserInfo.PARTNERID);
                        obj.PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                        obj.PATIENT_PRESCRIPTION.MEDICINE_UNIT = 0;
                        obj.CMS_PATIENT_SERVICE_RESULTS_INPUT = new CMS_PATIENT_SERVICE_RESULT();


                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }

                obj.SERVICEID = string.Empty;
                obj.SERVICE_GROUPID = 0;
                obj.KeyPrivate = CMSLIS.Common.Common.generalKeyPrivate(obj.SID.ToString(), obj.PATIENTINFO.PID.ToString());
                //Lấy thông tin phòng khám
                CMS_SERVICE_TYPE _SERVICE_TYPE = impCMS_SERVICE_TYPE.Get(Convert.ToInt32(obj.channel), UserInfo.PARTNERID);
                if (_SERVICE_TYPE != null)
                {
                    if (!string.IsNullOrEmpty(_SERVICE_TYPE.NAME))
                    {
                        obj.channelName = _SERVICE_TYPE.NAME;
                    }
                    else
                    {
                        obj.channelName = "Khám bệnh";
                    }
                }
                else
                {
                    obj.channelName = "Khám bệnh";
                }


            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi trong quá trình cập nhật";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientsAdd: " + ex.ToString());
            }





            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetInfoTemplate(string idTemplate)
        {
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                List<CMS_TEMPLATERESULTEXAMINATION> modelList = new List<CMS_TEMPLATERESULTEXAMINATION>();


                modelList.Add(impCMS_TEMPLATERESULTEXAMINATION.Get(Convert.ToInt32(idTemplate), UserInfo.PARTNERID));

                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("GetInfoTemplate: " + idTemplate + "  " + ex.ToString());
                return Json(string.Empty);
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetResultPatient(string ID)
        {
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                List<CMS_PATIENT_SERVICE_RESULT> modelList = new List<CMS_PATIENT_SERVICE_RESULT>();

                modelList.Add(impCMS_PATIENT_SERVICE_RESULT.Get(Convert.ToInt32(ID), UserInfo.PARTNERID));

                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("GetResultPatient: " + ID + "  " + ex.ToString());
                return Json(string.Empty);
            }
        }




        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult SavePATIENT_PRESCRIPTION(string MEDICINE_NAME, string MEDICINE_ID, string MEDICINE_AMOUNT, string MEDICINE_USAGE, string CHUANDOAN, string TAIKHAM_VIEW, string GHICHU, string SID, string PID, string id)
        {
            try
            {
                if (Convert.ToInt32(MEDICINE_ID) > 0 && Convert.ToInt64(SID) > 0 && Convert.ToInt64(PID) > 0 && Convert.ToInt64(MEDICINE_AMOUNT) > 0)
                {

                    if (string.IsNullOrEmpty(MEDICINE_USAGE))
                        MEDICINE_USAGE = string.Empty;

                    if (string.IsNullOrEmpty(CHUANDOAN))
                        CHUANDOAN = string.Empty;
                    if (string.IsNullOrEmpty(GHICHU))
                        GHICHU = string.Empty;



                    List<CMS_PATIENT_PRESCRIPTION> PATIENT_PRESCRIPTION = new List<CMS_PATIENT_PRESCRIPTION>();
                    if (Session["PATIENT_PRESCRIPTION"] != null)
                    {
                        PATIENT_PRESCRIPTION = (List<CMS_PATIENT_PRESCRIPTION>)Session["PATIENT_PRESCRIPTION"];
                    }

                    if (PATIENT_PRESCRIPTION.Find(x => x.MEDICINE_ID.ToString().Equals(MEDICINE_ID) && id.Equals("0")) != null)
                    {
                        return Json("2");
                    }
                    else
                    {

                        var UserInfo = ((cms_Account)Session["UserInfo"]);
                        // ImpCMS_PATIENT_PRESCRIPTION impCMS_PATIENT_PRESCRIPTION = new ImpCMS_PATIENT_PRESCRIPTION();
                        List<CMS_MEDICINE> CMS_MEDICINES = impCMS_MEDICINE.GetCMS_MEDICINEByID(Convert.ToInt32(MEDICINE_ID), UserInfo.PARTNERID);
                        if (CMS_MEDICINES != null)
                        {
                            if (CMS_MEDICINES.Count > 0)
                            {
                                CMS_PATIENT_PRESCRIPTION _PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                                if ("0".Equals(id))
                                {
                                    _PATIENT_PRESCRIPTION.SID = Convert.ToInt64(SID);
                                    _PATIENT_PRESCRIPTION.PID = Convert.ToInt64(PID);
                                    _PATIENT_PRESCRIPTION.PARTNERID = UserInfo.PARTNERID;
                                    _PATIENT_PRESCRIPTION.CREATEBY = UserInfo.accountid;
                                    _PATIENT_PRESCRIPTION.CREATE_DATE = DateTime.Now;
                                    _PATIENT_PRESCRIPTION.DOCTOR_ID = UserInfo.accountid;
                                    _PATIENT_PRESCRIPTION.DOCTOR_NAME = UserInfo.Hoten;
                                    _PATIENT_PRESCRIPTION.MEDICINE_CODE = CMS_MEDICINES[0].MEDICINE_CODE;
                                    _PATIENT_PRESCRIPTION.MEDICINE_ID = CMS_MEDICINES[0].ID;
                                    _PATIENT_PRESCRIPTION.MEDICINE_NAME = CMS_MEDICINES[0].MEDICINE_NAME;
                                    _PATIENT_PRESCRIPTION.MEDICINE_UNIT = CMS_MEDICINES[0].MEDICINE_UNIT;
                                    _PATIENT_PRESCRIPTION.MEDICINE_USAGE = MEDICINE_USAGE;
                                    _PATIENT_PRESCRIPTION.COUNT = Convert.ToInt32(MEDICINE_AMOUNT);
                                    var errors = _PATIENT_PRESCRIPTION.Validate(new ValidationContext(_PATIENT_PRESCRIPTION));
                                    if (errors.ToList().Count == 0)
                                    {
                                        DateTime dateTime = new DateTime();
                                        if (!string.IsNullOrEmpty(TAIKHAM_VIEW))
                                        {
                                            try
                                            {
                                                dateTime = DateTime.ParseExact(TAIKHAM_VIEW.Trim(), "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
                                                if ((dateTime - DateTime.Now).TotalDays < 1)
                                                {
                                                    return Json("1");
                                                }
                                            }
                                            catch
                                            {
                                                return Json("2");
                                            }
                                        }


                                        string result = impCMS_PATIENT_PRESCRIPTION.Add(_PATIENT_PRESCRIPTION, UserInfo.PARTNERID);
                                        if (!string.IsNullOrEmpty(result))
                                        {
                                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                                            PATIENT_PRESCRIPTION.Add(_PATIENT_PRESCRIPTION);
                                            Session["PATIENT_PRESCRIPTION"] = PATIENT_PRESCRIPTION;
                                            impCMS_PATIENT.UpdateChanDoanCMS_PATIENT(Convert.ToInt64(SID), CHUANDOAN, dateTime, GHICHU);
                                            return Json(result);
                                        }
                                        else
                                        {
                                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                                            return Json("0");
                                        }
                                    }
                                }
                                else
                                {
                                    _PATIENT_PRESCRIPTION.ID = Convert.ToInt64(id);
                                    _PATIENT_PRESCRIPTION.SID = Convert.ToInt64(SID);
                                    _PATIENT_PRESCRIPTION.PID = Convert.ToInt64(PID);
                                    _PATIENT_PRESCRIPTION.PARTNERID = UserInfo.PARTNERID;
                                    _PATIENT_PRESCRIPTION.UPDATEBY = UserInfo.accountid;
                                    _PATIENT_PRESCRIPTION.UPDATE_DATE = DateTime.Now;
                                    _PATIENT_PRESCRIPTION.DOCTOR_ID = UserInfo.accountid;
                                    _PATIENT_PRESCRIPTION.DOCTOR_NAME = UserInfo.Hoten;
                                    _PATIENT_PRESCRIPTION.MEDICINE_CODE = CMS_MEDICINES[0].MEDICINE_CODE;
                                    _PATIENT_PRESCRIPTION.MEDICINE_ID = CMS_MEDICINES[0].ID;
                                    _PATIENT_PRESCRIPTION.MEDICINE_NAME = CMS_MEDICINES[0].MEDICINE_NAME;
                                    _PATIENT_PRESCRIPTION.MEDICINE_UNIT = CMS_MEDICINES[0].MEDICINE_UNIT;
                                    _PATIENT_PRESCRIPTION.MEDICINE_USAGE = MEDICINE_USAGE;
                                    _PATIENT_PRESCRIPTION.COUNT = Convert.ToInt32(MEDICINE_AMOUNT);
                                    var errors = _PATIENT_PRESCRIPTION.Validate(new ValidationContext(_PATIENT_PRESCRIPTION));
                                    if (errors.ToList().Count == 0)
                                    {
                                        string result = impCMS_PATIENT_PRESCRIPTION.Update(_PATIENT_PRESCRIPTION, UserInfo.PARTNERID);
                                        if (!string.IsNullOrEmpty(result))
                                        {
                                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                            PATIENT_PRESCRIPTION.Add(_PATIENT_PRESCRIPTION);
                                            Session["PATIENT_PRESCRIPTION"] = PATIENT_PRESCRIPTION;


                                            DateTime dateTime = new DateTime();
                                            if (!string.IsNullOrEmpty(TAIKHAM_VIEW))
                                            {


                                                try
                                                {
                                                    dateTime = DateTime.ParseExact(TAIKHAM_VIEW.Trim(), "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
                                                    if ((dateTime - DateTime.Now).TotalDays < 1)
                                                    {
                                                        return Json("1");
                                                    }
                                                }
                                                catch
                                                {
                                                    return Json("2");
                                                }
                                            }


                                            impCMS_PATIENT.UpdateChanDoanCMS_PATIENT(Convert.ToInt64(SID), CHUANDOAN, dateTime, GHICHU);

                                            return Json(_PATIENT_PRESCRIPTION.ID);
                                        }
                                        else
                                        {
                                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                            return Json("0");
                                        }
                                    }
                                }
                            }
                        }


                    }
                }
                // Info.  
                return Json("0");
            }
            catch
            {
                return Json("0");
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult DeletePATIENT_PRESCRIPTION(string[] customerIDs, string SID, string PID)
        {
            try
            {
                if (Convert.ToInt64(SID) > 0 && Convert.ToInt64(PID) > 0 && customerIDs != null)
                {
                    var toRemove = new List<CMS_PATIENT_PRESCRIPTION>();
                    List<CMS_PATIENT_PRESCRIPTION> PATIENT_PRESCRIPTION = null;
                    if (Session["PATIENT_PRESCRIPTION"] != null)
                    {
                        PATIENT_PRESCRIPTION = (List<CMS_PATIENT_PRESCRIPTION>)Session["PATIENT_PRESCRIPTION"];
                    }

                    var UserInfo = ((cms_Account)Session["UserInfo"]);
                    ImpCMS_PATIENT_PRESCRIPTION impCMS_PATIENT_PRESCRIPTION = new ImpCMS_PATIENT_PRESCRIPTION();

                    foreach (string customerID in customerIDs)
                    {
                        try
                        {
                            if (PATIENT_PRESCRIPTION != null)
                            {
                                foreach (CMS_PATIENT_PRESCRIPTION _PATIENT_PRESCRIPTION in PATIENT_PRESCRIPTION.ToList())
                                {
                                    if (customerID.Equals(_PATIENT_PRESCRIPTION.MEDICINE_ID.ToString().Trim()))
                                    {
                                        toRemove.Add(_PATIENT_PRESCRIPTION);
                                        // Xóa hẳn dữ liệu ra khỏi phiếu nhập
                                        if (_PATIENT_PRESCRIPTION.MEDICINE_ID > 0)
                                        {
                                            impCMS_PATIENT_PRESCRIPTION.Delete(Convert.ToInt32(SID), Convert.ToInt32(PID), Convert.ToInt32(customerID), UserInfo.PARTNERID);
                                            AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                impCMS_PATIENT_PRESCRIPTION.Delete(Convert.ToInt32(SID), Convert.ToInt32(PID), Convert.ToInt32(customerID), UserInfo.PARTNERID);
                                AddLogAction(customerID, Constant.ActionDeleteOK.ToString());

                            }
                        }
                        catch
                        {
                            return Json("0");
                        }
                    }

                    if (toRemove != null)
                    {
                        if (toRemove.Count > 0)
                        {
                            PATIENT_PRESCRIPTION.RemoveAll(toRemove.Contains);
                            Session["PATIENT_PRESCRIPTION"] = PATIENT_PRESCRIPTION;
                        }
                    }
                    return Json("1");
                }
                return Json("0");
            }
            catch
            {
                return Json("0");
            }
        }





        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult SaveSERVICEExamination(string[] locationID, string[] SERVICEID, int pid, int sid)
        {
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);

                string textCheckLocation = string.Empty;
                string LOCATIONIDNEW = string.Empty;
                if (SERVICEID != null)
                {
                    if (locationID != null)
                    {
                        foreach (string LOCATIONIDValue in locationID)
                        {
                            LOCATIONIDNEW = LOCATIONIDNEW + LOCATIONIDValue + ",";
                            bool checkLocation = false;
                            foreach (string service in SERVICEID)
                            {
                                List<CMS_SERVICE> datas = impCMS_SERVICE.GetCMS_SERVICEByID(Convert.ToInt32(service), UserInfo.PARTNERID);
                                if (datas != null)
                                {
                                    if (datas.Count > 0)
                                    {
                                        if (impCMS_ROOM_CLINIC.CheckRoomAndService(Convert.ToInt32(LOCATIONIDValue), datas[0].ID, datas[0].SERVICETYPE, UserInfo.PARTNERID))
                                        {
                                            checkLocation = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (!checkLocation)
                            {
                                textCheckLocation = textCheckLocation + " Mời bạn dịch vụ cho phòng: " + impCMS_ROOM_CLINIC.Get(Convert.ToInt32(LOCATIONIDValue), UserInfo.PARTNERID).ROOM_NAME + ",";
                            }

                        }

                        if (textCheckLocation.Length > 0)
                        {
                            textCheckLocation = textCheckLocation.Substring(0, textCheckLocation.Length - 1);
                        }
                        if (LOCATIONIDNEW.Length > 0)
                        {
                            LOCATIONIDNEW = LOCATIONIDNEW.Substring(0, LOCATIONIDNEW.Length - 1);
                        }



                        if (textCheckLocation.Length == 0)
                        {
                            foreach (string service in SERVICEID)
                            {

                                List<CMS_SERVICE> datas = impCMS_SERVICE.GetCMS_SERVICEByID(Convert.ToInt32(service), UserInfo.PARTNERID);
                                if (datas != null)
                                {
                                    if (datas.Count > 0)
                                    {
                                        CMS_PATIENT_SERVICE _PATIENT_SERVICE = new CMS_PATIENT_SERVICE();
                                        _PATIENT_SERVICE.PID = pid; // Giá trị pid của bệnh nhân
                                        _PATIENT_SERVICE.PATIENT_ID = sid;
                                        _PATIENT_SERVICE.SERVICE_ID = datas[0].ID;
                                        _PATIENT_SERVICE.SERVICE_NAME = datas[0].SERVICE_NAME;
                                        _PATIENT_SERVICE.SERVICE_PRICE = datas[0].SERVICE_PRICE ?? 0;
                                        _PATIENT_SERVICE.CREATEBY = UserInfo.accountid;
                                        _PATIENT_SERVICE.CREATE_DATE = DateTime.Now;
                                        _PATIENT_SERVICE.IS_PRINT = 0;
                                        _PATIENT_SERVICE.IS_PRINT_RESULT = 0;
                                        _PATIENT_SERVICE.IS_PAY = 0;
                                        _PATIENT_SERVICE.COUNT = 1;
                                        _PATIENT_SERVICE.PARTNERID = UserInfo.PARTNERID;
                                        _PATIENT_SERVICE.SERVICETYPE = datas[0].SERVICETYPE;
                                        _PATIENT_SERVICE.DOCTOR_ID = UserInfo.accountid;
                                        _PATIENT_SERVICE.SERVICE_GROUP_ID = 0;

                                        var errors = _PATIENT_SERVICE.Validate(new ValidationContext(_PATIENT_SERVICE));
                                        if (errors.ToList().Count == 0)
                                        {
                                            impCMS_PATIENT_SERVICE.AddService(_PATIENT_SERVICE, UserInfo.PARTNERID);

                                        }
                                    }
                                }
                            }

                            foreach (string LOCATIONIDValue in locationID)
                            {
                                impCMS_PATIENTINFO_QUEUE.Add(Convert.ToInt64(sid), Convert.ToInt64(pid), Convert.ToInt32(LOCATIONIDValue), UserInfo.PARTNERID);
                            }

                            impCMS_PATIENT.UpdateListRoomCMS_PATIENT(Convert.ToInt64(sid), LOCATIONIDNEW);

                            return Json("1");
                        }
                        else
                        {
                            return Json(textCheckLocation);
                        }



                    }
                    else
                    {
                        return Json("4");
                    }

                }
                else
                {
                    return Json("3");
                }

                // Info.  
                // return Json("0");
            }
            catch (Exception ex)
            {
                return Json("0");
            }
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult SavemedicalExamination(CMS_PATIENT_SERVICE_RESULT PATIENT_SERVICE_RESULT)
        {
            try
            {
                List<CMS_PATIENT_SERVICE_RESULT> PATIENTSERVICE_RESULT = new List<CMS_PATIENT_SERVICE_RESULT>();

                var UserInfo = ((cms_Account)Session["UserInfo"]);
                if (Session["medicalExamination"] != null)
                {
                    PATIENTSERVICE_RESULT = (List<CMS_PATIENT_SERVICE_RESULT>)Session["medicalExamination"];
                }

                if (PATIENTSERVICE_RESULT.Find(x => x.SERVICE_RESULT.Equals(PATIENT_SERVICE_RESULT.SERVICE_RESULT)) != null)
                {
                    return Json("2");
                }
                else
                {
                    PATIENT_SERVICE_RESULT.CREATEBY = UserInfo.accountid;
                    PATIENT_SERVICE_RESULT.CREATE_DATE = DateTime.Now;
                    PATIENT_SERVICE_RESULT.PARTNERID = UserInfo.PARTNERID;

                    PATIENT_SERVICE_RESULT.DOCTOR_NAME = UserInfo.Hoten;
                    PATIENT_SERVICE_RESULT.DOCTOR_ID = UserInfo.accountid;

                    var errors = PATIENT_SERVICE_RESULT.Validate(new ValidationContext(PATIENT_SERVICE_RESULT));

                    if (errors.ToList().Count == 0)
                    {
                        ImpCMS_PATIENT_SERVICE_RESULT impCMS_PATIENT_SERVICE_RESULT = new ImpCMS_PATIENT_SERVICE_RESULT();
                        string result = impCMS_PATIENT_SERVICE_RESULT.Add(PATIENT_SERVICE_RESULT, UserInfo.PARTNERID);
                        if (string.IsNullOrEmpty(result))
                        {
                            return Json("1");
                        }
                    }

                }

                // Info.  
                return Json("0");
            }
            catch
            {
                return Json("0");
            }
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult SaveHISTORIEExamination(string SICKNAME, string SICKYEAR, string NOTE, int pid, int id)
        {
            try
            {
                CMS_PATIENT_HISTORY _PATIENT_HISTORY = new CMS_PATIENT_HISTORY();
                List<CMS_PATIENT_HISTORY> _PATIENT_HISTORYS = new List<CMS_PATIENT_HISTORY>();
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                if (Session["SaveHISTORIE1"] != null)
                {
                    _PATIENT_HISTORYS = (List<CMS_PATIENT_HISTORY>)Session["SaveHISTORIE1"];
                }

                if (!string.IsNullOrEmpty(SICKNAME))
                {
                    _PATIENT_HISTORY.SICKNAME = SICKNAME;
                    _PATIENT_HISTORY.SICKYEAR = Convert.ToInt32(SICKYEAR);
                    _PATIENT_HISTORY.NOTE = NOTE;
                    _PATIENT_HISTORY.PID = pid;
                    _PATIENT_HISTORY.CREATEBY = UserInfo.accountid;
                    _PATIENT_HISTORY.CREATEDATE = DateTime.Now;
                    _PATIENT_HISTORY.PARTNERID = UserInfo.PARTNERID;

                    if (_PATIENT_HISTORYS.Find(x => x.SICKNAME.Equals(SICKNAME) && x.SICKYEAR == Convert.ToInt32(SICKYEAR) && id == 0) != null)
                    {
                        return Json("2");

                    }
                    else
                    {
                        var errors = _PATIENT_HISTORY.Validate(new ValidationContext(_PATIENT_HISTORY));
                        if (errors.ToList().Count == 0)
                        {

                            if (id == 0)
                            {
                                if (pid != 0)
                                {
                                    string result = impCMS_PATIENT_HISTORY.InsertCMS_PATIENT_HISTORY(_PATIENT_HISTORY);
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        _PATIENT_HISTORYS.Add(_PATIENT_HISTORY);
                                        Session["SaveHISTORIE1"] = _PATIENT_HISTORYS;
                                        AddLogAction(result, Constant.ActionInsertOK.ToString());
                                        return Json(result);
                                    }
                                    else
                                    {
                                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                                    }
                                }
                                else
                                {
                                    _PATIENT_HISTORYS.Add(_PATIENT_HISTORY);
                                    Session["SaveHISTORIE1"] = _PATIENT_HISTORYS;
                                    return Json("1");
                                }
                            }
                            else
                            {
                                _PATIENT_HISTORY.UPDATEBY = UserInfo.accountid;
                                _PATIENT_HISTORY.UPDATEDATE = DateTime.Now;
                                _PATIENT_HISTORY.ID = Convert.ToInt32(id);
                                string result = impCMS_PATIENT_HISTORY.UpdateCMS_PATIENT_HISTORY(_PATIENT_HISTORY);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    _PATIENT_HISTORYS.Add(_PATIENT_HISTORY);
                                    Session["SaveHISTORIE1"] = _PATIENT_HISTORYS;
                                    AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                    return Json(_PATIENT_HISTORY.ID);
                                }
                                else
                                {
                                    AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                                }

                            }

                        }


                        return Json("0");
                    }
                }
                // Info.  
                return Json("0");
            }
            catch
            {
                return Json("0");
            }
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult SaveALLERGGYExamination(string ALLERGY_NAME, string ALLERGY_TYPE, string ALLERGY_NOTE, int pid, int id)
        {
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);

                CMS_PATIENT_ALLERGY _PATIENT_ALLERGY = new CMS_PATIENT_ALLERGY();

                List<CMS_PATIENT_ALLERGY> _PATIENT_ALLERGYS = new List<CMS_PATIENT_ALLERGY>();

                if (Session["SaveALLERGGY1"] != null)
                {
                    _PATIENT_ALLERGYS = (List<CMS_PATIENT_ALLERGY>)Session["SaveALLERGGY1"];
                }

                if (!string.IsNullOrEmpty(ALLERGY_NAME))
                {
                    _PATIENT_ALLERGY.ALLERGY_NAME = ALLERGY_NAME;
                    _PATIENT_ALLERGY.ALLERGY_TYPE = Convert.ToInt32(ALLERGY_TYPE);
                    _PATIENT_ALLERGY.ALLERGY_NOTE = ALLERGY_NOTE;
                    _PATIENT_ALLERGY.PID = pid;
                    _PATIENT_ALLERGY.CREATEBY = UserInfo.accountid;
                    _PATIENT_ALLERGY.CREATEDATE = DateTime.Now;
                    _PATIENT_ALLERGY.PARTNERID = UserInfo.PARTNERID;


                    if (_PATIENT_ALLERGYS.Find(x => x.ALLERGY_NAME.Equals(ALLERGY_NAME) && x.ALLERGY_TYPE == Convert.ToInt32(ALLERGY_TYPE) && id == 0) != null)
                    {
                        return Json("2");
                    }
                    else
                    {
                        var errors = _PATIENT_ALLERGY.Validate(new ValidationContext(_PATIENT_ALLERGY));
                        if (errors.ToList().Count == 0)
                        {
                            if (id == 0)
                            {
                                string result = impCMS_PATIENT_ALLERGY.InsertCMS_PATIENT_ALLERGY(_PATIENT_ALLERGY);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    _PATIENT_ALLERGYS.Add(_PATIENT_ALLERGY);
                                    Session["SaveALLERGGY1"] = _PATIENT_ALLERGYS;
                                    AddLogAction(result, Constant.ActionInsertOK.ToString());
                                    return Json(result);
                                }
                                else
                                {
                                    AddLogAction(result, Constant.ActionInsertNOK.ToString());
                                }
                            }
                            else
                            {
                                _PATIENT_ALLERGY.UPDATEBY = UserInfo.accountid;
                                _PATIENT_ALLERGY.UPDATEDATE = DateTime.Now;
                                _PATIENT_ALLERGY.ID = Convert.ToInt32(id);
                                string result = impCMS_PATIENT_ALLERGY.UpdateCMS_PATIENT_ALLERGY(_PATIENT_ALLERGY);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    _PATIENT_ALLERGYS.Add(_PATIENT_ALLERGY);
                                    Session["SaveALLERGGY1"] = _PATIENT_ALLERGYS;
                                    AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                    return Json(_PATIENT_ALLERGY.ID);
                                }
                                else
                                {
                                    AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                                }

                            }

                        }

                    }
                }
                // Info.  
                return Json("0");
            }
            catch
            {
                return Json("0");
            }
        }



        #endregion
        #region --> Khám bệnh Nội soi, siêu âm

        public ActionResult medicalExaminationSupersonic(string sid)
        {
            // Initialization.  
            AddPageHeader("Khám bệnh ", "");
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatientWaitingForMedical");
            AddBreadcrumb("Khám bệnh siêu âm", "/Clinic/medicalExaminationSupersonic");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            CMS_Core.Models.MedicalExaminationViewModel medicalExamination = new CMS_Core.Models.MedicalExaminationViewModel();
            Session["SaveALLERGGY1"] = null;
            Session["SaveHISTORIE1"] = null;
            Session["medicalExamination"] = null;
            Session["PATIENT_PRESCRIPTION"] = null;

            try
            {

                if (!string.IsNullOrWhiteSpace(sid))
                {
                    medicalExamination.SID = Convert.ToInt32(sid);


                    List<CMS_PATIENT> patients = impCMS_PATIENT.GetCMS_PATIENT_BYSID(sid, UserInfo.PARTNERID);
                    if (patients != null)
                    {
                        if (patients.Count > 0)
                        {
                            medicalExamination.PATIENT = patients[0];
                            List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(medicalExamination.PATIENT.PID.ToString(), UserInfo.PARTNERID);
                            if (_PATIENTINFOS != null)
                            {
                                if (_PATIENTINFOS.Count > 0)
                                {
                                    medicalExamination.PATIENTINFO = _PATIENTINFOS[0];
                                }
                            }


                            medicalExamination.CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetCMS_PATIENT_SERVICE_BYSID(Convert.ToInt64(sid), UserInfo.PARTNERID);
                            medicalExamination.HISTORIES = impCMS_PATIENT_HISTORY.GetCMS_PATIENT_HISTORYByPID(medicalExamination.PATIENTINFO.PID ?? 0, UserInfo.PARTNERID);
                            medicalExamination.ALLERGGYS = impCMS_PATIENT_ALLERGY.GetCMS_PATIENT_ALLERGYByPID(medicalExamination.PATIENTINFO.PID ?? 0, UserInfo.PARTNERID);
                            medicalExamination.PATIENTS = impCMS_PATIENT.GetCMS_PATIENTByPID((medicalExamination.PATIENTINFO.PID ?? 0).ToString(), UserInfo.PARTNERID);
                            Session["SaveALLERGGY1"] = medicalExamination.ALLERGGYS;
                            Session["SaveHISTORIE1"] = medicalExamination.HISTORIES;

                            medicalExamination.HISTORIE = new CMS_PATIENT_HISTORY();
                            medicalExamination.HISTORIE.SICKNAME = string.Empty;
                            medicalExamination.ALLERGGY = new CMS_PATIENT_ALLERGY();
                            medicalExamination.PATIENT_SERVICE_RESULT = new CMS_PATIENT_SERVICE_RESULT();
                            medicalExamination.PATIENT_SERVICE_RESULT.SECONDARY_SICK = string.Empty;
                            medicalExamination.CMS_PATIENT_SERVICE_RESULTS = impCMS_PATIENT_SERVICE_RESULT.GetCMS_PATIENT_SERVICE_BYSID(Convert.ToInt64(sid), UserInfo.PARTNERID);
                            medicalExamination.PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                            medicalExamination.PATIENT_PRESCRIPTION.MEDICINE_UNIT = 0;
                            medicalExamination.CMS_PATIENT_SERVICE_RESULTS_INPUT = new CMS_PATIENT_SERVICE_RESULT();
                            medicalExamination.CMS_PATIENT_PRESCRIPTIONS = impCMS_PATIENT_PRESCRIPTION.GetCMS_PATIENT_PRESCRIPTION_BYSID(Convert.ToInt64(sid), UserInfo.PARTNERID);



                        }
                        else
                        {
                            medicalExamination.PATIENTINFO = new CMS_PATIENTINFO();
                            medicalExamination.ALLERGGY = new CMS_PATIENT_ALLERGY();
                            medicalExamination.HISTORIE = new CMS_PATIENT_HISTORY();
                            medicalExamination.HISTORIE.SICKNAME = string.Empty;
                            medicalExamination.PATIENT_SERVICE_RESULT = new CMS_PATIENT_SERVICE_RESULT();
                            medicalExamination.PATIENT_SERVICE_RESULT.SECONDARY_SICK = string.Empty;
                            medicalExamination.PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                            medicalExamination.PATIENT_PRESCRIPTION.MEDICINE_UNIT = 0;
                            medicalExamination.CMS_PATIENT_SERVICE_RESULTS_INPUT = new CMS_PATIENT_SERVICE_RESULT();
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                        }
                    }
                    else
                    {
                        medicalExamination.PATIENTINFO = new CMS_PATIENTINFO();
                        medicalExamination.ALLERGGY = new CMS_PATIENT_ALLERGY();
                        medicalExamination.HISTORIE = new CMS_PATIENT_HISTORY();
                        medicalExamination.HISTORIE.SICKNAME = string.Empty;
                        medicalExamination.PATIENT_SERVICE_RESULT = new CMS_PATIENT_SERVICE_RESULT();
                        medicalExamination.PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                        medicalExamination.PATIENT_PRESCRIPTION.MEDICINE_UNIT = 0;
                        medicalExamination.PATIENT_SERVICE_RESULT.SECONDARY_SICK = string.Empty;
                        medicalExamination.CMS_PATIENT_SERVICE_RESULTS_INPUT = new CMS_PATIENT_SERVICE_RESULT();
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                    }

                }
                else
                {
                    medicalExamination.PATIENTINFO = new CMS_PATIENTINFO();
                    medicalExamination.ALLERGGY = new CMS_PATIENT_ALLERGY();
                    medicalExamination.HISTORIE = new CMS_PATIENT_HISTORY();
                    medicalExamination.HISTORIE.SICKNAME = string.Empty;
                    medicalExamination.PATIENT_SERVICE_RESULT = new CMS_PATIENT_SERVICE_RESULT();
                    medicalExamination.PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                    medicalExamination.PATIENT_PRESCRIPTION.MEDICINE_UNIT = 0;
                    medicalExamination.PATIENT_SERVICE_RESULT.SECONDARY_SICK = string.Empty;
                    medicalExamination.CMS_PATIENT_SERVICE_RESULTS_INPUT = new CMS_PATIENT_SERVICE_RESULT();
                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                }


            }
            catch (Exception ex)
            {
                logError.Info("ListPatientWaitingForMedical:" + ex.ToString());
            }

            // Info.  
            medicalExamination.SERVICEID = string.Empty;
            medicalExamination.SERVICE_GROUPID = 0;
            return View(medicalExamination);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult medicalExaminationSupersonic(CMS_Core.Models.MedicalExaminationViewModel obj, string submit)
        {
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {
                AddPageHeader("Khám bệnh ", "");
                AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
                AddBreadcrumb("Khám bệnh", "/Clinic/medicalExamination");


                string result = string.Empty;
                if (obj.CMS_PATIENT_SERVICE_RESULTS_INPUT != null)
                {
                    //foreach (var data in obj.CMS_PATIENT_SERVICE_RESULTS_INPUT)
                    //{
                    if (!string.IsNullOrEmpty(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_RESULT))
                    {
                        if (obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_ID > 0)
                        {
                            List<CMS_SERVICE> _SERVICES = impCMS_SERVICE.GetCMS_SERVICEByID(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_ID, UserInfo.PARTNERID);
                            if (_SERVICES != null)
                            {
                                if (_SERVICES.Count > 0)
                                {
                                    obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_NAME = _SERVICES[0].SERVICE_NAME;
                                    obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.SERVICE_PRICE = _SERVICES[0].SERVICE_PRICE ?? 0;
                                }
                            }
                        }
                        var errors = obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.Validate(new ValidationContext(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT));
                        if (errors.ToList().Count == 0)
                        {
                            if (obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.ID == 0)
                            {

                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.CREATEBY = UserInfo.accountid;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.CREATE_DATE = DateTime.Now;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.PARTNERID = UserInfo.PARTNERID;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.PID = obj.PATIENTINFO.PID ?? 0;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.PATIENT_ID = obj.SID;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.DOCTOR_NAME = UserInfo.Hoten;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.DOCTOR_ID = UserInfo.accountid;

                                result = impCMS_PATIENT_SERVICE_RESULT.Add(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT, UserInfo.PARTNERID);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật thông tin kết quả chẩn đoán thành công";
                                    AddLogAction(result, Constant.ActionInsertOK.ToString());
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật thông tin kết quả chẩn đoán không thành công";
                                    AddLogAction(result, Constant.ActionInsertNOK.ToString());
                                }
                            }
                            else
                            {
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.UPDATEBY = UserInfo.accountid;
                                obj.CMS_PATIENT_SERVICE_RESULTS_INPUT.UPDATE_DATE = DateTime.Now;

                                result = impCMS_PATIENT_SERVICE_RESULT.Update(obj.CMS_PATIENT_SERVICE_RESULTS_INPUT, UserInfo.PARTNERID);
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = errors.ToList()[0].ErrorMessage;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Mời bạn nhập kết quả chuẩn đoán!";
                    }


                    //}
                }


            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi trong quá trình cập nhật";
                logError.Info("ListPatientsAdd: " + ex.ToString());
            }




            List<CMS_PATIENT> patients = impCMS_PATIENT.GetCMS_PATIENT_BYSID(obj.SID.ToString(), UserInfo.PARTNERID);

            if (patients != null)
            {
                if (patients.Count > 0)
                {

                    obj.PATIENT = patients[0];
                    List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(obj.PATIENT.PID.ToString(), UserInfo.PARTNERID);
                    if (_PATIENTINFOS != null)
                    {
                        if (_PATIENTINFOS.Count > 0)
                        {
                            obj.PATIENTINFO = _PATIENTINFOS[0];
                        }
                    }

                    //ImpCMS_PATIENT_SERVICE_RESULT impCMS_PATIENT_SERVICE_RESULT = new ImpCMS_PATIENT_SERVICE_RESULT();
                    obj.CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetCMS_PATIENT_SERVICE_BYSID(Convert.ToInt64(obj.SID), UserInfo.PARTNERID);
                    obj.HISTORIES = impCMS_PATIENT_HISTORY.GetCMS_PATIENT_HISTORYByPID(obj.PATIENTINFO.PID ?? 0, UserInfo.PARTNERID);
                    obj.ALLERGGYS = impCMS_PATIENT_ALLERGY.GetCMS_PATIENT_ALLERGYByPID(obj.PATIENTINFO.PID ?? 0, UserInfo.PARTNERID);
                    obj.PATIENTS = impCMS_PATIENT.GetCMS_PATIENTByPID((obj.PATIENTINFO.PID ?? 0).ToString(), UserInfo.PARTNERID);

                    obj.HISTORIE = new CMS_PATIENT_HISTORY();
                    obj.HISTORIE.SICKNAME = string.Empty;
                    obj.ALLERGGY = new CMS_PATIENT_ALLERGY();
                    obj.PATIENT_SERVICE_RESULT = new CMS_PATIENT_SERVICE_RESULT();
                    obj.PATIENT_SERVICE_RESULT.SECONDARY_SICK = string.Empty;
                    obj.CMS_PATIENT_SERVICE_RESULTS = impCMS_PATIENT_SERVICE_RESULT.GetCMS_PATIENT_SERVICE_BYSID(Convert.ToInt64(obj.SID), UserInfo.PARTNERID);
                    obj.PATIENT_PRESCRIPTION = new CMS_PATIENT_PRESCRIPTION();
                    obj.PATIENT_PRESCRIPTION.MEDICINE_UNIT = 0;
                    obj.CMS_PATIENT_SERVICE_RESULTS_INPUT = new CMS_PATIENT_SERVICE_RESULT();
                    obj.CMS_PATIENT_PRESCRIPTIONS = impCMS_PATIENT_PRESCRIPTION.GetCMS_PATIENT_PRESCRIPTION_BYSID(Convert.ToInt64(obj.SID), UserInfo.PARTNERID);


                }
                else
                {
                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân";
                }
            }
            obj.SERVICEID = string.Empty;
            obj.SERVICE_GROUPID = 0;
            return View(obj);
        }


        public ActionResult medicalExaminationPrescription(string sid, string pid, string Token)
        {
            // Initialization.  
            AddPageHeader("In đơn thuốc", "");
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatientWaitingForMedical");
            AddBreadcrumb("In đơn thuốc", "/Clinic/medicalExaminationPrescription");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            CMS_Core.Models.MedicalExaminationViewModel obj = new CMS_Core.Models.MedicalExaminationViewModel();

            try
            {


                if (!string.IsNullOrWhiteSpace(sid) && !string.IsNullOrWhiteSpace(pid) && !string.IsNullOrWhiteSpace(Token))
                {
                    if (CMSLIS.Common.Common.CheckKeyPrivate(sid, pid, Token))
                    {
                        obj.CMS_PATIENT_PRESCRIPTIONS = impCMS_PATIENT_PRESCRIPTION.GetCMS_PATIENT_PRESCRIPTION_BYSID(Convert.ToInt64(sid), UserInfo.PARTNERID);
                        List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);
                        if (_PATIENTINFOS != null)
                        {
                            if (_PATIENTINFOS.Count > 0)
                            {
                                obj.PATIENTINFO = _PATIENTINFOS[0];
                            }
                        }
                        obj.SID = Convert.ToInt32(sid);

                        List<CMS_PATIENT> _PATIENTS = impCMS_PATIENT.GetCMS_PATIENT_BYSID(sid, UserInfo.PARTNERID);
                        if (_PATIENTS != null)
                        {
                            if (_PATIENTS.Count > 0)
                            {
                                obj.PATIENT = _PATIENTS[0];
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                logError.Info("medicalExaminationPrescription:" + ex.ToString());
            }

            // Info.  

            // Info.  
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(obj)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A5
                },
            };


            // return View(obj);
        }


        //public ActionResult medicalExaminationPrescriptionDoctor(string sid, string pid, string Token)
        //{
        //    // Initialization.  
        //    AddPageHeader("In đơn thuốc", "");
        //    AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatientWaitingForMedical");
        //    AddBreadcrumb("In đơn thuốc theo bác sỹ", "/Clinic/medicalExaminationPrescriptionDoctor");
        //    var UserInfo = ((cms_Account)Session["UserInfo"]);
        //    CMS_Core.Models.MedicalExaminationViewModel obj = new CMS_Core.Models.MedicalExaminationViewModel();

        //    try
        //    {


        //        if (!string.IsNullOrWhiteSpace(sid) && !string.IsNullOrWhiteSpace(pid) && !string.IsNullOrWhiteSpace(Token))
        //        {
        //            if (CMSLIS.Common.Common.CheckKeyPrivate(sid, pid, Token))
        //            {
        //                obj.CMS_PATIENT_PRESCRIPTIONS = impCMS_PATIENT_PRESCRIPTION.GetCMS_PATIENT_PRESCRIPTION_BYSID_ANDDORTOR(Convert.ToInt64(sid), UserInfo.accountid, UserInfo.PARTNERID);
        //                List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);
        //                if (_PATIENTINFOS != null)
        //                {
        //                    if (_PATIENTINFOS.Count > 0)
        //                    {
        //                        obj.PATIENTINFO = _PATIENTINFOS[0];
        //                    }
        //                }
        //                obj.SID = Convert.ToInt32(sid);

        //                List<CMS_PATIENT> _PATIENTS = impCMS_PATIENT.GetCMS_PATIENT_BYSID(sid, UserInfo.PARTNERID);
        //                if (_PATIENTS != null)
        //                {
        //                    if (_PATIENTS.Count > 0)
        //                    {
        //                        obj.PATIENT = _PATIENTS[0];
        //                    }
        //                }
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        logError.Info("medicalExaminationPrescription:" + ex.ToString());
        //    }

        //    // Info.  

        //    // Info.  
        //    // return View(obj);
        //    string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
        //    return new Rotativa.MVC.PartialViewAsPdf(obj)
        //    {
        //        RotativaOptions = new Rotativa.Core.DriverOptions()
        //        {
        //            CustomSwitches = footer,
        //            PageSize = Rotativa.Core.Options.Size.A5
        //        },
        //    };


        //    // return View(obj);
        //}


        public ActionResult medicalExaminationPrescriptionDoctor(string sid, string pid, string accountid, string Token)
        {
            // Initialization.  
            AddPageHeader("In đơn thuốc", "");
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatientWaitingForMedical");
            AddBreadcrumb("In đơn thuốc theo bác sỹ", "/Clinic/medicalExaminationPrescriptionDoctor");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            CMS_Core.Models.MedicalExaminationViewModel obj = new CMS_Core.Models.MedicalExaminationViewModel();

            try
            {


                if (!string.IsNullOrWhiteSpace(sid) && !string.IsNullOrWhiteSpace(pid) && !string.IsNullOrWhiteSpace(Token))
                {
                    if (CMSLIS.Common.Common.CheckKeyPrivate(sid, pid, Token))
                    {
                        obj.CMS_PATIENT_PRESCRIPTIONS = impCMS_PATIENT_PRESCRIPTION.GetCMS_PATIENT_PRESCRIPTION_BYSID_ANDDORTOR(Convert.ToInt64(sid), Convert.ToInt32(accountid), UserInfo.PARTNERID);
                        List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);
                        if (_PATIENTINFOS != null)
                        {
                            if (_PATIENTINFOS.Count > 0)
                            {
                                obj.PATIENTINFO = _PATIENTINFOS[0];
                            }
                        }
                        obj.SID = Convert.ToInt32(sid);

                        List<CMS_PATIENT> _PATIENTS = impCMS_PATIENT.GetCMS_PATIENT_BYSID(sid, UserInfo.PARTNERID);
                        if (_PATIENTS != null)
                        {
                            if (_PATIENTS.Count > 0)
                            {
                                obj.PATIENT = _PATIENTS[0];
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                logError.Info("medicalExaminationPrescription:" + ex.ToString());
            }

            // Info.  

            // Info.  
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(obj)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A5
                },
            };


            // return View(obj);
        }


        public ActionResult medicalExaminationResult(string sid, string pid, string Token)
        {
            // Initialization.  
            AddPageHeader("In kết quả", "");
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatientWaitingForMedical");
            AddBreadcrumb("In kết quả", "/Clinic/medicalExaminationResult");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            CMS_Core.Models.MedicalExaminationViewModel obj = new CMS_Core.Models.MedicalExaminationViewModel();

            try
            {


                if (!string.IsNullOrWhiteSpace(sid) && !string.IsNullOrWhiteSpace(pid) && !string.IsNullOrWhiteSpace(Token))
                {
                    if (CMSLIS.Common.Common.CheckKeyPrivate(sid, pid, Token))
                    {
                        obj.CMS_PATIENT_SERVICE_RESULTS = impCMS_PATIENT_SERVICE_RESULT.GetCMS_PATIENT_SERVICE_BYSID(Convert.ToInt64(sid), UserInfo.PARTNERID);
                        List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);
                        if (_PATIENTINFOS != null)
                        {
                            if (_PATIENTINFOS.Count > 0)
                            {
                                obj.PATIENTINFO = _PATIENTINFOS[0];
                            }
                        }
                        obj.SID = Convert.ToInt32(sid);
                    }
                }


            }
            catch (Exception ex)
            {
                logError.Info("medicalExaminationPrescription:" + ex.ToString());
            }

            // Info.  

            // Info.  
            //  return View(obj);
            // string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(obj)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    // CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A4
                },
            };


            // return View(obj);
        }


        public ActionResult medicalExaminationSingleResult(string sid, string pid, string id, string Token)
        {
            // Initialization.  
            AddPageHeader("In kết quả", "");
            AddBreadcrumb("Danh sách bệnh nhân", "/Clinic/ListPatientWaitingForMedical");
            AddBreadcrumb("In kết quả", "/Clinic/medicalExaminationSingleResult");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            CMS_Core.Models.MedicalExaminationViewModel obj = new CMS_Core.Models.MedicalExaminationViewModel();

            try
            {


                if (!string.IsNullOrWhiteSpace(sid) && !string.IsNullOrWhiteSpace(pid) && !string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(Token))
                {
                    if (CMSLIS.Common.Common.CheckKeyPrivate(id, Token))
                    {
                        obj.PATIENT_SERVICE_RESULT = impCMS_PATIENT_SERVICE_RESULT.Get(Convert.ToInt32(id), UserInfo.PARTNERID);
                        List<CMS_PATIENTINFO> _PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);
                        if (_PATIENTINFOS != null)
                        {
                            if (_PATIENTINFOS.Count > 0)
                            {
                                obj.PATIENTINFO = _PATIENTINFOS[0];
                            }
                        }
                        obj.SID = Convert.ToInt32(sid);
                    }
                }


            }
            catch (Exception ex)
            {
                logError.Info("medicalExaminationPrescription:" + ex.ToString());
            }

            // Info.  

            // Info.  
            //  return View(obj);
            // string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(obj)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    // CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A4
                },
            };


            // return View(obj);
        }

        #endregion

        public ActionResult ListNumberPatientWaitingForMedical()
        {
            // Initialization.  

            var UserInfo = ((cms_Account)Session["UserInfo"]);




            AddPageHeader("Danh sách chờ khám ", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách chờ khám ", "/Clinic/ListNumberPatientWaitingForMedical");

            CMS_Core.Models.PatientViewModel patientViewModel = new CMS_Core.Models.PatientViewModel();

            patientViewModel.partnerid = UserInfo.PARTNERID;

            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("LOCATIONIDNEW");
                if (cookie.Value != null)
                {
                    if (!string.IsNullOrEmpty(cookie.Value))
                    {
                        patientViewModel.LOCATIONID = Convert.ToInt32(cookie.Value);
                    }
                    else
                    {
                        patientViewModel.LOCATIONID = 0;
                    }
                }
            }
            catch
            {
                patientViewModel.LOCATIONID = 0;
            }


            try
            {
                CMS_ROOM_CLINIC _ROOM_CLINIC = impCMS_ROOM_CLINIC.Get(patientViewModel.LOCATIONID, UserInfo.PARTNERID);
                if (_ROOM_CLINIC != null)
                {
                    patientViewModel.channel = _ROOM_CLINIC.SERVICETYPE;
                }
                List<CMS_PATIENTINFO_QUEUE> CMS_PATIENTINFO_QUEUES = impCMS_PATIENTINFO_QUEUE.GetAll(Convert.ToInt32(patientViewModel.LOCATIONID), UserInfo.PARTNERID);
                ViewBag.CMS_PATIENTINFO_QUEUES = CMS_PATIENTINFO_QUEUES;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListNumberPatientWaitingForMedical:" + ex.ToString());
            }

            // Info.  
            return View(patientViewModel);
        }

        [HttpPost]
        public ActionResult ListNumberPatientWaitingForMedical(CMS_Core.Models.PatientViewModel obj, string submit)
        {
            // Initialization.  
            try
            {
                HttpCookie cookieq = HttpContext.Request.Cookies.Get("LOCATIONIDNEW");


                if (cookieq.Value != null)
                {
                    if (!string.IsNullOrEmpty(cookieq.Value))
                    {
                        if (obj.LOCATIONID != Convert.ToInt32(cookieq.Value))
                        {
                            cookieq.Value = obj.LOCATIONID.ToString();
                            cookieq.Expires = DateTime.Now.AddDays(100);
                            Response.Cookies.Add(cookieq);
                        }
                    }
                    else
                    {
                        cookieq.Value = obj.LOCATIONID.ToString();
                        cookieq.Expires = DateTime.Now.AddDays(100);
                        Response.Cookies.Add(cookieq);
                    }
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("LOCATIONIDNEW");
                    cookie.Value = obj.LOCATIONID.ToString();
                    cookie.Expires = DateTime.Now.AddDays(100);
                    Response.Cookies.Add(cookie);
                }

            }
            catch
            {
                HttpCookie cookie = new HttpCookie("LOCATIONIDNEW", obj.LOCATIONID.ToString());
                cookie.Expires = DateTime.Now.AddDays(100);
                Response.Cookies.Add(cookie);

            }


            var UserInfo = ((cms_Account)Session["UserInfo"]);

            AddPageHeader("Danh sách chờ khám ", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách chờ khám ", "/Clinic/ListNumberPatientWaitingForMedical");



            try
            {

                switch (submit)
                {
                    case "SearchListPatientWaitingForMedical":
                        CMS_ROOM_CLINIC _ROOM_CLINIC = impCMS_ROOM_CLINIC.Get(obj.LOCATIONID, UserInfo.PARTNERID);
                        if (_ROOM_CLINIC != null)
                        {
                            obj.channel = _ROOM_CLINIC.SERVICETYPE;
                        }

                        List<CMS_PATIENTINFO_QUEUE> CMS_PATIENTINFO_QUEUES = impCMS_PATIENTINFO_QUEUE.GetAll(Convert.ToInt32(obj.LOCATIONID), UserInfo.PARTNERID);
                        ViewBag.CMS_PATIENTINFO_QUEUES = CMS_PATIENTINFO_QUEUES;
                        break;
                }

            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientWaitingForMedical: " + ex.ToString());

            }

            // Info.  
            return View(obj);
        }

        #region --> Lịch sử khám bệnh
        public ActionResult PatientHistory(string pid, string Token)
        {
            // Initialization.  

            var UserInfo = ((cms_Account)Session["UserInfo"]);




            AddPageHeader("Lịch sử khám bệnh", "");
            AddBreadcrumb("Lịch sử khám bệnh", "/Clinic/PatientHistory");
            AddBreadcrumb("Lịch sử khám bệnh", "/Clinic/PatientHistory");

            CMS_Core.Models.PatientHistoryViewModel obj = new CMS_Core.Models.PatientHistoryViewModel();





            try
            {
                if (!string.IsNullOrEmpty(pid))
                {

                    if (CMSLIS.Common.Common.CheckKeyPrivate(pid, Token))
                    {

                        List<CMS_PATIENTINFO> CMS_PATIENTINFOS = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_BYID(pid, UserInfo.PARTNERID);
                        if (CMS_PATIENTINFOS != null)
                        {
                            if (CMS_PATIENTINFOS.Count > 0)
                            {
                                obj.PATIENTINFO = CMS_PATIENTINFOS[0];
                                obj.PATIENTS = impCMS_PATIENT.GetCMS_PATIENTByPID(pid, UserInfo.PARTNERID);
                                obj.PATIENTS_PRESCRIPTION = impCMS_PATIENT.GetCMS_PATIENTByPID_PRESCRIPTION(pid, UserInfo.PARTNERID);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân, mời bạn kiểm tra lại!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin bệnh nhân, mời bạn kiểm tra lại!";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }

                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thông tin nhập vào không chính xác, Mời bạn kiểm tra lại!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else
                {

                    ViewBag.TitleSuccsess = "Mời bạn nhập PID bệnh nhân!";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;

                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListNumberPatientWaitingForMedical:" + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        #endregion


        #region --> Hoàn tiền dịch vụ



        public ActionResult REFUNDService()
        {
            // Initialization.  
            AddPageHeader("Danh sách hoàn tiền dịch vụ", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách hoàn tiền dịch vụ", "/clinic/REFUNDService");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                this.ViewBag.GetTypeStatus = CMSLIS.Common.Common.GetTypeStatus();
                List<CMS_PATIENT_SERVICE_REFUND> _PATIENT_SERVICE_REFUNDS = new List<CMS_PATIENT_SERVICE_REFUND>();
                _PATIENT_SERVICE_REFUNDS = impCMS_PATIENT_SERVICE_REFUND.GetCMS_PATIENT_SERVICE_REFUNDByCODE(DateTime.Now.AddDays(-10), DateTime.Now.AddDays(1), string.Empty, UserInfo.PARTNERID);

                ViewBag.PATIENT_SERVICE_REFUNDS = _PATIENT_SERVICE_REFUNDS;
            }
            catch (Exception ex)
            {
                ViewBag.TextErr = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListMedicine: " + ex.ToString());
            }
            pharmacyStoreViewModels obj = new pharmacyStoreViewModels();
            if (UserInfo.PARTNERID == 1)
            {
                obj.PARTNERID = 0;
            }
            else
            {
                obj.PARTNERID = UserInfo.PARTNERID;
            }
            obj.startdate = DateTime.Now.AddDays(-10).ToString("dd/MM/yyyy");
            obj.enddate = DateTime.Now.ToString("dd/MM/yyyy");
            obj.pharmacyStoreCode = string.Empty;
            obj.Status = CMSLIS.Common.Constant.TypeStatusAll;
            // Info.  
            return View(obj);

        }

        [HttpPost]
        public ActionResult REFUNDService(pharmacyStoreViewModels obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách hoàn tiền dịch vụ", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách hoàn tiền dịch vụ", "/clinic/REFUNDService");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            this.ViewBag.GetTypeStatus = CMSLIS.Common.Common.GetTypeStatus();

            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();

            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(obj.startdate.Trim()))
                    Tungay = DateTime.ParseExact(obj.startdate.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(obj.enddate.Trim()))
                    Denngay = DateTime.ParseExact(obj.enddate.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

                if (Tungay > Denngay)
                {
                    TextErr = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày";
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                }
            }
            catch
            {
                TextErr = "Dữ liệu ngày tháng không đúng định dạng";
            }
            #endregion

            if (string.IsNullOrEmpty(TextErr))
            {
                try
                {
                    switch (submit)
                    {
                        case "SearchREFUNDService":

                            this.ViewBag.GetTypeStatus = CMSLIS.Common.Common.GetTypeStatus();
                            List<CMS_PATIENT_SERVICE_REFUND> _PATIENT_SERVICE_REFUNDS = new List<CMS_PATIENT_SERVICE_REFUND>();
                            _PATIENT_SERVICE_REFUNDS = impCMS_PATIENT_SERVICE_REFUND.GetCMS_PATIENT_SERVICE_REFUNDByCODE(DateTime.Now.AddDays(-10), DateTime.Now.AddDays(1), string.Empty, UserInfo.PARTNERID);



                            ViewBag.PATIENT_SERVICE_REFUNDS = _PATIENT_SERVICE_REFUNDS;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    logError.Info("ReceiptStore: " + ex.ToString());
                }
            }
            else
            {
                ViewBag.TitleSuccsess = TextErr;
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }
            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult REFUNDServiceDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách hoàn thuốc", "/clinic/REFUNDServiceDelete");
            string listID = string.Empty;
            string listIDError = string.Empty;
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        string result = impCMS_PATIENT_SERVICE_REFUND.Delete(Convert.ToInt32(customerID), UserInfo.PARTNERID);
                        if (!string.IsNullOrEmpty(result))
                        {
                            if (result.Equals(CMS_Core.Common.Constant.typeORAForeign.ToString()))
                            {
                                listIDError += customerID + ",";
                            }
                            else
                            {
                                AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                                listID += customerID + ",";
                            }
                        }
                        else
                        {
                            AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                            listID += customerID + ",";
                        }


                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }
                    if (listIDError.IndexOf(",") != -1)
                    {
                        listIDError = listIDError.Substring(0, listIDError.Length - 1);
                    }


                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("REFUNDStoreDelete: " + ex.ToString());
                return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
            }

            if (listIDError.Length > 0)
            {
                return Json("Xóa thành công bản ghi có id là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do thuốc đã được chọn rồi!");
            }
            else
            {
                return Json("Xóa thành công bản ghi có id là: " + listID);
            }


            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult REFUNDServicePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách hoàn thuốc", "/clinic/REFUNDServicePublic");
            string listID = string.Empty;
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impCMS_PATIENT_SERVICE_REFUND.Publish(Convert.ToInt32(customerID), UserInfo.PARTNERID);
                        AddLogAction(customerID, Constant.ActionPublicOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để duyệt");
                }
            }
            catch (Exception ex)
            {
                logError.Info("ReceiptStorePublic: " + ex.ToString());
                return Json("Có lỗi trong quá trình duyệt");
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }



        public ActionResult REFUNDServiceAdd(string sid, string id)
        {
            // Initialization.  
            AddPageHeader("Hoàn tiền dịch vụ", "");
            AddBreadcrumb("Hoàn tiền dịch vụ", "");
            AddBreadcrumb("Hoàn tiền dịch vụ", "/Clinic/REFUNDServiceAdd");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            CMSNEW.Models.ClinicViewModels clinicViewModels = new CMSNEW.Models.ClinicViewModels();
            clinicViewModels.PATIENTINFO = new CMS_PATIENTINFO();
            clinicViewModels.PATIENT = new CMS_PATIENT();
            clinicViewModels.CMS_PATIENT_SERVICES = new List<CMS_PATIENT_SERVICE>();
            this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordWaitingForMedical();

            try
            {

                if (string.IsNullOrEmpty(sid))
                {
                    sid = "0";
                }

                if (string.IsNullOrEmpty(id))
                {
                    id = "0";
                }
                if (!"0".Equals(id))
                {
                    CMS_PATIENT_SERVICE_REFUND _PATIENT_SERVICE_REFUND = new CMS_PATIENT_SERVICE_REFUND();
                    _PATIENT_SERVICE_REFUND = impCMS_PATIENT_SERVICE_REFUND.Get(Convert.ToInt32(id), UserInfo.PARTNERID);
                    if (_PATIENT_SERVICE_REFUND != null)
                    {
                        if (_PATIENT_SERVICE_REFUND.SID != 0)
                        {
                            CMS_Core.Models.PatientAddViewModel obj = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_CreateOrderBYSID(_PATIENT_SERVICE_REFUND.SID, UserInfo.PARTNERID);
                            if (obj != null)
                            {
                                if (obj.PATIENTINFO != null)
                                {
                                    if (obj.PATIENTINFO.PATIENTNAME.Length > 0)
                                    {
                                        clinicViewModels.PATIENTINFO = obj.PATIENTINFO;
                                        clinicViewModels.PATIENT = obj.PATIENT;
                                        clinicViewModels.CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetCMS_PATIENT_SERVICE_BYSID(Convert.ToInt64(_PATIENT_SERVICE_REFUND.SID), UserInfo.PARTNERID);
                                        List<CMS_PATIENT_SERVICE_REFUND> CMS_PATIENT_SERVICE_REFUNDS = impCMS_PATIENT_SERVICE_REFUND.GetBYRefundBySID(Convert.ToInt32(_PATIENT_SERVICE_REFUND.SID), UserInfo.PARTNERID);

                                        if (clinicViewModels.CMS_PATIENT_SERVICES != null)
                                        {
                                            foreach (var data in clinicViewModels.CMS_PATIENT_SERVICES)
                                            {
                                                if (CMS_PATIENT_SERVICE_REFUNDS != null)
                                                {
                                                    foreach (var value in CMS_PATIENT_SERVICE_REFUNDS)
                                                    {
                                                        if (value.SERVICE_ID == data.SERVICE_ID)
                                                        {
                                                            data.isRefund = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }


                                        clinicViewModels.keyword = _PATIENT_SERVICE_REFUND.SID.ToString();
                                    }
                                    else
                                    {
                                        ViewBag.TitleSuccsess = "Không tồn tại ID khám bệnh nhân, Mời bạn nhậplại!";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    }
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Không tồn tại ID khám bệnh nhân, Mời bạn nhậplại!";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tồn tại ID khám bệnh nhân, Mời bạn nhậplại!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }

                        }
                    }
                }
                else if (sid.Length > 1)
                {
                    bool checkExit = false;
                    List<CMS_PATIENT_SERVICE_REFUND> CMS_PATIENT_SERVICE_REFUNDS = impCMS_PATIENT_SERVICE_REFUND.GetBYRefundBySID(Convert.ToInt32(sid), UserInfo.PARTNERID);

                    if (CMS_PATIENT_SERVICE_REFUNDS != null)
                    {
                        if (CMS_PATIENT_SERVICE_REFUNDS.Count != 0)
                        {
                            checkExit = true;
                        }
                    }
                    if (!checkExit)
                    {

                        CMS_Core.Models.PatientAddViewModel obj = impCMS_PATIENTINFO.GetCMS_PATIENTINFO_CreateOrderBYSID(Convert.ToInt64(sid), UserInfo.PARTNERID);
                        if (obj != null)
                        {
                            if (obj.PATIENTINFO != null)
                            {
                                if (obj.PATIENTINFO.PATIENTNAME.Length > 0)
                                {
                                    clinicViewModels.PATIENTINFO = obj.PATIENTINFO;
                                    clinicViewModels.PATIENT = obj.PATIENT;
                                    clinicViewModels.CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetCMS_PATIENT_SERVICE_BYSID(Convert.ToInt64(sid), UserInfo.PARTNERID);
                                    clinicViewModels.keyword = sid;
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Không tồn tại ID khám bệnh nhân, Mời bạn nhậplại!";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tồn tại ID khám bệnh nhân, Mời bạn nhậplại!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tồn tại ID khám bệnh nhân, Mời bạn nhậplại!";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Dịch vụ đã hoàn trẩ rồi. Mời bạn vào danh sách dịch vụ hoàn trả để kiểm tra!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else
                {
                    clinicViewModels.PATIENTINFO.SEX = 0;
                }

            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ExportStore: " + ex.ToString());
            }

            // Info.  
            return View(clinicViewModels);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult REFUNDServiceAdd(CMSNEW.Models.ClinicViewModels obj, string submit)
        {
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {

                AddPageHeader("Hoàn tiền dịch vụ", "");
                AddBreadcrumb("Hoàn tiền dịch vụ", "");
                AddBreadcrumb("Hoàn tiền dịch vụ", "/Clinic/REFUNDServiceAdd");

                switch (submit)
                {
                    case "SaveREFUNDService":

                        if (obj.CMS_PATIENT_SERVICES != null)
                        {
                            foreach (var data in obj.CMS_PATIENT_SERVICES)
                            {
                                string SERVICE_REFUND_ID = string.Empty;

                                if (data.isRefund == true)
                                {
                                    CMS_PATIENT_SERVICE_REFUND _PATIENT_SERVICE_REFUND = new CMS_PATIENT_SERVICE_REFUND();


                                    _PATIENT_SERVICE_REFUND.PARTNERID = UserInfo.PARTNERID;
                                    _PATIENT_SERVICE_REFUND.PID = obj.PATIENTINFO.PID ?? 0;
                                    _PATIENT_SERVICE_REFUND.SID = obj.PATIENT.SID;

                                    _PATIENT_SERVICE_REFUND.SERVICE_ID = data.SERVICE_ID;
                                    _PATIENT_SERVICE_REFUND.SERVICE_NAME = data.SERVICE_NAME;
                                    _PATIENT_SERVICE_REFUND.SERVICE_PRICE = data.SERVICE_PRICE;
                                    _PATIENT_SERVICE_REFUND.SERVICE_NOTE = data.SERVICE_NOTE;
                                    _PATIENT_SERVICE_REFUND.CREATEBY = UserInfo.accountid;
                                    _PATIENT_SERVICE_REFUND.CREATE_DATE = DateTime.Now;

                                    _PATIENT_SERVICE_REFUND.SERVICETYPE = data.SERVICETYPE;

                                    var errorsSave1 = _PATIENT_SERVICE_REFUND.Validate(new ValidationContext(_PATIENT_SERVICE_REFUND));

                                    if (errorsSave1.ToList().Count == 0)
                                    {
                                        SERVICE_REFUND_ID = impCMS_PATIENT_SERVICE_REFUND.Add(_PATIENT_SERVICE_REFUND, UserInfo.PARTNERID);
                                        if (!string.IsNullOrEmpty(SERVICE_REFUND_ID))
                                        {
                                            ViewBag.TitleSuccsess = "Hoàn tiền dịch vụ thành công!";
                                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                            Response.Redirect("/Clinic/REFUNDService", false);
                                        }
                                        else
                                        {
                                            ViewBag.TitleSuccsess = "Hoàn tiền dịch vụ không thành công!";
                                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                        }

                                    }
                                    else
                                    {
                                        ViewBag.TitleSuccsess = errorsSave1.ToList()[0].ErrorMessage;
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi trong quá trình cập nhật";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListMedicineAdd: " + ex.ToString());
            }



            return View(obj);
        }

        public ActionResult REFUNDServiceViewPrint(string sid, string pid, string Token)
        {
            // Initialization.             

            AddPageHeader("In phiếu hoàn tiền", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("In phiếu hoàn tiền", "/Clinic/REFUNDServiceViewPrint");


            CMS_Core.Models.PatientPrintViewModel obj = new CMS_Core.Models.PatientPrintViewModel();


            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (!string.IsNullOrEmpty(sid) && !string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(Token))
                {
                    if (CMSLIS.Common.Common.CheckKeyPrivate(sid, pid, Token))
                    {
                        List<CMS_PATIENTINFO_TOTAL> _PATIENTINFO_TOTALS = impCMS_PATIENT.GetWaitingForMedical_BYSID(Convert.ToInt32(sid), UserInfo.PARTNERID);

                        if (_PATIENTINFO_TOTALS != null)
                        {
                            if (_PATIENTINFO_TOTALS.Count > 0)
                            {
                                obj.PATIENTINFO = _PATIENTINFO_TOTALS[0];


                                obj.CMS_PATIENT_SERVICE_REFUNDS = impCMS_PATIENT_SERVICE_REFUND.GetBYRefundBySID(Convert.ToInt32(sid), UserInfo.PARTNERID);

                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu hoàn tiền";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }

                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu hoàn tiền";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thông tin phiếu không chính xác, Mời bạn kiểm tra lại!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu hoàn tiền";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }

            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ExportStoreAdd: " + ex.ToString());
            }

            // Info.  
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(obj)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A5
                },
            };


        }


        #endregion


        #region --> Danh sách khách hàng bất thường 
        public ActionResult ListPatientsWeirdo()
        {
            // Initialization.  
            AddPageHeader("Danh sách bệnh nhân bất thường", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách bệnh nhân bất thường", "/Clinic/ListPatientsWeirdo");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            CMS_Core.Models.PatientViewModel patientViewModel = new CMS_Core.Models.PatientViewModel();
            patientViewModel.tungay = DateTime.Now.AddDays(-15).ToString("dd/MM/yyyy");
            patientViewModel.denngay = DateTime.Now.ToString("dd/MM/yyyy");
            patientViewModel.keyword = string.Empty;
            patientViewModel.TypeKeyword = 0;
            patientViewModel.partnerid = UserInfo.PARTNERID;
            patientViewModel.Weirdo = 0;

            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordWeirdo();
                List<CMS_PATIENTINFO> CMS_PATIENTINFOS = _PATIENTINFO_TOTAL.GetCMS_PATIENTINFO_TOTALByWeirdo(DateTime.Now.AddDays(-15), DateTime.Now.AddDays(1), string.Empty, 1, 0, 0, UserInfo.PARTNERID);
                ViewBag.CMS_PATIENTINFOS = CMS_PATIENTINFOS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatients: " + ex.ToString());
            }

            // Info.  
            return View(patientViewModel);
        }

        [HttpPost]
        public ActionResult ListPatientsWeirdo(CMS_Core.Models.PatientViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách bệnh nhân bất thường", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách bệnh nhân bất thường", "/Clinic/ListPatientsWeirdo");
            var UserInfo = ((cms_Account)Session["UserInfo"]);



            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();

            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(obj.tungay.Trim()))
                    Tungay = DateTime.ParseExact(obj.tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(obj.denngay.Trim()))
                    Denngay = DateTime.ParseExact(obj.denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

                if (Tungay > Denngay)
                {
                    TextErr = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày";
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                }
            }
            catch
            {
                TextErr = "Dữ liệu ngày tháng không đúng định dạng";
            }
            #endregion


            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordWeirdo();
                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchListPatientsWeirdo":
                            if (string.IsNullOrEmpty(obj.keyword))
                            {
                                obj.keyword = string.Empty;
                            }
                            List<CMS_PATIENTINFO> CMS_PATIENTINFOS = _PATIENTINFO_TOTAL.GetCMS_PATIENTINFO_TOTALByWeirdo(Tungay, Denngay.AddDays(1), obj.keyword, obj.TypeKeyword, obj.Weirdo, obj.SERVICETYPE, UserInfo.PARTNERID);
                            ViewBag.CMS_PATIENTINFOS = CMS_PATIENTINFOS;
                            break;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = TextErr;
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatients: " + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListPatientsWeirdoDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Danh sách thuốc", "/Clinic/ListPatientsWeirdoDelete");
            string listID = string.Empty;
            string listIDError = string.Empty;
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {

                        string result = impCMS_PATIENTINFO.DeleteCMS_PATIENTINFO_Weirdo(Convert.ToInt32(customerID), UserInfo.PARTNERID);
                        if (!string.IsNullOrEmpty(result))
                        {
                            if (result.Equals(CMS_Core.Common.Constant.typeORAForeign.ToString()))
                            {
                                listIDError += customerID + ",";
                            }
                            else
                            {
                                AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                                listID += customerID + ",";
                            }
                        }
                        else
                        {
                            AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                            listID += customerID + ",";
                        }


                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }
                    if (listIDError.IndexOf(",") != -1)
                    {
                        listIDError = listIDError.Substring(0, listIDError.Length - 1);
                    }


                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListPatientsWeirdoDelete: " + ex.ToString());
                return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
            }

            if (listIDError.Length > 0)
            {
                return Json("Xóa thành công bản ghi có id là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do thuốc đã được chọn rồi!");
            }
            else
            {
                return Json("Xóa thành công bản ghi có id là: " + listID);
            }


            // Info.  
            //return View();
        }


        #endregion

        #region --> Danh sách khách hàng bảo hiểm
        public ActionResult ListPatientsInsurrance()
        {
            // Initialization.  
            AddPageHeader("Danh sách khách hàng bảo hiểm", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách khách hàng bảo hiểm", "/Clinic/ListPatientsInsurrance");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            CMS_Core.Models.PatientViewModel patientViewModel = new CMS_Core.Models.PatientViewModel();
            patientViewModel.tungay = DateTime.Now.AddDays(-15).ToString("dd/MM/yyyy");
            patientViewModel.denngay = DateTime.Now.ToString("dd/MM/yyyy");
            patientViewModel.keyword = string.Empty;
            patientViewModel.TypeKeyword = 0;
            patientViewModel.partnerid = UserInfo.PARTNERID;
            patientViewModel.LOCATIONID = 323;

            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordWaitingForMedical();
                List<CMS_PATIENTINFO_TOTAL> CMS_PATIENTINFO_TOTALS = _PATIENTINFO_TOTAL.GetCMS_PATIENTINFO_TOTALByInsurrance(DateTime.Now.AddDays(-15), DateTime.Now.AddDays(1), string.Empty, 1, 0, UserInfo.PARTNERID);
                ViewBag.CMS_PATIENTINFO_TOTALS = CMS_PATIENTINFO_TOTALS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatients: " + ex.ToString());
            }

            // Info.  
            return View(patientViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListPatientsInsurrance(CMS_Core.Models.PatientViewModel obj, string submit)
        {


            AddPageHeader("Danh sách khách hàng bảo hiểm", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("Danh sách khách hàng bảo hiểm", "/Clinic/ListPatientsInsurrance");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();

            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(obj.tungay.Trim()))
                    Tungay = DateTime.ParseExact(obj.tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(obj.denngay.Trim()))
                    Denngay = DateTime.ParseExact(obj.denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

                if (Tungay > Denngay)
                {
                    TextErr = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày";
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                }
            }
            catch
            {
                TextErr = "Dữ liệu ngày tháng không đúng định dạng";
            }
            #endregion


            try
            {
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordWaitingForMedical();
                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchListPatientsInsurrance":
                            this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordWaitingForMedical();
                            List<CMS_PATIENTINFO_TOTAL> CMS_PATIENTINFO_TOTALS = _PATIENTINFO_TOTAL.GetCMS_PATIENTINFO_TOTALByInsurrance(Tungay, Denngay.AddDays(1), obj.keyword, obj.TypeKeyword, 0, UserInfo.PARTNERID);
                            ViewBag.CMS_PATIENTINFO_TOTALS = CMS_PATIENTINFO_TOTALS;
                            break;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = TextErr;
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientsInsurrance: " + ex.ToString());

            }

            // Info.  
            return View(obj);
        }

        public ActionResult ListPatientInsurrancePrint(string sid, string pid, string Token)
        {
            // Initialization.             

            AddPageHeader("In phiếu in", "");
            AddBreadcrumb("Khám chữa bệnh", "/Clinic/ListPatients");
            AddBreadcrumb("In phiếu in", "/Clinic/ListPatientInsurrancePrint");


            CMS_Core.Models.PatientPrintViewModel obj = new CMS_Core.Models.PatientPrintViewModel();


            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (!string.IsNullOrEmpty(sid) && !string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(Token))
                {
                    if (CMSLIS.Common.Common.CheckKeyPrivate(sid, pid, Token))
                    {
                        List<CMS_PATIENTINFO_TOTAL> _PATIENTINFO_TOTALS = impCMS_PATIENT.GetWaitingForMedical_BYSID(Convert.ToInt32(sid), UserInfo.PARTNERID);

                        if (_PATIENTINFO_TOTALS != null)
                        {
                            if (_PATIENTINFO_TOTALS.Count > 0)
                            {
                                obj.PATIENTINFO = _PATIENTINFO_TOTALS[0];


                                obj.CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetCMS_PATIENT_SERVICE_BYSID_NOPAY(Convert.ToInt32(sid), UserInfo.PARTNERID);

                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }

                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thông tin phiếu không chính xác, Mời bạn kiểm tra lại!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin phiếu khám";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }

            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ExportStoreAdd: " + ex.ToString());
            }

            // Info.  
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(obj)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A5
                },
            };


        }


        #endregion
    }
}
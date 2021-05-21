using CMS_Core.Entity;
using CMS_Core.Implementtion;
using CMSLIS.Controllers;
using CMSLIS.Models;
using CMSNEW.Common;
using CMSNEW.Models;
using log4net;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;


namespace CMSNEW.Controllers
{
    [CheckAuthorization]
    public class ReportController : BaseController
    {
        ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");
        private ImpCMS_REPORT_SERVICE impCMS_REPORT_SERVICE;
        private ImpCms_Account impCms_Account;
        private ImpCMS_MEDICINE_RECEIPT_STORE impCMS_MEDICINE_RECEIPT_STORE;
        private ImpCMS_MEDICINE_EXPORT_STORE impCMS_MEDICINE_EXPORT_STORE;
        private ImpCMS_PATIENT_PRESCRIPTION impCMS_PATIENT_PRESCRIPTION;
        private ImpCMS_PATIENT_SERVICE impCMS_PATIENT_SERVICE;
        private ImpCMS_SERVICE_TYPE impCMS_SERVICE_TYPE;

        private Imptbl_logSearch imptbl_logSearch;

        // GET: Report

        public ReportController()
        {
            impCMS_REPORT_SERVICE = new ImpCMS_REPORT_SERVICE();
            impCms_Account = new ImpCms_Account();
            impCMS_MEDICINE_RECEIPT_STORE = new ImpCMS_MEDICINE_RECEIPT_STORE();
            impCMS_MEDICINE_EXPORT_STORE = new ImpCMS_MEDICINE_EXPORT_STORE();
            impCMS_PATIENT_PRESCRIPTION = new ImpCMS_PATIENT_PRESCRIPTION();
            impCMS_PATIENT_SERVICE = new ImpCMS_PATIENT_SERVICE();
            impCMS_SERVICE_TYPE = new ImpCMS_SERVICE_TYPE();
            imptbl_logSearch = new Imptbl_logSearch();
        }

        public ActionResult Index()
        {
            return View();
        }

        #region --> Báo cáo doanh thu    
        public ActionResult salesReport()
        {
            return View();
        }

        #endregion


        #region --> Báo cáo lượt khám bệnh
        public ActionResult patientsReport()
        {
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo số lượng bệnh nhân", "");
            AddBreadcrumb("Báo cáo số lượng bệnh nhân", "/Report/patientsReport");
            ViewBag.Title = "Báo cáo số lượng bệnh nhân";

            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            report.DOCTOR_ID = 0;
            report.startdate = DateTime.Now.ToString("dd/MM/yyyy");
            report.enddate = DateTime.Now.ToString("dd/MM/yyyy");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                DateTime Tungay = new DateTime();
                Tungay = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", new CultureInfo("en-US"));

                List<CMS_MEDICINE_EXPORT_STORE> CMS_MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByDATE(Tungay, DateTime.Now.AddDays(1), UserInfo.PARTNERID);
                ViewBag.CMS_MEDICINE_EXPORT_STORES = CMS_MEDICINE_EXPORT_STORES;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ReportExportMedicine:" + ex.ToString());
            }


            return View(report);
        }

        [AjaxValidateAntiForgeryToken]
        public JsonResult getDashboard(int type, String startdate, String enddate)
        {
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                DateTime Tungay = new DateTime();
                DateTime Denngay = new DateTime();
                string TextErr = string.Empty;
                #region Check input data
                try
                {
                    if (!string.IsNullOrEmpty(startdate.Trim()))
                        Tungay = DateTime.ParseExact(startdate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));
                    if (!string.IsNullOrEmpty(enddate.Trim()))
                        Denngay = DateTime.ParseExact(enddate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));

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

                IEnumerable<Dashboard_Index> modelList = new List<Dashboard_Index>();
                ListDashboard_Index listDashboard = null;


                ImpDashboard_Index impDashboard_Index = new ImpDashboard_Index();
                listDashboard = impDashboard_Index.GetDashboard_ByDate(Tungay, Denngay.AddDays(1), UserInfo.PARTNERID);


                if (type == 0)
                {
                    modelList = listDashboard.Revenue;
                }
                if (type == 1)
                {
                    modelList = listDashboard.PatienInfos;
                }
                if (type == 2)
                {
                    modelList = listDashboard.RevenueMedicine;
                }
                if (type == 3)
                {
                    modelList = listDashboard.Prescriptions;
                }

                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("getDashboard:" + ex.ToString());
                return Json(string.Empty);
            }
        }


        #endregion


        #region --> Báo cáo dịch vụ  
        public ActionResult serviceReport()
        {
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo dịch vụ", "");
            AddBreadcrumb("Báo cáo dịch vụ", "/Report/serviceReport");
            ViewBag.Title = "Báo cáo dịch vụ";
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();

            report.startdate = DateTime.Now.ToString("dd/MM/yyyy");
            report.enddate = DateTime.Now.ToString("dd/MM/yyyy");


            try
            {
                DateTime Tungay = new DateTime();
                Tungay = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", new CultureInfo("en-US"));

                List<CMS_REPORT_SERVICE> CMS_REPORT_SERVICES = impCMS_REPORT_SERVICE.GetREPORT_SERVICE(Tungay, DateTime.Now.AddDays(1), report.SERVICETYPE, UserInfo.PARTNERID);
                ViewBag.CMS_REPORT_SERVICES = CMS_REPORT_SERVICES;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientWaitingForMedical:" + ex.ToString());
            }

            // Info.  
            return View(report);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult serviceReport(CMSNEW.Models.ReportViewModels obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo dịch vụ", "");
            AddBreadcrumb("Báo cáo dịch vụ", "/Report/serviceReport");
            ViewBag.Title = "Báo cáo dịch vụ";
            List<CMS_REPORT_SERVICE> CMS_REPORT_SERVICES = null;
            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
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


            try
            {

                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchserviceReport":
                            CMS_REPORT_SERVICES = impCMS_REPORT_SERVICE.GetREPORT_SERVICE(Tungay, Denngay.AddDays(1), obj.SERVICETYPE, UserInfo.PARTNERID);
                            ViewBag.CMS_REPORT_SERVICES = CMS_REPORT_SERVICES;
                            if (CMS_REPORT_SERVICES != null)
                            {
                                Session["CMS_REPORT_SERVICES"] = CMS_REPORT_SERVICES;
                            }
                            break;

                        case "ExportserviceReport":

                            if (Session["CMS_REPORT_SERVICES"] != null)
                            {
                                CMS_REPORT_SERVICES = (List<CMS_REPORT_SERVICE>)Session["CMS_REPORT_SERVICES"];
                            }
                            else
                            {
                                CMS_REPORT_SERVICES = impCMS_REPORT_SERVICE.GetREPORT_SERVICE(Tungay, Denngay.AddDays(1), 0, UserInfo.PARTNERID);
                                if (CMS_REPORT_SERVICES != null)
                                {
                                    Session["CMS_REPORT_SERVICES"] = CMS_REPORT_SERVICES;
                                }
                            }

                            if (CMS_REPORT_SERVICES != null)
                            {
                                if (CMS_REPORT_SERVICES.Count > 0)
                                {
                                    string[] columns = { "rownum", "SERVICE_NAME", "Count_Service", "SERVICETYPE_NAME", "Total_Price" };
                                    string timeReport = "Thời gian báo cáo từ: " + obj.startdate + " - " + obj.enddate;


                                    if (UserInfo == null)
                                    {
                                        Response.Redirect("/account/login");
                                    }
                                    string creater = "Người báo cáo: " + UserInfo.Hoten;
                                    byte[] filecontent = ExcelExportHelper.ExportExcel(CMS_REPORT_SERVICES, "Tổng hợp báo cáo dịch vụ", timeReport, creater, true, columns);


                                    return File(filecontent, ExcelExportHelper.ExcelContentType, "ReportService.xlsx");
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Không tìm thấy dữ liệu. Mời bạn chọn lại";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy dữ liệu. Mời bạn chọn lại";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
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


        [HttpGet]
        public FileContentResult serviceExportToExcel(String startdate, String enddate, int DOCTOR_ID, string Token)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo dịch vụ", "");
            AddBreadcrumb("Báo cáo dịch vụ", "/Report/serviceReport");
            ViewBag.Title = "Báo cáo dịch vụ";
            List<CMS_REPORT_SERVICE> CMS_REPORT_SERVICES = null;
            string TextErr = string.Empty;
            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(startdate.Trim()))
                    Tungay = DateTime.ParseExact(startdate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(enddate.Trim()))
                    Denngay = DateTime.ParseExact(enddate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));

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

                List<cms_Account> cms_Accounts = impCms_Account.Getcms_AccountByUserUid(DOCTOR_ID.ToString());
                if (cms_Accounts != null)
                {
                    if (cms_Accounts.Count > 0)
                    {
                        report.DOCTOR_NAME = cms_Accounts[0].Hoten;
                    }
                }
                report.DOCTOR_ID = DOCTOR_ID;
                report.startdate = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                report.enddate = DateTime.Now.ToString("dd/MM/yyyy");
                if (TextErr.Length == 0)
                {
                    if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate) && !string.IsNullOrEmpty(Token))
                    {
                        if (CMSLIS.Common.Common.CheckKeyPrivate(startdate, enddate, Token))
                        {
                            CMS_REPORT_SERVICES = impCMS_REPORT_SERVICE.GetREPORT_SERVICE(Tungay, Denngay.AddDays(1), 0, UserInfo.PARTNERID);

                            if (CMS_REPORT_SERVICES != null)
                            {
                                if (CMS_REPORT_SERVICES.Count > 0)
                                {
                                    string[] columns = { "rownum", "SERVICE_NAME", "Count_Service", "SERVICETYPE_NAME", "Total_Price" };
                                    string timeReport = "Thời gian báo cáo từ: " + startdate + " - " + enddate;


                                    if (UserInfo == null)
                                    {
                                        Response.Redirect("/account/login");
                                    }
                                    string creater = "Người báo cáo: " + UserInfo.Hoten;
                                    byte[] filecontent = ExcelExportHelper.ExportExcel(CMS_REPORT_SERVICES, "Tổng hợp báo cáo dịch vụ", timeReport, creater, true, columns);


                                    return File(filecontent, ExcelExportHelper.ExcelContentType, "ReportService.xlsx");
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Không tìm thấy dữ liệu. Mời bạn chọn lại";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy dữ liệu. Mời bạn chọn lại";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }



                        }
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


            return null;
        }

        public ActionResult serviceReportPrint(String startdate, String enddate, int SERVICETYPE, string Token)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo dịch vụ", "");
            AddBreadcrumb("Báo cáo dịch vụ", "/Report/serviceReport");
            ViewBag.Title = "Báo cáo dịch vụ";
            List<CMS_REPORT_SERVICE> CMS_REPORT_SERVICES = null;
            string TextErr = string.Empty;
            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(startdate.Trim()))
                    Tungay = DateTime.ParseExact(startdate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(enddate.Trim()))
                    Denngay = DateTime.ParseExact(enddate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));

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

                CMS_SERVICE_TYPE _SERVICE_TYPE = impCMS_SERVICE_TYPE.Get(SERVICETYPE, UserInfo.PARTNERID);

                if (_SERVICE_TYPE != null)
                {
                    if (_SERVICE_TYPE.NAME.Length > 0)
                    {
                        report.DOCTOR_NAME = _SERVICE_TYPE.NAME;
                    }
                }
                // report.DOCTOR_ID = DOCTOR_ID;
                report.startdate = Tungay.ToString("dd/MM/yyyy");
                report.enddate = Denngay.ToString("dd/MM/yyyy");
                if (TextErr.Length == 0)
                {
                    if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate) && !string.IsNullOrEmpty(Token))
                    {
                        if (CMSLIS.Common.Common.CheckKeyPrivate(startdate, enddate, Token))
                        {
                            CMS_REPORT_SERVICES = impCMS_REPORT_SERVICE.GetREPORT_SERVICE(Tungay, Denngay.AddDays(1), SERVICETYPE, UserInfo.PARTNERID);
                            ViewBag.CMS_REPORT_SERVICES = CMS_REPORT_SERVICES;
                        }
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
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(report)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A4
                },
            };

        }


        #endregion

        #region --> Báo cáo bán thuốc 
        public ActionResult ReportExportMedicine()
        {
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo bán thuốc", "");
            AddBreadcrumb("Báo cáo bán thuốc", "/Report/ReportExportMedicine");
            ViewBag.Title = "Báo cáo bán thuốc";

            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            report.DOCTOR_ID = 0;
            report.startdate = DateTime.Now.ToString("dd/MM/yyyy");
            report.enddate = DateTime.Now.ToString("dd/MM/yyyy");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                DateTime Tungay = new DateTime();
                Tungay = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", new CultureInfo("en-US"));

                List<CMS_MEDICINE_EXPORT_STORE> CMS_MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByDATE(Tungay, DateTime.Now.AddDays(1), UserInfo.PARTNERID);
                ViewBag.CMS_MEDICINE_EXPORT_STORES = CMS_MEDICINE_EXPORT_STORES;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ReportExportMedicine:" + ex.ToString());
            }

            // Info.  
            return View(report);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportExportMedicine(CMSNEW.Models.ReportViewModels obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo bán thuốc", "");
            AddBreadcrumb("Báo cáo bán thuốc", "/Report/ReportExportMedicine");
            ViewBag.Title = "Báo cáo bán thuốc";
            List<CMS_MEDICINE_EXPORT_STORE> CMS_MEDICINE_EXPORT_STORES;
            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
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


            try
            {

                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchReportExportMedicine":
                            CMS_MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByDATE(Tungay, Denngay.AddDays(1), UserInfo.PARTNERID);
                            ViewBag.CMS_MEDICINE_EXPORT_STORES = CMS_MEDICINE_EXPORT_STORES;
                            if (CMS_MEDICINE_EXPORT_STORES != null)
                            {
                                Session["CMS_REPORT_SERVICES"] = CMS_MEDICINE_EXPORT_STORES;
                            }
                            break;

                            //case "ExportReportExportMedicine":

                            //    if (Session["CMS_REPORT_SERVICES"] != null)
                            //    {
                            //        CMS_MEDICINE_EXPORT_STORES = (List<CMS_MEDICINE_EXPORT_STORE>)Session["CMS_REPORT_SERVICES"];
                            //    }
                            //    else
                            //    {
                            //        CMS_MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByDATE(Tungay, Denngay.AddDays(1), UserInfo.PARTNERID);
                            //        if (CMS_MEDICINE_EXPORT_STORES != null)
                            //        {
                            //            Session["CMS_REPORT_SERVICES"] = CMS_MEDICINE_EXPORT_STORES;
                            //        }
                            //    }

                            //    if (CMS_MEDICINE_EXPORT_STORES != null)
                            //    {
                            //        if (CMS_MEDICINE_EXPORT_STORES.Count > 0)
                            //        {
                            //            string[] columns = { "rownum", "SERVICE_NAME", "Count_Service", "SERVICETYPE_NAME", "Total_Price" };
                            //            string timeReport = "Thời gian báo cáo từ: " + obj.startdate + " - " + obj.enddate;


                            //            if (UserInfo == null)
                            //            {
                            //                Response.Redirect("/account/login");
                            //            }
                            //            string creater = "Người báo cáo: " + UserInfo.Hoten;
                            //            byte[] filecontent = ExcelExportHelper.ExportExcel(CMS_REPORT_SERVICES, "Tổng hợp báo cáo dịch vụ", timeReport, creater, true, columns);


                            //            return File(filecontent, ExcelExportHelper.ExcelContentType, "ReportService.xlsx");
                            //        }
                            //        else
                            //        {
                            //            ViewBag.TitleSuccsess = "Không tìm thấy dữ liệu. Mời bạn chọn lại";
                            //            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        ViewBag.TitleSuccsess = "Không tìm thấy dữ liệu. Mời bạn chọn lại";
                            //        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            //    }
                            //    break;

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


        public ActionResult ReportExportMedicinePrint(String startdate, String enddate, string Token)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo bán thuốc", "");
            AddBreadcrumb("Báo cáo bán thuốc", "/Report/ReportExportMedicine");
            ViewBag.Title = "Báo cáo bán thuốc";
            List<CMS_MEDICINE_EXPORT_STORE> CMS_MEDICINE_EXPORT_STORES;
            string TextErr = string.Empty;
            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(startdate.Trim()))
                    Tungay = DateTime.ParseExact(startdate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(enddate.Trim()))
                    Denngay = DateTime.ParseExact(enddate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));

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
                report.startdate = Tungay.ToString("dd/MM/yyyy");
                report.enddate = Denngay.ToString("dd/MM/yyyy");
                if (TextErr.Length == 0)
                {
                    if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate) && !string.IsNullOrEmpty(Token))
                    {
                        if (CMSLIS.Common.Common.CheckKeyPrivate(startdate, enddate, Token))
                        {
                            CMS_MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByDATE(Tungay, Denngay.AddDays(1), UserInfo.PARTNERID);
                            ViewBag.CMS_MEDICINE_EXPORT_STORES = CMS_MEDICINE_EXPORT_STORES;
                        }
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
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(report)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A4
                },
            };

        }


        #endregion


        #region --> Báo cáo nhập thuốc 
        public ActionResult ReceiptExportMedicine()
        {
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo nhập thuốc", "");
            AddBreadcrumb("Báo cáo nhập thuốc", "/Report/ReportReceiptMedicine");
            ViewBag.Title = "Báo cáo nhập thuốc";

            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            report.DOCTOR_ID = 0;
            report.startdate = DateTime.Now.ToString("dd/MM/yyyy");
            report.enddate = DateTime.Now.ToString("dd/MM/yyyy");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                DateTime Tungay = new DateTime();
                Tungay = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", new CultureInfo("en-US"));

                List<CMS_MEDICINE_RECEIPT_STORE> CMS_MEDICINE_RECEIPT_STORES = impCMS_MEDICINE_RECEIPT_STORE.GetReportCMS_MEDICINE_RECEIPT_STOREByDate(Tungay, DateTime.Now.AddDays(1), UserInfo.PARTNERID);
                ViewBag.CMS_MEDICINE_RECEIPT_STORES = CMS_MEDICINE_RECEIPT_STORES;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ReportReceiptMedicine:" + ex.ToString());
            }

            // Info.  
            return View(report);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiptExportMedicine(CMSNEW.Models.ReportViewModels obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo nhập thuốc", "");
            AddBreadcrumb("Báo cáo nhập thuốc", "/Report/ReportReceiptMedicine");
            ViewBag.Title = "Báo cáo nhập thuốc";
            List<CMS_MEDICINE_RECEIPT_STORE> CMS_MEDICINE_RECEIPT_STORES;
            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
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


            try
            {

                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchReceiptExportMedicine":
                            CMS_MEDICINE_RECEIPT_STORES = impCMS_MEDICINE_RECEIPT_STORE.GetReportCMS_MEDICINE_RECEIPT_STOREByDate(Tungay, Denngay.AddDays(1), UserInfo.PARTNERID);
                            ViewBag.CMS_MEDICINE_RECEIPT_STORES = CMS_MEDICINE_RECEIPT_STORES;
                            if (CMS_MEDICINE_RECEIPT_STORES != null)
                            {
                                Session["CMS_REPORT_SERVICES"] = CMS_MEDICINE_RECEIPT_STORES;
                            }
                            break;

                            //case "ExportReportExportMedicine":

                            //    if (Session["CMS_REPORT_SERVICES"] != null)
                            //    {
                            //        CMS_MEDICINE_EXPORT_STORES = (List<CMS_MEDICINE_EXPORT_STORE>)Session["CMS_REPORT_SERVICES"];
                            //    }
                            //    else
                            //    {
                            //        CMS_MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByDATE(Tungay, Denngay.AddDays(1), UserInfo.PARTNERID);
                            //        if (CMS_MEDICINE_EXPORT_STORES != null)
                            //        {
                            //            Session["CMS_REPORT_SERVICES"] = CMS_MEDICINE_EXPORT_STORES;
                            //        }
                            //    }

                            //    if (CMS_MEDICINE_EXPORT_STORES != null)
                            //    {
                            //        if (CMS_MEDICINE_EXPORT_STORES.Count > 0)
                            //        {
                            //            string[] columns = { "rownum", "SERVICE_NAME", "Count_Service", "SERVICETYPE_NAME", "Total_Price" };
                            //            string timeReport = "Thời gian báo cáo từ: " + obj.startdate + " - " + obj.enddate;


                            //            if (UserInfo == null)
                            //            {
                            //                Response.Redirect("/account/login");
                            //            }
                            //            string creater = "Người báo cáo: " + UserInfo.Hoten;
                            //            byte[] filecontent = ExcelExportHelper.ExportExcel(CMS_REPORT_SERVICES, "Tổng hợp báo cáo dịch vụ", timeReport, creater, true, columns);


                            //            return File(filecontent, ExcelExportHelper.ExcelContentType, "ReportService.xlsx");
                            //        }
                            //        else
                            //        {
                            //            ViewBag.TitleSuccsess = "Không tìm thấy dữ liệu. Mời bạn chọn lại";
                            //            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        ViewBag.TitleSuccsess = "Không tìm thấy dữ liệu. Mời bạn chọn lại";
                            //        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            //    }
                            //    break;

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


        public ActionResult ReceiptExportMedicinePrint(String startdate, String enddate, string Token)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo bán thuốc", "");
            AddBreadcrumb("Báo cáo bán thuốc", "/Report/ReportReceiptMedicinePrint");
            ViewBag.Title = "Báo cáo bán thuốc";
            List<CMS_MEDICINE_RECEIPT_STORE> CMS_MEDICINE_RECEIPT_STORES;
            string TextErr = string.Empty;
            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(startdate.Trim()))
                    Tungay = DateTime.ParseExact(startdate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(enddate.Trim()))
                    Denngay = DateTime.ParseExact(enddate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));

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
                report.startdate = Tungay.ToString("dd/MM/yyyy");
                report.enddate = Denngay.ToString("dd/MM/yyyy");
                if (TextErr.Length == 0)
                {
                    if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate) && !string.IsNullOrEmpty(Token))
                    {
                        if (CMSLIS.Common.Common.CheckKeyPrivate(startdate, enddate, Token))
                        {
                            CMS_MEDICINE_RECEIPT_STORES = impCMS_MEDICINE_RECEIPT_STORE.GetReportCMS_MEDICINE_RECEIPT_STOREByDate(Tungay, Denngay.AddDays(1), UserInfo.PARTNERID);
                            ViewBag.CMS_MEDICINE_RECEIPT_STORES = CMS_MEDICINE_RECEIPT_STORES;
                        }
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
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(report)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A4
                },
            };

        }


        #endregion

        #region --> Báo cáo đặt lịch khám 
        public ActionResult medicalAppointmentReport()
        {

            return View();
        }


        //[HttpPost]
        //public ActionResult BaoCaoBanHangNhom(SearchBanHangTheoNhomModel obj, string submit)
        //{
        //    AddBreadcrumb("Báo cáo bán hàng theo nhóm", "/ReportingSystem/BaoCaoBanHangNhom");
        //    AddBreadcrumb("Chi tiet", "/Home/About");
        //    List<TongHopBanHangTheoNhom> DataReportExcel = null;
        //    DateTime Tungay = new DateTime();
        //    DateTime Denngay = new DateTime();

        //    // Loading drop down lists.              
        //    this.ViewBag.GetMaKho = this.GetMaKho();
        //    this.ViewBag.GetManhom = this.GetManhom();


        //    #region Check input data
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(obj.Tungay.Trim()))
        //            Tungay = DateTime.ParseExact(obj.Tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
        //        if (!string.IsNullOrEmpty(obj.Denngay.Trim()))
        //            Denngay = DateTime.ParseExact(obj.Denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

        //        if (Tungay > Denngay)
        //        {
        //            ViewBag.TextErr = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày";
        //        }

        //        if (Tungay.Date.AddMonths(10) < Denngay.Date)
        //        {
        //            ViewBag.TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
        //        }
        //    }
        //    catch
        //    {
        //        ViewBag.TextErr = "Dữ liệu ngày tháng không đúng định dạng";
        //    }
        //    #endregion

        //    ImpTongHopBanHangTheoNhom impTongHopBanHangTheoNhom = new ImpTongHopBanHangTheoNhom();
        //    try
        //    {
        //        switch (submit)
        //        {
        //            case "SearchBaoCaoBanHangNhom":
        //                DataReportExcel = impTongHopBanHangTheoNhom.GetTongHopBanHangTheoNhom(Tungay, Denngay, obj.makho, obj.manhom);
        //                ViewBag.DataReport = DataReportExcel;
        //                if (DataReportExcel != null)
        //                {
        //                    Session["BaoCaoBanHangNhom"] = DataReportExcel;
        //                }

        //                break;
        //            case "ExportBaoCaoBanHangNhom":

        //                if (Session["BaoCaoBanHangNhom"] != null)
        //                {
        //                    DataReportExcel = (List<TongHopBanHangTheoNhom>)Session["BaoCaoBanHangNhom"];
        //                }
        //                else
        //                {
        //                    DataReportExcel = impTongHopBanHangTheoNhom.GetTongHopBanHangTheoNhom(Tungay, Denngay, obj.makho, obj.manhom);
        //                    Session["BaoCaoBanHangNhom"] = DataReportExcel;
        //                }

        //                if (DataReportExcel != null)
        //                {
        //                    if (DataReportExcel.Count > 0)
        //                    {
        //                        string[] columns = { "MANHOM", "TENNHOM", "tongnhap", "tongban", "tylelaisuat", "TENKHOLE" };
        //                        string timeReport = "Thời gian báo cáo từ: " + obj.Tungay + " - " + obj.Denngay;

        //                        var UserInfo = ((ZD_DM_USER)Session["UserInfo"]);
        //                        if (UserInfo == null)
        //                        {
        //                            Response.Redirect("/account/login");
        //                        }
        //                        string creater = "Người báo cáo: " + UserInfo.HOTEN;
        //                        byte[] filecontent = ExcelExportHelper.ExportExcel(DataReportExcel, "Tổng hợp bán hàng theo nhóm", timeReport, creater, true, columns);
        //                        return File(filecontent, ExcelExportHelper.ExcelContentType, "BaoCaoBanHangNhom.xlsx");
        //                    }
        //                    else
        //                    {
        //                        ViewBag.TextErr = "Không tìm thấy dữ liệu. Mời bạn chọn lại";
        //                    }
        //                }
        //                else
        //                {
        //                    ViewBag.TextErr = "Không tìm thấy dữ liệu. Mời bạn chọn lại";
        //                }
        //                break;
        //            case "PrintBaoCaoBanHangNhom":
        //                return RedirectToAction("BaoCaoBanHangNhomPrint", "ReportingSystem", obj);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.Info("BaoCaoBanHangNhom:" + ex.ToString());
        //    }

        //    return View(obj);
        //}


        #endregion


        #region --> Báo cáo kê đơn thuốc
        public ActionResult ReportMedicinePrescription()
        {
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo kê đơn thuốc", "");
            AddBreadcrumb("Báo cáo kê đơn thuốc", "/Report/ReportMedicinePrescription");
            ViewBag.Title = "Báo cáo kê đơn thuốc";

            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            report.DOCTOR_ID = 0;
            report.startdate = DateTime.Now.ToString("dd/MM/yyyy");
            report.enddate = DateTime.Now.ToString("dd/MM/yyyy");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {

                List<CMS_PATIENT_PRESCRIPTION> CMS_PATIENT_PRESCRIPTIONS = impCMS_PATIENT_PRESCRIPTION.GetPRESCRIPTION_REPORT(DateTime.Today, DateTime.Now.AddDays(1), 0, UserInfo.PARTNERID);
                ViewBag.CMS_PATIENT_PRESCRIPTIONS = CMS_PATIENT_PRESCRIPTIONS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ReportMedicinePrescription:" + ex.ToString());
            }

            // Info.  
            return View(report);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportMedicinePrescription(CMSNEW.Models.ReportViewModels obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo kê đơn thuốc", "");
            AddBreadcrumb("Báo cáo kê đơn thuốc", "/Report/ReportMedicinePrescription");
            ViewBag.Title = "Báo cáo kê đơn thuốc";
            List<CMS_PATIENT_PRESCRIPTION> CMS_PATIENT_PRESCRIPTIONS;
            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
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


            try
            {

                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchReportMedicinePrescription":
                            CMS_PATIENT_PRESCRIPTIONS = impCMS_PATIENT_PRESCRIPTION.GetPRESCRIPTION_REPORT(Tungay, Denngay.AddDays(1), obj.DOCTOR_ID, UserInfo.PARTNERID);
                            ViewBag.CMS_PATIENT_PRESCRIPTIONS = CMS_PATIENT_PRESCRIPTIONS;

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

            }

            // Info.  
            return View(obj);
        }


        public ActionResult ReportMedicinePrescriptionPrint(String startdate, String enddate, String DOCTOR_ID, string Token)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo kê đơn thuốc", "");
            AddBreadcrumb("Báo cáo kê đơn thuốc", "/Report/ReportMedicinePrescriptionPrint");
            ViewBag.Title = "Báo cáo kê đơn thuốc";
            List<CMS_PATIENT_PRESCRIPTION> CMS_PATIENT_PRESCRIPTIONS;
            string TextErr = string.Empty;
            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(startdate.Trim()))
                    Tungay = DateTime.ParseExact(startdate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(enddate.Trim()))
                    Denngay = DateTime.ParseExact(enddate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));

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
                if (string.IsNullOrEmpty(DOCTOR_ID))
                {
                    DOCTOR_ID = "0";
                }



                report.startdate = Tungay.ToString("dd/MM/yyyy");
                report.enddate = Denngay.ToString("dd/MM/yyyy");
                if (TextErr.Length == 0)
                {
                    if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate) && !string.IsNullOrEmpty(Token))
                    {
                        if (CMSLIS.Common.Common.CheckKeyPrivate(startdate, enddate, Token))
                        {
                            CMS_PATIENT_PRESCRIPTIONS = impCMS_PATIENT_PRESCRIPTION.GetPRESCRIPTION_REPORT(Tungay, Denngay.AddDays(1), Convert.ToInt32(DOCTOR_ID), UserInfo.PARTNERID);
                            ViewBag.CMS_PATIENT_PRESCRIPTIONS = CMS_PATIENT_PRESCRIPTIONS;
                        }
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
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(report)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A4
                },
            };

        }


        #endregion


        #region --> Báo cáo kê chỉ định  
        public ActionResult ReportPaientService()
        {
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo chỉ định dịch vụ", "");
            AddBreadcrumb("Báo cáo chỉ định dịch vụ", "/Report/ReportPaientService");
            ViewBag.Title = "Báo cáo chỉ định dịch vụ";

            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            report.DOCTOR_ID = 0;
            report.startdate = DateTime.Now.ToString("dd/MM/yyyy");
            report.enddate = DateTime.Now.ToString("dd/MM/yyyy");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {

                List<CMS_PATIENT_SERVICE> CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetSP_PATIENT_SERVICE_REPORT(DateTime.Today, DateTime.Now.AddDays(1), 0, UserInfo.PARTNERID);
                ViewBag.CMS_PATIENT_SERVICES = CMS_PATIENT_SERVICES;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ReportMedicinePrescription:" + ex.ToString());
            }

            // Info.  
            return View(report);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportPaientService(CMSNEW.Models.ReportViewModels obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo chỉ định dịch vụ", "");
            AddBreadcrumb("Báo cáo chỉ định dịch vụ", "/Report/ReportPaientService");
            ViewBag.Title = "Báo cáo chỉ định dịch vụ";
            List<CMS_PATIENT_SERVICE> CMS_PATIENT_SERVICES;
            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
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

            try
            {

                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchReportPaientService":
                            CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetSP_PATIENT_SERVICE_REPORT(Tungay, Denngay.AddDays(1), obj.DOCTOR_ID, UserInfo.PARTNERID);
                            ViewBag.CMS_PATIENT_SERVICES = CMS_PATIENT_SERVICES;
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

            }

            // Info.  
            return View(obj);
        }


        public ActionResult ReportPaientServicePrint(String startdate, String enddate, String DOCTOR_ID, string Token)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo chỉ định dịch vụ", "");
            AddBreadcrumb("Báo cáo chỉ định dịch vụ", "/Report/ReportPaientServicePrint");
            ViewBag.Title = "Báo cáo chỉ định dịch vụ";


            List<CMS_PATIENT_SERVICE> CMS_PATIENT_SERVICES;
            string TextErr = string.Empty;
            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(startdate.Trim()))
                    Tungay = DateTime.ParseExact(startdate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(enddate.Trim()))
                    Denngay = DateTime.ParseExact(enddate.Trim(), "ddMMyyyy", new CultureInfo("en-US"));

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
                if (string.IsNullOrEmpty(DOCTOR_ID))
                {
                    DOCTOR_ID = "0";
                }



                report.startdate = Tungay.ToString("dd/MM/yyyy");
                report.enddate = Denngay.ToString("dd/MM/yyyy");
                if (TextErr.Length == 0)
                {
                    if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate) && !string.IsNullOrEmpty(Token))
                    {
                        if (CMSLIS.Common.Common.CheckKeyPrivate(startdate, enddate, Token))
                        {
                            CMS_PATIENT_SERVICES = impCMS_PATIENT_SERVICE.GetSP_PATIENT_SERVICE_REPORT(Tungay, Denngay.AddDays(1), Convert.ToInt32(DOCTOR_ID), UserInfo.PARTNERID);
                            ViewBag.CMS_PATIENT_SERVICES = CMS_PATIENT_SERVICES;
                        }
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
            // return View(obj);
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Trang: [page]/[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.MVC.PartialViewAsPdf(report)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions()
                {
                    CustomSwitches = footer,
                    PageSize = Rotativa.Core.Options.Size.A4
                },
            };

        }


        #endregion

        #region --> Báo cáo truy cap log 
        public ActionResult ReportSearchCTV()
        {
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo truy cập ctv", "");
            AddBreadcrumb("Báo cáo truy cập ctv", "/Report/ReportSearchCTV");
            ViewBag.Title = "Báo cáo truy cập ctv";

            CMSNEW.Models.ReportViewModels report = new Models.ReportViewModels();
            report.DOCTOR_ID = 0;
            report.startdate = DateTime.Now.ToString("dd/MM/yyyy");
            report.enddate = DateTime.Now.ToString("dd/MM/yyyy");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {

                List<tbl_logSearch> CMS_logSearch = imptbl_logSearch.Gettbl_logSearchByDoctorName(DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"), "0");
                ViewBag.CMS_logSearch = CMS_logSearch;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ReportSearchCTV:" + ex.ToString());
            }

            // Info.  
            return View(report);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportSearchCTV(CMSNEW.Models.ReportViewModels obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Báo cáo", "");
            AddPageHeader("Báo cáo truy cập ctv", "");
            AddBreadcrumb("Báo cáo truy cập ctv", "/Report/ReportSearchCTV");
            ViewBag.Title = "Báo cáo truy cập ctv";

            string TextErr = string.Empty;

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
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

            try
            {

                if (TextErr.Length == 0)
                {
                    switch (submit)
                    {
                        case "SearchReportSearchCTV":
                            List<tbl_logSearch> CMS_logSearch = imptbl_logSearch.Gettbl_logSearchByDoctorName(Tungay.ToString("yyyyMMdd"), Tungay.ToString("yyyyMMdd"), obj.DOCTOR_ID.ToString());
                            ViewBag.CMS_logSearch = CMS_logSearch;
                            break;
                        case "ExportReportSearchCTV":
                            CMS_logSearch = imptbl_logSearch.Gettbl_logSearchByDoctorName(Tungay.ToString("yyyyMMdd"), Tungay.ToString("yyyyMMdd"), obj.DOCTOR_ID.ToString());
                            ViewBag.CMS_logSearch = CMS_logSearch;



                            ExcelPackage Ep = new ExcelPackage();
                            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("dulieu");


                            Sheet.Cells["A1"].Value = "THỐNG KÊ  ĐĂNG NHẬP CTV";
                            Sheet.Cells["A1:E1"].Merge = true;
                            Sheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            Sheet.Cells["A2"].Value = "Thống kê từ ngày: " + Tungay.ToString("dd/MM/yyyy") + " đến ngày: " + Denngay.ToString("dd/MM/yyyy");
                            Sheet.Cells["A2:E2"].Merge = true;
                            Sheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            Sheet.Cells["A3"].Value = "Người lập báo cáo: " + UserInfo.Hoten;
                            Sheet.Cells["A3:E3"].Merge = true;
                            Sheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            Sheet.Cells["A4"].Value = "STT";
                            Sheet.Cells["B4"].Value = "UserName";
                            Sheet.Cells["C4"].Value = "doctorid";
                            Sheet.Cells["D4"].Value = "IP";
                            Sheet.Cells["E4"].Value = "DateSearch";


                            int stt = 1;
                            int row = 5;
                            foreach (var item in CMS_logSearch)
                            {
                                Sheet.Cells[string.Format("A{0}", row)].Value = stt;
                                Sheet.Cells[string.Format("B{0}", row)].Value = item.Username;
                                Sheet.Cells[string.Format("C{0}", row)].Value = item.doctorID;
                                Sheet.Cells[string.Format("D{0}", row)].Value = item.IP;
                                Sheet.Cells[string.Format("E{0}", row)].Value = CMSLIS.Common.Common.ReFmtTime(item.datesave);

                                row++;
                                stt++;
                            }


                            Sheet.Cells["A:AZ"].AutoFitColumns();
                            Response.Clear();
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment: filename=" + "ReportSearchCTV.xlsx");
                            Response.BinaryWrite(Ep.GetAsByteArray());
                            Response.End();

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

            }

            // Info.  
            return View(obj);
        }


      

        #endregion

    }
}
using CMS_Core.Entity;
using CMS_Core.Implementtion;
using CMSLIS.Models;
using CMSNEW.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSLIS.Controllers
{
    [CheckAuthorization]
    public class HomeController : BaseController
    {
        ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");

        public ActionResult Index()
        {
            try
            {

                // Initialization.       
                AddPageHeader("Hệ thống CMS MEDCOM", "");
                AddBreadcrumb("Hệ thống CMS MEDCOM", "/Home/index");


            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("Index:" + ex.ToString());
            }
            // Info.  
            return View();

        }
        [AjaxValidateAntiForgeryToken]
        public JsonResult getDashboard(int type)
        {
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                IEnumerable<Dashboard_Index> modelList = new List<Dashboard_Index>();
                ListDashboard_Index listDashboard = null;

                if (Session["DashboardIndex"] != null)
                {
                    listDashboard = (ListDashboard_Index)Session["DashboardIndex"];
                }
                else
                {
                    ImpDashboard_Index impDashboard_Index = new ImpDashboard_Index();
                    listDashboard = impDashboard_Index.GetDashboard_Index(UserInfo.PARTNERID);
                }

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


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
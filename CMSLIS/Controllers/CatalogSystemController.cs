using CMS_Core.Entity;
using CMS_Core.Implementtion;
using CMSLIS.Common;
using CMSLIS.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Globalization;
using CMSNEW.Models;
using System.IO;

namespace CMSLIS.Controllers
{
    [CheckAuthorization]
    public class CatalogSystemController : BaseController
    {
        ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");

        private Impcms_Category impcms_Category;
        private ImpCOMPLEMENT_SETTUP impCOMPLEMENT_SETTUP;

        private ImpClassComboBox impClassComboBox;

        private ImpCMS_PARTNER impCMS_PARTNER;
        private ImpCMS_SERVICE impCMS_SERVICE;
        private ImpCMS_SERVICE_GROUP impCMS_SERVICE_GROUP;
        private ImpCMS_SERVICE_GROUP_CATE impCMS_SERVICE_GROUP_CATE;
        private ImpCMS_SICK impCMS_SICK;
        private ImpCMS_TEMPLATERESULTEXAMINATION impCMS_TEMPLATERESULTEXAMINATION;
        private ImpCMS_DEPARTMENT impCMS_DEPARTMENT;
        private ImpCMS_ROOM_CLINIC impCMS_ROOM_CLINIC;
        private Impcms_NewsSource impcms_NewsSource;
        private Impcms_NewsTypeMessage impcms_NewsTypeMessage;
        private ImpAutolink impAutolink;
        private Imptbl_Slider imptbl_Slider;
        private Imptbl_Map imptbl_Map;
        private Imptbl_TestCodecs imptbl_TestCodecs;
        private Impcms_News_RelateTestcode impcms_News_RelateTestcode;
        private Impcms_News impcms_News;
        private Imptbl_seo imptbl_seo;
        private Imptbl_VoteCate imptbl_VoteCate;
        private Imptbl_Vote imptbl_Vote;
        private Imptbl_VoteQuestions imptbl_VoteQuestions;
        public CatalogSystemController()
        {
            impcms_Category = new Impcms_Category();
            impCOMPLEMENT_SETTUP = new ImpCOMPLEMENT_SETTUP();

            impClassComboBox = new ImpClassComboBox();

            impCMS_PARTNER = new ImpCMS_PARTNER();
            impCMS_SERVICE = new ImpCMS_SERVICE();
            impCMS_SERVICE_GROUP = new ImpCMS_SERVICE_GROUP();
            impCMS_SERVICE_GROUP_CATE = new ImpCMS_SERVICE_GROUP_CATE();
            impCMS_SICK = new ImpCMS_SICK();
            impCMS_TEMPLATERESULTEXAMINATION = new ImpCMS_TEMPLATERESULTEXAMINATION();
            impCMS_DEPARTMENT = new ImpCMS_DEPARTMENT();
            impCMS_ROOM_CLINIC = new ImpCMS_ROOM_CLINIC();
            impcms_NewsSource = new Impcms_NewsSource();
            impcms_NewsTypeMessage = new Impcms_NewsTypeMessage();
            impAutolink = new ImpAutolink();
            imptbl_Slider = new Imptbl_Slider();
            imptbl_Map = new Imptbl_Map();
            imptbl_TestCodecs = new Imptbl_TestCodecs();
            impcms_News_RelateTestcode = new Impcms_News_RelateTestcode();
            impcms_News = new Impcms_News();
            imptbl_seo = new Imptbl_seo();
            imptbl_VoteCate = new Imptbl_VoteCate();
            imptbl_Vote = new Imptbl_Vote();
            imptbl_VoteQuestions = new Imptbl_VoteQuestions();
        }


        // GET: CatalogSystem
        public ActionResult Index()
        {
            return View();
        }


        //#region --> Danh sách Menu

        //public ActionResult ListCategorys(string cateParrent)
        //{
        //    // Initialization. 
        //    AddPageHeader("Hệ thống danh mục", "");
        //    AddBreadcrumb("Hệ thống danh mục", "");

        //    AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/ListCategorys");


        //    var UserInfo = ((cms_Account)Session["UserInfo"]);

        //    if (string.IsNullOrEmpty(cateParrent))
        //    {
        //        cateParrent = "0";
        //    }

        //    SearchMenuViewModel obj = new SearchMenuViewModel();
        //    obj.Status = Constant.TypeStatusPublic.ToString();
        //    obj.parentID = cateParrent.ToString();
        //    obj.TypeKeyword = 0;
        //    obj.keyword = string.Empty;

        //    this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
        //    this.ViewBag.GetTypeKeyword = Common.Common.GetCategoryKeyword();

        //    try
        //    {

        //        List<cms_Category> _cms_Categorys = impcms_Category.GetAllcms_Category(Convert.ToInt32(obj.parentID), Constant.TypeStatusPublic, obj.TypeKeyword, obj.keyword);
        //        ViewBag.DataReport = _cms_Categorys;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
        //        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
        //        logError.Info("ListCategorys:" + ex.ToString());
        //    }

        //    // Info.  
        //    return View(obj);
        //}

        //[HttpPost]
        //public ActionResult ListCategorys(SearchMenuViewModel obj, string submit)
        //{
        //    // Initialization.  
        //    AddPageHeader("Hệ thống danh mục", "");
        //    AddBreadcrumb("Hệ thống danh mục", "");

        //    AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/ListCategorys");


        //    this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
        //    this.ViewBag.GetTypeKeyword = Common.Common.GetCategoryKeyword();

        //    try
        //    {
        //        var UserInfo = ((cms_Account)Session["UserInfo"]);

        //        switch (submit)
        //        {
        //            case "SearchMenu":
        //                List<cms_Category> _cms_Categorys = impcms_Category.GetAllcms_Category(Convert.ToInt32(obj.parentID), Convert.ToInt32(obj.Status), obj.TypeKeyword, obj.keyword);
        //                ViewBag.DataReport = _cms_Categorys;
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
        //        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
        //        logError.Info("ListCategorys:" + ex.ToString());
        //    }

        //    // Info.  
        //    return View(obj);
        //}

        //[HttpPost]
        //[AjaxValidateAntiForgeryToken]
        //public ActionResult ListCategorysDelete(string[] customerIDs)
        //{
        //    // Initialization.  
        //    AddPageHeader("Hệ thống danh mục", "");
        //    AddBreadcrumb("Hệ thống danh mục", "");

        //    AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/ListCategorysDelete");
        //    string listID = string.Empty;
        //    string listIDError = string.Empty;
        //    string result = string.Empty;
        //    var UserInfo = ((cms_Account)Session["UserInfo"]);

        //    try
        //    {
        //        // Kiểm tra xem có bản ghi nào được chọn không?
        //        if (customerIDs != null)
        //        {


        //            // xóa dữ liệu
        //            foreach (string customerID in customerIDs)
        //            {
        //                result = impcms_Category.Delete(Convert.ToInt32(customerID));
        //                if (!string.IsNullOrEmpty(result))
        //                {
        //                    if (result.Equals(CMS_Core.Common.Constant.typeORAForeign.ToString()))
        //                    {
        //                        listIDError += customerID + ",";
        //                    }
        //                    else
        //                    {
        //                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
        //                        listID += customerID + ",";
        //                    }
        //                }
        //                else
        //                {
        //                    AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
        //                    listID += customerID + ",";
        //                }
        //            }
        //            if (listID.IndexOf(",") != -1)
        //            {
        //                listID = listID.Substring(0, listID.Length - 1);
        //            }
        //            if (listIDError.IndexOf(",") != -1)
        //            {
        //                listIDError = listIDError.Substring(0, listIDError.Length - 1);
        //            }
        //        }
        //        else
        //        {
        //            return Json("Chưa có bản tin nào được chọn để xóa");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.Info("ListCategorysDelete: " + ex.ToString());
        //        return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
        //    }

        //    if (listIDError.Length > 0)
        //    {
        //        return Json("Xóa thành công bản ghi có id là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do bản ghi đã được chọn rồi!");
        //    }
        //    else
        //    {
        //        return Json("Xóa thành công bản ghi có id là: " + listID);
        //    }

        //    // Info.  
        //    //return View();
        //}


        //[HttpPost]
        //[AjaxValidateAntiForgeryToken]
        //public ActionResult ListCategorysPublic(string[] customerIDs)
        //{
        //    // Initialization.  
        //    AddPageHeader("Hệ thống danh mục", "");
        //    AddBreadcrumb("Hệ thống danh mục", "");

        //    AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/ListCategorysPublic");
        //    string listID = string.Empty;
        //    try
        //    {
        //        // Kiểm tra xem có bản ghi nào được chọn không?
        //        if (customerIDs != null)
        //        {
        //            var UserInfo = ((cms_Account)Session["UserInfo"]);

        //            // duyệt dữ liệu
        //            foreach (string customerID in customerIDs)
        //            {
        //                impcms_Category.Publish(Convert.ToInt32(customerID));
        //                AddLogAction(customerID, Constant.ActionPublicOK.ToString());
        //                listID += customerID + ",";
        //            }
        //            if (listID.IndexOf(",") != -1)
        //            {
        //                listID = listID.Substring(0, listID.Length - 1);
        //            }
        //        }
        //        else
        //        {
        //            return Json("Chưa có bản tin nào được chọn để duyệt");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.Info("MenuPublic: " + ex.ToString());
        //    }

        //    return Json("Duyệt thành công bản ghi có id là: " + listID);

        //    // Info.  
        //    //return View();
        //}


        //public ActionResult CategorysAdd(string cateParrent, string cateId)
        //{
        //    // Initialization.   
        //    AddPageHeader("Hệ thống danh mục", "");


        //    cms_Category _cms_Category = new cms_Category();


        //    var UserInfo = ((cms_Account)Session["UserInfo"]);

        //    if (string.IsNullOrEmpty(cateParrent))
        //    {
        //        cateParrent = "0";
        //    }

        //    try
        //    {
        //        if (string.IsNullOrEmpty(cateId))
        //        {
        //            AddPageHeader("Thêm mới chuyên mục", "");
        //            AddBreadcrumb("Thêm mới chuyên mục", "/CatalogSystem/CategorysAdd");
        //            ViewBag.Title = "Thêm mới menu";
        //            _cms_Category.cateParrent = Convert.ToInt32(cateParrent);

        //        }
        //        else
        //        {
        //            AddPageHeader("Cập nhật  chuyên mục", "");
        //            AddBreadcrumb("Cập nhật  chuyên mục", "/CatalogSystem/CategorysAdd");
        //            ViewBag.Title = "Cập nhật menu";
        //            _cms_Category = impcms_Category.Get(Convert.ToInt32(cateId));
        //            if (_cms_Category != null)
        //            {
        //                if (!string.IsNullOrEmpty(_cms_Category.cateName))
        //                {
        //                    //  _Menus[0].cateId = Convert.ToInt32(parentID);
        //                    return View(_cms_Category);
        //                }
        //                else
        //                {
        //                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin chuyên mục";
        //                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
        //                }
        //            }
        //            else
        //            {
        //                ViewBag.TitleSuccsess = "Không tìm thấy thông tin chuyên mục";
        //                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
        //        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
        //        logError.Info("CategorysAdd:" + ex.ToString());
        //    }

        //    // Info.  
        //    return View(_cms_Category);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[ValidateInput(false)]
        //public ActionResult CategorysAdd(cms_Category _cms_Category)
        //{
        //    // Initialization.             
        //    AddPageHeader("Hệ thống danh mục", "");
        //    if (_cms_Category.cateId == 0)
        //    {
        //        AddPageHeader("Thêm mới chuyên mục", "");
        //        AddBreadcrumb("Thêm mới chuyên mục", "/CatalogSystem/CategorysAdd");

        //    }
        //    else
        //    {
        //        AddPageHeader("Cập nhật chuyên mục", "");
        //        AddBreadcrumb("Cập nhật chuyên mục", "/CatalogSystem/CategorysAdd");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var UserInfo = ((cms_Account)Session["UserInfo"]);

        //        _cms_Category.CreateBy = UserInfo.uid;
        //        _cms_Category.Create_date = DateTime.Now;



        //        string result = string.Empty;
        //        if (_cms_Category.cateId == 0)
        //        {

        //            List<cms_Category> cms_Categories = impcms_Category.Getcms_CategoryByCateName(_cms_Category.cateName);

        //            if (cms_Categories != null)
        //            {
        //                if (cms_Categories.Count > 0)
        //                {
        //                    ViewBag.TitleSuccsess = "Đã tồn tại chuyên mục rồi. Mời bạn chọn tên chuyên mục mới";
        //                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
        //                }
        //                else
        //                {
        //                    result = impcms_Category.Add(_cms_Category);
        //                }
        //            }
        //            else
        //            {


        //                result = impcms_Category.Add(_cms_Category);
        //            }

        //            if (!string.IsNullOrEmpty(result))
        //            {
        //                ViewBag.TitleSuccsess = "Thêm mới chuyên mục thành công";
        //                AddLogAction(result, Constant.ActionInsertOK.ToString());
        //                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
        //                Response.Redirect("/CatalogSystem/ListCategorys?cateParrent=" + _cms_Category.cateParrent, false);
        //            }
        //            else
        //            {
        //                ViewBag.TitleSuccsess = "Thêm mới Menu không thành công";
        //                AddLogAction(result, Constant.ActionInsertNOK.ToString());
        //                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
        //            }
        //        }
        //        else
        //        {
        //            _cms_Category.UpdateBy = UserInfo.uid;
        //            _cms_Category.Update_date = DateTime.Now;

        //            result = impcms_Category.Update(_cms_Category);
        //            if (!string.IsNullOrEmpty(result))
        //            {
        //                ViewBag.TitleSuccsess = "Cập nhật thông tin menu thành công";
        //                AddLogAction(result, Constant.ActionUpdateOK.ToString());
        //                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
        //                Response.Redirect("/CatalogSystem/ListCategorys?cateParrent=" + _cms_Category.cateParrent, false);
        //            }
        //            else
        //            {
        //                ViewBag.TitleSuccsess = "Cập nhật thông tin menu không thành công";
        //                AddLogAction(result, Constant.ActionUpdateNOK.ToString());
        //                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
        //            }
        //        }
        //    }

        //    return View(_cms_Category);
        //}

        //#endregion





        #region --> Danh sách chuyên mục động





        [HttpPost]
        public JsonResult getListData(string str1, string str2, string str3)
        {
            try
            {
                IEnumerable<ClassComboBox> modelList = new List<ClassComboBox>();

                modelList = impClassComboBox.Getcms_CategoryByParent(str1, Convert.ToInt32(str2), str3);
                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("getListData:" + str2 + "  " + ex.ToString());
                return Json(string.Empty);
            }
        }

        #endregion






        #region --> Danh sách Chuyên mục Hệ thống setup trường  

        public ActionResult COMPLEMENT_SETTUPS(string ID)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách các trường trong bảng", "/CatalogSystem/CATEGORYS");
            COMPLEMENT_SETTUPViewModel COMPLEMENT_SETTUP = new COMPLEMENT_SETTUPViewModel();

            try
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    COMPLEMENT_SETTUP.COMPLEMENT_SETTUPS = impCOMPLEMENT_SETTUP.GetAllCOMPLEMENT_SETTUP_BYCATEID(Convert.ToInt32(ID));
                }

            }
            catch (Exception ex)
            {
                logError.Info("COMPLEMENT_SETTUPS: " + ex.ToString());
            }

            // Info.  
            return View(COMPLEMENT_SETTUP);
        }





        #endregion


        #region --> Danh sách đối tác

        public ActionResult partner()
        {
            // Initialization.  
            AddPageHeader("Danh sách đối tác", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách đối tác", "/CatalogSystem/partner");


            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {
                List<CMS_PARTNER> _PARTNERs = impCMS_PARTNER.GetAllMS_PARTNER();
                ViewBag.PARTNER = _PARTNERs;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("partner:" + ex.ToString());
            }
            SearchMenuViewModel obj = new SearchMenuViewModel();
            obj.parentID = "0";
            // Info.  
            return View(obj);
        }

        [HttpPost]
        public ActionResult partner(SearchMenuViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách đối tác", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách đối tác", "/CatalogSystem/partner");


            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);

                switch (submit)
                {
                    case "SearchPartner":
                        List<CMS_PARTNER> _PARTNERs = impCMS_PARTNER.GetAllMS_PARTNER_BYSTATUS(Convert.ToInt32(obj.Status));
                        ViewBag.PARTNER = _PARTNERs;

                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("Menu:" + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult partnerDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách đối tác", "/CatalogSystem/partner");
            string listID = string.Empty;
            string listIDError = string.Empty;
            string result = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        result = impCMS_PARTNER.DeleteCMS_PARTNER(Convert.ToInt32(customerID));

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
                logError.Info("partnerDelete: " + ex.ToString());
                return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
            }

            if (listIDError.Length > 0)
            {
                return Json("Xóa thành công bản ghi có id là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do bản ghi đã được chọn rồi!");
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
        public ActionResult partnerPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách đối tác", "/CatalogSystem/partner");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impCMS_PARTNER.PUBLILC_CMS_PARTNER(Convert.ToInt32(customerID));
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
                logError.Info("partnerPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult partnerAdd(string ID)
        {
            // Initialization.    

            AddBreadcrumb("Hệ thống danh mục", "");

            CMS_PARTNER _PARTNER = new CMS_PARTNER();
            List<CMS_PARTNER> _PARTNERs = null;
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    AddPageHeader("Thêm mới đối tác", "");
                    AddBreadcrumb("Thêm mới đối tác", "/CatalogSystem/partnerAdd");
                    ViewBag.panelTitle = "Thêm mới đối tác";
                }
                else
                {
                    AddPageHeader("Cập nhật đối tác", "");
                    AddBreadcrumb("Cập nhật đối tác", "/CatalogSystem/partnerAdd");
                    ViewBag.panelTitle = "Cập nhật đối tác";
                    _PARTNERs = impCMS_PARTNER.GetCMS_PARTNER_BYID(ID);
                    if (_PARTNERs != null)
                    {
                        if (_PARTNERs.Count > 0)
                            return View(_PARTNERs[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("partnerAdd:" + ex.ToString());
            }

            // Info.  
            return View(_PARTNER);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult partnerAdd(CMS_PARTNER _PARTNER)
        {
            AddBreadcrumb("Hệ thống danh mục", "");

            try
            {
                if (_PARTNER.ID == 0)
                {
                    AddPageHeader("Thêm mới đối tác", "");
                    AddBreadcrumb("Thêm mới đối tác", "/CatalogSystem/partnerAdd");
                    ViewBag.panelTitle = "Thêm mới đối tác";
                }
                else
                {
                    AddPageHeader("Cập nhật đối tác", "");
                    AddBreadcrumb("Cập nhật đối tác", "/CatalogSystem/partnerAdd");
                    ViewBag.panelTitle = "Cập nhật đối tác";
                }

                if (ModelState.IsValid)
                {
                    string result = string.Empty;
                    if (_PARTNER.ID == 0)
                    {
                        // check partner code
                        List<CMS_PARTNER> _PARTNERs = impCMS_PARTNER.GetCMS_PARTNERByCode(_PARTNER.PARTNER_CODE);
                        var UserInfo = ((cms_Account)Session["UserInfo"]);
                        if (_PARTNERs != null)
                        {
                            if (_PARTNERs.Count > 0)
                            {
                                ViewBag.TitleSuccsess = "Đã tồn tại mã cho đối tác rồi, Mời bạn chọn mã khác!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                            else
                            {
                                bool check = true;
                                _PARTNERs = impCMS_PARTNER.GetCMS_PARTNERByName(_PARTNER.PARTNER_NAME);
                                if (_PARTNERs != null)
                                {
                                    if (_PARTNERs.Count > 0)
                                    {
                                        ViewBag.TitleSuccsess = "Đã tồn tại tên đối tác rồi, Mời bạn nhập tên đối tác khác!";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                        check = false;
                                    }
                                }

                                if (check)
                                {
                                    _PARTNER.PARTNER_CREATE_DATE = DateTime.Now;
                                    _PARTNER.PARTNER_CREATE = UserInfo.uid;

                                    result = impCMS_PARTNER.InsertCMS_PARTNER(_PARTNER);
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        ViewBag.TitleSuccsess = "Thêm mới đối tác thành công";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                        AddLogAction(result, Constant.ActionInsertOK.ToString());
                                        Response.Redirect("/CatalogSystem/Partner", false);
                                    }
                                    else
                                    {
                                        ViewBag.TitleSuccsess = "Thêm mới đối tác không thành công";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            _PARTNER.PARTNER_CREATE_DATE = DateTime.Now;
                            _PARTNER.PARTNER_CREATE = UserInfo.uid;
                            result = impCMS_PARTNER.InsertCMS_PARTNER(_PARTNER);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới đối tác thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                Response.Redirect("/CatalogSystem/Partner", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới đối tác không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
                            }
                        }
                    }
                    else
                    {

                        result = impCMS_PARTNER.UpdateCMS_PARTNER(_PARTNER);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin đối tác thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/CatalogSystem/Partner", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin đối tác không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("partnerAdd:" + ex.ToString());
            }
            return View(_PARTNER);
        }

        #endregion


        #region --> Danh sách dịch vụ

        public ActionResult ListService(string id)
        {
            // Initialization.  
            AddPageHeader("Danh sách dịch vụ", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách dịch vụ", "/CatalogSystem/ListService");


            var UserInfo = ((cms_Account)Session["UserInfo"]);
            CMS_SERVICE _SERVICE = new CMS_SERVICE();
            try
            {
                List<CMS_SERVICE> _SERVICES = impCMS_SERVICE.GetAllCMS_SERVICE(0, UserInfo.PARTNERID);
                ViewBag.SERVICES = _SERVICES;
                if (string.IsNullOrEmpty(id))
                {
                    _SERVICE.SERVICE_UNIT = 0;
                    if (UserInfo.PARTNERID == 1)
                    {
                        _SERVICE.PARTNERID = 0;
                    }
                    else
                    {
                        _SERVICE.PARTNERID = UserInfo.PARTNERID;
                    }
                }
                else
                {
                    _SERVICES = impCMS_SERVICE.GetCMS_SERVICEByID(Convert.ToInt32(id), UserInfo.PARTNERID);
                    if (_SERVICES != null)
                    {
                        if (_SERVICES.Count > 0)
                        {
                            return View(_SERVICES[0]);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin dịch vụ";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin dịch vụ";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;

                logError.Info("ListService:" + ex.ToString());
            }
            // Info.  
            return View(_SERVICE);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListService(CMS_SERVICE obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách dịch vụ", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách dịch vụ", "/CatalogSystem/ListService");
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {

                switch (submit)
                {
                    case "SaveListService":
                        var errors = obj.Validate(new ValidationContext(obj));
                        if (errors.ToList().Count == 0)
                        {

                            if (obj.ID == 0)
                            {
                                bool checkExit = false;
                                // check tên dịch vụ
                                List<CMS_SERVICE> _SERVICESBYNAME = impCMS_SERVICE.GetCMS_SERVICEByServiceName(obj.SERVICE_NAME, UserInfo.PARTNERID);
                                if (_SERVICESBYNAME != null)
                                {
                                    if (_SERVICESBYNAME.Count > 0)
                                    {
                                        ViewBag.TitleSuccsess = "Đã tồn tại tên dịch vụ rồi. Mời bạn kiểm tra lại ";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                        checkExit = true;
                                    }
                                }

                                // check mã dịch vụ
                                _SERVICESBYNAME = impCMS_SERVICE.GetCMS_SERVICEByServiceCode(obj.SERVICE_CODE, UserInfo.PARTNERID);
                                if (_SERVICESBYNAME != null)
                                {
                                    if (_SERVICESBYNAME.Count > 0)
                                    {
                                        ViewBag.TitleSuccsess = "Đã tồn tại mã dịch vụ rồi. Mời bạn kiểm tra lại ";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                        checkExit = true;
                                    }
                                }


                                if (!checkExit)
                                {
                                    obj.SERVICE_CREATEBY = UserInfo.uid;
                                    obj.SERVICE_CREATEDATE = DateTime.Now;
                                    obj.SERVICE_STATUS = Common.Constant.TypeStatusPending;
                                    obj.PARTNERID = UserInfo.PARTNERID;

                                    string result = impCMS_SERVICE.InsertCMS_SERVICE(obj);
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        ViewBag.TitleSuccsess = "Thêm mới dịch vụ thành công";
                                        AddLogAction(result, Constant.ActionInsertOK.ToString());
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;

                                    }
                                    else
                                    {
                                        ViewBag.TitleSuccsess = "Thêm mới dịch vụ không thành công";
                                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    }
                                }
                            }
                            else
                            {
                                obj.SERVICE_UPDATEBY = UserInfo.uid;
                                obj.SERVICE_UPDATEDATE = DateTime.Now;
                                string result = impCMS_SERVICE.UpdateCMS_SERVICE(obj);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật dịch vụ thành công";
                                    AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                    //Clear the Model.
                                    ModelState.Clear();
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật dịch vụ không thành công";
                                    AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = errors.ToList()[0].ErrorMessage;
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                        break;

                        //case "SearchListService":
                        //    List<CMS_SERVICE> _SERVICES = impCMS_SERVICE.GetAllCMS_SERVICE_BYSERVICETYPE(0, UserInfo.PARTNERID, Convert.ToInt32(obj.SERVICETYPE));
                        //    ViewBag.SERVICES = _SERVICES;
                        //    break;
                }

                List<CMS_SERVICE> _SERVICES = impCMS_SERVICE.GetAllCMS_SERVICE_BYSERVICETYPE(0, UserInfo.PARTNERID, Convert.ToInt32(obj.SERVICETYPE));
                ViewBag.SERVICES = _SERVICES;
                //List<CMS_SERVICE> _SERVICES = impCMS_SERVICE.GetAllCMS_SERVICE(obj.PARTNERID, UserInfo.PARTNERID);
                //ViewBag.SERVICES = _SERVICES;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListService:" + ex.ToString());
            }

            int PARTNERID = 0;
            PARTNERID = obj.PARTNERID;

            obj = new CMS_SERVICE();
            //Clear the Model.
            ModelState.Clear();
            obj.PARTNERID = PARTNERID;

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListServiceDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách dịch vụ", "/CatalogSystem/ListService");
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
                        result = impCMS_SERVICE.DeleteCMS_SERVICE(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("ListServiceDelete: " + ex.ToString());
                return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
            }

            if (listIDError.Length > 0)
            {
                return Json("Xóa thành công bản ghi có id là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do bản ghi đã được chọn rồi!");
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
        public ActionResult ListServicePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách dịch vụ", "/CatalogSystem/ListService");
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
                        impCMS_SERVICE.PublicCMS_SERVICE(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("ListServicePublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }

        #endregion

        #region --> Danh sách nhóm dịch vụ
        public ActionResult ListGroupService(string id)
        {
            // Initialization.  
            AddPageHeader("Danh sách nhóm dịch vụ", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách nhóm dịch vụ", "/CatalogSystem/ListGroupService");


            var UserInfo = ((cms_Account)Session["UserInfo"]);
            CMS_SERVICE_GROUP _SERVICE_GROUP = new CMS_SERVICE_GROUP();

            try
            {
                List<CMS_SERVICE_GROUP> _SERVICE_GROUPS = impCMS_SERVICE_GROUP.GetAllCMS_SERVICE_GROUP(0, UserInfo.PARTNERID);
                ViewBag.SERVICE_GROUPS = _SERVICE_GROUPS;
                if (string.IsNullOrEmpty(id))
                {
                    _SERVICE_GROUP.PARTNERID = UserInfo.PARTNERID;
                }
                else
                {
                    _SERVICE_GROUPS = impCMS_SERVICE_GROUP.GetCMS_SERVICE_GROUPByID(Convert.ToInt32(id), UserInfo.PARTNERID);
                    if (_SERVICE_GROUPS != null)
                    {
                        if (_SERVICE_GROUPS.Count > 0)
                        {
                            return View(_SERVICE_GROUPS[0]);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin nhóm dịch vụ";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin nhóm dịch vụ";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListGroupService:" + ex.ToString());
            }



            // Info.  
            return View(_SERVICE_GROUP);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListGroupService(CMS_SERVICE_GROUP obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách nhóm dịch vụ", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách nhóm dịch vụ", "/CatalogSystem/ListGroupService");


            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {

                switch (submit)
                {
                    case "SaveListGroupService":
                        var errors = obj.Validate(new ValidationContext(obj));
                        if (errors.ToList().Count == 0)
                        {

                            if (obj.ID == 0)
                            {
                                bool checkExit = false;
                                // check tên nhóm dịch vụ
                                List<CMS_SERVICE_GROUP> _SERVICE_GROUPSBYNAME = impCMS_SERVICE_GROUP.GetCMS_SERVICE_GROUPBySERVICE_GROUP_NAME(obj.SERVICE_GROUP_NAME, UserInfo.PARTNERID);
                                if (_SERVICE_GROUPSBYNAME != null)
                                {
                                    if (_SERVICE_GROUPSBYNAME.Count > 0)
                                    {
                                        checkExit = true;
                                        ViewBag.TitleSuccsess = "Đã tồn tại nhóm dịch vụ rồi, Mời bạn xem lại!";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    }
                                }
                                // check mã nhóm dịch vụ
                                _SERVICE_GROUPSBYNAME = impCMS_SERVICE_GROUP.GetCMS_SERVICE_GROUPBySERVICE_GROUP_CODE(obj.SERVICE_GROUP_CODE, UserInfo.PARTNERID);
                                if (_SERVICE_GROUPSBYNAME.Count > 0)
                                {
                                    checkExit = true;
                                    ViewBag.TitleSuccsess = "Đã tồn tại mã nhóm dịch vụ rồi, Mời bạn xem lại!";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }

                                if (!checkExit)
                                {
                                    obj.SERVICE_GROUP_CREATEBY = UserInfo.uid;
                                    obj.SERVICE_GROUP_DATE = DateTime.Now;
                                    obj.SERVICE_GROUP_STATUS = Common.Constant.TypeStatusPending;
                                    obj.PARTNERID = UserInfo.PARTNERID;

                                    string result = impCMS_SERVICE_GROUP.InsertCMS_SERVICE_GROUP(obj);
                                    if (!string.IsNullOrEmpty(result))
                                    {
                                        ViewBag.TitleSuccsess = "Thêm mới nhóm dịch vụ thành công";
                                        AddLogAction(result, Constant.ActionInsertOK.ToString());
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                    }
                                    else
                                    {
                                        ViewBag.TitleSuccsess = "Thêm mới nhóm dịch vụ không thành công";
                                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    }
                                }
                            }
                            else
                            {
                                obj.SERVICE_GROUP_UPDATEBY = UserInfo.uid;
                                obj.SERVICE_GROUP_UPDATEDATE = DateTime.Now;
                                string result = impCMS_SERVICE_GROUP.UpdateCMS_SERVICE_GROUP(obj);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật nhóm dịch vụ thành công";
                                    AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật nhóm dịch vụ không thành công";
                                    AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = errors.ToList()[0].ErrorMessage;
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                        break;
                }
                List<CMS_SERVICE_GROUP> _SERVICE_GROUPS = impCMS_SERVICE_GROUP.GetAllCMS_SERVICE_GROUP(obj.PARTNERID, UserInfo.PARTNERID);
                ViewBag.SERVICE_GROUPS = _SERVICE_GROUPS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListGroupService: " + ex.ToString());
            }

            int PARTNERID = 0;
            PARTNERID = obj.PARTNERID;

            obj = new CMS_SERVICE_GROUP();
            ModelState.Clear();
            obj.PARTNERID = PARTNERID;

            // Info.  
            return View(obj);
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListGroupServiceDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách nhóm dịch vụ", "/CatalogSystem/ListGroupService");
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
                        result = impCMS_SERVICE_GROUP.DeleteCMS_SERVICE_GROUP(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("ListGroupServiceDelete: " + ex.ToString());
                return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
            }

            if (listIDError.Length > 0)
            {
                return Json("Xóa thành công bản ghi có id là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do bản ghi đã được chọn rồi!");
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
        public ActionResult ListGroupServicePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách nhóm dịch vụ", "/CatalogSystem/ListGroupService");
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
                        impCMS_SERVICE_GROUP.PublicCMS_SERVICE_GROUP(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("ListGroupServicePublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }

        #endregion

        #region --> chọn dịch vụ cho nhóm

        public ActionResult ListGroupServiceAddService(string groupid)
        {
            // Initialization.  
            AddPageHeader("Danh sách nhóm dịch vụ", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách nhóm dịch vụ", "/CatalogSystem/ListGroupServiceAddService");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            CMS_Core.Models.ListServiceViewModel obj = new CMS_Core.Models.ListServiceViewModel();
            obj.Visit_ID = 0;
            try
            {
                List<CMS_SERVICE_GROUP> _SERVICE_GROUPS = impCMS_SERVICE_GROUP.GetCMS_SERVICE_GROUPByID(Convert.ToInt32(groupid), UserInfo.PARTNERID);
                if (_SERVICE_GROUPS != null)
                {
                    if (_SERVICE_GROUPS.Count > 0)
                    {
                        obj.GroupName = _SERVICE_GROUPS[0].SERVICE_GROUP_NAME;
                        // List<CMS_SERVICE> _SERVICES = impCMS_SERVICE.GetAllCMS_SERVICE_BYGROUPSEVICEID(Convert.ToInt32(groupid), UserInfo.PARTNERID);
                        obj.SERVICES = new List<CMS_SERVICE>();
                        obj.PARTNERID = UserInfo.PARTNERID;
                        obj.GroupID = Convert.ToInt32(groupid);

                        obj.SERVICESBYGROUP = impCMS_SERVICE.GetAllCMS_SERVICE_CHECKBYGROUPSEVICEID(Convert.ToInt32(groupid), UserInfo.PARTNERID);


                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tim thấy thông tin nhóm dịch vụ. Mời bạn kiểm tra lại!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Không tim thấy thông tin nhóm dịch vụ. Mời bạn kiểm tra lại!";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }


            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListGroupServiceAddService: " + ex.ToString());
            }



            // Info.  
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListGroupServiceAddService(CMS_Core.Models.ListServiceViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách nhóm dịch vụ", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách nhóm dịch vụ", "/CatalogSystem/ListGroupServiceAddService");


            var UserInfo = ((cms_Account)Session["UserInfo"]);

            // this.ViewBag.Getcms_Permissiont = Common.Common.Getcms_Permissiont();
            // Impcms_Menu impcms_Menu = new Impcms_Menu();

            //String path = ControllerContext.HttpContext.Server.MapPath(@"~/Uploads/Patients/");

            //path = path + DateTime.Now.ToString("yyyyMMddHHmm") + "_" + CMSLIS.Common.Common.getRandom() + "_" + CMSLIS.Common.Common.getNiceUrl("abcx") + ".jpg";


            //MessagingToolkit.QRCode.Codec.QRCodeEncoder qe = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
            //System.Drawing.Bitmap bm = qe.Encode("http://www.yourdomain.com/product.aspx?code=");

            //bm.Save(path, System.Drawing.Imaging.ImageFormat.Gif);



            //  bm.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);

            // Impcms_Permission impcms_Permission = new Impcms_Permission();
            try
            {
                switch (submit)
                {

                    case "SaveListGroupServiceAddService":
                        if (obj.mulSERVICEID != null)
                        {
                            string listServiceID = string.Empty;
                            string listServiceIDDelete = string.Empty;
                            foreach (var data in obj.mulSERVICEID)
                            {
                                listServiceID = listServiceID + data.ToString() + ",";
                            }

                            if (listServiceID.Length > 0)
                            {
                                //if (listServiceIDDelete.Length > 1)
                                //{
                                //    listServiceIDDelete = listServiceIDDelete.Substring(0, listServiceIDDelete.Length - 1);
                                //    impCMS_SERVICE_GROUP_CATE.DeleteCMS_SERVICE_GROUP_CATE(Convert.ToInt32(obj.GroupID), listServiceIDDelete, UserInfo.PARTNERID);
                                //}

                                if (listServiceID.Length > 1)
                                {
                                    listServiceID = listServiceID.Substring(0, listServiceID.Length - 1);
                                    impCMS_SERVICE_GROUP_CATE.InsertListCMS_SERVICE_GROUP_CATE(Convert.ToInt32(obj.GroupID), listServiceID, obj.Visit_ID, UserInfo.PARTNERID);
                                }

                                ViewBag.TitleSuccsess = "Cập nhật thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            }

                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Bạn chưa chọn dịch vụ! Mời bạn chọn!";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                        break;

                    case "DeleteListGroupServiceAddService":
                        if (obj.SERVICESBYGROUP != null)
                        {
                            string listServiceID = string.Empty;
                            string listServiceIDDelete = string.Empty;
                            foreach (var data in obj.SERVICESBYGROUP)
                            {
                                if (!data.view)
                                {
                                    listServiceIDDelete = listServiceIDDelete + data.ID.ToString() + ",";
                                }
                            }

                            if (listServiceIDDelete.Length > 0)
                            {
                                if (listServiceIDDelete.Length > 1)
                                {
                                    listServiceIDDelete = listServiceIDDelete.Substring(0, listServiceIDDelete.Length - 1);
                                    impCMS_SERVICE_GROUP_CATE.DeleteCMS_SERVICE_GROUP_CATE(Convert.ToInt32(obj.GroupID), listServiceIDDelete, UserInfo.PARTNERID);
                                }

                                ViewBag.TitleSuccsess = "Cập nhật thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            }

                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Bạn chưa chọn dịch vụ! Mời bạn chọn!";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListGroupService: " + ex.ToString());

            }
            //   List<CMS_SERVICE> _SERVICES = impCMS_SERVICE.GetAllCMS_SERVICE_BYGROUPSEVICEID(obj.GroupID, UserInfo.PARTNERID);
            obj.SERVICES = new List<CMS_SERVICE>();
            obj.PARTNERID = UserInfo.PARTNERID;
            obj.GroupID = obj.GroupID;

            obj.SERVICESBYGROUP = impCMS_SERVICE.GetAllCMS_SERVICE_CHECKBYGROUPSEVICEID(obj.GroupID, UserInfo.PARTNERID);


            // Info.  
            return View(obj);
        }





        #endregion


        #region --> Danh sách bệnh

        public ActionResult ListSick()
        {
            // Initialization.  
            AddPageHeader("Danh sách các bệnh", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách các bệnh", "/CatalogSystem/ListSick");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {
                List<CMS_SICK> _CMS_SICKS = impCMS_SICK.GetAll(UserInfo.PARTNERID);
                ViewBag.CMS_SICKS = _CMS_SICKS;
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordSick();
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListSick:" + ex.ToString());
            }
            SearchMenuViewModel obj = new SearchMenuViewModel();
            obj.parentID = "0";
            obj.TypeKeyword = 0;
            // Info.  
            return View(obj);
        }

        [HttpPost]
        public ActionResult ListSick(SearchMenuViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách các bệnh", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách các bệnh", "/CatalogSystem/ListSick");


            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordSick();
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);

                switch (submit)
                {
                    case "SearchListSick":
                        List<CMS_SICK> _CMS_SICKS = impCMS_SICK.GetAll_CMS_SICK_BYSTATUS(Convert.ToInt32(obj.Status), obj.TypeKeyword, obj.keyword, UserInfo.PARTNERID);
                        ViewBag.CMS_SICKS = _CMS_SICKS;

                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListSick:" + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListSickDelete(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Danh sách các bệnh", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách các bệnh", "/CatalogSystem/ListSick");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            string listID = string.Empty;
            string listIDError = string.Empty;
            string result = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        result = impCMS_SICK.Delete(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("partnerDelete: " + ex.ToString());
                return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
            }

            if (listIDError.Length > 0)
            {
                return Json("Xóa thành công bản ghi có id là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do bản ghi đã được chọn rồi!");
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
        public ActionResult ListSickPublic(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Danh sách các bệnh", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách các bệnh", "/CatalogSystem/ListSick");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impCMS_SICK.Publish(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("partnerPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListSickAdd(string ID)
        {
            // Initialization.    

            AddBreadcrumb("Hệ thống danh mục", "");

            CMS_SICK _SICK = new CMS_SICK();
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    AddPageHeader("Thêm mới danh mục bệnh", "");
                    AddBreadcrumb("Thêm mới danh mục bệnh", "/CatalogSystem/ListSickAdd");
                    ViewBag.panelTitle = "Thêm mới danh mục bệnh";
                }
                else
                {
                    AddPageHeader("Cập nhật danh mục bệnh", "");
                    AddBreadcrumb("Cập nhật danh mục bệnh", "/CatalogSystem/ListSickAdd");
                    ViewBag.panelTitle = "Cập nhật danh mục bệnh";
                    _SICK = impCMS_SICK.Get(Convert.ToInt32(ID), UserInfo.PARTNERID);
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListSickAdd:" + ex.ToString());
            }

            // Info.  
            return View(_SICK);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListSickAdd(CMS_SICK _SICK)
        {
            AddBreadcrumb("Hệ thống danh mục", "");

            try
            {
                if (_SICK.ID == 0)
                {
                    AddPageHeader("Thêm mới danh mục bệnh", "");
                    AddBreadcrumb("Thêm mới danh mục bệnh", "/CatalogSystem/ListSickAdd");
                    ViewBag.panelTitle = "Thêm mới danh mục bệnh";
                }
                else
                {
                    AddPageHeader("Cập nhật danh mục bệnh", "");
                    AddBreadcrumb("Cập nhật danh mục bệnh", "/CatalogSystem/ListSickAdd");
                    ViewBag.panelTitle = "Cập nhật danh mục bệnh";
                }

                if (ModelState.IsValid)
                {
                    var UserInfo = ((cms_Account)Session["UserInfo"]);
                    string result = string.Empty;
                    if (_SICK.ID == 0)
                    {
                        // check partner code



                        bool check = true;
                        List<CMS_SICK> _CMS_SICKS = impCMS_SICK.GetAll_CMS_SICK_BYNAME(_SICK.SICK_NAME, UserInfo.PARTNERID);
                        if (_CMS_SICKS != null)
                        {
                            if (_CMS_SICKS.Count > 0)
                            {
                                ViewBag.TitleSuccsess = "Đã tồn tại tên bệnh rồi, Mời bạn nhập tên bệnh khác!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                check = false;
                            }
                        }

                        if (check)
                        {
                            _SICK.CREATEDATE = DateTime.Now;
                            _SICK.CREATEBY = UserInfo.uid;
                            _SICK.PARTNERID = UserInfo.PARTNERID;
                            result = impCMS_SICK.Add(_SICK, UserInfo.PARTNERID);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới danh mục bệnh thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                Response.Redirect("/CatalogSystem/ListSick", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới danh mục bệnh không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
                            }
                        }

                    }


                    else
                    {
                        _SICK.UPDATEBY = UserInfo.uid;
                        _SICK.UPDATEDATE = DateTime.Now;
                        result = impCMS_SICK.Update(_SICK, UserInfo.PARTNERID);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin danh mục bệnh thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/CatalogSystem/ListSick", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin danh mục bệnh không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListSickAdd:" + ex.ToString());
            }
            return View(_SICK);
        }

        #endregion



        #region --> Danh mẫu template nhập kết quả

        public ActionResult ListTemplateResultExamination()
        {
            // Initialization.  
            AddPageHeader("Danh sách mẫu nhật kết quả", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách mẫu nhật kết quả", "/CatalogSystem/ListTemplateResultExamination");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {


                List<CMS_TEMPLATERESULTEXAMINATION> _TEMPLATERESULTEXAMINATIONS = impCMS_TEMPLATERESULTEXAMINATION.GetAll(UserInfo.PARTNERID);
                ViewBag.TEMPLATERESULTEXAMINATIONS = _TEMPLATERESULTEXAMINATIONS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("partner:" + ex.ToString());
            }
            SearchMenuViewModel obj = new SearchMenuViewModel();
            obj.parentID = "0";
            // Info.  
            return View(obj);
        }

        [HttpPost]

        public ActionResult ListTemplateResultExamination(SearchMenuViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách mẫu nhật kết quả", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách mẫu nhật kết quả", "/CatalogSystem/ListTemplateResultExamination");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                switch (submit)
                {
                    case "SearchListTemplateResultExamination":
                        List<CMS_TEMPLATERESULTEXAMINATION> _TEMPLATERESULTEXAMINATIONS = impCMS_TEMPLATERESULTEXAMINATION.GetCMS_TEMPLATERESULTEXAMINATION_BYSTATUS(Convert.ToInt32(obj.Status), UserInfo.PARTNERID);
                        ViewBag.TEMPLATERESULTEXAMINATIONS = _TEMPLATERESULTEXAMINATIONS;
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListTemplateResultExamination:" + ex.ToString());
            }

            // Info.  
            return View(obj);
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListTemplateResultExaminationDelete(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Danh sách mẫu nhật kết quả", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách mẫu nhật kết quả", "/CatalogSystem/ListTemplateResultExamination");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            string listID = string.Empty;
            string listIDError = string.Empty;
            string result = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        result = impCMS_TEMPLATERESULTEXAMINATION.Delete(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("partnerDelete: " + ex.ToString());
                return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
            }

            if (listIDError.Length > 0)
            {
                return Json("Xóa thành công bản ghi có id là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do bản ghi đã được chọn rồi!");
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
        public ActionResult ListTemplateResultExaminationPublic(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Danh sách các bệnh", "");
            AddBreadcrumb("Hệ thống danh mục", "");
            AddBreadcrumb("Danh sách các bệnh", "/CatalogSystem/ListTemplateResultExaminationPublic");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impCMS_TEMPLATERESULTEXAMINATION.Publish(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("partnerPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListTemplateResultExaminationAdd(string ID)
        {
            // Initialization.    

            AddBreadcrumb("Hệ thống danh mục", "");

            CMS_TEMPLATERESULTEXAMINATION _TEMPLATERESULTEXAMINATION = new CMS_TEMPLATERESULTEXAMINATION();

            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    AddPageHeader("Thêm mới danh mẫu kết quả", "");
                    AddBreadcrumb("Thêm mới danh mẫu kết quả", "/CatalogSystem/ListTemplateResultExaminationAdd");
                    ViewBag.panelTitle = "Thêm mới danh mẫu kết quả";
                    _TEMPLATERESULTEXAMINATION.GROUP_SERVICEID = 0;
                    _TEMPLATERESULTEXAMINATION.SERVICEID = 0;
                }
                else
                {
                    AddPageHeader("Cập nhật danh mục mẫu kết quả", "");
                    AddBreadcrumb("Cập nhật danh mục mẫu kết quả", "/CatalogSystem/ListTemplateResultExaminationAdd");
                    ViewBag.panelTitle = "Cập nhật danh mục mẫu kết quả";
                    _TEMPLATERESULTEXAMINATION = impCMS_TEMPLATERESULTEXAMINATION.Get(Convert.ToInt32(ID), UserInfo.PARTNERID);
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListSickAdd:" + ex.ToString());
            }

            // Info.  
            return View(_TEMPLATERESULTEXAMINATION);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ListTemplateResultExaminationAdd(CMS_TEMPLATERESULTEXAMINATION _TEMPLATERESULTEXAMINATION)
        {
            AddBreadcrumb("Hệ thống danh mục", "");

            try
            {
                if (_TEMPLATERESULTEXAMINATION.ID == 0)
                {
                    AddPageHeader("Thêm mới danh mẫu kết quả", "");
                    AddBreadcrumb("Thêm mới danh mẫu kết quả", "/CatalogSystem/ListTemplateResultExaminationAdd");
                    ViewBag.panelTitle = "Thêm mới danh mẫu kết quả";
                }
                else
                {
                    AddPageHeader("Cập nhật danh mục mẫu kết quả", "");
                    AddBreadcrumb("Cập nhật danh mục mẫu kết quả", "/CatalogSystem/ListTemplateResultExaminationAdd");
                    ViewBag.panelTitle = "Cập nhật danh mục mẫu kết quả";
                }

                if (ModelState.IsValid)
                {
                    var UserInfo = ((cms_Account)Session["UserInfo"]);
                    string result = string.Empty;
                    if (_TEMPLATERESULTEXAMINATION.ID == 0)
                    {
                        // check partner code



                        bool check = true;
                        CMS_TEMPLATERESULTEXAMINATION TEMPLATERESULTEXAMINATION = impCMS_TEMPLATERESULTEXAMINATION.GetByName(_TEMPLATERESULTEXAMINATION.NAME, UserInfo.PARTNERID);
                        if (TEMPLATERESULTEXAMINATION != null)
                        {
                            if (string.IsNullOrEmpty(TEMPLATERESULTEXAMINATION.NAME))
                            {
                                ViewBag.TitleSuccsess = "Đã tồn tại tên mẫu, Mời bạn nhập tên khác!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                check = false;
                            }
                        }

                        if (check)
                        {
                            _TEMPLATERESULTEXAMINATION.CREATEDATE = DateTime.Now;
                            _TEMPLATERESULTEXAMINATION.CREATEBY = UserInfo.uid;
                            _TEMPLATERESULTEXAMINATION.PARTNERID = UserInfo.PARTNERID;
                            _TEMPLATERESULTEXAMINATION.STATUS = 1;
                            result = impCMS_TEMPLATERESULTEXAMINATION.Add(_TEMPLATERESULTEXAMINATION, UserInfo.PARTNERID);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới mẫu kết quả thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                Response.Redirect("/CatalogSystem/ListTemplateResultExamination", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới mẫu kết quả không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
                            }
                        }

                    }


                    else
                    {
                        _TEMPLATERESULTEXAMINATION.UPDATEBY = UserInfo.uid;
                        _TEMPLATERESULTEXAMINATION.UPDATEDATE = DateTime.Now;
                        result = impCMS_TEMPLATERESULTEXAMINATION.Update(_TEMPLATERESULTEXAMINATION, UserInfo.PARTNERID);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin mẫu kết quả thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/CatalogSystem/ListTemplateResultExamination", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin mẫu kết quả không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListTemplateResultExaminationAdd:" + ex.ToString());
            }
            return View(_TEMPLATERESULTEXAMINATION);
        }

        #endregion


        #region --> Danh sách khoa

        public ActionResult ListDepartment()
        {
            // Initialization.  
            AddPageHeader("Danh sách khoa", "");
            AddBreadcrumb("Danh sách khoa", "");
            AddBreadcrumb("Danh sách khoa", "/CatalogSystem/ListDepartment");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {
                List<CMS_DEPARTMENT> CMS_DEPARTMENTS = impCMS_DEPARTMENT.GetAll(UserInfo.PARTNERID);
                ViewBag.CMS_DEPARTMENTS = CMS_DEPARTMENTS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("partner:" + ex.ToString());
            }
            SearchMenuViewModel obj = new SearchMenuViewModel();
            obj.parentID = "0";
            // Info.  
            return View(obj);
        }

        [HttpPost]
        public ActionResult ListDepartment(SearchMenuViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách khoa", "");
            AddBreadcrumb("Danh sách khoa", "");
            AddBreadcrumb("Danh sách khoa", "/CatalogSystem/ListDepartment");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                switch (submit)
                {
                    case "SearchListDepartment":
                        List<CMS_DEPARTMENT> CMS_DEPARTMENTS = impCMS_DEPARTMENT.GetCMS_DEPARTMENT_BYSTATUS(Convert.ToInt32(obj.Status), UserInfo.PARTNERID);
                        ViewBag.CMS_DEPARTMENTS = CMS_DEPARTMENTS;
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListDepartment:" + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListDepartmentDelete(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Danh sách khoa khám", "");
            AddBreadcrumb("Danh sách khoa khám", "");
            AddBreadcrumb("Danh sách khoa khám", "/CatalogSystem/ListDepartmentDelete");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            string listID = string.Empty;
            string listIDError = string.Empty;
            string result = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        result = impCMS_DEPARTMENT.Delete(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("partnerDelete: " + ex.ToString());
                return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
            }

            if (listIDError.Length > 0)
            {
                return Json("Xóa thành công bản ghi có id là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do bản ghi đã được chọn rồi!");
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
        public ActionResult ListDepartmentPublic(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Danh sách khoa khám", "");
            AddBreadcrumb("Danh sách khoa khám", "");
            AddBreadcrumb("Danh sách khoa khám", "/CatalogSystem/ListDepartmentPublic");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impCMS_DEPARTMENT.Publish(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("partnerPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListDepartmentAdd(string ID)
        {
            // Initialization.    

            AddBreadcrumb("Hệ thống danh mục", "");

            CMS_DEPARTMENT _DEPARTMENT = new CMS_DEPARTMENT();

            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    AddPageHeader("Thêm mới khoa khám", "");
                    AddBreadcrumb("Thêm mới khoa khám", "/CatalogSystem/ListDepartmentAdd");
                    ViewBag.panelTitle = "Thêm mới khoa khám";
                }
                else
                {
                    AddPageHeader("Cập nhật danh mục khoa khám", "");
                    AddBreadcrumb("Cập nhật danh khoa khám", "/CatalogSystem/ListDepartmentAdd");
                    ViewBag.panelTitle = "Cập nhật danh mục khoa khám";
                    _DEPARTMENT = impCMS_DEPARTMENT.Get(Convert.ToInt32(ID), UserInfo.PARTNERID);
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListDepartmentAdd:" + ex.ToString());
            }

            // Info.  
            return View(_DEPARTMENT);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListDepartmentAdd(CMS_DEPARTMENT _DEPARTMENT)
        {
            AddBreadcrumb("Hệ thống danh mục", "");

            try
            {
                if (_DEPARTMENT.ID == 0)
                {
                    AddPageHeader("Thêm mới khoa khám", "");
                    AddBreadcrumb("Thêm mới khoa khám", "/CatalogSystem/ListDepartmentAdd");
                    ViewBag.panelTitle = "Thêm mới khoa khám";
                }
                else
                {
                    AddPageHeader("Cập nhật danh mục khoa khám", "");
                    AddBreadcrumb("Cập nhật danh khoa khám", "/CatalogSystem/ListDepartmentAdd");
                    ViewBag.panelTitle = "Cập nhật danh mục khoa khám";
                }

                if (ModelState.IsValid)
                {
                    var UserInfo = ((cms_Account)Session["UserInfo"]);
                    string result = string.Empty;
                    if (_DEPARTMENT.ID == 0)
                    {
                        // check partner code
                        bool check = true;
                        CMS_DEPARTMENT DEPARTMENT = impCMS_DEPARTMENT.GetName(_DEPARTMENT.DEPARTMENT_NAME, UserInfo.PARTNERID);
                        if (DEPARTMENT != null)
                        {
                            if (string.IsNullOrEmpty(DEPARTMENT.DEPARTMENT_NAME))
                            {
                                ViewBag.TitleSuccsess = "Đã tồn tại tên khoa rồi, Mời bạn nhập tên khác!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                check = false;
                            }
                        }

                        if (check)
                        {
                            _DEPARTMENT.CREATEDATE = DateTime.Now;
                            _DEPARTMENT.CREATEBY = UserInfo.uid;
                            _DEPARTMENT.PARTNERID = UserInfo.PARTNERID;
                            _DEPARTMENT.DEPARTMENT_STATUS = 1;
                            result = impCMS_DEPARTMENT.Add(_DEPARTMENT, UserInfo.PARTNERID);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới khoa khám thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                Response.Redirect("/CatalogSystem/ListDepartment", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới  khoa khám không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
                            }
                        }

                    }


                    else
                    {
                        _DEPARTMENT.UPDATEBY = UserInfo.uid;
                        _DEPARTMENT.UPDATEDATE = DateTime.Now;
                        result = impCMS_DEPARTMENT.Update(_DEPARTMENT, UserInfo.PARTNERID);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin khoa khám thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/CatalogSystem/ListDepartment", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin khoa khám không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListDepartmentAdd:" + ex.ToString());
            }
            return View(_DEPARTMENT);
        }

        #endregion

        #region --> Danh sách phòng khám  

        public ActionResult ListRoomClinic()
        {
            // Initialization.  
            AddPageHeader("Danh sách phòng khám", "");
            AddBreadcrumb("Danh sách phòng khám", "");
            AddBreadcrumb("Danh sách phòng khám", "/CatalogSystem/ListRoomClinic");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {
                List<CMS_ROOM_CLINIC> _ROOM_CLINICS = impCMS_ROOM_CLINIC.GetAll(UserInfo.PARTNERID);
                ViewBag.ROOM_CLINICS = _ROOM_CLINICS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("partner:" + ex.ToString());
            }
            SearchMenuViewModel obj = new SearchMenuViewModel();
            obj.parentID = "0";
            // Info.  
            return View(obj);
        }

        [HttpPost]
        public ActionResult ListRoomClinic(SearchMenuViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách phòng khám", "");
            AddBreadcrumb("Danh sách phòng khám", "");
            AddBreadcrumb("Danh sách phòng khám", "/CatalogSystem/ListRoomClinic");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                switch (submit)
                {
                    case "SearchListRoomClinic":
                        List<CMS_ROOM_CLINIC> _ROOM_CLINICS = impCMS_ROOM_CLINIC.GetCMS_ROOM_CLINIC_BYSTATUS(Convert.ToInt32(obj.Status), Convert.ToInt32(obj.parentID), UserInfo.PARTNERID);
                        ViewBag.ROOM_CLINICS = _ROOM_CLINICS;
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("Menu:" + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListRoomClinicDelete(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Danh sách phòng khám", "");
            AddBreadcrumb("Danh sách phòng khám", "");
            AddBreadcrumb("Danh sách phòng khám", "/CatalogSystem/ListRoomClinicDelete");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            string listID = string.Empty;
            string listIDError = string.Empty;
            string result = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        result = impCMS_ROOM_CLINIC.Delete(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("partnerDelete: " + ex.ToString());
                return Json("Có lỗi trong quá trình xóa bản ghi. Mời bạn thực hiện lại!");
            }

            if (listIDError.Length > 0)
            {
                return Json("Xóa thành công bản ghi có id là: " + listID + ", Các bản ghi lỗi: " + listIDError + " do bản ghi đã được chọn rồi!");
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
        public ActionResult ListRoomClinicPublic(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Danh sách phòng khám", "");
            AddBreadcrumb("Danh sách phòng khám", "");
            AddBreadcrumb("Danh sách phòng khám", "/CatalogSystem/ListRoomClinicPublic");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impCMS_ROOM_CLINIC.Publish(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("partnerPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListRoomClinicAdd(string ID)
        {
            // Initialization.    

            AddBreadcrumb("Hệ thống danh mục", "");
            CMS_ROOM_CLINIC _ROOM_CLINIC = new CMS_ROOM_CLINIC();
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    AddPageHeader("Thêm mới phòng khám", "");
                    AddBreadcrumb("Thêm mới phòng khám", "/CatalogSystem/ListRoomClinicAdd");
                    ViewBag.panelTitle = "Thêm mới khoa khám";
                }
                else
                {
                    AddPageHeader("Cập nhật danh mục phòng khám", "");
                    AddBreadcrumb("Cập nhật danh phòng khám", "/CatalogSystem/ListRoomClinicAdd");
                    ViewBag.panelTitle = "Cập nhật danh mục phòng khám";
                    _ROOM_CLINIC = impCMS_ROOM_CLINIC.Get(Convert.ToInt32(ID), UserInfo.PARTNERID);
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListRoomClinicAdd:" + ex.ToString());
            }

            // Info.  
            return View(_ROOM_CLINIC);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListRoomClinicAdd(CMS_ROOM_CLINIC _ROOM_CLINIC)
        {
            AddBreadcrumb("Hệ thống danh mục", "");

            try
            {
                if (_ROOM_CLINIC.ID == 0)
                {
                    AddPageHeader("Thêm mới phòng khám", "");
                    AddBreadcrumb("Thêm mới phòng khám", "/CatalogSystem/ListRoomClinicAdd");
                    ViewBag.panelTitle = "Thêm mới khoa khám";
                }
                else
                {
                    AddPageHeader("Cập nhật danh mục phòng khám", "");
                    AddBreadcrumb("Cập nhật danh phòng khám", "/CatalogSystem/ListRoomClinicAdd");
                    ViewBag.panelTitle = "Cập nhật danh mục phòng khám";
                }

                if (ModelState.IsValid)
                {
                    var UserInfo = ((cms_Account)Session["UserInfo"]);
                    string result = string.Empty;
                    if (_ROOM_CLINIC.ID == 0)
                    {
                        // check partner code
                        bool check = true;
                        CMS_ROOM_CLINIC ROOM_CLINIC = impCMS_ROOM_CLINIC.GetName(_ROOM_CLINIC.ROOM_NAME, UserInfo.PARTNERID);
                        if (ROOM_CLINIC != null)
                        {
                            if (string.IsNullOrEmpty(ROOM_CLINIC.ROOM_NAME))
                            {
                                ViewBag.TitleSuccsess = "Đã tồn tại tên phòng khám rồi, Mời bạn nhập tên khác!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                check = false;
                            }
                        }

                        if (check)
                        {
                            _ROOM_CLINIC.CREATEDATE = DateTime.Now;
                            _ROOM_CLINIC.CREATEBY = UserInfo.uid;
                            _ROOM_CLINIC.PARTNERID = UserInfo.PARTNERID;
                            _ROOM_CLINIC.STATUS = 1;
                            result = impCMS_ROOM_CLINIC.Add(_ROOM_CLINIC, UserInfo.PARTNERID);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới phòng khám thành công";
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                Response.Redirect("/CatalogSystem/ListRoomClinic", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới  phòng khám không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
                            }
                        }

                    }


                    else
                    {
                        _ROOM_CLINIC.UPDATEBY = UserInfo.uid;
                        _ROOM_CLINIC.UPDATEDATE = DateTime.Now;
                        result = impCMS_ROOM_CLINIC.Update(_ROOM_CLINIC, UserInfo.PARTNERID);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin phòng khám thành công";
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            Response.Redirect("/CatalogSystem/ListRoomClinic", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin phòng khám không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListRoomClinicAdd:" + ex.ToString());
            }
            return View(_ROOM_CLINIC);
        }

        #endregion




        #region --> Danh sách Chuyên mục     
        public ActionResult Category()
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/Category");
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {

                List<cms_Category> _Categories = new List<cms_Category>();


                //CMS_Core.Common.MemcacheConnection mencache = new CMS_Core.Common.MemcacheConnection();
                // _Categories = (List<cms_Category>)mencache.getMemcacheByKey("Getcms_CategoryParrent");

                //if (_Categories == null)
                //{
                _Categories = impcms_Category.GetAllcms_Category();




                //    mencache.setMemcacheMinutesByKey("Getcms_CategoryParrent",_Categories);
                //}
                ViewBag.DataReport = _Categories;
            }
            catch (Exception ex)
            {
                logError.Info("Category:" + ex.ToString());
            }
            SearchCategoryViewModel obj = new SearchCategoryViewModel();
            obj.parentID = "0";
            // Info.  
            return View(obj);
        }

        [HttpPost]
        public ActionResult Category(SearchCategoryViewModel obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/Category");
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {
                switch (submit)
                {
                    case "SearchCategory":
                        List<cms_Category> _Categories = impcms_Category.GetAllcms_CategoryByStatus(Convert.ToInt32(obj.parentID), Convert.ToInt32(obj.Status));

                        ViewBag.DataReport = _Categories;
                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("Category:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        public ActionResult CategoryDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/Category");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    List<cms_Category> _Categories = null;

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Category.Deletecms_Category(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    // _Categories = impcms_Category.GetAllcms_Category();
                    ViewBag.DataReport = _Categories;
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("CategoryDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        public ActionResult CategoryPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/Category");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    List<cms_Category> _Categories = null;

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Category.Publiccms_Category(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionPublicOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    // _Categories = impcms_Category.GetAllcms_Category();
                    ViewBag.DataReport = _Categories;
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để duyệt");
                }
            }
            catch (Exception ex)
            {
                logError.Info("MenuPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult CategoryAdd(string cateId)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới chuyên mục", "/CatalogSystem/CategoryAdd");
            cms_Category _Category = new cms_Category();
            List<cms_Category> _Categories = null;



            try
            {
                if (string.IsNullOrEmpty(cateId))
                {
                    ViewBag.panelTitle = "Thêm mới chuyên mục";
                    _Category.cateId = 0;
                    _Category.cateOrd = 1;
                    _Category.cateLevel = string.Empty;
                    this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
                }
                else
                {
                    ViewBag.panelTitle = "Cập nhật chuyên mục";
                    _Categories = impcms_Category.Getcms_CategoryByID(Convert.ToInt32(cateId));
                    if (_Categories != null)
                    {
                        this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent(_Categories[0].cateLevel);
                        if (_Categories.Count > 0)
                        {
                            return View(_Categories[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("CategoryAdd:" + ex.ToString());
            }

            // Info.  
            return View(_Category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryAdd(cms_Category _Category)
        {
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent(_Category.cateLevel);




            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(_Category.cateLevelParent))
                    _Category.cateLevelParent = string.Empty;



                string result = string.Empty;
                if (_Category.cateId == 0)
                {
                    List<cms_Category> _Categories = impcms_Category.Getcms_CategoryByCateName(_Category.cateName);
                    bool checkExit = false;
                    if (_Categories != null)
                    {
                        if (_Categories.Count > 0)
                        {
                            checkExit = true;
                        }
                    }

                    if (!checkExit)
                    {
                        result = impcms_Category.Insertcms_Category(_Category);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới chuyên mục thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới chuyên mục không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Đã tồn tại chuyên mục rồi, Mời bạn kiểm tra lại";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else
                {
                    result = impcms_Category.Updatecms_Category(_Category);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin chuyên mục thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin chuyên mục không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }
            }

            return View(_Category);
        }

        #endregion



        #region --> Danh sách nguồn tin

        public ActionResult Source()
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách nguồn tin", "/CatalogSystem/Source");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            try
            {
                List<cms_NewsSource> _NewsSources = impcms_NewsSource.GetAllcms_NewsSource();
                ViewBag.DataReport = _NewsSources;
            }
            catch (Exception ex)
            {
                logError.Info("Source: " + ex.ToString());
            }
            CMS_Core.Models.SearchNewsViewModel searchNewsView = new CMS_Core.Models.SearchNewsViewModel();
            // Info.  
            return View(searchNewsView);
        }



        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult SourceDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách nguồn tin", "/CatalogSystem/SourceDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    List<cms_NewsSource> _NewsSources = null;

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_NewsSource.Deletecms_NewsSource(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    _NewsSources = impcms_NewsSource.GetAllcms_NewsSource();
                    ViewBag.DataReport = _NewsSources;
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("CategoryDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }



        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult SourcePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách  ảnh", "/News/ListImagePublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_NewsSource.Publiccms_NewsSource(Convert.ToInt32(customerID));
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
                logError.Info("MenuPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }




        public ActionResult SourceAdd(string SourceId)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới nguồn tin", "/CatalogSystem/SourceAdd");
            cms_NewsSource _NewsSource = new cms_NewsSource();
            List<cms_NewsSource> _NewsSources = null;

            try
            {
                if (string.IsNullOrEmpty(SourceId))
                {
                    ViewBag.panelTitle = "Thêm mới nguồn tin";
                }
                else
                {
                    ViewBag.panelTitle = "Cập nhật nguồn tin";
                    _NewsSources = impcms_NewsSource.Getcms_NewsSourceByID(Convert.ToInt32(SourceId));
                    if (_NewsSources != null)
                    {
                        if (_NewsSources.Count > 0)
                        {
                            return View(_NewsSources[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("SourceAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_NewsSource);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SourceAdd(cms_NewsSource _NewsSource)
        {

            if (ModelState.IsValid)
            {

                string result = string.Empty;
                if (_NewsSource.SourceId == 0)
                {
                    result = impcms_NewsSource.Insertcms_NewsSource(_NewsSource);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Thêm mới nguồn tin thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionInsertOK.ToString());
                        Response.Redirect("/CatalogSystem/Source", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thêm mới nguồn tin không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                    }
                }
                else
                {
                    result = impcms_NewsSource.Updatecms_NewsSource(_NewsSource);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin nguồn tin thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                        Response.Redirect("/CatalogSystem/Source", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin nguồn tin không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }
            }

            return View(_NewsSource);
        }

        #endregion


        #region --> Danh sách kểu bài

        public ActionResult TypeMessage()
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách loại bài viết", "/CatalogSystem/TypeMessage");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {
                List<cms_NewsTypeMessage> NewsTypeMessage = impcms_NewsTypeMessage.GetAllcms_NewsTypeMessage();
                ViewBag.DataReport = NewsTypeMessage;
            }
            catch (Exception ex)
            {
                logError.Info("TypeMessage: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }
            // Info.  
            SearchCategoryViewModel searchCategoryViewModel = new SearchCategoryViewModel();
            searchCategoryViewModel.Status = Common.Constant.TypeStatusAll.ToString();
            return View(searchCategoryViewModel);
        }


        [HttpPost]
        public ActionResult TypeMessage(SearchCategoryViewModel obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách loại bài viết", "/CatalogSystem/TypeMessage");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {
                switch (submit)
                {
                    case "SearchTypeMessage":
                        List<cms_NewsTypeMessage> cms_NewsTypes = impcms_NewsTypeMessage.GetAllcms_NewsTypeMessageByStatus(Convert.ToInt32(obj.Status));
                        ViewBag.DataReport = cms_NewsTypes;
                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("TypeMessage: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(obj);
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult TypeMessageDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách loại bài viết", "/CatalogSystem/TypeMessage");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    List<cms_NewsTypeMessage> _NewsTypeMessages = null;

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_NewsTypeMessage.Deletecms_NewsTypeMessage(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    _NewsTypeMessages = impcms_NewsTypeMessage.GetAllcms_NewsTypeMessage();
                    ViewBag.DataReport = _NewsTypeMessages;
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("CategoryDelete: " + ex.ToString());

            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult TypeMessagePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách loại bài viết", "/CatalogSystem/TypeMessage");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    List<cms_NewsTypeMessage> _NewsTypeMessages = null;

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_NewsTypeMessage.Publiccms_NewsTypeMessage(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionPublicOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    _NewsTypeMessages = impcms_NewsTypeMessage.GetAllcms_NewsTypeMessage();
                    ViewBag.DataReport = _NewsTypeMessages;
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để duyệt");
                }
            }
            catch (Exception ex)
            {
                logError.Info("MenuPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult TypeMessageAdd(string TypeMessageId)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới kiểu bài", "/CatalogSystem/TypeMessageAdd");
            cms_NewsTypeMessage cms_NewsTypeMessage = new cms_NewsTypeMessage();


            List<cms_NewsTypeMessage> _NewsTypeMessages = null;

            try
            {
                if (string.IsNullOrEmpty(TypeMessageId))
                {
                    ViewBag.panelTitle = "Thêm mới nguồn tin";
                }
                else
                {
                    ViewBag.panelTitle = "Cập nhật nguồn tin";
                    _NewsTypeMessages = impcms_NewsTypeMessage.Getcms_NewsTypeMessageByID(Convert.ToInt32(TypeMessageId));
                    if (_NewsTypeMessages != null)
                    {
                        return View(_NewsTypeMessages[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("TypeMessageAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(cms_NewsTypeMessage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TypeMessageAdd(cms_NewsTypeMessage _NewsTypeMessage)
        {

            if (ModelState.IsValid)
            {
                string result = string.Empty;
                if (_NewsTypeMessage.TypeMessageId == 0)
                {
                    result = impcms_NewsTypeMessage.Insertcms_NewsTypeMessage(_NewsTypeMessage);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Thêm mới loại bài thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionInsertOK.ToString());
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thêm mới loại bài không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                    }
                }
                else
                {
                    result = impcms_NewsTypeMessage.Updatecms_NewsTypeMessage(_NewsTypeMessage);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin loại bài thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin loại bài không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }
            }

            return View(_NewsTypeMessage);
        }

        #endregion


        #region --> Danh sách Autolink

        public ActionResult Autolink()
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách AutoLink", "/CatalogSystem/Autolink");
            CMS_Core.Models.SearchNewsViewModel searchNewsView = new CMS_Core.Models.SearchNewsViewModel();

            try
            {
                searchNewsView.tungay = DateTime.Now.AddDays(-14).ToString("dd/MM/yyyy");
                searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
                searchNewsView.keyword = string.Empty;
                searchNewsView.ParrenId = 0;


                List<Autolink> autolink = impAutolink.GetAllAutolinkByKeyword(DateTime.Now.AddDays(-14).ToString("yyyyMMdd"), DateTime.Now.AddDays(1).ToString("yyyyMMdd"), -1, 0, string.Empty);
                ViewBag.autolink = autolink;
            }
            catch (Exception ex)
            {
                logError.Info("Autolink: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autolink(CMS_Core.Models.SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách AutoLink", "/CatalogSystem/Autolink");
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            bool checkDate = true;

            #region Check input data
            try
            {
                if (!string.IsNullOrEmpty(searchNewsView.tungay.Trim()))
                    Tungay = DateTime.ParseExact(searchNewsView.tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(searchNewsView.denngay.Trim()))
                    Denngay = DateTime.ParseExact(searchNewsView.denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

                if (Tungay > Denngay)
                {
                    ViewBag.TitleSuccsess = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    checkDate = false;
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    ViewBag.TitleSuccsess = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    checkDate = false;
                }
            }
            catch
            {
                ViewBag.TitleSuccsess = "Dữ liệu ngày tháng không đúng định dạng";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                checkDate = false;
            }
            #endregion

            try
            {
                if (checkDate)
                {
                    switch (submit)
                    {
                        case "SearchAutolink":

                            List<Autolink> autolink = impAutolink.GetAllAutolinkByKeyword(Tungay.ToString("yyyyMMdd"), Denngay.AddDays(1).ToString("yyyyMMdd"), -1, 0, searchNewsView.keyword);
                            ViewBag.autolink = autolink;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("Autolink:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);


        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult AutolinkDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách AutoLink", "/CatalogSystem/AutolinkDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impAutolink.DeleteAutolink(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }


                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("AutolinkDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult AutolinkPublich(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách AutoLink", "/CatalogSystem/AutolinkPublich");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impAutolink.PublicAutolink(Convert.ToInt32(customerID));
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
                logError.Info("AutolinkPublich: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }




        public ActionResult AutolinkAdd(string id)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới Autolink", "/CatalogSystem/AutolinkAdd");
            Autolink autolink = new Autolink();
            List<Autolink> autolinkS = null;

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.panelTitle = "Thêm mới AutoLink";
                }
                else
                {
                    ViewBag.panelTitle = "Cập nhật AutoLink";
                    autolinkS = impAutolink.GetAutolinkByID(Convert.ToInt32(id));
                    if (autolinkS != null)
                    {
                        if (autolinkS.Count > 0)
                        {
                            return View(autolinkS[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("AutolinkAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }
            autolink.MaxReplacements = 1;
            autolink.SortOrder = 1;
            autolink.TypeStart = 1;
            // Info.  
            return View(autolink);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AutolinkAdd(Autolink autolink)
        {

            if (ModelState.IsValid)
            {

                string result = string.Empty;
                if (autolink.ID == 0)
                {
                    bool exitName = false;
                    List<Autolink> autolinks = impAutolink.GetAutolinkByName(autolink.Keyword);
                    if (autolinks != null)
                    {
                        if (autolinks.Count > 0)
                        {
                            exitName = true;
                        }
                    }

                    if (!exitName)
                    {
                        result = impAutolink.InsertAutolink(autolink);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới autolink thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới autolink không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Đã tồn tại autolink rồi, mời bạn kiểm tra lại";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                    }
                }
                else
                {
                    result = impAutolink.UpdateAutolink(autolink);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin autoLink thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin autoLink không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }
            }

            return View(autolink);
        }


        #endregion



        #region --> Danh sách Slider     
        public ActionResult Slider()
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/Slider");
            AddBreadcrumb("Danh sách slider", "/CatalogSystem/Slider");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeyword = Common.Common.GetTypeKeywordSlider();
            try
            {

                List<tbl_slider> slider = new List<tbl_slider>();

                slider = imptbl_Slider.Gettbl_sliderByStatus(Common.Constant.TypeStatusPublic, 1, string.Empty);

                ViewBag.slider = slider;
            }
            catch (Exception ex)
            {
                logError.Info("Category:" + ex.ToString());
            }
            SearchCategoryViewModel obj = new SearchCategoryViewModel();
            obj.parentID = "0";
            obj.keyword = string.Empty;
            obj.Status = "-1";
            // Info.  
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Slider(SearchCategoryViewModel obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/Slider");
            AddBreadcrumb("Danh sách slider", "/CatalogSystem/Slider");

            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeyword = Common.Common.GetTypeKeywordSlider();
            try
            {
                switch (submit)
                {
                    case "SearchListSlider":
                        if (string.IsNullOrEmpty(obj.keyword))
                        {
                            obj.keyword = string.Empty;
                        }
                        List<tbl_slider> slider = new List<tbl_slider>();

                        slider = imptbl_Slider.Gettbl_sliderByStatus(Convert.ToInt32(obj.Status), Convert.ToInt32(obj.TypeKeyword), obj.keyword);

                        ViewBag.slider = slider;


                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("Slider:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult SliderDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/SliderDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    List<cms_Category> _Categories = null;

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_Slider.Deletetbl_slider(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    // _Categories = impcms_Category.GetAllcms_Category();
                    ViewBag.DataReport = _Categories;
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("CategoryDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult SliderPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/SliderPublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_Slider.Publictbl_slider(Convert.ToInt32(customerID));
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
                logError.Info("MenuPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListSliderAdd(string id)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới slider", "/CatalogSystem/ListSliderAdd");
            tbl_slider _slider = new tbl_slider();
            List<tbl_slider> sliders = null;



            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.panelTitle = "Thêm mới slider";

                }
                else
                {
                    ViewBag.panelTitle = "Cập nhật slider";
                    sliders = imptbl_Slider.Gettbl_SliderByID(Convert.ToInt32(id));
                    if (sliders != null)
                    {
                        if (sliders.Count > 0)
                        {
                            return View(sliders[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListSliderAdd:" + ex.ToString());
            }

            // Info.  
            return View(_slider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListSliderAdd(tbl_slider _slider)
        {



            bool validateImage = true;
            if (ModelState.IsValid)
            {

                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\images\\";

                //Upload Image
                if (_slider.ImageFile != null)
                {
                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(_slider.ImageFile.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(_slider.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    _slider.sliderImage = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(_slider.sliderImage))
                        Directory.CreateDirectory(_slider.sliderImage);

                    _slider.sliderImage = _slider.sliderImage + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(_slider.sliderImage))
                    {
                        System.IO.File.Delete(_slider.sliderImage);
                    }

                    if (!_slider.ImageFile.FileName.ToLower().EndsWith("gif") && !_slider.ImageFile.FileName.ToLower().EndsWith("jpg") && !_slider.ImageFile.FileName.ToLower().EndsWith("jpeg") && !_slider.ImageFile.FileName.ToLower().EndsWith("jpg") && !_slider.ImageFile.FileName.ToLower().EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                        validateImage = false;
                    }

                    if (!FileExtension.ToLower().EndsWith("gif") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("jpeg") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                        validateImage = false;
                    }

                    //Its Create complete path to store in server.
                    //To copy and save file into server.
                    if (validateImage)
                    {
                        _slider.ImageFile.SaveAs(_slider.sliderImage);
                        _slider.sliderImageWeb = Common.Common.reSizeImageBanner(_slider.sliderImage);
                        
                    }
                }
                else
                {
                    validateImage = true;
                }



                string result = string.Empty;
                if (validateImage)
                {
                    if (_slider.ID == 0)
                    {
                        List<tbl_slider> _Categories = imptbl_Slider.Gettbl_sliderByName(_slider.sliderTitle);
                        bool checkExit = false;
                        if (_Categories != null)
                        {
                            if (_Categories.Count > 0)
                            {
                                checkExit = true;
                            }
                        }

                        if (!checkExit)
                        {
                            _slider.sliderImage = Common.Common.GetFormatImage(_slider.sliderImage.Replace(Common.Common.getImagePath(), ""));
                            _slider.sliderImageWeb = Common.Common.GetFormatImage(_slider.sliderImageWeb.ToLower().Replace(Common.Common.getImagePath().ToLower(), ""));


                            _slider.createby = ((cms_Account)Session["UserInfo"]).Username;
                            result = imptbl_Slider.Inserttbl_slider(_slider);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới slider thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                Response.Redirect("/CatalogSystem/Slider", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới slider không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Đã tồn tại chuyên mục rồi, Mời bạn kiểm tra lại";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        _slider.udateby = ((cms_Account)Session["UserInfo"]).Username;
                        _slider.sliderImage = Common.Common.GetFormatImage(_slider.sliderImage.Replace(Common.Common.getImagePath(), ""));
                        _slider.sliderImageWeb = Common.Common.GetFormatImage(_slider.sliderImageWeb.ToLower().Replace(Common.Common.getImagePath().ToLower(), ""));

                        result = imptbl_Slider.Updatetbl_slider(_slider);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin slider thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/CatalogSystem/Slider", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin slider không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
                else
                {
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }

            return View(_slider);
        }

        #endregion

        #region --> Danh sách công ty     
        public ActionResult compay()
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/compay");
            AddBreadcrumb("Danh sách slider", "/CatalogSystem/compay");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeyword = Common.Common.GetTypeKeywordCompany();
            this.ViewBag.typeCompay = Common.Common.GetTypeCompany();
            try
            {

                List<tbl_Map> Map = new List<tbl_Map>();

                Map = imptbl_Map.Gettbl_MapByStatus(0, Common.Constant.TypeStatusPublic, 1, string.Empty);

                ViewBag.Map = Map;
            }
            catch (Exception ex)
            {
                logError.Info("Category:" + ex.ToString());
            }
            SearchCategoryViewModel obj = new SearchCategoryViewModel();
            obj.parentID = "0";
            obj.keyword = string.Empty;
            obj.Status = "-1";
            // Info.  
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult compay(SearchCategoryViewModel obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/Slider");
            AddBreadcrumb("Danh sách slider", "/CatalogSystem/Slider");

            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeyword = Common.Common.GetTypeKeywordCompany();
            this.ViewBag.typeCompay = Common.Common.GetTypeCompany();

            try
            {
                switch (submit)
                {
                    case "SearchListcompay":
                        List<tbl_Map> Map = new List<tbl_Map>();
                        if (string.IsNullOrEmpty(obj.keyword))
                        {
                            obj.keyword = string.Empty;
                        }
                        Map = imptbl_Map.Gettbl_MapByStatus(Convert.ToInt32(obj.parentID), Convert.ToInt32(obj.Status), Convert.ToInt32(obj.TypeKeyword), obj.keyword);

                        ViewBag.Map = Map;




                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("Slider:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult compayDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/compayDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    List<cms_Category> _Categories = null;

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_Map.Deletetbl_Map(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    // _Categories = impcms_Category.GetAllcms_Category();
                    ViewBag.DataReport = _Categories;
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("CategoryDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult compayPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/compayPublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_Map.Publictbl_Map(Convert.ToInt32(customerID));
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
                logError.Info("MenuPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListCompayAdd(string id)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới chi nhánh công ty", "/CatalogSystem/ListCompayAdd");
            tbl_Map Map = new tbl_Map();
            List<tbl_Map> Maps = null;
            this.ViewBag.GetTypeCompany = Common.Common.GetTypeCompany();
            this.ViewBag.GetTypeLocaltion = Common.Common.GetTypeLocaltion();



            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.panelTitle = "Thêm mới chi nhánh công ty";

                }
                else
                {
                    ViewBag.panelTitle = "Thêm mới chi nhánh công ty";
                    Maps = imptbl_Map.Gettbl_MapByID(Convert.ToInt32(id));
                    if (Maps != null)
                    {
                        if (Maps.Count > 0)
                        {
                            return View(Maps[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListCompayAdd:" + ex.ToString());
            }

            // Info.  
            return View(Map);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListCompayAdd(tbl_Map _Map)
        {


            this.ViewBag.GetTypeCompany = Common.Common.GetTypeCompany();
            this.ViewBag.GetTypeLocaltion = Common.Common.GetTypeLocaltion();
            bool validateImage = true;
            if (ModelState.IsValid)
            {

                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\images\\";

                //Upload Image
                if (_Map.ImageFile != null)
                {
                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(_Map.ImageFile.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(_Map.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    _Map.Img = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(_Map.Img))
                        Directory.CreateDirectory(_Map.Img);

                    _Map.Img = _Map.Img + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(_Map.Img))
                    {
                        System.IO.File.Delete(_Map.Img);
                    }

                    if (!_Map.ImageFile.FileName.ToLower().EndsWith("gif") && !_Map.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Map.ImageFile.FileName.ToLower().EndsWith("jpeg") && !_Map.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Map.ImageFile.FileName.ToLower().EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                        validateImage = false;
                    }

                    if (!FileExtension.ToLower().EndsWith("gif") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("jpeg") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                        validateImage = false;
                    }

                    //Its Create complete path to store in server.
                    //To copy and save file into server.
                    if (validateImage)
                    {
                        _Map.ImageFile.SaveAs(_Map.Img);
                    }
                }
                else
                {
                    validateImage = true;
                }



                string result = string.Empty;
                if (validateImage)
                {
                    if (_Map.ID == 0)
                    {
                        List<tbl_Map> _Categories = imptbl_Map.Gettbl_MapByName(_Map.TenPK);
                        bool checkExit = false;
                        if (_Categories != null)
                        {
                            if (_Categories.Count > 0)
                            {
                                checkExit = true;
                            }
                        }

                        if (!checkExit)
                        {
                            _Map.Img = Common.Common.GetFormatImage(_Map.Img.Replace(Common.Common.getImagePath(), ""));

                            _Map.createby = ((cms_Account)Session["UserInfo"]).Username;
                            result = imptbl_Map.Inserttbl_Map(_Map);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới chi nhánh công ty thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                Response.Redirect("/CatalogSystem/compay", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới chi nhánh công ty không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Đã tồn tại chi nhánh công ty rồi, Mời bạn kiểm tra lại";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        _Map.updateby = ((cms_Account)Session["UserInfo"]).Username;
                        _Map.Img = Common.Common.GetFormatImage(_Map.Img.Replace(Common.Common.getImagePath(), ""));
                        result = imptbl_Map.Updatetbl_Map(_Map);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin chi nhánh công ty thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/CatalogSystem/compay", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin chi nhánh công ty không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
                else
                {
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }

            return View(_Map);
        }

        #endregion

        #region --> Danh sách xét nghiệm  
        public ActionResult TestCode()
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/TestCode");
            AddBreadcrumb("Danh sách xét nghiệm", "/CatalogSystem/TestCode");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeywordTestcode = Common.Common.GetTypeKeywordTestcode();

            try
            {

                List<tbl_TestCode> TestCode = new List<tbl_TestCode>();

                TestCode = imptbl_TestCodecs.Gettbl_TestCodeByStatus(Common.Constant.TypeStatusPublic, 1, string.Empty);

                ViewBag.TestCode = TestCode;
            }
            catch (Exception ex)
            {
                logError.Info("Category:" + ex.ToString());
            }
            SearchCategoryViewModel obj = new SearchCategoryViewModel();
            obj.parentID = "0";
            obj.keyword = string.Empty;
            obj.Status = Common.Constant.TypeStatusPublic.ToString();
            // Info.  
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TestCode(SearchCategoryViewModel obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/TestCode");
            AddBreadcrumb("Danh sách xét nghiệm", "/CatalogSystem/TestCode");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeywordTestcode = Common.Common.GetTypeKeywordTestcode();

            try
            {
                switch (submit)
                {
                    case "SearchListTestCode":
                        List<tbl_TestCode> TestCode = new List<tbl_TestCode>();
                        if (string.IsNullOrEmpty(obj.keyword))
                        {
                            obj.keyword = string.Empty;
                        }
                        TestCode = imptbl_TestCodecs.Gettbl_TestCodeByStatus(Convert.ToInt32(obj.Status), Convert.ToInt32(obj.TypeKeyword), obj.keyword);

                        ViewBag.TestCode = TestCode;



                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("TestCode:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult TestCodeDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/TestCodeDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_TestCodecs.Deletetbl_TestCode(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }


                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("CategoryDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult TestCodePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/TestCodePublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_TestCodecs.Publictbl_TestCode(Convert.ToInt32(customerID));
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
                logError.Info("TestCodePublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListTestCodeAdd(string id)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới xét nghiệm", "/CatalogSystem/ListTestCodeAdd");
            tbl_TestCode TestCode = new tbl_TestCode();
            List<tbl_TestCode> TestCodes = null;

            Session["testcodeRelate" + id] = null;

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.panelTitle = "Thêm mới loại xét nghiệm";

                }
                else
                {
                    ViewBag.panelTitle = "Update loại xét nghiệm";
                    TestCodes = imptbl_TestCodecs.Gettbl_TestCodeByID(Convert.ToInt32(id));
                    if (TestCodes != null)
                    {
                        if (TestCodes.Count > 0)
                        {
                            List<cms_News> _NewsRelate = impcms_News.GetAllcms_NewsRelateTestcode(Convert.ToInt32(id));

                            if (_NewsRelate != null)
                            {
                                ViewBag.CountNewsRelate = "Số tin liên quan là: " + _NewsRelate.Count.ToString();

                            }

                            return View(TestCodes[0]);
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                logError.Info("ListTestCodeAdd:" + ex.ToString());
            }

            // Info.  
            return View(TestCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ListTestCodeAdd(tbl_TestCode _TestCode)
        {
            List<cms_News> _NewsRelate = new List<cms_News>();
            _TestCode.ListNewsRelate = string.Empty;
            if (Session["testcodeRelate" + _TestCode.ID] != null)
            {
                _NewsRelate = (List<cms_News>)Session["testcodeRelate" + _TestCode.ID];
            }


            if (ModelState.IsValid)
            {


                string result = string.Empty;

                if (_TestCode.ID == 0)
                {
                    List<tbl_TestCode> _TestCodes = imptbl_TestCodecs.Gettbl_TestCodeByName(_TestCode.TestCode);
                    bool checkExit = false;
                    if (_TestCodes != null)
                    {
                        if (_TestCodes.Count > 0)
                        {
                            checkExit = true;
                        }
                    }

                    if (!checkExit)
                    {

                        _TestCode.createby = ((cms_Account)Session["UserInfo"]).Username;
                        result = imptbl_TestCodecs.Inserttbl_TestCode(_TestCode);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới loại xét nghiệm thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());

                            // chèn tin bài liên quan                           
                            if (_NewsRelate.Count > 0)
                            {
                                foreach (var NewsRelate in _NewsRelate)
                                {
                                    impcms_News_RelateTestcode.Insertcms_News_Relate(NewsRelate, result);
                                }
                            }

                            Response.Redirect("/CatalogSystem/TestCode", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới loại xét nghiệm không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Đã tồn tại loại xét nghiệm loại xét nghiệm rồi, Mời bạn kiểm tra lại";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
                else
                {
                    _TestCode.updateby = ((cms_Account)Session["UserInfo"]).Username;

                    result = imptbl_TestCodecs.Updatetbl_TestCode(_TestCode);
                    if (!string.IsNullOrEmpty(result))
                    {

                        // chèn tin bài liên quan
                        string newsIdRelate = string.Empty;

                        if (_NewsRelate.Count > 0)
                        {
                            foreach (var NewsRelate in _NewsRelate)
                            {
                                impcms_News_RelateTestcode.Insertcms_News_Relate(NewsRelate, _TestCode.ID.ToString());
                                newsIdRelate = newsIdRelate + NewsRelate.newsId.ToString() + ",";
                            }
                        }

                        if (newsIdRelate.Length > 1)
                        {
                            newsIdRelate = newsIdRelate.Substring(0, newsIdRelate.Length - 1);

                            impcms_News_RelateTestcode.Deletecms_News_Relate(newsIdRelate, _TestCode.ID.ToString());
                        }


                        ViewBag.TitleSuccsess = "Cập nhật thông tin loại xét nghiệm thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                        Response.Redirect("/CatalogSystem/TestCode", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin loại xét nghiệm không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }

            }

            return View(_TestCode);
        }

        #endregion


        #region --> Danh sách Seo
        public ActionResult Listseo()
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/Listseo");
            AddBreadcrumb("Danh sách seo", "/CatalogSystem/Listseo");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeywordSeo = Common.Common.GetTypeKeywordSeo();
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrentNews();

            try
            {

                List<tbl_seo> seo = new List<tbl_seo>();

                seo = imptbl_seo.Gettbl_seoByStatus(0, Common.Constant.TypeStatusPublic, 1, string.Empty);

                ViewBag.seo = seo;
            }
            catch (Exception ex)
            {
                logError.Info("Listseo:" + ex.ToString());
            }
            SearchCategoryViewModel obj = new SearchCategoryViewModel();
            obj.parentID = "0";
            obj.keyword = string.Empty;
            obj.Status = Common.Constant.TypeStatusPublic.ToString();
            // Info.  
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Listseo(SearchCategoryViewModel obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/Listseo");
            AddBreadcrumb("Danh sách seo", "/CatalogSystem/Listseo");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeywordSeo = Common.Common.GetTypeKeywordSeo();
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrentNews();
            try
            {
                switch (submit)
                {
                    case "SearchListseo":
                        List<tbl_seo> seo = new List<tbl_seo>();
                        if (string.IsNullOrEmpty(obj.keyword))
                        {
                            obj.keyword = string.Empty;
                        }
                        seo = imptbl_seo.Gettbl_seoByStatus(Convert.ToInt32(obj.parentID), Convert.ToInt32(obj.Status), Convert.ToInt32(obj.TypeKeyword), obj.keyword);
                        ViewBag.seo = seo;



                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("Listseo:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListseoDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/Listseo");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/ListseoDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_seo.Deletetbl_seo(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }


                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListseoDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListseoPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Danh sách chuyên mục", "/CatalogSystem/ListseoPublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_seo.Publictbl_seo(Convert.ToInt32(customerID));
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
                logError.Info("ListseoPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListseoAdd(string id)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới seo", "/CatalogSystem/ListseoAdd");
            tbl_seo Seo = new tbl_seo();
            List<tbl_seo> Seos = null;
            Seo.cateid = 0;
            Seo.type = 0;


            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.panelTitle = "Thêm mới seo";

                }
                else
                {
                    ViewBag.panelTitle = "Update seo";
                    Seos = imptbl_seo.Gettbl_seoByID(Convert.ToInt32(id));
                    if (Seos != null)
                    {
                        if (Seos.Count > 0)
                        {
                            return View(Seos[0]);
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                logError.Info("ListseoAdd:" + ex.ToString());
            }

            // Info.  
            return View(Seo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ListseoAdd(tbl_seo Seo)
        {



            if (ModelState.IsValid)
            {


                string result = string.Empty;

                if (Seo.id == 0)
                {


                    Seo.createby = ((cms_Account)Session["UserInfo"]).Username;
                    result = imptbl_seo.Inserttbl_seo(Seo);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Thêm mới seo thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionInsertOK.ToString());



                        Response.Redirect("/CatalogSystem/Listseo", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thêm mới seo không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                    }

                }
                else
                {
                    Seo.updateby = ((cms_Account)Session["UserInfo"]).Username;

                    result = imptbl_seo.Updatetbl_seo(Seo);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin seo thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                        Response.Redirect("/CatalogSystem/Listseo", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin seo không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }

            }

            return View(Seo);
        }

        #endregion


        #region --> Danh sách Vote
        public ActionResult ListVoteCate()
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/ListVoteCate");
            AddBreadcrumb("Danh sách loại vote ", "/CatalogSystem/ListVoteCate");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeywordVoteCate = Common.Common.GetTypeKeywordVoteCate();


            try
            {

                List<tbl_VoteCate> VoteCates = new List<tbl_VoteCate>();

                VoteCates = imptbl_VoteCate.Gettbl_VoteCateByStatus(Common.Constant.TypeStatusPublic, 1, string.Empty);

                ViewBag.VoteCates = VoteCates;
            }
            catch (Exception ex)
            {
                logError.Info("ListVoteCate:" + ex.ToString());
            }
            SearchCategoryViewModel obj = new SearchCategoryViewModel();
            obj.parentID = "0";
            obj.keyword = string.Empty;
            obj.Status = Common.Constant.TypeStatusPublic.ToString();
            // Info.  
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListVoteCate(SearchCategoryViewModel obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/ListVoteCate");
            AddBreadcrumb("Danh sách loại vote ", "/CatalogSystem/ListVoteCate");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeywordVoteCate = Common.Common.GetTypeKeywordVoteCate();
            try
            {
                switch (submit)
                {
                    case "SearchListVoteCate":
                        List<tbl_VoteCate> VoteCates = new List<tbl_VoteCate>();
                        if (string.IsNullOrEmpty(obj.keyword))
                        {
                            obj.keyword = string.Empty;
                        }
                        VoteCates = imptbl_VoteCate.Gettbl_VoteCateByStatus(Convert.ToInt32(obj.Status), Convert.ToInt32(obj.TypeKeyword), obj.keyword);
                        ViewBag.VoteCates = VoteCates;
                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListVoteCate:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListVoteCateDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/ListVoteCate");
            AddBreadcrumb("Danh sách loại vote ", "/CatalogSystem/ListVoteCateDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_VoteCate.Deletetbl_VoteCate(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }


                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListVoteCateDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListVoteCatePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/ListVoteCate");
            AddBreadcrumb("Danh sách loại vote ", "/CatalogSystem/ListVoteCatePublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_VoteCate.Publictbl_VoteCate(Convert.ToInt32(customerID));
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
                logError.Info("ListVoteCatePublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListVoteCateAdd(string id)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới loại vote ", "/CatalogSystem/ListVoteCateAdd");
            tbl_VoteCate VoteCate = new tbl_VoteCate();
            List<tbl_VoteCate> VoteCates = null;
            VoteCate.status = 1;
            VoteCate.ID = 0;
            VoteCate.datesartView = DateTime.Now.ToString("dd/MM/yyyy");
            VoteCate.dataendView = DateTime.Now.ToString("dd/MM/yyyy");
            VoteCate.ImagePath = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.panelTitle = "Thêm mới loại vote ";

                }
                else
                {
                    ViewBag.panelTitle = "Update loại vote";
                    VoteCates = imptbl_VoteCate.Gettbl_VoteCateByID(Convert.ToInt32(id));
                    if (VoteCates != null)
                    {
                        if (VoteCates.Count > 0)
                        {
                            return View(VoteCates[0]);
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                logError.Info("ListseoAdd:" + ex.ToString());
            }

            // Info.  
            return View(VoteCate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListVoteCateAdd(tbl_VoteCate VoteCate)
        {
            if (ModelState.IsValid)
            {

                DateTime Tungay = new DateTime();
                DateTime Denngay = new DateTime();
                bool validateImage = true;
                bool validatedate = true;
                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\imageslead\\";


                //Upload Image
                if (VoteCate.ImageFile != null)
                {
                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(VoteCate.ImageFile.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(VoteCate.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    VoteCate.ImagePath = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(VoteCate.ImagePath))
                        Directory.CreateDirectory(VoteCate.ImagePath);

                    VoteCate.ImagePath = VoteCate.ImagePath + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(VoteCate.ImagePath))
                    {
                        System.IO.File.Delete(VoteCate.ImagePath);
                    }

                    if (!VoteCate.ImageFile.FileName.EndsWith("gif") && !VoteCate.ImageFile.FileName.EndsWith("jpg") && !VoteCate.ImageFile.FileName.EndsWith("jpeg") && !VoteCate.ImageFile.FileName.EndsWith("jpg") && !VoteCate.ImageFile.FileName.EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                        validateImage = false;
                    }

                    if (!FileExtension.EndsWith("gif") && !FileExtension.EndsWith("jpg") && !FileExtension.EndsWith("jpeg") && !FileExtension.EndsWith("jpg") && !FileExtension.EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                        validateImage = false;
                    }

                    //Its Create complete path to store in server.
                    //To copy and save file into server.
                    if (validateImage)
                    {
                        VoteCate.ImageFile.SaveAs(VoteCate.ImagePath);
                    }
                }
                else
                {
                    validateImage = false;
                }

                #region Check input data
                try
                {
                    if (!string.IsNullOrEmpty(VoteCate.datesartView.Trim()))
                        Tungay = DateTime.ParseExact(VoteCate.datesartView.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                    if (!string.IsNullOrEmpty(VoteCate.dataendView.Trim()))
                        Denngay = DateTime.ParseExact(VoteCate.dataendView.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

                    if (Tungay > Denngay)
                    {
                        ViewBag.TitleSuccsess = "Từ ngày phải nhỏ hơn hoặc bằng đến ngày";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        validatedate = false;
                    }
                }
                catch
                {
                    ViewBag.TitleSuccsess = "Dữ liệu ngày tháng không đúng định dạng";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    validatedate = false;
                }
                #endregion

                VoteCate.datesart = Tungay;
                VoteCate.dataend = Denngay;

                string result = string.Empty;
                if (validateImage)
                {
                    if (validatedate)
                    {
                        if (VoteCate.ID == 0)
                        {
                            bool checkExit = true;
                            List<tbl_VoteCate> VoteCateExit = imptbl_VoteCate.Gettbl_VoteCateByName(VoteCate.VoteCate);
                            if (VoteCateExit != null)
                            {
                                if (VoteCateExit.Count > 0)
                                {
                                    checkExit = false;
                                }
                            }
                            if (checkExit)
                            {
                                VoteCate.createby = ((cms_Account)Session["UserInfo"]).Username;
                                result = imptbl_VoteCate.Inserttbl_VoteCate(VoteCate);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    ViewBag.TitleSuccsess = "Thêm mới loại vote thành công";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                    AddLogAction(result, Constant.ActionInsertOK.ToString());



                                    Response.Redirect("/CatalogSystem/ListVoteCate", false);
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Thêm mới loại vote không thành công";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    AddLogAction(result, Constant.ActionInsertNOK.ToString());
                                }
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới loại vote không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
                            }

                        }
                        else
                        {
                            VoteCate.updateby = ((cms_Account)Session["UserInfo"]).Username;

                            result = imptbl_VoteCate.Updatetbl_VoteCate(VoteCate);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Cập nhật thông tin seo thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                Response.Redirect("/CatalogSystem/ListVoteCate", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Cập nhật thông tin seo không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Dữ liệu hình ảnh sai định dạng";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }

            return View(VoteCate);
        }

        #endregion

        #region --> Danh sách Vote
        public ActionResult ListVote()
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/ListVote");
            AddBreadcrumb("Danh sách loại vote ", "/CatalogSystem/ListVote");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeywordVote = Common.Common.GetTypeKeywordVote();


            try
            {

                List<tbl_Vote> Votes = new List<tbl_Vote>();

                Votes = imptbl_Vote.Gettbl_VoteByStatus(0, Common.Constant.TypeStatusPublic, 1, string.Empty);

                ViewBag.Votes = Votes;
            }
            catch (Exception ex)
            {
                logError.Info("ListVote:" + ex.ToString());
            }
            SearchCategoryViewModel obj = new SearchCategoryViewModel();
            obj.parentID = "0";
            obj.keyword = string.Empty;
            obj.Status = Common.Constant.TypeStatusPublic.ToString();
            // Info.  
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListVote(SearchCategoryViewModel obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/ListVoteCate");
            AddBreadcrumb("Danh sách loại vote ", "/CatalogSystem/ListVoteCate");
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeywordVote = Common.Common.GetTypeKeywordVote();

            try
            {
                switch (submit)
                {
                    case "SearchListVote":
                        List<tbl_Vote> Votes = new List<tbl_Vote>();
                        if (string.IsNullOrEmpty(obj.keyword))
                        {
                            obj.keyword = string.Empty;
                        }
                        Votes = imptbl_Vote.Gettbl_VoteByStatus(Convert.ToInt32(obj.parentID), Convert.ToInt32(obj.Status), Convert.ToInt32(obj.TypeKeyword), obj.keyword);
                        ViewBag.Votes = Votes;
                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListVote:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListVoteDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/ListVoteCate");
            AddBreadcrumb("Danh sách loại vote ", "/CatalogSystem/ListVoteDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_Vote.Deletetbl_Vote(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }


                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListVoteDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListVotePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/ListVoteCate");
            AddBreadcrumb("Danh sách loại vote ", "/CatalogSystem/ListVotePublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        imptbl_Vote.Publictbl_Vote(Convert.ToInt32(customerID));
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
                logError.Info("ListVotePublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListVoteAdd(string id)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới loại vote ", "/CatalogSystem/ListVoteAdd");
            tbl_Vote Vote = new tbl_Vote();
            List<tbl_Vote> Votes = null;
            Vote.status = 1;
            Vote.VoteCateID = 0;
            Vote.TypeVote = 1;



            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.panelTitle = "Thêm mới vote ";

                }
                else
                {
                    ViewBag.panelTitle = "Update vote";
                    Votes = imptbl_Vote.Gettbl_VoteByID(Convert.ToInt32(id));
                    if (Votes != null)
                    {
                        if (Votes.Count > 0)
                        {
                            return View(Votes[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListVoteAdd:" + ex.ToString());
            }

            // Info.  
            return View(Vote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListVoteAdd(tbl_Vote Vote)
        {



            if (ModelState.IsValid)
            {



                string result = string.Empty;

                if (Vote.ID == 0)
                {
                    bool checkExit = true;
                    List<tbl_Vote> VoteExit = imptbl_Vote.Gettbl_VoteByName(Vote.VoteName);
                    if (VoteExit != null)
                    {
                        if (VoteExit.Count > 0)
                        {
                            checkExit = false;
                        }
                    }
                    if (checkExit)
                    {
                        Vote.createby = ((cms_Account)Session["UserInfo"]).Username;
                        result = imptbl_Vote.Inserttbl_Vote(Vote);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới loại vote thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                            Response.Redirect("/CatalogSystem/ListVote", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới loại vote không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thêm mới loại vote không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                    }

                }
                else
                {
                    Vote.updateby = ((cms_Account)Session["UserInfo"]).Username;

                    result = imptbl_Vote.Updatetbl_Vote(Vote);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin vote thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                        Response.Redirect("/CatalogSystem/ListVote", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin vote không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }

            }

            return View(Vote);
        }

        #endregion

        #region --> Nhap question

        public ActionResult VoteQuestionsAdd(string VoteID)
        {
            // Initialization.             
            AddBreadcrumb("Hệ thống danh mục", "/CatalogSystem/listAccount");
            AddBreadcrumb("Thêm mới câu hỏi", "/CatalogSystem/VoteQuestionsAdd");
            tbl_VoteQuestions VoteQuestionse = new tbl_VoteQuestions();
            List<tbl_VoteQuestions> VoteQuestions = null;

            VoteQuestionse.VoteID = 0;




            try
            {
                if (string.IsNullOrEmpty(VoteID))
                {
                    ViewBag.panelTitle = "Thêm mới câu hỏi";

                }
                else
                {
                    ViewBag.panelTitle = "Cập nhật câu hỏi";
                    VoteQuestions = imptbl_VoteQuestions.Gettbl_VoteQuestionsByVoteID(Convert.ToInt32(VoteID));
                    if (VoteQuestions != null)
                    {
                        if (VoteQuestions.Count > 0)
                        {
                            return View(VoteQuestions[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("VoteQuestionsAdd:" + ex.ToString());
            }

            // Info.  
            return View(VoteQuestionse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VoteQuestionsAdd(tbl_VoteQuestions VoteQuestionse)
        {



            if (ModelState.IsValid)
            {



                string result = string.Empty;

                if (VoteQuestionse.ID == 0)
                {

                    VoteQuestionse.createby = ((cms_Account)Session["UserInfo"]).Username;
                    result = imptbl_VoteQuestions.Inserttbl_VoteQuestions(VoteQuestionse);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Thêm mới câu hỏi thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionInsertOK.ToString());
                        Response.Redirect("/CatalogSystem/ListVote", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thêm mới câu hỏi không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                    }


                }
                else
                {
                    VoteQuestionse.updateby = ((cms_Account)Session["UserInfo"]).Username;

                    result = imptbl_VoteQuestions.Updatetbl_VoteQuestions(VoteQuestionse);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin câu hỏi thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                        Response.Redirect("/CatalogSystem/ListVote", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin câu hỏi không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }

            }

            return View(VoteQuestionse);
        }

        #endregion
    }
}
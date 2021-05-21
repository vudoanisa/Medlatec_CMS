using CMS_Core.Entity;
using CMS_Core.Implementtion;
using CMSLIS.Controllers;
using CMSLIS.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSLIS.Common;
using CMSNEW.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using System.Text;
using System.Xml;
using Spire.Pdf;
using CMSNEW.Common;

namespace CMSNEW.Controllers
{
    [CheckAuthorization]
    public class pharmacyStoreController : BaseController
    {
        ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");
        // GET: pharmacyStore


        private ImpCMS_MEDICINE impCMS_MEDICINE;
        private ImpCMS_MEDICINE_RECEIPT_STORE impCMS_MEDICINE_RECEIPT_STORE;
        private ImpCMS_MEDICINE_RECEIPT_DETAIL impCMS_MEDICINE_RECEIPT_DETAIL;
        private ImpCMS_MEDICINE_EXPORT_DETAIL impCMS_MEDICINE_EXPORT_DETAIL;
        private ImpCMS_GROUP_MEDICINE impCMS_GROUP_MEDICINE;
        private ImpCMS_MEDICINE_EXPORT_STORE impCMS_MEDICINE_EXPORT_STORE;
        private ImpCMS_MEDICINE_REFUND_STORE impCMS_MEDICINE_REFUND_STORE;
        private ImpCMS_MEDICINE_REFUND_DETAIL impCMS_MEDICINE_REFUND_DETAIL;
        private ImpCMS_MEDICINE_INVENTORY impCMS_MEDICINE_INVENTORY;


        public pharmacyStoreController()
        {
            impCMS_MEDICINE = new ImpCMS_MEDICINE();
            impCMS_MEDICINE_RECEIPT_STORE = new ImpCMS_MEDICINE_RECEIPT_STORE();
            impCMS_MEDICINE_RECEIPT_DETAIL = new ImpCMS_MEDICINE_RECEIPT_DETAIL();
            impCMS_MEDICINE_EXPORT_DETAIL = new ImpCMS_MEDICINE_EXPORT_DETAIL();
            impCMS_GROUP_MEDICINE = new ImpCMS_GROUP_MEDICINE();
            impCMS_MEDICINE_EXPORT_STORE = new ImpCMS_MEDICINE_EXPORT_STORE();
            impCMS_MEDICINE_REFUND_STORE = new ImpCMS_MEDICINE_REFUND_STORE();
            impCMS_MEDICINE_REFUND_DETAIL = new ImpCMS_MEDICINE_REFUND_DETAIL();
            impCMS_MEDICINE_INVENTORY = new ImpCMS_MEDICINE_INVENTORY();

        }


        public ActionResult Index()
        {
            return View();
        }


        #region --> Danh sách nhóm thuốc  
        public ActionResult ListGroupMedicine()
        {
            // Initialization.  
            AddPageHeader("Danh sách nhóm thuốc", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách nhóm thuốc", "/pharmacyStore/ListGroupMedicine");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                this.ViewBag.GetTypeStatus = CMSLIS.Common.Common.GetTypeStatus();



                List<CMS_GROUP_MEDICINE> _GROUP_MEDICINES = new List<CMS_GROUP_MEDICINE>();
                _GROUP_MEDICINES = impCMS_GROUP_MEDICINE.GetCMS_GROUP_MEDICINE(UserInfo.PARTNERID);

                ViewBag.GROUP_MEDICINES = _GROUP_MEDICINES;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListGroupMedicine: " + ex.ToString());
            }
            SearchCategoryViewModel obj = new SearchCategoryViewModel();
            if (UserInfo.PARTNERID == 1)
            {
                obj.PARTNERID = "0";
            }
            else
            {
                obj.PARTNERID = UserInfo.PARTNERID.ToString();
            }

            obj.parentID = "0";
            // Info.  
            return View(obj);
        }

        [HttpPost]
        public ActionResult ListGroupMedicine(SearchCategoryViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách nhóm thuốc", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách nhóm thuốc", "/pharmacyStore/ListGroupMedicine");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            this.ViewBag.GetTypeStatus = CMSLIS.Common.Common.GetTypeStatus();
            if (string.IsNullOrEmpty(obj.parentID))
            {
                obj.parentID = "0";
            }
            try
            {
                switch (submit)
                {
                    case "SearchListGroupMedicine":

                        List<CMS_GROUP_MEDICINE> _GROUP_MEDICINES = impCMS_GROUP_MEDICINE.GetAllCMS_GROUP_MEDICINEByStatus(Convert.ToInt32(obj.parentID), Convert.ToInt32(obj.Status), Convert.ToInt32(obj.PARTNERID), UserInfo.PARTNERID);
                        ViewBag.GROUP_MEDICINES = _GROUP_MEDICINES;
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListGroupMedicine: " + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListGroupMedicineDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Danh sách nhóm thuốc", "/pharmacyStore/ListGroupMedicineDelete");
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
                        result = impCMS_GROUP_MEDICINE.DeleteCMS_GROUP_MEDICINE(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("ListGroupMedicineDelete: " + ex.ToString());
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
        public ActionResult ListGroupMedicinePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Danh sách nhóm thuốc", "/pharmacyStore/ListGroupMedicinePublic");
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
                        impCMS_GROUP_MEDICINE.PUBLICCMS_GROUP_MEDICINE(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("ListGroupMedicinePublic: " + ex.ToString());
                return Json("Có lỗi trong quá trình duyệt");
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListGroupMedicineAdd(string ID)
        {
            // Initialization.             
            AddBreadcrumb("Kho thuốc", "");

            CMS_GROUP_MEDICINE _GROUP_MEDICINE = new CMS_GROUP_MEDICINE();
            List<CMS_GROUP_MEDICINE> _GROUP_MEDICINES = null;

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    AddPageHeader("Thêm mới nhóm thuốc", "");
                    AddBreadcrumb("Thêm mới nhóm thuốc", "/pharmacyStore/ListGroupMedicineAdd");
                    ViewBag.Title = "Thêm mới nhóm thuốc";
                    _GROUP_MEDICINE.ID = 0;
                    if (UserInfo.PARTNERID != 1)
                    {
                        _GROUP_MEDICINE.PARTNERID = UserInfo.PARTNERID;
                    }
                    else
                    {
                        _GROUP_MEDICINE.PARTNERID = 0;
                    }
                    _GROUP_MEDICINE.GROUP_MEDICINE_PARENT = 0;

                }
                else
                {
                    AddPageHeader("Cập nhật nhóm thuốc", "");
                    AddBreadcrumb("Cập nhật nhóm thuốc", "/pharmacyStore/ListGroupMedicineAdd");
                    ViewBag.Title = "Cập nhật nhóm thuốc";
                    _GROUP_MEDICINES = impCMS_GROUP_MEDICINE.GetCMS_GROUP_MEDICINEByID(Convert.ToInt32(ID), UserInfo.PARTNERID);
                    if (_GROUP_MEDICINES != null)
                    {
                        if (_GROUP_MEDICINES.Count > 0)
                        {
                            return View(_GROUP_MEDICINES[0]);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin nhóm thuốc";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin nhóm thuốc";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListGroupMedicineAdd: " + ex.ToString());
            }

            // Info.  
            return View(_GROUP_MEDICINE);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListGroupMedicineAdd(CMS_GROUP_MEDICINE _GROUP_MEDICINE)
        {
            try
            {
                if (_GROUP_MEDICINE.ID == 0)
                {
                    AddPageHeader("Thêm mới nhóm thuốc", "");
                    AddBreadcrumb("Cập nhật nhóm thuốc", "/pharmacyStore/ListGroupMedicineAdd");
                }
                else
                {
                    AddPageHeader("Cập nhật nhóm thuốc", "");
                    AddBreadcrumb("Cập nhật nhóm thuốc", "/pharmacyStore/ListGroupMedicineAdd");
                }

                if (ModelState.IsValid)
                {
                    var UserInfo = ((cms_Account)Session["UserInfo"]);


                    string result = string.Empty;
                    if (_GROUP_MEDICINE.ID == 0)
                    {
                        bool checkExit = false;

                        List<CMS_GROUP_MEDICINE> _GROUP_MEDICINEExit = impCMS_GROUP_MEDICINE.GetCMS_GROUP_MEDICINEByName(_GROUP_MEDICINE.GROUP_MEDICINE_NAME, UserInfo.PARTNERID);
                        if (_GROUP_MEDICINEExit != null)
                        {
                            if (_GROUP_MEDICINEExit.Count > 0)
                            {
                                checkExit = true;
                                ViewBag.TitleSuccsess = "Đã tồn tại nhóm thuốc rồi, Mời bạn kiểm tra lại!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                        }
                        if (!checkExit)
                        {
                            _GROUP_MEDICINE.GROUP_MEDICINE_CREATEBY = UserInfo.accountid;
                            _GROUP_MEDICINE.GROUP_MEDICINE_CREATEDATE = DateTime.Now;
                            _GROUP_MEDICINE.GROUP_MEDICINE_STATUS = 1;

                            result = impCMS_GROUP_MEDICINE.InsertCMS_GROUP_MEDICINE(_GROUP_MEDICINE);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới nhóm thuốc thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                Response.Redirect("/pharmacyStore/ListGroupMedicine", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới nhóm thuốc không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());

                            }
                        }
                    }
                    else
                    {
                        _GROUP_MEDICINE.GROUP_MEDICINE_UPDATEBY = UserInfo.accountid;
                        _GROUP_MEDICINE.GROUP_MEDICINE_UPDATEDATE = DateTime.Now;

                        result = impCMS_GROUP_MEDICINE.UpdateCMS_GROUP_MEDICINE(_GROUP_MEDICINE);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin nhóm thuốc thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/pharmacyStore/ListGroupMedicine", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin nhóm thuốc không thành công";
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
                logError.Info("ListGroupMedicineAdd: " + ex.ToString());
            }
            return View(_GROUP_MEDICINE);
        }

        #endregion




        #region --> Danh sách thuốc  
        public ActionResult ListMedicine()
        {
            // Initialization.  
            AddPageHeader("Danh sách thuốc", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách thuốc", "/pharmacyStore/ListMedicine");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                this.ViewBag.GetTypeStatus = CMSLIS.Common.Common.GetTypeStatus();
                this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordMedicine();


                List<CMS_MEDICINE> _MEDICINES = new List<CMS_MEDICINE>();
                _MEDICINES = impCMS_MEDICINE.GetCMS_MEDICINE(UserInfo.PARTNERID);

                ViewBag.MEDICINES = _MEDICINES;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListMedicine: " + ex.ToString());
            }
            SearchCategoryViewModel obj = new SearchCategoryViewModel();
            if (UserInfo.PARTNERID == 1)
            {
                obj.PARTNERID = "0";
            }
            else
            {
                obj.PARTNERID = UserInfo.PARTNERID.ToString();
            }

            obj.parentID = "0";
            // Info.  
            return View(obj);
        }

        [HttpPost]
        public ActionResult ListMedicine(SearchCategoryViewModel obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách thuốc", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách thuốc", "/pharmacyStore/ListMedicine");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            this.ViewBag.GetTypeStatus = CMSLIS.Common.Common.GetTypeStatus();
            this.ViewBag.GetTypeKeyword = CMSLIS.Common.Common.GetTypeKeywordMedicine();

            if (string.IsNullOrEmpty(obj.parentID))
            {
                obj.parentID = "0";
            }
            try
            {
                switch (submit)
                {
                    case "SearchListMedicine":
                        List<CMS_MEDICINE> _MEDICINES = impCMS_MEDICINE.GetAllCMS_MEDICINEByStatus(Convert.ToInt32(obj.parentID), Convert.ToInt32(obj.Status), Convert.ToInt32(obj.PARTNERID), UserInfo.PARTNERID, obj.TypeKeyword, obj.keyword);
                        ViewBag.MEDICINES = _MEDICINES;
                        break;
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
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListMedicineDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Danh sách thuốc", "/pharmacyStore/ListMedicine");
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

                        string result = impCMS_MEDICINE.DeleteCMS_MEDICINE(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("ListMedicineDelete: " + ex.ToString());
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
        public ActionResult ListMedicinePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Danh sách thuốc", "/pharmacyStore/ListMedicine");
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
                        impCMS_MEDICINE.PUBLICCMS_MEDICINE(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("ListGroupMedicinePublic: " + ex.ToString());
                return Json("Có lỗi trong quá trình duyệt");
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListMedicineAdd(string ID)
        {
            // Initialization.             
            AddBreadcrumb("Kho thuốc", "");

            CMS_MEDICINE _MEDICINE = new CMS_MEDICINE();
            List<CMS_MEDICINE> _MEDICINES = null;

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    AddPageHeader("Thêm mới thuốc", "");
                    AddBreadcrumb("Thêm mới thuốc", "/pharmacyStore/ListMedicineAdd");
                    ViewBag.Title = "Thêm mới thuốc";
                    _MEDICINE.ID = 0;
                    if (UserInfo.PARTNERID != 1)
                    {
                        _MEDICINE.PARTNERID = UserInfo.PARTNERID;
                    }
                    else
                    {
                        _MEDICINE.PARTNERID = 0;
                    }
                    _MEDICINE.MEDICINE_UNIT = 0;
                    _MEDICINE.MEDICINE_LOCATION = 0;
                    _MEDICINE.GROUP_MEDICINE = 0;
                    _MEDICINE.MEDICINE_TEMPERATURE = 742;
                    _MEDICINE.MEDICINE_INVENTORY = 0;



                }
                else
                {
                    AddPageHeader("Cập nhật thuốc", "");
                    AddBreadcrumb("Cập nhật thuốc", "/pharmacyStore/ListMedicineAdd");
                    ViewBag.Title = "Cập nhật thuốc";
                    _MEDICINES = impCMS_MEDICINE.GetCMS_MEDICINEByID(Convert.ToInt32(ID), UserInfo.PARTNERID);
                    if (_MEDICINES != null)
                    {
                        if (_MEDICINES.Count > 0)
                        {
                            return View(_MEDICINES[0]);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin thuốc";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin thuốc";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListMedicineAdd: " + ex.ToString());
            }

            // Info.  
            return View(_MEDICINE);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListMedicineAdd(CMS_MEDICINE _MEDICINE)
        {
            try
            {
                if (_MEDICINE.ID == 0)
                {
                    AddPageHeader("Thêm mới thuốc", "");
                    AddBreadcrumb("Thêm mới thuốc", "/pharmacyStore/ListMedicineAdd");
                    ViewBag.Title = "Thêm mới thuốc";
                }
                else
                {
                    AddPageHeader("Cập nhật thuốc", "");
                    AddBreadcrumb("Cập nhật thuốc", "/pharmacyStore/ListMedicineAdd");
                    ViewBag.Title = "Cập nhật thuốc";
                }

                if (ModelState.IsValid)
                {
                    var UserInfo = ((cms_Account)Session["UserInfo"]);


                    string result = string.Empty;
                    if (_MEDICINE.ID == 0)
                    {
                        bool checkExit = false;

                        List<CMS_MEDICINE> MEDICINEExit = impCMS_MEDICINE.GetCMS_MEDICINEByName(_MEDICINE.MEDICINE_NAME, UserInfo.PARTNERID);
                        if (MEDICINEExit != null)
                        {
                            if (MEDICINEExit.Count > 0)
                            {
                                checkExit = true;
                                ViewBag.TitleSuccsess = "Đã tồn tại thuốc rồi, Mời bạn kiểm tra lại!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                        }
                        if (!checkExit)
                        {
                            _MEDICINE.MEDICINE_CREATEBY = UserInfo.accountid;
                            _MEDICINE.MEDICINE_CREATEDATE = DateTime.Now;
                            _MEDICINE.MEDICINE_STATUS = 1;
                            if (_MEDICINE.MEDICINE_HUONGTHANCheck)
                                _MEDICINE.MEDICINE_HUONGTHAN = 1;
                            else
                                _MEDICINE.MEDICINE_HUONGTHAN = 0;

                            if (_MEDICINE.MEDICINE_INSURANCECheck)
                                _MEDICINE.MEDICINE_INSURANCE = 1;
                            else
                                _MEDICINE.MEDICINE_INSURANCE = 0;
                            _MEDICINE.MEDICINE_STATUS = 1;
                            _MEDICINE.PARTNERID = UserInfo.PARTNERID;
                            _MEDICINE.MEDICINE_INVENTORY = 0;
                            result = impCMS_MEDICINE.InsertCMS_MEDICINE(_MEDICINE);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới thuốc thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                Response.Redirect("/pharmacyStore/ListMedicine", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới thuốc không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
                            }
                        }
                    }
                    else
                    {
                        if (_MEDICINE.MEDICINE_INVENTORY == null)
                            _MEDICINE.MEDICINE_INVENTORY = 0;
                        _MEDICINE.MEDICINE_UPDATEBY = UserInfo.accountid;
                        _MEDICINE.MEDICINE_UPDATEDATE = DateTime.Now;

                        result = impCMS_MEDICINE.UpdateCMS_MEDICINE(_MEDICINE);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin thuốc thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/pharmacyStore/ListMedicine", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin thuốc không thành công";
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
                logError.Info("ListMedicineAdd: " + ex.ToString());
            }
            return View(_MEDICINE);
        }

        #endregion


        #region --> Danh sách thuốc nhập  
        public ActionResult ReceiptStore()
        {
            // Initialization.  
            AddPageHeader("Danh sách thuốc nhập", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách thuốc nhập", "/pharmacyStore/ReceiptStore");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                this.ViewBag.GetTypeStatus = CMSLIS.Common.Common.GetTypeStatus();

                List<CMS_MEDICINE_RECEIPT_STORE> _MEDICINE_RECEIPT_STORES = new List<CMS_MEDICINE_RECEIPT_STORE>();

                _MEDICINE_RECEIPT_STORES = impCMS_MEDICINE_RECEIPT_STORE.GetCMS_MEDICINE_RECEIPT_STOREByCODE(DateTime.Now.AddDays(-10), DateTime.Now.AddDays(1), string.Empty, UserInfo.PARTNERID);

                ViewBag.MEDICINE_RECEIPT_STORES = _MEDICINE_RECEIPT_STORES;
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
        public ActionResult ReceiptStore(pharmacyStoreViewModels obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách thuốc nhập", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách thuốc nhập", "/pharmacyStore/ReceiptStore");
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
                        case "SearchReceiptStore":

                            List<CMS_MEDICINE_RECEIPT_STORE> _MEDICINE_RECEIPT_STORES = new List<CMS_MEDICINE_RECEIPT_STORE>();

                            if (string.IsNullOrEmpty(obj.pharmacyStoreCode))
                            {
                                obj.pharmacyStoreCode = string.Empty;
                            }

                            _MEDICINE_RECEIPT_STORES = impCMS_MEDICINE_RECEIPT_STORE.GetCMS_MEDICINE_RECEIPT_STOREBYALL(Tungay, Denngay.AddDays(1), obj.pharmacyStoreCode.Trim(), obj.Status, obj.PARTNERID, UserInfo.PARTNERID);

                            ViewBag.MEDICINE_RECEIPT_STORES = _MEDICINE_RECEIPT_STORES;
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
        public ActionResult ReceiptStoreDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Danh sách thuốc nhập", "/pharmacyStore/ReceiptStore");
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
                        string result = impCMS_MEDICINE_RECEIPT_STORE.DeleteCMS_MEDICINE_RECEIPT_STORE(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("ReceiptStoreDelete: " + ex.ToString());
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
        public ActionResult ReceiptStorePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Danh sách thuốc", "/pharmacyStore/ListMedicine");
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
                        impCMS_MEDICINE_RECEIPT_STORE.PUBLICCMS_MEDICINE_RECEIPT_STORE(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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


        public ActionResult ReceiptStoreAdd(string ID)
        {
            // Initialization.             
            AddBreadcrumb("Kho thuốc", "");
            CMS_MEDICINE_RECEIPT_STORE _MEDICINE_RECEIPT_STORE = new CMS_MEDICINE_RECEIPT_STORE();

            List<CMS_MEDICINE_RECEIPT_STORE> _MEDICINE_RECEIPT_STORES = null;
            List<CMS_MEDICINE_RECEIPT_DETAIL> _MEDICINE_RECEIPT_DETAILS = null;


            CMS_Core.Models.ReceiptStoreViewModel obj = new CMS_Core.Models.ReceiptStoreViewModel();
            obj._MEDICINE_RECEIPT_STORE = new CMS_MEDICINE_RECEIPT_STORE();
            obj._MEDICINE_RECEIPT_DETAILS = new List<CMS_MEDICINE_RECEIPT_DETAIL>();
            obj.MEDICINE_RECEIPT_DETAIL = new CMS_MEDICINE_RECEIPT_DETAIL();


            Int64 SEQ_STORE = impCMS_MEDICINE_RECEIPT_STORE.GET_MEDICINE_RECEIPT_STORE_SEQ();


            Session["PhieuNhapThuoc"] = null;
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    AddPageHeader("Nhập thuốc vào kho", "");
                    AddBreadcrumb("Nhập thuốc vào kho", "/pharmacyStore/ReceiptStoreAdd");
                    ViewBag.Title = "Nhập thuốc vào kho";
                    _MEDICINE_RECEIPT_STORE.ID = 0;
                    if (UserInfo.PARTNERID != 1)
                    {
                        _MEDICINE_RECEIPT_STORE.PARTNERID = UserInfo.PARTNERID;
                    }
                    else
                    {
                        _MEDICINE_RECEIPT_STORE.PARTNERID = 0;
                    }

                    obj._MEDICINE_RECEIPT_STORE = _MEDICINE_RECEIPT_STORE;
                    obj.MEDICINE_RECEIPT_DETAIL.MEDICINE_ID = 0;
                    obj._MEDICINE_RECEIPT_STORE.MEDICINE_RECEIPT_STORE_CODE = DateTime.Now.ToString("ddMMyy") + "_" + UserInfo.PARTNER_CODE.ToUpper() + "_" + SEQ_STORE;
                    obj._MEDICINE_RECEIPT_STORE.CONTRACT_DATE_VIEW = DateTime.Now.ToString("dd/MM/yyyy");


                }
                else
                {
                    AddPageHeader("Nhập thuốc vào kho", "");
                    AddBreadcrumb("Cập nhật nhập thuốc vào kho", "/pharmacyStore/ReceiptStoreAdd");
                    ViewBag.Title = "Cập nhật nhập thuốc vào kho";
                    _MEDICINE_RECEIPT_STORES = impCMS_MEDICINE_RECEIPT_STORE.GetCMS_MEDICINE_RECEIPT_STOREByID(Convert.ToInt32(ID), UserInfo.PARTNERID);
                    if (_MEDICINE_RECEIPT_STORES != null)
                    {
                        if (_MEDICINE_RECEIPT_STORES.Count > 0)
                        {
                            obj._MEDICINE_RECEIPT_STORE = _MEDICINE_RECEIPT_STORES[0];

                            _MEDICINE_RECEIPT_DETAILS = impCMS_MEDICINE_RECEIPT_DETAIL.GetCMS_MEDICINE_RECEIPT_DETAILBySTORE_ID(obj._MEDICINE_RECEIPT_STORE.ID, UserInfo.PARTNERID);
                            if (_MEDICINE_RECEIPT_DETAILS != null)
                            {
                                if (_MEDICINE_RECEIPT_DETAILS.Count > 0)
                                {
                                    obj._MEDICINE_RECEIPT_DETAILS = _MEDICINE_RECEIPT_DETAILS;

                                    if (Session["PhieuNhapThuoc"] != null)
                                    {
                                        Session["PhieuNhapThuoc"] = null;
                                    }

                                    Session["PhieuNhapThuoc"] = _MEDICINE_RECEIPT_DETAILS;


                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin nhập thuốc";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy thông tin nhập thuốc";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }

                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin nhập thuốc";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin nhập thuốc";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }



                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ReceiptStoreAdd: " + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetInfoMedicine(string ID)
        {
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                IEnumerable<CMS_MEDICINE> modelList = new List<CMS_MEDICINE>();


                modelList = impCMS_MEDICINE.GetCMS_MEDICINEByID(Convert.ToInt32(ID), UserInfo.PARTNERID);

                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("GetInfoMedicine: " + ID + "  " + ex.ToString());
                return Json(string.Empty);
            }
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ReceiptStoreDeleteRECEIPT_DETAILS(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Nhập thuốc vào kho", "/pharmacyStore/ReceiptStoreAdd");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    if (Session["PhieuNhapThuoc"] != null)
                    {
                        var toRemove = new List<CMS_MEDICINE_RECEIPT_DETAIL>();

                        List<CMS_MEDICINE_RECEIPT_DETAIL> _MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                        foreach (string customerID in customerIDs)
                        {
                            try
                            {

                                foreach (CMS_MEDICINE_RECEIPT_DETAIL MEDICINE_RECEIPT_DETAIL in _MEDICINE_RECEIPT_DETAILS.ToList())
                                {
                                    if (customerID.Equals(MEDICINE_RECEIPT_DETAIL.MEDICINE_ID.ToString().Trim()))
                                    {
                                        List<CMS_MEDICINE_RECEIPT_DETAIL> data = impCMS_MEDICINE_RECEIPT_DETAIL.GetCMS_MEDICINE_RECEIPT_DETAILByMEDICINE_CODE(MEDICINE_RECEIPT_DETAIL.MEDICINE_CODE, UserInfo.PARTNERID);
                                        bool checkExit = false;
                                        if (data != null)
                                        {
                                            if (data.Count > 0)
                                            {
                                                checkExit = true;
                                            }
                                        }
                                        if (checkExit)
                                        {
                                            if (MEDICINE_RECEIPT_DETAIL.AMOUNT > data[0].TOTAL_AMOUNT)
                                            {
                                                return Json("Không xóa được do số tồn kho nhỏ hơn số thuốc xóa");
                                            }
                                            else
                                            {
                                                toRemove.Add(MEDICINE_RECEIPT_DETAIL);
                                                // Xóa hẳn dữ liệu ra khỏi phiếu nhập
                                                if (MEDICINE_RECEIPT_DETAIL.RECEIPT_STORE_ID > 0)
                                                {
                                                    impCMS_MEDICINE_RECEIPT_DETAIL.DeleteCMS_MEDICINE_RECEIPT_DETAIL(customerID, MEDICINE_RECEIPT_DETAIL.RECEIPT_STORE_ID, UserInfo.PARTNERID);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            toRemove.Add(MEDICINE_RECEIPT_DETAIL);
                                            // Xóa hẳn dữ liệu ra khỏi phiếu nhập
                                            if (MEDICINE_RECEIPT_DETAIL.RECEIPT_STORE_ID > 0)
                                            {
                                                impCMS_MEDICINE_RECEIPT_DETAIL.DeleteCMS_MEDICINE_RECEIPT_DETAIL(customerID, MEDICINE_RECEIPT_DETAIL.RECEIPT_STORE_ID, UserInfo.PARTNERID);
                                            }
                                        }
                                    }
                                }
                            }
                            catch { }
                        }

                        _MEDICINE_RECEIPT_DETAILS.RemoveAll(toRemove.Contains);

                        Session["PhieuNhapThuoc"] = _MEDICINE_RECEIPT_DETAILS;
                        return Json("Xóa thành công");
                    }
                    else
                    {
                        return Json("Xóa không thành công");
                    }
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("ReceiptStoreDelete:" + ex.ToString());
                return Json("Xóa không thành công");
            }

            // return Json("Xóa thành công");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceiptStoreAdd(CMS_Core.Models.ReceiptStoreViewModel obj, string submit)
        {
            try
            {

                AddPageHeader("Nhập thuốc vào kho", "");
                AddBreadcrumb("Kho thuốc", "/pharmacyStore");
                AddBreadcrumb("Nhập thuốc vào kho", "/pharmacyStore/ReceiptStoreAdd");
                var UserInfo = ((cms_Account)Session["UserInfo"]);



                switch (submit)
                {
                    case "SaveReceiptStoreAdd":
                        if (Session["PhieuNhapThuoc"] != null)
                        {
                            obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                        }

                        var errorsSave = obj._MEDICINE_RECEIPT_STORE.Validate(new ValidationContext(obj._MEDICINE_RECEIPT_STORE));
                        if (errorsSave.ToList().Count == 0)
                        {

                            // Insert
                            if (obj._MEDICINE_RECEIPT_STORE.ID == 0)
                            {
                                if (obj._MEDICINE_RECEIPT_DETAILS != null)
                                {
                                    float total = 0;
                                    float totalPay = 0;
                                    foreach (var data in obj._MEDICINE_RECEIPT_DETAILS)
                                    {
                                        total += (data.EXPORTPRICE ?? 0) * (data.AMOUNT ?? 0);
                                        totalPay += (data.IMPORTPRICE ?? 0) * (data.AMOUNT ?? 0);
                                    }
                                    obj._MEDICINE_RECEIPT_STORE.TOTAL_PRICE = total;
                                    obj._MEDICINE_RECEIPT_STORE.TOTAL_PAYMENTS = totalPay;
                                    obj._MEDICINE_RECEIPT_STORE.CREATEBY = UserInfo.accountid;
                                    obj._MEDICINE_RECEIPT_STORE.PARTNERID = UserInfo.PARTNERID;
                                    obj._MEDICINE_RECEIPT_STORE.CREATEDATE = DateTime.Now;
                                    obj._MEDICINE_RECEIPT_STORE.STATUS = 1;


                                    List<CMS_MEDICINE_RECEIPT_STORE> ListRECEIPT_STORE = impCMS_MEDICINE_RECEIPT_STORE.GetCMS_MEDICINE_RECEIPT_STOREByCODE(obj._MEDICINE_RECEIPT_STORE.MEDICINE_RECEIPT_STORE_CODE, UserInfo.PARTNERID);
                                    bool checkExit = false;
                                    if (ListRECEIPT_STORE != null)
                                    {
                                        if (ListRECEIPT_STORE.Count > 0)
                                        { checkExit = true; }
                                    }

                                    if (!checkExit)
                                    {
                                        string idRECEIPT_STORE = impCMS_MEDICINE_RECEIPT_STORE.InsertCMS_MEDICINE_RECEIPT_STORE(obj._MEDICINE_RECEIPT_STORE);
                                        if (!string.IsNullOrEmpty(idRECEIPT_STORE))
                                        {

                                            foreach (var data in obj._MEDICINE_RECEIPT_DETAILS)
                                            {
                                                data.RECEIPT_STORE_ID = Convert.ToInt32(idRECEIPT_STORE);

                                                data.CREATEBY = UserInfo.accountid;
                                                data.CREATEDATE = DateTime.Now;
                                                data.PARTNERID = UserInfo.PARTNERID;
                                                data.MEDICINE_CODE = data.MEDICINE_ID.ToString();
                                                var errorsRECEIPT_DETAIL = data.Validate(new ValidationContext(data));
                                                if (errorsRECEIPT_DETAIL.ToList().Count == 0)
                                                {
                                                    impCMS_MEDICINE_RECEIPT_DETAIL.InsertCMS_MEDICINE_RECEIPT_DETAIL(data);
                                                }
                                            }

                                            ViewBag.TitleSuccsess = "Cập nhật thành công!";
                                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                            Response.Redirect("/pharmacyStore/ReceiptStore", false);
                                        }
                                        else
                                        {
                                            ViewBag.TitleSuccsess = "Lỗi cập nhật dữ liệu";
                                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.TitleSuccsess = "Đã cập nhật dữ liệu rồi, Mời bạn kiểm tra lại";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    }

                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Bạn chưa nhập thuốc vào kho";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }  // update
                            else
                            {


                                if (obj._MEDICINE_RECEIPT_DETAILS != null)
                                {
                                    float total = 0;
                                    float totalPay = 0;
                                    foreach (var data in obj._MEDICINE_RECEIPT_DETAILS)
                                    {
                                        total += (data.EXPORTPRICE ?? 0) * (data.AMOUNT ?? 0);
                                        totalPay += (data.IMPORTPRICE ?? 0) * (data.AMOUNT ?? 0);
                                    }
                                    obj._MEDICINE_RECEIPT_STORE.TOTAL_PRICE = total;
                                    obj._MEDICINE_RECEIPT_STORE.TOTAL_PAYMENTS = totalPay;
                                    obj._MEDICINE_RECEIPT_STORE.UPDATEBY = UserInfo.accountid;
                                    obj._MEDICINE_RECEIPT_STORE.PARTNERID = UserInfo.PARTNERID;
                                    obj._MEDICINE_RECEIPT_STORE.UPDATEDATE = DateTime.Now;


                                    impCMS_MEDICINE_RECEIPT_STORE.UpdateCMS_MEDICINE_RECEIPT_STORE(obj._MEDICINE_RECEIPT_STORE);


                                    foreach (var data in obj._MEDICINE_RECEIPT_DETAILS)
                                    {
                                        if (data.ID != 0)
                                        {
                                            data.RECEIPT_STORE_ID = Convert.ToInt32(obj._MEDICINE_RECEIPT_STORE.ID);
                                            data.UPDATEBY = UserInfo.accountid;
                                            data.UPDATEDATE = DateTime.Now;
                                            data.PARTNERID = UserInfo.PARTNERID;
                                            data.MEDICINE_CODE = data.MEDICINE_ID.ToString();
                                            var errorsRECEIPT_DETAIL = data.Validate(new ValidationContext(data));
                                            if (errorsRECEIPT_DETAIL.ToList().Count == 0)
                                            {
                                                impCMS_MEDICINE_RECEIPT_DETAIL.UpdateCMS_MEDICINE_RECEIPT_DETAIL(data);
                                            }
                                        }
                                        else
                                        {
                                            data.RECEIPT_STORE_ID = Convert.ToInt32(obj._MEDICINE_RECEIPT_STORE.ID);
                                            data.CREATEBY = UserInfo.accountid;
                                            data.CREATEDATE = DateTime.Now;
                                            data.PARTNERID = UserInfo.PARTNERID;
                                            data.MEDICINE_CODE = data.MEDICINE_ID.ToString();
                                            var errorsRECEIPT_DETAIL = data.Validate(new ValidationContext(data));
                                            if (errorsRECEIPT_DETAIL.ToList().Count == 0)
                                            {
                                                impCMS_MEDICINE_RECEIPT_DETAIL.InsertCMS_MEDICINE_RECEIPT_DETAIL(data);
                                            }
                                        }
                                    }

                                    ViewBag.TitleSuccsess = "Cập nhật thành công!";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;

                                    Response.Redirect("/pharmacyStore/ReceiptStore", false);

                                }

                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = errorsSave.ToList()[0].ErrorMessage;
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }


                        break;

                    case "SaveReceiptStoreChoi":

                        DateTime ngaysanxuat = new DateTime();
                        DateTime hansudung = new DateTime();
                        bool checkValidate = true;
                        #region Check input data
                        try
                        {
                            if (string.IsNullOrEmpty(obj.MEDICINE_RECEIPT_DETAIL.MEDICINE_NAME))
                            {
                                ViewBag.TitleSuccsess = "Mời bạn chọn tên thuốc";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                checkValidate = false;
                                if (Session["PhieuNhapThuoc"] != null)
                                {
                                    obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                                }
                                break;
                            }

                            if (!string.IsNullOrEmpty(obj.MEDICINE_RECEIPT_DETAIL.DATEOFMANUFACTURE_View.Trim()))
                                ngaysanxuat = DateTime.ParseExact(obj.MEDICINE_RECEIPT_DETAIL.DATEOFMANUFACTURE_View.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                            if (!string.IsNullOrEmpty(obj.MEDICINE_RECEIPT_DETAIL.EXPIRYDATE_View.Trim()))
                                hansudung = DateTime.ParseExact(obj.MEDICINE_RECEIPT_DETAIL.EXPIRYDATE_View.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                            if (ngaysanxuat > DateTime.Now)
                            {
                                ViewBag.TitleSuccsess = "Ngày sản xuất phải nhỏ hơn ngày hiện tại";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                checkValidate = false;
                                if (Session["PhieuNhapThuoc"] != null)
                                {
                                    obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                                }
                                break;
                            }
                            if (hansudung < DateTime.Now)
                            {
                                ViewBag.TitleSuccsess = "Hạn sử dụng phải lớn hơn ngày hiện tại";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                checkValidate = false;
                                if (Session["PhieuNhapThuoc"] != null)
                                {
                                    obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                                }
                                break;
                            }

                            if (ngaysanxuat > hansudung)
                            {
                                ViewBag.TitleSuccsess = "Ngày sản xuất phải nhỏ hơn hạn sử dụng";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                checkValidate = false;
                                if (Session["PhieuNhapThuoc"] != null)
                                {
                                    obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                                }
                                break;
                            }
                            if (obj.MEDICINE_RECEIPT_DETAIL.ID > 0)
                            {

                                List<CMS_MEDICINE_RECEIPT_DETAIL> dataOld = impCMS_MEDICINE_RECEIPT_DETAIL.GetCMS_MEDICINE_RECEIPT_DETAILByID(obj.MEDICINE_RECEIPT_DETAIL.ID, UserInfo.PARTNERID);
                                if (dataOld != null)
                                {
                                    if (dataOld.Count > 0)
                                    {
                                        if (dataOld[0].AMOUNT - obj.MEDICINE_RECEIPT_DETAIL.AMOUNT > dataOld[0].TOTAL_AMOUNT)
                                        {
                                            ViewBag.TitleSuccsess = "Số thuốc hủy lớn hơn số thuốc tồn kho";
                                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                            checkValidate = false;
                                            if (Session["PhieuNhapThuoc"] != null)
                                            {
                                                obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                                            }
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                        catch
                        {
                            ViewBag.TitleSuccsess = "Dữ liệu ngày tháng không đúng định dạng";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            checkValidate = false;
                            if (Session["PhieuNhapThuoc"] != null)
                            {
                                obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                            }
                            break;
                        }
                        #endregion

                        if (checkValidate)
                        {
                            obj.MEDICINE_RECEIPT_DETAIL.EXPIRYDATE = hansudung;
                            obj.MEDICINE_RECEIPT_DETAIL.DATEOFMANUFACTURE = ngaysanxuat;
                            if (Session["PhieuNhapThuoc"] != null)
                            {
                                obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                            }
                            else
                            {
                                obj._MEDICINE_RECEIPT_DETAILS = new List<CMS_MEDICINE_RECEIPT_DETAIL>();
                            }

                            var errors = obj.MEDICINE_RECEIPT_DETAIL.Validate(new ValidationContext(obj.MEDICINE_RECEIPT_DETAIL));
                            if (errors.ToList().Count == 0)
                            {

                                if (obj._MEDICINE_RECEIPT_DETAILS.Where(x => x.MEDICINE_NAME.Equals(obj.MEDICINE_RECEIPT_DETAIL.MEDICINE_NAME)).Count() > 0)
                                {
                                    var queryLondonCustomers = from cust in obj._MEDICINE_RECEIPT_DETAILS
                                                               where cust.MEDICINE_NAME.Equals(obj.MEDICINE_RECEIPT_DETAIL.MEDICINE_NAME)
                                                               select cust;
                                    queryLondonCustomers.ElementAt(0);
                                    //Lấy ID bản ghi trước chèn vào;
                                    obj.MEDICINE_RECEIPT_DETAIL.ID = queryLondonCustomers.ElementAt(0).ID;
                                    obj._MEDICINE_RECEIPT_DETAILS.Remove(queryLondonCustomers.ElementAt(0));
                                }

                                obj._MEDICINE_RECEIPT_DETAILS.Add(obj.MEDICINE_RECEIPT_DETAIL);
                                Session["PhieuNhapThuoc"] = obj._MEDICINE_RECEIPT_DETAILS;
                                ViewBag.TitleSuccsess = "Chọn thêm thuốc thành công!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                ModelState.Clear();


                            }
                            else
                            {
                                if (Session["PhieuNhapThuoc"] != null)
                                {
                                    obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                                }
                                ViewBag.TitleSuccsess = errors.ToList()[0].ErrorMessage;
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;

                            }
                            obj.MEDICINE_RECEIPT_DETAIL = new CMS_MEDICINE_RECEIPT_DETAIL();
                        }
                        else
                        {
                            if (Session["PhieuNhapThuoc"] != null)
                            {
                                obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                            }
                        }
                        break;
                    case "SaveReceiptStoreDelete":
                        if (Session["PhieuNhapThuoc"] != null)
                        {
                            obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                        }
                        obj.MEDICINE_RECEIPT_DETAIL = new CMS_MEDICINE_RECEIPT_DETAIL();

                        break;


                }


            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi trong quá trình cập nhật";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListMedicineAdd: " + ex.ToString());
                if (Session["PhieuNhapThuoc"] != null)
                {
                    obj._MEDICINE_RECEIPT_DETAILS = (List<CMS_MEDICINE_RECEIPT_DETAIL>)Session["PhieuNhapThuoc"];
                }
            }
            return View(obj);
        }

        public ActionResult ReceiptStoreView(string ID, string Token)
        {
            // Initialization.             
            AddBreadcrumb("Kho thuốc", "");
            AddPageHeader("Nhập thuốc vào kho", "");
            AddBreadcrumb("Cập nhật nhập thuốc vào kho", "/pharmacyStore/ReceiptStoreView");
            ViewBag.Title = "Cập nhật nhập thuốc vào kho";
            CMS_MEDICINE_RECEIPT_STORE _MEDICINE_RECEIPT_STORE = new CMS_MEDICINE_RECEIPT_STORE();

            List<CMS_MEDICINE_RECEIPT_STORE> _MEDICINE_RECEIPT_STORES = null;
            List<CMS_MEDICINE_RECEIPT_DETAIL> _MEDICINE_RECEIPT_DETAILS = null;

            CMS_Core.Models.ReceiptStoreViewModel obj = new CMS_Core.Models.ReceiptStoreViewModel();
            obj._MEDICINE_RECEIPT_STORE = new CMS_MEDICINE_RECEIPT_STORE();
            obj._MEDICINE_RECEIPT_DETAILS = new List<CMS_MEDICINE_RECEIPT_DETAIL>();
            obj.MEDICINE_RECEIPT_DETAIL = new CMS_MEDICINE_RECEIPT_DETAIL();


            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (!string.IsNullOrEmpty(ID))
                {

                    if (CMSLIS.Common.Common.CheckKeyPrivate(ID, Token))
                    {
                        _MEDICINE_RECEIPT_STORES = impCMS_MEDICINE_RECEIPT_STORE.GetCMS_MEDICINE_RECEIPT_STOREByID(Convert.ToInt32(ID), UserInfo.PARTNERID);
                        if (_MEDICINE_RECEIPT_STORES != null)
                        {
                            if (_MEDICINE_RECEIPT_STORES.Count > 0)
                            {
                                obj._MEDICINE_RECEIPT_STORE = _MEDICINE_RECEIPT_STORES[0];

                                _MEDICINE_RECEIPT_DETAILS = impCMS_MEDICINE_RECEIPT_DETAIL.GetCMS_MEDICINE_RECEIPT_DETAILBySTORE_ID(obj._MEDICINE_RECEIPT_STORE.ID, UserInfo.PARTNERID);
                                if (_MEDICINE_RECEIPT_DETAILS != null)
                                {
                                    if (_MEDICINE_RECEIPT_DETAILS.Count > 0)
                                    {
                                        obj._MEDICINE_RECEIPT_DETAILS = _MEDICINE_RECEIPT_DETAILS;
                                    }
                                    else
                                    {
                                        ViewBag.StatusMessage = "Không tìm thấy thông tin nhập thuốc";
                                    }
                                }
                                else
                                {
                                    ViewBag.StatusMessage = "Không tìm thấy thông tin nhập thuốc";
                                }

                            }
                            else
                            {
                                ViewBag.StatusMessage = "Không tìm thấy thông tin nhập thuốc";
                            }

                        }
                        else
                        {
                            ViewBag.StatusMessage = "Không tìm thấy thông tin nhập thuốc";
                        }
                    }
                    else
                    {
                        ViewBag.StatusMessage = "Dữ liệu nhập vào không đúng, mời bạn kiểm tra lại!";
                    }
                }
                else
                {
                    ViewBag.StatusMessage = "Không tìm thấy thông tin nhập thuốc";
                }
            }
            catch (Exception ex)
            {
                logError.Info("ReceiptStoreView: " + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        public ActionResult ReceiptStoreViewPrint(string ID, string Token)
        {
            // Initialization.             
            AddBreadcrumb("Kho thuốc", "");
            AddPageHeader("Nhập thuốc vào kho", "");
            AddBreadcrumb("Cập nhật nhập thuốc vào kho", "/pharmacyStore/ReceiptStoreViewPrint");
            ViewBag.Title = "Cập nhật nhập thuốc vào kho";

            CMS_MEDICINE_RECEIPT_STORE _MEDICINE_RECEIPT_STORE = new CMS_MEDICINE_RECEIPT_STORE();

            List<CMS_MEDICINE_RECEIPT_STORE> _MEDICINE_RECEIPT_STORES = null;
            List<CMS_MEDICINE_RECEIPT_DETAIL> _MEDICINE_RECEIPT_DETAILS = null;

            CMS_Core.Models.ReceiptStoreViewModel obj = new CMS_Core.Models.ReceiptStoreViewModel();
            obj._MEDICINE_RECEIPT_STORE = new CMS_MEDICINE_RECEIPT_STORE();
            obj._MEDICINE_RECEIPT_DETAILS = new List<CMS_MEDICINE_RECEIPT_DETAIL>();
            obj.MEDICINE_RECEIPT_DETAIL = new CMS_MEDICINE_RECEIPT_DETAIL();


            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    if (CMSLIS.Common.Common.CheckKeyPrivate(ID, Token))
                    {
                        _MEDICINE_RECEIPT_STORES = impCMS_MEDICINE_RECEIPT_STORE.GetCMS_MEDICINE_RECEIPT_STOREByID(Convert.ToInt32(ID), UserInfo.PARTNERID);
                        if (_MEDICINE_RECEIPT_STORES != null)
                        {
                            if (_MEDICINE_RECEIPT_STORES.Count > 0)
                            {
                                obj._MEDICINE_RECEIPT_STORE = _MEDICINE_RECEIPT_STORES[0];

                                _MEDICINE_RECEIPT_DETAILS = impCMS_MEDICINE_RECEIPT_DETAIL.GetCMS_MEDICINE_RECEIPT_DETAILBySTORE_ID(obj._MEDICINE_RECEIPT_STORE.ID, UserInfo.PARTNERID);
                                if (_MEDICINE_RECEIPT_DETAILS != null)
                                {
                                    if (_MEDICINE_RECEIPT_DETAILS.Count > 0)
                                    {
                                        obj._MEDICINE_RECEIPT_DETAILS = _MEDICINE_RECEIPT_DETAILS;
                                    }
                                    else
                                    {
                                        ViewBag.StatusMessage = "Không tìm thấy thông tin nhập thuốc";
                                    }
                                }
                                else
                                {
                                    ViewBag.StatusMessage = "Không tìm thấy thông tin nhập thuốc";
                                }

                            }
                            else
                            {
                                ViewBag.StatusMessage = "Không tìm thấy thông tin nhập thuốc";
                            }
                        }
                        else
                        {
                            ViewBag.StatusMessage = "Không tìm thấy thông tin nhập thuốc";
                        }
                    }
                    else
                    {
                        ViewBag.StatusMessage = "Dữ liệu nhập vào không đúng, mời bạn kiểm tra lại";
                    }
                }
                else
                {
                    ViewBag.StatusMessage = "Không tìm thấy thông tin nhập thuốc";
                }
            }
            catch (Exception ex)
            {
                logError.Info("ReceiptStoreView: " + ex.ToString());
            }

            // Info.  
            //return View(obj);

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


        #region --> Danh sách xuất thuốc

        public ActionResult ExportStore()
        {
            // Initialization.  
            AddPageHeader("Danh sách thuốc xuất", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách thuốc xuất", "/pharmacyStore/ExportStore");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                this.ViewBag.GetTypeStatus = CMSLIS.Common.Common.GetTypeStatus();

                List<CMS_MEDICINE_EXPORT_STORE> _CMS_MEDICINE_EXPORT_STORES = new List<CMS_MEDICINE_EXPORT_STORE>();

                _CMS_MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByCODE(DateTime.Now.AddDays(-10), DateTime.Now.AddDays(1), string.Empty, UserInfo.PARTNERID);

                ViewBag.CMS_MEDICINE_EXPORT_STORES = _CMS_MEDICINE_EXPORT_STORES;
            }
            catch (Exception ex)
            {
                logError.Info("ExportStore: " + ex.ToString());
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
            obj.enddate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            obj.pharmacyStoreCode = string.Empty;
            obj.Status = CMSLIS.Common.Constant.TypeStatusAll;
            // Info.  
            return View(obj);

        }

        [HttpPost]
        public ActionResult ExportStore(pharmacyStoreViewModels obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách thuốc xuất", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách thuốc xuất", "/pharmacyStore/ExportStore");
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
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                    ViewBag.TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            catch
            {
                TextErr = "Dữ liệu ngày tháng không đúng định dạng";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }
            #endregion

            if (string.IsNullOrEmpty(TextErr))
            {

                try
                {
                    switch (submit)
                    {
                        case "SearchExportStore":
                            List<CMS_MEDICINE_EXPORT_STORE> _CMS_MEDICINE_EXPORT_STORES = new List<CMS_MEDICINE_EXPORT_STORE>();
                            if (string.IsNullOrEmpty(obj.pharmacyStoreCode))
                            {
                                obj.pharmacyStoreCode = string.Empty;
                            }
                            _CMS_MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByALL(Tungay, Denngay.AddDays(1), obj.pharmacyStoreCode.Trim(), obj.Status, obj.PARTNERID, UserInfo.PARTNERID);
                            ViewBag.CMS_MEDICINE_EXPORT_STORES = _CMS_MEDICINE_EXPORT_STORES;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.TextErr = "Có lỗi xẩy tra trong quá trình thực hiện. Mời bạn thực hiện lại!";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    logError.Info("ReceiptStore: " + ex.ToString());
                }
            }
            else
            {
                ViewBag.TextErr = TextErr;
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }
            // Info.  
            return View(obj);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ExportStoreDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Danh sách thuốc nhập", "/pharmacyStore/ReceiptStore");
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
                        string result = impCMS_MEDICINE_EXPORT_STORE.DeleteCMS_MEDICINE_EXPORT_STORE(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
                logError.Info("ReceiptStoreDelete: " + ex.ToString());
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
        public ActionResult ExportStorePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Danh sách thuốc", "/pharmacyStore/ListMedicine");
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
                        impCMS_MEDICINE_EXPORT_STORE.PUBLICCMS_MEDICINE_EXPORT_STORE(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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


        public ActionResult ExportStoreAdd(string ID)
        {
            // Initialization.             
            AddBreadcrumb("Kho thuốc", "");
            CMS_MEDICINE_EXPORT_STORE _MEDICINE_EXPORT_STORE = new CMS_MEDICINE_EXPORT_STORE();

            List<CMS_MEDICINE_EXPORT_STORE> _MEDICINE_EXPORT_STORES = null;
            List<CMS_MEDICINE_EXPORT_DETAIL> __MEDICINE_EXPORT_DETAILS = null;

            CMS_Core.Models.ExportStoreViewModel obj = new CMS_Core.Models.ExportStoreViewModel();



            obj._MEDICINE_EXPORT_STORE = new CMS_MEDICINE_EXPORT_STORE();

            obj._MEDICINE_EXPORT_DETAILS = new List<CMS_MEDICINE_EXPORT_DETAIL>();
            obj.MEDICINE_EXPORT_DETAIL = new CMS_MEDICINE_EXPORT_DETAIL();


            Int64 SEQ_STORE = impCMS_MEDICINE_EXPORT_STORE.GET_CMS_MEDICINE_EXPORT_STORE_SEQ();


            Session["PhieuXuatThuoc"] = null;
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    AddPageHeader("Phiếu xuất thuốc", "");
                    AddBreadcrumb("Phiếu xuất thuốc", "/pharmacyStore/ExportStoreAdd");
                    ViewBag.Title = "Phiếu xuất thuốc";
                    _MEDICINE_EXPORT_STORE.ID = 0;
                    if (UserInfo.PARTNERID != 1)
                    {
                        _MEDICINE_EXPORT_STORE.PARTNERID = UserInfo.PARTNERID;
                    }
                    else
                    {
                        _MEDICINE_EXPORT_STORE.PARTNERID = 0;
                    }

                    obj._MEDICINE_EXPORT_STORE = _MEDICINE_EXPORT_STORE;
                    obj.MEDICINE_EXPORT_DETAIL.MEDICINE_ID = 0;
                    obj._MEDICINE_EXPORT_STORE.CUSTOMER_GENDER = 2;
                    obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_CODE = DateTime.Now.ToString("ddMMyy") + "_" + UserInfo.PARTNER_CODE.ToUpper() + "_" + SEQ_STORE;


                }
                else
                {
                    AddPageHeader("Phiếu xuất thuốc", "");
                    AddBreadcrumb("Phiếu xuất thuốc", "/pharmacyStore/ExportStoreAdd");
                    ViewBag.Title = "Phiếu xuất thuốc";

                    _MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByID(Convert.ToInt32(ID), UserInfo.PARTNERID);
                    if (_MEDICINE_EXPORT_STORES != null)
                    {
                        if (_MEDICINE_EXPORT_STORES.Count > 0)
                        {
                            obj._MEDICINE_EXPORT_STORE = _MEDICINE_EXPORT_STORES[0];

                            __MEDICINE_EXPORT_DETAILS = impCMS_MEDICINE_EXPORT_DETAIL.GetCMS_MEDICINE_EXPORT_DETAILBySTORE_ID(obj._MEDICINE_EXPORT_STORE.ID, UserInfo.PARTNERID);
                            if (__MEDICINE_EXPORT_DETAILS != null)
                            {
                                if (__MEDICINE_EXPORT_DETAILS.Count > 0)
                                {
                                    obj._MEDICINE_EXPORT_DETAILS = __MEDICINE_EXPORT_DETAILS;

                                    Session["PhieuXuatThuoc"] = __MEDICINE_EXPORT_DETAILS;
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Không tìm thấy thông tin xuất thuốc";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Không tìm thấy thông tin xuất thuốc";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }

                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin xuất thuốc";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin xuất thuốc";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;

                logError.Info("ExportStoreAdd: " + ex.ToString());
            }

            // Info.  
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportStoreAdd(CMS_Core.Models.ExportStoreViewModel obj, string submit)
        {
            try
            {

                AddPageHeader("Phiếu xuất thuốc", "");
                AddBreadcrumb("Kho thuốc", "");
                AddBreadcrumb("Phiếu xuất thuốc", "/pharmacyStore/ExportStoreAdd");
                var UserInfo = ((cms_Account)Session["UserInfo"]);


                switch (submit)
                {
                    case "SaveExportStoreAdd":
                        if (Session["PhieuXuatThuoc"] != null)
                        {
                            obj._MEDICINE_EXPORT_DETAILS = (List<CMS_MEDICINE_EXPORT_DETAIL>)Session["PhieuXuatThuoc"];
                        }
                        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

                        obj._MEDICINE_EXPORT_STORE.CUSTOMER_NAME = myTI.ToTitleCase(obj._MEDICINE_EXPORT_STORE.CUSTOMER_NAME);


                        var errorsSave = obj._MEDICINE_EXPORT_STORE.Validate(new ValidationContext(obj._MEDICINE_EXPORT_STORE));


                        if (errorsSave.ToList().Count == 0)
                        {
                            if (obj._MEDICINE_EXPORT_DETAILS != null)
                            {
                                float total = 0;

                                foreach (var data in obj._MEDICINE_EXPORT_DETAILS)
                                {
                                    total += (data.MEDICINE_PRICE ?? 0) * (data.MEDICINE_AMOUNT ?? 0);

                                }
                                obj._MEDICINE_EXPORT_STORE.TOTAL_PRICE = total;
                                obj._MEDICINE_EXPORT_STORE.CREATEBY = UserInfo.accountid;
                                obj._MEDICINE_EXPORT_STORE.PARTNERID = UserInfo.PARTNERID;
                                obj._MEDICINE_EXPORT_STORE.CREATEDATE = DateTime.Now;
                                obj._MEDICINE_EXPORT_STORE.STATUS = 1;


                                if (obj._MEDICINE_EXPORT_STORE.ID == 0)
                                {

                                    List<CMS_MEDICINE_EXPORT_STORE> ListRECEIPT_STORE = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByCODE(obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_CODE, UserInfo.PARTNERID);
                                    bool checkExit = false;
                                    if (ListRECEIPT_STORE != null)
                                    {
                                        if (ListRECEIPT_STORE.Count > 0)
                                        { checkExit = true; }
                                    }

                                    if (!checkExit)
                                    {
                                        string idRECEIPT_STORE = impCMS_MEDICINE_EXPORT_STORE.InsertCMS_MEDICINE_EXPORT_STORE(obj._MEDICINE_EXPORT_STORE);
                                        if (!string.IsNullOrEmpty(idRECEIPT_STORE))
                                        {
                                            ImpCMS_MEDICINE_EXPORT_DETAIL impCMS_MEDICINE_EXPORT_DETAIL = new ImpCMS_MEDICINE_EXPORT_DETAIL();
                                            foreach (var data in obj._MEDICINE_EXPORT_DETAILS)
                                            {
                                                data.CMS_MEDICINE_EXPORT_STORE_ID = Convert.ToInt32(idRECEIPT_STORE);
                                                data.CMS_MEDICINE_EXPORT_STORE_CODE = obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_CODE;
                                                data.CREATEBY = UserInfo.accountid;
                                                data.CREATEDATE = DateTime.Now;
                                                data.PARTNERID = UserInfo.PARTNERID;
                                                data.MEDICINE_CODE = data.MEDICINE_ID.ToString();
                                                impCMS_MEDICINE_EXPORT_DETAIL.InsertCMS_MEDICINE_EXPORT_DETAIL(data);
                                            }

                                            ViewBag.TitleSuccsess = "Cập nhật thành công!";
                                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;

                                        }
                                        else
                                        {
                                            ViewBag.TitleSuccsess = "Lỗi cập nhật dữ liệu";
                                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.TitleSuccsess = "Đã cập nhật dữ liệu rồi, Mời bạn kiểm tra lại";
                                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    }
                                }
                                else
                                {
                                    string idRECEIPT_STORE = impCMS_MEDICINE_EXPORT_STORE.UpdateCMS_MEDICINE_EXPORT_STORE(obj._MEDICINE_EXPORT_STORE);
                                    foreach (var data in obj._MEDICINE_EXPORT_DETAILS)
                                    {
                                        if (data.ID == 0)
                                        {
                                            data.CMS_MEDICINE_EXPORT_STORE_ID = obj._MEDICINE_EXPORT_STORE.ID;
                                            data.CMS_MEDICINE_EXPORT_STORE_CODE = obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_CODE;
                                            data.CREATEBY = UserInfo.accountid;
                                            data.CREATEDATE = DateTime.Now;
                                            data.PARTNERID = UserInfo.PARTNERID;
                                            data.MEDICINE_CODE = data.MEDICINE_ID.ToString();
                                            impCMS_MEDICINE_EXPORT_DETAIL.InsertCMS_MEDICINE_EXPORT_DETAIL(data);
                                        }
                                        else
                                        {
                                            data.CMS_MEDICINE_EXPORT_STORE_ID = obj._MEDICINE_EXPORT_STORE.ID;
                                            data.CMS_MEDICINE_EXPORT_STORE_CODE = obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_CODE;
                                            data.UPDATEBY = UserInfo.accountid;
                                            data.UPDATEDATE = DateTime.Now;
                                            data.PARTNERID = UserInfo.PARTNERID;
                                            data.MEDICINE_CODE = data.MEDICINE_ID.ToString();
                                            impCMS_MEDICINE_EXPORT_DETAIL.UpdateCMS_MEDICINE_RECEIPT_DETAIL(data);
                                        }
                                    }

                                    ViewBag.TitleSuccsess = "Cập nhật thành công!";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                }
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Bạn chưa nhập thuốc vào kho";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }

                        }
                        else
                        {
                            ViewBag.TitleSuccsess = errorsSave.ToList()[0].ErrorMessage;
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }


                        break;

                    case "SaveExportStoreChoi":
                        bool checkValidate = true;

                        DateTime Tungay = new DateTime();
                        DateTime Denngay = new DateTime();

                        #region Check input data
                        try
                        {
                            if (!string.IsNullOrEmpty(obj.MEDICINE_EXPORT_DETAIL.MEDICINE_CREATE_View.Trim()))
                                Tungay = DateTime.ParseExact(obj.MEDICINE_EXPORT_DETAIL.MEDICINE_CREATE_View.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                            if (!string.IsNullOrEmpty(obj.MEDICINE_EXPORT_DETAIL.MEDICINE_EXPIRED_View.Trim()))
                                Denngay = DateTime.ParseExact(obj.MEDICINE_EXPORT_DETAIL.MEDICINE_EXPIRED_View.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));

                            if (Tungay > Denngay)
                            {
                                ViewBag.TitleSuccsess = "Ngày sản xuất phải nhỏ hơn ngày hết hạn";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;

                            }

                        }
                        catch
                        {
                            ViewBag.TitleSuccsess = "Dữ liệu ngày tháng không đúng định dạng";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                        #endregion


                        #region Check input data
                        try
                        {
                            if (string.IsNullOrEmpty(obj.MEDICINE_EXPORT_DETAIL.MEDICINE_NAME))
                            {
                                ViewBag.TitleSuccsess = "Mời bạn chọn tên thuốc";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                checkValidate = false;
                                if (Session["PhieuXuatThuoc"] != null)
                                {
                                    obj._MEDICINE_EXPORT_DETAILS = (List<CMS_MEDICINE_EXPORT_DETAIL>)Session["PhieuXuatThuoc"];
                                }
                                break;
                            }



                            if (obj.MEDICINE_EXPORT_DETAIL.MEDICINE_TOTAL != null)
                            {
                                // Nếu mà update khi số lượng thuốc chỉnh sửa giảm xuống thì không cần check số thuốc với thuốc trong kho, các trường hợp khác phải so sanh với thuốc trong kho
                                // lấy bản ghi trong trường hợp update  
                                int MEDICINE_AMOUNTOLD = 0;
                                if (obj.MEDICINE_EXPORT_DETAIL.ID != 0)
                                {
                                    List<CMS_MEDICINE_EXPORT_DETAIL> data = impCMS_MEDICINE_EXPORT_DETAIL.GetCMS_MEDICINE_EXPORT_DETAILByID(obj.MEDICINE_EXPORT_DETAIL.ID, UserInfo.PARTNERID);
                                    if (data != null)
                                    {
                                        if (data.Count > 0)
                                        {
                                            MEDICINE_AMOUNTOLD = data[0].MEDICINE_AMOUNT ?? 0;
                                        }
                                    }
                                }



                                if (obj.MEDICINE_EXPORT_DETAIL.MEDICINE_AMOUNT - MEDICINE_AMOUNTOLD > obj.MEDICINE_EXPORT_DETAIL.MEDICINE_TOTAL)
                                {
                                    ViewBag.TitleSuccsess = "Số lượng bán không được lớn hơn số tồn kho";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    checkValidate = false;
                                    if (Session["PhieuXuatThuoc"] != null)
                                    {
                                        obj._MEDICINE_EXPORT_DETAILS = (List<CMS_MEDICINE_EXPORT_DETAIL>)Session["PhieuXuatThuoc"];
                                    }
                                    break;
                                }

                            }
                            else
                            {
                                checkValidate = false;
                                ViewBag.TitleSuccsess = "Không tồn tại thuốc trong kho!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                if (Session["PhieuXuatThuoc"] != null)
                                {
                                    obj._MEDICINE_EXPORT_DETAILS = (List<CMS_MEDICINE_EXPORT_DETAIL>)Session["PhieuXuatThuoc"];
                                }
                                break;
                            }

                        }
                        catch
                        {
                            ViewBag.TitleSuccsess = "Có lỗi xẩy ra mời bạn kiểm tra lại";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            checkValidate = false;
                            if (Session["PhieuXuatThuoc"] != null)
                            {
                                obj._MEDICINE_EXPORT_DETAILS = (List<CMS_MEDICINE_EXPORT_DETAIL>)Session["PhieuXuatThuoc"];
                            }
                            break;
                        }
                        #endregion

                        if (checkValidate)
                        {
                            obj.MEDICINE_EXPORT_DETAIL.MEDICINE_CREATE = Tungay;
                            obj.MEDICINE_EXPORT_DETAIL.MEDICINE_EXPIRED = Denngay;

                            if (Session["PhieuXuatThuoc"] != null)
                            {
                                obj._MEDICINE_EXPORT_DETAILS = (List<CMS_MEDICINE_EXPORT_DETAIL>)Session["PhieuXuatThuoc"];
                            }
                            else
                            {
                                obj._MEDICINE_EXPORT_DETAILS = new List<CMS_MEDICINE_EXPORT_DETAIL>();
                            }

                            var errors = obj.MEDICINE_EXPORT_DETAIL.Validate(new ValidationContext(obj.MEDICINE_EXPORT_DETAIL));
                            if (errors.ToList().Count == 0)
                            {
                                if (obj._MEDICINE_EXPORT_DETAILS.Where(x => x.MEDICINE_NAME.Equals(obj.MEDICINE_EXPORT_DETAIL.MEDICINE_NAME)).Count() > 0)
                                {
                                    var queryLondonCustomers = from cust in obj._MEDICINE_EXPORT_DETAILS
                                                               where cust.MEDICINE_NAME.Equals(obj.MEDICINE_EXPORT_DETAIL.MEDICINE_NAME)
                                                               select cust;
                                    queryLondonCustomers.ElementAt(0);
                                    //Lấy ID bản ghi trước chèn vào với update;
                                    if (obj.MEDICINE_EXPORT_DETAIL.ID != 0)
                                    {
                                        obj.MEDICINE_EXPORT_DETAIL.ID = queryLondonCustomers.ElementAt(0).ID;
                                    }
                                    obj._MEDICINE_EXPORT_DETAILS.Remove(queryLondonCustomers.ElementAt(0));
                                }

                                obj._MEDICINE_EXPORT_DETAILS.Add(obj.MEDICINE_EXPORT_DETAIL);
                                Session["PhieuXuatThuoc"] = obj._MEDICINE_EXPORT_DETAILS;
                                ViewBag.TitleSuccsess = "Chọn thêm thuốc thành công!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                ModelState.Clear();
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = errors.ToList()[0].ErrorMessage;
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                            obj.MEDICINE_EXPORT_DETAIL = new CMS_MEDICINE_EXPORT_DETAIL();
                        }
                        else
                        {
                            if (Session["PhieuXuatThuoc"] != null)
                            {
                                obj._MEDICINE_EXPORT_DETAILS = (List<CMS_MEDICINE_EXPORT_DETAIL>)Session["PhieuXuatThuoc"];
                            }
                        }
                        break;
                    case "SaveExportStoreDelete":
                        if (Session["PhieuXuatThuoc"] != null)
                        {
                            obj._MEDICINE_EXPORT_DETAILS = (List<CMS_MEDICINE_EXPORT_DETAIL>)Session["PhieuXuatThuoc"];
                        }
                        obj.MEDICINE_EXPORT_DETAIL = new CMS_MEDICINE_EXPORT_DETAIL();

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



        public ActionResult ExportStoreView(string ID, string Token)
        {
            // Initialization.             
            AddBreadcrumb("Kho thuốc", "");
            AddPageHeader("Phiếu xuất thuốc", "");
            AddBreadcrumb("Phiếu xuất thuốc", "/pharmacyStore/ExportStoreView");
            ViewBag.Title = "Phiếu xuất thuốc";

            CMS_MEDICINE_EXPORT_STORE _MEDICINE_EXPORT_STORE = new CMS_MEDICINE_EXPORT_STORE();

            List<CMS_MEDICINE_EXPORT_STORE> _MEDICINE_EXPORT_STORES = null;
            List<CMS_MEDICINE_EXPORT_DETAIL> __MEDICINE_EXPORT_DETAILS = null;

            CMS_Core.Models.ExportStoreViewModel obj = new CMS_Core.Models.ExportStoreViewModel();



            obj._MEDICINE_EXPORT_STORE = new CMS_MEDICINE_EXPORT_STORE();

            obj._MEDICINE_EXPORT_DETAILS = new List<CMS_MEDICINE_EXPORT_DETAIL>();
            obj.MEDICINE_EXPORT_DETAIL = new CMS_MEDICINE_EXPORT_DETAIL();




            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (!string.IsNullOrEmpty(ID))
                {

                    if (CMSLIS.Common.Common.CheckKeyPrivate(ID, Token))
                    {
                        _MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByID(Convert.ToInt32(ID), UserInfo.PARTNERID);
                        if (_MEDICINE_EXPORT_STORES != null)
                        {
                            if (_MEDICINE_EXPORT_STORES.Count > 0)
                            {
                                obj._MEDICINE_EXPORT_STORE = _MEDICINE_EXPORT_STORES[0];

                                __MEDICINE_EXPORT_DETAILS = impCMS_MEDICINE_EXPORT_DETAIL.GetCMS_MEDICINE_EXPORT_DETAILBySTORE_ID(obj._MEDICINE_EXPORT_STORE.ID, UserInfo.PARTNERID);
                                if (__MEDICINE_EXPORT_DETAILS != null)
                                {
                                    if (__MEDICINE_EXPORT_DETAILS.Count > 0)
                                    {
                                        obj._MEDICINE_EXPORT_DETAILS = __MEDICINE_EXPORT_DETAILS;
                                    }
                                    else
                                    {
                                        ViewBag.StatusMessage = "Không tìm thấy thông tin xuất thuốc";
                                    }
                                }
                                else
                                {
                                    ViewBag.StatusMessage = "Không tìm thấy thông tin xuất thuốc";
                                }

                            }
                            else
                            {
                                ViewBag.StatusMessage = "Không tìm thấy thông tin xuất thuốc";
                            }
                        }
                        else
                        {
                            ViewBag.StatusMessage = "Không tìm thấy thông tin xuất thuốc";
                        }

                    }
                    else
                    {
                        ViewBag.StatusMessage = "Thông tin nhập vào không chính xác, mời bạn kiểm tra lại!";
                    }
                }
                else
                {
                    ViewBag.StatusMessage = "Không tìm thấy thông tin xuất thuốc";
                }
            }
            catch (Exception ex)
            {
                logError.Info("ExportStoreAdd: " + ex.ToString());
            }

            // Info.  
            return View(obj);
        }

        public ActionResult ExportStoreViewPrint(string ID, string Token)
        {
            // Initialization.             
            AddBreadcrumb("Kho thuốc", "");

            AddPageHeader("Phiếu xuất thuốc", "");
            AddBreadcrumb("Phiếu xuất thuốc", "/pharmacyStore/ExportStoreView");
            ViewBag.Title = "Phiếu xuất thuốc";

            CMS_MEDICINE_EXPORT_STORE _MEDICINE_EXPORT_STORE = new CMS_MEDICINE_EXPORT_STORE();

            List<CMS_MEDICINE_EXPORT_STORE> _MEDICINE_EXPORT_STORES = null;
            List<CMS_MEDICINE_EXPORT_DETAIL> __MEDICINE_EXPORT_DETAILS = null;

            CMS_Core.Models.ExportStoreViewModel obj = new CMS_Core.Models.ExportStoreViewModel();



            obj._MEDICINE_EXPORT_STORE = new CMS_MEDICINE_EXPORT_STORE();

            obj._MEDICINE_EXPORT_DETAILS = new List<CMS_MEDICINE_EXPORT_DETAIL>();
            obj.MEDICINE_EXPORT_DETAIL = new CMS_MEDICINE_EXPORT_DETAIL();




            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (!string.IsNullOrEmpty(ID))
                {

                    if (CMSLIS.Common.Common.CheckKeyPrivate(ID, Token))
                    {
                        _MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByID(Convert.ToInt32(ID), UserInfo.PARTNERID);
                        if (_MEDICINE_EXPORT_STORES != null)
                        {
                            if (_MEDICINE_EXPORT_STORES.Count > 0)
                            {
                                obj._MEDICINE_EXPORT_STORE = _MEDICINE_EXPORT_STORES[0];

                                __MEDICINE_EXPORT_DETAILS = impCMS_MEDICINE_EXPORT_DETAIL.GetCMS_MEDICINE_EXPORT_DETAILBySTORE_ID(obj._MEDICINE_EXPORT_STORE.ID, UserInfo.PARTNERID);
                                if (__MEDICINE_EXPORT_DETAILS != null)
                                {
                                    if (__MEDICINE_EXPORT_DETAILS.Count > 0)
                                    {
                                        obj._MEDICINE_EXPORT_DETAILS = __MEDICINE_EXPORT_DETAILS;
                                    }
                                    else
                                    {
                                        ViewBag.StatusMessage = "Không tìm thấy thông tin xuất thuốc";
                                    }
                                }
                                else
                                {
                                    ViewBag.StatusMessage = "Không tìm thấy thông tin xuất thuốc";
                                }

                            }
                            else
                            {
                                ViewBag.StatusMessage = "Không tìm thấy thông tin xuất thuốc";
                            }
                        }
                        else
                        {
                            ViewBag.StatusMessage = "Không tìm thấy thông tin xuất thuốc";
                        }
                    }
                    else
                    {
                        ViewBag.StatusMessage = "Thông tin nhập vào không chính xác, mời bạn kiểm tra lại!";
                    }
                }
                else
                {
                    ViewBag.StatusMessage = "Không tìm thấy thông tin xuất thuốc";
                }
            }
            catch (Exception ex)
            {
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


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult GetInfoMedicineExport(string code)
        {
            try
            {
                var UserInfo = ((cms_Account)Session["UserInfo"]);
                IEnumerable<CMS_MEDICINE_RECEIPT_DETAIL> modelList = new List<CMS_MEDICINE_RECEIPT_DETAIL>();

                modelList = impCMS_MEDICINE_RECEIPT_DETAIL.GetCMS_MEDICINE_RECEIPT_DETAILByMEDICINE_CODE(code, UserInfo.PARTNERID);

                return Json(modelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logError.Info("GetInfoMedicineExport: " + code + "  " + ex.ToString());
                return Json(string.Empty);
            }
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ExportStorDeleteEXPORT_DETAIL(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "/pharmacyStore");
            AddBreadcrumb("Phiếu xuất thuốc", "/pharmacyStore/ExportStorDeleteEXPORT_DETAIL");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    if (Session["PhieuXuatThuoc"] != null)
                    {
                        var toRemove = new List<CMS_MEDICINE_EXPORT_DETAIL>();

                        List<CMS_MEDICINE_EXPORT_DETAIL> _MEDICINE_EXPORT_DETAILS = (List<CMS_MEDICINE_EXPORT_DETAIL>)Session["PhieuXuatThuoc"];
                        foreach (string customerID in customerIDs)
                        {
                            try
                            {

                                foreach (CMS_MEDICINE_EXPORT_DETAIL MEDICINE_EXPORT_DETAIL in _MEDICINE_EXPORT_DETAILS.ToList())
                                {
                                    if (customerID.Equals(MEDICINE_EXPORT_DETAIL.MEDICINE_ID.ToString().Trim()))
                                    {
                                        toRemove.Add(MEDICINE_EXPORT_DETAIL);

                                        // Xóa hẳn dữ liệu ra khỏi phiếu nhập
                                        if (MEDICINE_EXPORT_DETAIL.CMS_MEDICINE_EXPORT_STORE_ID > 0)
                                        {
                                            impCMS_MEDICINE_EXPORT_DETAIL.DeleteCMS_MEDICINE_EXPORT_DETAILBYCODE(customerID, MEDICINE_EXPORT_DETAIL.CMS_MEDICINE_EXPORT_STORE_ID, UserInfo.PARTNERID);
                                        }

                                        //  _MEDICINE_RECEIPT_DETAILS.Remove(MEDICINE_RECEIPT_DETAIL);
                                    }
                                }
                            }
                            catch { }
                        }

                        _MEDICINE_EXPORT_DETAILS.RemoveAll(toRemove.Contains);

                        Session["PhieuNhapThuoc"] = _MEDICINE_EXPORT_DETAILS;
                        return Json("Xóa thành công");
                    }
                    else
                    {
                        return Json("Xóa không thành công");
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Chưa có bản tin nào được chọn để xóa";
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("ExportStorDeleteEXPORT_DETAIL:" + ex.ToString());
                return Json("Xóa không thành công");
            }

            // return Json("Xóa thành công");

        }


        #endregion


        #region --> Danh sách hoàn thuốc
        public ActionResult REFUNDStore()
        {
            // Initialization.  
            AddPageHeader("Danh sách hoàn thuốc", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách hoàn thuốc", "/pharmacyStore/REFUNDStore");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                this.ViewBag.GetTypeStatus = CMSLIS.Common.Common.GetTypeStatus();

                List<CMS_MEDICINE_REFUND_STORE> CMS_MEDICINE_REFUND_STORES = new List<CMS_MEDICINE_REFUND_STORE>();

                CMS_MEDICINE_REFUND_STORES = impCMS_MEDICINE_REFUND_STORE.GetCMS_MEDICINE_REFUND_STOREByCODE(DateTime.Now.AddDays(-10), DateTime.Now.AddDays(1), string.Empty, UserInfo.PARTNERID);

                ViewBag.CMS_MEDICINE_REFUND_STORES = CMS_MEDICINE_REFUND_STORES;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
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
        public ActionResult REFUNDStore(pharmacyStoreViewModels obj, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách hoàn thuốc", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách hoàn thuốc", "/pharmacyStore/REFUNDStore");
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
                        case "SearchREFUNDStore":

                            List<CMS_MEDICINE_REFUND_STORE> CMS_MEDICINE_REFUND_STORES = new List<CMS_MEDICINE_REFUND_STORE>();

                            if (string.IsNullOrEmpty(obj.pharmacyStoreCode))
                            {
                                obj.pharmacyStoreCode = string.Empty;
                            }

                            CMS_MEDICINE_REFUND_STORES = impCMS_MEDICINE_REFUND_STORE.GetCMS_MEDICINE_REFUND_STOREByALL(Tungay, Denngay.AddDays(1), obj.pharmacyStoreCode.Trim(), obj.Status, obj.PARTNERID, UserInfo.PARTNERID);

                            ViewBag.CMS_MEDICINE_REFUND_STORES = CMS_MEDICINE_REFUND_STORES;
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
        public ActionResult REFUNDStoreDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách hoàn thuốc", "/pharmacyStore/REFUNDStoreDelete");
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
                        string result = impCMS_MEDICINE_REFUND_STORE.Delete(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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
        public ActionResult REFUNDStorePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Danh sách hoàn thuốc", "/pharmacyStore/REFUNDStorePublic");
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
                        impCMS_MEDICINE_REFUND_STORE.Publish(Convert.ToInt32(customerID), UserInfo.PARTNERID);
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



        public ActionResult REFUNDStoreAdd(string EXPORT_STORE_ID, string id)
        {
            // Initialization.  
            AddPageHeader("Hoàn thuốc", "");
            AddBreadcrumb("Kho thuốc", "");
            AddBreadcrumb("Hoàn thuốc", "/pharmacyStore/REFUNDStoreAdd");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            CMSNEW.Models.RefundStoreViewModels refundStoreView = new CMSNEW.Models.RefundStoreViewModels();
            refundStoreView._MEDICINE_EXPORT_DETAILS = new List<CMS_MEDICINE_EXPORT_DETAIL>();
            refundStoreView._MEDICINE_EXPORT_STORE = new CMS_MEDICINE_EXPORT_STORE();
            try
            {

                if (string.IsNullOrEmpty(EXPORT_STORE_ID))
                {
                    EXPORT_STORE_ID = string.Empty;
                }
                if (string.IsNullOrEmpty(id))
                {
                    id = "0";
                }

                if (!"0".Equals(id))
                {

                    CMS_MEDICINE_REFUND_STORE CMS_MEDICINE_REFUND_STORE_Value = impCMS_MEDICINE_REFUND_STORE.Get(Convert.ToInt32(id), UserInfo.PARTNERID);
                    if (CMS_MEDICINE_REFUND_STORE_Value != null)
                    {
                        if (CMS_MEDICINE_REFUND_STORE_Value.CMS_MEDICINE_EXPORT_STORE_CODE.Length > 0)
                        {
                            List<CMS_MEDICINE_EXPORT_STORE> _MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByCODE(CMS_MEDICINE_REFUND_STORE_Value.CMS_MEDICINE_EXPORT_STORE_CODE, UserInfo.PARTNERID);
                            if (_MEDICINE_EXPORT_STORES != null)
                            {
                                if (_MEDICINE_EXPORT_STORES.Count > 0)
                                {
                                    refundStoreView._MEDICINE_EXPORT_STORE = _MEDICINE_EXPORT_STORES[0];
                                    refundStoreView._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_NOTE = string.Empty;
                                    refundStoreView._MEDICINE_EXPORT_STORE.REFUND_STORE = Convert.ToInt32(id);
                                    List<CMS_MEDICINE_REFUND_DETAIL> _MEDICINE_REFUND_DETAILS = impCMS_MEDICINE_REFUND_DETAIL.GetBYRefundStoreID(Convert.ToInt32(id), UserInfo.PARTNERID);
                                    List<CMS_MEDICINE_EXPORT_DETAIL> _MEDICINE_EXPORT_DETAIS = impCMS_MEDICINE_EXPORT_DETAIL.GetCMS_MEDICINE_EXPORT_DETAILBySTORE_CODE(CMS_MEDICINE_REFUND_STORE_Value.CMS_MEDICINE_EXPORT_STORE_CODE, UserInfo.PARTNERID);
                                    if (_MEDICINE_EXPORT_DETAIS != null)
                                    {
                                        foreach (var data in _MEDICINE_EXPORT_DETAIS)
                                        {
                                            if (_MEDICINE_REFUND_DETAILS != null)
                                            {
                                                foreach (var value in _MEDICINE_REFUND_DETAILS)
                                                {
                                                    if (value.MEDICINE_ID == data.MEDICINE_ID)
                                                    {
                                                        data.Refund = value.MEDICINE_AMOUNT;
                                                    }
                                                }
                                            }
                                        }
                                    }


                                    refundStoreView._MEDICINE_EXPORT_DETAILS = _MEDICINE_EXPORT_DETAIS;
                                }
                                else
                                {
                                    refundStoreView._MEDICINE_EXPORT_STORE.CUSTOMER_GENDER = 2;
                                    ViewBag.TextErr = "Không tồn tại đơn thuốc, Mời bạn kiểm tra lại!";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                refundStoreView._MEDICINE_EXPORT_STORE.CUSTOMER_GENDER = 2;
                                ViewBag.TextErr = "Không tồn tại đơn thuốc, Mời bạn kiểm tra lại!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                        }
                    }


                }
                else
                {

                    if (EXPORT_STORE_ID.Length > 0)
                    {
                        bool checkExit = false;
                        CMS_MEDICINE_REFUND_STORE _MEDICINE_REFUND_STORE = impCMS_MEDICINE_REFUND_STORE.GetBYEXPORT_STORE_CODE(EXPORT_STORE_ID, UserInfo.PARTNERID);
                        if (_MEDICINE_REFUND_STORE != null)
                        {
                            if (_MEDICINE_REFUND_STORE.ID != 0)
                            {
                                checkExit = true;
                            }
                        }
                        if (!checkExit)
                        {
                            List<CMS_MEDICINE_EXPORT_STORE> _MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByCODE(EXPORT_STORE_ID, UserInfo.PARTNERID);
                            if (_MEDICINE_EXPORT_STORES != null)
                            {
                                if (_MEDICINE_EXPORT_STORES.Count > 0)
                                {
                                    refundStoreView._MEDICINE_EXPORT_STORE = _MEDICINE_EXPORT_STORES[0];
                                    refundStoreView._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_NOTE = string.Empty;
                                    refundStoreView._MEDICINE_EXPORT_DETAILS = impCMS_MEDICINE_EXPORT_DETAIL.GetCMS_MEDICINE_EXPORT_DETAILBySTORE_CODE(EXPORT_STORE_ID, UserInfo.PARTNERID);
                                }
                                else
                                {
                                    refundStoreView._MEDICINE_EXPORT_STORE.CUSTOMER_GENDER = 2;
                                    ViewBag.TitleSuccsess = "Không tồn tại đơn thuốc. Mời bạn kiểm tra lại!";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }
                            }
                            else
                            {
                                refundStoreView._MEDICINE_EXPORT_STORE.CUSTOMER_GENDER = 2;
                                ViewBag.TitleSuccsess = "Không tồn tại đơn thuốc. Mời bạn kiểm tra lại!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Phiếu đã hoàn trả rồi. Mời bạn vào danh sách phiếu hoàn trả để kiểm tra!";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }


                    }
                    else
                    {
                        refundStoreView._MEDICINE_EXPORT_STORE.CUSTOMER_GENDER = 2;
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ExportStore: " + ex.ToString());
            }

            // Info.  
            return View(refundStoreView);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult REFUNDStoreAdd(CMSNEW.Models.RefundStoreViewModels obj, string submit)
        {
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {

                AddPageHeader("Hoàn thuốc", "");
                AddBreadcrumb("Kho thuốc", "");
                AddBreadcrumb("Hoàn thuốc", "/pharmacyStore/REFUNDStore");




                switch (submit)
                {
                    case "SaveREFUNDStore":
                        bool checkValidate = true;
                        float totalRefundPrice = 0;
                        if (obj._MEDICINE_EXPORT_DETAILS != null)
                        {
                            foreach (var data in obj._MEDICINE_EXPORT_DETAILS)
                            {
                                if (data.MEDICINE_AMOUNT < data.Refund)
                                {
                                    checkValidate = false;
                                    break;
                                }
                            }
                        }

                        if (checkValidate)
                        {
                            string REFUND_STORE_ID = string.Empty;

                            if (obj._MEDICINE_EXPORT_DETAILS != null)
                            {
                                foreach (var data in obj._MEDICINE_EXPORT_DETAILS)
                                {
                                    totalRefundPrice = totalRefundPrice + ((data.MEDICINE_PRICE ?? 0) * data.Refund);
                                }
                            }



                            CMS_MEDICINE_REFUND_STORE _MEDICINE_REFUND_STORE = new CMS_MEDICINE_REFUND_STORE();
                            _MEDICINE_REFUND_STORE.PARTNERID = UserInfo.PARTNERID;
                            _MEDICINE_REFUND_STORE.TOTAL_PRICE_REFUND = totalRefundPrice;
                            _MEDICINE_REFUND_STORE.CMS_MEDICINE_EXPORT_STORE_ID = Convert.ToInt32(obj._MEDICINE_EXPORT_STORE.ID);
                            _MEDICINE_REFUND_STORE.CUSTOMER_NAME = obj._MEDICINE_EXPORT_STORE.CUSTOMER_NAME;
                            _MEDICINE_REFUND_STORE.CUSTOMER_MOBILE = obj._MEDICINE_EXPORT_STORE.CUSTOMER_MOBILE;
                            _MEDICINE_REFUND_STORE.CUSTOMER_EMAIL = obj._MEDICINE_EXPORT_STORE.CUSTOMER_EMAIL;
                            _MEDICINE_REFUND_STORE.CUSTOMER_GENDER = obj._MEDICINE_EXPORT_STORE.CUSTOMER_GENDER;
                            _MEDICINE_REFUND_STORE.CREATEBY = UserInfo.accountid;
                            _MEDICINE_REFUND_STORE.CREATEDATE = DateTime.Now;
                            _MEDICINE_REFUND_STORE.CMS_MEDICINE_EXPORT_STORE_CODE = obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_CODE;
                            _MEDICINE_REFUND_STORE.NOTE = obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_NOTE;
                            var errorsSave1 = _MEDICINE_REFUND_STORE.Validate(new ValidationContext(_MEDICINE_REFUND_STORE));

                            if (errorsSave1.ToList().Count == 0)
                            {
                                REFUND_STORE_ID = impCMS_MEDICINE_REFUND_STORE.Add(_MEDICINE_REFUND_STORE, UserInfo.PARTNERID);
                                if (obj._MEDICINE_EXPORT_STORE.REFUND_STORE != 0)
                                {
                                    REFUND_STORE_ID = obj._MEDICINE_EXPORT_STORE.REFUND_STORE.ToString();

                                }

                                if (!string.IsNullOrEmpty(REFUND_STORE_ID))

                                {
                                    foreach (var data in obj._MEDICINE_EXPORT_DETAILS)
                                    {
                                        CMS_MEDICINE_REFUND_DETAIL _REFUND_DETAIL = new CMS_MEDICINE_REFUND_DETAIL();
                                        _REFUND_DETAIL.CMS_MEDICINE_EXPORT_STORE_CODE = obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_CODE;
                                        _REFUND_DETAIL.CMS_MEDICINE_EXPORT_STORE_ID = obj._MEDICINE_EXPORT_STORE.ID;
                                        _REFUND_DETAIL.MEDICINE_CODE = data.MEDICINE_CODE;
                                        _REFUND_DETAIL.MEDICINE_AMOUNT = data.Refund;
                                        _REFUND_DETAIL.MEDICINE_ID = data.MEDICINE_ID;
                                        _REFUND_DETAIL.MEDICINE_PRICE = (data.MEDICINE_PRICE ?? 0);
                                        _REFUND_DETAIL.NOTE = string.Empty;
                                        _REFUND_DETAIL.CREATEBY = UserInfo.accountid;
                                        _REFUND_DETAIL.CREATEDATE = DateTime.Now;
                                        _REFUND_DETAIL.PARTNERID = UserInfo.PARTNERID;
                                        _REFUND_DETAIL.REFUND_STORE = Convert.ToInt32(REFUND_STORE_ID);

                                        var errorsSave = _REFUND_DETAIL.Validate(new ValidationContext(_REFUND_DETAIL));
                                        if (errorsSave.ToList().Count == 0)
                                        {
                                            impCMS_MEDICINE_REFUND_DETAIL.Add(_REFUND_DETAIL, UserInfo.PARTNERID);
                                            totalRefundPrice = totalRefundPrice + (data.MEDICINE_PRICE ?? 0);
                                        }
                                    }

                                    // trả về trang danh sách hoàn thuốc
                                    Response.Redirect("/pharmacyStore/REFUNDStore", false);
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Có lõi trong quá trình cập nhật, Mời bạn kiểm tra lại!";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                }

                            }
                            else
                            {
                                ViewBag.TitleSuccsess = errorsSave1.ToList()[0].ErrorMessage;
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }



                        }

                        break;



                }

                if (obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_CODE.Length > 0)
                {
                    List<CMS_MEDICINE_EXPORT_STORE> _MEDICINE_EXPORT_STORES = impCMS_MEDICINE_EXPORT_STORE.GetCMS_MEDICINE_EXPORT_STOREByCODE(obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_CODE, UserInfo.PARTNERID);
                    if (_MEDICINE_EXPORT_STORES != null)
                    {
                        if (_MEDICINE_EXPORT_STORES.Count > 0)
                        {
                            obj._MEDICINE_EXPORT_STORE = _MEDICINE_EXPORT_STORES[0];
                            obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_NOTE = string.Empty;
                            obj._MEDICINE_EXPORT_DETAILS = impCMS_MEDICINE_EXPORT_DETAIL.GetCMS_MEDICINE_EXPORT_DETAILBySTORE_CODE(obj._MEDICINE_EXPORT_STORE.CMS_MEDICINE_EXPORT_STORE_CODE, UserInfo.PARTNERID);
                        }
                        else
                        {
                            obj._MEDICINE_EXPORT_STORE.CUSTOMER_GENDER = 2;
                            ViewBag.TitleSuccsess = "Không tồn tại đơn thuốc, Mời bạn kiểm tra lại!";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        obj._MEDICINE_EXPORT_STORE.CUSTOMER_GENDER = 2;
                        ViewBag.TitleSuccsess = "Không tồn tại đơn thuốc, Mời bạn kiểm tra lại!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }


                }
                else
                {
                    obj._MEDICINE_EXPORT_STORE = new CMS_MEDICINE_EXPORT_STORE();
                    obj._MEDICINE_EXPORT_DETAILS = new List<CMS_MEDICINE_EXPORT_DETAIL>();
                    obj._MEDICINE_EXPORT_STORE.CUSTOMER_GENDER = 2;
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

        public ActionResult REFUNDStorePrint(string ID, string Token)
        {
            // Initialization.             
            AddBreadcrumb("Kho thuốc", "");

            AddPageHeader("Phiếu hoàn thuốc", "");
            AddBreadcrumb("Phiếu hoàn thuốc", "/pharmacyStore/REFUNDStorePrint");
            ViewBag.Title = "Phiếu hoàn thuốc";


            CMSNEW.Models.RefundStoreViewModels obj = new RefundStoreViewModels();

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (!string.IsNullOrEmpty(ID))
                {

                    if (CMSLIS.Common.Common.CheckKeyPrivate(ID, Token))
                    {
                        CMS_MEDICINE_REFUND_STORE CMS_MEDICINE_REFUND_STORE_Value = impCMS_MEDICINE_REFUND_STORE.Get(Convert.ToInt32(ID), UserInfo.PARTNERID);
                        if (CMS_MEDICINE_REFUND_STORE_Value != null)
                        {
                            obj._CMS_MEDICINE_REFUND_STORE = CMS_MEDICINE_REFUND_STORE_Value;
                            obj._MEDICINE_REFUND_DETAILS = impCMS_MEDICINE_REFUND_DETAIL.GetBYRefundStoreID(CMS_MEDICINE_REFUND_STORE_Value.ID, UserInfo.PARTNERID);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin hoàn thuốc";
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin hoàn thuốc";
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Thông tin nhập vào không chính xác, mời bạn kiểm tra lại!";
                }

            }
            catch (Exception ex)
            {
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

        #region --> Danh sách thuốc tồn kho

        public ActionResult MedicineInventorys()
        {
            AddBreadcrumb("Kho thuốc", "");
            AddPageHeader("Danh sách tồn kho", "");
            AddBreadcrumb("Danh sách tồn kho", "/pharmacyStore/MedicineInventorys");
            ViewBag.Title = "Danh sách tồn kho";



            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {

                List<CMS_MEDICINE_INVENTORY> CMS_MEDICINE_INVENTORYS = impCMS_MEDICINE_INVENTORY.GetAll(UserInfo.PARTNERID);
                ViewBag.CMS_MEDICINE_INVENTORYS = CMS_MEDICINE_INVENTORYS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("MedicineInventorys:" + ex.ToString());
            }

            // Info.  
            return View();


        }


        [HttpPost]

        public ActionResult MedicineInventorys(string submit)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "");
            AddPageHeader("Danh sách tồn kho", "");
            AddBreadcrumb("Danh sách tồn kho", "/pharmacyStore/MedicineInventorys");
            ViewBag.Title = "Danh sách tồn kho";

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {


                List<CMS_MEDICINE_INVENTORY> CMS_MEDICINE_INVENTORYS = impCMS_MEDICINE_INVENTORY.GetAll(UserInfo.PARTNERID);
                if (CMS_MEDICINE_INVENTORYS != null)
                {
                    if (CMS_MEDICINE_INVENTORYS.Count > 0)
                    {
                        string[] columns = { "MEDICINE_NAME", "MEDICINE_ID", "INVENTORY" };
                        string timeReport = "Thời gian báo cáo :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm");


                        if (UserInfo == null)
                        {
                            Response.Redirect("/account/login");
                        }
                        string creater = "Người báo cáo: " + UserInfo.Hoten;
                        byte[] filecontent = ExcelExportHelper.ExportExcel(CMS_MEDICINE_INVENTORYS, "Tổng hợp tồn kho", timeReport, creater, true, columns);

                        //  new FileContentResult(filecontent, ExcelExportHelper.ExcelContentType);
                        return File(filecontent, ExcelExportHelper.ExcelContentType, "MedicineInventorys.xlsx");
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
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListPatientWaitingForMedical: " + ex.ToString());

            }

            // Info.  
            return View();
        }


        #endregion

        #region --> Danh sách thuốc tồn kho

        public ActionResult ListMedicineExports()
        {
            AddBreadcrumb("Kho thuốc", "");
            AddPageHeader("Danh sách thuốc đã bán", "");
            AddBreadcrumb("Danh sách thuốc đã bán", "/pharmacyStore/ListMedicineExports");
            ViewBag.Title = "Danh sách thuốc đã bán";
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            pharmacyStoreViewModels obj = new pharmacyStoreViewModels();
            if (UserInfo.PARTNERID == 1)
            {
                obj.PARTNERID = 0;
            }
            else
            {
                obj.PARTNERID = UserInfo.PARTNERID;
            }
            obj.startdate = DateTime.Today.ToString("dd/MM/yyyy");
            obj.enddate = DateTime.Now.ToString("dd/MM/yyyy");
            obj.pharmacyStoreCode = string.Empty;
            obj.Status = CMSLIS.Common.Constant.TypeStatusAll;
            try
            {

                List<CMS_MEDICINE_EXPORT_DETAIL> CMS_MEDICINE_EXPORT_DETAILS = impCMS_MEDICINE_EXPORT_DETAIL.GetCMS_MEDICINE_EXPORT(DateTime.Today, DateTime.Now.AddDays(1), UserInfo.PARTNERID);
                ViewBag.CMS_MEDICINE_EXPORT_DETAILS = CMS_MEDICINE_EXPORT_DETAILS;
            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListMedicineExports:" + ex.ToString());
            }

            // Info.  
            return View(obj);


        }


        [HttpPost]

        public ActionResult ListMedicineExports(pharmacyStoreViewModels obj, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Kho thuốc", "");
            AddPageHeader("Danh sách thuốc đã bán", "");
            AddBreadcrumb("Danh sách thuốc đã bán", "/pharmacyStore/ListMedicineExports");
            ViewBag.Title = "Danh sách thuốc đã bán";

            var UserInfo = ((cms_Account)Session["UserInfo"]);
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
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                    ViewBag.TextErr = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            catch
            {
                TextErr = "Dữ liệu ngày tháng không đúng định dạng";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }
            #endregion

            try
            {

                if (string.IsNullOrEmpty(TextErr))
                {

                    try
                    {
                        switch (submit)
                        {
                            case "SearchListMedicineExports":
                                List<CMS_MEDICINE_EXPORT_DETAIL> CMS_MEDICINE_EXPORT_DETAILS = impCMS_MEDICINE_EXPORT_DETAIL.GetCMS_MEDICINE_EXPORT(Tungay, Denngay.AddDays(1), UserInfo.PARTNERID);
                                ViewBag.CMS_MEDICINE_EXPORT_DETAILS = CMS_MEDICINE_EXPORT_DETAILS;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.TextErr = "Có lỗi xẩy tra trong quá trình thực hiện. Mời bạn thực hiện lại!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        logError.Info("ListMedicineExports: " + ex.ToString());
                    }
                }
                else
                {
                    ViewBag.TextErr = TextErr;
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }


            }
            catch (Exception ex)
            {
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListMedicineExports: " + ex.ToString());

            }

            // Info.  
            return View();
        }


        #endregion

    }
}
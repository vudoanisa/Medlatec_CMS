using CMS_Core.Entity;
using CMS_Core.Implementtion;
using CMSLIS.Common;
using CMSLIS.Models;
using CMSNEW.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSLIS.Controllers
{
    [CheckAuthorization]
    public class DictionaryController : BaseController
    {
        ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");
        // GET: Dictionary

        public DictionaryController()
        {

        }

        public ActionResult Index()
        {
            AddPageHeader("Từ điển bệnh lý", "");
            AddBreadcrumb("Từ điển bệnh lý", "/Dictionary/Index");

            var listItem = Cms_DictionaryService.Instance.CreateQuery()
                                                .ToList();

            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            ViewBag.DataResult = listItem;
            return View();
        }

        

        #region --> Nhập tin bài
        private Cms_DictionaryEntity dicEntity;
        public ActionResult Add(string dicId)
        {
            if (!string.IsNullOrEmpty(dicId))
            {
                int id = IMEX.Core.Global.Convert.ToInt(dicId);
                dicEntity = Cms_DictionaryService.Instance.GetById(id) ?? new Cms_DictionaryEntity();
                AddPageHeader("Từ điển bệnh lý", "");
                ViewBag.Title = "Cập nhật từ điển";
                AddBreadcrumb("Cập nhật từ điển", "/Dictionary/Add");
                this.ViewBag.Getcms_NewsDoctor = Common.Common.Getcms_NewsDoctor();
                ViewBag.Data = dicEntity;
                ViewBag.CountNews = !string.IsNullOrEmpty(dicEntity.NewIds) ? dicEntity.NewIds.Split(',').Length : 0;
                Session["ChoseNewsDic" + dicEntity.ID] = dicEntity.NewIds;


            }
            else
            {
                this.ViewBag.Getcms_NewsDoctor = Common.Common.Getcms_NewsDoctor();
                dicEntity = new Cms_DictionaryEntity();
            }

            return View(dicEntity);
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(Cms_DictionaryEntity dictionaryEntity)
        {
            // Initialization.  
            AddPageHeader("Từ điển bệnh lý", "");
            ViewBag.Title = "Cập nhật từ điển";
            AddBreadcrumb("Cập nhật từ điển", "/Dictionary/Add");

            this.ViewBag.Getcms_NewsDoctor = Common.Common.Getcms_NewsDoctor();

            bool validateImage = true;

            if (ModelState.IsValid)
            {



                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\imageslead\\";

                //Upload Image
                if (dictionaryEntity.ImageFile != null)
                {


                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(dictionaryEntity.ImageFile.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(dictionaryEntity.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    dictionaryEntity.Image = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(dictionaryEntity.Image))
                        Directory.CreateDirectory(dictionaryEntity.Image);

                    dictionaryEntity.Image = dictionaryEntity.Image + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim() + FileExtension;
                    if (System.IO.File.Exists(dictionaryEntity.Image))
                    {
                        System.IO.File.Delete(dictionaryEntity.Image);
                    }

                    if (!dictionaryEntity.Image.EndsWith("gif") && !dictionaryEntity.ImageFile.FileName.EndsWith("jpg") && !dictionaryEntity.ImageFile.FileName.EndsWith("jpeg") && !dictionaryEntity.ImageFile.FileName.EndsWith("jpg") && !dictionaryEntity.ImageFile.FileName.EndsWith("png"))
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
                        dictionaryEntity.ImageFile.SaveAs(dictionaryEntity.Image);
                        dictionaryEntity.Image = Common.Common.reSizeImage(dictionaryEntity.Image);
                        dictionaryEntity.Image = dictionaryEntity.Image.Replace("//", "/");
                    }
                }
                else
                {
                    validateImage = false;
                }

                int result = 0;
                if (dictionaryEntity.ID == 0)
                {
                    AddBreadcrumb("Thêm mới bài viết", "/Dictionary/Add");

                    if (validateImage)
                    {
                        dictionaryEntity.UserId = ((cms_Account)Session["UserInfo"]).uid;
                        dictionaryEntity.DateCreated = DateTime.Now;
                        if (dictionaryEntity.IsActive)
                            dictionaryEntity.DatePublish = DateTime.Now;
                        dictionaryEntity.Content = Common.Common.FormatSave(dictionaryEntity.Content);
                        dictionaryEntity.Url = Common.Common.GetCode(dictionaryEntity.Name) + "-s" + Common.Common.GetRandom(4, true);
                        dictionaryEntity.Image = dictionaryEntity.Image.Replace(Common.Common.getImagePath().ToLower(), "");
                        if (Session["ChoseNewsDic" + dictionaryEntity.ID] != null) { dictionaryEntity.NewIds = Session["ChoseNewsDic" + dictionaryEntity.ID].ToString(); }
                        dictionaryEntity.GroupName = Common.Common.GetFirstChar(dictionaryEntity.Name);

                        string imagePathServer = dictionaryEntity.Image;
                        string rep = @"D:\doanv\WEB\Medicons\CMSLIS\";
                        imagePathServer = imagePathServer.Replace(rep.ToLower(), "");

                        dictionaryEntity.Image = Common.Common.getImageLink() + imagePathServer.Replace("\\", "/");

                        result = Cms_DictionaryService.Instance.Save(dictionaryEntity);
                        if (result > 0)
                        {
                            ViewBag.TitleSuccsess = "Thêm mới bài viết thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result.ToString(), Constant.ActionInsertOK.ToString());



                            Response.Redirect("/Dictionary/Index", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới bài viết không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result.ToString(), Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Mời bạn nhập ảnh!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }

                }
                else
                {
                    AddBreadcrumb("Cập nhật bài viết", "/Dictionary/Add");
                    dictionaryEntity.UserId = ((cms_Account)Session["UserInfo"]).uid;
                    //if (dictionaryEntity.IsActive) dictionaryEntity.DatePublish = DateTime.Now;
                    if (validateImage)
                    {
                        string imagePathServer = dictionaryEntity.Image;
                        string rep = @"D:\doanv\WEB\Medicons\CMSLIS\";
                        imagePathServer = imagePathServer.Replace(rep.ToLower(), "");
                        dictionaryEntity.Image = Common.Common.getImageLink() + imagePathServer.Replace("\\", "/");
                    }
                    dictionaryEntity.Content = Common.Common.FormatSave(dictionaryEntity.Content);
                    if (Session["ChoseNewsDic" + dictionaryEntity.ID] != null)
                    {
                        dictionaryEntity.NewIds = Session["ChoseNewsDic" + dictionaryEntity.ID].ToString();
                    }
                    dictionaryEntity.GroupName = Common.Common.GetFirstChar(dictionaryEntity.Name);
                    result = Cms_DictionaryService.Instance.Save(dictionaryEntity);
                    if (result > 0)
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin bài viết thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result.ToString(), Constant.ActionUpdateOK.ToString());


                        Response.Redirect("/Dictionary/Index", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin bài viết không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result.ToString(), Constant.ActionUpdateNOK.ToString());
                    }
                }
            }


            return View(dictionaryEntity);
        }
     
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddRelate(SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  

            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
            this.ViewBag.Getcms_Account = Common.Common.Getcms_Account();

            var impcms_News = new Impcms_News();

            try
            {
                switch (submit)
                {
                    case "SearchAddRelate":
                        List<cms_News> _News = impcms_News.GetAllcms_NewsSearchNew(searchNewsView.ParrenId, searchNewsView.keyword);

                        ViewBag.DataReport = _News;

                        _News = impcms_News.GetAllcms_NewsRelate(Convert.ToInt32(searchNewsView.ParrenId));
                        if (Session["ChoseNewsDic" + searchNewsView.newsId] != null)
                        {
                            var listNews = Session["ChoseNewsDic" + searchNewsView.newsId].ToString();
                            List<cms_News> news = impcms_News.Getcms_NewsByIDs(listNews);
                            ViewBag.DataChoi = news;
                        }
                        else
                        {
                            ViewBag.DataChoi = _News;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListNews:" + ex.ToString());
            }

            // Info.  
            return View(searchNewsView);
        }

        public ActionResult AddRelate(string newsId)
        {
            SearchNewsViewModel searchNewsView = new SearchNewsViewModel();
            try
            {
                List<cms_News> _NewsRelate = null;

                if (string.IsNullOrEmpty(newsId))
                {
                    newsId = "0";
                }
                else
                {
                    if (newsId != "0" && Session["ChoseNewsDic" + newsId] == null)
                    {
                        var impcms_News1 = new Impcms_News();
                        _NewsRelate = impcms_News1.GetAllcms_NewsRelate(Convert.ToInt32(newsId));
                        Session["ChoseNewsDic" + newsId] = _NewsRelate;
                    }
                }

                this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
                this.ViewBag.Getcms_Account = Common.Common.Getcms_Account();
                searchNewsView.ParrenId = 0;
                searchNewsView.newsId = Convert.ToInt32(newsId);
                var impcms_News = new Impcms_News();
                List<cms_News> _News = impcms_News.GetAllcms_NewsSearch(0);

                ViewBag.DataReport = _News;
                if (Session["ChoseNewsDic" + newsId] != null)
                {
                    var listNews = Session["ChoseNewsDic" + newsId].ToString();
                    List<cms_News> news = impcms_News.Getcms_NewsByIDs(listNews);
                    ViewBag.DataChoi = news;
                }
                else
                {
                    ViewBag.DataChoi = null;
                }


            }
            catch (Exception ex)
            {
                logError.Info("ListNews: " + ex.ToString());
            }

            // Info.  
            return View(searchNewsView);
        }

        public ActionResult ChoseAddRelate(string[] customerIDs, string newid)
        {
            // Initialization.  

            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    List<cms_News> _NewsRelate = new List<cms_News>();

                    if (Session["ChoseNewsDic" + newid] != null)
                    {
                        var impcms_News = new Impcms_News();
                        var listNews = Session["ChoseNewsDic" + newid].ToString();
                        _NewsRelate = impcms_News.Getcms_NewsByIDs(listNews);
                        listID = listNews + ",";
                    }

                    // xóa dữ liệu                  
                    foreach (string customerID in customerIDs)
                    {
                        if (!string.IsNullOrEmpty(customerID))
                        {
                            string customerIDValue = customerID.Replace("[\"", "");
                            customerIDValue = customerIDValue.Replace("\"]", "");
                            customerIDValue = customerIDValue.Replace("\\", "");
                            customerIDValue = customerIDValue.Replace("\"", "");


                            //customerIDValue = customerIDValue.Replace("]", "");

                            string[] Values = customerIDValue.Split(',');
                            foreach (string Value in Values)
                            {
                                bool checkExit = false;
                                foreach (var NewsRelate in _NewsRelate)
                                {
                                    if (NewsRelate.newsId.ToString().Equals(Value))
                                    {
                                        checkExit = true;
                                        break;
                                    }
                                }
                                var impcms_News = new Impcms_News();
                                if (!checkExit)
                                {
                                    var NewsChoi = impcms_News.Getcms_NewsByID(Convert.ToInt32(Value));
                                    if (NewsChoi != null)
                                    {
                                        if (_NewsRelate.IndexOf(NewsChoi[0]) == -1)
                                            _NewsRelate.Add(NewsChoi[0]);
                                    }
                                    listID += Value + ",";
                                }
                            }
                        }
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }
                    Session["ChoseNewsDic" + newid] = listID;

                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn ");
                }
            }
            catch (Exception ex)
            {
                logError.Info("ChoiAddRelate: " + ex.ToString());
            }

            return Json("objID:" + listID);

            // Info.  
            //return View();
        }


        public ActionResult DeleteAddRelate(string customerIDs, string newid)
        {
            // Initialization.  

            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (!string.IsNullOrEmpty(customerIDs))
                {



                    List<cms_News> _NewsRelate = new List<cms_News>();
                    if (Session["ChoseNewsDic" + newid] != null)
                    {
                        var impcms_News = new Impcms_News();
                        var listNews = Session["ChoseNewsDic" + newid].ToString();
                        _NewsRelate = impcms_News.Getcms_NewsByIDs(listNews);
                        listID = listNews;
                    }

                    string customerIDValue = customerIDs.Replace("[\"", "");
                    customerIDValue = customerIDValue.Replace("\"]", "");
                    customerIDValue = customerIDValue.Replace("\\", "");
                    customerIDValue = customerIDValue.Replace("\"", "");

                    var listIDRemove = customerIDValue.Split(',');
                    var listIDchose = listID.Split(',');
                    listID = string.Empty;

                    string[] DifferArray = listIDchose.Except(listIDRemove).ToArray();
                    listID = string.Join(",", DifferArray);

                   
                    Session["ChoseNewsDic" + newid] = listID;
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("NewsRelateDic: " + ex.ToString());
            }

            return Json("objID" + listID);

            // Info.  
            //return View();
        }


        #endregion

        #region --> xóa và duyệt bài

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListDictionaryPublic(int[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Từ điển bệnh lý", "/Dictionary");
            AddBreadcrumb("Danh sách bài viết", "/Dictionary/ListDictionaryPublic");
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    Cms_DictionaryService.Instance.Update("[ID] IN (" + IMEX.Core.Global.Array.ToString(customerIDs) + ")", "@IsActive", 1);

                  
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để duyệt");
                }
            }
            catch (Exception ex)
            {
                logError.Info("DictionaryPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + IMEX.Core.Global.Array.ToString(customerIDs));

            // Info.  
            //return View();
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListDictionaryUnPublic(int[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Từ điển bệnh lý", "/Dictionary");
            AddBreadcrumb("Danh sách bài viết", "/Dictionary/ListDictionaryUnPublic");
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    Cms_DictionaryService.Instance.Update("[ID] IN (" + IMEX.Core.Global.Array.ToString(customerIDs) + ")", "@IsActive", 0);

                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để duyệt");
                }
            }
            catch (Exception ex)
            {
                logError.Info("DictionaryUnPublic: " + ex.ToString());
            }

            return Json("Bỏ duyệt thành công bản ghi có id là: " + IMEX.Core.Global.Array.ToString(customerIDs));

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListDictionaryDelete(int[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Từ điển bệnh lý", "/Dictionary");
            AddBreadcrumb("Danh sách bài viết", "/Dictionary/ListDictionaryDelete");
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {
                    Cms_DictionaryService.Instance.Delete("[ID] IN (" + IMEX.Core.Global.Array.ToString(customerIDs) + ")");
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("DictionaryDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + IMEX.Core.Global.Array.ToString(customerIDs));

            // Info.  
            //return View();
        }

        #endregion

    }

}
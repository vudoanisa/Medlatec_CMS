using CMS_Core.Entity;
using CMS_Core.Implementtion;
using CMSLIS.Common;
using CMSLIS.Models;
using CMSNEW.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSLIS.Controllers
{
    [CheckAuthorization]
    public class NewsController : BaseController
    {
        ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");
        // GET: News


        private Impcms_News impcms_News;
        private Impcms_Category impcms_Category;
        private Impcms_News_Cate impcms_News_Cate;
        private Impcms_News_Relate impcms_News_Relate;
        private Impcms_News_Map_Tags impcms_News_Map_Tags;
        private Impcms_News_Images impcms_News_Images;
        private Impcms_VideoCate impcms_VideoCate;
        private Impcms_Video impcms_Video;
        private Impcms_ImagesCate impcms_ImagesCate;
        private Impcms_Images impcms_Images;
        private Impcms_Banner_Plans impcms_Banner_Plans;
        private Impcms_Banner_rows impcms_Banner_rows;
        private Impcms_Banner_rows impcms_Banner_Rows;
        private Impcms_Contact impcms_Contact;
        private Impcms_Group_Doctor impcms_Group_Doctor;
        private Impcms_Doctor impcms_Doctor;
        private Impcms_Comment impcms_Comment;
        private Impcms_Doctor_Cate impcms_Doctor_Cate;
        private Impcms_Scientist_Cate impcms_Scientist_Cate;
        private Impcms_Scientist impcms_Scientist;
        public NewsController()
        {

            impcms_Category = new Impcms_Category();
            impcms_News = new Impcms_News();
            impcms_News_Cate = new Impcms_News_Cate();
            impcms_News_Relate = new Impcms_News_Relate();
            impcms_News_Map_Tags = new Impcms_News_Map_Tags();
            impcms_News_Images = new Impcms_News_Images();
            impcms_VideoCate = new Impcms_VideoCate();
            impcms_Video = new Impcms_Video();
            impcms_ImagesCate = new Impcms_ImagesCate();
            impcms_Images = new Impcms_Images();
            impcms_Banner_Plans = new Impcms_Banner_Plans();
            impcms_Banner_rows = new Impcms_Banner_rows();
            impcms_Banner_Rows = new Impcms_Banner_rows();
            impcms_Contact = new Impcms_Contact();
            impcms_Group_Doctor = new Impcms_Group_Doctor();
            impcms_Doctor = new Impcms_Doctor();
            impcms_Doctor_Cate = new Impcms_Doctor_Cate();
            impcms_Comment = new Impcms_Comment();
            impcms_Scientist_Cate = new Impcms_Scientist_Cate();
            impcms_Scientist = new Impcms_Scientist();
        }

        public ActionResult Index()
        {
            return View();
        }



        #region --> Nhập tin bài
        public ActionResult Add(string newsId)
        {
            // Initialization.  
            AddPageHeader("Tin tức", "");

            AddBreadcrumb("Tin tức", "/News");

            cms_News _News = new cms_News();
            List<cms_News> _NewsList = null;
            if (string.IsNullOrEmpty(newsId))
            {
                newsId = "0";
            }
            Session["NewsRelate" + newsId] = null;

            try
            {
                this.ViewBag.Getcms_NewsSource = Common.Common.Getcms_NewsSource();
                this.ViewBag.Getcms_NewsDoctor = Common.Common.Getcms_NewsDoctor();
                this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrentNews();
                _News.datepubText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                _News.datepubView = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                if (string.IsNullOrEmpty(newsId))
                {
                    ViewBag.Title = "Thêm mới bài viết";
                    AddBreadcrumb("Thêm mới bài viết", "/News/Add");


                }
                else if (newsId.Equals("0"))
                {
                    ViewBag.Title = "Thêm mới bài viết";
                    AddBreadcrumb("Thêm mới bài viết", "/News/Add");


                }
                else
                {
                    ViewBag.Title = "Cập nhật bài viết";
                    AddBreadcrumb("Cập nhật bài viết", "/News/Add");
                    _NewsList = impcms_News.Getcms_NewsByID(Convert.ToInt32(newsId));
                    List<cms_News> _NewsRelate = impcms_News.GetAllcms_NewsRelate(Convert.ToInt32(newsId));

                    if (_NewsRelate != null)
                    {
                        ViewBag.CountNewsRelate = "Số tin liên quan là: " + _NewsRelate.Count.ToString();

                    }


                    if (_NewsList != null)
                    {
                        this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrentBYCateID(_NewsList[0].cateId.ToString());

                        _NewsList[0].newsContent = Common.Common.FormatView(_NewsList[0].newsContent);
                        return View(_NewsList[0]);
                    }
                }


            }
            catch (Exception ex)
            {
                logError.Info("Source: " + ex.ToString());
            }

            // Info.  
            _News.LinksLimitPerPage = 5;
            _News.OptionReplate = 0;
            _News.cateId = 0;
            return View(_News);
        }

        public ActionResult AddCTV(string newsId)
        {
            // Initialization.  
            AddPageHeader("Tin tức", "");
            AddBreadcrumb("Tin tức", "/News");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            cms_News _News = new cms_News();
            List<cms_News> _NewsList = null;
            if (string.IsNullOrEmpty(newsId))
            {
                newsId = "0";
            }
            Session["NewsRelate" + newsId] = null;

            try
            {
                this.ViewBag.Getcms_NewsSource = Common.Common.Getcms_NewsSource();
                this.ViewBag.Getcms_NewsDoctor = Common.Common.Getcms_NewsDoctor();
                this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrentNews();
                _News.datepubText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                _News.datepubView = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                if (string.IsNullOrEmpty(newsId))
                {
                    ViewBag.Title = "Thêm mới bài viết";
                    AddBreadcrumb("Thêm mới bài viết", "/News/AddCTV");


                }
                else if (newsId.Equals("0"))
                {
                    ViewBag.Title = "Thêm mới bài viết";
                    AddBreadcrumb("Thêm mới bài viết", "/News/AddCTV");


                }
                else
                {
                    ViewBag.Title = "Cập nhật bài viết";
                    AddBreadcrumb("Cập nhật bài viết", "/News/AddCTV");
                    _NewsList = impcms_News.Getcms_NewsByIDUser(Convert.ToInt32(newsId), UserInfo.uid);
                    List<cms_News> _NewsRelate = impcms_News.GetAllcms_NewsRelate(Convert.ToInt32(newsId));

                    if (_NewsRelate != null)
                    {
                        ViewBag.CountNewsRelate = "Số tin liên quan là: " + _NewsRelate.Count.ToString();

                    }


                    if (_NewsList != null)
                    {
                        this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrentBYCateID(_NewsList[0].cateId.ToString());

                        _NewsList[0].newsContent = Common.Common.FormatView(_NewsList[0].newsContent);
                        return View(_NewsList[0]);
                    }
                }


            }
            catch (Exception ex)
            {
                logError.Info("Source: " + ex.ToString());
            }

            // Info.  
            _News.LinksLimitPerPage = 5;
            _News.OptionReplate = 0;
            _News.cateId = 0;
            return View(_News);
        }

        public JsonResult AddGetCategory(string query)
        {

            try
            {
                List<Location> records = new List<Location>();

                List<cms_Category> _Categories = impcms_Category.Getcms_CategoryParentNew();
                List<cms_News_Cate> _News_Cates = impcms_News_Cate.Getcms_CategoryByID(Convert.ToInt32(query));


                foreach (var data in _Categories)
                {
                    Location location = new Location();
                    location.id = data.cateId;
                    location.text = data.cateName;
                    bool ExistsItem = false;
                    if (_News_Cates != null)
                        ExistsItem = _News_Cates.Any(x => x.CateId == data.cateId);

                    location.@checked = ExistsItem;
                    location.population = 0;
                    location.flagUrl = string.Empty;

                    List<cms_Category> _CategoriesChild = impcms_Category.Getcms_CategoryChild(data.cateLevel);

                    location.children = new List<Location>();

                    foreach (var Child in _CategoriesChild)
                    {
                        Location locationChild = new Location();
                        locationChild.id = Child.cateId;
                        locationChild.text = Child.cateName;
                        bool alreadyExists = false;
                        if (_News_Cates != null)
                            alreadyExists = _News_Cates.Any(x => x.CateId == Child.cateId);


                        locationChild.@checked = alreadyExists;
                        locationChild.population = 0;
                        locationChild.flagUrl = string.Empty;

                        location.children.Add(locationChild);
                    }
                    records.Add(location);
                }



                return this.Json(records, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return this.Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(cms_News _News)
        {
            // Initialization.  
            AddPageHeader("Tin tức", "");
            AddBreadcrumb("Tin tức", "/News");
            this.ViewBag.Getcms_NewsSource = Common.Common.Getcms_NewsSource();
            this.ViewBag.Getcms_NewsDoctor = Common.Common.Getcms_NewsDoctor();
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrentNews();



            bool validateImage = true;
            string ImageFBPath = string.Empty;
            List<cms_News> _NewsRelate = new List<cms_News>();

            _News.ListNewsRelate = string.Empty;
            if (Session["NewsRelate" + _News.newsId] != null)
            {
                _NewsRelate = (List<cms_News>)Session["NewsRelate" + _News.newsId];
            }



            if (ModelState.IsValid)
            {

                if (!string.IsNullOrEmpty(_News.cateIdList))
                {
                    _News.cateIdList = _News.cateId.ToString() + "," + _News.cateIdList;
                }
                else
                {
                    _News.cateIdList = _News.cateId.ToString();
                }


                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\imageslead\\";

                //Upload Image
                if (_News.ImageFile != null)
                {
                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(_News.ImageFile.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(_News.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    _News.newsImages = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(_News.newsImages))
                        Directory.CreateDirectory(_News.newsImages);

                    _News.newsImages = _News.newsImages + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(_News.newsImages))
                    {
                        System.IO.File.Delete(_News.newsImages);
                    }

                    if (!_News.ImageFile.FileName.EndsWith("gif") && !_News.ImageFile.FileName.EndsWith("jpg") && !_News.ImageFile.FileName.EndsWith("jpeg") && !_News.ImageFile.FileName.EndsWith("jpg") && !_News.ImageFile.FileName.EndsWith("png"))
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
                        _News.ImageFile.SaveAs(_News.newsImages);
                        _News.newsImagesWeb = Common.Common.reSizeImage(_News.newsImages);
                        _News.newsImagesWeb = _News.newsImagesWeb.Replace("//", "/");
                    }
                }
                else
                {
                    validateImage = false;
                }

                ////Upload Image fb
                //if (_News.ImageFB != null)
                //{
                //    // Use Namespace called: System.IO
                //    string FileNameFB = Path.GetFileNameWithoutExtension(_News.ImageFB.FileName);

                //    //To Get File Extension
                //    string FileExtensionFB = Path.GetExtension(_News.ImageFB.FileName);

                //    //Add Current Date To Attached File Name                      
                //    ImageFBPath = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                //    if (!System.IO.Directory.Exists(ImageFBPath))
                //        Directory.CreateDirectory(ImageFBPath);

                //    ImageFBPath = ImageFBPath + "/" + DateTime.Now.ToString("yyyyMMdd") + "FB-" + FileNameFB.Trim() + FileExtensionFB;
                //    if (System.IO.File.Exists(ImageFBPath))
                //    {
                //        System.IO.File.Delete(ImageFBPath);
                //    }

                //    if (!_News.ImageFB.FileName.EndsWith("gif") && !_News.ImageFB.FileName.EndsWith("jpg") && !_News.ImageFB.FileName.EndsWith("jpeg") && !_News.ImageFB.FileName.EndsWith("jpg") && !_News.ImageFB.FileName.EndsWith("png"))
                //    {
                //        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtensionFB + "\". Chỉ upload được file  .jpeg .gif .png";
                //        validateImage = false;
                //    }

                //    if (!FileExtensionFB.EndsWith("gif") && !FileExtensionFB.EndsWith("jpg") && !FileExtensionFB.EndsWith("jpeg") && !FileExtensionFB.EndsWith("jpg") && !FileExtensionFB.EndsWith("png"))
                //    {
                //        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtensionFB + "\". Chỉ upload được file  .jpeg .gif .png";
                //        validateImage = false;
                //    }

                //    //Its Create complete path to store in server.
                //    //To copy and save file into server.
                //    _News.ImageFB.SaveAs(ImageFBPath);
                //}


                string result = string.Empty;
                if (_News.newsId == 0)
                {
                    AddBreadcrumb("Thêm mới bài viết", "/News/Add");
                    //if (_NewsRelate.Count > 0)
                    //{
                    if (validateImage)
                    {
                        _News.userId = ((cms_Account)Session["UserInfo"]).uid;
                        _News.AccountTypeId = ((cms_Account)Session["UserInfo"]).AccountTypeId;
                        _News.dateCreate = DateTime.Now;
                        _News.datepub = DateTime.ParseExact(_News.datepubView, "dd/MM/yyyy HH:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);
                        _News.newsImages = Common.Common.GetPathImage(_News.newsImages.Replace(Common.Common.getImagePath(), ""));
                        _News.newsImagesWeb = Common.Common.GetPathImage(_News.newsImagesWeb.Replace(Common.Common.getImagePath().ToLower(), ""));
                        _News.newsContent = Common.Common.FormatSave(_News.newsContent);
                        _News.LinksLimitPerPage = 5;
                        _News.OptionReplate = 0;
                        string ContentAutoLink = Common.Common.AddAutoLink(_News.newsContent, _News.LinksLimitPerPage, _News.OptionReplate);
                        _News.newsContentAutoLink = ContentAutoLink;

                        result = impcms_News.Insertcms_News(_News);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới bài viết thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());

                            // chèn tin bài liên quan

                            if (_NewsRelate.Count > 0)
                            {
                                foreach (var NewsRelate in _NewsRelate)
                                {

                                    impcms_News_Relate.Insertcms_News_Relate(NewsRelate, result);

                                }
                            }

                            // _News.cateIdList



                            // Chèn tag bài viết

                            cms_News_Map_Tags _News_Map_Tags = new cms_News_Map_Tags();
                            _News_Map_Tags.newsId = Convert.ToInt32(result);
                            _News_Map_Tags.ListTags = _News.Tags;
                            impcms_News_Map_Tags.Insertcms_News_Map_Tags(_News_Map_Tags);

                            Session["NewsRelate" + _News.newsId] = null;

                            if (ImageFBPath.Length > 5)
                            {

                                cms_News_Images _News_Images = new cms_News_Images();
                                _News_Images.Image = ImageFBPath;
                                _News_Images.newsId = Convert.ToInt32(result);
                                _News_Images.Type = Common.Constant.Images_TypeFB;
                                impcms_News_Images.Insertcms_News_Images(_News_Images);
                            }

                            Response.Redirect("/News/ListNews", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới bài viết không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Mời bạn nhập ảnh!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                    //}
                    //else
                    //{
                    //    ViewBag.TitleSuccsess = "Mời bạn nhập tin bài liên quan!";
                    //}
                }
                else
                {
                    AddBreadcrumb("Cập nhật bài viết", "/News/Add");
                    _News.userId = ((cms_Account)Session["UserInfo"]).uid;
                    _News.AccountTypeId = ((cms_Account)Session["UserInfo"]).AccountTypeId;
                    _News.dateCreate = DateTime.Now;
                    _News.datepub = DateTime.ParseExact(_News.datepubView, "dd/MM/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                    //DateTime.Now;
                    _News.newsImages = Common.Common.GetPathImage(_News.newsImages.Replace(Common.Common.getImagePath(), ""));
                    _News.newsImagesWeb = Common.Common.GetPathImage(_News.newsImagesWeb.Replace(Common.Common.getImagePath().ToLower(), ""));
                    _News.newsContent = Common.Common.FormatSave(_News.newsContent);
                    _News.LinksLimitPerPage = 5;
                    _News.OptionReplate = 0;

                    string ContentAutoLink = Common.Common.AddAutoLink(_News.newsContent, _News.LinksLimitPerPage, _News.OptionReplate);
                    _News.newsContentAutoLink = ContentAutoLink;

                    result = impcms_News.Updatecms_News(_News);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin bài viết thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());

                        // chèn tin bài liên quan
                        string newsIdRelate = string.Empty;
                        Impcms_News_Relate impcms_News_Relate = new Impcms_News_Relate();
                        if (_NewsRelate.Count > 0)
                        {
                            foreach (var NewsRelate in _NewsRelate)
                            {
                                impcms_News_Relate.Insertcms_News_Relate(NewsRelate, _News.newsId.ToString());
                                newsIdRelate = newsIdRelate + NewsRelate.newsId.ToString() + ",";
                            }
                        }

                        if (newsIdRelate.Length > 1)
                        {
                            newsIdRelate = newsIdRelate.Substring(0, newsIdRelate.Length - 1);
                            impcms_News_Relate.Deletecms_News_Relate(newsIdRelate, _News.newsId.ToString());
                        }



                        // Chèn tag bài viết

                        cms_News_Map_Tags _News_Map_Tags = new cms_News_Map_Tags();
                        _News_Map_Tags.newsId = _News.newsId;
                        _News_Map_Tags.ListTags = _News.Tags;
                        impcms_News_Map_Tags.Insertcms_News_Map_Tags(_News_Map_Tags);
                        Session["NewsRelate" + _News.newsId] = null;

                        if (ImageFBPath.Length > 5)
                        {

                            cms_News_Images _News_Images = new cms_News_Images();
                            _News_Images.Image = ImageFBPath;
                            _News_Images.newsId = Convert.ToInt32(result);
                            _News_Images.Type = Common.Constant.Images_TypeFB;
                            impcms_News_Images.Insertcms_News_Images(_News_Images);
                        }

                        Response.Redirect("/News/ListNews", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin bài viết không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }
            }


            return View(_News);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddCTV(cms_News _News)
        {
            // Initialization.  
            AddPageHeader("Tin tức", "");
            AddBreadcrumb("Tin tức", "/News");
            this.ViewBag.Getcms_NewsSource = Common.Common.Getcms_NewsSource();
            this.ViewBag.Getcms_NewsDoctor = Common.Common.Getcms_NewsDoctor();
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrentNews();

            var UserInfo = ((cms_Account)Session["UserInfo"]);

            bool validateImage = true;
            string ImageFBPath = string.Empty;
            List<cms_News> _NewsRelate = new List<cms_News>();

            _News.ListNewsRelate = string.Empty;
            if (Session["NewsRelate" + _News.newsId] != null)
            {
                _NewsRelate = (List<cms_News>)Session["NewsRelate" + _News.newsId];
            }



            if (ModelState.IsValid)
            {

                if (!string.IsNullOrEmpty(_News.cateIdList))
                {
                    _News.cateIdList = _News.cateId.ToString() + "," + _News.cateIdList;
                }
                else
                {
                    _News.cateIdList = _News.cateId.ToString();
                }


                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\imageslead\\";

                //Upload Image
                if (_News.ImageFile != null)
                {
                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(_News.ImageFile.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(_News.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    _News.newsImages = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(_News.newsImages))
                        Directory.CreateDirectory(_News.newsImages);

                    _News.newsImages = _News.newsImages + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(_News.newsImages))
                    {
                        System.IO.File.Delete(_News.newsImages);
                    }

                    if (!_News.ImageFile.FileName.EndsWith("gif") && !_News.ImageFile.FileName.EndsWith("jpg") && !_News.ImageFile.FileName.EndsWith("jpeg") && !_News.ImageFile.FileName.EndsWith("jpg") && !_News.ImageFile.FileName.EndsWith("png"))
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
                        _News.ImageFile.SaveAs(_News.newsImages);
                        _News.newsImagesWeb = Common.Common.reSizeImage(_News.newsImages);
                        _News.newsImagesWeb = _News.newsImagesWeb.Replace("//", "/");
                    }
                }
                else
                {
                    validateImage = false;
                }
                 

                string result = string.Empty;
                if (_News.newsId == 0)
                {
                    AddBreadcrumb("Thêm mới bài viết", "/News/AddCTV");
                    //if (_NewsRelate.Count > 0)
                    //{
                    if (validateImage)
                    {
                        _News.userId = ((cms_Account)Session["UserInfo"]).uid;
                        _News.AccountTypeId = ((cms_Account)Session["UserInfo"]).AccountTypeId;
                        _News.dateCreate = DateTime.Now;
                        _News.datepub = DateTime.ParseExact(_News.datepubView, "dd/MM/yyyy HH:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);
                        _News.newsImages = Common.Common.GetPathImage(_News.newsImages.Replace(Common.Common.getImagePath(), ""));
                        _News.newsImagesWeb = Common.Common.GetPathImage(_News.newsImagesWeb.Replace(Common.Common.getImagePath().ToLower(), ""));
                        _News.newsContent = Common.Common.FormatSave(_News.newsContent);
                        _News.LinksLimitPerPage = 5;
                        _News.OptionReplate = 0;
                        string ContentAutoLink = Common.Common.AddAutoLink(_News.newsContent, _News.LinksLimitPerPage, _News.OptionReplate);
                        _News.newsContentAutoLink = ContentAutoLink;

                        result = impcms_News.Insertcms_News(_News);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới bài viết thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());

                            // chèn tin bài liên quan

                            if (_NewsRelate.Count > 0)
                            {
                                foreach (var NewsRelate in _NewsRelate)
                                {

                                    impcms_News_Relate.Insertcms_News_Relate(NewsRelate, result);

                                }
                            }

                            // _News.cateIdList



                            // Chèn tag bài viết

                            cms_News_Map_Tags _News_Map_Tags = new cms_News_Map_Tags();
                            _News_Map_Tags.newsId = Convert.ToInt32(result);
                            _News_Map_Tags.ListTags = _News.Tags;
                            impcms_News_Map_Tags.Insertcms_News_Map_Tags(_News_Map_Tags);

                            Session["NewsRelate" + _News.newsId] = null;

                            if (ImageFBPath.Length > 5)
                            {

                                cms_News_Images _News_Images = new cms_News_Images();
                                _News_Images.Image = ImageFBPath;
                                _News_Images.newsId = Convert.ToInt32(result);
                                _News_Images.Type = Common.Constant.Images_TypeFB;
                                impcms_News_Images.Insertcms_News_Images(_News_Images);
                            }

                            Response.Redirect("/News/ListNewsCTV", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới bài viết không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Mời bạn nhập ảnh!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                    //}
                    //else
                    //{
                    //    ViewBag.TitleSuccsess = "Mời bạn nhập tin bài liên quan!";
                    //}
                }
                else
                {
                    AddBreadcrumb("Cập nhật bài viết", "/News/AddCTV");
                    _News.userId = ((cms_Account)Session["UserInfo"]).uid;
                    _News.AccountTypeId = ((cms_Account)Session["UserInfo"]).AccountTypeId;
                    _News.dateCreate = DateTime.Now;
                    _News.datepub = DateTime.ParseExact(_News.datepubView, "dd/MM/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                    //DateTime.Now;
                    _News.newsImages = Common.Common.GetPathImage(_News.newsImages.Replace(Common.Common.getImagePath(), ""));
                    _News.newsImagesWeb = Common.Common.GetPathImage(_News.newsImagesWeb.Replace(Common.Common.getImagePath().ToLower(), ""));
                    _News.newsContent = Common.Common.FormatSave(_News.newsContent);
                    _News.LinksLimitPerPage = 5;
                    _News.OptionReplate = 0;

                    string ContentAutoLink = Common.Common.AddAutoLink(_News.newsContent, _News.LinksLimitPerPage, _News.OptionReplate);
                    _News.newsContentAutoLink = ContentAutoLink;

                    result = impcms_News.Updatecms_News(_News);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin bài viết thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());

                        // chèn tin bài liên quan
                        string newsIdRelate = string.Empty;
                        Impcms_News_Relate impcms_News_Relate = new Impcms_News_Relate();
                        if (_NewsRelate.Count > 0)
                        {
                            foreach (var NewsRelate in _NewsRelate)
                            {
                                impcms_News_Relate.Insertcms_News_Relate(NewsRelate, _News.newsId.ToString());
                                newsIdRelate = NewsRelate.newsId.ToString() + ",";
                            }
                        }

                        if (newsIdRelate.Length > 1)
                        {
                            newsIdRelate = newsIdRelate.Substring(0, newsIdRelate.Length - 1);
                            impcms_News_Relate.Deletecms_News_Relate(newsIdRelate, _News.newsId.ToString());
                        }



                        // Chèn tag bài viết

                        cms_News_Map_Tags _News_Map_Tags = new cms_News_Map_Tags();
                        _News_Map_Tags.newsId = _News.newsId;
                        _News_Map_Tags.ListTags = _News.Tags;
                        impcms_News_Map_Tags.Insertcms_News_Map_Tags(_News_Map_Tags);
                        Session["NewsRelate" + _News.newsId] = null;

                        if (ImageFBPath.Length > 5)
                        {

                            cms_News_Images _News_Images = new cms_News_Images();
                            _News_Images.Image = ImageFBPath;
                            _News_Images.newsId = Convert.ToInt32(result);
                            _News_Images.Type = Common.Constant.Images_TypeFB;
                            impcms_News_Images.Insertcms_News_Images(_News_Images);
                        }

                        Response.Redirect("/News/ListNewsCTV", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin bài viết không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }
            }


            return View(_News);
        }


        public ActionResult AddUploadimg()
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Upload ảnh", "/News/AddUploadimg");

            // Info.  
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUploadimg(HttpPostedFileBase[] files)
        {
            List<string> ImagePaths = new List<string>();

            bool validateImage = true;
            //Get Upload path from Web.Config file AppSettings.
            string UploadPath = Common.Common.getImagePath() + "ImagePath\\images\\";


            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        //Add Current Date To Attached File Name                      
                        string ImagePath = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                        if (!System.IO.Directory.Exists(ImagePath))
                            Directory.CreateDirectory(ImagePath);


                        // Use Namespace called: System.IO
                        string FileName = Path.GetFileNameWithoutExtension(file.FileName);

                        //To Get File Extension
                        string FileExtension = Path.GetExtension(file.FileName);

                        ImagePath = ImagePath + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                        if (System.IO.File.Exists(ImagePath))
                        {
                            System.IO.File.Delete(ImagePath);
                        }

                        if (!file.FileName.EndsWith("gif") && !file.FileName.EndsWith("jpg") && !file.FileName.EndsWith("jpeg") && !file.FileName.EndsWith("jpg") && !file.FileName.EndsWith("png"))
                        {
                            ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                            validateImage = false;
                        }

                        if (!FileExtension.EndsWith("gif") && !FileExtension.EndsWith("jpg") && !FileExtension.EndsWith("jpeg") && !FileExtension.EndsWith("jpg") && !FileExtension.EndsWith("png"))
                        {
                            ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                            validateImage = false;
                        }

                        if (validateImage)
                        {
                            //Save file to server folder  
                            file.SaveAs(ImagePath);
                            Common.Common.reSizeImageDetail(ImagePath);



                            ImagePath = Common.Common.getImageLink() + ImagePath.Replace(Common.Common.getImagePath(), "");
                           

                            ImagePaths.Add(ImagePath);

                        }
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadImagePaths = ImagePaths;
                    }

                }
            }

            return View();
        }


        public ActionResult AddUploadVideo()
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Upload video", "/News/AddUploadVideo");

            // Info.  
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUploadVideo(HttpPostedFileBase fileupload)
        {

            bool validateVideo = true;
            //Get Upload path from Web.Config file AppSettings.
            string UploadPath = Common.Common.getImagePath() + "ImagePath\\media\\";


            if (ModelState.IsValid)
            {   //iterating through multiple file collection   

                //Checking file is available to save.  
                if (fileupload != null)
                {
                    //Add Current Date To Attached File Name                      
                    string VideoPath = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(VideoPath))
                        Directory.CreateDirectory(VideoPath);


                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(fileupload.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(fileupload.FileName);

                    VideoPath = VideoPath + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(VideoPath))
                    {
                        System.IO.File.Delete(VideoPath);
                    }

                    if (!fileupload.FileName.EndsWith("mp4"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .mp4";
                        validateVideo = false;
                    }

                    if (!FileExtension.EndsWith("mp4"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .mp4";
                        validateVideo = false;
                    }

                    if (validateVideo)
                    {
                        //Save file to server folder  
                        fileupload.SaveAs(VideoPath);

                        VideoPath = Common.Common.getImageLink() + VideoPath.Replace(Common.Common.getImagePath(), "");
                    }
                    //assigning file uploaded status to ViewBag for showing message to user.  
                    ViewBag.UploadVideoPath = VideoPath;
                }


            }

            return View();
        }

        public ActionResult AddRelateTestcode(string testcodeID)
        {
            SearchNewsViewModel searchNewsView = new SearchNewsViewModel();
            try
            {
                List<cms_News> _NewsRelate = null;

                if (string.IsNullOrEmpty(testcodeID))
                {
                    testcodeID = "0";
                }
                else
                {
                    if (testcodeID != "0" && Session["testcodeRelate" + testcodeID] == null)
                    {
                        _NewsRelate = impcms_News.GetAllcms_NewsRelateTestcode(Convert.ToInt32(testcodeID));
                        Session["testcodeRelate" + testcodeID] = _NewsRelate;
                    }
                }

                this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
                this.ViewBag.Getcms_Account = Common.Common.Getcms_Account();
                searchNewsView.ParrenId = 0;
                searchNewsView.newsId = Convert.ToInt32(testcodeID);

                List<cms_News> _News = impcms_News.GetAllcms_NewsSearch(0);

                ViewBag.DataReport = _News;
                if (Session["testcodeRelate" + testcodeID] != null)
                {
                    ViewBag.DataChoi = (List<cms_News>)Session["testcodeRelate" + testcodeID];
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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddRelateTestcode(SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  

            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
            this.ViewBag.Getcms_Account = Common.Common.Getcms_Account();



            try
            {
                switch (submit)
                {
                    case "SearchAddRelateTestcode":
                        List<cms_News> _News = impcms_News.GetAllcms_NewsSearchNew(searchNewsView.ParrenId, searchNewsView.keyword);

                        ViewBag.DataReport = _News;

                        _News = impcms_News.GetAllcms_NewsRelate(Convert.ToInt32(searchNewsView.ParrenId));
                        //  ViewBag.DataReport = _News;
                        if (Session["testcodeRelate" + searchNewsView.newsId] != null)
                        {
                            ViewBag.DataChoi = (List<cms_News>)Session["testcodeRelate" + searchNewsView.newsId];
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

        public ActionResult ChoiAddRelateTestcode(string[] customerIDs, string newid)
        {
            // Initialization.  

            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    List<cms_News> _NewsRelate = new List<cms_News>();

                    if (Session["testcodeRelate" + newid] != null)
                    {
                        _NewsRelate = (List<cms_News>)Session["testcodeRelate" + newid];
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
                    Session["testcodeRelate" + newid] = _NewsRelate;

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

            return Json("Chọn thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]

        public ActionResult DeleteAddRelateTestcode(string[] customerIDs, string newid)
        {
            // Initialization.  

            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {



                    List<cms_News> _NewsRelate = new List<cms_News>();
                    if (Session["testcodeRelate" + newid] != null)
                    {
                        _NewsRelate = (List<cms_News>)Session["testcodeRelate" + newid];
                    }

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        string customerIDValue = customerID.Replace("[\"", "");
                        customerIDValue = customerIDValue.Replace("\"]", "");
                        customerIDValue = customerIDValue.Replace("\\", "");
                        customerIDValue = customerIDValue.Replace("\"", "");


                        string[] Values = customerIDValue.Split(',');
                        foreach (string Value in Values)
                        {
                            foreach (var NewsRelate in _NewsRelate)
                            {
                                if (NewsRelate.newsId.ToString().Equals(Value))
                                {
                                    _NewsRelate.Remove(NewsRelate);
                                    break;
                                }
                            }
                            listID += Value + ",";
                        }


                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    Session["testcodeRelate" + newid] = _NewsRelate;
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("DeleteAddRelate: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
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
                    if (newsId != "0" && Session["NewsRelate" + newsId] == null)
                    {
                        _NewsRelate = impcms_News.GetAllcms_NewsRelate(Convert.ToInt32(newsId));
                        Session["NewsRelate" + newsId] = _NewsRelate;
                    }
                }

                this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
                this.ViewBag.Getcms_Account = Common.Common.Getcms_Account();
                searchNewsView.ParrenId = 0;
                searchNewsView.newsId = Convert.ToInt32(newsId);

                List<cms_News> _News = impcms_News.GetAllcms_NewsSearch(0);

                ViewBag.DataReport = _News;
                if (Session["NewsRelate" + newsId] != null)
                {
                    ViewBag.DataChoi = (List<cms_News>)Session["NewsRelate" + newsId];
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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddRelate(SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  

            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
            this.ViewBag.Getcms_Account = Common.Common.Getcms_Account();



            try
            {
                switch (submit)
                {
                    case "SearchAddRelate":
                        List<cms_News> _News = impcms_News.GetAllcms_NewsSearchNew(searchNewsView.ParrenId, searchNewsView.keyword);

                        ViewBag.DataReport = _News;

                        _News = impcms_News.GetAllcms_NewsRelate(Convert.ToInt32(searchNewsView.ParrenId));
                        //  ViewBag.DataReport = _News;
                        if (Session["NewsRelate" + searchNewsView.newsId] != null)
                        {
                            ViewBag.DataChoi = (List<cms_News>)Session["NewsRelate" + searchNewsView.newsId];
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


        public ActionResult ChoiAddRelate(string[] customerIDs, string newid)
        {
            // Initialization.  

            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    List<cms_News> _NewsRelate = new List<cms_News>();

                    if (Session["NewsRelate" + newid] != null)
                    {
                        _NewsRelate = (List<cms_News>)Session["NewsRelate" + newid];
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
                    Session["NewsRelate" + newid] = _NewsRelate;

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

            return Json("Chọn thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]

        public ActionResult DeleteAddRelate(string[] customerIDs, string newid)
        {
            // Initialization.  

            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {



                    List<cms_News> _NewsRelate = new List<cms_News>();
                    if (Session["NewsRelate" + newid] != null)
                    {
                        _NewsRelate = (List<cms_News>)Session["NewsRelate" + newid];
                    }

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        string customerIDValue = customerID.Replace("[\"", "");
                        customerIDValue = customerIDValue.Replace("\"]", "");
                        customerIDValue = customerIDValue.Replace("\\", "");
                        customerIDValue = customerIDValue.Replace("\"", "");


                        string[] Values = customerIDValue.Split(',');
                        foreach (string Value in Values)
                        {
                            foreach (var NewsRelate in _NewsRelate)
                            {
                                if (NewsRelate.newsId.ToString().Equals(Value))
                                {
                                    _NewsRelate.Remove(NewsRelate);
                                    break;
                                }
                            }
                            listID += Value + ",";
                        }


                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    Session["NewsRelate" + newid] = _NewsRelate;
                }
                else
                {
                    return Json("Chưa có bản tin nào được chọn để xóa");
                }
            }
            catch (Exception ex)
            {
                logError.Info("DeleteAddRelate: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }



        #endregion


        #region --> Danh sách bài viết
        public ActionResult ListNews()
        {
            // Initialization.  
            AddPageHeader("Tin tức", "");
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách bài viết", "/News/ListNews");
            SearchNewsViewModel searchNewsView = new SearchNewsViewModel();
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {
                this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
                this.ViewBag.Getcms_NewsSource = Common.Common.Getcms_NewsSource();
                //this.ViewBag.Getcms_NewsTypeMessage = Common.Common.Getcms_NewsTypeMessage();
                this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
                this.ViewBag.Getcms_Account = Common.Common.Getcms_Account();

                //Impcms_ControlName impcms_ControlName = new Impcms_ControlName();
                //List<cms_ControlName> cms_ControlNames=  impcms_ControlName.GetAllcms_ControlName();




                cms_ControlName _ControlName = new cms_ControlName();
                _ControlName.ControlID = "cmdPublich";
                _ControlName.ControlName = "Xuất bản ";
                _ControlName.LangID = 1;
                _ControlName.ControlDes = "Xuất bản bài viết";
                _ControlName.ControlStatus = Constant.TypeStatusPending;
                _ControlName.menID = 9;


                //impcms_ControlName.Insertcms_ControlName(_ControlName);

                searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
                searchNewsView.tungay = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                searchNewsView.ParrenId = 0;
                searchNewsView.SourceId = 0;
                searchNewsView.Status = Common.Constant.TypeStatusAll;
                searchNewsView.keyword = string.Empty;

                //this.ViewBag.Getcms_CategoryChild = Common.Common.Getcms_CategoryChild(searchNewsView.ParrenId);
                Impcms_News impcms_News = new Impcms_News();
                List<cms_News> _News = new List<cms_News>();
                if (UserInfo.AccountTypeId == 6)
                {
                    searchNewsView.userId = UserInfo.uid;
                    _News = impcms_News.GetAllcms_News(DateTime.Now.AddDays(-7), DateTime.Now, 0, 0, 0, 0, searchNewsView.userId, Common.Constant.TypeStatusAll, string.Empty);
                }
                else
                {

                    _News = impcms_News.GetAllcms_News(DateTime.Now.AddDays(-7), DateTime.Now, 0, 0, 0, 0, 0, Common.Constant.TypeStatusAll, string.Empty);

                }
                ViewBag.DataReport = _News;



            }
            catch (Exception ex)
            {
                logError.Info("ListNews: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }

        public ActionResult ListNewsCTV()
        {
            // Initialization.  
            AddPageHeader("Tin tức", "");
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách bài viết", "/News/ListNewsCTV");
            SearchNewsViewModel searchNewsView = new SearchNewsViewModel();
            var UserInfo = ((cms_Account)Session["UserInfo"]);

            try
            {
                this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
                this.ViewBag.Getcms_NewsSource = Common.Common.Getcms_NewsSource();

                this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
                this.ViewBag.Getcms_Account = Common.Common.Getcms_Account();





                cms_ControlName _ControlName = new cms_ControlName();
                _ControlName.ControlID = "cmdPublich";
                _ControlName.ControlName = "Xuất bản ";
                _ControlName.LangID = 1;
                _ControlName.ControlDes = "Xuất bản bài viết";
                _ControlName.ControlStatus = Constant.TypeStatusPending;
                _ControlName.menID = 9;


                //impcms_ControlName.Insertcms_ControlName(_ControlName);

                searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
                searchNewsView.tungay = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                searchNewsView.ParrenId = 0;
                searchNewsView.SourceId = 0;
                searchNewsView.Status = Common.Constant.TypeStatusAll;
                searchNewsView.keyword = string.Empty;

                //this.ViewBag.Getcms_CategoryChild = Common.Common.Getcms_CategoryChild(searchNewsView.ParrenId);
                Impcms_News impcms_News = new Impcms_News();
                List<cms_News> _News = new List<cms_News>();

                searchNewsView.userId = UserInfo.uid;
                _News = impcms_News.GetAllcms_NewsCTV(DateTime.Now.AddDays(-7), DateTime.Now, 0, 0, 0, 0, searchNewsView.userId, Common.Constant.TypeStatusAll, string.Empty);

                ViewBag.DataReport = _News;



            }
            catch (Exception ex)
            {
                logError.Info("ListNews: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListNewsCTV(SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  
            AddPageHeader("Tin tức", "");
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách bài viết", "/News/ListNewsCTV");
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
            this.ViewBag.Getcms_NewsSource = Common.Common.Getcms_NewsSource();

            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.Getcms_Account = Common.Common.Getcms_Account();
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();

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
                }

                //if (Tungay.Date.AddMonths(10) < Denngay.Date)
                //{
                //    ViewBag.TitleSuccsess = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                //}
            }
            catch
            {
                ViewBag.TitleSuccsess = "Dữ liệu ngày tháng không đúng định dạng";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }
            #endregion

            try
            {
                switch (submit)
                {
                    case "SearchListNews":
                        List<cms_News> _News = impcms_News.GetAllcms_News(Tungay, Denngay, Convert.ToInt32(searchNewsView.SourceId), Convert.ToInt32(searchNewsView.ParrenId), Convert.ToInt32(searchNewsView.cateId), Convert.ToInt32(searchNewsView.newsTypeMessage), UserInfo.uid, Convert.ToInt32(searchNewsView.Status), searchNewsView.keyword);
                        ViewBag.DataReport = _News;
                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListNews:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }




        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListNewsDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách bài viết", "/News/ListNewsDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    List<cms_News> _News = null;


                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_News.Deletecms_News(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionDeleteOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    _News = impcms_News.GetAllcms_News();
                    ViewBag.DataReport = _News;
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
        public ActionResult ListNewsPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách bài viết", "/News/ListNewsPublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    List<cms_News> _News = null;

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_News.Publiccms_News(Convert.ToInt32(customerID));
                        AddLogAction(customerID, Constant.ActionPublicOK.ToString());
                        listID += customerID + ",";
                    }
                    if (listID.IndexOf(",") != -1)
                    {
                        listID = listID.Substring(0, listID.Length - 1);
                    }

                    _News = impcms_News.GetAllcms_News();
                    ViewBag.DataReport = _News;
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListNews(SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  
            AddPageHeader("Tin tức", "");
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách bài viết", "/News/ListNews");
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrent();
            this.ViewBag.Getcms_NewsSource = Common.Common.Getcms_NewsSource();

            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.Getcms_Account = Common.Common.Getcms_Account();

            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();

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
                }

                //if (Tungay.Date.AddMonths(10) < Denngay.Date)
                //{
                //    ViewBag.TitleSuccsess = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                //}
            }
            catch
            {
                ViewBag.TitleSuccsess = "Dữ liệu ngày tháng không đúng định dạng";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }
            #endregion

            try
            {
                switch (submit)
                {
                    case "SearchListNews":
                        List<cms_News> _News = impcms_News.GetAllcms_News(Tungay, Denngay, Convert.ToInt32(searchNewsView.SourceId), Convert.ToInt32(searchNewsView.ParrenId), Convert.ToInt32(searchNewsView.cateId), Convert.ToInt32(searchNewsView.newsTypeMessage), Convert.ToInt32(searchNewsView.userId), Convert.ToInt32(searchNewsView.Status), searchNewsView.keyword);
                        ViewBag.DataReport = _News;
                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListNews:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }



        #endregion



        #region --> Danh sách chuyên mục video
        public ActionResult ListCateVideo()
        {
            // Initialization.  
            AddPageHeader("Chuyên mục video", "");
            AddBreadcrumb("Chuyên mục video", "/News/ListCateVideo");
            AddBreadcrumb("Danh sách chuyên mục video", "/News/ListCateVideo");
            SearchNewsViewModel searchNewsView = new SearchNewsViewModel();
            try
            {
                List<cms_VideoCate> _VideoCates = impcms_VideoCate.GetAllcms_VideoCate();
                ViewBag.VideoCates = _VideoCates;

            }
            catch (Exception ex)
            {
                logError.Info("ListNews: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }



        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListCateVideoDelete(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Chuyên mục video", "");
            AddBreadcrumb("Chuyên mục video", "/News/ListCateVideo");
            AddBreadcrumb("Danh sách chuyên mục video", "/News/ListCateVideo");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_VideoCate.Deletecms_VideoCate(Convert.ToInt32(customerID));
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
        public ActionResult ListCateVideoPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách chuyên mục video", "/News/ListCateVideo");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_VideoCate.Publichcms_VideoCate(Convert.ToInt32(customerID));
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


        public ActionResult ListCateVideoAdd(string vId)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục video", "");
            AddBreadcrumb("Chuyên mục video", "/News/ListCateVideo");
            AddBreadcrumb("Thêm mới chuyên mục video", "/News/ListCateVideoAdd");
            cms_VideoCate _VideoCate = new cms_VideoCate();
            List<cms_VideoCate> _VideoCates = null;

            try
            {
                if (string.IsNullOrEmpty(vId))
                {
                    ViewBag.panelTitle = "Thêm mới chuyên mục video";
                    _VideoCate.vId = 0;
                }
                else
                {
                    ViewBag.panelTitle = "Cập nhật chuyên mục video";
                    _VideoCates = impcms_VideoCate.Getcms_VideoCateByID(Convert.ToInt32(vId));
                    if (_VideoCates != null)
                    {
                        return View(_VideoCates[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListCateVideoAdd:" + ex.ToString());
            }

            // Info.  
            return View(_VideoCate);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListCateVideoAdd(cms_VideoCate _VideoCate)
        {

            AddPageHeader("Chuyên mục video", "");
            AddBreadcrumb("Chuyên mục video", "/News/ListCateVideo");
            AddBreadcrumb("Thêm mới chuyên mục video", "/News/ListCateVideoAdd");
            if (ModelState.IsValid)
            {


                string result = string.Empty;
                if (_VideoCate.vId == 0)
                {
                    result = impcms_VideoCate.Insertcms_VideoCate(_VideoCate);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Thêm mới chuyên mục thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionInsertOK.ToString());
                        Response.Redirect("/News/ListCateVideo", false);
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
                    result = impcms_VideoCate.Updatecms_VideoCate(_VideoCate);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin chuyên mục thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                        Response.Redirect("/News/ListCateVideo", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin chuyên mục không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }
            }



            return View(_VideoCate);
        }
        #endregion


        #region --> Danh sách  video
        public ActionResult ListVideo()
        {
            // Initialization.  
            AddPageHeader("Chuyên mục video", "");
            AddBreadcrumb("Danh sách  video", "/News/ListVideo");            
            
            CMS_Core.Models.SearchNewsViewModel searchNewsView = new CMS_Core.Models.SearchNewsViewModel();
            try
            {
                searchNewsView.tungay = DateTime.Now.AddDays(-14).ToString("dd/MM/yyyy");
                searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
                searchNewsView.keyword = string.Empty;
                searchNewsView.ParrenId = 0;

                List<cms_Video> cms_Videos = impcms_Video.Getcms_VideoByVideoCate(searchNewsView);
                ViewBag.cms_Videos = cms_Videos;
            }
            catch (Exception ex)
            {
                logError.Info("ListVideo: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListVideo(CMS_Core.Models.SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  
            AddPageHeader("Chuyên mục video", "");
            AddBreadcrumb("Danh sách  video", "/News/ListVideo");
            
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();

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
                }

                if (Tungay.Date.AddMonths(10) < Denngay.Date)
                {
                    ViewBag.TitleSuccsess = "Chỉ được phép tra cứu tối đa trong vòng 10 tháng";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            catch
            {
                ViewBag.TitleSuccsess = "Dữ liệu ngày tháng không đúng định dạng";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }
            #endregion


            try
            {
                switch (submit)
                {
                    case "SearchListVideo":

                        List<cms_Video> cms_Videos = impcms_Video.Getcms_VideoByVideoCate(searchNewsView);
                        ViewBag.cms_Videos = cms_Videos;
                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListVideo:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);


        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListVideoDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách  video", "/News/ListVideoDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Video.Deletecms_Video(Convert.ToInt32(customerID));
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
        public ActionResult ListVideoPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách  video", "/News/ListVideoPublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Video.Publiccms_Video(Convert.ToInt32(customerID));
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


        public ActionResult ListVideoAdd(string videoId)
        {
            // Initialization.   
            AddPageHeader("Chuyên mục video", "");
            AddBreadcrumb("Danh sách  video", "/News/ListVideo");

           

            cms_Video _Video = new cms_Video();
            List<cms_Video> _Videos = null;

            try
            {
                if (string.IsNullOrEmpty(videoId))
                {
                    AddBreadcrumb("Thêm mới video", "/News/ListVideoAdd");
                    ViewBag.Title = "Thêm mới video";
                    _Video.videoId = 0;
                    _Video.datepubView = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                }
                else
                {
                    AddBreadcrumb("Cập nhật video", "/News/ListVideoAdd");
                    ViewBag.Title = "Cập nhật video";
                    _Videos = impcms_Video.Getcms_VideoByID(Convert.ToInt32(videoId));
                    if (_Videos != null)
                    {
                        return View(_Videos[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListVideoAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_Video);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ListVideoAdd(cms_Video _Video)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục video", "");
            AddBreadcrumb("Danh sách  video", "/News/ListVideo");

            bool validateImage = true;
            bool validateVideo = true;

            try
            {
                if (ModelState.IsValid)
                {

                    //Get Upload path from Web.Config file AppSettings.
                    string UploadPath = Common.Common.getImagePath() + "ImagePath\\imagesvideo\\";

                    //Upload Image
                    if (_Video.ImageFile != null)
                    {
                        // Use Namespace called: System.IO
                        string FileName = Path.GetFileNameWithoutExtension(_Video.ImageFile.FileName);

                        //To Get File Extension
                        string FileExtension = Path.GetExtension(_Video.ImageFile.FileName);

                        //Add Current Date To Attached File Name                      
                        _Video.VideoImageThumb = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                        if (!System.IO.Directory.Exists(_Video.VideoImageThumb))
                            Directory.CreateDirectory(_Video.VideoImageThumb);

                        _Video.VideoImageThumb = _Video.VideoImageThumb + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim() + FileExtension;
                        if (System.IO.File.Exists(_Video.VideoImageThumb))
                        {
                            System.IO.File.Delete(_Video.VideoImageThumb);
                        }

                        if (!_Video.ImageFile.FileName.ToLower().EndsWith("gif") && !_Video.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Video.ImageFile.FileName.ToLower().EndsWith("jpeg") && !_Video.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Video.ImageFile.FileName.ToLower().EndsWith("png"))
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
                            _Video.ImageFile.SaveAs(_Video.VideoImageThumb);
                            _Video.VideoFileWeb = Common.Common.reSizeImage(_Video.VideoImageThumb);
                            _Video.VideoFileWeb = _Video.VideoFileWeb.Replace("//", "/");
                            _Video.VideoFileWeb = Common.Common.GetPathImage(_Video.VideoFileWeb.Replace(Common.Common.getImagePath().ToLower(), ""));

                        }
                    }
                    else
                    {
                        validateImage = false;
                    }


                    //Get Upload path from Web.Config file AppSettings.
                    string UploadVideoPath = Common.Common.getImagePath() + "ImagePath\\imagesvideo\\";

                    //Upload Image
                    if (_Video.VideoFilePath != null)
                    {
                        // Use Namespace called: System.IO
                        string FileName = Path.GetFileNameWithoutExtension(_Video.VideoFilePath.FileName);

                        //To Get File Extension
                        string FileExtension = Path.GetExtension(_Video.VideoFilePath.FileName);

                        //Add Current Date To Attached File Name                      
                        _Video.VideoFile = UploadVideoPath + DateTime.Now.ToString("yyyyMMdd");
                        if (!System.IO.Directory.Exists(_Video.VideoFile))
                            Directory.CreateDirectory(_Video.VideoFile);

                        _Video.VideoFile = _Video.VideoFile + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                        if (System.IO.File.Exists(_Video.VideoFile))
                        {
                            System.IO.File.Delete(_Video.VideoFile);
                        }

                        if (!_Video.VideoFilePath.FileName.ToLower().EndsWith("mp4"))
                        {
                            ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .mp4";
                            validateVideo = false;
                        }

                        if (!FileExtension.ToLower().EndsWith("mp4"))
                        {
                            ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .mp4";
                            validateVideo = false;
                        }

                        //Its Create complete path to store in server.
                        //To copy and save file into server.
                        if (validateVideo)
                        {
                            _Video.VideoFilePath.SaveAs(_Video.VideoFile);
                        }
                    }
                    else
                    {
                        validateVideo = false;
                    }



                    string result = string.Empty;
                    Impcms_Video impcms_Video = new Impcms_Video();

                    if (_Video.videoId == 0)
                    {

                        AddBreadcrumb("Thêm mới video", "/News/ListVideoAdd");
                        if (validateImage)
                        {
                            _Video.DatePublic = DateTime.ParseExact(_Video.datepubView, "dd/MM/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                            _Video.VideoImageThumb = Common.Common.GetFormatImage(_Video.VideoImageThumb.Replace(Common.Common.getImagePath(), ""));

                         //   _Video.VideoFileWeb = Common.Common.GetFormatImage(_Video.VideoImageThumb.Replace(Common.Common.getImagePath(), ""));

                            if (validateVideo)
                            {
                                _Video.VideoFile = Common.Common.GetFormatImage(_Video.VideoFile.Replace(Common.Common.getImagePath(), ""));
                            }
                            else
                            {
                                _Video.VideoFile = string.Empty;
                            }

                            _Video.VideoCode = Common.Common.FormatSave(_Video.VideoCode);

                            result = impcms_Video.Insertcms_Video(_Video);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới video thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                Response.Redirect("/News/ListVideo", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới video không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
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
                        AddBreadcrumb("Cập nhật video", "/News/ListVideoAdd");
                        _Video.DatePublic = DateTime.ParseExact(_Video.datepubView, "dd/MM/yyyy HH:mm:ss",
                                      System.Globalization.CultureInfo.InvariantCulture);

                        if (!string.IsNullOrEmpty(_Video.VideoImageThumb))
                        {
                            _Video.VideoImageThumb = Common.Common.GetFormatImage(_Video.VideoImageThumb.Replace(Common.Common.getImagePath(), ""));
                        }

                        if (!string.IsNullOrEmpty(_Video.VideoFileWeb))
                        {
                            _Video.VideoFileWeb = Common.Common.GetFormatImage(_Video.VideoFileWeb.Replace(Common.Common.getImagePath(), ""));
                        }


                        if (!string.IsNullOrEmpty(_Video.VideoFile))
                        {
                            _Video.VideoFile = Common.Common.GetFormatImage(_Video.VideoFile.Replace(Common.Common.getImagePath(), ""));
                        }


                        if (!string.IsNullOrEmpty(_Video.VideoCode))
                        {
                            _Video.VideoCode = Common.Common.FormatSave(_Video.VideoCode);
                        }
                        if (_Video.ImageFile != null)
                        {
                            if (validateImage)
                            {
                                result = impcms_Video.Updatecms_Video(_Video);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật thông tin video thành công";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                    AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                    Response.Redirect("/News/ListVideo", false);
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật thông tin video không thành công";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    AddLogAction(result, Constant.ActionUpdateNOK.ToString());
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
                            result = impcms_Video.Updatecms_Video(_Video);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Cập nhật thông tin video thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                Response.Redirect("/News/ListVideo", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Cập nhật thông tin video không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListVideoAdd: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }


            return View(_Video);
        }
        #endregion


        #region --> Danh sách chuyên mục Ảnh
        public ActionResult ListCateImage()
        {
            // Initialization.  
            AddPageHeader("Chuyên mục ảnh", "");
            AddBreadcrumb("Danh sách chuyên mục Ảnh", "/News/ListCateImage");

             
            SearchNewsViewModel searchNewsView = new SearchNewsViewModel();
            try
            {

                List<cms_ImagesCate> _ImagesCates = impcms_ImagesCate.GetAllcms_ImagesCate();
                ViewBag.ImagesCates = _ImagesCates;
            }
            catch (Exception ex)
            {
                logError.Info("ListCateImage: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }



        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListCateImageDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách chuyên mục Ảnh", "/News/ListCateImageDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_ImagesCate.Deletecms_ImagesCate(Convert.ToInt32(customerID));
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
                logError.Info("ListCateImageDelete: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListCateImagePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách chuyên mục Ảnh", "/News/ListCateImagePublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_ImagesCate.Publiccms_ImagesCate(Convert.ToInt32(customerID));
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


        public ActionResult ListCateImageAdd(string vId)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục ảnh", "");
            AddBreadcrumb("Danh sách chuyên mục Ảnh", "/News/ListCateImage");

            cms_ImagesCate _ImagesCate = new cms_ImagesCate();
            List<cms_ImagesCate> _ImagesCates = null;

            try
            {
                if (string.IsNullOrEmpty(vId))
                {
                    AddBreadcrumb("Thêm mới chuyên mục ảnh", "/News/ListCateImageAdd");
                    ViewBag.Title = "Thêm mới chuyên mục ảnh";
                    _ImagesCate.vId = 0;
                }
                else
                {
                    AddBreadcrumb("Cập nhật chuyên mục ảnh", "/News/ListCateImageAdd");
                    ViewBag.Title = "Cập nhật chuyên mục ảnh";
                    _ImagesCates = impcms_ImagesCate.Getcms_ImagesCateByID(Convert.ToInt32(vId));
                    if (_ImagesCates != null)
                    {
                        return View(_ImagesCates[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListCateImageAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_ImagesCate);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListCateImageAdd(cms_ImagesCate _ImagesCate)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục ảnh", "");
            AddBreadcrumb("Danh sách chuyên mục Ảnh", "/News/ListCateImage");

            try
            {
                if (ModelState.IsValid)
                {

                    string result = string.Empty;
                    if (_ImagesCate.vId == 0)
                    {
                        AddBreadcrumb("Thêm mới chuyên mục ảnh", "/News/ListCateImageAdd");
                        result = impcms_ImagesCate.Insertcms_ImagesCate(_ImagesCate);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới chuyên mục thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                            Response.Redirect("/News/ListCateImage", false);
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
                        AddBreadcrumb("Cập nhật chuyên mục ảnh", "/News/ListCateImageAdd");
                        result = impcms_ImagesCate.Updatecms_ImagesCate(_ImagesCate);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin chuyên mục thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/News/ListCateImage", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin chuyên mục không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListCateImageAdd: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }


            return View(_ImagesCate);
        }

        #endregion


        #region --> Danh sách  Ảnh
        public ActionResult ListImage()
        {
            // Initialization.  
            AddPageHeader("Chuyên mục ảnh", "");
            AddBreadcrumb("Danh sách  ảnh", "/News/ListImage");

            
            CMS_Core.Models.SearchNewsViewModel searchNewsView = new CMS_Core.Models.SearchNewsViewModel();
            try
            {
                searchNewsView.tungay = DateTime.Now.AddDays(-14).ToString("dd/MM/yyyy");
                searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
                searchNewsView.keyword = string.Empty;
                searchNewsView.ParrenId = 0;

                List<cms_Images> cms_Images = impcms_Images.GetAllcms_ImagesByImageCate(searchNewsView);
                ViewBag.cms_Images = cms_Images;
            }
            catch (Exception ex)
            {
                logError.Info("ListImage: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListImage(CMS_Core.Models.SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  
            AddPageHeader("Chuyên mục ảnh", "");
            AddBreadcrumb("Danh sách  ảnh", "/News/ListImage");
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
            }
            #endregion


            try
            {
                if (checkDate)
                {
                    switch (submit)
                    {
                        case "SearchListImage":

                            List<cms_Images> cms_Images = impcms_Images.GetAllcms_ImagesByImageCate(searchNewsView);
                            ViewBag.cms_Images = cms_Images;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListImage:" + ex.ToString());
            }

            // Info.  
            return View(searchNewsView);


        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListImageDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách  ảnh", "/News/ListImageDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Images.Deletecms_Images(Convert.ToInt32(customerID));
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
        public ActionResult ListImagePublic(string[] customerIDs)
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
                        impcms_Images.Publiccms_Images(Convert.ToInt32(customerID));
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


        public ActionResult ListImageAdd(string videoId)
        {
            // Initialization.   
            AddPageHeader("Chuyên mục ảnh", "");
            AddBreadcrumb("Danh sách  ảnh", "/News/ListImage");
           


            cms_Images _Image = new cms_Images();
            List<cms_Images> _Images = null;

            try
            {
                if (string.IsNullOrEmpty(videoId))
                {
                    AddBreadcrumb("Thêm mới ảnh", "/News/ListImageAdd");
                    ViewBag.Title = "Thêm mới ảnh";
                    _Image.videoId = 0;
                    _Image.datepubView = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                }
                else
                {
                    AddBreadcrumb("Cập nhật ảnh", "/News/ListImageAdd");
                    ViewBag.Title = "Cập nhật ảnh";
                    _Images = impcms_Images.Getcms_ImagesByID(Convert.ToInt32(videoId));
                    if (_Images != null)
                    {
                        // _Images[0]._Images = new List<cms_Images>();
                        List<cms_Images> cms_ImagesCate = impcms_Images.GetAllcms_ImagesByVID(_Images[0].vId.ToString(), _Images[0].videoId.ToString());
                        _Images[0]._Images = cms_ImagesCate;
                        // ViewBag.cms_ImagesCate = cms_ImagesCate;
                        return View(_Images[0]);

                    }




                }
            }
            catch (Exception ex)
            {
                logError.Info("ListImageAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_Image);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListImageAdd(cms_Images _Image, List<HttpPostedFileBase> fileUpload)
        {

            // Initialization.             
            AddPageHeader("Chuyên mục ảnh", "");
            AddBreadcrumb("Danh sách  ảnh", "/News/ListImage");


            bool validateImage = true;
            List<string> imageAlbum = new List<string>();

            if (ModelState.IsValid)
            {
                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\imageslead\\";

                // upload multi image
                foreach (HttpPostedFileBase item in fileUpload)
                {
                    if (item != null && Array.Exists(_Image.FilesToBeUploaded.Split(','), s => s.Equals(item.FileName)))
                    {
                        string FileName = Path.GetFileNameWithoutExtension(item.FileName);

                        //To Get File Extension
                        string FileExtension = Path.GetExtension(item.FileName);

                        //Add Current Date To Attached File Name                      
                        string imagePath = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                        if (!System.IO.Directory.Exists(imagePath))
                            Directory.CreateDirectory(imagePath);

                        imagePath = imagePath + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        if (!item.FileName.ToLower().EndsWith("gif") && !item.FileName.ToLower().EndsWith("jpg") && !item.FileName.ToLower().EndsWith("jpeg") && !item.FileName.ToLower().EndsWith("jpg") && !item.FileName.ToLower().EndsWith("png"))
                        {
                            ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            validateImage = false;
                        }

                        if (!FileExtension.ToLower().EndsWith("gif") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("jpeg") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("png"))
                        {
                            ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            validateImage = false;
                        }

                        //Its Create complete path to store in server.
                        //To copy and save file into server.
                        if (validateImage)
                        {
                            item.SaveAs(imagePath);
                            imageAlbum.Add(imagePath);
                        }

                    }
                }




                //Upload Image
                if (_Image.ImageFile != null)
                {
                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(_Image.ImageFile.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(_Image.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    _Image.VideoImageThumb = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(_Image.VideoImageThumb))
                        Directory.CreateDirectory(_Image.VideoImageThumb);

                    _Image.VideoImageThumb = _Image.VideoImageThumb + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(_Image.VideoImageThumb))
                    {
                        System.IO.File.Delete(_Image.VideoImageThumb);
                    }

                    if (!_Image.ImageFile.FileName.ToLower().EndsWith("gif") && !_Image.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Image.ImageFile.FileName.ToLower().EndsWith("jpeg") && !_Image.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Image.ImageFile.FileName.ToLower().EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        validateImage = false;
                    }

                    if (!FileExtension.ToLower().EndsWith("gif") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("jpeg") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        validateImage = false;
                    }

                    //Its Create complete path to store in server.
                    //To copy and save file into server.
                    if (validateImage)
                    {
                        _Image.ImageFile.SaveAs(_Image.VideoImageThumb);
                    }
                }
                else
                {
                    validateImage = false;
                }

                string result = string.Empty;

                if (_Image.videoId == 0)
                {
                    AddBreadcrumb("Thêm mới ảnh", "/News/ListImageAdd");
                    if (validateImage)
                    {
                        _Image.DatePublic = DateTime.ParseExact(_Image.datepubView, "dd/MM/yyyy HH:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);
                        _Image.VideoImageThumb = Common.Common.GetFormatImage(_Image.VideoImageThumb.Replace(Common.Common.getImagePath(), ""));


                        if (string.IsNullOrEmpty(_Image.VideoCode))
                        {
                            _Image.VideoCode = string.Empty;
                        }

                        _Image.VideoCode = Common.Common.FormatSave(_Image.VideoCode);
                        _Image.Delegate = true;
                        _Image.videoParrentID = 0;
                        result = impcms_Images.Insertcms_Images(_Image);
                        if (!string.IsNullOrEmpty(result))
                        {
                            if (imageAlbum.Count > 0)
                            {
                                foreach (var imageAlbumValue in imageAlbum)
                                {
                                    _Image.Delegate = false;
                                    _Image.VideoImageThumb = Common.Common.GetFormatImage(imageAlbumValue.Replace(Common.Common.getImagePath(), ""));
                                    _Image.videoParrentID = Convert.ToInt32(result);
                                    string result1 = impcms_Images.Insertcms_Images(_Image);
                                }
                            }

                            ViewBag.TitleSuccsess = "Thêm mới ảnh thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                            Response.Redirect("/News/ListImage", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới ảnh không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
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
                    AddBreadcrumb("Cập nhật ảnh", "/News/ListImageAdd");

                    _Image.DatePublic = DateTime.ParseExact(_Image.datepubView, "dd/MM/yyyy HH:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);
                    _Image.VideoImageThumb = Common.Common.GetFormatImage(_Image.VideoImageThumb.Replace(Common.Common.getImagePath(), ""));


                    if (string.IsNullOrEmpty(_Image.VideoCode))
                    {
                        _Image.VideoCode = string.Empty;
                    }

                    _Image.VideoCode = Common.Common.FormatSave(_Image.VideoCode);

                    result = impcms_Images.Updatecms_Images(_Image);
                    if (!string.IsNullOrEmpty(result))
                    {
                        if (imageAlbum.Count > 0)
                        {
                            foreach (var imageAlbumValue in imageAlbum)
                            {
                                _Image.Delegate = false;
                                _Image.VideoImageThumb = Common.Common.GetFormatImage(imageAlbumValue.Replace(Common.Common.getImagePath(), ""));
                                _Image.videoParrentID = _Image.videoId;
                                result = impcms_Images.Insertcms_Images(_Image);
                            }
                        }

                        if (_Image._Images != null)
                        {
                            foreach (var imageValue in _Image._Images)
                            {
                                string value = impcms_Images.Updatecms_ImagesChild(imageValue);
                            }
                        }

                        ViewBag.TitleSuccsess = "Cập nhật thông tin ảnh thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                        Response.Redirect("/News/ListImage", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin ảnh không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }
            }



            return View(_Image);
        }
        #endregion


        #region --> Danh sách chuyên khoa
        public ActionResult ListScientistCate()
        {
            // Initialization.  
            AddPageHeader("Chuyên mục chuyên khoa", "");
            AddBreadcrumb("Danh sách chuyên khoa", "/News/ListScientistCate");

            
            SearchNewsViewModel searchNewsView = new SearchNewsViewModel();
            try
            {

                List<cms_Scientist_Cate> _cms_Scientist_Cates = impcms_Scientist_Cate.GetAllcms_Scientist_Cate();
                ViewBag.cms_Scientist_Cates = _cms_Scientist_Cates;
            }
            catch (Exception ex)
            {
                logError.Info("ListScientistCate: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }



        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListScientistCateDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách chuyên khoa", "/News/ListScientistCateDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Scientist_Cate.Deletecms_Scientist_Cate(Convert.ToInt32(customerID));
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
                logError.Info("ListScientistCateDelete: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListScientistCatePublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách chuyên khoa", "/News/ListScientistCatePublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Scientist_Cate.Publiccms_Scientist_Cate(Convert.ToInt32(customerID));
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
                logError.Info("ListScientistCatePublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListScientistCateAdd(string ID)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục chuyên khoa", "");
            AddBreadcrumb("Danh sách chuyên khoa", "/News/ListScientistCate");

            cms_Scientist_Cate _cms_Scientist_Cate = new cms_Scientist_Cate();
            List<cms_Scientist_Cate> _cms_Scientist_Cates = null;

            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    AddBreadcrumb("Thêm mới chuyên mục chuyên khoa", "/News/ListScientistCateAdd");
                    ViewBag.Title = "Thêm mới chuyên mục chuyên khoa";
                    _cms_Scientist_Cate.ID = Convert.ToInt32(ID);
                }
                else
                {
                    AddBreadcrumb("Cập nhật chuyên mục chuyên khoa", "/News/ListScientistCateAdd");
                    ViewBag.Title = "Cập nhật chuyên mục chuyên khoa";
                    _cms_Scientist_Cates = impcms_Scientist_Cate.Getcms_Scientist_CateByID(Convert.ToInt32(ID));
                    if (_cms_Scientist_Cates != null)
                    {
                        return View(_cms_Scientist_Cates[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListCateImageAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_cms_Scientist_Cate);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListScientistCateAdd(cms_Scientist_Cate _cms_Scientist_Cate)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục chuyên khoa", "");
            AddBreadcrumb("Danh sách chuyên khoa", "/News/ListScientistCate");
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            try
            {
                if (ModelState.IsValid)
                {

                    string result = string.Empty;
                    _cms_Scientist_Cate.userId = UserInfo.uid;
                    if (_cms_Scientist_Cate.ID == 0)
                    {
                        AddBreadcrumb("Thêm mới chuyên mục chuyên khoa", "/News/ListScientistCateAdd");
                        result = impcms_Scientist_Cate.Insertcms_Scientist_Cate(_cms_Scientist_Cate);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới chuyên mục chuyên khoa thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                            Response.Redirect("/News/ListScientistCate", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới chuyên mục chuyên khoa không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        AddBreadcrumb("Cập nhật chuyên mục ảnh", "/News/ListScientistCateAdd");
                        result = impcms_Scientist_Cate.Updatecms_Scientist_Cate(_cms_Scientist_Cate);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin chuyên mục chuyên khoa thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/News/ListScientistCate", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin chuyên mục chuyên khoa không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListScientistCateAdd: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }


            return View(_cms_Scientist_Cate);
        }

        #endregion


        #region --> Bài viết chuyên khoa
        public ActionResult ListScientist()
        {
            // Initialization.  
            AddPageHeader("Chuyên mục chuyên khoa", "");
            AddBreadcrumb("Danh sách tài liệu chuyên khoa", "/News/ListScientist");

            var UserInfo = ((cms_Account)Session["UserInfo"]);
            SearchNewsViewModel searchNewsView = new SearchNewsViewModel();

            searchNewsView.tungay = DateTime.Now.AddDays(-14).ToString("dd/MM/yyyy");
            searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
            searchNewsView.keyword = string.Empty;
            searchNewsView.ParrenId = 0;
            searchNewsView.userId = UserInfo.uid;
            searchNewsView.Status = 2;
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();

            try
            {
                List<cms_Scientist> _cms_Scientists = impcms_Scientist.Getcms_Scientist(DateTime.Now.AddDays(-14).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"), UserInfo.uid, searchNewsView.Status, string.Empty);
                ViewBag.cms_Scientists = _cms_Scientists;
            }
            catch (Exception ex)
            {
                logError.Info("ListScientistCate: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListScientist(CMSLIS.Models.SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  
            AddPageHeader("Chuyên mục chuyên khoa", "");
            AddBreadcrumb("Danh sách tài liệu chuyên khoa", "/News/ListScientist");
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            bool checkDate = true;
            var UserInfo = ((cms_Account)Session["UserInfo"]);
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
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
            }
            #endregion


            try
            {
                if (checkDate)
                {
                    switch (submit)
                    {
                        case "SearchListScientist":

                            if(string.IsNullOrEmpty(searchNewsView.keyword))
                            {
                                searchNewsView.keyword = string.Empty;
                            }

                            List<cms_Scientist> _cms_Scientists = impcms_Scientist.Getcms_Scientist(Tungay.ToString("yyyyMMdd"), Denngay.ToString("yyyyMMdd"), UserInfo.uid, searchNewsView.Status, string.Empty);
                            ViewBag.cms_Scientists = _cms_Scientists;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                logError.Info("ListScientist:" + ex.ToString());
            }

            // Info.  
            return View(searchNewsView);


        }




        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListScientistDelete(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Chuyên mục chuyên khoa", "");
            AddBreadcrumb("Danh sách tài liệu chuyên khoa", "/News/ListScientistDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Scientist.Deletecms_Scientist(Convert.ToInt32(customerID));
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
                logError.Info("ListScientistCateDelete: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListScientistPublic(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Chuyên mục chuyên khoa", "");
            AddBreadcrumb("Danh sách tài liệu chuyên khoa", "/News/ListScientistPublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Scientist.Publiccms_Scientist(Convert.ToInt32(customerID));
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
                logError.Info("ListScientistCatePublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListScientistAdd(string ID)
        {
            // Initialization.     
            AddPageHeader("Chuyên mục chuyên khoa", "");
            AddBreadcrumb("Danh sách tài liệu chuyên khoa", "/News/ListScientist");
             

            cms_Scientist _cms_Scientist = new cms_Scientist();
            List<cms_Scientist> _cms_Scientists = null;
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrentNews();

            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    ID = "0";
                    AddBreadcrumb("Thêm mới tài liệu chuyên khoa", "/News/ListScientistAdd");
                    ViewBag.Title = "Thêm mới tài liệu chuyên khoa";
                    _cms_Scientist.ID = Convert.ToInt32(ID);
                }
                else
                {
                    AddBreadcrumb("Cập nhật tài liệu chuyên khoa", "/News/ListScientistAdd");
                    ViewBag.Title = "Cập nhật tài liệu chuyên khoa";
                    _cms_Scientists = impcms_Scientist.Getcms_ScientistByID(Convert.ToInt32(ID));
                    if (_cms_Scientists != null)
                    {
                        return View(_cms_Scientists[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListScientistAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_cms_Scientist);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ListScientistAdd(cms_Scientist _cms_Scientist)
        {
            // Initialization.  
            AddPageHeader("Chuyên mục chuyên khoa", "");
            AddBreadcrumb("Danh sách tài liệu chuyên khoa", "/News/ListScientist");
            this.ViewBag.Getcms_CategoryParrent = Common.Common.Getcms_CategoryParrentNews();


          


            bool validateImage = true;
            string ImageFBPath = string.Empty;
  
            if (ModelState.IsValid)
            {
 

                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\imageslead\\";

                //Upload Image
                if (_cms_Scientist.ImageFile != null)
                {
                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(_cms_Scientist.ImageFile.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(_cms_Scientist.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    _cms_Scientist.newsImages = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(_cms_Scientist.newsImages))
                        Directory.CreateDirectory(_cms_Scientist.newsImages);

                    _cms_Scientist.newsImages = _cms_Scientist.newsImages + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(_cms_Scientist.newsImages))
                    {
                        System.IO.File.Delete(_cms_Scientist.newsImages);
                    }

                    if (!_cms_Scientist.ImageFile.FileName.EndsWith("gif") && !_cms_Scientist.ImageFile.FileName.EndsWith("jpg") && !_cms_Scientist.ImageFile.FileName.EndsWith("jpeg") && !_cms_Scientist.ImageFile.FileName.EndsWith("jpg") && !_cms_Scientist.ImageFile.FileName.EndsWith("png"))
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
                        _cms_Scientist.ImageFile.SaveAs(_cms_Scientist.newsImages);
                       
                    }
                }
                else
                {
                    validateImage = false;
                }
                 

                string result = string.Empty;
                if (_cms_Scientist.ID == 0)
                {
                    AddBreadcrumb("Thêm mới bài viết", "/News/ListScientistAdd");
                    //if (_NewsRelate.Count > 0)
                    //{
                    if (validateImage)
                    {
                        _cms_Scientist.userId = ((cms_Account)Session["UserInfo"]).uid;

                        _cms_Scientist.dateCreate = DateTime.Now;
                        _cms_Scientist.datepub = DateTime.ParseExact(_cms_Scientist.datepubView, "dd/MM/yyyy HH:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);
                        _cms_Scientist.newsImages = Common.Common.GetPathImage(_cms_Scientist.newsImages.Replace(Common.Common.getImagePath(), ""));

                        _cms_Scientist.newsContent = Common.Common.FormatSave(_cms_Scientist.newsContent);
                        
                        //string ContentAutoLink = Common.Common.AddAutoLink(_News.newsContent, _News.LinksLimitPerPage, _News.OptionReplate);
                        //_News.newsContentAutoLink = ContentAutoLink;

                        result = impcms_Scientist.Insertcms_Scientist(_cms_Scientist);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới bài viết thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());                                                     
                            Response.Redirect("/News/ListScientist", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới bài viết không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
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
                    AddBreadcrumb("Cập nhật bài viết", "/News/ListScientistAdd");
                    _cms_Scientist.userId = ((cms_Account)Session["UserInfo"]).uid;

                    _cms_Scientist.dateCreate = DateTime.Now;
                    _cms_Scientist.datepub = DateTime.ParseExact(_cms_Scientist.datepubView, "dd/MM/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                    //DateTime.Now;
                    _cms_Scientist.newsImages = Common.Common.GetPathImage(_cms_Scientist.newsImages.Replace(Common.Common.getImagePath(), ""));

                    _cms_Scientist.newsContent = Common.Common.FormatSave(_cms_Scientist.newsContent);
                    
                    //string ContentAutoLink = Common.Common.AddAutoLink(_News.newsContent, _News.LinksLimitPerPage, _News.OptionReplate);
                    //_News.newsContentAutoLink = ContentAutoLink;

                    result = impcms_Scientist.Updatecms_Scientist(_cms_Scientist);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin bài viết thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());  
                        Response.Redirect("/News/ListScientist", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin bài viết không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }
            }


            return View(_cms_Scientist);
        }



      

        #endregion


        #region --> Danh sách vị trí quảng cáo
        public ActionResult ListBannerPlans()
        {
            // Initialization.  
            AddPageHeader("Chuyên mục quảng cáo", "");                        
            AddBreadcrumb("Danh sách vị trí quảng cáo", "/News/ListBannerPlans");
            SearchNewsViewModel searchNewsView = new SearchNewsViewModel();
            try
            {

                List<cms_Banner_Plans> _Banner_Plans = impcms_Banner_Plans.GetAllcms_Banner_Plans();
                ViewBag.Banner_Plans = _Banner_Plans;
            }
            catch (Exception ex)
            {
                logError.Info("ListBannerPlans: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }



        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListBannerPlansDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách vị trí quảng cáo", "/News/ListBannerPlans");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Banner_Plans.Deletecms_Banner_Plans(Convert.ToInt32(customerID));
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
                logError.Info("ListBannerPlansDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListBannerPlansPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách vị trí quảng cáo", "/News/ListBannerPlans");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Banner_Plans.Publiccms_Banner_Plans(Convert.ToInt32(customerID));
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
                logError.Info("ListBannerPlansPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListBannerPlansAdd(string planId)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục quảng cáo", "");
            AddBreadcrumb("Danh sách vị trí quảng cáo", "/News/ListBannerPlans");


            cms_Banner_Plans _Banner_Plan = new cms_Banner_Plans();
            List<cms_Banner_Plans> _Banner_Plans = null;

            try
            {
                if (string.IsNullOrEmpty(planId))
                {
                    AddBreadcrumb("Thêm mới vị trí quảng cáo", "/News/ListBannerPlansAdd");
                    ViewBag.Title = "Thêm mới vị trí quảng cáo";
                    _Banner_Plan.planId = 0;
                }
                else
                {
                    AddBreadcrumb("Cập nhật vị trí quảng cáo", "/News/ListBannerPlansAdd");
                    ViewBag.Title = "Cập nhật vị trí quảng cáo";
                    _Banner_Plans = impcms_Banner_Plans.Getcms_Banner_PlansByID(Convert.ToInt32(planId));
                    if (_Banner_Plans != null)
                    {
                        return View(_Banner_Plans[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListBannerPlansAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_Banner_Plan);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListBannerPlansAdd(cms_Banner_Plans _Banner_Plan)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục quảng cáo", "");
            AddBreadcrumb("Danh sách vị trí quảng cáo", "/News/ListBannerPlans");


            if (ModelState.IsValid)
            {

                string result = string.Empty;
                if (_Banner_Plan.planId == 0)
                {
                    AddBreadcrumb("Thêm mới vị trí quảng cáo", "/News/ListBannerPlansAdd");
                    result = impcms_Banner_Plans.Insertcms_Banner_Plans(_Banner_Plan);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Thêm mới vị trí quảng cáo thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionInsertOK.ToString());
                        Response.Redirect("/News/ListBannerPlans", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thêm mới vị trí quảng cáo không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                    }
                }
                else
                {
                    AddBreadcrumb("Cập nhật vị trí quảng cáo", "/News/ListBannerPlansAdd");
                    result = impcms_Banner_Plans.Updatecms_Banner_Plans(_Banner_Plan);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin vị trí quảng cáo thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                        Response.Redirect("/News/ListBannerPlans", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật thông tin vị trí quảng cáo không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }
            }

            return View(_Banner_Plan);
        }

        #endregion


        #region --> Danh sách quảng cáo
        public ActionResult ListBanner()
        {
            // Initialization.  
            AddPageHeader("Chuyên mục quảng cáo", "");            
            AddBreadcrumb("Danh sách  quảng cáo", "/News/ListBanner");
            CMS_Core.Models.SearchNewsViewModel searchNewsView = new CMS_Core.Models.SearchNewsViewModel();
            try
            {
                searchNewsView.tungay = DateTime.Now.AddDays(-14).ToString("dd/MM/yyyy");
                searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
                searchNewsView.keyword = string.Empty;
                searchNewsView.ParrenId = 0;

                List<cms_Banner_rows> _Banner_Rows = impcms_Banner_rows.Getcms_Banner_rowsByBannerCate(searchNewsView);
                ViewBag.Banner_Rows = _Banner_Rows;
            }
            catch (Exception ex)
            {
                logError.Info("ListImage: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListBanner(CMS_Core.Models.SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  
            AddPageHeader("Chuyên mục quảng cáo", "");
            AddBreadcrumb("Danh sách  quảng cáo", "/News/ListBanner");
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            bool checkdate = true;
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
                    checkdate = false;
                }
            }
            catch
            {
                ViewBag.TitleSuccsess = "Dữ liệu ngày tháng không đúng định dạng";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                checkdate = false;
            }
            #endregion


            try
            {
                if (checkdate)
                {
                    switch (submit)
                    {
                        case "SearchListBanner":

                            List<cms_Banner_rows> _Banner_Rows = impcms_Banner_rows.Getcms_Banner_rowsByBannerCate(searchNewsView);
                            ViewBag.Banner_Rows = _Banner_Rows;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListBanner:" + ex.ToString());
            }

            // Info.  
            return View(searchNewsView);


        }



        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListBannerDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách  quảng cáo", "/News/ListBannerDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Banner_rows.Deletecms_Banner_rows(Convert.ToInt32(customerID));
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
                logError.Info("ListBannerPlansDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListBannerPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách  quảng cáo", "/News/ListBannerPublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Banner_rows.Publiccms_Banner_rows(Convert.ToInt32(customerID));
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
                logError.Info("ListBannerPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListBannerAdd(string bannerId)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục quảng cáo", "");
            AddBreadcrumb("Danh sách  quảng cáo", "/News/ListBanner");


            cms_Banner_rows _Banner_Row = new cms_Banner_rows();
            List<cms_Banner_rows> _Banner_Rows = null;

            try
            {
                if (string.IsNullOrEmpty(bannerId))
                {
                    AddBreadcrumb("Thêm mới quảng cáo", "/News/ListBannerAdd");
                    ViewBag.Title = "Thêm mới quảng cáo";
                    _Banner_Row.bannerId = 0;
                    _Banner_Row.addTime = DateTime.Now;
                    _Banner_Row.expTime = DateTime.Now.AddDays(1);
                }
                else
                {
                    AddBreadcrumb("Cập nhật quảng cáo", "/News/ListBannerAdd");
                    ViewBag.Title = "Cập nhật quảng cáo";
                    _Banner_Rows = impcms_Banner_Rows.Getcms_Banner_rowsByID(Convert.ToInt32(bannerId));
                    if (_Banner_Rows != null)
                    {
                        return View(_Banner_Rows[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListBannerAdd:" + ex.ToString());
            }

            // Info.  
            return View(_Banner_Row);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListBannerAdd(cms_Banner_rows _Banner_Row)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục quảng cáo", "");
            AddBreadcrumb("Danh sách  quảng cáo", "/News/ListBanner");

            bool validateImage = true;
            string FileExtension = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {

                    //Get Upload path from Web.Config file AppSettings.
                    string UploadPath = Common.Common.getImagePath() + "ImagePath\\imagesBanner\\";

                    //Upload Image
                    if (_Banner_Row.ImageFile != null)
                    {
                        // Use Namespace called: System.IO
                        string FileName = Path.GetFileNameWithoutExtension(_Banner_Row.ImageFile.FileName);

                        //To Get File Extension
                        FileExtension = Path.GetExtension(_Banner_Row.ImageFile.FileName);

                        //Add Current Date To Attached File Name                      
                        _Banner_Row.fileName = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                        if (!System.IO.Directory.Exists(_Banner_Row.fileName))
                            Directory.CreateDirectory(_Banner_Row.fileName);

                        _Banner_Row.fileName = _Banner_Row.fileName + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                        if (System.IO.File.Exists(_Banner_Row.fileName))
                        {
                            System.IO.File.Delete(_Banner_Row.fileName);
                        }

                        if (!_Banner_Row.ImageFile.FileName.ToLower().EndsWith("gif") && !_Banner_Row.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Banner_Row.ImageFile.FileName.ToLower().EndsWith("jpeg") && !_Banner_Row.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Banner_Row.ImageFile.FileName.ToLower().EndsWith("png") && !_Banner_Row.ImageFile.FileName.ToLower().EndsWith("mp4"))
                        {
                            ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png .mp4";
                            validateImage = false;
                        }

                        if (!FileExtension.ToLower().EndsWith("gif") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("jpeg") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("png") && !FileExtension.ToLower().EndsWith("mp4"))
                        {
                            ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png .mp4";
                            validateImage = false;
                        }

                        //Its Create complete path to store in server.
                        //To copy and save file into server.
                        if (validateImage)
                        {
                            _Banner_Row.ImageFile.SaveAs(_Banner_Row.fileName);

                            _Banner_Row.fileNameWeb = Common.Common.reSizeImageBanner(_Banner_Row.fileName);
                           
                          

                        }
                    }
                    else
                    {
                        validateImage = false;
                    }

                    string result = string.Empty;

                    if (_Banner_Row.bannerId == 0)
                    {

                        AddBreadcrumb("Thêm mới quảng cáo", "/News/ListBannerAdd");
                        if (validateImage)
                        {
                            _Banner_Row.expTime = DateTime.ParseExact(_Banner_Row.expTimeView, "dd/MM/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                            _Banner_Row.addTime = DateTime.ParseExact(_Banner_Row.addTimeView, "dd/MM/yyyy HH:mm:ss",
                                      System.Globalization.CultureInfo.InvariantCulture);
                            _Banner_Row.fileName = Common.Common.GetFormatImage(_Banner_Row.fileName.Replace(Common.Common.getImagePath(), ""));
                            _Banner_Row.fileNameWeb = Common.Common.GetFormatImage(_Banner_Row.fileNameWeb.Replace(Common.Common.getImagePath(), ""));
                            _Banner_Row.fileMine = FileExtension;

                            result = impcms_Banner_Rows.Insertcms_Banner_rows(_Banner_Row);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Thêm mới quảng cáo thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionInsertOK.ToString());
                                Response.Redirect("/News/ListBanner", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Thêm mới quảng cáo không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionInsertNOK.ToString());
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Mời bạn nhập file quảng cáo!";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }

                    }
                    else
                    {
                        AddBreadcrumb("Cập nhật quảng cáo", "/News/ListBannerAdd");


                        _Banner_Row.fileName = Common.Common.GetFormatImage(_Banner_Row.fileName.Replace(Common.Common.getImagePath(), ""));
                        _Banner_Row.fileNameWeb = Common.Common.GetFormatImage(_Banner_Row.fileNameWeb.Replace(Common.Common.getImagePath(), ""));

                        if (!string.IsNullOrEmpty(FileExtension))
                        {
                            _Banner_Row.fileMine = FileExtension;
                        }
                        _Banner_Row.expTime = DateTime.ParseExact(_Banner_Row.expTimeView, "dd/MM/yyyy HH:mm:ss",
                                     System.Globalization.CultureInfo.InvariantCulture);
                        _Banner_Row.addTime = DateTime.ParseExact(_Banner_Row.addTimeView, "dd/MM/yyyy HH:mm:ss",
                                  System.Globalization.CultureInfo.InvariantCulture);


                        if (_Banner_Row.ImageFile != null)
                        {
                            if (validateImage)
                            {
                                result = impcms_Banner_Rows.Updatecms_Banner_rows(_Banner_Row);
                                if (!string.IsNullOrEmpty(result))
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật thông tin quảng cáo thành công";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                    AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                    Response.Redirect("/News/ListBanner", false);
                                }
                                else
                                {
                                    ViewBag.TitleSuccsess = "Cập nhật thông tin quảng cáo không thành công";
                                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                    AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                                }
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Mời bạn nhập file quảng cáo!";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            }
                        }
                        else
                        {
                            result = impcms_Banner_Rows.Updatecms_Banner_rows(_Banner_Row);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Cập nhật thông tin quảng cáo thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                Response.Redirect("/News/ListBanner", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Cập nhật thông tin quảng cáo không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListContact: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }


            return View(_Banner_Row);
        }

        #endregion


        #region --> Danh sách liên hệ
        public ActionResult ListContact()
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách liên hệ", "/News/ListContact");
            CMS_Core.Models.SearchNewsViewModel searchNewsView = new CMS_Core.Models.SearchNewsViewModel();
            try
            {
                searchNewsView.tungay = DateTime.Now.AddDays(-14).ToString("dd/MM/yyyy");
                searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
                searchNewsView.keyword = string.Empty;
                searchNewsView.ParrenId = 0;


                List<cms_Contact> _Contacts = impcms_Contact.Getcms_ContactAll(searchNewsView);
                ViewBag.Contacts = _Contacts;
            }
            catch (Exception ex)
            {
                logError.Info("ListContact: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListContact(CMS_Core.Models.SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách liên hệ", "/News/ListContact");
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            bool check = true;
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
                    check = false;
                }
            }
            catch
            {
                ViewBag.TitleSuccsess = "Dữ liệu ngày tháng không đúng định dạng";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                check = false;
            }
            #endregion


            try
            {
                if (check)
                {
                    switch (submit)
                    {
                        case "SearchListContact":

                            List<cms_Contact> _Contacts = impcms_Contact.Getcms_ContactAll(searchNewsView);
                            ViewBag.Contacts = _Contacts;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListContact:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);


        }





        public ActionResult ListContactAdd(string contactId)
        {
            // Initialization.             
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("FeedBack lại khách hàng", "/News/ListContactAdd");


            cms_Contact _Contact = new cms_Contact();
            List<cms_Contact> _Contacts = null;

            try
            {
                if (string.IsNullOrEmpty(contactId))
                {
                    ViewBag.TitleSuccsess = "Tham số không hợp lệ! Mời bạn thực hiện lại";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
                else
                {
                    _Contacts = impcms_Contact.Getcms_ContactByID(Convert.ToInt32(contactId));
                    if (_Contacts != null)
                    {
                        if (_Contacts[0].FeedBack)
                        {
                            ViewBag.TitleSuccsess = "Liên hệ đã thực hiện FeedBack rồi! ";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            return View(_Contacts[0]);
                        }
                        else
                        {
                            return View(_Contacts[0]);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListContactAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_Contact);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ListContactAdd(cms_Contact _Contact)
        {

            if (ModelState.IsValid)
            {
                string result = string.Empty;
                if (_Contact.contactId != 0)
                {
                    var validateEmail = new EmailAddressAttribute();
                    if (!validateEmail.IsValid(_Contact.Email))
                    {
                        ViewBag.TitleSuccsess = "Thông tin Email không hợp lệ";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }
                    else
                    {
                        string title = "Xin chào: " + _Contact.FullName.ToUpperInvariant() + " .Bệnh viện Đa khoa MEDLATEC xin trả lời câu hỏi của ban";
                        string content = _Contact.ContentFeedBack;
                        bool resultSendEmail = Common.Common.sendGmail(title, _Contact.FullName, _Contact.Email, content);

                        if (resultSendEmail)
                        {
                            impcms_Contact.Updatecms_ContactFeedBack(_Contact);
                            ViewBag.TitleSuccsess = "Thông tin Email không hợp lệ";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Có lỗi trong quá trình gửi email. Xin thực hiện lại";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                }
                else
                {
                    ViewBag.TitleSuccsess = "Thông tin đầu vào không hợp lệ. Xin kiểm tra lại dữ liệu đầu vào.";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
            }
            List<cms_Contact> _Contacts = impcms_Contact.Getcms_ContactByID(Convert.ToInt32(_Contact.contactId));

            return View(_Contacts[0]);
        }

        #endregion


        #region --> Danh sách nhóm bác sỹ
        public ActionResult ListGroupDoctor()
        {
            // Initialization.  
            AddPageHeader("Chuyên mục bác sỹ", "");            
            AddBreadcrumb("Chuyên mục bác sỹ", "/News/ListGroupDoctor");
            AddBreadcrumb("Danh sách nhóm bác sỹ", "/News/ListGroupDoctor");
            CMS_Core.Models.SearchNewsViewModel searchNewsView = new CMS_Core.Models.SearchNewsViewModel();
            try
            {
                searchNewsView.tungay = DateTime.Now.AddDays(-14).ToString("dd/MM/yyyy");
                searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
                searchNewsView.keyword = string.Empty;
                searchNewsView.ParrenId = 0;

                List<cms_Group_Doctor> _Group_Doctors = impcms_Group_Doctor.GetAllcms_Group_Doctor();
                ViewBag.Group_Doctors = _Group_Doctors;
            }
            catch (Exception ex)
            {
                logError.Info("ListGroupDoctor: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }




        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListGroupDoctorDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách nhóm bác sỹ", "/News/ListGroupDoctorDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Group_Doctor.Deletecms_Group_Doctor(Convert.ToInt32(customerID));
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
                logError.Info("ListGroupDoctorDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListGroupDoctorPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách nhóm bác sỹ", "/News/ListGroupDoctorPublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {

                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Group_Doctor.Publiccms_Group_Doctor(Convert.ToInt32(customerID));
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
                logError.Info("ListGroupDoctorPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListGroupDoctorAdd(string id)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục bác sỹ", "");
            AddBreadcrumb("Chuyên mục bác sỹ", "/News/ListGroupDoctor");



            cms_Group_Doctor _Group_Doctor = new cms_Group_Doctor();
            List<cms_Group_Doctor> _Group_Doctors = null;

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    AddBreadcrumb("Thêm mới nhóm bác sỹ", "/News/ListGroupDoctorAdd");
                    ViewBag.Title = "Thêm mới nhóm bác sỹ";

                }
                else
                {
                    AddBreadcrumb("Cập nhật nhóm bác sỹ", "/News/ListGroupDoctorAdd");
                    ViewBag.Title = "Cập nhật nhóm bác sỹ";
                    _Group_Doctors = impcms_Group_Doctor.Getcms_Group_DoctorByID(Convert.ToInt32(id));
                    if (_Group_Doctors != null)
                    {
                        return View(_Group_Doctors[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListGroupDoctorAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_Group_Doctor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ListGroupDoctorAdd(cms_Group_Doctor _Group_Doctor)
        {
            AddBreadcrumb("Tin tức", "/News");

            bool validateImage = true;
            string FileExtension = string.Empty;
            if (ModelState.IsValid)
            {

                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\imagesGroupDoctor\\";

                //Upload Image
                if (_Group_Doctor.ImageFile != null)
                {
                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(_Group_Doctor.ImageFile.FileName);

                    //To Get File Extension
                    FileExtension = Path.GetExtension(_Group_Doctor.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    _Group_Doctor.Image = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(_Group_Doctor.Image))
                        Directory.CreateDirectory(_Group_Doctor.Image);

                    _Group_Doctor.Image = _Group_Doctor.Image + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(_Group_Doctor.Image))
                    {
                        System.IO.File.Delete(_Group_Doctor.Image);
                    }

                    if (!_Group_Doctor.ImageFile.FileName.ToLower().EndsWith("gif") && !_Group_Doctor.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Group_Doctor.ImageFile.FileName.ToLower().EndsWith("jpeg") && !_Group_Doctor.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Group_Doctor.ImageFile.FileName.ToLower().EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png ";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        validateImage = false;
                    }

                    if (!FileExtension.ToLower().EndsWith("gif") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("jpeg") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png  ";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        validateImage = false;
                    }

                    //Its Create complete path to store in server.
                    //To copy and save file into server.
                    if (validateImage)
                    {
                        _Group_Doctor.ImageFile.SaveAs(_Group_Doctor.Image);
                    }
                }
                else
                {
                    validateImage = false;
                }

                string result = string.Empty;

                if (_Group_Doctor.id == 0)
                {
                    AddBreadcrumb("Thêm mới nhóm bác sỹ", "/News/ListGroupDoctorAdd");

                    if (validateImage)
                    {
                        _Group_Doctor.Image = Common.Common.GetFormatImage(_Group_Doctor.Image.Replace(Common.Common.getImagePath(), ""));
                        result = impcms_Group_Doctor.Insertcms_Group_Doctor(_Group_Doctor);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới nhóm bác sỹ thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                            Response.Redirect("/News/ListGroupDoctor", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới nhóm bác sỹ không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Mời bạn nhập ảnh nhóm bác sỹ!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }

                }
                else
                {
                    AddBreadcrumb("Cập nhật nhóm bác sỹ", "/News/ListGroupDoctorAdd");

                    if (_Group_Doctor.ImageFile != null)
                    {
                        if (validateImage)
                        {
                            _Group_Doctor.Image = Common.Common.GetFormatImage(_Group_Doctor.Image.Replace(Common.Common.getImagePath(), ""));
                            result = impcms_Group_Doctor.Updatecms_Group_Doctor(_Group_Doctor);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Cập nhật thông tin nhóm bác sỹ thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                Response.Redirect("/News/ListGroupDoctor", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Cập nhật thông tin nhóm bác sỹ không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Mời bạn nhập ảnh nhóm bác sỹ!";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        _Group_Doctor.Image = Common.Common.GetFormatImage(_Group_Doctor.Image.Replace(Common.Common.getImagePath(), ""));
                        result = impcms_Group_Doctor.Updatecms_Group_Doctor(_Group_Doctor);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin nhóm bác sỹ thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/News/ListGroupDoctor", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin nhóm bác sỹ không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
            }


            return View(_Group_Doctor);
        }

        #endregion


        #region --> Danh sách bác sỹ
        public ActionResult ListDoctor()
        {
            // Initialization.  
            AddPageHeader("Chuyên mục bác sỹ", "");
            AddBreadcrumb("Chuyên mục bác sỹ", "/News/ListDoctor");
            AddBreadcrumb("Danh sách bác sỹ", "/News/ListDoctor");
            CMS_Core.Models.SearchNewsViewModel searchNewsView = new CMS_Core.Models.SearchNewsViewModel();
            try
            {
                searchNewsView.tungay = DateTime.Now.AddDays(-14).ToString("dd/MM/yyyy");
                searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
                searchNewsView.keyword = string.Empty;
                searchNewsView.ParrenId = 0;

                List<cms_Doctor> _Doctors = impcms_Doctor.GetAllcms_Doctor();
                ViewBag.Doctors = _Doctors;
            }
            catch (Exception ex)
            {
                logError.Info("ListDoctor: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListDoctor(CMS_Core.Models.SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  
            AddPageHeader("Chuyên mục bác sỹ", "");
            AddBreadcrumb("Chuyên mục bác sỹ", "/News/ListDoctor");
            AddBreadcrumb("Danh sách bác sỹ", "/News/ListDoctor");
            try
            {
                switch (submit)
                {
                    case "SearchListDoctor":

                        List<cms_Doctor> _Doctors = impcms_Doctor.Getcms_DoctorByCateID(Convert.ToInt32(searchNewsView.ParrenId));
                        ViewBag.Doctors = _Doctors;
                        break;
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListDoctor:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);


        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListDoctorDelete(string[] customerIDs)
        {
            // Initialization.  
            AddPageHeader("Chuyên mục bác sỹ", "");
            AddBreadcrumb("Chuyên mục bác sỹ", "/News/ListDoctor");
            AddBreadcrumb("Danh sách bác sỹ", "/News/ListDoctorDelete");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // xóa dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Doctor.Deletecms_Doctor(Convert.ToInt32(customerID));
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
                logError.Info("ListGroupDoctorDelete: " + ex.ToString());
            }

            return Json("Xóa thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListDoctorPublic(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách bác sỹ", "/News/ListDoctorPublic");
            string listID = string.Empty;
            try
            {
                // Kiểm tra xem có bản ghi nào được chọn không?
                if (customerIDs != null)
                {


                    // duyệt dữ liệu
                    foreach (string customerID in customerIDs)
                    {
                        impcms_Doctor.Publiccms_Doctor(Convert.ToInt32(customerID));
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
                logError.Info("ListDoctorPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }


        public ActionResult ListDoctorAdd(string id)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục bác sỹ", "");
            AddBreadcrumb("Chuyên mục bác sỹ", "/News/ListDoctor");



            cms_Doctor _Doctor = new cms_Doctor();
            List<cms_Doctor> _Doctors = null;

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    AddBreadcrumb("Thêm mới bác sỹ", "/News/ListDoctorAdd");
                }
                else
                {
                    AddBreadcrumb("Cập nhật bác sỹ", "/News/ListDoctorAdd");

                    _Doctors = impcms_Doctor.Getcms_DoctorByID(Convert.ToInt32(id));
                    if (_Doctors != null)
                    {
                        if (_Doctors.Count > 0)
                        {
                            return View(_Doctors[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListDoctorAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_Doctor);
        }


        public ActionResult ListDoctorAdd1(string id)
        {
            // Initialization.             
            AddPageHeader("Chuyên mục bác sỹ", "");
            AddBreadcrumb("Chuyên mục bác sỹ", "/News/ListDoctor");



            cms_Doctor _Doctor = new cms_Doctor();
            List<cms_Doctor> _Doctors = null;

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    AddBreadcrumb("Thêm mới bác sỹ", "/News/ListDoctorAdd");
                }
                else
                {
                    AddBreadcrumb("Cập nhật bác sỹ", "/News/ListDoctorAdd");

                    _Doctors = impcms_Doctor.Getcms_DoctorByID(Convert.ToInt32(id));
                    if (_Doctors != null)
                    {
                        if (_Doctors.Count > 0)
                        {
                            return View(_Doctors[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListDoctorAdd:" + ex.ToString());
            }

            // Info.  
            return View(_Doctor);
        }


        //[HttpPost]
        //[AjaxValidateAntiForgeryToken]
        public JsonResult AddGetGroupDoctor(string query)
        {

            try
            {
                List<Location> records = new List<Location>();



                List<cms_Group_Doctor> _Group_Doctors = impcms_Group_Doctor.GetAllcms_Group_DoctorActive();
                List<cms_Doctor_Cate> _Doctor_Cates = impcms_Doctor_Cate.Getcms_Doctor_CateByID(Convert.ToInt32(query));


                foreach (var data in _Group_Doctors)
                {
                    Location location = new Location();
                    location.id = data.id;
                    location.text = data.Name;
                    bool ExistsItem = false;
                    if (_Doctor_Cates != null)
                        ExistsItem = _Doctor_Cates.Any(x => x.CateID == data.id);

                    location.@checked = ExistsItem;
                    location.population = 0;
                    location.flagUrl = string.Empty;


                    //List<cms_Doctor_Cate> cms_Doctor_Cates  = impcms_Doctor_Cate.Getcms_Doctor_CateByID(0);

                    //location.children = new List<Location>();

                    //foreach (var Child in cms_Doctor_Cates)
                    //{
                    //    Location locationChild = new Location();
                    //    locationChild.id = Child.id;
                    //    locationChild.text = Child.DoctorName;
                    //    bool alreadyExists = false;
                    //    if (_News_Cates != null)
                    //        alreadyExists = _News_Cates.Any(x => x.CateId == Child.cateId);


                    //    locationChild.@checked = alreadyExists;
                    //    locationChild.population = 0;
                    //    locationChild.flagUrl = string.Empty;

                    //    location.children.Add(locationChild);
                    //}


                    records.Add(location);
                }



                return this.Json(records, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return this.Json("", JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ListDoctorAdd(cms_Doctor _Doctor)
        {
            AddPageHeader("Chuyên mục bác sỹ", "");
            AddBreadcrumb("Chuyên mục bác sỹ", "/News/ListDoctor");


            bool validateImage = true;
            string FileExtension = string.Empty;
            if (ModelState.IsValid)
            {

                if (!string.IsNullOrEmpty(_Doctor.cateIdList))
                {
                    _Doctor.cateIdList = _Doctor.DoctorCate.ToString() + "," + _Doctor.cateIdList;
                }
                else
                {
                    _Doctor.cateIdList = _Doctor.DoctorCate.ToString();
                }


                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\imagesDoctor\\";

                //Upload Image
                if (_Doctor.ImageFile != null)
                {
                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(_Doctor.ImageFile.FileName);

                    //To Get File Extension
                    FileExtension = Path.GetExtension(_Doctor.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    _Doctor.DoctorImage = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(_Doctor.DoctorImage))
                        Directory.CreateDirectory(_Doctor.DoctorImage);

                    _Doctor.DoctorImage = _Doctor.DoctorImage + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(_Doctor.DoctorImage))
                    {
                        System.IO.File.Delete(_Doctor.DoctorImage);
                    }

                    if (!_Doctor.ImageFile.FileName.ToLower().EndsWith("gif") && !_Doctor.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Doctor.ImageFile.FileName.ToLower().EndsWith("jpeg") && !_Doctor.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Doctor.ImageFile.FileName.ToLower().EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png ";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        validateImage = false;
                    }

                    if (!FileExtension.ToLower().EndsWith("gif") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("jpeg") && !FileExtension.ToLower().EndsWith("jpg") && !FileExtension.ToLower().EndsWith("png"))
                    {
                        ViewBag.TitleSuccsess = "Không upload được file \"" + FileExtension + "\". Chỉ upload được file  .jpeg .gif .png  ";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        validateImage = false;
                    }

                    //Its Create complete path to store in server.
                    //To copy and save file into server.
                    if (validateImage)
                    {
                        _Doctor.ImageFile.SaveAs(_Doctor.DoctorImage);
                    }
                }
                else
                {
                    validateImage = false;
                }

                string result = string.Empty;

                if (_Doctor.id == 0)
                {
                    AddBreadcrumb("Thêm mới bác sỹ", "/News/ListDoctorAdd");
                    if (validateImage)
                    {
                        _Doctor.DoctorImage = Common.Common.GetFormatImage(_Doctor.DoctorImage.Replace(Common.Common.getImagePath(), ""));
                        result = impcms_Doctor.Insertcms_Doctor(_Doctor);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Thêm mới bác sỹ thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionInsertOK.ToString());
                            Response.Redirect("/News/ListDoctor", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Thêm mới bác sỹ không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionInsertNOK.ToString());
                        }
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Mời bạn nhập file ảnh bác sỹ!";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }

                }
                else
                {
                    if (_Doctor.ImageFile != null)

                    {
                        if (validateImage)
                        {
                            AddBreadcrumb("Cập nhật bác sỹ", "/News/ListDoctorAdd");
                            _Doctor.DoctorImage = Common.Common.GetFormatImage(_Doctor.DoctorImage.Replace(Common.Common.getImagePath(), ""));
                            result = impcms_Doctor.Updatecms_Doctor(_Doctor);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ViewBag.TitleSuccsess = "Cập nhật thông tin bác sỹ thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                                AddLogAction(result, Constant.ActionUpdateOK.ToString());
                                Response.Redirect("/News/ListDoctor", false);
                            }
                            else
                            {
                                ViewBag.TitleSuccsess = "Cập nhật thông tin bác sỹ không thành công";
                                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                                AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Mời bạn nhập file ảnh bác sỹ!";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }
                    }
                    else
                    {
                        AddBreadcrumb("Cập nhật bác sỹ", "/News/ListDoctorAdd");
                        _Doctor.DoctorImage = Common.Common.GetFormatImage(_Doctor.DoctorImage.Replace(Common.Common.getImagePath(), ""));
                        result = impcms_Doctor.Updatecms_Doctor(_Doctor);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin bác sỹ thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                            AddLogAction(result, Constant.ActionUpdateOK.ToString());
                            Response.Redirect("/News/ListDoctor", false);
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Cập nhật thông tin bác sỹ không thành công";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                            AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                        }
                    }
                }
            }


            return View(_Doctor);
        }

        #endregion


        #region --> Danh sách comment
        public ActionResult ListComment()
        {
            // Initialization.  
            AddPageHeader("Danh sách liên hệ", "");
            AddBreadcrumb("Danh sách liên hệ", "/News/ListComment");           
            AddBreadcrumb("Danh sách liên hệ", "/News/ListComment");
            CMS_Core.Models.SearchNewsViewModel searchNewsView = new CMS_Core.Models.SearchNewsViewModel();
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetCommentKeyword = Common.Common.GetCommentKeyword();
            try
            {
                searchNewsView.tungay = DateTime.Now.AddDays(-14).ToString("dd/MM/yyyy");
                searchNewsView.denngay = DateTime.Now.ToString("dd/MM/yyyy");
                searchNewsView.keyword = string.Empty;
                searchNewsView.ParrenId = 0;
                searchNewsView.TypeKeyword = 0;
                searchNewsView.Status = 2;

                List<cms_Comment> cms_Comments = impcms_Comment.Getcms_Comment(DateTime.Now.AddDays(-14).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"), 2, 1, string.Empty);
                ViewBag.cms_Comments = cms_Comments;
            }
            catch (Exception ex)
            {
                logError.Info("ListComment: " + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListComment(CMS_Core.Models.SearchNewsViewModel searchNewsView, string submit)
        {
            // Initialization.  
            AddPageHeader("Danh sách liên hệ", "");
            AddBreadcrumb("Danh sách liên hệ", "/News/ListComment");
            AddBreadcrumb("Danh sách liên hệ", "/News/ListComment");
            DateTime Tungay = new DateTime();
            DateTime Denngay = new DateTime();
            bool check = true;
            this.ViewBag.GetTypeStatus = Common.Common.GetTypeStatus();
            this.ViewBag.GetCommentKeyword = Common.Common.GetCommentKeyword();

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
                    check = false;
                }
            }
            catch
            {
                ViewBag.TitleSuccsess = "Dữ liệu ngày tháng không đúng định dạng";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                check = false;
            }
            #endregion


            try
            {
                if (check)
                {
                    switch (submit)
                    {
                        case "SearchListComment":

                            List<cms_Comment> cms_Comments = impcms_Comment.Getcms_Comment(Tungay.ToString("yyyyMMdd"), Denngay.ToString("yyyyMMdd"), searchNewsView.Status, searchNewsView.TypeKeyword, searchNewsView.keyword);
                            ViewBag.cms_Comments = cms_Comments;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("ListContact:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(searchNewsView);


        }





        public ActionResult ListCommentAdd(string CommentId, string newsid)
        {
            // Initialization.             
            AddPageHeader("Danh sách liên hệ", "");
            AddBreadcrumb("Danh sách liên hệ", "/News/ListComment");
            AddBreadcrumb("Comment khách hàng", "/News/ListCommentAdd");



            List<cms_News> cms_NewsValue = new List<cms_News>();

            cms_Comment _Comment = new cms_Comment();
            try
            {
                if (string.IsNullOrEmpty(newsid))
                {
                    ViewBag.TitleSuccsess = "Tham số không hợp lệ! Mời bạn thực hiện lại";
                    ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                }
                else
                {
                    cms_NewsValue = impcms_News.Getcms_NewsByID(Convert.ToInt32(newsid));
                    _Comment.NewsId = Convert.ToInt32(newsid);

                    if (cms_NewsValue != null)
                    {
                        if (cms_NewsValue.Count > 0)
                        {
                            ViewBag.cms_News = cms_NewsValue;
                            if (!string.IsNullOrEmpty(CommentId))
                            {
                                _Comment = impcms_Comment.Get(Convert.ToInt32(CommentId));
                                _Comment.NewsId = Convert.ToInt32(newsid);
                            }
                        }
                        else
                        {
                            ViewBag.TitleSuccsess = "Không tìm thấy thông tin bài viết! ";
                            ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                        }

                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Không tìm thấy thông tin bài viết! ";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
                    }


                }
            }
            catch (Exception ex)
            {
                logError.Info("ListContactAdd:" + ex.ToString());
                ViewBag.TitleSuccsess = "Có lỗi xẩy ra trong quá trình thực hiện, Mời bạn thực hiện lại!";
                ViewBag.TypeAlert = CMSLIS.Common.Constant.typeError;
            }

            // Info.  
            return View(_Comment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListCommentAdd(cms_Comment _Comment)
        {
            // Initialization.             
            AddPageHeader("Danh sách liên hệ", "");
            AddBreadcrumb("Danh sách liên hệ", "/News/ListComment");
            AddBreadcrumb("Comment khách hàng", "/News/ListCommentAdd");

            if (ModelState.IsValid)
            {
                bool validateImage = true;

                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = Common.Common.getImagePath() + "ImagePath\\imageslead\\";

                //Upload Image
                if (_Comment.ImageFile != null)
                {
                    // Use Namespace called: System.IO
                    string FileName = Path.GetFileNameWithoutExtension(_Comment.ImageFile.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(_Comment.ImageFile.FileName);

                    //Add Current Date To Attached File Name                      
                    _Comment.ImgAvatar = UploadPath + DateTime.Now.ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(_Comment.ImgAvatar))
                        Directory.CreateDirectory(_Comment.ImgAvatar);

                    _Comment.ImgAvatar = _Comment.ImgAvatar + "/" + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim().Replace(" ","") + FileExtension;
                    if (System.IO.File.Exists(_Comment.ImgAvatar))
                    {
                        System.IO.File.Delete(_Comment.ImgAvatar);
                    }

                    if (!_Comment.ImageFile.FileName.ToLower().EndsWith("gif") && !_Comment.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Comment.ImageFile.FileName.ToLower().EndsWith("jpeg") && !_Comment.ImageFile.FileName.ToLower().EndsWith("jpg") && !_Comment.ImageFile.FileName.ToLower().EndsWith("png"))
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
                        _Comment.ImageFile.SaveAs(_Comment.ImgAvatar);
                        _Comment.ImgAvatar = Common.Common.GetPathImage(_Comment.ImgAvatar.Replace(Common.Common.getImagePath(), ""));
                    }
                }
                else
                {
                    validateImage = false;
                }

                string result = string.Empty;
                if (_Comment.CommentId == 0)
                {
                    _Comment.CreateBy = ((cms_Account)Session["UserInfo"]).uid;
                    result = impcms_Comment.Add(_Comment);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Thêm mới comment thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionInsertOK.ToString());
                        Response.Redirect("/News/ListComment", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Thêm mới comment không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionInsertNOK.ToString());
                    }
                }
                else
                {
                    result = impcms_Comment.Update(_Comment);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ViewBag.TitleSuccsess = "Cập nhật comment thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateOK.ToString());
                        Response.Redirect("/News/ListComment", false);
                    }
                    else
                    {
                        ViewBag.TitleSuccsess = "Cập nhật comment không thành công";
                        ViewBag.TypeAlert = CMSLIS.Common.Constant.typeSuccsess;
                        AddLogAction(result, Constant.ActionUpdateNOK.ToString());
                    }
                }

            }
            List<cms_News> cms_NewsValue = new List<cms_News>();

            cms_NewsValue = impcms_News.Getcms_NewsByID(Convert.ToInt32(_Comment.NewsId));
            ViewBag.cms_News = cms_NewsValue;
            return View(_Comment);
        }


        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult ListCommentDelete(string[] customerIDs)
        {
            // Initialization.  
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách liên hệ", "/News/ListComment");

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
                        result = impcms_Comment.Delete(Convert.ToInt32(customerID));
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
                logError.Info("ListOrderBillDelete: " + ex.ToString());
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
        public ActionResult ListCommentPublic(string[] customerIDs)
        {
            AddBreadcrumb("Tin tức", "/News");
            AddBreadcrumb("Danh sách liên hệ", "/News/ListComment");



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
                        impcms_Comment.Publish(Convert.ToInt32(customerID));
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
                logError.Info("ListOrderPublic: " + ex.ToString());
            }

            return Json("Duyệt thành công bản ghi có id là: " + listID);

            // Info.  
            //return View();
        }

        #endregion

    }
}
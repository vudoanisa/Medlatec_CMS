using CMS_Core.Entity;
using CMS_Core.Implementtion;
using log4net;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;


namespace CMSLIS.Common
{
    public class Common
    {
        #region Properties
        private static string ConnStr = ConfigurationSettings.AppSettings["ConnStr"];
        private static ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");
        private static string ImagePath = ConfigurationSettings.AppSettings["ImagePath"];
        private static string ImageLink = ConfigurationSettings.AppSettings["ImageLink"];
        private static string KeyPrivate = ConfigurationSettings.AppSettings["KeyPrivate"];
        private static Random rnd = new Random();
        #endregion Properties

        #region GetData

        /// <summary>
        /// getImagePath
        /// </summary>        
        /// <returns></returns> 
        public static string getImagePath()
        {

            return ImagePath;
        }

        /// <summary>
        /// getKeyPrivate
        /// </summary>        
        /// <returns></returns> 
        public static string getKeyPrivate()
        {

            return KeyPrivate;
        }


        /// <summary>
        /// getKeyPrivate
        /// </summary>        
        /// <returns></returns> 
        public static string generalKeyPrivate(string sid, string pid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sid))
                {
                    sid = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(pid))
                {
                    pid = string.Empty;
                }

                return SaltedHash.GetSHA512(sid + getKeyPrivate() + pid);

            }
            catch
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// getKeyPrivate
        /// </summary>        
        /// <returns></returns> 
        public static string generalKeyPrivate(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    id = string.Empty;
                }
                return SaltedHash.GetSHA512(id + getKeyPrivate());
            }
            catch
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// getKeyPrivate
        /// </summary>        
        /// <returns></returns> 
        public static bool CheckKeyPrivate(string sid, string pid, string keyPrivate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sid))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(pid))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(keyPrivate))
                {
                    return false;
                }
                else
                {
                    if (keyPrivate.Equals(SaltedHash.GetSHA512(sid + getKeyPrivate() + pid)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

        }


        /// <summary>
        /// getKeyPrivate
        /// </summary>        
        /// <returns></returns> 
        public static bool CheckKeyPrivate(string id, string keyPrivate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(keyPrivate))
                {
                    return false;
                }
                else
                {
                    if (keyPrivate.Equals(SaltedHash.GetSHA512(id + getKeyPrivate())))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

        }



        /// <summary>
        /// getImagePath
        /// </summary>        
        /// <returns></returns> 
        public static string getImageLink()
        {

            return ImageLink;
        }

        /// <summary>  
        /// Get Getcms_AccountType method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_AccountType()
        {
            try
            {
                List<cms_AccountType> data = null;
                if (HttpContext.Current.Session["Getcms_AccountType"] != null)
                {
                    data = (List<cms_AccountType>)HttpContext.Current.Session["Getcms_AccountType"];
                }
                else
                {
                    Impcms_AccountType _AccountType = new Impcms_AccountType();
                    data = _AccountType.GetAll();
                    HttpContext.Current.Session["Getcms_AccountType"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        items.Add(new SelectListItem { Text = value.AName, Value = value.AccountTypeId.ToString() });
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_AccountType:" + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeKeywordSlider()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tiêu đề", Value = "1" });
                items.Add(new SelectListItem { Text = "Theo url", Value = "2" });



                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeStatus: " + ex.ToString());
                return null;
            }
        }


        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeKeywordCompany()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tên phòng khám", Value = "1" });
                items.Add(new SelectListItem { Text = "Giám đốc", Value = "2" });



                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeStatus: " + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeKeywordTestcode()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Mã xét nghiệm", Value = "1" });
                items.Add(new SelectListItem { Text = "Tên xét nghiệm", Value = "2" });
                items.Add(new SelectListItem { Text = "Linh bài viết", Value = "3" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeStatus: " + ex.ToString());
                return null;
            }
        }


        public static List<SelectListItem> GetTypeKeywordSeo()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Mô tả seo", Value = "1" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeStatus: " + ex.ToString());
                return null;
            }
        }

        public static List<SelectListItem> GetTypeKeywordVoteCate()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tên loại vote", Value = "1" });
                items.Add(new SelectListItem { Text = "Mô tả vote", Value = "2" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeywordVoteCate: " + ex.ToString());
                return null;
            }
        }

        public static List<SelectListItem> GetTypeKeywordVote()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tên vote", Value = "1" });
                items.Add(new SelectListItem { Text = "Mô tả vote", Value = "2" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeywordVote: " + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeCompany()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Bệnh viện & Phòng khám", Value = "1" });
                items.Add(new SelectListItem { Text = "Chi nhánh", Value = "2" });
                items.Add(new SelectListItem { Text = "Văn phòng", Value = "3" });



                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeStatus: " + ex.ToString());
                return null;
            }
        }

        public static List<SelectListItem> GetTypeLocaltion()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Miền bắc", Value = "MB" });
                items.Add(new SelectListItem { Text = "Miền trung", Value = "MT" });
                items.Add(new SelectListItem { Text = "Miền nam", Value = "MN" });



                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeLocaltion: " + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeStatus()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Tất cả", Value = "-1" });
                items.Add(new SelectListItem { Text = "Ẩn", Value = "0" });
                items.Add(new SelectListItem { Text = "Chờ duyệt", Value = "1" });
                items.Add(new SelectListItem { Text = "Hoạt động", Value = "2" });



                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeStatus: " + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeKeyword()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "PID", Value = "1" });
                items.Add(new SelectListItem { Text = "Tên bệnh nhân", Value = "2" });
                items.Add(new SelectListItem { Text = "Số điện thoại", Value = "3" });
                items.Add(new SelectListItem { Text = "Email", Value = "4" });
                // items.Add(new SelectListItem { Text = "Mã khám bệnh", Value = "5" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeyword: " + ex.ToString());
                return null;
            }
        }



        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetCommentKeyword()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tên người gửi", Value = "1" });
                items.Add(new SelectListItem { Text = "Tiêu đề gửi", Value = "2" });
                items.Add(new SelectListItem { Text = "Tên bài viết", Value = "3" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetCommentKeyword: " + ex.ToString());
                return null;
            }
        }


        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeMenuKeyword()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tên menu", Value = "1" });
                items.Add(new SelectListItem { Text = "Link", Value = "2" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeyword: " + ex.ToString());
                return null;
            }
        }


        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeNewsKeyword()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tên bài viết", Value = "1" });
                items.Add(new SelectListItem { Text = "Mô tả", Value = "2" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeyword: " + ex.ToString());
                return null;
            }
        }


        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetCategoryKeyword()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tên chuyên mục", Value = "1" });
                items.Add(new SelectListItem { Text = "Mô tả chuyên mục", Value = "2" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeyword: " + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeAccountKeyword()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Username", Value = "1" });
                items.Add(new SelectListItem { Text = "Email", Value = "2" });
                items.Add(new SelectListItem { Text = "Mobile", Value = "3" });
                items.Add(new SelectListItem { Text = "Họ tên", Value = "4" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeyword: " + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetAccountTypeKeyword()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tên nhóm", Value = "1" });
                items.Add(new SelectListItem { Text = "Mô tả nhóm", Value = "2" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeyword: " + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeKeywordWeirdo()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "PID", Value = "1" });
                items.Add(new SelectListItem { Text = "Email", Value = "2" });
                items.Add(new SelectListItem { Text = "Số điện thoại", Value = "3" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeyword: " + ex.ToString());
                return null;
            }
        }


        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeKeywordSick()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tên bệnh", Value = "1" });
                items.Add(new SelectListItem { Text = "Mô tả", Value = "2" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeyword: " + ex.ToString());
                return null;
            }
        }
        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeKeywordMedicine()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Tên thuốc", Value = "1" });
                items.Add(new SelectListItem { Text = "Mô tả", Value = "2" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeyword: " + ex.ToString());
                return null;
            }
        }



        /// <summary>  
        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeKeywordMedicalAppointment()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "Mã khám bệnh", Value = "1" });
                items.Add(new SelectListItem { Text = "Tên bệnh nhân", Value = "2" });
                items.Add(new SelectListItem { Text = "Số điện thoại", Value = "3" });
                items.Add(new SelectListItem { Text = "Email", Value = "4" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeywordMedicalAppointment: " + ex.ToString());
                return null;
            }
        }


        /// Get GetTypeStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetTypeKeywordWaitingForMedical()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });
                items.Add(new SelectListItem { Text = "PID", Value = "1" });
                items.Add(new SelectListItem { Text = "SID", Value = "2" });
                items.Add(new SelectListItem { Text = "SĐT", Value = "3" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetTypeKeywordWaitingForMedical: " + ex.ToString());
                return null;
            }
        }




        /// <summary>  
        /// Get Getcms_MenuParrent method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_MenuParrent()
        {
            try
            {
                List<cms_Menu> data = null;
                if (HttpContext.Current.Session["Getcms_MenuParrent"] != null)
                {
                    data = (List<cms_Menu>)HttpContext.Current.Session["Getcms_MenuParrent"];
                }
                else
                {
                    Impcms_Menu impcms_Menu = new Impcms_Menu();
                    data = impcms_Menu.GetAllcms_MenuParent();
                    HttpContext.Current.Session["Getcms_MenuParrent"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        items.Add(new SelectListItem { Text = value.menName, Value = value.menId.ToString() });
                    }
                }

                items.Add(new SelectListItem { Text = "Menu gốc", Value = "0" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_MenuParrent:" + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// Get Getcms_MenuParrent method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<Sys_GroupMenuControlPermission> GetControlPermission(int GroupID)
        {
            try
            {
                List<Sys_GroupMenuControlPermission> data = null;
                List<Sys_GroupMenuControlPermission> ValueReturn = new List<Sys_GroupMenuControlPermission>();
                string url = HttpContext.Current.Request.RawUrl.ToLower();

                if (HttpContext.Current.Session["GetControlPermission"] != null)
                {
                    data = (List<Sys_GroupMenuControlPermission>)HttpContext.Current.Session["GetControlPermission"];
                }
                else
                {
                    ImpSys_GroupMenuControlPermission impSys_GroupMenuControlPermission = new ImpSys_GroupMenuControlPermission();
                    data = impSys_GroupMenuControlPermission.GetSys_GroupMenuControlPermissionByGroupID(GroupID);
                    HttpContext.Current.Session["GetControlPermission"] = data;
                }

                if (data != null)
                {
                    foreach (var value in data)
                    {
                        if (url.IndexOf(value.MenLink.ToLower()) != -1)
                        {
                            ValueReturn.Add(value);
                        }
                    }
                }

                return ValueReturn;

            }
            catch (Exception ex)
            {
                logError.Info("GetControlPermission:" + ex.ToString());
                return null;
            }
        }





        /// <summary>  
        /// Get Getcms_MenuChild method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_MenuChild(int ParrentID)
        {
            try
            {
                List<cms_Menu> data = null;
                if (HttpContext.Current.Session["Getcms_MenuChild" + ParrentID] != null)
                {
                    data = (List<cms_Menu>)HttpContext.Current.Session["Getcms_MenuChild" + ParrentID];
                }
                else
                {
                    Impcms_Menu impcms_Menu = new Impcms_Menu();
                    data = impcms_Menu.GetAllcms_MenuChild(ParrentID);
                    HttpContext.Current.Session["Getcms_MenuChild" + ParrentID] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        items.Add(new SelectListItem { Text = value.menName, Value = value.menId.ToString() });
                    }
                }

                items.Add(new SelectListItem { Text = "Menu Con ", Value = "0" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_MenuChild: " + ex.ToString());
                return null;
            }
        }


        /// <summary>  
        /// Get Getcms_CategoryParrent method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_CategoryParrent()
        {
            try
            {
                List<cms_Category> data = null;
                if (HttpContext.Current.Session["Getcms_CategoryParrent"] != null)
                {
                    data = (List<cms_Category>)HttpContext.Current.Session["Getcms_CategoryParrent"];
                }
                else
                {
                    Impcms_Category impcms_Category = new Impcms_Category();
                    data = impcms_Category.Getcms_CategoryParent();
                    HttpContext.Current.Session["Getcms_CategoryParrent"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();

                CMS_Core.Common.ComboBoxFinal<cms_Category> comboBoxFinal = new CMS_Core.Common.ComboBoxFinal<cms_Category>();
                items = comboBoxFinal.GetComboBox(data, "cateId", "cateName", true);



                //if (data != null)
                //{
                //    foreach (var value in data)
                //    {
                //        items.Add(new SelectListItem { Text = value.cateName, Value = value.cateId.ToString() });
                //    }
                //}

                // items.Add(new SelectListItem { Text = "Chuyên mục gốc", Value = "0" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_CategoryParrent:" + ex.ToString());
                return null;
            }
        }





        /// <summary>  
        /// Get Getcms_CategoryParrent method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_Account()
        {
            try
            {
                List<cms_Account> data = null;
                if (HttpContext.Current.Session["Getcms_Account"] != null)
                {
                    data = (List<cms_Account>)HttpContext.Current.Session["Getcms_Account"];
                }
                else
                {
                    ImpCms_Account impCms_Account = new ImpCms_Account();
                    data = impCms_Account.GetAll();
                    HttpContext.Current.Session["Getcms_Account"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        items.Add(new SelectListItem { Text = value.Username, Value = value.uid.ToString() });
                    }
                }

                items.Add(new SelectListItem { Text = "Chọn tất cả", Value = "0" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_CategoryParrent:" + ex.ToString());
                return null;
            }
        }






        /// <summary>  
        /// Get Getcms_Permissiont method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_Permissiont()
        {
            try
            {
                List<cms_Permission> data = null;
                if (HttpContext.Current.Session["Getcms_Permissiont"] != null)
                {
                    data = (List<cms_Permission>)HttpContext.Current.Session["Getcms_Permissiont"];
                }
                else
                {
                    Impcms_Permission impcms_Permission = new Impcms_Permission();
                    data = impcms_Permission.GetAllcms_Permission();
                    HttpContext.Current.Session["Getcms_Permissiont"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        items.Add(new SelectListItem { Text = value.permissionName, Value = value.permissionId.ToString() });
                    }
                }


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Permissiont:" + ex.ToString());
                return null;
            }
        }




        ///// <summary>  
        ///// Get Getcms_Permissiont method.  
        ///// </summary>  
        ///// <returns>Return Type for drop down list.</returns>  
        //public static List<SelectListItem> GetComboBox(List<AnyType> data,  int selected)
        //{
        //    try
        //    {
        //        List<AnyType> data = null;
        //        if (HttpContext.Current.Session["Getcms_Permissiont"] != null)
        //        {
        //            data = (List<cms_Permission>)HttpContext.Current.Session["Getcms_Permissiont"];
        //        }
        //        else
        //        {
        //            Impcms_Permission impcms_Permission = new Impcms_Permission();
        //            data = impcms_Permission.GetAllcms_Permission();
        //            HttpContext.Current.Session["Getcms_Permissiont"] = data;
        //        }

        //        List<SelectListItem> items = new List<SelectListItem>();
        //        if (data != null)
        //        {
        //            foreach (var value in data)
        //            {
        //                if (value.permissionId == selected)
        //                {
        //                    items.Add(new SelectListItem { Text = value.permissionName, Value = value.permissionId.ToString(), Selected = true });
        //                }
        //                else
        //                {
        //                    items.Add(new SelectListItem { Text = value.permissionName, Value = value.permissionId.ToString() });
        //                }
        //            }
        //        }


        //        return items;
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.Info("Getcms_Permissiont:" + ex.ToString());
        //        return null;
        //    }
        //}



        /// <summary>  
        /// Get Getcms_Permissiont method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_Permissiont(int selected)
        {
            try
            {
                List<cms_Permission> data = null;
                if (HttpContext.Current.Session["Getcms_Permissiont"] != null)
                {
                    data = (List<cms_Permission>)HttpContext.Current.Session["Getcms_Permissiont"];
                }
                else
                {
                    Impcms_Permission impcms_Permission = new Impcms_Permission();
                    data = impcms_Permission.GetAllcms_Permission();
                    HttpContext.Current.Session["Getcms_Permissiont"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        if (value.permissionId == selected)
                        {
                            items.Add(new SelectListItem { Text = value.permissionName, Value = value.permissionId.ToString(), Selected = true });
                        }
                        else
                        {
                            items.Add(new SelectListItem { Text = value.permissionName, Value = value.permissionId.ToString() });
                        }
                    }
                }


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Permissiont:" + ex.ToString());
                return null;
            }
        }


        /// <summary>  
        /// Get GetStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetStatus(int selected)
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();
                if (selected == 1)
                {
                    items.Add(new SelectListItem { Text = "Chờ duyệt", Value = "1", Selected = true });
                }
                else
                {
                    items.Add(new SelectListItem { Text = "Chờ duyệt", Value = "1" });
                }
                if (selected == 2)
                {
                    items.Add(new SelectListItem { Text = "Duyệt", Value = "2", Selected = true });
                }
                else
                {
                    items.Add(new SelectListItem { Text = "Duyệt", Value = "2" });
                }

                if (selected == 0)
                {
                    items.Add(new SelectListItem { Text = "Xóa", Value = "0", Selected = true });
                }
                else
                {
                    items.Add(new SelectListItem { Text = "Xóa", Value = "0" });
                }


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetStatus: " + ex.ToString());
                return null;
            }
        }


        /// <summary>  
        /// Get GetStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> GetStatus()
        {
            try
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Chờ duyệt", Value = "1", Selected = true });

                items.Add(new SelectListItem { Text = "Duyệt", Value = "2", Selected = true });

                items.Add(new SelectListItem { Text = "Xóa", Value = "0", Selected = true });
                return items;
            }
            catch (Exception ex)
            {
                logError.Info("GetStatus: " + ex.ToString());
                return null;
            }
        }



        /// <summary>  
        /// Get GetStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_Language(int selected)
        {
            try
            {

                List<cms_Language> data = null;
                if (HttpContext.Current.Session["Getcms_Language"] != null)
                {
                    data = (List<cms_Language>)HttpContext.Current.Session["Getcms_Language"];
                }
                else
                {
                    Impcms_Language impcms_Language = new Impcms_Language();
                    data = impcms_Language.Getcms_LanguageAll(CMSLIS.Common.Constant.TypeStatusAll);
                    HttpContext.Current.Session["Getcms_Language"] = data;
                }


                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        if (selected == value.ID)
                        {
                            items.Add(new SelectListItem { Text = value.LanguageName, Value = value.ID.ToString(), Selected = true });
                        }
                        else
                        {
                            items.Add(new SelectListItem { Text = value.LanguageName, Value = value.ID.ToString() });
                        }
                    }
                }
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Language: " + ex.ToString());
                return null;
            }
        }


        /// <summary>  
        /// Get GetStatus method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_Language()
        {
            try
            {

                List<cms_Language> data = null;
                if (HttpContext.Current.Session["Getcms_Language"] != null)
                {
                    data = (List<cms_Language>)HttpContext.Current.Session["Getcms_Language"];
                }
                else
                {
                    Impcms_Language impcms_Language = new Impcms_Language();
                    data = impcms_Language.Getcms_LanguageAll(CMSLIS.Common.Constant.TypeStatusAll);
                    HttpContext.Current.Session["Getcms_Language"] = data;
                }


                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        items.Add(new SelectListItem { Text = value.LanguageName, Value = value.ID.ToString() });
                    }
                }
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Language: " + ex.ToString());
                return null;
            }
        }




        /// <summary>  
        /// Get Getcms_Permissiont method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_NewsSource()
        {
            try
            {
                List<cms_NewsSource> data = null;
                if (HttpContext.Current.Session["Getcms_NewsSource"] != null)
                {
                    data = (List<cms_NewsSource>)HttpContext.Current.Session["Getcms_NewsSource"];
                }
                else
                {
                    Impcms_NewsSource impcms_NewsSource = new Impcms_NewsSource();
                    data = impcms_NewsSource.GetAllcms_NewsSource();
                    HttpContext.Current.Session["Getcms_NewsSource"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        items.Add(new SelectListItem { Text = value.SourceName, Value = value.SourceId.ToString() });
                    }
                }
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_NewsSource: " + ex.ToString());
                return null;
            }
        }

        /// <summary>  
        /// Get Getcms_Permissiont method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_NewsTypeMessage()
        {
            try
            {
                List<cms_NewsTypeMessage> data = null;
                if (HttpContext.Current.Session["Getcms_NewsTypeMessage"] != null)
                {
                    data = (List<cms_NewsTypeMessage>)HttpContext.Current.Session["Getcms_NewsTypeMessage"];
                }
                else
                {
                    Impcms_NewsTypeMessage impcms_NewsTypeMessage = new Impcms_NewsTypeMessage();

                    data = impcms_NewsTypeMessage.GetAllcms_NewsTypeMessage();
                    HttpContext.Current.Session["Getcms_NewsTypeMessage"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        items.Add(new SelectListItem { Text = value.TypeMessageName, Value = value.TypeMessageId.ToString() });
                    }
                }
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_NewsTypeMessage: " + ex.ToString());
                return null;
            }
        }

        #endregion


        #region private

        /// <summary>
        /// get IP Logcal
        /// </summary>        
        /// <returns></returns> 
        public static string GetIPHelper()
        {
            string ip = string.Empty;
            try
            {
                ip = HttpContext.Current.Request.UserHostAddress;
            }
            catch { }

            return ip;
        }


        /// <summary>
        /// convert object to string
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static string getFeedBack(bool Status)
        {
            try
            {
                if (Status == true)
                {
                    return "<span style=\"width: 100px; \"><span class=\"badge-text badge-text-small success\">FeedBack</span></span>";
                }
                else if (Status == false)
                {
                    return "<span style=\"width: 100px; \"><span class=\"badge-text badge-text-small danger\">Chờ FeedBack</span></span>";
                }
                else
                {
                    return "<span style=\"width: 100px; \"><span class=\"badge-text badge-text-small info\">Chờ FeedBack</span></span>";
                }
            }
            catch
            {
                return "";
            }
        }


        public static string RenderFinal(int GroupID, string url)
        {
            var strBuilder = new StringBuilder();
            // check null URL
            if (string.IsNullOrEmpty(url))
            {
                url = string.Empty;
            }
            if (!string.IsNullOrEmpty(url))
            {
                url = url.Substring(1);
            }


            Impcms_Menu_Cms impcms_Menu = new Impcms_Menu_Cms();
            List<cms_Menu_Cms> dataParent = null;
            if (HttpContext.Current.Session["GetAllcms_MenuParentByUserid" + GroupID] != null)
            {
                dataParent = (List<cms_Menu_Cms>)HttpContext.Current.Session["GetAllcms_MenuParentByUserid" + GroupID];
            }
            else
            {
                dataParent = impcms_Menu.GetAllcms_MenuParentByGroupID(GroupID);
                HttpContext.Current.Session["GetAllcms_MenuParentByUserid" + GroupID] = dataParent;
            }

            if (dataParent != null)
            {
                strBuilder.Append("<li class=\"treeview\">\n");
                dataParent.ForEach(x =>
                {
                    List<cms_Menu_Cms> listChild = null;

                    if (HttpContext.Current.Session["GetAllcms_MenuChildByUserid" + x.menId] != null)
                    {
                        listChild = (List<cms_Menu_Cms>)HttpContext.Current.Session["GetAllcms_MenuChildByUserid" + x.menId];
                    }
                    else
                    {
                        listChild = impcms_Menu.GetAllcms_MenuChildByGroupID(x.menId, GroupID);
                        HttpContext.Current.Session["GetAllcms_MenuChildByUserid" + x.menId] = listChild;
                    }



                    //tag open <li> parent
                    strBuilder.Append(listChild != null && listChild.Count > 0 ? "<li>\n" : "<li>\n");
                    if (listChild != null && listChild.Count > 0)
                    {
                        bool isAuthorization = false;
                        foreach (var Child in listChild)
                        {
                            if (url.Equals(Child.menLinks.ToLower()))
                            {
                                isAuthorization = true;
                                break;
                            }

                            if ((url.IndexOf("catalogsystem/categorydynamic") == -1) && (url.IndexOf("Clinic/ListPatientWaitingForMedical") == -1) && (url.IndexOf(Child.menLinks.ToLower()) != -1))
                            {
                                isAuthorization = true;
                                break;
                            }
                            if ((url.IndexOf("Clinic/ListPatientWaitingForMedical") == -1) && (url.IndexOf(Child.menLinks.ToLower()) != -1))
                            {
                                isAuthorization = true;
                                break;
                            }


                        }

                        if (isAuthorization == true)
                        {
                            //tag open <li> parent
                            strBuilder.Append("<li class=\"treeview menu-open\">\n");
                            //tên menu cha
                            strBuilder.AppendFormat("<a href=\"#\"><i class=\"{1}\"></i><span>{0}</span><span class=\"pull-right-container\"><i class=\"fa fa-angle-left pull-right\"></i></span></a>\n", x.menName, x.Style);
                            //menu con
                            strBuilder.Append("<ul class=\"treeview-menu\"  style =\"display: block;\">\n");
                        }
                        else
                        {
                            //tag open <li> parent
                            strBuilder.Append("<li class=\"treeview\">\n");
                            //tên menu cha
                            strBuilder.AppendFormat("<a href=\"#\"><i class=\"{1}\"></i><span>{0}</span><span class=\"pull-right-container\"><i class=\"fa fa-angle-left pull-right\"></i></span></a>\n", x.menName, x.Style);
                            //menu con
                            strBuilder.Append("<ul class=\"treeview-menu\">\n");
                        }



                        strBuilder.Append(LoopBackChildMenu(listChild, GroupID, url));
                        //end ul tag child
                        strBuilder.Append("</ul>\n");
                    }
                    else
                    {
                        strBuilder.Append("<li>\n");
                        strBuilder.AppendFormat("<a href=\"{0}\"><i class=\"{1}\"></i><span>{2}</span></a>\n", x.menLinks, x.Style, x.menName);

                        //  strBuilder.AppendFormat("<a href=\"{0}\"><i class=\"{1}\"></i><span>{2}</span></a></li>\n", x.menLinks, x.Style, x.menName);
                    }
                });

                //end tag <li> parent
                strBuilder.Append("</li>\n");
            }
            return strBuilder.ToString();
        }



        /// <summary>  
        /// Get Getcms_Permissiont method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_NewsDoctor()
        {
            try
            {
                List<cms_Doctor> data = null;
                if (HttpContext.Current.Session["Getcms_NewsDoctor"] != null)
                {
                    data = (List<cms_Doctor>)HttpContext.Current.Session["Getcms_NewsDoctor"];
                }
                else
                {
                    Impcms_Doctor impcms_Doctor = new Impcms_Doctor();

                    data = impcms_Doctor.GetAllcms_DoctorActive();
                    HttpContext.Current.Session["Getcms_NewsDoctor"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();
                if (data != null)
                {
                    foreach (var value in data)
                    {
                        items.Add(new SelectListItem { Text = value.DoctorName, Value = value.id.ToString() });
                    }
                }
                items.Add(new SelectListItem { Text = "Tất cả", Value = "0" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_NewsDoctor: " + ex.ToString());
                return null;
            }
        }




        /// <summary>  
        /// Get Getcms_CategoryParrent method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_CategoryParrentBYCateID(string CateID)
        {
            try
            {
                if (string.IsNullOrEmpty(CateID))
                    CateID = string.Empty;


                List<cms_Category> data = null;
                if (HttpContext.Current.Session["Getcms_CategoryParrent"] != null)
                {
                    data = (List<cms_Category>)HttpContext.Current.Session["Getcms_CategoryParrent"];
                }
                else
                {
                    Impcms_Category impcms_Category = new Impcms_Category();
                    data = impcms_Category.Getcms_CategoryParent();
                    HttpContext.Current.Session["Getcms_CategoryParrent"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();

                //CMS_Core.Common.ComboBoxFinal<cms_Category> comboBoxFinal = new CMS_Core.Common.ComboBoxFinal<cms_Category>();
                //items = comboBoxFinal.GetComboBox(data, "cateId", "cateName", true);

                if (data != null)
                {
                    foreach (var value in data)
                    {
                        if (!string.IsNullOrEmpty(value.cateLevel))
                        {
                            if (value.cateLevel.ToString().Length > 5)
                            {
                                if (CateID.Equals(value.cateId.ToString()))
                                {
                                    items.Add(new SelectListItem { Text = "|--->" + value.cateName, Value = value.cateId.ToString(), Selected = true });
                                }
                                else
                                {
                                    items.Add(new SelectListItem { Text = "|--->" + value.cateName, Value = value.cateId.ToString() });
                                }
                            }
                            else
                            {
                                if (CateID.Equals(value.cateId.ToString()))
                                {
                                    items.Add(new SelectListItem { Text = value.cateName, Value = value.cateId.ToString(), Selected = true });
                                }
                                else
                                {
                                    items.Add(new SelectListItem { Text = value.cateName, Value = value.cateId.ToString() });
                                }
                            }
                        }

                    }
                }


                // items.Add(new SelectListItem { Text = "Chuyên mục gốc", Value = "0" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_CategoryParrent:" + ex.ToString());
                return null;
            }
        }



        /// <summary>  
        /// Get Getcms_CategoryParrent method.  
        /// </summary>  
        /// <returns>Return Type for drop down list.</returns>  
        public static List<SelectListItem> Getcms_CategoryParrent(string cateLevel)
        {
            try
            {
                if (string.IsNullOrEmpty(cateLevel))
                    cateLevel = string.Empty;
                if (cateLevel.Length <= 5)
                {
                    cateLevel = string.Empty;
                }

                if (cateLevel.Length > 5)
                {
                    cateLevel = cateLevel.Substring(0, 5);
                }

                List<cms_Category> data = null;
                if (HttpContext.Current.Session["Getcms_CategoryParrent"] != null)
                {
                    data = (List<cms_Category>)HttpContext.Current.Session["Getcms_CategoryParrent"];
                }
                else
                {
                    Impcms_Category impcms_Category = new Impcms_Category();
                    data = impcms_Category.Getcms_CategoryParent();
                    HttpContext.Current.Session["Getcms_CategoryParrent"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();

                //CMS_Core.Common.ComboBoxFinal<cms_Category> comboBoxFinal = new CMS_Core.Common.ComboBoxFinal<cms_Category>();
                //items = comboBoxFinal.GetComboBox(data, "cateId", "cateName", true);

                if (data != null)
                {
                    foreach (var value in data)
                    {
                        if (value.cateLevel.ToString().Length > 5)
                        {
                            if (cateLevel.Equals(value.cateLevel.ToString()))
                            {
                                items.Add(new SelectListItem { Text = "|--->" + value.cateName, Value = value.cateLevel.ToString(), Selected = true });
                            }
                            else
                            {
                                items.Add(new SelectListItem { Text = "|--->" + value.cateName, Value = value.cateLevel.ToString() });
                            }
                        }
                        else
                        {
                            if (cateLevel.Equals(value.cateLevel.ToString()))
                            {
                                items.Add(new SelectListItem { Text = value.cateName, Value = value.cateLevel.ToString(), Selected = true });
                            }
                            else
                            {
                                items.Add(new SelectListItem { Text = value.cateName, Value = value.cateLevel.ToString() });
                            }
                        }

                    }
                }

                if (string.IsNullOrEmpty(cateLevel))
                {
                    items.Add(new SelectListItem { Text = "Chuyên mục gốc", Value = "", Selected = true });
                }
                else
                {
                    items.Add(new SelectListItem { Text = "Chuyên mục gốc", Value = "" });
                }


                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_CategoryParrent:" + ex.ToString());
                return null;
            }
        }




        /// <summary>
        /// convert object to string
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static string getTypeStatus(bool Status)
        {
            try
            {
                if (Status == true)
                {
                    return "<span style=\"width: 100px; \"><span class=\"badge-text badge-text-small success\">Xuất bản</span></span>";
                }
                else if (Status == false)
                {
                    return "<span style=\"width: 100px; \"><span class=\"btn btn-warning  btn-xs\">Chờ duyệt</span></span>";
                }
                else
                {
                    return "<span style=\"width: 100px; \"><span class=\"badge-text badge-text-small info\">Chờ duyệt</span></span>";
                }
            }
            catch
            {
                return "";
            }
        }


        public static string GetURLDetailByTestcode(string title, string newsid)
        {
            try
            {
                return "https://medlatec.vn/" + "y-nghia-xet-nghiem/" + getNiceUrl(title.Trim()) + "-n" + newsid;
            }
            catch
            {
                return "https://medlatec.vn/" + "y-nghia-xet-nghiem/" + getNiceUrl(title.Trim()) + "-n" + newsid;
            }
        }

        public static string GetURLDetailByNews(string aliasCate, string title, string cateid, string newsid)
        {
            try
            {

                return "https://medlatec.vn/" + aliasCate + "/" + getNiceUrl(title) + "-" + cateid + "-" + newsid;

            }
            catch
            {
                return "https://medlatec.vn/" + aliasCate + "/" + getNiceUrl(title) + "-" + cateid + "-" + newsid;
            }
        }
        public static string GetURLDetailByDictionary(string aliasCate, string url)
        {
            return "https://medlatec.vn/" + aliasCate + "/" + url + "/";
        }

        public static List<SelectListItem> Getcms_CategoryParrentNews()
        {
            try
            {
                List<cms_Category> data = null;
                if (HttpContext.Current.Session["Getcms_CategoryParrent"] != null)
                {
                    data = (List<cms_Category>)HttpContext.Current.Session["Getcms_CategoryParrent"];
                }
                else
                {
                    Impcms_Category impcms_Category = new Impcms_Category();
                    data = impcms_Category.Getcms_CategoryParent();
                    HttpContext.Current.Session["Getcms_CategoryParrent"] = data;
                }

                List<SelectListItem> items = new List<SelectListItem>();

                //CMS_Core.Common.ComboBoxFinal<cms_Category> comboBoxFinal = new CMS_Core.Common.ComboBoxFinal<cms_Category>();
                //items = comboBoxFinal.GetComboBox(data, "cateId", "cateName", true);

                if (data != null)
                {
                    foreach (var value in data)
                    {
                        if (!string.IsNullOrEmpty(value.cateLevel))
                        {
                            if (value.cateLevel.ToString().Length > 5)
                            {
                                items.Add(new SelectListItem { Text = "|--->" + value.cateName, Value = value.cateId.ToString() });
                            }
                            else
                            {
                                items.Add(new SelectListItem { Text = value.cateName, Value = value.cateId.ToString() });
                            }
                        }
                    }
                }

                items.Add(new SelectListItem { Text = "Chuyên mục gốc", Value = "0" });

                return items;
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_CategoryParrent:" + ex.ToString());
                return null;
            }
        }
        private static char[] _letter = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        public static string GetRandom(int size)
        {
            var data = new byte[1];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(data);
                data = new byte[size];
                rng.GetNonZeroBytes(data);
            }

            var result = new StringBuilder(size);
            foreach (var o in data)
                result.Append(_letter[o % (_letter.Length)]);

            return result.ToString();
        }
        public static string GetCode(string s)
        {
            s = RemoveNotAbcChar(RemoveVietNamese(s));

            return s.Trim().Replace(" ", "-")
                .Replace("'", "")
                .Replace("/", "-")
                .Replace("*", "-")
                .Replace("\\", "-")
                .Replace("--", "-")
                .Replace("--", "-").ToLower();

        }
        public static string GetFirstChar(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string result = "";

            //lấy danh sách các từ  
            s = RemoveVietNamese(s);

            string[] words = s.Split(' ');
            for (int i = 0; i < 1; i++)
            {
                if (words[i].Length > 1)
                    result += words[i].Substring(0, 1).ToUpper();
            }


            return result.Trim();
        }
        public static string RemoveVietNamese(string s)
        {
            const string findText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string replText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";

            int index;
            while ((index = s.IndexOfAny(findText.ToCharArray())) != -1)
            {
                var index2 = findText.IndexOf(s[index]);
                s = s.Replace(s[index], replText[index2]);
            }
            return s;
        }
        public static string GetRandom(int size, bool letterOrNumber)
        {
            _letter = letterOrNumber ? "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray() : "1234567890".ToCharArray();

            return GetRandom(size);
        }

        private static string RemoveNotAbcChar(string s)
        {
            var result = string.Empty;
            foreach (var t in s)
            {
                var c = t;

                if (c == 160)
                    c = ' ';

                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                    result += c.ToString();
            }
            return result;
        }


        public static string AddAutoLink(string Html, int LinksLimitPerPage, int OptionReplate)
        {
            string HtmlOld = Html;
            try
            {
                string HtmlReturn = string.Empty;
                string keyword = string.Empty;
                ImpAutolink impAutolink = new ImpAutolink();
                List<Autolink> Autolinks = impAutolink.GetAllAutolink();

                if (string.IsNullOrEmpty(Html))
                {
                    Html = string.Empty;
                }

                keyword = Tagging(Html, LoadDict(Autolinks), LinksLimitPerPage);

                if (!string.IsNullOrEmpty(keyword))
                {
                    string[] ListKeywords = keyword.Split(',');
                    foreach (string data in ListKeywords)
                    {
                        List<Autolink> autolinks = impAutolink.GetAutolinkByName(data.Trim());
                        if (autolinks != null)
                        {
                            if (autolinks.Count > 0)
                            {
                                if (autolinks[0].TypeStart == 1) // thay vị trí đầu
                                {
                                    string replace = "<a href='" + autolinks[0].URL + "'  title ='" + data.Trim() + "'>" + EDS.Common.HtmlEncoderNew.Encode(data.Trim()) + "</a>";

                                    Html = ReplaceFirstOccurrence(Html, EDS.Common.HtmlEncoderNew.Encode(data.Trim()), replace);
                                    //int Place = Html.IndexOf(EDS.Common.HtmlEncoderNew.Encode(data.Trim()));
                                    int Place = Html.IndexOf(replace);

                                    if (Place != -1)
                                    {
                                        // string replace = "<a href='" + autolinks[0].URL + "'  title ='" + data.Trim() + "'>";
                                        if (Html.Length > Place + replace.Length)
                                        {
                                            HtmlReturn = HtmlReturn + Html.Substring(0, Place + replace.Length);
                                            Html = Html.Substring(Place + replace.Length, Html.Length - (Place + replace.Length));
                                        }
                                    }

                                    // Html = ReplaceFirstOccurrence(Html, EDS.Common.HtmlEncoderNew.Encode(data.Trim()), "<a href='" + autolinks[0].URL + "'  title ='" + data.Trim() + " '>" + EDS.Common.HtmlEncoderNew.Encode(data.Trim()) + "</a>");
                                }
                                else if (autolinks[0].TypeStart == 2) // thay vị trí đầu
                                {
                                    Html = ReplaceLastOccurrence(Html, EDS.Common.HtmlEncoderNew.Encode(data.Trim()), "<a href='" + autolinks[0].URL + "'  title ='" + data.Trim() + " '>" + EDS.Common.HtmlEncoderNew.Encode(data.Trim()) + "</a>");
                                }

                            }

                        }

                    }
                }

                return HtmlReturn + Html;
            }
            catch (Exception ex)
            {
                return HtmlOld;
            }
        }


        public static string GetTinBaiDetail(string cateId, string newsId, string title)
        {
            try
            {

                return "Tin-tuc/" + getNiceUrl(title) + "-" + cateId + "-" + newsId;

            }
            catch
            {
                return "Tin-tuc/" + getNiceUrl(title) + "-" + cateId + "-" + newsId;
            }
        }


        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


        public static string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            string SourceOld = Source;
            try

            {
                string HtmlReturn = string.Empty;
                int PlaceKeyword = Source.IndexOf(Find);

                int EndPlaceH1 = Source.IndexOf("</h1>");
                int StartPlaceH1 = Source.IndexOf("<h1");

                int EndPlaceH2 = Source.IndexOf("</h2>");
                int StartPlaceH2 = Source.IndexOf("<h2");


                if (EndPlaceH1 != -1 && PlaceKeyword != -1)
                {
                    if (PlaceKeyword < EndPlaceH1 && PlaceKeyword > StartPlaceH1)
                    {
                        HtmlReturn = HtmlReturn + Source.Substring(0, EndPlaceH1);
                        Source = Source.Substring(EndPlaceH1, Source.Length - EndPlaceH1);
                    }
                    else if (EndPlaceH2 != -1 && PlaceKeyword != -1)
                    {
                        if (PlaceKeyword < EndPlaceH2 && PlaceKeyword > StartPlaceH2)
                        {
                            HtmlReturn = HtmlReturn + Source.Substring(0, EndPlaceH2);
                            Source = Source.Substring(EndPlaceH2, Source.Length - EndPlaceH2);
                        }
                    }
                }
                else if (EndPlaceH2 != -1 && PlaceKeyword != -1)
                {
                    if (PlaceKeyword < EndPlaceH2 && PlaceKeyword > StartPlaceH2)
                    {
                        HtmlReturn = HtmlReturn + Source.Substring(0, EndPlaceH2);
                        Source = Source.Substring(EndPlaceH2, Source.Length - EndPlaceH2);
                    }
                }

                Match match = Regex.Match(Source, "<img[^>]+>");
                int Place = Source.IndexOf(Find);

                if (match.Success)
                {
                    int PostionImage = Source.IndexOf("<img");
                    if (Place != -1 && PostionImage != -1)
                    {
                        if (Place < PostionImage + match.Value.Length && Place > PostionImage)
                        {
                            HtmlReturn = HtmlReturn + Source.Substring(0, PostionImage + match.Value.Length);
                            Source = Source.Substring(PostionImage + match.Value.Length, Source.Length - PostionImage - match.Value.Length);
                        }
                    }
                }

                Place = Source.IndexOf(Find);
                if (Place != -1)
                {
                    string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
                    return HtmlReturn + result;
                }
                else
                {
                    return HtmlReturn + Source;
                }
            }
            catch
            {
                return SourceOld;
            }
        }

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            try
            {
                int Place = Source.LastIndexOf(Find);
                if (Place != -1)
                {
                    string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
                    return result;
                }
                else
                {
                    return Source;
                }
            }
            catch
            {
                return Source;
            }
        }

        public static List<String> LoadDict(List<Autolink> Autolinks)
        {
            List<String> dict = new List<string>();

            try
            {
                if (Autolinks != null)
                {
                    foreach (var data in Autolinks)
                    {
                        dict.Add(data.Keyword.ToLower().Trim());
                    }
                }

                dict.Sort();
            }
            catch { };

            return dict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string Tagging(string html, List<String> dict, int countKeyword)
        {
            string data = EDS.Common.Web.Content.StripHTML(html);

            //string data = input.Title + " " + input.Description;

            data = EDS.Common.Utilities.ConvertISO8859ToUnicode(data);

            List<EDS.Common.StatisticsTableInfo> list = EDS.Common.Tag.Tagging(dict, data);

            string tags = "";
            int count = 0;
            if (list.Count > 0)
            {
                foreach (EDS.Common.StatisticsTableInfo info in list)
                {
                    if (count < countKeyword)
                    {
                        tags += info.Key_Word + ",";
                    }
                    else
                    {
                        break;
                    }

                    count++;
                }
            }


            if (tags.Length > 0)
                return tags.Substring(0, tags.Length - 1);
            else
                return "";
        }


        public static string GetFormatImage(string newsImages)
        {
            try
            {
                if (!string.IsNullOrEmpty(newsImages))
                {

                    newsImages = newsImages.Replace("Upload/", "").Replace("\\", "/");

                }
                return newsImages;

            }
            catch
            {
                return newsImages;
            }
        }

        public static bool sendGmail(string title, string fullname, string to, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("suport.medlatec@gmail.com");
                mail.To.Add(to);
                //mail.Bcc.Add("nguyennhathoang@medlatec.com");
                //mail.CC.Add("nguyennhathoang@medlatec.com");
                //  mail.CC.Add("trandinhtoan1988@gmail.com");

                string bodyContent = "<p><span style='font-size:18px'><strong>K&iacute;nh thưa qu&yacute; kh&aacute;ch</strong></span>: <span style='font-size:18px'>" + fullname.ToUpper() + "</span></p>";

                bodyContent += "<p><strong><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>Bệnh viện Đa khoa MEDLATEC xin trả lời c&acirc;u hỏi của qu&yacute; kh&aacute;ch.</span></strong></p>";
                bodyContent += "<p>" + body + "</p><table border='1' cellpadding='0' cellspacing='0' class='MsoNormalTable' style='background:#daeef3; border-collapse:collapse; border:none; mso-border-alt:solid windowtext .5pt; mso-border-insideh:.5pt solid windowtext; mso-border-insidev:.5pt solid windowtext; mso-padding-alt:0in 5.4pt 0in 5.4pt; mso-yfti-tbllook:1184'>";
                bodyContent += "<tbody>		<tr><td style='vertical-align:top; width:462.25pt'><p><strong><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>Bệnh viện Đa khoa MEDLATEC hiện cung c&aacute;c dịch vụ:</span></strong></p>";
                bodyContent += "<p style='margin-left:.5in'><!--[if !supportLists]--><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>1.<span style='font-size:7pt'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><!--[endif]--><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>Lấy mẫu x&eacute;t nghiệm tận nơi, trả kết quả x&eacute;t qua: Website, Fax, Email, đường bưu điện v&agrave; trực tiếp.</span></p>";
                bodyContent += "<p style='margin-left:.5in'><!--[if !supportLists]--><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>2.<span style='font-size:7pt'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><!--[endif]--><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>Kh&aacute;m c&aacute;c chuy&ecirc;n khoa Nội, Ngoại, Sản, Nhi, Chuy&ecirc;n khoa lẻ (Tai-Mũi-Họng, Răng-H&agrave;m-Mắt, Mắt).</span></p>";
                bodyContent += "<p style='margin-left:.5in'><!--[if !supportLists]--><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>3.<span style='font-size:7pt'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><!--[endif]--><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>Kiểm tra sức khỏe định kỳ cho c&aacute; nh&acirc;n v&agrave; tập thể.</span></p>";
                bodyContent += "<p style='margin-left:.5in'><!--[if !supportLists]--><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>4.<span style='font-size:7pt'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><!--[endif]--><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>X&eacute;t nghiệm: Huyết học, H&oacute;a sinh - Miễn dịch (c&aacute;c loại hormone, c&aacute;c loại marker ung thư,&hellip;), Giải phẫu bệnh (m&ocirc; mềm, m&ocirc; xương, tế b&agrave;o &acirc;m đạo, tế b&agrave;o học,&hellip;), Sinh học ph&acirc;n tử (HBV, HCB, HPV, lậu, chlamydia,&hellip;).</span></p>";
                bodyContent += "<p style='margin-left:.5in'><!--[if !supportLists]--><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>5.<span style='font-size:7pt'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><!--[endif]--><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>Chẩn đo&aacute;n h&igrave;nh ảnh - Thăm d&ograve; chức năng: Chụp CT, si&ecirc;u &acirc;m 3D, 4D, chụp X quang kỹ thuật số, nội soi dạ d&agrave;y,&hellip;</span></p>";
                bodyContent += "<p><span style='font-family:&quot;Times New Roman&quot;,&quot;serif&quot;; font-size:13.0pt'>Bệnh viện l&agrave;m việc 24 giờ/7 ng&agrave;y&nbsp; *&nbsp; Tổng đ&agrave;i li&ecirc;n hệ v&agrave; tư vấn: 1900 56 56 56</span></p>";
                bodyContent += "</td>";
                bodyContent += "</tr></tbody></table><p> &nbsp;</p>";



                mail.Subject = title;

                mail.IsBodyHtml = true;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = bodyContent;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("suport.medlatec@gmail.com", "theson267");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                // lbnThongbao.Text = "Gửi mail bị lỗi";
            }
        }


        public static string GetPathImage(string newsImages)
        {
            try
            {
                if (!string.IsNullOrEmpty(newsImages))
                {
                    if (!newsImages.StartsWith("http"))
                    {
                        newsImages = ImageLink + newsImages.Replace("Upload/", "").Replace("\\", "/");
                    }
                }
                return newsImages;

            }
            catch
            {
                return newsImages;
            }
        }

        public static List<cms_Menu> GetsortCut(int UserID)
        {
            List<cms_Menu> dataSortCut = null;
            try
            {
                Impcms_Menu impcms_Menu = new Impcms_Menu();
                if (HttpContext.Current.Session["GetsortCut" + UserID] != null)
                {
                    dataSortCut = (List<cms_Menu>)HttpContext.Current.Session["GetsortCut" + UserID];
                }
                else
                {
                    dataSortCut = impcms_Menu.GetAllcms_MenuBySortCut(UserID);
                    HttpContext.Current.Session["GetsortCut" + UserID] = dataSortCut;
                }
            }
            catch { }

            return dataSortCut;
        }



        public static string LoopBackChildMenu(List<cms_Menu_Cms> listSys, int UserID, string url)
        {
            var strLoop = new StringBuilder();
            listSys.ForEach(x =>
            {

                if (!string.IsNullOrEmpty(url))
                {
                    bool isAuthorization = false;
                    if (url.Equals(x.menLinks.ToLower()))
                    {
                        isAuthorization = true;
                    }
                    else
                        if ((url.IndexOf("catalogsystem/categorydynamic") == -1) && (url.IndexOf("Clinic/ListPatientWaitingForMedical") == -1) && (url.IndexOf(x.menLinks.ToLower()) != -1))
                    {
                        isAuthorization = true;
                    }
                    //else
                    //    if ((url.IndexOf("Clinic/ListPatientWaitingForMedical") == -1) && (url.IndexOf(x.menLinks.ToLower()) != -1))
                    //{
                    //    isAuthorization = true;
                    //}

                    if (isAuthorization)
                    {
                        strLoop.Append("<li class=\"active\">");
                    }
                    else
                    {
                        strLoop.Append("<li>");
                    }

                    strLoop.AppendFormat("<a  href=\"/{0}\"><i class=\"{1}\"></i><span>{2}</span></a>\n", x.menLinks, x.Style, x.menName);
                    strLoop.Append("</li>\n");

                }
                else
                {
                    strLoop.AppendFormat("<li><a href=\"/{0}\"><i class=\"{1}\"></i><span>{2}</span></a></li>\n", x.menLinks, x.Style, x.menName);
                }

            });

            return strLoop.ToString();
        }




        public static bool Base64ToImage(string base64String, string path)
        {
            bool result = true;
            try
            {
                base64String = base64String.Replace("data:image/jpeg;base64,", "");

                // Convert Base64 String to byte[]
                byte[] imageBytes = Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                image.Save(path);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }


        // <summary>
        /// format datetime
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static string ReFmtTime(object obj)
        {
            try
            {
                string ss = convertString(obj);

                if (DateTime.Parse(ss) < DateTime.Now.AddYears(-100))
                    return "";
                else return DateTime.Parse(ss).ToString("dd/MM/yyyy HH:mm");
            }
            catch
            {
                return "";
            }
        }


        public static string getRandom()
        {
            return rnd.Next(99999).ToString();
        }

        public static string getNiceUrl(Object objurl)
        {
            try
            {
                String url = objurl.ToString();
                String niceurl = ConvertEN(url);
                niceurl = niceurl.Replace("-", "");
                niceurl = niceurl.Replace(" ", "-");
                niceurl = niceurl.Replace("_", "-");
                niceurl = niceurl.Replace("nbsp;", "-");
                niceurl = niceurl.Replace("'", "");
                // niceurl = niceurl.Replace("-", "_");

                niceurl = removeChar(niceurl, new String[] { "/", "m²", ":", ",", "<", ">", "”", "“", ".", "!", "?", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "~", "`", "'", "'", "\"" });
                return niceurl;
            }
            catch
            {
                return "";
            }
        }

        public static String removeChar(String niceurl, String[] danhsach)
        {
            foreach (String xoa in danhsach)
            {
                niceurl = niceurl.Replace(xoa, "");
            }
            return niceurl;
        }
        public static string ConvertVietnameseCharacterToEN(string sCharacter)
        {
            string sTemplate = "ĂẮẰẲẴẶăắằẳẵặÂẤẦẨẪẬâấầẩẫậÁÀẢÃẠáàảãạÔỐỒỔỖỘôốồổỗộƠỚỜỞỠỢơớờởỡợÓÒỎÕỌóòỏõọĐđÊẾỀỂỄỆêếềểễệÉÈẺẼẸéèẻẽẹƯỨỪỬỮỰưứừửữựÚÙỦŨỤúùủũụÍÌỈĨỊíìỉĩịÝỲỶỸỴýỳỷỹỵ";
            string sReplate = "AAAAAAaaaaaaAAAAAAaaaaaaAAAAAaaaaaOOOOOOooooooOOOOOOooooooOOOOOoooooDdEEEEEEeeeeeeEEEEEeeeeeUUUUUUuuuuuuUUUUUuuuuuIIIIIiiiiiYYYYYyyyyy";
            char[] arrChar = sTemplate.ToCharArray();
            char[] arrReChar = sReplate.ToCharArray();
            string sResult = "";//sCharacter;
            char digit;

            for (int ch = 0; ch < sCharacter.Length; ch++)
            {
                digit = Convert.ToChar(sCharacter.Substring(ch, 1));//arrChar[ch].ToString();;
                for (int i = 0; i < arrChar.Length; i++)
                    if (digit.Equals(arrChar[i]))
                        digit = arrReChar[i];
                sResult += digit;
            }

            return sResult;
        }
        public static string ConvertEN(string sCharacter)
        {
            string sTemplate = "ĂẮẰẲẴẶăắằẳẵặÂẤẦẨẪẬâấầẩẫậÁÀẢÃẠáàảãạÔỐỒỔỖỘôốồổỗộƠỚỜỞỠỢơớờởỡợÓÒỎÕỌóòỏõọĐđÊẾỀỂỄỆêếềểễệÉÈẺẼẸéèẻẽẹƯỨỪỬỮỰưứừửữựÚÙỦŨỤúùủũụÍÌỈĨỊíìỉĩịÝỲỶỸỴýỳỷỹỵ";
            string sReplate = "AAAAAAaaaaaaAAAAAAaaaaaaAAAAAaaaaaOOOOOOooooooOOOOOOooooooOOOOOoooooDdEEEEEEeeeeeeEEEEEeeeeeUUUUUUuuuuuuUUUUUuuuuuIIIIIiiiiiYYYYYyyyyy";
            string sDau = "[̣̃́̉̀]"; // dấu ngã, sắc, nặng, hỏi, ngã
            char[] arrChar = sTemplate.ToCharArray();
            char[] arrReChar = sReplate.ToCharArray();
            string sResult = "";//sCharacter;
            char digit;
            // modified by datnd - 1/4/2010
            // xoa di tat ca cac dau
            Regex reg = new Regex(sDau);
            sCharacter = reg.Replace(sCharacter, "");
            // xoa dia tat ca cac ki tu ma hoa html	http://www.kamol.info/html-encoding-of-foreign-language-characters/		
            try
            {
                sCharacter = Regex.Replace(sCharacter, @"&(\w)(\w){4,5};", "$1");
            }
            catch (ArgumentException ex) { }

            // end modified
            for (int ch = 0; ch < sCharacter.Length; ch++)
            {
                digit = Convert.ToChar(sCharacter.Substring(ch, 1));//arrChar[ch].ToString();;
                for (int i = 0; i < arrChar.Length; i++)
                {
                    if (digit.Equals(arrChar[i]))
                        digit = arrReChar[i];
                }
                sResult += digit;
            }

            return sResult;
        }


        /// <summary>
        /// convert object to string
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static string convertString(object obj)
        {
            try
            {
                return obj.ToString().Trim();
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// convert object to string
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static string getStatusActive(bool action)
        {
            try
            {
                if (action)
                {
                    return "<span style=\"width: 100px; \"><span class=\"badge-text badge-text-small success\">Hoạt động</span></span>";
                }
                else
                {
                    return "<span style=\"width: 100px; \"><span class=\"badge-text badge-text-small danger\">Ẩn</span></span>";
                }
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// convert object to string
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static string getTypeStatus(int Status)
        {
            try
            {
                if (Status == 2)
                {
                    return "<span style=\"width: 130px; \"><span class=\"btn btn-info btn-xs\">Hoạt động</span></span>";
                }
                else if (Status == 0)
                {
                    return "<span style=\"width: 130px; \"><span class=\"btn btn-danger btn-xs \">Ẩn</span></span>";
                }
                else
                {
                    return "<span style=\"width: 130px; \"><span class=\"btn btn-warning  btn-xs \">Chờ duyệt</span></span>";
                }
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// format tiền tệ
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static string ReFmtAmt(object oj, bool isCutoff)
        {
            try
            {
                string ss = convertString(oj);
                if (ss.Length > 2 && isCutoff)
                    ss = ss.Substring(0, ss.Length - 2);

                return FmtAmt(convertInt(ss));
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// format datetime
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static string ReFmtDate(object obj)
        {
            try
            {
                string ss = convertString(obj);

                if (DateTime.Parse(ss) < DateTime.Now.AddYears(-100))
                    return "";
                else return DateTime.Parse(ss).ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// convert object to int
        /// </summary>
        /// <param name="obj">input</param>
        /// <returns></returns>
        public static int convertInt(object obj)
        {
            try
            {
                return int.Parse(obj.ToString().Trim());
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// convert object  
        /// </summary>
        /// <param name="obj">input</param>
        /// <returns></returns>
        public static string FmtAmt(object obj)
        {
            try
            {
                double dobj = double.Parse(obj.ToString());
                CultureInfo cultureToUse = new CultureInfo("en-GB");

                string ss = dobj.ToString("N", cultureToUse);
                ss = ss.Replace(".00", "");
                return ss;
            }
            catch
            {
                return convertString(obj);
            }
        }
        /// <summary>
        /// convert object  
        /// </summary>
        /// <param name="obj">input</param>
        /// <returns></returns>
        public static string FmtAmtWithDecimal(object obj)
        {
            try
            {
                double dobj = double.Parse(obj.ToString());
                CultureInfo cultureToUse = new CultureInfo("en-GB");

                string ss = dobj.ToString("N", cultureToUse);
                //ss = ss.Replace(".00", "");
                return ss;
            }
            catch
            {
                return convertString(obj);
            }
        }


        /// <summary>
        /// convert string to datatime
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.Now;
            }
            if (retVal == new DateTime(1, 1, 1)) return DateTime.Now;

            return retVal;
        }

        /// <summary>
        /// convert string to datatime
        /// </summary>
        /// <param name="obj">string datatime</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.Now;
            }
            if (retVal == new DateTime(1, 1, 1)) return defaultValue;

            return retVal;
        }


        /// <summary>
        /// Trả về ngày tháng tiếng việt
        /// </summary>
        /// <param name="date">datetime</param>
        /// <returns></returns>
        public static string VNFormatDate(DateTime date)
        {
            string sDate = "";

            try
            {

                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        sDate = "Thứ Hai  &bull;   ";
                        break;
                    case DayOfWeek.Tuesday:
                        sDate = "Thứ Ba  &bull;  ";
                        break;
                    case DayOfWeek.Wednesday:
                        sDate = "Thứ Tư  &bull;  ";
                        break;
                    case DayOfWeek.Thursday:
                        sDate = "Thứ Năm  &bull;  ";
                        break;
                    case DayOfWeek.Friday:
                        sDate = "Thứ Sáu   &bull; ";
                        break;
                    case DayOfWeek.Saturday:
                        sDate = "Thứ Bảy   &bull; ";
                        break;
                    case DayOfWeek.Sunday:
                        sDate = "Chủ Nhật  &bull;  ";
                        break;
                }


            }
            catch
            {

            }
            sDate += " " + date.ToString("dd/MM/yyyy &bull;   HH:mm");
            return sDate;
        }

        /// <summary>
        /// kiểm tra xem có phải là số không
        /// </summary>
        /// <param name="input">input validate</param>
        /// <returns></returns>
        public static bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }


        public static string FormatSave(string Html)
        {

            if (string.IsNullOrEmpty(Html))
            {
                Html = string.Empty;
            }

            Html = Html.Replace("http://login.medlatec.vn", "");

            Html = Html.Replace(ImageLink, "");
            Html = Html.Replace(ImagePath, "");
            Html = Html.Replace("/../", "../");
            Html = Html.Replace("//", "/");
            Html = Html.Replace(ImagePath + "/Images/images", "/Images/images");
            Html = Html.Replace("/ckfinder/userfiles", "ckfinder/userfiles");

            Html = Html.Replace("\\", "/");
            // Html = Html.Replace("/ckfinder/userfiles", ImageLink + "/ckfinder/userfiles");
            Html = Html.Replace("ckfinder/userfiles", ImageLink + "/ckfinder/userfiles");
            Html = Html.Replace("/ImagePath/images", "ImagePath/images");
            Html = Html.Replace("ImagePath/images", ImageLink + "/ImagePath/images");

            Html = Html.Replace(".http://", "http://");
            Html = Html.Replace(".//", "./");
            Html = Html.Replace(".//", "./");
            Html = Html.Replace(".\\", "./");
            Html = Html.Replace("\\", "/");


            Html = Html.Replace(".../http://", "http://");
            Html = Html.Replace("<span style=\"font-family: Times New Roman;\">&nbsp;</span>", "");
            Html = Html.Replace("<p></p>", "");
            Html = Html.Replace("<p> </p>", "");
            Html = Html.Replace("&rsquo;", "'");
            Html = Html.Replace("muted = \"\"", "");
            Html = Html.Replace(".JPG", ".jpg");
            Html = Html.Replace(".PNG", ".png");
            Html = Html.Replace(".GIF", ".gif");

            return Html;
        }


        public static string FormatView(string Html)
        {



            Html = Html.Replace("/http://", "http://");
            Html = Html.Replace("//http://", "//http://");



            return Html;
        }


        //public static string FormatView(string Html)
        //{



        //    Html = Html.Replace("/cms/", "/");
        //    Html = Html.Replace("../../../ImagePath/images", "/ImagePath/images");
        //    Html = Html.Replace("../../ImagePath/images", "/ImagePath/images");
        //    Html = Html.Replace("../ImagePath/images", "/ImagePath/images");
        //    Html = Html.Replace("./ImagePath/images", "/ImagePath/images");
        //    Html = Html.Replace(".../http://", "http://");


        //    Html = Html.Replace("./ImagePath/images", "/ImagePath/images");
        //    Html = Html.Replace("../ImagePath/media", "/ImagePath/media");




        //    Html = Html.Replace("/ImagePath/images", ImageLink + "ImagePath/images"); //swich to new ip

        //    Html = Html.Replace("../ImagePath/media", "/ImagePath/media");
        //    Html = Html.Replace("./ImagePath/media", "/ImagePath/media");
        //    Html = Html.Replace("ImagePath/media", "/ImagePath/media");
        //    Html = Html.Replace("//ImagePath/media", "/ImagePath/media");
        //    Html = Html.Replace("/ImagePath/media", ImageLink + "ImagePath/media"); //swich to new ip



        //    Html = Html.Replace("/http://", "http://");
        //    Html = Html.Replace("//http://", "//http://");



        //    return Html;
        //}




        /// <summary>
        /// Get giới tính
        /// </summary>
        /// <param name="obj">input</param>
        /// <returns></returns>
        public static string GetGENDERName(string GENDER)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(GENDER))
                    GENDER = string.Empty;
                if (GENDER.Equals("1"))
                    return "Nam";
                else if (GENDER.Equals("0"))
                    return "Nữ";
                else
                    return "Khác";
            }
            catch
            {
                return "Khác";
            }
        }
        /// <summary>
        /// Get giới tính
        /// </summary>
        /// <param name="obj">input</param>
        /// <returns></returns>
        public static string GetStatusWaiting(int status)
        {
            try
            {

                if (status == 1)
                    return "Chờ gọi";
                else return "Qua lượt";
            }
            catch
            {
                return "Khác";
            }
        }


        #endregion




        public static string reSizeImage(string path)
        {
            string pathNew = string.Empty;
            try
            {
                byte[] data = null;
                FileInfo fInfo = new FileInfo(path);
                long numBytes = fInfo.Length;
                FileStream fStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                data = br.ReadBytes((int)numBytes);


                MemoryStream img = new MemoryStream(data);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(img);

                pathNew = resizeImageDetail(returnImage, 400, path);

                img.Dispose();
                br.Close();
                if (br is IDisposable)
                    ((IDisposable)br).Dispose();
                if (fStream is IDisposable)
                    ((IDisposable)fStream).Dispose();

            }
            catch (Exception ex)
            {
            }

            return pathNew;
        }

        public static string reSizeImageBanner(string path)
        {
            string pathNew = string.Empty;
            try
            {
                byte[] data = null;
                FileInfo fInfo = new FileInfo(path);
                long numBytes = fInfo.Length;
                FileStream fStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                data = br.ReadBytes((int)numBytes);


                MemoryStream img = new MemoryStream(data);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(img);

                pathNew = resizeImageDetail(returnImage, 800, path);

                img.Dispose();
                br.Close();
                if (br is IDisposable)
                    ((IDisposable)br).Dispose();
                if (fStream is IDisposable)
                    ((IDisposable)fStream).Dispose();

            }
            catch (Exception ex)
            {
            }

            return pathNew;
        }

        

     

        public static string GenerateLinkQR(string sid)
        {
            if (string.IsNullOrEmpty(sid))
            {
                return "https://medlatec.vn/gop-y-online";
            }
            else
            {
                sid = sid.Trim();
                string keyPrivate = "MedCOm20190401";
                string link = string.Empty;
                link = CMS_Core.Common.AES.EncryptString(sid, keyPrivate);
                link = "https://medlatec.vn/gop-y-online?sid=" + link + "&token=" + CMS_Core.Common.SaltedHash.GetSHA512(sid);
                return link;
            }           
        }





        public static string reSizeImageDetail(string path)
        {
            string pathNew = string.Empty;
            try
            {
                byte[] data = null;
                FileInfo fInfo = new FileInfo(path);
                long numBytes = fInfo.Length;
                FileStream fStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                data = br.ReadBytes((int)numBytes);


                MemoryStream img = new MemoryStream(data);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(img);

                pathNew = resizeImageDetail(returnImage, 700, path);

                img.Dispose();
                br.Close();
                if (br is IDisposable)
                    ((IDisposable)br).Dispose();
                if (fStream is IDisposable)
                    ((IDisposable)fStream).Dispose();

            }
            catch (Exception ex)
            {
            }

            return pathNew;
        }

        public static string resizeImageDetail(System.Drawing.Image ima, int widths, string fileName)
        {
            try
            {
                int maxWidth = widths;
                decimal dHeight, dWidth, dNewHeight, dNewWidth;



                if (fileName.ToLower().Contains(".jpg"))
                {
                    fileName = fileName.ToLower().Replace(".jpg", "web_" + maxWidth.ToString() + ".jpg");
                }
                else if (fileName.ToLower().Contains(".jpeg"))
                {
                    fileName = fileName.ToLower().Replace(".jpeg", "web_" + maxWidth.ToString() + ".jpeg");
                }
                else if (fileName.ToLower().Contains(".gif"))
                {
                    fileName = fileName.ToLower().Replace(".gif", "web_" + maxWidth.ToString() + ".gif");
                }
                else if (fileName.ToLower().Contains(".png"))
                {
                    fileName = fileName.ToLower().Replace(".png", "web_" + maxWidth.ToString() + ".png");
                }
                fileName = fileName.Replace("/", "\\");



                if (widths == 320)
                    maxWidth = 400;



                int width = ima.Width;
                int height = ima.Height;

                if (width > maxWidth)
                {
                    Decimal divider = Math.Abs((Decimal)width / (Decimal)maxWidth);
                    height = (int)Math.Round((Decimal)(height / divider));
                }
                else
                {
                    maxWidth = width;
                }

                Size newSize = new Size(maxWidth, height);


                // Bitmap dstImg = new Bitmap(newOrigWidth, newOrigHeight);
                // dstImg.SetResolution(72, 72);
                //Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
                using (Bitmap thumb = new Bitmap(maxWidth, height, PixelFormat.Format32bppArgb))
                {
                    //thumb.SetResolution(500, 500);
                    using (Graphics g = Graphics.FromImage(thumb)) // Create Graphics object from original Image
                    {
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.DrawImage(ima, new Rectangle(0, 0, thumb.Width, thumb.Height));

                        //string path_to_folder = System.IO.Path.GetDirectoryName(ConfigurationSettings.AppSettings["ImagePath"] + fileName);
                        //if (!System.IO.Directory.Exists(path_to_folder))
                        //    Directory.CreateDirectory(path_to_folder);

                        EncoderParameters encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 95L);
                        thumb.Save(fileName, ImageCodecInfo.GetImageEncoders()[1], encoderParameters);

                    }

                }

                return fileName;
            }
            catch (Exception ex)
            {

                return "";
            }
        }



        public static string CropImage(string imagePath, int x, int y, int width, int height, string type)
        {
            string imageResult = imagePath;
            try
            {
                Bitmap croppedImage;

                // Here we capture the resource - image file.
                using (var originalImage = new Bitmap(imagePath))
                {
                    int originalWidth = originalImage.Width;
                    int originalHeight = originalImage.Height;
                    if (type == "11")
                    {
                        if (originalWidth < width)
                        {
                            width = originalWidth;
                            height = width;
                        }
                        if (originalHeight < height)
                        {
                            height = originalHeight;
                            width = height;
                        }

                        x = (originalWidth - width) / 2;
                        if (x < 0) x = 0;
                        y = (originalHeight - height) / 2;
                        if (y < 0) y = 0;

                    }
                    else if (type == "88")
                    {
                        x = 0;
                        y = 0;
                        height = height;
                        width = width;

                    }
                    else
                    {
                        x = (originalWidth - width) / 2;
                        if (x < 0) x = 0;
                        y = (originalHeight - height) / 2;
                        if (y < 0) y = 0;
                    }


                    Rectangle crop = new Rectangle(x, y, width, height);

                    // Here we capture another resource.
                    croppedImage = originalImage.Clone(crop, originalImage.PixelFormat);

                } // Here we release the original resource - bitmap in memory and file on disk.

                imageResult = imagePath.Replace(".jpg", "_" + type + ".jpg");
                // At this point the file on disk already free - you can record to the same path.
                croppedImage.Save(imageResult, ImageFormat.Jpeg);

                // It is desirable release this resource too.
                croppedImage.Dispose();
            }
            catch { }
            return imageResult;
        }





    }
}
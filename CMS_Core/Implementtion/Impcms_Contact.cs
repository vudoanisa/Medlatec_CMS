using CMS_Core.Common;
using CMS_Core.Entity;
using CMS_Core.Interface;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Implementtion
{
    public class Impcms_Contact : Icms_Contact
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_Contact(cms_Contact _Contact)
        {
            try
            {
                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.ExecuteNonQuery("SP_cms_Contact_DeleteByPrimaryKey", _Contact.contactId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Contact: " + _Contact.contactId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_Contact(int contactId)
        {
            try
            {
                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.ExecuteNonQuery("SP_cms_Contact_DeleteByPrimaryKey", contactId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Contact: " + contactId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_Contact> GetAllcms_Contact()
        {
            try
            {
                SQLServerConnection<cms_Contact> sQLServer = new SQLServerConnection<cms_Contact>();
                return sQLServer.SelectQueryCommand("SP_cms_Contact_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Contact: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Contact> Getcms_ContactByFullName(string FullName)
        {
            try
            {
                SQLServerConnection<cms_Contact> sQLServer = new SQLServerConnection<cms_Contact>();
                return sQLServer.SelectQueryCommand("SP_cms_Contact_SelectByField", "FullName", FullName);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_ContactByFullName: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Contact> Getcms_ContactAll(CMS_Core.Models.SearchNewsViewModel searchNewsView)
        {
            try
            {
                DateTime Tungay = new DateTime();
                DateTime Denngay = new DateTime();

                #region Check input data
                try
                {
                    if (!string.IsNullOrEmpty(searchNewsView.tungay.Trim()))
                        Tungay = DateTime.ParseExact(searchNewsView.tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                    if (!string.IsNullOrEmpty(searchNewsView.denngay.Trim()))
                        Denngay = DateTime.ParseExact(searchNewsView.denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                    if (string.IsNullOrEmpty(searchNewsView.keyword))
                        searchNewsView.keyword = string.Empty;
                }
                catch
                {
                }
                #endregion


                SQLServerConnection<cms_Contact> sQLServer = new SQLServerConnection<cms_Contact>();
                return sQLServer.SelectQueryCommand("SP_cms_Contact_SelectByAll", Tungay,Denngay, searchNewsView.keyword);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_ContactAll: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Contact> Getcms_ContactByID(int contactId)
        {
            try
            {
                SQLServerConnection<cms_Contact> sQLServer = new SQLServerConnection<cms_Contact>();
                return sQLServer.SelectQueryCommand("SP_cms_Contact_SelectByPrimaryKey", contactId);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_ContactByID: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_Contact(cms_Contact _Contact)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteInsert("SP_cms_Contact_Insert", _Contact.FullName, _Contact.Email, _Contact.Address, _Contact.Phone, _Contact.Title, _Contact.Content, _Contact.DateCreate, _Contact.FeedBack, _Contact.DateFeedBack );
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Contact: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string FeedBackcms_Contact(int contactId)
        {
            try
            {
                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.ExecuteNonQuery("SP_cms_Contact_FeedBackByPrimaryKey", contactId);
            }
            catch (Exception ex)
            {
                logError.Info("FeedBackcms_Contact: " + contactId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatecms_Contact(cms_Contact _Contact)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_Contact_Update", _Contact.contactId, _Contact.FullName, _Contact.Email, _Contact.Address, _Contact.Phone, _Contact.Title, _Contact.Content, _Contact.DateCreate, _Contact.FeedBack, _Contact.DateFeedBack);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Contact: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Updatecms_ContactFeedBack(cms_Contact _Contact)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_Contact_UpdateFeedBack", _Contact.contactId, _Contact.ContentFeedBack);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Banner_Plans: " + ex.ToString());
                return result = string.Empty;
            }
        }

    }
}

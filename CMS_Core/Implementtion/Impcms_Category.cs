using CMS_Core.Common;
using CMS_Core.Entity;
using CMS_Core.Interface;
using Dapper;
using log4net;
using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Implementtion
{
    public class Impcms_Category : Icms_Category
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_Category(cms_Category _Category)
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_Category_DeleteByPrimaryKey", _Category.cateId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Category: " + _Category.cateId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_Category(int cateId)
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_Category_DeleteByPrimaryKey", cateId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Category: " + cateId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Publiccms_Category(int cateId)
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_Category_PublicByPrimaryKey", cateId);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Category: " + cateId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_Category> GetAllcms_Category()
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.SelectQueryCommand("SP_cms_Category_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Category: " + ex.ToString());
                return null;
            }
        }


        public List<cms_Category> GetAllcms_CategoryByStatus(int ParentID, int status)
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.SelectQueryCommand("SP_cms_Category_SelectByStatus", ParentID, status);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_CategoryByStatus: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Category> Getcms_CategoryByCateName(string cateName)
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.SelectQueryCommand("SP_cms_Category_SelectByField", "cateName", cateName);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_CategoryByCateName: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Category> Getcms_CategoryByID(int cateId)
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.SelectQueryCommand("SP_cms_Category_SelectByPrimaryKey", cateId);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_CategoryByID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Category> Getcms_CategoryParent()
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.SelectQueryCommand("SP_cms_Category_SelectParent");
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_CategoryParent: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Category> Getcms_CategoryParentNew()
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.SelectQueryCommand("SP_cms_Category_SelectParentNew");
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_CategoryParent: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Category> Getcms_CategoryChild(string Parent)
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.SelectQueryCommand("SP_cms_Category_SelectChild", Parent);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_CategoryParent: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_Category(cms_Category _Category)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteInsert("SP_cms_Category_Insert", _Category.cateName, _Category.cateKeyword, _Category.cateDescription, string.Empty, _Category.cateLevel, _Category.cateOrd, _Category.cateLang, _Category.isShowHome, _Category.cateActive, _Category.TitleSeo);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Category: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Insertcms_CategoryEncrypt(cms_Category _Category, string listColumn)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteInsertEncrypt("SP_cms_Category_Insert_Encrypt", listColumn, _Category.cateName, _Category.cateKeyword, _Category.cateDescription, string.Empty, _Category.cateLevel, _Category.cateOrd, _Category.cateLang, _Category.isShowHome, _Category.cateActive, _Category.cateLink);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_CategoryEncrypt: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Updatecms_Category(cms_Category _Category)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_Category_Update", _Category.cateId, _Category.cateName, _Category.cateKeyword, _Category.cateDescription, string.Empty, _Category.cateLevel, _Category.cateLevelParent, _Category.cateOrd, _Category.cateLang, _Category.isShowHome, _Category.cateActive, _Category.TitleSeo);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Category: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Updatecms_CategoryEncrypt(cms_Category _Category, string listColumn)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteUpdateEncrypt("SP_cms_Category_Update_Encrypt", listColumn, _Category.cateId, _Category.cateName, _Category.cateKeyword, _Category.cateDescription, string.Empty, _Category.cateLevel, _Category.cateOrd, _Category.cateLang, _Category.isShowHome, _Category.cateActive, _Category.cateLink);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Category: " + ex.ToString());
                return result = string.Empty;
            }
        }


    }
}

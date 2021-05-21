using CMS_Core.Common;
using CMS_Core.Entity;
using CMS_Core.Interface;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Implementtion
{
    public class Impcms_News_Relate : Icms_News_Relate
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_News_Relate(cms_News_Relate _News_Relate)
        {
            try
            {
                SQLServerConnection<cms_News_Relate> sQLServer = new SQLServerConnection<cms_News_Relate>();
                return sQLServer.ExecuteNonQuery("SP_cms_Category_DeleteByPrimaryKey", _News_Relate.newsIdRelate);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_News_Relate: " + _News_Relate.newsIdRelate + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_News_Relate(int _Id)
        {
            try
            {
                SQLServerConnection<cms_News_Relate> sQLServer = new SQLServerConnection<cms_News_Relate>();
                return sQLServer.ExecuteNonQuery("SP_cms_Category_DeleteByPrimaryKey", _Id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_News_Relate: " + _Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_News_Relate> GetAllcms_News_Relate()
        {
            try
            {
                SQLServerConnection<cms_News_Relate> sQLServer = new SQLServerConnection<cms_News_Relate>();
                return sQLServer.SelectQueryCommand("SP_cms_Category_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_News_Relate: " + ex.ToString());
                return null;
            }
        }

        public List<cms_News_Relate> Getcms_News_RelateByID(int _Id)
        {
            try
            {
                SQLServerConnection<cms_News_Relate> sQLServer = new SQLServerConnection<cms_News_Relate>();
                return sQLServer.SelectQueryCommand("SP_cms_Category_SelectAll",_Id);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_News_RelateByID: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_News_Relate(cms_News_Relate _News_Relate)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteInsert("SP_cms_News_Relate_Insert_batch", _News_Relate.newsIdSource, _News_Relate.newsIdRelate, _News_Relate.newsName, _News_Relate.newsDescription, _News_Relate.newsImages, _News_Relate.datepub, _News_Relate.cateId, _News_Relate.SourceId);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_News_Relate: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Insertcms_News_Relate(cms_News _News, string newsIdSource)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_News_Relate> sQLServer = new SQLServerConnection<cms_News_Relate>();
                return sQLServer.ExecuteInsert("SP_cms_News_Relate_Insert", newsIdSource, _News.newsId.ToString(), _News.newsName, _News.newsDescription, _News.newsImages, _News.datepub, _News.cateId, _News.SourceId);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_News_Relate: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Deletecms_News_Relate(string listRelate, string newsIdSource)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_News_Relate> sQLServer = new SQLServerConnection<cms_News_Relate>();
                return sQLServer.ExecuteInsert("SP_cms_News_Relate_DeleteNotIn", newsIdSource, listRelate);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_News_Relate: " + ex.ToString());
                return result = string.Empty;
            }
        }



        public string Updatecms_News_Relate(cms_News_Relate _News_Relate)
        {
            throw new NotImplementedException();
        }
    }
}

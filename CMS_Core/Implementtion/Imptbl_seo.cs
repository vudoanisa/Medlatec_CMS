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
    public class Imptbl_seo : Itbl_seo
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletetbl_seo(tbl_seo seo)
        {
            try
            {
                SQLServerConnection<tbl_seo> sQLServer = new SQLServerConnection<tbl_seo>();
                return sQLServer.ExecuteNonQuery("tbl_seo_DeleteByPrimaryKey", seo.id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_VoteQuestions: " + seo.id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletetbl_seo(int Id)
        {
            try
            {
                SQLServerConnection<tbl_seo> sQLServer = new SQLServerConnection<tbl_seo>();
                return sQLServer.ExecuteNonQuery("tbl_seo_DeleteByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_VoteQuestions: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<tbl_seo> GetAlltbl_seo()
        {
            try
            {
                SQLServerConnection<tbl_seo> sQLServer = new SQLServerConnection<tbl_seo>();
                return sQLServer.SelectQueryCommand("tbl_seo_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAlltbl_seo: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_seo> Gettbl_seoByID(int Id)
        {
            try
            {
                SQLServerConnection<tbl_seo> sQLServer = new SQLServerConnection<tbl_seo>();
                return sQLServer.SelectQueryCommand("tbl_seo_SelectByID", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_seoByID: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_seo> Gettbl_seoByCateID(int CateID)
        {
            try
            {
                SQLServerConnection<tbl_seo> sQLServer = new SQLServerConnection<tbl_seo>();
                return sQLServer.SelectQueryCommand("tbl_seo_SelectByCateID", CateID);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_seoByCateID: " + ex.ToString());
                return null;
            }
        }


        public List<tbl_seo> Gettbl_seoByName(string Name)
        {
            return null;
            //try
            //{
            //    SQLServerConnection<tbl_seo> sQLServer = new SQLServerConnection<tbl_seo>();
            //    return sQLServer.SelectQueryCommand("tbl_seo_SelectByID", Name);
            //}
            //catch (Exception ex)
            //{
            //    logError.Info("Gettbl_seoByName: " + ex.ToString());
            //    return null;
            //}
        }

        public List<tbl_seo> Gettbl_seoByStatus(int cateid, int status, int type, string keyword)
        {
            try
            {
                SQLServerConnection<tbl_seo> sQLServer = new SQLServerConnection<tbl_seo>();
                return sQLServer.SelectQueryCommand("tbl_seo_SelectSTATUS", cateid, status, type, keyword);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_seoByStatus: " + ex.ToString());
                return null;
            }
        }

        public string Inserttbl_seo(tbl_seo seo)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_seo> sQLServer = new SQLServerConnection<tbl_seo>();
                return sQLServer.ExecuteInsert("tbl_seo_Insert", seo.Content, seo.type, seo.createby, seo.cateid);
            }
            catch (Exception ex)
            {
                logError.Info("Inserttbl_seo: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publictbl_seo(int Id)
        {
            try
            {
                SQLServerConnection<tbl_seo> sQLServer = new SQLServerConnection<tbl_seo>();
                return sQLServer.ExecuteNonQuery("tbl_seo_PublicByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Publictbl_seo: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatetbl_seo(tbl_seo seo)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_seo> sQLServer = new SQLServerConnection<tbl_seo>();
                return sQLServer.ExecuteNonQuery("tbl_seo_Update", seo.id, seo.Content, seo.type, seo.updateby, seo.cateid);
            }
            catch (Exception ex)
            {
                logError.Info("Updatetbl_seo: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

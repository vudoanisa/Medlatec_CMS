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
    public class ImpAutolink : IAutolink
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string DeleteAutolink(Autolink autolink)
        {
            try
            {
                SQLServerConnection<Autolink> sQLServer = new SQLServerConnection<Autolink>();
                return sQLServer.ExecuteNonQuery("SP_Autolink_DeleteByPrimaryKey", autolink.ID);
            }
            catch (Exception ex)
            {
                logError.Info("DeleteAutolink: " + autolink.ID + "  " + ex.ToString());
                return string.Empty;
            }

        }

        public string DeleteAutolink(int Id)
        {
            try
            {
                SQLServerConnection<Autolink> sQLServer = new SQLServerConnection<Autolink>();
                return sQLServer.ExecuteNonQuery("SP_Autolink_DeleteByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("DeleteAutolink: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<Autolink> GetAllAutolink()
        {
            try
            {
                SQLServerConnection<Autolink> sQLServer = new SQLServerConnection<Autolink>();
                return sQLServer.SelectQueryCommand("SP_Autolink_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllAutolink: " + ex.ToString());
                return null;
            }
        }

        public List<Autolink> GetAllAutolinkByKeyword( string startdate, string enddate, int status, int typeKeyword, string keyword)
        {
            try
            {
                SQLServerConnection<Autolink> sQLServer = new SQLServerConnection<Autolink>();
                return sQLServer.SelectQueryCommand("SP_Autolink_SelectByKeyword", startdate, enddate, status, typeKeyword, keyword);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllAutolinkByKeyword: " + ex.ToString());
                return null;
            }
        }


        public List<Autolink> GetAllAutolinkAddNews()
        {
            try
            {
                SQLServerConnection<Autolink> sQLServer = new SQLServerConnection<Autolink>();
                return sQLServer.SelectQueryCommand("SP_Autolink_SelectAllAddNews");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllAutolinkAddNews: " + ex.ToString());
                return null;
            }
        }


        public List<Autolink> GetAutolinkByID(int Id)
        {
            try
            {
                SQLServerConnection<Autolink> sQLServer = new SQLServerConnection<Autolink>();
                return sQLServer.SelectQueryCommand("SP_Autolink_SelectByID", Id);
            }
            catch (Exception ex)
            {
                logError.Info("GetAutolinkByID: " + ex.ToString());
                return null;
            }
        }

        public List<Autolink> GetAutolinkByName(string Name)
        {
            try
            {
                SQLServerConnection<Autolink> sQLServer = new SQLServerConnection<Autolink>();
                return sQLServer.SelectQueryCommand("SP_Autolink_SelectByName", Name);
            }
            catch (Exception ex)
            {
                logError.Info("GetAutolinkByName: " + ex.ToString());
                return null;
            }
        }

        public string InsertAutolink(Autolink autolink)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<Autolink> sQLServer = new SQLServerConnection<Autolink>();
                return sQLServer.ExecuteInsert("SP_Autolink_Insert", autolink.Keyword, autolink.URL, autolink.SortOrder, autolink.MaxReplacements, autolink.TypeStart);
            }
            catch (Exception ex)
            {
                logError.Info("InsertAutolink: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string PublicAutolink(int Id)
        {
            try
            {
                SQLServerConnection<Autolink> sQLServer = new SQLServerConnection<Autolink>();
                return sQLServer.ExecuteNonQuery("SP_Autolink_Public", Id);
            }
            catch (Exception ex)
            {
                logError.Info("PublicAutolink: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string UpdateAutolink(Autolink autolink)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_Autolink_Update", autolink.ID, autolink.Keyword, autolink.URL, autolink.SortOrder, autolink.MaxReplacements, autolink.TypeStart);
            }
            catch (Exception ex)
            {
                logError.Info("UpdateAutolink: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

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
    public class Impcms_ImagesCate : Icms_ImagesCate
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_ImagesCate(cms_ImagesCate _ImagesCate)
        {
            try
            {
                SQLServerConnection<cms_ImagesCate> sQLServer = new SQLServerConnection<cms_ImagesCate>();
                return sQLServer.ExecuteNonQuery("SP_cms_ImagesCate_DeleteByPrimaryKey", _ImagesCate.vId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_ImagesCate: " + _ImagesCate.vId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_ImagesCate(int _vId)
        {
            try
            {
                SQLServerConnection<cms_ImagesCate> sQLServer = new SQLServerConnection<cms_ImagesCate>();
                return sQLServer.ExecuteNonQuery("SP_cms_ImagesCate_DeleteByPrimaryKey", _vId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_ImagesCate: " + _vId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_ImagesCate> GetAllcms_ImagesCate()
        {
            try
            {
                SQLServerConnection<cms_ImagesCate> sQLServer = new SQLServerConnection<cms_ImagesCate>();
                return sQLServer.SelectQueryCommand("SP_cms_ImagesCate_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_ImagesCate: " + ex.ToString());
                return null;
            }
        }

        public List<cms_ImagesCate> Getcms_ImagesCateByID(int _vId)
        {
            try
            {
                SQLServerConnection<cms_ImagesCate> sQLServer = new SQLServerConnection<cms_ImagesCate>();
                return sQLServer.SelectQueryCommand("SP_cms_ImagesCate_SelectByPrimaryKey", _vId);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_ImagesCateByID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_ImagesCate> Getcms_ImagesCateByName(string vName)
        {
            try
            {
                SQLServerConnection<cms_ImagesCate> sQLServer = new SQLServerConnection<cms_ImagesCate>();
                return sQLServer.SelectQueryCommand("SP_cms_ImagesCate_SelectByField", "vName", vName);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_ImagesCateByName: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_ImagesCate(cms_ImagesCate _ImagesCate)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteInsert("SP_cms_ImagesCate_Insert", _ImagesCate.vName, _ImagesCate.vTitle, _ImagesCate.vDescription, _ImagesCate.vKeyword, _ImagesCate.vActive);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_ImagesCate: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publiccms_ImagesCate(int _vId)
        {
            try
            {
                SQLServerConnection<cms_ImagesCate> sQLServer = new SQLServerConnection<cms_ImagesCate>();
                return sQLServer.ExecuteNonQuery("SP_cms_ImagesCate_PublicByPrimaryKey", _vId);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_ImagesCate: " + _vId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatecms_ImagesCate(cms_ImagesCate _ImagesCate)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_ImagesCate_Update", _ImagesCate.vId, _ImagesCate.vName, _ImagesCate.vTitle, _ImagesCate.vDescription, _ImagesCate.vKeyword, _ImagesCate.vActive);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_ImagesCate: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

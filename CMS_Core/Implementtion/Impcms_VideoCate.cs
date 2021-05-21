using CMS_Core.Common;
using CMS_Core.Entity;
using CMS_Core.Interface;
using log4net;
using System;
using System.Collections.Generic;

namespace CMS_Core.Implementtion
{
    public class Impcms_VideoCate : Icms_VideoCate
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_VideoCate(cms_VideoCate cms_Video)
        {
            try
            {
                SQLServerConnection<cms_VideoCate> sQLServer = new SQLServerConnection<cms_VideoCate>();
                return sQLServer.ExecuteNonQuery("SP_cms_VideoCate_DeleteByPrimaryKey", cms_Video.vId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_VideoCate: " + cms_Video.vId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_VideoCate(int _vId)
        {
            try
            {
                SQLServerConnection<cms_VideoCate> sQLServer = new SQLServerConnection<cms_VideoCate>();
                return sQLServer.ExecuteNonQuery("SP_cms_VideoCate_DeleteByPrimaryKey", _vId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_VideoCate: " + _vId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_VideoCate> GetAllcms_VideoCate()
        {
            try
            {
                SQLServerConnection<cms_VideoCate> sQLServer = new SQLServerConnection<cms_VideoCate>();
                return sQLServer.SelectQueryCommand("SP_cms_VideoCate_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_VideoCate: " + ex.ToString());
                return null;
            }
        }

        public List<cms_VideoCate> Getcms_VideoCateByID(int vid)
        {
            try
            {
                SQLServerConnection<cms_VideoCate> sQLServer = new SQLServerConnection<cms_VideoCate>();
                return sQLServer.SelectQueryCommand("SP_cms_VideoCate_SelectByPrimaryKey", vid);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_VideoCateByID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_VideoCate> Getcms_VideoCateByName(string vName)
        {
            try
            {
                SQLServerConnection<cms_VideoCate> sQLServer = new SQLServerConnection<cms_VideoCate>();
                return sQLServer.SelectQueryCommand("SP_cms_VideoCate_SelectByField", "vName", vName);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_VideoCateByvName: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_VideoCate(cms_VideoCate cms_Video)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteInsert("SP_cms_VideoCate_Insert", cms_Video.vName, cms_Video.vTitle, cms_Video.vDescription, cms_Video.vKeyword, cms_Video.vActive );
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_VideoCate: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publichcms_VideoCate(int _vId)
        {
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_VideoCate_PublichByPrimaryKey", _vId);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Category: " + _vId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatecms_VideoCate(cms_VideoCate cms_Video)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_VideoCate_Update", cms_Video.vId, cms_Video.vName, cms_Video.vTitle, cms_Video.vDescription, cms_Video.vKeyword, cms_Video.vActive);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Category: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

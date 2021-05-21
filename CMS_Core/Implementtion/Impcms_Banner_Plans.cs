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
    public class Impcms_Banner_Plans : Icms_Banner_Plans
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_Banner_Plans(cms_Banner_Plans _Banner_Plans)
        {
            try
            {
                SQLServerConnection<cms_Banner_Plans> sQLServer = new SQLServerConnection<cms_Banner_Plans>();
                return sQLServer.ExecuteNonQuery("SP_cms_Banner_Plans_DeleteByPrimaryKey", _Banner_Plans.planId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Banner_Plans: " + _Banner_Plans.planId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_Banner_Plans(int planId)
        {
            try
            {
                SQLServerConnection<cms_Banner_Plans> sQLServer = new SQLServerConnection<cms_Banner_Plans>();
                return sQLServer.ExecuteNonQuery("SP_cms_Banner_Plans_DeleteByPrimaryKey", planId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Banner_Plans: " + planId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_Banner_Plans> GetAllcms_Banner_Plans()
        {
            try
            {
                SQLServerConnection<cms_Banner_Plans> sQLServer = new SQLServerConnection<cms_Banner_Plans>();
                return sQLServer.SelectQueryCommand("SP_cms_Banner_Plans_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Banner_Plans: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Banner_Plans> Getcms_Banner_PlansByID(int planId)
        {
            try
            {
                SQLServerConnection<cms_Banner_Plans> sQLServer = new SQLServerConnection<cms_Banner_Plans>();
                return sQLServer.SelectQueryCommand("SP_cms_Banner_Plans_SelectByPrimaryKey", planId);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Banner_PlansByID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Banner_Plans> Getcms_Banner_PlansByName(string planTitle)
        {
            try
            {
                SQLServerConnection<cms_Banner_Plans> sQLServer = new SQLServerConnection<cms_Banner_Plans>();
                return sQLServer.SelectQueryCommand("SP_cms_Banner_Plans_SelectByField", "planTitle", planTitle);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Banner_PlansByName: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_Banner_Plans(cms_Banner_Plans _Banner_Plans)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteInsert("SP_cms_Banner_Plans_Insert", _Banner_Plans.planTitle, _Banner_Plans.planDescription, _Banner_Plans.planForm, _Banner_Plans.width, _Banner_Plans.height, _Banner_Plans.lang, _Banner_Plans.active);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Banner_Plans: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publiccms_Banner_Plans(int planId)
        {
            try
            {
                SQLServerConnection<cms_Banner_Plans> sQLServer = new SQLServerConnection<cms_Banner_Plans>();
                return sQLServer.ExecuteNonQuery("SP_cms_Banner_Plans_PublicByPrimaryKey", planId);                
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Banner_Plans: " + ex.ToString());
                return null;
            }
        }

        public string Updatecms_Banner_Plans(cms_Banner_Plans _Banner_Plans)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_Banner_Plans_Update", _Banner_Plans.planId, _Banner_Plans.planTitle, _Banner_Plans.planDescription, _Banner_Plans.planForm, _Banner_Plans.width, _Banner_Plans.height, _Banner_Plans.lang, _Banner_Plans.active);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Banner_Plans: " + ex.ToString());
                return result = string.Empty;
            }
        }

       

    }
}

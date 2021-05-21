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
    public class Impcms_Scientist : Icms_Scientist
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_Scientist(cms_Scientist _cms_Scientist)
        {
            try
            {
                SQLServerConnection<cms_Scientist> sQLServer = new SQLServerConnection<cms_Scientist>();
                return sQLServer.ExecuteNonQuery("cms_Scientist_DeleteByPrimaryKey", _cms_Scientist.ID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Scientist: " + _cms_Scientist.ID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_Scientist(int ID)
        {
            try
            {
                SQLServerConnection<cms_Scientist> sQLServer = new SQLServerConnection<cms_Scientist>();
                return sQLServer.ExecuteNonQuery("cms_Scientist_DeleteByPrimaryKey", ID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Scientist: " + ID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_Scientist> GetAllcms_Scientist()
        {
            try
            {
                SQLServerConnection<cms_Scientist> sQLServer = new SQLServerConnection<cms_Scientist>();
                return sQLServer.SelectQueryCommand("cms_Scientist_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Scientist: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Scientist> Getcms_Scientist(string Tungay, string Denngay,  int userId, int status, string keyword)
        {
            try
            {
                SQLServerConnection<cms_Scientist> sQLServer = new SQLServerConnection<cms_Scientist>();
                return sQLServer.SelectQueryCommand("cms_Scientist_SelectPara", Tungay, Denngay, userId, status, keyword);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Scientist: " + ex.ToString());
                return null;
            }
        }


        public List<cms_Scientist> Getcms_ScientistByID(int ID)
        {
            try
            {
                SQLServerConnection<cms_Scientist> sQLServer = new SQLServerConnection<cms_Scientist>();
                return sQLServer.SelectQueryCommand("cms_Scientist_SelectByPrimaryKey", ID);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Scientist_CateByID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Scientist> Getcms_ScientistByName(string ScientistTitle)
        {
            try
            {
                SQLServerConnection<cms_Scientist> sQLServer = new SQLServerConnection<cms_Scientist>();
                return sQLServer.SelectQueryCommand("cms_Scientist_SelectByName", ScientistTitle);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_ScientistByName: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_Scientist(cms_Scientist _cms_Scientist)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Scientist> sQLServer = new SQLServerConnection<cms_Scientist>();
                return sQLServer.ExecuteInsert("cms_Scientist_Insert", _cms_Scientist.ScientistTitle, _cms_Scientist.DatePerform, _cms_Scientist.PersionPerform, _cms_Scientist.UnitPerform, _cms_Scientist.CommissioningCommittee, _cms_Scientist.scientistDesc, _cms_Scientist.newsImages, _cms_Scientist.Slug, _cms_Scientist.newsContent, _cms_Scientist.userId , _cms_Scientist.newsKeyword, _cms_Scientist.ScientistCae);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Banner_Plans: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publiccms_Scientist(int ID)
        {
            try
            {
                SQLServerConnection<cms_Scientist> sQLServer = new SQLServerConnection<cms_Scientist>();
                return sQLServer.ExecuteNonQuery("SP_cms_Scientist_PublicByPrimaryKey", ID);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Scientist: " + ex.ToString());
                return null;
            }
        }

        public string Updatecms_Scientist(cms_Scientist _cms_Scientist)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("cms_Scientist_Update", _cms_Scientist.ID, _cms_Scientist.ScientistTitle, _cms_Scientist.DatePerform, _cms_Scientist.PersionPerform, _cms_Scientist.UnitPerform, _cms_Scientist.CommissioningCommittee, _cms_Scientist.scientistDesc, _cms_Scientist.newsImages, _cms_Scientist.Slug, _cms_Scientist.newsContent, _cms_Scientist.userId, _cms_Scientist.newsKeyword, _cms_Scientist.ScientistCae);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Scientist: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

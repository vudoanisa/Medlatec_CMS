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
    public class Impcms_Scientist_Cate : Icms_Scientist_Cate
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");

        public string Deletecms_Scientist_Cate(cms_Scientist_Cate _cms_Scientist_Cate)
        {
            try
            {
                SQLServerConnection<cms_Scientist_Cate> sQLServer = new SQLServerConnection<cms_Scientist_Cate>();
                return sQLServer.ExecuteNonQuery("cms_Scientist_Cate_DeleteByPrimaryKey", _cms_Scientist_Cate.ID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Scientist_Cate: " + _cms_Scientist_Cate.ID  + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_Scientist_Cate(int ID)
        {
            try
            {
                SQLServerConnection<cms_Scientist_Cate> sQLServer = new SQLServerConnection<cms_Scientist_Cate>();
                return sQLServer.ExecuteNonQuery("cms_Scientist_Cate_DeleteByPrimaryKey", ID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Scientist_Cate: " + ID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_Scientist_Cate> GetAllcms_Scientist_Cate()
        {
            try
            {
                SQLServerConnection<cms_Scientist_Cate> sQLServer = new SQLServerConnection<cms_Scientist_Cate>();
                return sQLServer.SelectQueryCommand("cms_Scientist_Cate_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Scientist_Cate: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Scientist_Cate> GetAllcms_Scientist_CateActive()
        {
            try
            {
                SQLServerConnection<cms_Scientist_Cate> sQLServer = new SQLServerConnection<cms_Scientist_Cate>();
                return sQLServer.SelectQueryCommand("cms_Scientist_Cate_SelectActive");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Scientist_Cate: " + ex.ToString());
                return null;
            }
        }



        public List<cms_Scientist_Cate> Getcms_Scientist_CateByID(int ID)
        {
            try
            {
                SQLServerConnection<cms_Scientist_Cate> sQLServer = new SQLServerConnection<cms_Scientist_Cate>();
                return sQLServer.SelectQueryCommand("cms_Scientist_Cate_SelectByPrimaryKey", ID);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Scientist_CateByID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Scientist_Cate> Getcms_Scientist_CateByName(string ScientistTitle)
        {
            try
            {
                SQLServerConnection<cms_Scientist_Cate> sQLServer = new SQLServerConnection<cms_Scientist_Cate>();
                return sQLServer.SelectQueryCommand("cms_Scientist_Cate_SelectByName", ScientistTitle);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Scientist_CateByName: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_Scientist_Cate(cms_Scientist_Cate _cms_Scientist_Cate)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Scientist_Cate> sQLServer = new SQLServerConnection<cms_Scientist_Cate>();
                return sQLServer.ExecuteInsert("cms_Scientist_Cate_Insert", _cms_Scientist_Cate.ScientistTitle, _cms_Scientist_Cate.ScientistDesc, _cms_Scientist_Cate.userId );
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Banner_Plans: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publiccms_Scientist_Cate(int ID)
        {
            try
            {
                SQLServerConnection<cms_Banner_Plans> sQLServer = new SQLServerConnection<cms_Banner_Plans>();
                return sQLServer.ExecuteNonQuery("SP_cms_Scientist_Cate_PublicByPrimaryKey", ID);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Scientist_Cate: " + ex.ToString());
                return null;
            }
        }

        public string Updatecms_Scientist_Cate(cms_Scientist_Cate _cms_Scientist_Cate)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Banner_Plans> sQLServer = new SQLServerConnection<cms_Banner_Plans>();
                return sQLServer.ExecuteNonQuery("cms_Scientist_Cate_update", _cms_Scientist_Cate.ID, _cms_Scientist_Cate.ScientistTitle, _cms_Scientist_Cate.ScientistDesc, _cms_Scientist_Cate.userId);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Scientist_Cate: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

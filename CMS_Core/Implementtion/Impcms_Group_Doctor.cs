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
    public class Impcms_Group_Doctor : Icms_Group_Doctor
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_Group_Doctor(cms_Group_Doctor group_Doctor)
        {
            try
            {
                SQLServerConnection<cms_Group_Doctor> sQLServer = new SQLServerConnection<cms_Group_Doctor>();
                return sQLServer.ExecuteNonQuery("SP_cms_Group_Doctor_Delete", group_Doctor.id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Group_Doctor: " + group_Doctor.id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_Group_Doctor(int Id)
        {
            try
            {
                SQLServerConnection<cms_Group_Doctor> sQLServer = new SQLServerConnection<cms_Group_Doctor>();
                return sQLServer.ExecuteNonQuery("SP_cms_Group_Doctor_Delete", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Group_Doctor: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_Group_Doctor> GetAllcms_Group_Doctor()
        {
            try
            {
                SQLServerConnection<cms_Group_Doctor> sQLServer = new SQLServerConnection<cms_Group_Doctor>();
                return sQLServer.SelectQueryCommand("SP_cms_Group_Doctor_SelectALL");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Group_Doctor: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Group_Doctor> GetAllcms_Group_DoctorActive()
        {
            try
            {
                SQLServerConnection<cms_Group_Doctor> sQLServer = new SQLServerConnection<cms_Group_Doctor>();
                return sQLServer.SelectQueryCommand("SP_cms_Group_Doctor_SelectActive");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Group_Doctor: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Group_Doctor> Getcms_Group_DoctorByID(int Id)
        {
            try
            {
                SQLServerConnection<cms_Group_Doctor> sQLServer = new SQLServerConnection<cms_Group_Doctor>();
                return sQLServer.SelectQueryCommand("SP_cms_Group_Doctor_SelectByID", Id);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Group_Doctor: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Group_Doctor> Getcms_Group_DoctorByName(string Name)
        {
            try
            {
                SQLServerConnection<cms_Group_Doctor> sQLServer = new SQLServerConnection<cms_Group_Doctor>();
                return sQLServer.SelectQueryCommand("SP_cms_Group_Doctor_SelectByName", Name);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Group_Doctor: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_Group_Doctor(cms_Group_Doctor group_Doctor)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Group_Doctor> sQLServer = new SQLServerConnection<cms_Group_Doctor>();
                return sQLServer.ExecuteInsert("SP_cms_Group_Doctor_Insert", group_Doctor.Name, group_Doctor.Image, group_Doctor.Desc, group_Doctor.keyword, group_Doctor.ord);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Group_Doctor: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publiccms_Group_Doctor(int Id)
        {
            try
            {
                SQLServerConnection<cms_Group_Doctor> sQLServer = new SQLServerConnection<cms_Group_Doctor>();
                return sQLServer.ExecuteNonQuery("SP_cms_Group_Doctor_Publich", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Group_Doctor: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatecms_Group_Doctor(cms_Group_Doctor group_Doctor)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Group_Doctor> sQLServer = new SQLServerConnection<cms_Group_Doctor>();
                return sQLServer.ExecuteNonQuery("SP_cms_Group_Doctor_Update", group_Doctor.id, group_Doctor.Name, group_Doctor.Image, group_Doctor.Desc, group_Doctor.keyword, group_Doctor.ord);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Group_Doctor: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

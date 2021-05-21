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
    public class Impcms_Doctor : Icms_Doctor
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_Doctor(cms_Doctor _Doctor)
        {
            try
            {
                SQLServerConnection<cms_Doctor> sQLServer = new SQLServerConnection<cms_Doctor>();
                return sQLServer.ExecuteNonQuery("SP_cms_Doctor_Delete", _Doctor.id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Group_Doctor: " + _Doctor.id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_Doctor(int Id)
        {
            try
            {
                SQLServerConnection<cms_Doctor> sQLServer = new SQLServerConnection<cms_Doctor>();
                return sQLServer.ExecuteNonQuery("SP_cms_Doctor_Delete", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Group_Doctor: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_Doctor> GetAllcms_Doctor()
        {
            try
            {
                SQLServerConnection<cms_Doctor> sQLServer = new SQLServerConnection<cms_Doctor>();
                return sQLServer.SelectQueryCommand("SP_cms_Doctor_SelectByCateID",0);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Doctor: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Doctor> GetAllcms_DoctorActive()
        {
            try
            {
                SQLServerConnection<cms_Doctor> sQLServer = new SQLServerConnection<cms_Doctor>();
                return sQLServer.SelectQueryCommand("SP_cms_Doctor_SelectByActive");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_DoctorActive: " + ex.ToString());
                return null;
            }
        }


        public List<cms_Doctor> Getcms_DoctorByCateID(int CateID)
        {
            try
            {
                SQLServerConnection<cms_Doctor> sQLServer = new SQLServerConnection<cms_Doctor>();
                return sQLServer.SelectQueryCommand("SP_cms_Doctor_SelectByCateID", CateID);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_DoctorByCateID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Doctor> Getcms_DoctorByID(int Id)
        {
            try
            {
                SQLServerConnection<cms_Doctor> sQLServer = new SQLServerConnection<cms_Doctor>();
                return sQLServer.SelectQueryCommand("SP_cms_Doctor_SelectByID", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_DoctorByCateID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Doctor> Getcms_DoctorByName(string Name)
        {
            try
            {
                SQLServerConnection<cms_Doctor> sQLServer = new SQLServerConnection<cms_Doctor>();
                return sQLServer.SelectQueryCommand("SP_cms_Doctor_SelectByName", Name);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_DoctorByName: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_Doctor(cms_Doctor _Doctor)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Doctor> sQLServer = new SQLServerConnection<cms_Doctor>();
                return sQLServer.ExecuteInsert("SP_cms_Doctor_Insert", _Doctor.DoctorName, _Doctor.DoctorCate, _Doctor.DoctorAddress, _Doctor.DoctorImage, _Doctor.DoctorEmail, _Doctor.DoctorMobile, _Doctor.DoctorWorkPlace, _Doctor.DoctorInfo, _Doctor.DoctorPosition, _Doctor.DoctorExperience, _Doctor.DoctorAwards, _Doctor.DoctorResearchWorks, _Doctor.DoctorServices, _Doctor.DoctorTraining, _Doctor.DoctorOrganization, _Doctor.DoctorLanguage, _Doctor.DoctorOrd, _Doctor.cateIdList);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Group_Doctor: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publiccms_Doctor(int Id)
        {
            try
            {
                SQLServerConnection<cms_Doctor> sQLServer = new SQLServerConnection<cms_Doctor>();
                return sQLServer.ExecuteNonQuery("SP_cms_Doctor_Public", Id);
            }
            catch (Exception ex)
            {
                logError.Info("SP_cms_Doctor_Public: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatecms_Doctor(cms_Doctor _Doctor)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Doctor> sQLServer = new SQLServerConnection<cms_Doctor>();
                return sQLServer.ExecuteNonQuery("SP_cms_Doctor_Update", _Doctor.id, _Doctor.DoctorName, _Doctor.DoctorCate, _Doctor.DoctorAddress, _Doctor.DoctorImage, _Doctor.DoctorEmail, _Doctor.DoctorMobile, _Doctor.DoctorWorkPlace, _Doctor.DoctorInfo, _Doctor.DoctorPosition, _Doctor.DoctorExperience, _Doctor.DoctorAwards, _Doctor.DoctorResearchWorks, _Doctor.DoctorServices, _Doctor.DoctorTraining, _Doctor.DoctorOrganization, _Doctor.DoctorLanguage, _Doctor.DoctorOrd, _Doctor.cateIdList);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Doctor: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

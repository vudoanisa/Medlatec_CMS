using CMS_Core.Common;
using CMS_Core.Entity;
using CMS_Core.Interface;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Implementtion
{
    public class Impcms_Images : Icms_Images
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_Images(cms_Images _Images)
        {
            try
            {
                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.ExecuteNonQuery("SP_cms_Images_DeleteByPrimaryKey", _Images.videoId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Images: " + _Images.videoId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_Images(int videoId)
        {
            try
            {
                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.ExecuteNonQuery("SP_cms_Images_DeleteByPrimaryKey", videoId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Images: " + videoId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_Images> GetAllcms_Images()
        {
            try
            {
                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.SelectQueryCommand("SP_cms_Images_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Images: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Images> GetAllcms_ImagesByImageCate(CMS_Core.Models.SearchNewsViewModel searchNewsView)
        {
            try
            {
                DateTime Tungay = new DateTime();
                DateTime Denngay = new DateTime();

                #region Check input data
                try
                {
                    if (!string.IsNullOrEmpty(searchNewsView.tungay.Trim()))
                        Tungay = DateTime.ParseExact(searchNewsView.tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                    if (!string.IsNullOrEmpty(searchNewsView.denngay.Trim()))
                        Denngay = DateTime.ParseExact(searchNewsView.denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                    if (string.IsNullOrEmpty(searchNewsView.keyword))
                        searchNewsView.keyword = string.Empty;
                }
                catch
                {
                }
                #endregion

                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.SelectQueryCommand("SP_cms_Images_SelectByImageCate",Tungay, Denngay.AddDays(1), searchNewsView.keyword, searchNewsView.ParrenId);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_ImagesByImageCate: " + ex.ToString());
                return null;
            }
        }


        public List<cms_Images> GetAllcms_ImagesByVID( string vid, string videoParrentID)
        {
            try
            {               
                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.SelectQueryCommand("SP_cms_Images_SelectByVID_New", vid, videoParrentID);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_ImagesByVID: " + ex.ToString());
                return null;
            }
        }


        public List<cms_Images> Getcms_ImagesByID(int videoId)
        {
            try
            {
                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.SelectQueryCommand("SP_cms_Images_SelectByPrimaryKey", videoId);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_ImagesByID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Images> Getcms_ImagesByName(string VideoName)
        {
            try
            {
                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.SelectQueryCommand("SP_cms_Images_SelectByField", "VideoName", VideoName);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_ImagesByName: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_Images(cms_Images _Images)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteInsert("SP_cms_Images_Insert_new", _Images.VideoName, _Images.VideoTitle, _Images.VideoDescription, _Images.VideoType, _Images.VideoFile, _Images.VideoCode, _Images.VideoImageThumb, _Images.VideoWidth, _Images.VideoHight, _Images.vId, _Images.VideoActive, _Images.Delegate, _Images.videoParrentID);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Images: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publiccms_Images(int videoId)
        {
            try
            {
                SQLServerConnection<cms_Images> sQLServer = new SQLServerConnection<cms_Images>();
                return sQLServer.ExecuteNonQuery("SP_cms_Images_PublicByPrimaryKey", videoId);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Images: " + videoId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatecms_Images(cms_Images _Images)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_Images_Update", _Images.videoId, _Images.VideoName, _Images.VideoTitle, _Images.VideoDescription, _Images.VideoType, _Images.VideoFile, _Images.VideoCode, _Images.VideoImageThumb, _Images.VideoWidth, _Images.VideoHight, _Images.vId, _Images.VideoActive);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Images: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Updatecms_ImagesChild(cms_Images _Images)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_Images_Update_Child", _Images.videoId,  _Images.VideoTitle, _Images.VideoDescription);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_ImagesChild: " + ex.ToString());
                return result = string.Empty;
            }
        }

    }
}

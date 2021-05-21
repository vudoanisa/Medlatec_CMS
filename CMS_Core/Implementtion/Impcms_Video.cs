using CMS_Core.Common;
using CMS_Core.Entity;
using CMS_Core.Interface;
using CMS_Core.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Implementtion
{
    public class Impcms_Video : Icms_Video
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_Video(cms_Video _Video)
        {
            try
            {
                SQLServerConnection<cms_Video> sQLServer = new SQLServerConnection<cms_Video>();
                return sQLServer.ExecuteNonQuery("SP_cms_Video_DeleteByPrimaryKey", _Video.videoId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Video: " + _Video.videoId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_Video(int videoId)
        {
            try
            {
                SQLServerConnection<cms_Video> sQLServer = new SQLServerConnection<cms_Video>();
                return sQLServer.ExecuteNonQuery("SP_cms_Video_DeleteByPrimaryKey", videoId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Video: " + videoId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_Video> GetAllcms_Video()
        {
            try
            {
                SQLServerConnection<cms_Video> sQLServer = new SQLServerConnection<cms_Video>();
                return sQLServer.SelectQueryCommand("SP_cms_Video_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Video: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Video> GetAllcms_VideoByCateVideo()
        {
            try
            {
                SQLServerConnection<cms_Video> sQLServer = new SQLServerConnection<cms_Video>();
                return sQLServer.SelectQueryCommand("SP_cms_Video_SelectByVideoCate");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Video: " + ex.ToString());
                return null;
            }
        }


        public List<cms_Video> Getcms_VideoByID(int videoId)
        {
            try
            {
                SQLServerConnection<cms_Video> sQLServer = new SQLServerConnection<cms_Video>();
                return sQLServer.SelectQueryCommand("SP_cms_Video_SelectByPrimaryKey", videoId);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_VideoByID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Video> Getcms_VideoByVideoCate(SearchNewsViewModel searchNewsViewModel)
        {
            try
            {
                DateTime Tungay = new DateTime();
                DateTime Denngay = new DateTime();

                #region Check input data
                try
                {
                    if (!string.IsNullOrEmpty(searchNewsViewModel.tungay.Trim()))
                        Tungay = DateTime.ParseExact(searchNewsViewModel.tungay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                    if (!string.IsNullOrEmpty(searchNewsViewModel.denngay.Trim()))
                        Denngay = DateTime.ParseExact(searchNewsViewModel.denngay.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                    if (string.IsNullOrEmpty(searchNewsViewModel.keyword))
                        searchNewsViewModel.keyword = string.Empty;
                }
                catch
                {
                }
                #endregion


                SQLServerConnection<cms_Video> sQLServer = new SQLServerConnection<cms_Video>();
                return sQLServer.SelectQueryCommand("SP_cms_Video_SelectByVideoCate", Tungay, Denngay.AddDays(1), searchNewsViewModel.keyword, searchNewsViewModel.ParrenId);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_VideoByVideoCate: " + ex.ToString());
                return null;
            }


        }

        public List<cms_Video> Getcms_VideoByVideoName(string VideoName)
        {
            try
            {
                SQLServerConnection<cms_Video> sQLServer = new SQLServerConnection<cms_Video>();
                return sQLServer.SelectQueryCommand("SP_cms_Video_SelectByField", "VideoName", VideoName);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_VideoByVideoName: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_Video(cms_Video _Video)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteInsert("SP_cms_Video_Insert", _Video.VideoName, _Video.VideoTitle, _Video.VideoDescription, _Video.VideoType, _Video.VideoFile, _Video.VideoCode, _Video.VideoImageThumb, _Video.VideoHight, _Video.VideoWidth, _Video.vId, _Video.VideoActive, _Video.linkvideo, _Video.VideoFileWeb);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Video: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publiccms_Video(int videoId)
        {
            try
            {
                SQLServerConnection<cms_Video> sQLServer = new SQLServerConnection<cms_Video>();
                return sQLServer.ExecuteNonQuery("SP_cms_Video_PublicByPrimaryKey", videoId);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Video: " + videoId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatecms_Video(cms_Video _Video)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Category> sQLServer = new SQLServerConnection<cms_Category>();
                return sQLServer.ExecuteNonQuery("SP_cms_Video_Update", _Video.videoId, _Video.VideoName, _Video.VideoTitle, _Video.VideoDescription, _Video.VideoType, _Video.VideoFile, _Video.VideoCode, _Video.VideoImageThumb, _Video.VideoHight, _Video.VideoWidth, _Video.vId, _Video.VideoActive, _Video.linkvideo, _Video.VideoFileWeb);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Category: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

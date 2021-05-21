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
    public class Impcms_Banner_rows : Icms_Banner_rows
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_Banner_rows(int bannerId)
        {
            try
            {
                SQLServerConnection<cms_Banner_rows> sQLServer = new SQLServerConnection<cms_Banner_rows>();
                return sQLServer.ExecuteNonQuery("SP_cms_Banner_rows_DeleteByPrimaryKey", bannerId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Banner_rows: " + bannerId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletecms_Video(cms_Banner_rows _Banner_Rows)
        {
            try
            {
                SQLServerConnection<cms_Banner_rows> sQLServer = new SQLServerConnection<cms_Banner_rows>();
                return sQLServer.ExecuteNonQuery("SP_cms_Banner_rows_DeleteByPrimaryKey", _Banner_Rows.bannerId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Banner_rows: " + _Banner_Rows.bannerId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<cms_Banner_rows> GetAllcms_Banner_rows()
        {
            try
            {
                SQLServerConnection<cms_Banner_rows> sQLServer = new SQLServerConnection<cms_Banner_rows>();
                return sQLServer.SelectQueryCommand("SP_cms_Banner_rows_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_Banner_rows: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Banner_rows> Getcms_Banner_rowsByBannerCate(SearchNewsViewModel searchNewsViewModel)
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

                SQLServerConnection<cms_Banner_rows> sQLServer = new SQLServerConnection<cms_Banner_rows>();
                return sQLServer.SelectQueryCommand("SP_cms_Banner_rows_SelectByBanner_Plans", Tungay,Denngay.AddDays(1), searchNewsViewModel.keyword, searchNewsViewModel.ParrenId);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Banner_rowsByBannerCate: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Banner_rows> Getcms_Banner_rowsByID(int bannerId)
        {
            try
            {
                SQLServerConnection<cms_Banner_rows> sQLServer = new SQLServerConnection<cms_Banner_rows>();
                return sQLServer.SelectQueryCommand("SP_cms_Banner_rows_SelectByPrimaryKey", bannerId);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Banner_rowsByID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Banner_rows> Getcms_Banner_rowsByTitle(string title)
        {
            try
            {
                SQLServerConnection<cms_Banner_rows> sQLServer = new SQLServerConnection<cms_Banner_rows>();
                return sQLServer.SelectQueryCommand("SP_cms_Banner_rows_SelectByField", "title", title);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Banner_rowsByTitle: " + ex.ToString());
                return null;
            }
        }

        public string Insertcms_Banner_rows(cms_Banner_rows _Banner_Rows)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Banner_rows> sQLServer = new SQLServerConnection<cms_Banner_rows>();
                return sQLServer.ExecuteInsert("SP_cms_Banner_rows_Insert", _Banner_Rows.planId, _Banner_Rows.title, _Banner_Rows.fileName, _Banner_Rows.fileMine, _Banner_Rows.width, _Banner_Rows.height, _Banner_Rows.clickUrl, _Banner_Rows.addTime, _Banner_Rows.expTime, _Banner_Rows.ord, _Banner_Rows.active);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Banner_rows: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publiccms_Banner_rows(int bannerId)
        {
            try
            {
                SQLServerConnection<cms_Banner_rows> sQLServer = new SQLServerConnection<cms_Banner_rows>();
                return sQLServer.ExecuteNonQuery("SP_cms_Banner_rows_PublicByPrimaryKey", bannerId);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Banner_rows: " + bannerId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatecms_Banner_rows(cms_Banner_rows _Banner_Rows)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Banner_rows> sQLServer = new SQLServerConnection<cms_Banner_rows>();
                return sQLServer.ExecuteNonQuery("SP_cms_Banner_rows_Update", _Banner_Rows.bannerId,  _Banner_Rows.planId, _Banner_Rows.title, _Banner_Rows.fileName, _Banner_Rows.fileMine, _Banner_Rows.width, _Banner_Rows.height, _Banner_Rows.clickUrl, _Banner_Rows.addTime, _Banner_Rows.expTime, _Banner_Rows.ord, _Banner_Rows.active);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Banner_rows: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

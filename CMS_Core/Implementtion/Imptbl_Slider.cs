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
    public class Imptbl_Slider : Itbl_Slider
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletetbl_slider(tbl_slider _slider)
        {
            try
            {
                SQLServerConnection<tbl_slider> sQLServer = new SQLServerConnection<tbl_slider>();
                return sQLServer.ExecuteNonQuery("tbl_Slider_DeleteByPrimaryKey", _slider.ID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_slider: " + _slider.ID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<tbl_slider> Gettbl_sliderByStatus( int status, int type, string keyword)
        {
            try
            {
                SQLServerConnection<tbl_slider> sQLServer = new SQLServerConnection<tbl_slider>();
                return sQLServer.SelectQueryCommand("SP_tbl_Slider_SelectSTATUS", status, type, keyword);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_sliderByStatus: " + ex.ToString());
                return null;
            }
        }

        public string Deletetbl_slider(int Id)
        {
            try
            {
                SQLServerConnection<tbl_slider> sQLServer = new SQLServerConnection<tbl_slider>();
                return sQLServer.ExecuteNonQuery("tbl_Slider_DeleteByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_slider: " + Id  + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<tbl_slider> GetAlltbl_Slider()
        {
            throw new NotImplementedException();
        }

        public List<tbl_slider> Gettbl_SliderByID(int ID)
        {
            try
            {
                SQLServerConnection<tbl_slider> sQLServer = new SQLServerConnection<tbl_slider>();
                return sQLServer.SelectQueryCommand("tbl_Slider_SelectByPrimaryKey", ID);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_SliderByID: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_slider> Gettbl_sliderByName(string sliderName)
        {
            try
            {
                SQLServerConnection<tbl_slider> sQLServer = new SQLServerConnection<tbl_slider>();
                return sQLServer.SelectQueryCommand("tbl_Slider_SelectByName", sliderName);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_sliderByName: " + ex.ToString());
                return null;
            }
        }

        public string Inserttbl_slider(tbl_slider _slider)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_slider> sQLServer = new SQLServerConnection<tbl_slider>();
                return sQLServer.ExecuteInsert("tbl_Slider_insert", _slider.sliderTitle, _slider.sliderUrl, _slider.sliderImage, _slider.createby, _slider.sliderImageWeb);
            }
            catch (Exception ex)
            {
                logError.Info("Inserttbl_slider: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publictbl_slider(int Id)
        {
            try
            {
                SQLServerConnection<tbl_slider> sQLServer = new SQLServerConnection<tbl_slider>();
                return sQLServer.ExecuteNonQuery("SP_sliderUrl_PublicByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Publictbl_slider: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatetbl_slider(tbl_slider _slider)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_slider> sQLServer = new SQLServerConnection<tbl_slider>();
                return sQLServer.ExecuteNonQuery("tbl_Slider_update", _slider.ID, _slider.sliderTitle, _slider.sliderUrl, _slider.sliderImage, _slider.udateby,_slider.sliderImageWeb);
            }
            catch (Exception ex)
            {
                logError.Info("Updatetbl_slider: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

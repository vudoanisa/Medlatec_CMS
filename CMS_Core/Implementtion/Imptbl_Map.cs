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
    public class Imptbl_Map : Itbl_Map
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletetbl_Map(tbl_Map _Map)
        {
            try
            {
                SQLServerConnection<tbl_Map> sQLServer = new SQLServerConnection<tbl_Map>();
                return sQLServer.ExecuteNonQuery("tbl_Map_DeleteByPrimaryKey", _Map.ID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_Map: " + _Map.ID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletetbl_Map(int Id)
        {
            try
            {
                SQLServerConnection<tbl_Map> sQLServer = new SQLServerConnection<tbl_Map>();
                return sQLServer.ExecuteNonQuery("tbl_Map_DeleteByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_Map: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<tbl_Map> GetAlltbl_Map()
        {
            throw new NotImplementedException();
        }

        public List<tbl_Map> Gettbl_MapByID(int ID)
        {
            try
            {
                SQLServerConnection<tbl_Map> sQLServer = new SQLServerConnection<tbl_Map>();
                return sQLServer.SelectQueryCommand("SP_tbl_Map_ByID", ID);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_MapByID: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_Map> Gettbl_MapByName(string Mapname)
        {
            try
            {
                SQLServerConnection<tbl_Map> sQLServer = new SQLServerConnection<tbl_Map>();
                return sQLServer.SelectQueryCommand("SP_tbl_Map_ByName", Mapname);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_MapByName: " + ex.ToString());
                return null;
            }
        }
         
        public string Inserttbl_Map(tbl_Map _Map)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_Map> sQLServer = new SQLServerConnection<tbl_Map>();
                return sQLServer.ExecuteInsert("tbl_Map_Insert", _Map.TenPK, _Map.Diachi, _Map.Img, _Map.Giamdoc, _Map.Hotline, _Map.Website,_Map.Fanpage,_Map.Lo, _Map.Lu, _Map.Truso, _Map.Khuvuc, _Map.Thongso,1, _Map.createby, _Map.type);
            }
            catch (Exception ex)
            {
                logError.Info("Inserttbl_Map: " + ex.ToString());
                return result = string.Empty;
            }
        }


        public List<tbl_Map> Gettbl_MapByStatus(int typeCompay, int status, int type, string keyword)
        {
            try
            {
                SQLServerConnection<tbl_Map> sQLServer = new SQLServerConnection<tbl_Map>();
                return sQLServer.SelectQueryCommand("SP_tbl_Map_SelectSTATUS", typeCompay, status, type, keyword);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_MapByStatus: " + ex.ToString());
                return null;
            }
        }

        public string Publictbl_Map(int Id)
        {
            try
            {
                SQLServerConnection<tbl_Map> sQLServer = new SQLServerConnection<tbl_Map>();
                return sQLServer.ExecuteNonQuery("SP_tbl_Map_PublicByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Publictbl_slider: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatetbl_Map(tbl_Map _Map)
        {
            string result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(_Map.Lo))
                    _Map.Lo = string.Empty;
                if (string.IsNullOrEmpty(_Map.Lu))
                    _Map.Lu = string.Empty;
                SQLServerConnection<tbl_Map> sQLServer = new SQLServerConnection<tbl_Map>();
                return sQLServer.ExecuteNonQuery("tbl_Map_update", _Map.ID, _Map.TenPK, _Map.Diachi, _Map.Img, _Map.Giamdoc, _Map.Hotline, _Map.Website, _Map.Fanpage, _Map.Lo, _Map.Lu, _Map.Truso, _Map.Khuvuc, _Map.Thongso, _Map.updateby, _Map.type);
            }
            catch (Exception ex)
            {
                logError.Info("Updatetbl_Map: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

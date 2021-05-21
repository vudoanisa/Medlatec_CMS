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
    public class Imptbl_VoteCate : Itbl_VoteCate
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletetbl_VoteCate(tbl_VoteCate VoteCate)
        {
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.ExecuteNonQuery("tbl_VoteCate_DeleteByPrimaryKey", VoteCate.ID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_VoteCate: " + VoteCate.ID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletetbl_VoteCate(int Id)
        {
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.ExecuteNonQuery("tbl_VoteCate_DeleteByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_VoteCate: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<tbl_VoteCate> GetAlltbl_VoteCate()
        {
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.SelectQueryCommand("tbl_VoteCate_SelectAll" );
            }
            catch (Exception ex)
            {
                logError.Info("GetAlltbl_VoteCate: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_VoteCate> Gettbl_VoteCateByID(int Id)
        {
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.SelectQueryCommand("tbl_VoteCate_SelectByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_VoteCateByID: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_VoteCate> Gettbl_VoteCateByName(string Name)
        {
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.SelectQueryCommand("tbl_VoteCate_SelectByName", Name);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_VoteCateByName: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_VoteCate> Gettbl_VoteCateByStatus(int status, int type, string keyword)
        {
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.SelectQueryCommand("tbl_VoteCate_SelectSTATUS", status, type, keyword);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_VoteCateByStatus: " + ex.ToString());
                return null;
            }
        }
       
        public string Inserttbl_VoteCate(tbl_VoteCate VoteCate)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.ExecuteInsert("tbl_VoteCate_Insert", VoteCate.VoteCate, VoteCate.VoteCateDesc, VoteCate.createby, VoteCate.ImagePath, VoteCate.CodeHIS, VoteCate.CodeName, VoteCate.datesart, VoteCate.dataend);
            }
            catch (Exception ex)
            {
                logError.Info("Inserttbl_VoteCate: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publictbl_VoteCate(int Id)
        {
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.ExecuteNonQuery("tbl_VoteCate_PublicByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Publictbl_VoteCate: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatetbl_VoteCate(tbl_VoteCate VoteCate)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.ExecuteNonQuery("tbl_VoteCate_Update", VoteCate.ID, VoteCate.VoteCate, VoteCate.VoteCateDesc, VoteCate.updateby, VoteCate.ImagePath, VoteCate.CodeHIS, VoteCate.CodeName, VoteCate.datesart, VoteCate.dataend);
            }
            catch (Exception ex)
            {
                logError.Info("Updatetbl_VoteCate: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

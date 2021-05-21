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
    public class Imptbl_TestCodecs : Itbl_TestCode
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletetbl_TestCode(tbl_TestCode TestCode)
        {
            try
            {
                SQLServerConnection<tbl_TestCode> sQLServer = new SQLServerConnection<tbl_TestCode>();
                return sQLServer.ExecuteNonQuery("tbl_TestCode_DeleteByPrimaryKey", TestCode.ID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_TestCode: " + TestCode.ID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletetbl_TestCode(int TestCodeID)
        {
            try
            {
                SQLServerConnection<tbl_TestCode> sQLServer = new SQLServerConnection<tbl_TestCode>();
                return sQLServer.ExecuteNonQuery("tbl_TestCode_DeleteByPrimaryKey", TestCodeID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_TestCode: " + TestCodeID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<tbl_TestCode> GetAlltbl_TestCode()
        {
            throw new NotImplementedException();
        }

        public List<tbl_TestCode> Gettbl_TestCodeByID(int ID)
        {
            try
            {
                SQLServerConnection<tbl_TestCode> sQLServer = new SQLServerConnection<tbl_TestCode>();
                return sQLServer.SelectQueryCommand("tbl_TestCode_SelectByPrimaryKey", ID);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_TestCodeByID: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_TestCode> Gettbl_TestCodeByName(string sliderName)
        {
            try
            {
                SQLServerConnection<tbl_TestCode> sQLServer = new SQLServerConnection<tbl_TestCode>();
                return sQLServer.SelectQueryCommand("tbl_TestCode_SelectByName", sliderName);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_TestCodeByName: " + ex.ToString());
                return null;
            }
        }

        public string Inserttbl_TestCode(tbl_TestCode TestCode)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_TestCode> sQLServer = new SQLServerConnection<tbl_TestCode>();
                return sQLServer.ExecuteInsert("tbl_TestCode_insert", TestCode.TestCode, TestCode.TestCode, TestCode.TestName, TestCode.Category, TestCode.Price,  TestCode.linkurl, TestCode.createby, TestCode.Conclusion, TestCode.Conclusion1);
            }
            catch (Exception ex)
            {
                logError.Info("Inserttbl_slider: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public List<tbl_TestCode> Gettbl_TestCodeByStatus(int status, int type, string keyword)
        {
            try
            {
                SQLServerConnection<tbl_TestCode> sQLServer = new SQLServerConnection<tbl_TestCode>();
                return sQLServer.SelectQueryCommand("tbl_TestCode_SelectSTATUS", status, type, keyword);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_TestCodeByStatus: " + ex.ToString());
                return null;
            }
        }

        public string Publictbl_TestCode(int Id)
        {
            try
            {
                SQLServerConnection<tbl_TestCode> sQLServer = new SQLServerConnection<tbl_TestCode>();
                return sQLServer.ExecuteNonQuery("tbl_TestCode_PublicByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Publictbl_TestCode: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatetbl_TestCode(tbl_TestCode TestCode)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_TestCode> sQLServer = new SQLServerConnection<tbl_TestCode>();
                return sQLServer.ExecuteNonQuery("tbl_TestCode_update", TestCode.ID, TestCode.TestCode, TestCode.QuickCode, TestCode.TestName, TestCode.Category, TestCode.Price,   TestCode.Conclusion, TestCode.linkurl, TestCode.updateby, TestCode.Conclusion1);
            }
            catch (Exception ex)
            {
                logError.Info("Updatetbl_TestCode: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

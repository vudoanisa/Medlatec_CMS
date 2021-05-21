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
    public class Imptbl_YnghiaTestcode : Itbl_YnghiaTestcode
    {
        ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletetbl_YnghiaTestcode(tbl_YnghiaTestcode YnghiaTestcode)
        {
            try
            {
                SQLServerConnection<tbl_TestCode> sQLServer = new SQLServerConnection<tbl_TestCode>();
                return sQLServer.ExecuteNonQuery("tbl_YnghiaTestcode_DeleteByPrimaryKey", YnghiaTestcode.ID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_YnghiaTestcode: " + YnghiaTestcode.ID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletetbl_YnghiaTestcode(int TestCodeID)
        {
            try
            {
                SQLServerConnection<tbl_TestCode> sQLServer = new SQLServerConnection<tbl_TestCode>();
                return sQLServer.ExecuteNonQuery("tbl_YnghiaTestcode_DeleteByPrimaryKey", TestCodeID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_YnghiaTestcode: " + TestCodeID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<tbl_YnghiaTestcode> GetAlltbl_YnghiaTestcode( string testcode)
        {
            try
            {
                SQLServerConnection<tbl_YnghiaTestcode> sQLServer = new SQLServerConnection<tbl_YnghiaTestcode>();
                return sQLServer.SelectQueryCommand("tbl_YnghiaTestcode_SelectByTestcode", testcode);
            }
            catch (Exception ex)
            {
                logError.Info("GetAlltbl_YnghiaTestcode: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_YnghiaTestcode> Gettbl_YnghiaTestcodeByID(int ID)
        {
            try
            {
                SQLServerConnection<tbl_YnghiaTestcode> sQLServer = new SQLServerConnection<tbl_YnghiaTestcode>();
                return sQLServer.SelectQueryCommand("tbl_YnghiaTestcode_SelectByPrimaryKey", ID);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_YnghiaTestcodeByID: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_YnghiaTestcode> Gettbl_YnghiaTestcodeByName(string YnghiaTestcode)
        {
            try
            {
                SQLServerConnection<tbl_YnghiaTestcode> sQLServer = new SQLServerConnection<tbl_YnghiaTestcode>();
                return sQLServer.SelectQueryCommand("tbl_TestCode_SelectByName", YnghiaTestcode);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_YnghiaTestcodeByName: " + ex.ToString());
                return null;
            }
        }

        public string Inserttbl_YnghiaTestcode(tbl_YnghiaTestcode YnghiaTestcode)
        {
            throw new NotImplementedException();
        }

        public string Publictbl_YnghiaTestcode(int TestCodeID)
        {
            throw new NotImplementedException();
        }

        public string Updatetbl_YnghiaTestcode(tbl_YnghiaTestcode YnghiaTestcode)
        {
            throw new NotImplementedException();
        }
    }
}

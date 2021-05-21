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
    public class Imptbl_VoteQuestions : Itbl_VoteQuestions
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletetbl_VoteQuestions(int Id)
        {
            try
            {
                SQLServerConnection<tbl_VoteQuestions> sQLServer = new SQLServerConnection<tbl_VoteQuestions>();
                return sQLServer.ExecuteNonQuery("tbl_VoteQuestions_DeleteByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_VoteQuestions: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletetbl_VoteQuestionsByVoteID(int VoteID)
        {
            try
            {
                SQLServerConnection<tbl_VoteQuestions> sQLServer = new SQLServerConnection<tbl_VoteQuestions>();
                return sQLServer.ExecuteNonQuery("tbl_VoteQuestions_DeleteByVoteID", VoteID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_VoteQuestions: " + VoteID + "  " + ex.ToString());
                return string.Empty;
            }
        }


        public List<tbl_VoteQuestions> GetAlltbl_VoteQuestions()
        {
            throw new NotImplementedException();
        }

        public List<tbl_VoteQuestions> Gettbl_VoteQuestionsByID(int Id)
        {
            try
            {
                SQLServerConnection<tbl_VoteQuestions> sQLServer = new SQLServerConnection<tbl_VoteQuestions>();
                return sQLServer.SelectQueryCommand("tbl_VoteQuestions_SelectByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_VoteQuestionsByID: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_VoteQuestions> Gettbl_VoteQuestionsByVoteID(int VoteID)
        {
            try
            {
                SQLServerConnection<tbl_VoteQuestions> sQLServer = new SQLServerConnection<tbl_VoteQuestions>();
                return sQLServer.SelectQueryCommand("tbl_VoteQuestions_SelectByVoteID", VoteID);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_VoteQuestionsByVoteID: " + ex.ToString());
                return null;
            }
        }

        public string Inserttbl_VoteQuestions(tbl_VoteQuestions VoteQuestions)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_VoteQuestions> sQLServer = new SQLServerConnection<tbl_VoteQuestions>();
                return sQLServer.ExecuteInsert("tbl_VoteQuestions_Insert", VoteQuestions.VoteQuestion1, VoteQuestions.VoteQuestion2, VoteQuestions.VoteQuestion3, VoteQuestions.VoteQuestion4, VoteQuestions.VoteQuestion5, VoteQuestions.VoteQuestion6, VoteQuestions.VoteQuestion7, VoteQuestions.VoteQuestion8, VoteQuestions.createby, VoteQuestions.VoteID);
            }
            catch (Exception ex)
            {
                logError.Info("Inserttbl_VoteQuestions: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Updatetbl_VoteQuestions(tbl_VoteQuestions VoteQuestions)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.ExecuteNonQuery("tbl_VoteQuestions_Update", VoteQuestions.ID, VoteQuestions.VoteQuestion1, VoteQuestions.VoteQuestion2, VoteQuestions.VoteQuestion3, VoteQuestions.VoteQuestion4, VoteQuestions.VoteQuestion5, VoteQuestions.VoteQuestion6, VoteQuestions.VoteQuestion7, VoteQuestions.VoteQuestion8, VoteQuestions.updateby, VoteQuestions.VoteID);
            }
            catch (Exception ex)
            {
                logError.Info("Updatetbl_VoteQuestions: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

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
    public class Imptbl_Vote : Itbl_Vote
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletetbl_Vote(tbl_Vote Vote)
        {
            try
            {
                SQLServerConnection<tbl_Vote> sQLServer = new SQLServerConnection<tbl_Vote>();
                return sQLServer.ExecuteNonQuery("tbl_Vote_DeleteByPrimaryKey", Vote.ID);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_Vote: " + Vote.ID + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Deletetbl_Vote(int Id)
        {
            try
            {
                SQLServerConnection<tbl_Vote> sQLServer = new SQLServerConnection<tbl_Vote>();
                return sQLServer.ExecuteNonQuery("tbl_Vote_DeleteByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletetbl_Vote: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public List<tbl_Vote> GetAlltbl_Vote()
        {
            try
            {
                SQLServerConnection<tbl_Vote> sQLServer = new SQLServerConnection<tbl_Vote>();
                return sQLServer.SelectQueryCommand("tbl_Vote_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAlltbl_Vote: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_Vote> Gettbl_VoteByID(int Id)
        {
            try
            {
                SQLServerConnection<tbl_Vote> sQLServer = new SQLServerConnection<tbl_Vote>();
                return sQLServer.SelectQueryCommand("tbl_Vote_SelectByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_VoteByID: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_Vote> Gettbl_VoteByName(string Name)
        {
            try
            {
                SQLServerConnection<tbl_Vote> sQLServer = new SQLServerConnection<tbl_Vote>();
                return sQLServer.SelectQueryCommand("tbl_Vote_SelectByName", Name);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_VoteByName: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_Vote> Gettbl_VoteByStatus(int votecate, int status, int type, string keyword)
        {
            try
            {
                SQLServerConnection<tbl_Vote> sQLServer = new SQLServerConnection<tbl_Vote>();
                return sQLServer.SelectQueryCommand("tbl_Vote_SelectSTATUS", votecate, status, type, keyword);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_VoteByStatus: " + ex.ToString());
                return null;
            }
        }

        public string Inserttbl_Vote(tbl_Vote Vote)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.ExecuteInsert("tbl_Vote_Insert", Vote.VoteName, Vote.VoteDesc,   Vote.VoteAnswer , Vote.createby, Vote.VoteCateID, Vote.linkExample, Vote.TypeVote);
            }
            catch (Exception ex)
            {
                logError.Info("Inserttbl_Vote: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publictbl_Vote(int Id)
        {
            try
            {
                SQLServerConnection<tbl_Vote> sQLServer = new SQLServerConnection<tbl_Vote>();
                return sQLServer.ExecuteNonQuery("tbl_Vote_PublicByPrimaryKey", Id);
            }
            catch (Exception ex)
            {
                logError.Info("Publictbl_Vote: " + Id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Updatetbl_Vote(tbl_Vote Vote)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<tbl_VoteCate> sQLServer = new SQLServerConnection<tbl_VoteCate>();
                return sQLServer.ExecuteNonQuery("tbl_VoteCate_Update", Vote.ID, Vote.VoteName, Vote.VoteDesc,   Vote.VoteAnswer, Vote.updateby, Vote.VoteCateID, Vote.linkExample, Vote.TypeVote);
            }
            catch (Exception ex)
            {
                logError.Info("Updatetbl_VoteCate: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

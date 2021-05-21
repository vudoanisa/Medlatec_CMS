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
    public class Impcms_Comment : Icms_Comment
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");
        public string Add(cms_Comment entity)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Comment> sQLServer = new SQLServerConnection<cms_Comment>();
                return sQLServer.ExecuteInsert("SP_cms_Comment_Insert", entity.NewsId, entity.FullName, entity.Email, entity.Title, entity.Content, entity.linkNews, entity.ImgAvatar, entity.linkFB, entity.type, entity.CreateBy );
            }
            catch (Exception ex)
            {
                logError.Info("Add: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Delete(cms_Comment entity)
        {
            try
            {
                SQLServerConnection<cms_Comment> sQLServer = new SQLServerConnection<cms_Comment>();
                return sQLServer.ExecuteNonQuery("SP_cms_Comment_DeletehByPrimaryKey", entity.CommentId);
            }
            catch (Exception ex)
            {
                logError.Info("Delete: " + entity.CommentId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Delete(int id)
        {
            try
            {
                SQLServerConnection<cms_Comment> sQLServer = new SQLServerConnection<cms_Comment>();
                return sQLServer.ExecuteNonQuery("SP_cms_Comment_DeletehByPrimaryKey", id);
            }
            catch (Exception ex)
            {
                logError.Info("Delete: " + id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public cms_Comment Get(int id)
        {
            try
            {
                SQLServerConnection<cms_Comment> sQLServer = new SQLServerConnection<cms_Comment>();
                var data = sQLServer.SelectQueryCommand("SP_cms_Comment_SelectByPrimaryKey", id);
                if (data != null)
                {
                    if (data.Count > 0)
                    {
                        return data[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logError.Info("Get: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<cms_Comment> Getcms_Comment(string Tungay, string Denngay, int STATUS,int type, string keyword)
        {
            try
            {
                SQLServerConnection<cms_Comment> sQLServer = new SQLServerConnection<cms_Comment>();
                return sQLServer.SelectQueryCommand("SP_cms_Comment_SelectSTATUS", Tungay, Denngay, STATUS, type, keyword);
            }
            catch (Exception ex)
            {
                logError.Info("DeleteCATEGORYS: " + ex.ToString());
                return null;
            }
        }

        public string Publish(int id)
        {
            try
            {
                SQLServerConnection<cms_Comment> sQLServer = new SQLServerConnection<cms_Comment>();
                return sQLServer.ExecuteNonQuery("SP_cms_Comment_PublichByPrimaryKey", id);
            }
            catch (Exception ex)
            {
                logError.Info("Delete: " + id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Update(cms_Comment entity)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Comment> sQLServer = new SQLServerConnection<cms_Comment>();
                return sQLServer.ExecuteInsert("SP_cms_Comment_Update", entity.CommentId, entity.NewsId, entity.FullName, entity.Email, entity.Title, entity.Content, entity.linkNews, entity.ImgAvatar, entity.linkFB, entity.type );
            }
            catch (Exception ex)
            {
                logError.Info("Update: " + ex.ToString());
                return result = string.Empty;
            }
        }
    }
}

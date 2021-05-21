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
    public class Impcms_Menu_Cms : Icms_Menu_Cms
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");

        public List<cms_Menu_Cms> GetAllcms_MenuParent()
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_SelectParrent");
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuParent: " + ex.ToString());
                return null;
            }
        }



        public List<cms_Menu_Cms> GetAllcms_MenuChild(int ParentID)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_SelectChild", ParentID);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuChild: " + ex.ToString());
                return null;
            }
        }




        public List<cms_Menu_Cms> GetAllcms_MenuChild(int ParentID, int status)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_ChildAndStatus", ParentID, status);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuChild: " + ex.ToString());
                return null;
            }

        }

        public List<cms_Menu_Cms> GetAllcms_MenuChildByUserid(int ParentID, int status, int userid)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_ChildStatusAndUser", ParentID, status, userid);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuChild: " + ex.ToString());
                return null;
            }

        }

        public List<cms_Menu_Cms> GetAllcms_MenuChildByGroupID(int ParentID, int GroupID)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("cms_Medicon_get_cms_MenuChildByGroupID", GroupID, ParentID);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuChildByGroupID: " + ex.ToString());
                return null;
            }

        }

        public List<cms_Menu_Cms> Getcms_MenuByID(int menId)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_Cms_SelectByPrimaryKey", menId);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_MenuByID: " + ex.ToString());
                return null;
            }

        }


        public List<cms_Menu_Cms> GetAllcms_MenuParentByUserid(int UserID)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_ParentAndUserID", UserID);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuParentByUserid: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Menu_Cms> GetAllcms_MenuParentByGroupID(int GroupID)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("cms_Medicon_get_cms_MenuParentByGroupID", GroupID);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuParentByGroupID: " + ex.ToString());
                return null;
            }
        }


        public List<cms_Menu_Cms> GetAllcms_MenuBySortCut(int UserID)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_Cms_SortCutByUserID", UserID);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuBySortCut: " + ex.ToString());
                return null;
            }
        }



        public List<cms_Menu_Cms> GetAllcms_MenuChildByUserid(int ParentID, int UserID)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_Cms_ChildAndUserID", ParentID, UserID);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuChildByUserid: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Menu_Cms> GetAllcms_MenuByUserid(int GroupID)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_Cms_SelectUserID", GroupID);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuByUserid: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Menu_Cms> GetAllcms_MenuByGroupID(int GroupID, int status)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_Cms_SelectGroupID", GroupID, status);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuByGroupID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Menu_Cms> GetAllcms_MenuByGroupID(int GroupIDUser, int GroupID, int status, int type, string keyword)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_Cms_SelectGroupID", GroupIDUser, GroupID, status, type, keyword);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuByGroupID: " + ex.ToString());
                return null;
            }
        }


        public List<cms_Menu_Cms> GetAllcms_MenuParentByGroupID(int GroupID, int GroupIDUser)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_Cms_ParrentByGroupID", GroupID, GroupIDUser);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuParentByGroupID: " + ex.ToString());
                return null;
            }
        }

        public List<cms_Menu_Cms> GetAllcms_MenuChildAndGroupID(int ParentID, int GroupID, int GroupIDUser)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_Cms_ChildAndGroupID", ParentID, GroupID, GroupIDUser);
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_MenuChildAndGroupID: " + ex.ToString());
                return null;
            }
        }

        public cms_Menu_Cms Get(int id)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                var data = sQLServer.SelectQueryCommand("SP_cms_Menu_Cms_SelectByPrimaryKey", id);
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

        public List<cms_Menu_Cms> GetAll()
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.SelectQueryCommand("SP_cms_Menu_Cms_SelectAll");
            }
            catch (Exception ex)
            {
                logError.Info("GetAll: " + ex.ToString());
                return null;
            }
        }

        public string Add(cms_Menu_Cms entity)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.ExecuteInsert("SP_cms_Menu_Cms_Insert", entity.menName, entity.menTitle, entity.menKeyword, entity.menDes, entity.menLinks, entity.cateId, entity.menOrd, entity.menImg, entity.MENDISPLAY, entity.MENStatus, entity.Style, entity.CreateBy, entity.Create_date, entity.UpdateBy, entity.Update_date);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_Menu: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Delete(cms_Menu_Cms entity)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.ExecuteNonQuery("SP_cms_Menu_Cms_DeleteByPrimaryKey", entity.menId);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Menu: " + entity.menId + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Delete(int GroupIDUser, int id)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.ExecuteNonQuery("SP_cms_Menu_Cms_DeleteByPrimaryKeyByGroupIDUser", GroupIDUser, id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Menu: " + id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Delete(int id)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.ExecuteNonQuery("SP_cms_Menu_Cms_DeleteByPrimaryKey", id);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_Menu: " + id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Update(cms_Menu_Cms entity)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.ExecuteNonQuery("SP_cms_Menu_Cms_UPDATE", entity.menId, entity.menName, entity.menTitle, entity.menKeyword, entity.menDes, entity.menLinks, entity.cateId, entity.menOrd, entity.menImg, entity.MENDISPLAY, entity.MENStatus, entity.Style, entity.CreateBy, entity.Create_date, entity.UpdateBy, entity.Update_date);
            }
            catch (Exception ex)
            {
                logError.Info("Updatecms_Menu: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Publish(int id)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.ExecuteNonQuery("SP_cms_Menu_Cms_PublicByPrimaryKey", id);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Menu: " + id + "  " + ex.ToString());
                return string.Empty;
            }
        }

        public string Publish(int GroupIDUser, int id)
        {
            try
            {
                SQLServerConnection<cms_Menu_Cms> sQLServer = new SQLServerConnection<cms_Menu_Cms>();
                return sQLServer.ExecuteNonQuery("SP_cms_Menu_Cms_PupblicByPrimaryKeyByGroupIDUser", GroupIDUser, id);
            }
            catch (Exception ex)
            {
                logError.Info("Publiccms_Menu: " + id + "  " + ex.ToString());
                return string.Empty;
            }
        }

    }
}

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
    public class Impcms_News_Map_Tags : Icms_News_Map_Tags
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_News_Map_Tags(cms_News_Map_Tags _News_Map_Tags)
        {
            throw new NotImplementedException();
        }

        public string Deletecms_News_Map_Tags(int Id)
        {
            throw new NotImplementedException();
        }

        public List<cms_News_Map_Tags> GetAllcms_NewsTags()
        {
            throw new NotImplementedException();
        }

        public List<cms_News_Map_Tags> Getcms_NewsTagsByID(int Id)
        {
            throw new NotImplementedException();
        }

        public string Insertcms_News_Map_Tags(cms_News_Map_Tags _News_Map_Tags)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_News_Map_Tags> sQLServer = new SQLServerConnection<cms_News_Map_Tags>();
                return sQLServer.ExecuteInsert("SP_cms_NewsTags_Insert_batch", _News_Map_Tags.newsId, _News_Map_Tags.ListTags);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_News_Map_Tags: " + ex.ToString());
                return result = string.Empty;
            }
        }

        public string Updatecms_News_Map_Tags(cms_News_Map_Tags _News_Map_Tags)
        {
            throw new NotImplementedException();
        }
    }
}

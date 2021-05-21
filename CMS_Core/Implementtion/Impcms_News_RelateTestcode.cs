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
    public class Impcms_News_RelateTestcode : Icms_News_RelateTestcode
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");
        public string Deletecms_News_RelateTestcode(cms_News_RelateTestcode _News_Relate)
        {
            throw new NotImplementedException();
        }

        public string Deletecms_News_RelateTestcode(int _Id)
        {
            throw new NotImplementedException();
        }

        public List<cms_News_RelateTestcode> GetAllcms_News_RelateTestcode()
        {
            throw new NotImplementedException();
        }

        public List<cms_News_RelateTestcode> Getcms_News_RelateTestcodeByID(int _Id)
        {
            throw new NotImplementedException();
        }


        public string Insertcms_News_Relate(cms_News _News, string newsIdSource)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_News_RelateTestcode> sQLServer = new SQLServerConnection<cms_News_RelateTestcode>();
                return sQLServer.ExecuteInsert("SP_cms_News_RelateTestcode_Insert", newsIdSource, _News.newsId.ToString(), _News.newsName, _News.newsDescription, _News.newsImages, _News.datepub, _News.cateId, _News.SourceId);
            }
            catch (Exception ex)
            {
                logError.Info("Insertcms_News_Relate: " + ex.ToString());
                return result = string.Empty;
            }
        }

        

        public string Deletecms_News_Relate(string listRelate, string newsIdSource)
        {
            string result = string.Empty;
            try
            {
                SQLServerConnection<cms_News_RelateTestcode> sQLServer = new SQLServerConnection<cms_News_RelateTestcode>();
                return sQLServer.ExecuteInsert("SP_cms_News_RelateTestcode_DeleteNotIn", listRelate, newsIdSource);
            }
            catch (Exception ex)
            {
                logError.Info("Deletecms_News_Relate: " + ex.ToString());
                return result = string.Empty;
            }
        }



        public string Insertcms_News_RelateTestcode(cms_News_RelateTestcode _News_Relate)
        {
            throw new NotImplementedException();
        }

        public string Updatecms_News_RelateTestcode(cms_News_RelateTestcode _News_Relate)
        {
            throw new NotImplementedException();
        }
    }
}

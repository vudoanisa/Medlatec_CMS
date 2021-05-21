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
    public class Impcms_Doctor_Cate : Icms_Doctor_Cate
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");

        public List<cms_Doctor_Cate> Getcms_Doctor_CateByID(int id)
        {
            try
            {
                SQLServerConnection<cms_Doctor_Cate> sQLServer = new SQLServerConnection<cms_Doctor_Cate>();
                return sQLServer.SelectQueryCommand("SP_cms_Doctor_Cate_SelectByCateID", id);
            }
            catch (Exception ex)
            {
                logError.Info("Getcms_Doctor_CateByID: " + ex.ToString());
                return null;
            }
        }
    }
}

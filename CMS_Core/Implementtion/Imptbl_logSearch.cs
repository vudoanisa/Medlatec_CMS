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
    public class Imptbl_logSearch : itbl_logSearch
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSNEWLogError");

        public List<tbl_logSearch> GetAlltbl_logSearch(string datestart, string dateEnd)
        {
            try
            {
                SQLServerConnection<tbl_logSearch> sQLServer = new SQLServerConnection<tbl_logSearch>();
                return sQLServer.SelectQueryCommand("SP_tbl_logSearch_Select", 0);
            }
            catch (Exception ex)
            {
                logError.Info("GetAlltbl_logSearch: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_logSearch> Gettbl_logSearchByDoctorName(string datestart, string dateEnd, string doctorid)
        {
            try
            {
                SQLServerConnection<tbl_logSearch> sQLServer = new SQLServerConnection<tbl_logSearch>();
                return sQLServer.SelectQueryCommand("SP_tbl_logSearch_Select", datestart, dateEnd, doctorid);
            }
            catch (Exception ex)
            {
                logError.Info("Gettbl_logSearchByDoctorName: " + ex.ToString());
                return null;
            }
        }

        public List<tbl_logSearch> Gettbl_logSearchByID(string datestart, string dateEnd, string doctorid)
        {
            throw new NotImplementedException();
        }
    }
}

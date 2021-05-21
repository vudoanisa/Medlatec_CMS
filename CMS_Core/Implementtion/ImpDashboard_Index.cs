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
    public class ImpDashboard_Index : IDashboard_Index
    {
        ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");

        public ListDashboard_Index GetDashboard_Index(int PARTNERID)
        {
            try
            {
                OracleServerConnection<Dashboard_Index> sQLServer = new OracleServerConnection<Dashboard_Index>();
                return sQLServer.SelectDashboard_IndexQueryCommand("SP_get_DashboardIndex", PARTNERID);
            }
            catch (Exception ex)
            {
                logError.Info("GetDashboard_Index: " + ex.ToString());
                return null;
            }
        }

        public ListDashboard_Index GetDashboard_ByDate(DateTime start, DateTime end, int PARTNERID)
        {
            try
            {
                OracleServerConnection<Dashboard_Index> sQLServer = new OracleServerConnection<Dashboard_Index>();
                return sQLServer.SelectDashboard_IndexQueryCommand("SP_DashboardIndex_ByDate", start, end, PARTNERID);
            }
            catch (Exception ex)
            {
                logError.Info("GetDashboard_Index: " + ex.ToString());
                return null;
            }
        }

    }
}

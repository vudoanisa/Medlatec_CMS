using CMS_Core.Entity;
using CMS_Core.Interface;
using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Implementtion
{
    public class Impcms_TypeStatus : Icms_TypeStatus
    {
        readonly ILog logError = log4net.LogManager.GetLogger("CMSLISLogError");
        public List<cms_TypeStatus> GetAllcms_TypeStatus()
        {
            try
            {
                var connection = new SqlConnection(Common.Common.getConnectionString());
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    using (var mul = con.QueryMultiple("SP_cms_TypeStatus_SelectAll", parm, commandType: CommandType.StoredProcedure))
                    {
                        var data = mul.Read<cms_TypeStatus>().ToList();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.Info("GetAllcms_TypeStatus: " + ex.ToString());

                return null;
            }
        }
    }
}

using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface itbl_logSearch
    {
        /// <summary>
        /// danh sách dịch vụ
        /// </summary>      
        /// <param name="partnerid">ID đối tác</param>    
        /// <returns></returns>        
        List<tbl_logSearch> GetAlltbl_logSearch(string  datestart, string dateEnd);

        /// <summary>
        /// tìm danh dịch vụ theo ID
        /// </summary>      
        /// <param name="ID">ID menu</param>       
        /// <returns></returns>        
        List<tbl_logSearch> Gettbl_logSearchByID(string datestart, string dateEnd , string doctorid );

        /// <summary>
        /// tìm danh dịch vụ theo ID
        /// </summary>      
        /// <param name="ID">ID menu</param>       
        /// <returns></returns>        
        List<tbl_logSearch> Gettbl_logSearchByDoctorName(string datestart, string dateEnd, string doctorid);

    }
}

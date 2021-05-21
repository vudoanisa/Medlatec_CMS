using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_TypeStatus
    {
        /// <summary>
        /// Danh sách loại status
        /// </summary>      

        /// <returns></returns>        
        List<cms_TypeStatus> GetAllcms_TypeStatus();

    }
}

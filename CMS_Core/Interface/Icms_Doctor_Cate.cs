using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Doctor_Cate
    {
        /// <summary>
        /// tìm danh sách nhóm bác sỹ
        /// </summary>      
        /// <param name="id">ID bác sỹ</param>       
        /// <returns></returns>        
        List<cms_Doctor_Cate> Getcms_Doctor_CateByID(int id);

    }
}

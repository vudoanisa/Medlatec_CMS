using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_VideoCate
    {
        /// <summary>
        /// danh sách chuyên mục video
        /// </summary>      

        /// <returns></returns>        
        List<cms_VideoCate> GetAllcms_VideoCate();

        /// <summary>
        /// tìm danh sách chuyên mục video theo ID
        /// </summary>      
        /// <param name="vid">ID video</param>       
        /// <returns></returns>        
        List<cms_VideoCate> Getcms_VideoCateByID(int vid);

        /// <summary>
        /// Tìm danh sách chuyên mục video theo ID
        /// </summary>      
        /// <param name="vName">cateName Tên menu</param>       
        /// <returns></returns>        
        List<cms_VideoCate> Getcms_VideoCateByName(string vName);



        /// <summary>
        /// Chèn dữ liệu chuyên mục video mới
        /// </summary>      
        /// <param name="cms_Video">Input insert</param>
        /// <returns></returns>        
        string Insertcms_VideoCate(cms_VideoCate cms_Video );

        /// <summary>
        /// Cập nhật dữ liệu cho chuyên mục video
        /// </summary>      
        /// <param name="cms_Video">Input update</param>
        /// <returns></returns>        
        string Updatecms_VideoCate(cms_VideoCate cms_Video);

        /// <summary>
        /// xóa dữ liệu video
        /// </summary>      
        /// <param name="cms_Video">Input delete</param>
        /// <returns></returns>        
        string Deletecms_VideoCate(cms_VideoCate cms_Video);

        /// <summary>
        /// Xóa dữ liệu chuyên mục video
        /// </summary>      
        /// <param name="_vId">_vId chuyên mục</param>
        /// <returns></returns>        
        string Deletecms_VideoCate(int _vId);

        /// <summary>
        /// xuat ban chuyên mục video
        /// </summary>      
        /// <param name="_vId">_vId chuyên mục</param>
        /// <returns></returns>        
        string Publichcms_VideoCate(int _vId);

    }
}

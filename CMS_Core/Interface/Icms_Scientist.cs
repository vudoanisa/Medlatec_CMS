using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Scientist
    {
        /// <summary>
        /// danh sách hợp đồng khoa học
        /// </summary>      

        /// <returns></returns>        
        List<cms_Scientist> GetAllcms_Scientist();

        /// <summary>
        /// Tìm danh sách nghiên cứu khoa học theo ID
        /// </summary>      
        /// <param name="ID">ID video</param>       
        /// <returns></returns>        
        List<cms_Scientist> Getcms_ScientistByID(int ID);

        /// <summary>
        /// Tìm danh sách nghiên cứu khoa học theo Tên
        /// </summary>      
        /// <param name="ScientistTitle">ScientistTitle Tên nghiên cứu khoa</param>       
        /// <returns></returns>        
        List<cms_Scientist> Getcms_ScientistByName(string ScientistTitle);



        /// <summary>
        /// Chèn dữ liệu  nghiên cứu khoa học mới
        /// </summary>      
        /// <param name="_cms_Scientist">Input insert</param>
        /// <returns></returns>        
        string Insertcms_Scientist(cms_Scientist _cms_Scientist);

        /// <summary>
        /// Cập nhật dữ liệu nghiên cứu khoa học
        /// </summary>      
        /// <param name="_ImagesCate">Input update</param>
        /// <returns></returns>        
        string Updatecms_Scientist(cms_Scientist _cms_Scientist);

        /// <summary>
        /// xóa dữ liệu nghiên cứu khoa học
        /// </summary>      
        /// <param name="_cms_Scientist">Input delete</param>
        /// <returns></returns>        
        string Deletecms_Scientist(cms_Scientist _cms_Scientist);

        /// <summary>
        /// Xóa dữ liệu nghiên cứu khoa học
        /// </summary>      
        /// <param name="planId">_vId chuyên mục</param>
        /// <returns></returns>        
        string Deletecms_Scientist(int ID);

        /// <summary>
        /// xuat ban nghiên cứu khoa học
        /// </summary>      
        /// <param name="planId">_vId chuyên mục</param>
        /// <returns></returns>        
        string Publiccms_Scientist(int ID);


    }
}

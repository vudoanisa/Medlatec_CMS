using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Scientist_Cate
    {
        /// <summary>
        /// danh sách hợp đồng khoa học
        /// </summary>      

        /// <returns></returns>        
        List<cms_Scientist_Cate> GetAllcms_Scientist_Cate();

        /// <summary>
        /// Tìm danh sách nghiên cứu khoa học theo ID
        /// </summary>      
        /// <param name="ID">ID video</param>       
        /// <returns></returns>        
        List<cms_Scientist_Cate> Getcms_Scientist_CateByID(int ID);

        /// <summary>
        /// Tìm danh sách nghiên cứu khoa học theo Tên
        /// </summary>      
        /// <param name="ScientistTitle">ScientistTitle Tên nghiên cứu khoa</param>       
        /// <returns></returns>        
        List<cms_Scientist_Cate> Getcms_Scientist_CateByName(string ScientistTitle);



        /// <summary>
        /// Chèn dữ liệu  nghiên cứu khoa học mới
        /// </summary>      
        /// <param name="_cms_Scientist_Cate">Input insert</param>
        /// <returns></returns>        
        string Insertcms_Scientist_Cate(cms_Scientist_Cate _cms_Scientist_Cate);

        /// <summary>
        /// Cập nhật dữ liệu nghiên cứu khoa học
        /// </summary>      
        /// <param name="_cms_Scientist_Cate">Input update</param>
        /// <returns></returns>        
        string Updatecms_Scientist_Cate(cms_Scientist_Cate _cms_Scientist_Cate);

        /// <summary>
        /// xóa dữ liệu nghiên cứu khoa học
        /// </summary>      
        /// <param name="_cms_Scientist_Cate">Input delete</param>
        /// <returns></returns>        
        string Deletecms_Scientist_Cate(cms_Scientist_Cate _cms_Scientist_Cate);

        /// <summary>
        /// Xóa dữ liệu nghiên cứu khoa học
        /// </summary>      
        /// <param name="ID">ID chuyên mục</param>
        /// <returns></returns>        
        string Deletecms_Scientist_Cate(int ID);

        /// <summary>
        /// xuat ban nghiên cứu khoa học
        /// </summary>      
        /// <param name="ID">ID nhà khoa học</param>
        /// <returns></returns>        
        string Publiccms_Scientist_Cate(int ID);

    }
}

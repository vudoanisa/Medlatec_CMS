using CMS_Core.Entity;
using CMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Banner_rows
    {
        /// <summary>
        /// danh sách video
        /// </summary>      

        /// <returns></returns>        
        List<cms_Banner_rows> GetAllcms_Banner_rows();

        /// <summary>
        /// Tìm danh sách quảng cáo theo ID
        /// </summary>      
        /// <param name="bannerId">ID quảng cáo</param>       
        /// <returns></returns>        
        List<cms_Banner_rows> Getcms_Banner_rowsByID(int bannerId);

        /// <summary>
        /// Tìm danh sách quảng cáo theo Tên
        /// </summary>      
        /// <param name="title">title Tên quảng cáo</param>       
        /// <returns></returns>        
        List<cms_Banner_rows> Getcms_Banner_rowsByTitle(string title);

        /// <summary>
        /// Tìm danh sách quảng cáo theo chuyên mục
        /// </summary>                  
        /// <returns></returns>        
        List<cms_Banner_rows> Getcms_Banner_rowsByBannerCate(SearchNewsViewModel searchNewsViewModel);



        /// <summary>
        /// Chèn dữ liệu  quảng cáo mới
        /// </summary>      
        /// <param name="_Banner_Rows">Input insert</param>
        /// <returns></returns>        
        string Insertcms_Banner_rows(cms_Banner_rows _Banner_Rows);

        /// <summary>
        /// Cập nhật dữ liệu quảng cáo
        /// </summary>      
        /// <param name="_Banner_Rows">Input update</param>
        /// <returns></returns>        
        string Updatecms_Banner_rows(cms_Banner_rows _Banner_Rows);

        /// <summary>
        /// xóa dữ liệu quảng cáo
        /// </summary>      
        /// <param name="_Banner_Rows">Input delete</param>
        /// <returns></returns>        
        string Deletecms_Video(cms_Banner_rows _Banner_Rows);

        /// <summary>
        /// Xóa dữ liệu quảng cáo
        /// </summary>      
        /// <param name="bannerId">bannerId id quảng cáo</param>
        /// <returns></returns>        
        string Deletecms_Banner_rows(int bannerId);

        /// <summary>
        /// xuat ban chuyên mục quảng cáo
        /// </summary>      
        /// <param name="bannerId">bannerId quảng cáo</param>
        /// <returns></returns>        
        string Publiccms_Banner_rows(int bannerId);

    }
}

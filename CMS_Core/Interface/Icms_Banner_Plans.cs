using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Banner_Plans
    {
        /// <summary>
        /// danh sách chuyên mục vị trí quảng cáo
        /// </summary>      

        /// <returns></returns>        
        List<cms_Banner_Plans> GetAllcms_Banner_Plans();

        /// <summary>
        /// Tìm danh sách chuyên mục vị trí quảng cáo theo ID
        /// </summary>      
        /// <param name="planId">ID video</param>       
        /// <returns></returns>        
        List<cms_Banner_Plans> Getcms_Banner_PlansByID(int planId);

        /// <summary>
        /// Tìm danh sách chuyên mục chuyên mục vị trí quảng cáo theo Tên
        /// </summary>      
        /// <param name="planTitle">cateName Tên menu</param>       
        /// <returns></returns>        
        List<cms_Banner_Plans> Getcms_Banner_PlansByName(string planTitle);



        /// <summary>
        /// Chèn dữ liệu  chuyên mục vị trí quảng cáo mới
        /// </summary>      
        /// <param name="_ImagesCate">Input insert</param>
        /// <returns></returns>        
        string Insertcms_Banner_Plans(cms_Banner_Plans _Banner_Plans);

        /// <summary>
        /// Cập nhật dữ liệu chuyên mục vị trí quảng cáo
        /// </summary>      
        /// <param name="_ImagesCate">Input update</param>
        /// <returns></returns>        
        string Updatecms_Banner_Plans(cms_Banner_Plans _Banner_Plans);

        /// <summary>
        /// xóa dữ liệu chuyên mục vị trí quảng cáo
        /// </summary>      
        /// <param name="_ImagesCate">Input delete</param>
        /// <returns></returns>        
        string Deletecms_Banner_Plans(cms_Banner_Plans _Banner_Plans);

        /// <summary>
        /// Xóa dữ liệu chuyên mục vị trí quảng cáo
        /// </summary>      
        /// <param name="planId">_vId chuyên mục</param>
        /// <returns></returns>        
        string Deletecms_Banner_Plans(int planId);

        /// <summary>
        /// xuat ban chuyên mục chuyên mục vị trí quảng cáo
        /// </summary>      
        /// <param name="planId">_vId chuyên mục</param>
        /// <returns></returns>        
        string Publiccms_Banner_Plans(int planId);

    }
}

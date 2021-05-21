using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_ImagesCate
    {
        /// <summary>
        /// danh sách chuyên mục ảnh
        /// </summary>      

        /// <returns></returns>        
        List<cms_ImagesCate> GetAllcms_ImagesCate();

        /// <summary>
        /// Tìm danh sách chuyên mục ảnh theo ID
        /// </summary>      
        /// <param name="_vId">ID video</param>       
        /// <returns></returns>        
        List<cms_ImagesCate> Getcms_ImagesCateByID(int _vId);

        /// <summary>
        /// Tìm danh sách chuyên mục chuyên mục ảnh theo Tên
        /// </summary>      
        /// <param name="vName">cateName Tên menu</param>       
        /// <returns></returns>        
        List<cms_ImagesCate> Getcms_ImagesCateByName(string vName);



        /// <summary>
        /// Chèn dữ liệu  chuyên mục ảnh mới
        /// </summary>      
        /// <param name="_ImagesCate">Input insert</param>
        /// <returns></returns>        
        string Insertcms_ImagesCate(cms_ImagesCate _ImagesCate);

        /// <summary>
        /// Cập nhật dữ liệu chuyên mục ảnh
        /// </summary>      
        /// <param name="_ImagesCate">Input update</param>
        /// <returns></returns>        
        string Updatecms_ImagesCate(cms_ImagesCate _ImagesCate);

        /// <summary>
        /// xóa dữ liệu chuyên mục ảnh
        /// </summary>      
        /// <param name="_ImagesCate">Input delete</param>
        /// <returns></returns>        
        string Deletecms_ImagesCate(cms_ImagesCate _ImagesCate);

        /// <summary>
        /// Xóa dữ liệu chuyên mục ảnh
        /// </summary>      
        /// <param name="_vId">_vId chuyên mục</param>
        /// <returns></returns>        
        string Deletecms_ImagesCate(int _vId);

        /// <summary>
        /// xuat ban chuyên mục chuyên mục ảnh
        /// </summary>      
        /// <param name="_vId">_vId chuyên mục</param>
        /// <returns></returns>        
        string Publiccms_ImagesCate(int _vId);


    }
}

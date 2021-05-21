using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Images
    {
        /// <summary>
        /// danh sách  ảnh
        /// </summary>      

        /// <returns></returns>        
        List<cms_Images> GetAllcms_Images();

        /// <summary>
        /// Tìm danh sách  ảnh theo ID
        /// </summary>      
        /// <param name="videoId">ID video</param>       
        /// <returns></returns>        
        List<cms_Images> Getcms_ImagesByID(int videoId);

        /// <summary>
        /// Tìm danh sách ảnh theo Tên
        /// </summary>      
        /// <param name="VideoName">VideoName Tên menu</param>       
        /// <returns></returns>        
        List<cms_Images> Getcms_ImagesByName(string VideoName);



        /// <summary>
        /// Chèn dữ liệu ảnh mới
        /// </summary>      
        /// <param name="_ImagesCate">Input insert</param>
        /// <returns></returns>        
        string Insertcms_Images(cms_Images _Images );

        /// <summary>
        /// Cập nhật dữ liệu ảnh
        /// </summary>      
        /// <param name="_ImagesCate">Input update</param>
        /// <returns></returns>        
        string Updatecms_Images(cms_Images _Images);

        /// <summary>
        /// xóa dữ liệu ảnh
        /// </summary>      
        /// <param name="_ImagesCate">Input delete</param>
        /// <returns></returns>        
        string Deletecms_Images(cms_Images _Images);

        /// <summary>
        /// Xóa dữ liệu ảnh
        /// </summary>      
        /// <param name="videoId">videoId chuyên mục</param>
        /// <returns></returns>        
        string Deletecms_Images(int videoId);

        /// <summary>
        /// xuat ban ảnh
        /// </summary>      
        /// <param name="videoId">videoId chuyên mục</param>
        /// <returns></returns>        
        string Publiccms_Images(int videoId);
    }
}

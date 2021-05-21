using CMS_Core.Entity;
using CMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Video
    {
        /// <summary>
        /// danh sách video
        /// </summary>      

        /// <returns></returns>        
        List<cms_Video> GetAllcms_Video();

        /// <summary>
        /// Tìm danh sách video theo ID
        /// </summary>      
        /// <param name="videoId">ID video</param>       
        /// <returns></returns>        
        List<cms_Video> Getcms_VideoByID(int videoId);

        /// <summary>
        /// Tìm danh sách chuyên mục video theo Tên
        /// </summary>      
        /// <param name="VideoName">cateName Tên menu</param>       
        /// <returns></returns>        
        List<cms_Video> Getcms_VideoByVideoName(string VideoName);

        /// <summary>
        /// Tìm danh sách chuyên mục video theo chuyên mục
        /// </summary>      
        /// <param name="VideoName">cateName Tên menu</param>       
        /// <returns></returns>        
        List<cms_Video> Getcms_VideoByVideoCate(SearchNewsViewModel searchNewsViewModel);



        /// <summary>
        /// Chèn dữ liệu  video mới
        /// </summary>      
        /// <param name="cms_Video">Input insert</param>
        /// <returns></returns>        
        string Insertcms_Video(cms_Video _Video );

        /// <summary>
        /// Cập nhật dữ liệu video
        /// </summary>      
        /// <param name="cms_Video">Input update</param>
        /// <returns></returns>        
        string Updatecms_Video(cms_Video _Video);

        /// <summary>
        /// xóa dữ liệu video
        /// </summary>      
        /// <param name="cms_Video">Input delete</param>
        /// <returns></returns>        
        string Deletecms_Video(cms_Video _Video);

        /// <summary>
        /// Xóa dữ liệu video
        /// </summary>      
        /// <param name="_vId">_vId chuyên mục</param>
        /// <returns></returns>        
        string Deletecms_Video(int _vId);

        /// <summary>
        /// xuat ban chuyên mục video
        /// </summary>      
        /// <param name="videoId">_vId chuyên mục</param>
        /// <returns></returns>        
        string Publiccms_Video(int videoId);


    }
}

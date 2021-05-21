using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_News_RelateTestcode
    {
        /// <summary>
        /// Danh sách bài viết liên quan
        /// </summary>      

        /// <returns></returns>        
        List<cms_News_RelateTestcode> GetAllcms_News_RelateTestcode();

        /// <summary>
        /// tìm danh sách bài viết liên quan theo ID
        /// </summary>      
        /// <param name="_Id">TagId  </param>       
        /// <returns></returns>        
        List<cms_News_RelateTestcode> Getcms_News_RelateTestcodeByID(int _Id);

        /// <summary>
        /// Chèn dữ liệu bài viết liên quan
        /// </summary>      
        /// <param name="_News_Relate">Input insert</param>
        /// <returns></returns>        
        string Insertcms_News_RelateTestcode(cms_News_RelateTestcode _News_Relate);

        /// <summary>
        /// Cập nhật dữ liệu cho bài viết liên quan
        /// </summary>      
        /// <param name="_News_Relate">Input update</param>
        /// <returns></returns>        
        string Updatecms_News_RelateTestcode(cms_News_RelateTestcode _News_Relate);


        /// <summary>
        /// xóa dữ liệu bài viết liên quan
        /// </summary>      
        /// <param name="_News_Relate">Input delete</param>
        /// <returns></returns>        
        string Deletecms_News_RelateTestcode(cms_News_RelateTestcode _News_Relate);

        /// <summary>
        /// Xóa dữ liệu bài viết liên quan
        /// </summary>      
        /// <param name="_Id">ID tag</param>
        /// <returns></returns>        
        string Deletecms_News_RelateTestcode(int _Id);
    }
}

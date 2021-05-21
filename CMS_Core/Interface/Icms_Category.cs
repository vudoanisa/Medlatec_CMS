using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Category
    {
        /// <summary>
        /// danh sách chuyên mục
        /// </summary>      

        /// <returns></returns>        
        List<cms_Category> GetAllcms_Category();

        /// <summary>
        /// tìm danh sách chuyên mục theo ID
        /// </summary>      
        /// <param name="cateId">ID menu</param>       
        /// <returns></returns>        
        List<cms_Category> Getcms_CategoryByID(int cateId);

        /// <summary>
        /// lấy danh sách chuyên mục Parent
        /// </summary>      
        /// <returns></returns>        
        List<cms_Category> Getcms_CategoryParent();

        /// <summary>
        /// tìm danh sách chuyên mục theo ID
        /// </summary>      
        /// <param name="cateName">cateName Tên menu</param>       
        /// <returns></returns>        
        List<cms_Category> Getcms_CategoryByCateName(string cateName);



        /// <summary>
        /// Chèn dữ liệu chuyên mục mới
        /// </summary>      
        /// <param name="_Category">Input insert</param>
        /// <returns></returns>        
        string Insertcms_Category(cms_Category _Category);


        /// <summary>
        /// Chèn dữ liệu chuyên mục mới có mã hóa
        /// </summary>      
        /// <param name="_Category">Input insert</param>
        /// <returns></returns>        
        string Insertcms_CategoryEncrypt(cms_Category _Category, string listColumn);



        /// <summary>
        /// Cập nhật dữ liệu cho chuyên mục
        /// </summary>      
        /// <param name="_Category">Input update</param>
        /// <returns></returns>        
        string Updatecms_Category(cms_Category _Category);


        /// <summary>
        /// Cập nhật dữ liệu cho chuyên mục
        /// </summary>      
        /// <param name="_Category">Input update</param>
        /// <returns></returns>        
        string Updatecms_CategoryEncrypt(cms_Category _Category, string listColumn);

        /// <summary>
        /// xóa dữ liệu menu
        /// </summary>      
        /// <param name="_Category">Input delete</param>
        /// <returns></returns>        
        string Deletecms_Category(cms_Category _Category);

        /// <summary>
        /// Xóa dữ liệu chuyên mục
        /// </summary>      
        /// <param name="cateId">ID chuyên mục</param>
        /// <returns></returns>        
        string Deletecms_Category(int cateId);


    }
}

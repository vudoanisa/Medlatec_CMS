using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Contact
    {
        /// <summary>
        /// danh sách  liên hệ
        /// </summary>      

        /// <returns></returns>        
        List<cms_Contact> GetAllcms_Contact();

        /// <summary>
        /// Tìm danh sách   liên hệ theo ID
        /// </summary>      
        /// <param name="contactId">ID video</param>       
        /// <returns></returns>        
        List<cms_Contact> Getcms_ContactByID(int contactId);

        /// <summary>
        /// Tìm danh sách  liên hệ theo Tên
        /// </summary>      
        /// <param name="FullName">VideoName Tên menu</param>       
        /// <returns></returns>        
        List<cms_Contact> Getcms_ContactByFullName(string FullName);



        /// <summary>
        /// Chèn dữ liệu  liên hệ mới
        /// </summary>      
        /// <param name="_Contact">Input insert</param>
        /// <returns></returns>        
        string Insertcms_Contact(cms_Contact _Contact);

        /// <summary>
        /// Cập nhật dữ liệu  liên hệ
        /// </summary>      
        /// <param name="_Contact">Input update</param>
        /// <returns></returns>        
        string Updatecms_Contact(cms_Contact _Contact);

        /// <summary>
        /// xóa dữ liệu  liên hệ
        /// </summary>      
        /// <param name="_Contact">Input delete</param>
        /// <returns></returns>        
        string Deletecms_Contact(cms_Contact _Contact);

        /// <summary>
        /// Xóa dữ liệu  liên hệ
        /// </summary>      
        /// <param name="contactId">contactId chuyên mục</param>
        /// <returns></returns>        
        string Deletecms_Contact(int contactId);

        /// <summary>
        /// xuat ban  liên hệ
        /// </summary>      
        /// <param name="contactId">contactId chuyên mục</param>
        /// <returns></returns>        
        string FeedBackcms_Contact(int contactId);

    }
}

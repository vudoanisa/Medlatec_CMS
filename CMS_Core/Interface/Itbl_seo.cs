using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Itbl_seo
    {
        /// <summary>
        /// danh sách Seo
        /// </summary>      

        /// <returns></returns>        
        List<tbl_seo> GetAlltbl_seo();

        /// <summary>
        /// tìm danh sách Seo theo ID
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>       
        /// <returns></returns>        
        List<tbl_seo> Gettbl_seoByID(int Id);

        /// <summary>
        /// tìm danh sách Seo theo Name
        /// </summary>      
        /// <param name="Name">Name Tên nhóm</param>       
        /// <returns></returns>        
        List<tbl_seo> Gettbl_seoByName(string Name);


        /// <summary>
        /// Chèn dữ liệu  chuyên mục seo mới
        /// </summary>      
        /// <param name="seo">Input insert</param>
        /// <returns></returns>        
        string Inserttbl_seo(tbl_seo seo);

        /// <summary>
        /// Cập nhật dữ liệu cho  chuyên mục seo
        /// </summary>      
        /// <param name="seo">Input update</param>
        /// <returns></returns>        
        string Updatetbl_seo(tbl_seo seo);

        /// <summary>
        /// lấy danh sách seo theo trạng thái
        /// </summary>      
        /// <param name="UserID">danh sách tiền cán bộ tại nhà</param>

        /// <returns></returns>        
        List<tbl_seo> Gettbl_seoByStatus(int cateid, int status, int type, string keyword);

        /// <summary>
        /// xóa dữ liệu seo
        /// </summary>      
        /// <param name="seo">Input delete</param>
        /// <returns></returns>        
        string Deletetbl_seo(tbl_seo seo);

        /// <summary>
        /// Xóa dữ liệu seo
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>
        /// <returns></returns>        
        string Deletetbl_seo(int Id);
        /// <summary>
        /// duyệt dữ liệu seo
        /// </summary>      
        /// <param name="Id">ID chuyên mục vote</param>
        /// <returns></returns>        
        string Publictbl_seo(int Id);

    }
}

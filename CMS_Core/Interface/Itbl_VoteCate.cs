using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Itbl_VoteCate
    {
        /// <summary>
        /// danh sách chuyên mục vote
        /// </summary>      

        /// <returns></returns>        
        List<tbl_VoteCate> GetAlltbl_VoteCate();

        /// <summary>
        /// tìm danh sách chuyên mục vote theo ID
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>       
        /// <returns></returns>        
        List<tbl_VoteCate> Gettbl_VoteCateByID(int Id);



        /// <summary>
        /// tìm danh sách chuyên mục vote theo Name
        /// </summary>      
        /// <param name="Name">Name Tên nhóm</param>       
        /// <returns></returns>        
        List<tbl_VoteCate> Gettbl_VoteCateByName(string Name);






        /// <summary>
        /// Chèn dữ liệu  chuyên mục vote mới
        /// </summary>      
        /// <param name="VoteCate">Input insert</param>
        /// <returns></returns>        
        string Inserttbl_VoteCate(tbl_VoteCate VoteCate);





        /// <summary>
        /// Cập nhật dữ liệu cho  chuyên mục vote
        /// </summary>      
        /// <param name="VoteCate">Input update</param>
        /// <returns></returns>        
        string Updatetbl_VoteCate(tbl_VoteCate VoteCate);

        /// <summary>
        /// lấy danh sách vote theo trạng thái
        /// </summary>      
        /// <param name="UserID">danh sách tiền cán bộ tại nhà</param>

        /// <returns></returns>        
        List<tbl_VoteCate> Gettbl_VoteCateByStatus(int status, int type, string keyword);

        /// <summary>
        /// xóa dữ liệu chuyên mục vote
        /// </summary>      
        /// <param name="VoteCate">Input delete</param>
        /// <returns></returns>        
        string Deletetbl_VoteCate(tbl_VoteCate VoteCate);

        /// <summary>
        /// Xóa dữ liệu  chuyên mục vote
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>
        /// <returns></returns>        
        string Deletetbl_VoteCate(int Id);
        /// <summary>
        /// duyệt dữ liệu  chuyên mục vote
        /// </summary>      
        /// <param name="Id">ID chuyên mục vote</param>
        /// <returns></returns>        
        string Publictbl_VoteCate(int Id);

    }
}

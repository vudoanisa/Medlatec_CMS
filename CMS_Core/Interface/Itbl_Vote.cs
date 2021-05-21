using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Itbl_Vote
    {
        /// <summary>
        /// danh sách chuyên mục vote
        /// </summary>      

        /// <returns></returns>        
        List<tbl_Vote> GetAlltbl_Vote();

        /// <summary>
        /// tìm danh sách chuyên mục vote theo ID
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>       
        /// <returns></returns>        
        List<tbl_Vote> Gettbl_VoteByID(int Id);



        /// <summary>
        /// tìm danh sách chuyên mục vote theo Name
        /// </summary>      
        /// <param name="Name">Name Tên nhóm</param>       
        /// <returns></returns>        
        List<tbl_Vote> Gettbl_VoteByName(string Name);






        /// <summary>
        /// Chèn dữ liệu  chuyên mục vote mới
        /// </summary>      
        /// <param name="Vote">Input insert</param>
        /// <returns></returns>        
        string Inserttbl_Vote(tbl_Vote Vote);



        /// <summary>
        /// lấy danh sách vote theo trạng thái
        /// </summary>      
        /// <param name="status">Trạng thái</param>

        /// <returns></returns>        
        List<tbl_Vote> Gettbl_VoteByStatus(int votecate, int status, int type, string keyword);


      


        /// <summary>
        /// Cập nhật dữ liệu cho  chuyên mục vote
        /// </summary>      
        /// <param name="Vote">Input update</param>
        /// <returns></returns>        
        string Updatetbl_Vote(tbl_Vote Vote);


        /// <summary>
        /// xóa dữ liệu chuyên mục vote
        /// </summary>      
        /// <param name="Vote">Input delete</param>
        /// <returns></returns>        
        string Deletetbl_Vote(tbl_Vote Vote);

        /// <summary>
        /// Xóa dữ liệu  chuyên mục vote
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>
        /// <returns></returns>        
        string Deletetbl_Vote(int Id);
        /// <summary>
        /// duyệt dữ liệu  chuyên mục vote
        /// </summary>      
        /// <param name="Id">ID chuyên mục vote</param>
        /// <returns></returns>        
        string Publictbl_Vote(int Id);

    }
}

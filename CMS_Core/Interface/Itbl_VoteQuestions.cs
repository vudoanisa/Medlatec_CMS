using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Itbl_VoteQuestions
    {
        /// <summary>
        /// danh sách chuyên mục vote
        /// </summary>      

        /// <returns></returns>        
        List<tbl_VoteQuestions> GetAlltbl_VoteQuestions();

        /// <summary>
        /// tìm danh sách chuyên mục vote theo ID
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>       
        /// <returns></returns>        
        List<tbl_VoteQuestions> Gettbl_VoteQuestionsByID(int Id);

        /// <summary>
        /// tìm danh sách chuyên mục vote theo ID
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>       
        /// <returns></returns>        
        List<tbl_VoteQuestions> Gettbl_VoteQuestionsByVoteID(int VoteID);







        /// <summary>
        /// Chèn dữ liệu  chuyên mục vote mới
        /// </summary>      
        /// <param name="VoteQuestions">Input insert</param>
        /// <returns></returns>        
        string Inserttbl_VoteQuestions(tbl_VoteQuestions VoteQuestions);







        /// <summary>
        /// Cập nhật dữ liệu cho  chuyên mục vote
        /// </summary>      
        /// <param name="VoteQuestions">Input update</param>
        /// <returns></returns>        
        string Updatetbl_VoteQuestions(tbl_VoteQuestions VoteQuestions);

 

        /// <summary>
        /// Xóa dữ liệu  chuyên mục vote
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>
        /// <returns></returns>        
        string Deletetbl_VoteQuestions(int Id);
       

    }
}

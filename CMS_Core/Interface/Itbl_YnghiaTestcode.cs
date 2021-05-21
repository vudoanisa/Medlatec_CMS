using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Itbl_YnghiaTestcode
    {
        /// <summary>
        /// danh sách slider
        /// </summary>      

        /// <returns></returns>        
        List<tbl_YnghiaTestcode> GetAlltbl_YnghiaTestcode( string testcode);

        /// <summary>
        /// Tìm danh sách  slider theo ID
        /// </summary>      
        /// <param name="ID">ID video</param>       
        /// <returns></returns>        
        List<tbl_YnghiaTestcode> Gettbl_YnghiaTestcodeByID(int ID);

        /// <summary>
        /// Tìm danh sách _slider theo tên
        /// </summary>      
        /// <param name="YnghiaTestcode">sliderName Tên silder</param>       
        /// <returns></returns>        
        List<tbl_YnghiaTestcode> Gettbl_YnghiaTestcodeByName(string YnghiaTestcode);



        /// <summary>
        /// Chèn dữ liệu _slider
        /// </summary>      
        /// <param name="YnghiaTestcode">Input insert</param>
        /// <returns></returns>        
        string Inserttbl_YnghiaTestcode(tbl_YnghiaTestcode YnghiaTestcode);

        /// <summary>
        /// Cập nhật dữ liệu slider
        /// </summary>      
        /// <param name="TestCode">Input update</param>
        /// <returns></returns>        
        string Updatetbl_YnghiaTestcode(tbl_YnghiaTestcode YnghiaTestcode);

        /// <summary>
        /// xóa dữ liệu slider
        /// </summary>      
        /// <param name="TestCode">Input delete</param>
        /// <returns></returns>        
        string Deletetbl_YnghiaTestcode(tbl_YnghiaTestcode YnghiaTestcode);

        /// <summary>
        /// Xóa dữ liệu slider
        /// </summary>      
        /// <param name="Id">Id slider</param>
        /// <returns></returns>        
        string Deletetbl_YnghiaTestcode(int TestCodeID);

        /// <summary>
        /// xuat ban slider
        /// </summary>      
        /// <param name="Id">Id slider</param>
        /// <returns></returns>        
        string Publictbl_YnghiaTestcode(int TestCodeID);
    }
}

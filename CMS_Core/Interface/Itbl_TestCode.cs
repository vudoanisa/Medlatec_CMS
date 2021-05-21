using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Itbl_TestCode
    {
        /// <summary>
        /// danh sách slider
        /// </summary>      

        /// <returns></returns>        
        List<tbl_TestCode> GetAlltbl_TestCode();

        /// <summary>
        /// Tìm danh sách  slider theo ID
        /// </summary>      
        /// <param name="ID">ID video</param>       
        /// <returns></returns>        
        List<tbl_TestCode> Gettbl_TestCodeByID(int ID);

        /// <summary>
        /// Tìm danh sách _slider theo tên
        /// </summary>      
        /// <param name="sliderName">sliderName Tên silder</param>       
        /// <returns></returns>        
        List<tbl_TestCode> Gettbl_TestCodeByName(string sliderName);



        /// <summary>
        /// Chèn dữ liệu _slider
        /// </summary>      
        /// <param name="TestCode">Input insert</param>
        /// <returns></returns>        
        string Inserttbl_TestCode(tbl_TestCode TestCode);

        /// <summary>
        /// Cập nhật dữ liệu slider
        /// </summary>      
        /// <param name="TestCode">Input update</param>
        /// <returns></returns>        
        string Updatetbl_TestCode(tbl_TestCode TestCode);

        /// <summary>
        /// xóa dữ liệu slider
        /// </summary>      
        /// <param name="TestCode">Input delete</param>
        /// <returns></returns>        
        string Deletetbl_TestCode(tbl_TestCode TestCode);

        /// <summary>
        /// Xóa dữ liệu slider
        /// </summary>      
        /// <param name="Id">Id slider</param>
        /// <returns></returns>        
        string Deletetbl_TestCode(int TestCodeID);

        /// <summary>
        /// xuat ban slider
        /// </summary>      
        /// <param name="Id">Id slider</param>
        /// <returns></returns>        
        string Publictbl_TestCode(int TestCodeID);
    }
}

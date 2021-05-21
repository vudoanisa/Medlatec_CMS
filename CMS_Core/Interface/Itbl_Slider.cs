using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Itbl_Slider
    {
        /// <summary>
        /// danh sách slider
        /// </summary>      

        /// <returns></returns>        
        List<tbl_slider> GetAlltbl_Slider();

        /// <summary>
        /// Tìm danh sách  slider theo ID
        /// </summary>      
        /// <param name="ID">ID video</param>       
        /// <returns></returns>        
        List<tbl_slider> Gettbl_SliderByID(int ID);

        /// <summary>
        /// Tìm danh sách _slider theo tên
        /// </summary>      
        /// <param name="sliderName">sliderName Tên silder</param>       
        /// <returns></returns>        
        List<tbl_slider> Gettbl_sliderByName(string sliderName);



        /// <summary>
        /// Chèn dữ liệu _slider
        /// </summary>      
        /// <param name="_slider">Input insert</param>
        /// <returns></returns>        
        string Inserttbl_slider(tbl_slider _slider);

        /// <summary>
        /// Cập nhật dữ liệu slider
        /// </summary>      
        /// <param name="_slider">Input update</param>
        /// <returns></returns>        
        string Updatetbl_slider(tbl_slider _slider);

        /// <summary>
        /// xóa dữ liệu slider
        /// </summary>      
        /// <param name="_ImagesCate">Input delete</param>
        /// <returns></returns>        
        string Deletetbl_slider(tbl_slider _slider);

        /// <summary>
        /// Xóa dữ liệu slider
        /// </summary>      
        /// <param name="Id">Id slider</param>
        /// <returns></returns>        
        string Deletetbl_slider(int Id);

        /// <summary>
        /// xuat ban slider
        /// </summary>      
        /// <param name="Id">Id slider</param>
        /// <returns></returns>        
        string Publictbl_slider(int Id);
    }
}

using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Itbl_Map
    {
        /// <summary>
        /// danh sách đơn vị thành viên
        /// </summary>      

        /// <returns></returns>        
        List<tbl_Map> GetAlltbl_Map();

        /// <summary>
        /// Tìm danh sách  đơn vị thành viên theo ID
        /// </summary>      
        /// <param name="ID">ID  </param>       
        /// <returns></returns>        
        List<tbl_Map> Gettbl_MapByID(int ID);

        /// <summary>
        /// Tìm danh sách đơn vị thành viên theo tên
        /// </summary>      
        /// <param name="Mapname">Mapname Tên đơn vị thành viên</param>       
        /// <returns></returns>        
        List<tbl_Map> Gettbl_MapByName(string Mapname);



        /// <summary>
        /// Chèn dữ liệu đơn vị thành viên
        /// </summary>      
        /// <param name="_Map">Input insert</param>
        /// <returns></returns>        
        string Inserttbl_Map(tbl_Map _Map);

        /// <summary>
        /// Cập nhật dữ liệu đơn vị thành viên
        /// </summary>      
        /// <param name="_Map">Input update</param>
        /// <returns></returns>        
        string Updatetbl_Map(tbl_Map _Map);

        /// <summary>
        /// xóa dữ liệu đơn vị thành viên
        /// </summary>      
        /// <param name="_Map">Input delete</param>
        /// <returns></returns>        
        string Deletetbl_Map(tbl_Map _Map);

        /// <summary>
        /// Xóa dữ liệu đơn vị thành viên
        /// </summary>      
        /// <param name="Id">Id slider</param>
        /// <returns></returns>        
        string Deletetbl_Map(int Id);

        /// <summary>
        /// xuat ban đơn vị thành viên
        /// </summary>      
        /// <param name="Id">Id đơn vị thành viên</param>
        /// <returns></returns>        
        string Publictbl_Map(int Id);

    }
}

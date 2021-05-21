using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Group_Doctor
    {
        /// <summary>
        /// danh sách nhóm bác sỹ
        /// </summary>      

        /// <returns></returns>        
        List<cms_Group_Doctor> GetAllcms_Group_Doctor();

        /// <summary>
        /// tìm danh sách nhóm bác sỹ theo ID
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>       
        /// <returns></returns>        
        List<cms_Group_Doctor> Getcms_Group_DoctorByID(int Id);



        /// <summary>
        /// tìm danh sách nhóm bác sỹ theo Name
        /// </summary>      
        /// <param name="Name">Name Tên nhóm</param>       
        /// <returns></returns>        
        List<cms_Group_Doctor> Getcms_Group_DoctorByName(string Name);



        /// <summary>
        /// Chèn dữ liệu nhóm bác sỹ mới
        /// </summary>      
        /// <param name="group_Doctor">Input insert</param>
        /// <returns></returns>        
        string Insertcms_Group_Doctor(cms_Group_Doctor group_Doctor );





        /// <summary>
        /// Cập nhật dữ liệu cho nhóm bác sỹ
        /// </summary>      
        /// <param name="group_Doctor">Input update</param>
        /// <returns></returns>        
        string Updatecms_Group_Doctor(cms_Group_Doctor group_Doctor);


        /// <summary>
        /// xóa dữ liệu nhóm bác sỹ
        /// </summary>      
        /// <param name="group_Doctor">Input delete</param>
        /// <returns></returns>        
        string Deletecms_Group_Doctor(cms_Group_Doctor group_Doctor);

        /// <summary>
        /// Xóa dữ liệu nhóm bác sỹ
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>
        /// <returns></returns>        
        string Deletecms_Group_Doctor(int Id);
        /// <summary>
        /// duyệt dữ liệu nhóm bác sỹ
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>
        /// <returns></returns>        
        string Publiccms_Group_Doctor(int Id);

    }
}

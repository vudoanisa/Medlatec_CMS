using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface Icms_Doctor
    {
        /// <summary>
        /// danh sách bác sỹ
        /// </summary>      

        /// <returns></returns>        
        List<cms_Doctor> GetAllcms_Doctor();

        /// <summary>
        /// tìm danh sách bác sỹ theo ID
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>       
        /// <returns></returns>        
        List<cms_Doctor> Getcms_DoctorByID(int Id);



        /// <summary>
        /// tìm danh sách bác sỹ theo Name
        /// </summary>      
        /// <param name="Name">Name Tên nhóm</param>       
        /// <returns></returns>        
        List<cms_Doctor> Getcms_DoctorByName(string Name);


        /// <summary>
        /// tìm danh sách bác sỹ theo Name
        /// </summary>      
        /// <param name="CateID">ID Nhóm bác sỹ</param>       
        /// <returns></returns>        
        List<cms_Doctor> Getcms_DoctorByCateID(int CateID);



        /// <summary>
        /// Chèn dữ liệu  bác sỹ mới
        /// </summary>      
        /// <param name="_Doctor">Input insert</param>
        /// <returns></returns>        
        string Insertcms_Doctor(cms_Doctor _Doctor );





        /// <summary>
        /// Cập nhật dữ liệu cho  bác sỹ
        /// </summary>      
        /// <param name="_Doctor">Input update</param>
        /// <returns></returns>        
        string Updatecms_Doctor(cms_Doctor _Doctor);


        /// <summary>
        /// xóa dữ liệu  bác sỹ
        /// </summary>      
        /// <param name="_Doctor">Input delete</param>
        /// <returns></returns>        
        string Deletecms_Doctor(cms_Doctor _Doctor);

        /// <summary>
        /// Xóa dữ liệu  bác sỹ
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>
        /// <returns></returns>        
        string Deletecms_Doctor(int Id);
        /// <summary>
        /// duyệt dữ liệu  bác sỹ
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>
        /// <returns></returns>        
        string Publiccms_Doctor(int Id);

    }
}

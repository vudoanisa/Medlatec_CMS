using CMS_Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Interface
{
    interface IAutolink
    {
        /// <summary>
        ///danh sách Autolink 
        /// </summary>      

        /// <returns></returns>        
        List<Autolink> GetAllAutolink();

        /// <summary>
        /// tìm danh sách Autolink  theo ID
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>       
        /// <returns></returns>        
        List<Autolink> GetAutolinkByID(int Id);



        /// <summary>
        /// tìm danh sách danh sách Autolink  Name
        /// </summary>      
        /// <param name="Name">Name Tên nhóm</param>       
        /// <returns></returns>        
        List<Autolink> GetAutolinkByName(string Name);




        /// <summary>
        /// Chèn dữ liệu  Autolink  mới
        /// </summary>      
        /// <param name="autolink">Input insert</param>
        /// <returns></returns>        
        string InsertAutolink(Autolink autolink);





        /// <summary>
        /// Cập nhật dữ liệu cho  Autolink
        /// </summary>      
        /// <param name="autolink">Input update</param>
        /// <returns></returns>        
        string UpdateAutolink(Autolink autolink);


        /// <summary>
        /// xóa dữ liệu Autolink
        /// </summary>      
        /// <param name="autolink">Input delete</param>
        /// <returns></returns>        
        string DeleteAutolink(Autolink autolink);

        /// <summary>
        /// Xóa dữ liệu  Autolink
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>
        /// <returns></returns>        
        string DeleteAutolink(int Id);
        /// <summary>
        /// duyệt dữ liệu Autolink
        /// </summary>      
        /// <param name="Id">ID nhóm bác sỹ</param>
        /// <returns></returns>        
        string PublicAutolink(int Id);
    }
}

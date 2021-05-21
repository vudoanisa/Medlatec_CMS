using IMEX.Core.Models;
using System;
using System.Web;

namespace CMS_Core.Entity
{
    /// <summary>
    /// Từ điển bệnh
    /// </summary>
    public class Cms_DictionaryEntity : EntityBase
    {
        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string GroupName { get; set; }

        [DataInfo]
        public string Url { get; set; }

        [DataInfo]
        public string Image { get; set; }

        [DataInfo]
        public string Description { get; set; }

        [DataInfo]
        public string Content { get; set; }// tổng quan

        [DataInfo]
        public string Cause { get; set; }// nguyên nhân

        [DataInfo]
        public string Symptom { get; set; }//triệu chứng

        [DataInfo]
        public string ObjectGravity { get; set; }//Đối tượng nguy cơ

        [DataInfo]
        public string Prevent { get; set; }//phòng ngừa

        [DataInfo]
        public string Diagnostic { get; set; }//Chuẩn đoán

        [DataInfo]
        public string PathOfInfection { get; set; }//Đường lây nhiễm

        [DataInfo]
        public string Treatment { get; set; }//Điều trị

        [DataInfo]
        public string Tags { get; set; }

        [DataInfo]
        public string NewIds { get; set; }

        [DataInfo]
        public string Authour { get; set; }

        [DataInfo]
        public string Keyword { get; set; }

        [DataInfo]
        public DateTime DateCreated { get; set; }


        [DataInfo]
        public DateTime DatePublish { get; set; }

        [DataInfo]
        public bool IsActive { get; set; }

        [DataInfo]
        public int UserId { get; set; }

        [DataInfo]
        public int DoctorID { get; set; }

        [DataInfo]
        public int Views { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

    }
    public class Cms_DictionaryService : ServiceBase<Cms_DictionaryEntity>
    {
        #region Autogen by RDV

        public Cms_DictionaryService()
            : base("[cms_Dictionary]")
        {

        }

        private static Cms_DictionaryService _instance;
        public static Cms_DictionaryService Instance => _instance ?? (_instance = new Cms_DictionaryService());

        #endregion Autogen by RDV

        public Cms_DictionaryEntity GetById(int id)
        {
            return CreateQuery().Where(o => o.ID == id).ToSingle();
        }
    }
}
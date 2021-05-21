using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS_Core.Entity
{
    [Serializable]
    public class cms_Scientist : IValidatableObject
    {
        #region Constructors  
        public cms_Scientist() { }
        #endregion
        #region Private Fields  
        private int _ID;
        private string _ScientistTitle;
        private string _DatePerform;
        private string _PersionPerform;
        private string _UnitPerform;
        private string _CommissioningCommittee;
        private string _scientistDesc;
        private string _newsImages;
        private int _Status;
        private DateTime _dateCreate;
        private DateTime _datepub;
        private string _Slug;
        private string _newsContent;
        private int _userId;
        private int _ScientistCae;
        private DateTime _dateupdate;
        private string _newsKeyword;
        private string _datepubView;
        

        #endregion
        #region Public Properties  
        public int ID { get { return _ID; } set { _ID = value; } }
        public string ScientistTitle { get { return _ScientistTitle; } set { _ScientistTitle = value; } }
        public string DatePerform { get { return _DatePerform; } set { _DatePerform = value; } }
        public string PersionPerform { get { return _PersionPerform; } set { _PersionPerform = value; } }
        public string UnitPerform { get { return _UnitPerform; } set { _UnitPerform = value; } }
        public string CommissioningCommittee { get { return _CommissioningCommittee; } set { _CommissioningCommittee = value; } }
        public string scientistDesc { get { return _scientistDesc; } set { _scientistDesc = value; } }
        public string newsImages { get { return _newsImages; } set { _newsImages = value; } }
        public int Status { get { return _Status; } set { _Status = value; } }
        public int ScientistCae { get { return _ScientistCae; } set { _ScientistCae = value; } }
        public DateTime dateCreate { get { return _dateCreate; } set { _dateCreate = value; } }
        public DateTime datepub { get { return _datepub; } set { _datepub = value; _datepubView = _datepub.ToString("dd/MM/yyyy HH:mm:ss"); } }

        public string datepubView { get { return _datepubView; } set { _datepubView = value; } }


      
        public string Slug { get { return _Slug; } set { _Slug = value; } }
        public string newsContent { get { return _newsContent; } set { _newsContent = value; } }
        public int userId { get { return _userId; } set { _userId = value; } }
        public DateTime dateupdate { get { return _dateupdate; } set { _dateupdate = value; } }
        public string newsKeyword { get { return _newsKeyword; } set { _newsKeyword = value; } }
        public HttpPostedFileBase ImageFile { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.ScientistTitle))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên đề tài"));
            }
            else if (this.ScientistTitle.Length > 200)
            {
                results.Add(new ValidationResult("Tên đề tài lớn hơn 200 ký tự"));
            }

            if (string.IsNullOrEmpty(this.DatePerform))
            {
                results.Add(new ValidationResult("Mời bạn nhập thời gian thực hiện"));
            }
            else if (this.DatePerform.Length > 50)
            {
                results.Add(new ValidationResult("Thời gian thực hiện lớn hơn 50 ký tự"));
            }

            if (string.IsNullOrEmpty(this.PersionPerform))
            {
                results.Add(new ValidationResult("Mời bạn nhập người thực hiện"));
            }
            else if (this.PersionPerform.Length > 50)
            {
                results.Add(new ValidationResult("Người thực hiện lớn hơn 50 ký tự"));
            }

            if (string.IsNullOrEmpty(this.UnitPerform))
            {
                results.Add(new ValidationResult("Mời bạn nhập đơn vị thực hiện"));
            }
            else if (this.UnitPerform.Length > 100)
            {
                results.Add(new ValidationResult("Đơn vị hiện lớn hơn 100 ký tự"));
            }

            if (string.IsNullOrEmpty(this.CommissioningCommittee))
            {
                results.Add(new ValidationResult("Mời bạn nhập hội đồng nghiệm thu"));
            }
            else if (this.CommissioningCommittee.Length > 500)
            {
                results.Add(new ValidationResult("Mời bạn nhập hội đồng nghiệm thu"));
            }

        


            if (!string.IsNullOrEmpty(this.scientistDesc))
            {
                if (this.scientistDesc.Length > 300)
                {
                    results.Add(new ValidationResult("Mô tả nghiên cứu lớn hơn 300 ký tự"));
                }
            }




            return results;
        }
        #endregion  }
    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    [Serializable]
    public class cms_Scientist_Cate : IValidatableObject
    {
        #region Constructors  
        public cms_Scientist_Cate() { }
        #endregion
        #region Private Fields  
        private int _ID;
        private string _ScientistTitle;
        private string _ScientistDesc;
        private int _Status;
        private DateTime _dateCreate;
        private DateTime _datepub;
        private int _userId;
        private DateTime _dateupdate;
        #endregion
        #region Public Properties  
        public int ID { get { return _ID; } set { _ID = value; } }
        public string ScientistTitle { get { return _ScientistTitle; } set { _ScientistTitle = value; } }
        public string ScientistDesc { get { return _ScientistDesc; } set { _ScientistDesc = value; } }
        public int Status { get { return _Status; } set { _Status = value; } }
        public DateTime dateCreate { get { return _dateCreate; } set { _dateCreate = value; } }
        public DateTime datepub { get { return _datepub; } set { _datepub = value; } }
        public int userId { get { return _userId; } set { _userId = value; } }
        public DateTime dateupdate { get { return _dateupdate; } set { _dateupdate = value; } }

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

            if (string.IsNullOrEmpty(this.ScientistDesc))
            {
                results.Add(new ValidationResult("Mời bạn nhập mô tả chuyên khoa"));
            }
            else if (this.ScientistDesc.Length > 500)
            {
                results.Add(new ValidationResult("Mô tả chuyên khoa lớn hơn 500 ký tự"));
            }

            return results;
        }
        #endregion  }
    }
}

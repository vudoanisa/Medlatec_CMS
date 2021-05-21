using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    [Serializable]
    public class Autolink : IValidatableObject
    {
        #region Constructors  
        public Autolink() { }
        #endregion
        #region Private Fields  
        private int _ID;
        private string _Keyword;
        private string _URL;
        private int _SortOrder;
        private int _MaxReplacements;
        private int _TypeStart;
        private int _Status;
        private DateTime _DateSave;
        #endregion
        #region Public Properties  
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Keyword { get { return _Keyword; } set { _Keyword = value; } }
        public string URL { get { return _URL; } set { _URL = value; } }
        public int SortOrder { get { return _SortOrder; } set { _SortOrder = value; } }
        public int MaxReplacements { get { return _MaxReplacements; } set { _MaxReplacements = value; } }
        public int TypeStart { get { return _TypeStart; } set { _TypeStart = value; } }
        public int Status { get { return _Status; } set { _Status = value; } }
        public DateTime DateSave { get { return _DateSave; } set { _DateSave = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Keyword))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào từ khóa"));
            }
            else if (this.Keyword.Length > 300)
            {
                results.Add(new ValidationResult("Từ khóa lớn hơn 300 ký tự"));
            }

            if (string.IsNullOrEmpty(this.URL))
            {
                results.Add(new ValidationResult("Mời bạn nhập link "));
            }
            else if (this.URL.Length > 300)
            {
                results.Add(new ValidationResult("Link lớn hơn 300 ký tự"));
            }

            if(SortOrder < 1)
            {
                results.Add(new ValidationResult("Thứ tự xuất hiện phải lớn hơn 0"));
            }


            if (MaxReplacements < 1)
            {
                results.Add(new ValidationResult("Số lượng 1 từ khóa được chèn link phải lớn hơn 0"));
            }
            if (MaxReplacements > 6)
            {
                results.Add(new ValidationResult("Số lượng 1 từ khóa được chèn link phải nhỏ hơn 6"));
            }


            return results;
        }
        #endregion
    }
}
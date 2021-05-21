using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    [Serializable]
    public class cms_VideoCate : IValidatableObject
    {
        #region Constructors  
        public cms_VideoCate() { }
        #endregion
        #region Private Fields  
        private int _vId;
        private string _vName;
        private string _vTitle;
        private string _vDescription;
        private string _vKeyword;
        private bool _vActive;
        #endregion
        #region Public Properties  
        public int vId { get { return _vId; } set { _vId = value; } }
        public string vName { get { return _vName; } set { _vName = value; } }
        public string vTitle { get { return _vTitle; } set { _vTitle = value; } }
        public string vDescription { get { return _vDescription; } set { _vDescription = value; } }
        public string vKeyword { get { return _vKeyword; } set { _vKeyword = value; } }
        public bool vActive { get { return _vActive; } set { _vActive = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.vName))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào chuyên mục video"));
            }
            else if (this.vName.Length > 200)
            {
                results.Add(new ValidationResult("Chuyên mục video lớn hơn 200 ký tự"));
            }

            if (string.IsNullOrEmpty(this.vTitle))
            {
                results.Add(new ValidationResult("Mời bạn nhập tiêu đề chuyên mục video"));
            }
            else if (this.vTitle.Length > 200)
            {
                results.Add(new ValidationResult("Tiêu đề chuyên mục video lớn hơn 200 ký tự"));
            }

            if (!string.IsNullOrEmpty(vDescription))
            {
                if (this.vDescription.Length > 500)
                {
                    results.Add(new ValidationResult("Mô tả chuyên mục video lớn hơn 500 ký tự"));
                }
            }

            if (!string.IsNullOrEmpty(vKeyword))
            {
                if (this.vKeyword.Length > 200)
                {
                    results.Add(new ValidationResult("Từ khóa chuyên mục video lớn hơn 200 ký tự"));
                }
            }


            return results;
        }
        #endregion

    }
}

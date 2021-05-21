using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    [Serializable]
    public class tbl_YnghiaTestcode : IValidatableObject
    {
        #region Constructors  
        public tbl_YnghiaTestcode() { }
        #endregion
        #region Private Fields  
        private int _ID;
        private string _Testcode;
        private string _TestName;
        private string _UserI;
        private string _DateIn;
        private string _UserU;
        private string _DateU;
        private bool _Active;
        private string _LQ1;
        private string _LQ2;
        private string _LQ3;
        private string _Ynghia;
        private string _Price;
        private int _status;
        private string _ynghia1;
        private string _createby;
        private DateTime _createdate;
        private string _updateby;
        private DateTime _updatedate;
        private string _linkUrl;
        #endregion
        #region Public Properties  
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Testcode { get { return _Testcode; } set { _Testcode = value; } }
        public string TestName { get { return _TestName; } set { _TestName = value; } }
        public string UserI { get { return _UserI; } set { _UserI = value; } }
        public string DateIn { get { return _DateIn; } set { _DateIn = value; } }
        public string UserU { get { return _UserU; } set { _UserU = value; } }
        public string DateU { get { return _DateU; } set { _DateU = value; } }
        public bool Active { get { return _Active; } set { _Active = value; } }
        public string LQ1 { get { return _LQ1; } set { _LQ1 = value; } }
        public string LQ2 { get { return _LQ2; } set { _LQ2 = value; } }
        public string LQ3 { get { return _LQ3; } set { _LQ3 = value; } }
        public string Ynghia { get { return _Ynghia; } set { _Ynghia = value; } }
        public string Price { get { return _Price; } set { _Price = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public string ynghia1 { get { return _ynghia1; } set { _ynghia1 = value; } }
        public string createby { get { return _createby; } set { _createby = value; } }
        public DateTime createdate { get { return _createdate; } set { _createdate = value; } }
        public string updateby { get { return _updateby; } set { _updateby = value; } }
        public DateTime updatedate { get { return _updatedate; } set { _updatedate = value; } }
        public string linkUrl { get { return _linkUrl; } set { _linkUrl = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Testcode))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào mã TestCode"));
            }
            else if (this.Testcode.Length > 200)
            {
                results.Add(new ValidationResult("TestCode lớn hơn 15 ký tự"));
            }

            if (string.IsNullOrWhiteSpace(this.TestName))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên xét nghiệm TestName"));
            }
            else if (this.TestName.Length > 150)
            {
                results.Add(new ValidationResult("Tên xét nghiệm TestName lớn hơn 150 ký tự"));
            }

            if (!string.IsNullOrWhiteSpace(this.linkUrl))
            {
                if (this.linkUrl.Length > 250)
                {
                    results.Add(new ValidationResult("Đường link ý nghĩa xét nghiệm lớn hơn 250 ký tự"));
                }
            }

            if (string.IsNullOrWhiteSpace(this.Ynghia))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào ý nghĩa xét nghiệm "));
            }

            if (string.IsNullOrWhiteSpace(this.ynghia1))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào ý nghĩa xét nghiệm  chuyên sâu "));
            }

            return results;

        }
        #endregion  }
    }

}
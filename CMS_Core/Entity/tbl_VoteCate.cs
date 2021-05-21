using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS_Core.Entity
{
    public class tbl_VoteCate : IValidatableObject
    {
        #region Constructors  
        public tbl_VoteCate() { }
        #endregion
        #region Private Fields  
        private int _ID;
        private string _VoteCate;
        private string _VoteCateDesc;
        private int _status;
        private string _createby;
        private DateTime _createdate;
        private string _updateby;
        private DateTime _updatedate;
        private DateTime _datesart;
        private DateTime _dataend;
        private string _datesartView;
        private string _dataendView;
        private string _CodeHIS;
        private string _CodeName;
        private string _ImagePath;
        #endregion
        #region Public Properties  
        public int ID { get { return _ID; } set { _ID = value; } }
        public string VoteCate { get { return _VoteCate; } set { _VoteCate = value; } }

        public string ImagePath { get { return _ImagePath; } set { _ImagePath = value; } }
        public string VoteCateDesc { get { return _VoteCateDesc; } set { _VoteCateDesc = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public string createby { get { return _createby; } set { _createby = value; } }
        public DateTime createdate { get { return _createdate; } set { _createdate = value; } }
        public string updateby { get { return _updateby; } set { _updateby = value; } }
        public DateTime updatedate { get { return _updatedate; } set { _updatedate = value; } }
       
        public DateTime datesart { get { return _datesart; } set { _datesart = value; _datesartView = _datesart.ToString("dd/MM/yyyy"); } }
        public DateTime dataend { get { return _dataend; } set { _dataend = value; _dataendView = _dataend.ToString("dd/MM/yyyy"); } }
        public string datesartView { get { return _datesartView; } set { _datesartView = value; } }
        public string dataendView { get { return _dataendView; } set { _dataendView = value; } }


        public HttpPostedFileBase ImageFile { get; set; }
        public string CodeHIS { get { return _CodeHIS; } set { _CodeHIS = value; } }
        public string CodeName { get { return _CodeName; } set { _CodeName = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.VoteCate))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên nhóm khảo sát"));
            }
            else if (this.VoteCate.Length > 200)
            {
                results.Add(new ValidationResult("Tên nhóm khảo sát lớn hơn 200 ký tự"));
            }

            if (!string.IsNullOrEmpty(this.VoteCateDesc))
            {
                if (this.VoteCateDesc.Length > 500)
                {
                    results.Add(new ValidationResult("Mời bạn nhập mô tả dịch vụ"));
                }
            }



            return results;
        }
        #endregion  }
    }
}

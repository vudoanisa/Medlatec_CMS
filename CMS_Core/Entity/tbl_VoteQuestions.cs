using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    public class tbl_VoteQuestions : IValidatableObject
    {
        #region Constructors  
        public tbl_VoteQuestions() { }
        #endregion
        #region Private Fields  
        private int _ID;
        private string _VoteQuestion1;
        private string _VoteQuestion2;
        private string _VoteQuestion3;
        private string _VoteQuestion4;
        private string _VoteQuestion5;
        private string _VoteQuestion6;
        private string _VoteQuestion7;
        private string _VoteQuestion8;
        private string _createby;
        private DateTime _creatdate;
        private string _updateby;
        private DateTime _updatedate;
        private int _VoteID;
        #endregion
        #region Public Properties  
        public int ID { get { return _ID; } set { _ID = value; } }
        public string VoteQuestion1 { get { return _VoteQuestion1; } set { _VoteQuestion1 = value; } }
        public string VoteQuestion2 { get { return _VoteQuestion2; } set { _VoteQuestion2 = value; } }
        public string VoteQuestion3 { get { return _VoteQuestion3; } set { _VoteQuestion3 = value; } }
        public string VoteQuestion4 { get { return _VoteQuestion4; } set { _VoteQuestion4 = value; } }
        public string VoteQuestion5 { get { return _VoteQuestion5; } set { _VoteQuestion5 = value; } }
        public string VoteQuestion6 { get { return _VoteQuestion6; } set { _VoteQuestion6 = value; } }
        public string VoteQuestion7 { get { return _VoteQuestion7; } set { _VoteQuestion7 = value; } }
        public string VoteQuestion8 { get { return _VoteQuestion8; } set { _VoteQuestion8 = value; } }
        public string createby { get { return _createby; } set { _createby = value; } }
        public DateTime creatdate { get { return _creatdate; } set { _creatdate = value; } }
        public string updateby { get { return _updateby; } set { _updateby = value; } }
        public DateTime updatedate { get { return _updatedate; } set { _updatedate = value; } }
        public int VoteID { get { return _VoteID; } set { _VoteID = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.VoteQuestion1))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào câu trả lời khảo sát"));
            }
            else if (this.VoteQuestion1.Length > 200)
            {
                results.Add(new ValidationResult("Câu khảo sát lớn hơn 200 ký tự"));
            }

            if (this.VoteID <= 0)
            {
                results.Add(new ValidationResult("Mời bạn chọn ID câu hỏi"));
            }

            return results;
        }
        #endregion  }
    }
}

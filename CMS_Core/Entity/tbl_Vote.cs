using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{

    public class tbl_Vote : IValidatableObject
    {
        #region Constructors  
        public tbl_Vote() { }
        #endregion
        #region Private Fields  
        private int _ID;
        private string _VoteName;
        private string _VoteDesc;
        private DateTime _datesart;
        private DateTime _dataend;
        private string _datesartView;
        private string _dataendView;

        private int _VoteAnswer;
        private int _status;
        private string _createby;
        private DateTime _createdate;
        private string _updateby;
        private DateTime _updatedate;
        private int _VoteCateID;
        private string _linkExample;
        private string _CodeHIS;
        private string _CodeName;
        private int _TypeVote;
        
        #endregion
        #region Public Properties  
        public int ID { get { return _ID; } set { _ID = value; } }
        public int TypeVote { get { return _TypeVote; } set { _TypeVote = value; } }
        public string VoteName { get { return _VoteName; } set { _VoteName = value; } }
        public string datesartView { get { return _datesartView; } set { _datesartView = value; } }
        public string dataendView { get { return _dataendView; } set { _dataendView = value; } }

        public string VoteDesc { get { return _VoteDesc; } set { _VoteDesc = value; } }
        public string CodeHIS { get { return _CodeHIS; } set { _CodeHIS = value; } }
        public string CodeName { get { return _CodeName; } set { _CodeName = value; } }

        public DateTime datesart { get { return _datesart; } set { _datesart = value; } }
        public DateTime dataend { get { return _dataend; } set { _dataend = value; } }
        public int VoteAnswer { get { return _VoteAnswer; } set { _VoteAnswer = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public string createby { get { return _createby; } set { _createby = value; } }
        public DateTime createdate { get { return _createdate; } set { _createdate = value; } }
        public string updateby { get { return _updateby; } set { _updateby = value; } }
        public DateTime updatedate { get { return _updatedate; } set { _updatedate = value; } }
        public int VoteCateID { get { return _VoteCateID; } set { _VoteCateID = value; } }
        public string linkExample { get { return _linkExample; } set { _linkExample = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.VoteName))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên  khảo sát"));
            }
            else if (this.VoteName.Length > 200)
            {
                results.Add(new ValidationResult("Tên khảo sát lớn hơn 200 ký tự"));
            }

            if (!string.IsNullOrEmpty(this.VoteDesc))
            {
                if (this.VoteDesc.Length > 500)
                {
                    results.Add(new ValidationResult("Mời bạn nhập mô tả khảo sát"));
                }
            }

            if (VoteAnswer <= 0)
            {
                results.Add(new ValidationResult("Mời bạn nhập câu trả lời"));
            }

            if (datesart > dataend)
            {
                results.Add(new ValidationResult("Thời gian bắt đầu lớn hơn thời gian kết thúc"));
            }

            return results;
        }
        #endregion  }

    }
}

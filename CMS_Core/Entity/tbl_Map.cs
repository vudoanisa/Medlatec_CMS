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
    public class tbl_Map : IValidatableObject
    {
        #region Constructors  
        public tbl_Map() { }
        #endregion
        #region Private Fields  
        private int _ID;
        private string _TenPK;
        private string _Diachi;
        private string _Img;
        private string _Giamdoc;
        private string _Hotline;
        private string _Website;
        private string _Fanpage;
        private string _Lo;
        private string _Lu;
        private string _Truso;
        private string _Khuvuc;
        private string _Thongso;
        private int _status;
        private string _createby;
        private DateTime _createdate;
        private string _updateby;
        private DateTime _updatetime;
        private int _type;
        #endregion
        #region Public Properties  
        public int ID { get { return _ID; } set { _ID = value; } }
        public int type { get { return _type; } set { _type = value; } }
        public string TenPK { get { return _TenPK; } set { _TenPK = value; } }
        public string Diachi { get { return _Diachi; } set { _Diachi = value; } }
        public string Img { get { return _Img; } set { _Img = value; } }
        public string Giamdoc { get { return _Giamdoc; } set { _Giamdoc = value; } }
        public string Hotline { get { return _Hotline; } set { _Hotline = value; } }
        public string Website { get { return _Website; } set { _Website = value; } }
        public string Fanpage { get { return _Fanpage; } set { _Fanpage = value; } }
        public string Lo { get { return _Lo; } set { _Lo = value; } }
        public string Lu { get { return _Lu; } set { _Lu = value; } }
        public string Truso { get { return _Truso; } set { _Truso = value; } }
        public string Khuvuc { get { return _Khuvuc; } set { _Khuvuc = value; } }
        public string Thongso { get { return _Thongso; } set { _Thongso = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public string createby { get { return _createby; } set { _createby = value; } }
        public DateTime createdate { get { return _createdate; } set { _createdate = value; } }
        public string updateby { get { return _updateby; } set { _updateby = value; } }
        public DateTime updatetime { get { return _updatetime; } set { _updatetime = value; } }
        public HttpPostedFileBase ImageFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.TenPK))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên phòng khám"));
            }
            else if (this.TenPK.Length > 250)
            {
                results.Add(new ValidationResult("Tên phòng khám lớn hơn 250 ký tự"));
            }

            if (string.IsNullOrWhiteSpace(this.Diachi))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào địa chỉ phòng khám"));
            }
            else if (this.Diachi.Length > 500)
            {
                results.Add(new ValidationResult("Địa chỉ phòng khám lớn hơn 500 ký tự"));
            }

            if (string.IsNullOrWhiteSpace(this.Website))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào website phòng khám"));
            }

            if (string.IsNullOrWhiteSpace(this.Fanpage))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào Fanpage phòng khám"));
            }

            if (string.IsNullOrWhiteSpace(this.Truso))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào Trụ sở phòng khám"));
            }

            return results;
        }
        #endregion  }
    }
}
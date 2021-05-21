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
    public class tbl_slider : IValidatableObject
    {
        #region Constructors  
        public tbl_slider() { }
        #endregion
        #region Private Fields  
        private int _ID;
        private string _sliderTitle;
        private string _sliderUrl;
        private string _sliderImage;
        private int _status;
        private string _createby;
        private DateTime _createdate;
        private string _udateby;
        private DateTime _updatedate;
        private string _sliderImageWeb;
        #endregion
        #region Public Properties  
        public int ID { get { return _ID; } set { _ID = value; } }
        public string sliderTitle { get { return _sliderTitle; } set { _sliderTitle = value; } }
        public string sliderUrl { get { return _sliderUrl; } set { _sliderUrl = value; } }
        public string sliderImage { get { return _sliderImage; } set { _sliderImage = value; } }
        public string sliderImageWeb { get { return _sliderImageWeb; } set { _sliderImageWeb = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public string createby { get { return _createby; } set { _createby = value; } }
        public DateTime createdate { get { return _createdate; } set { _createdate = value; } }
        public string udateby { get { return _udateby; } set { _udateby = value; } }
        public DateTime updatedate { get { return _updatedate; } set { _updatedate = value; } }
        public HttpPostedFileBase ImageFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.sliderTitle))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên slider"));
            }
            else if (this.sliderTitle.Length > 200)
            {
                results.Add(new ValidationResult("slider lớn hơn 200 ký tự"));
            }

            if (string.IsNullOrWhiteSpace(this.sliderUrl))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào đường link slider"));
            }
            else if (this.sliderUrl.Length > 200)
            {
                results.Add(new ValidationResult("Đường link Slider lớn hơn 200 ký tự"));
            }

           


            return results;
        }
        #endregion  }
    }
}
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
    public class cms_Images : IValidatableObject
    {
        #region Constructors  
        public cms_Images() { }
        #endregion
        #region Private Fields  
        private int _videoId;
        private string _VideoName;
        private string _VideoTitle;
        private string _VideoDescription;
        private int _VideoType;
        private string _VideoFile;
        private string _VideoCode;
        private string _VideoImageThumb;
        private int _VideoWidth;
        private int _VideoHight;
        private int _vId;
        private bool _VideoActive;
        private DateTime _DatePublic;
        private string _datepubView;
        private bool _delegate;
        private int _videoParrentID;

        #endregion
        #region Public Properties  
        public int videoId { get { return _videoId; } set { _videoId = value; } }
        public string VideoName { get { return _VideoName; } set { _VideoName = value; } }
        public string VideoTitle { get { return _VideoTitle; } set { _VideoTitle = value; } }
        public string VideoDescription { get { return _VideoDescription; } set { _VideoDescription = value; } }
        public int VideoType { get { return _VideoType; } set { _VideoType = value; } }
        public string VideoFile { get { return _VideoFile; } set { _VideoFile = value; } }
        public string VideoCode { get { return _VideoCode; } set { _VideoCode = value; } }
        public string VideoImageThumb { get { return _VideoImageThumb; } set { _VideoImageThumb = value; } }
        public int VideoWidth { get { return _VideoWidth; } set { _VideoWidth = value; } }
        public int VideoHight { get { return _VideoHight; } set { _VideoHight = value; } }
        public int vId { get { return _vId; } set { _vId = value; } }
        public bool VideoActive { get { return _VideoActive; } set { _VideoActive = value; } }
        public DateTime DatePublic { get { return _DatePublic; } set { _DatePublic = value; _datepubView = _DatePublic.ToString("dd/MM/yyyy HH:mm:ss"); } }
        public string datepubView { get { return _datepubView; } set { _datepubView = value; } }

        public bool Delegate { get { return _delegate; } set { _delegate = value; } }
        public int videoParrentID { get { return _videoParrentID; } set { _videoParrentID = value; } }

        public HttpPostedFileBase ImageFile { get; set; }

        public HttpPostedFileBase VideoFilePath { get; set; }

        public string FilesToBeUploaded { get; set; }


        public List<cms_Images> _Images  { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.VideoName))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên ảnh"));
            }
            else if (this.VideoName.Length > 300)
            {
                results.Add(new ValidationResult("Tên ảnh lớn hơn 300 ký tự"));
            }

            if (string.IsNullOrEmpty(this.VideoTitle))
            {
                results.Add(new ValidationResult("Mời bạn nhập tiêu đề ảnh"));
            }
            else if (this.VideoTitle.Length > 300)
            {
                results.Add(new ValidationResult("Tiêu đề ảnh lớn hơn 300 ký tự"));
            }

            if (!string.IsNullOrEmpty(this.VideoDescription))
            {
                if (this.VideoDescription.Length > 600)
                {
                    results.Add(new ValidationResult("Mô tả ảnh lớn hơn 600 ký tự"));
                }
            }


            if (this.vId == 0)
            {
                results.Add(new ValidationResult("Mời bạn chọn loại ảnh"));
            }


            return results;
        }
        #endregion
    }
}

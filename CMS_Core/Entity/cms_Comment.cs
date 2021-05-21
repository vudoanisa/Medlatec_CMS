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
    public class cms_Comment : IValidatableObject
    {
        #region Constructors  
        public cms_Comment() { }
        #endregion
        #region Private Fields  
        private int _CommentId;
        private int _NewsId;
        private string _FullName;
        private string _Email;
        private string _Title;
        private string _Content;
        private DateTime _DateCreate;
        private bool _Activate;
        private string _linkNews;
        private string _ImgAvatar;
        private string _linkFB;
        private int _type;
        private int _CommentParent;
        private int _status;
        private int _CreateBy;
        private string _newsName;
        
        #endregion
        #region Public Properties  
        public HttpPostedFileBase ImageFile { get; set; }
        public int CommentId { get { return _CommentId; } set { _CommentId = value; } }
        public int NewsId { get { return _NewsId; } set { _NewsId = value; } }
        public string FullName { get { return _FullName; } set { _FullName = value; } }
        public string newsName { get { return _newsName; } set { _newsName = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string Title { get { return _Title; } set { _Title = value; } }
        public string Content { get { return _Content; } set { _Content = value; } }
        public DateTime DateCreate { get { return _DateCreate; } set { _DateCreate = value; } }
        public bool Activate { get { return _Activate; } set { _Activate = value; } }
        public string linkNews { get { return _linkNews; } set { _linkNews = value; } }
        public string ImgAvatar { get { return _ImgAvatar; } set { _ImgAvatar = value; } }
        public string linkFB { get { return _linkFB; } set { _linkFB = value; } }
        public int type { get { return _type; } set { _type = value; } }
        public int CommentParent { get { return _CommentParent; } set { _CommentParent = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public int CreateBy { get { return _CreateBy; } set { _CreateBy = value; } }
        #endregion  
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (NewsId <= 0)
            {
                results.Add(new ValidationResult("Mời bạn chọn id tin bài"));
            }
            if (string.IsNullOrEmpty(this.FullName))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên người nhập"));
            }
            else if (this.FullName.Length > 200)
            {
                results.Add(new ValidationResult("Tên người nhập lớn hơn 200 ký tự mời nhập lại!"));
            }

            else if (this.FullName.Length < 5)
            {
                results.Add(new ValidationResult("Tên người nhập lớn nhỏ hơn 5 ký tự mời nhập lại!"));
            }

            if (string.IsNullOrEmpty(this.Title))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tiêu đề comment"));
            }
            else if (this.Title.Length > 200)
            {
                results.Add(new ValidationResult("Tiêu đề comment lớn hơn 200 ký tự mời nhập lại!"));
            }

            else if (this.Title.Length < 5)
            {
                results.Add(new ValidationResult("Tiêu đề comment  nhỏ hơn 5 ký tự mời nhập lại!"));
            }


            if (string.IsNullOrEmpty(this.Content))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào nội dung comment"));
            }
            

            else if (this.Content.Length < 10)
            {
                results.Add(new ValidationResult("Nội dung comment nhỏ hơn 10 ký tự mời nhập lại!"));
            }

            if (!string.IsNullOrEmpty(this.linkFB))
            {
                if(this.linkFB.IndexOf("https") == -1)
                {
                    results.Add(new ValidationResult("Link FB phải có https"));
                }
            }


            return results;
        }
    }
}

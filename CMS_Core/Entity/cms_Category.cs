using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS_Core.Entity
{
    [Serializable]
    public class cms_Category : IValidatableObject
    {
        #region Constructors  
        public cms_Category() { }
        #endregion
        #region Private Fields  
        private int _cateId;
        private string _cateName;
        private string _cateLink;
        private string _cateKeyword;
        private string _cateDescription;
        private string _cateImages;
        private string _cateLevel;
        private string _cateLevelParent;
        private int _cateOrd;
        private string _cateLang;
        private bool _isShowHome;
        private bool _cateActive;
        private int _cateStatus;
        private string _ListColumnDecrypt;
        private string _KeyDecrypt;
        private string _TitleSeo;
        #endregion
        #region Public Properties  
        public int cateId { get { return _cateId; } set { _cateId = value; } }
        public string cateName { get { return _cateName; } set { _cateName = value; } }
        public string cateLink { get { return _cateLink; } set { _cateLink = value; } }

        public string cateKeyword { get { return _cateKeyword; } set { _cateKeyword = value; } }
        public string cateDescription { get { return _cateDescription; } set { _cateDescription = value; } }
        public string cateImages { get { return _cateImages; } set { _cateImages = value; } }
        public string cateLevel
        {
            get { return _cateLevel; }
            set
            {
                _cateLevel = value; if (!string.IsNullOrEmpty(_cateLevel))
                {
                    if (_cateLevel.Length > 5)
                    {
                        cateLevelParent = _cateLevel.Substring(0, 5);
                    }
                }
            }
        }
        public string cateLevelParent { get { return _cateLevelParent; } set { _cateLevelParent = value; } }
        public int cateOrd { get { return _cateOrd; } set { _cateOrd = value; } }
        public string cateLang { get { return _cateLang; } set { _cateLang = value; } }
        public bool isShowHome { get { return _isShowHome; } set { _isShowHome = value; } }
        public bool cateActive { get { return _cateActive; } set { _cateActive = value; } }
        public int cateStatus { get { return _cateStatus; } set { _cateStatus = value; } }
        public string ListColumnDecrypt { get { return _ListColumnDecrypt; } set { _ListColumnDecrypt = value; } }
        public string KeyDecrypt { get { return _KeyDecrypt; } set { _KeyDecrypt = value; } }

        public string TitleSeo { get { return _TitleSeo; } set { _TitleSeo = value; } }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.cateName))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên chuyên mục"));
            }
            else if (this.cateName.Length > 200)
            {
                results.Add(new ValidationResult("Tên chuyên mục lớn hơn 200 ký tự"));
            }

            //if (string.IsNullOrEmpty(this.cateLink))
            //{
            //    results.Add(new ValidationResult("Mời bạn nhập vào link chuyên mục"));
            //}
            //else if (this.cateLink.Length > 200)
            //{
            //    results.Add(new ValidationResult("Link chuyên mục lớn hơn 200 ký tự"));
            //}


            if (!string.IsNullOrEmpty(this.cateKeyword))
            {
                if (this.cateKeyword.Length > 300)
                {
                    results.Add(new ValidationResult("Từ khóa chuyên mục không được dài quá 300 ký tự"));
                }
            }

            if (!string.IsNullOrEmpty(this.TitleSeo))
            {
                if (this.TitleSeo.Length > 300)
                {
                    results.Add(new ValidationResult("Tiêu đề seo chuyên mục không được dài quá 300 ký tự"));
                }
            }


            if (!string.IsNullOrEmpty(this.cateDescription))
            {
                if (this.cateDescription.Length > 200)
                {
                    results.Add(new ValidationResult("Mô tả chuyên mục không được dài quá 200 ký tự"));
                }
            }
            if (this.cateOrd < 1)
            {
                results.Add(new ValidationResult("Vị trí phải lớn hơn 1"));
            }

            return results;
        }
        #endregion
    }
}

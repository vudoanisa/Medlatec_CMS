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
    public  class cms_Group_Doctor : IValidatableObject
    {
        #region Constructors  
        public cms_Group_Doctor() { }
        #endregion
        #region Private Fields  
        private int _id;
        private string _Name;
        private string _Image;
        private string _Desc;
        private string _keyword;
        private int _ord;
        private bool _Active;
        private DateTime _DateSave;
        #endregion
        #region Public Properties  
        public int id { get { return _id; } set { _id = value; } }
        public string Name { get { return _Name; } set { _Name = value.Trim(); } }
        public string Image { get { return _Image; } set { _Image = value; } }
        public string Desc { get { return _Desc; } set { _Desc = value; } }
        public string keyword { get { return _keyword; } set { _keyword = value; } }
        public int ord { get { return _ord; } set { _ord = value; } }
        public bool Active { get { return _Active; } set { _Active = value; } }
        public DateTime DateSave { get { return _DateSave; } set { _DateSave = value; } }
        public HttpPostedFileBase ImageFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên nhóm bác sỹ"));
            }
            else if (this.Name.Trim().Length > 200)
            {
                results.Add(new ValidationResult("Tên nhóm bác sỹ lớn hơn 200 ký tự"));
            }

            if (!string.IsNullOrEmpty(Desc))
            {
                if (this.Desc.Length > 1000)
                {
                    results.Add(new ValidationResult("Trích dẫn nhóm bác sỹ lớn hơn 1000 ký tự"));
                }
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                if (this.keyword.Length > 500)
                {
                    results.Add(new ValidationResult("Từ khóa nhóm bác sỹ lớn hơn 500 ký tự"));
                }
            }

            return results;
        }

        #endregion

    }
}

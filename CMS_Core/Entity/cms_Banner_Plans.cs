using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    [Serializable]
    public class cms_Banner_Plans : IValidatableObject
    {
        #region Constructors  
        public cms_Banner_Plans() { }
        #endregion
        #region Private Fields  
        private int _planId;
        private string _planTitle;
        private string _planDescription;
        private string _planForm;
        private int _width;
        private int _height;
        private string _lang;
        private bool _active;
        #endregion
        #region Public Properties  
        public int planId { get { return _planId; } set { _planId = value; } }
        public string planTitle { get { return _planTitle; } set { _planTitle = value; } }
        public string planDescription { get { return _planDescription; } set { _planDescription = value; } }
        public string planForm { get { return _planForm; } set { _planForm = value; } }
        public int width { get { return _width; } set { _width = value; } }
        public int height { get { return _height; } set { _height = value; } }
        public string lang { get { return _lang; } set { _lang = value; } }
        public bool active { get { return _active; } set { _active = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.planTitle))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào vị trí quảng cáo"));
            }
            else if (this.planTitle.Length > 200)
            {
                results.Add(new ValidationResult("Vị trí quảng cáo lớn hơn 200 ký tự"));
            }
            
            if (!string.IsNullOrEmpty(planDescription))
            {
                if (this.planDescription.Length > 200)
                {
                    results.Add(new ValidationResult("Mô tả vị trí quảng cáo lớn hơn 200 ký tự"));
                }
            }
          
            return results;
        }
        #endregion
    }
}

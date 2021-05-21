using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    [Serializable]
    public class ServiceGroup : IValidatableObject
    {
        #region Constructors  
        public ServiceGroup() { }
        #endregion
        #region Private Fields  
        private int _ServiceGroupID;
        private string _Name;
        private string _Description;
        private int _status;
        private DateTime _DateCreate;
        private int _OrderNum;
        private string _userid;
        #endregion
        #region Public Properties  
        public int ServiceGroupID { get { return _ServiceGroupID; } set { _ServiceGroupID = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public DateTime DateCreate { get { return _DateCreate; } set { _DateCreate = value; } }
        public int OrderNum { get { return _OrderNum; } set { _OrderNum = value; } }
        public string userid { get { return _userid; } set { _userid = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Name))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên nhóm"));
            }


            return results;
        }
        #endregion  }
    }
}

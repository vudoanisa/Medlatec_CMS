using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    [Serializable]
    public class tbl_logSearch : IValidatableObject
    {
        #region Constructors  
        public tbl_logSearch() { }
        #endregion
        #region Private Fields  
        private int _id;
        private string _Username;
        private string _IP;
        private string _doctorID;
        private DateTime _datesave;
        #endregion
        #region Public Properties  
        public int id { get { return _id; } set { _id = value; } }
        public string Username { get { return _Username; } set { _Username = value; } }
        public string IP { get { return _IP; } set { _IP = value; } }
        public string doctorID { get { return _doctorID; } set { _doctorID = value; } }
        public DateTime datesave { get { return _datesave; } set { _datesave = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
        #endregion  }
    }
}

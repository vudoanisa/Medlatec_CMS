using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    [Serializable]
    public class cms_Doctor_Cate : IValidatableObject
    {
        #region Constructors  
        public cms_Doctor_Cate() { }
        #endregion
        #region Private Fields  
        private int _id;
        private string _DoctorName;
        private string _DoctorAddress;
        private string _DoctorImage;
        private string _DoctorEmail;
        private string _DoctorMobile;
        private string _DoctorWorkPlace;
        private string _DoctorInfo;
        private string _DoctorPosition;
        private int _CateID;
        private bool _delegate;
        private bool _Active;
        private DateTime _DateSave;
        #endregion
        #region Public Properties  
        public int id { get { return _id; } set { _id = value; } }
        public string DoctorName { get { return _DoctorName; } set { _DoctorName = value.Trim(); } }
        public string DoctorAddress { get { return _DoctorAddress; } set { _DoctorAddress = value; } }
        public string DoctorImage { get { return _DoctorImage; } set { _DoctorImage = value; } }
        public string DoctorEmail { get { return _DoctorEmail; } set { _DoctorEmail = value; } }
        public string DoctorMobile { get { return _DoctorMobile; } set { _DoctorMobile = value; } }
        public string DoctorWorkPlace { get { return _DoctorWorkPlace; } set { _DoctorWorkPlace = value; } }
        public string DoctorInfo { get { return _DoctorInfo; } set { _DoctorInfo = value; } }
        public string DoctorPosition { get { return _DoctorPosition; } set { _DoctorPosition = value; } }
        public int CateID { get { return _CateID; } set { _CateID = value; } }
        public bool Active { get { return _Active; } set { _Active = value; } }
        public DateTime DateSave { get { return _DateSave; } set { _DateSave = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

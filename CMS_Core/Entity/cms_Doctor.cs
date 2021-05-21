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
    public class cms_Doctor : IValidatableObject
    {
        #region Constructors  
        public cms_Doctor() { }
        #endregion
        #region Private Fields  
        private int _id;
        private string _DoctorName;
        private int _DoctorCate;
        private string _DoctorAddress;
        private string _DoctorImage;
        private string _DoctorEmail;
        private string _DoctorMobile;
        private string _DoctorWorkPlace;
        private string _DoctorInfo;
        private string _DoctorPosition;
        private string _DoctorExperience; private string _DoctorLever;
        private string _DoctorAwards;
        private string _DoctorResearchWorks;
        private string _DoctorServices;
        private string _DoctorTraining;
        private string _DoctorOrganization;
        private string _DoctorLanguage;
        private int _DoctorOrd;
        private bool _Active;
        private DateTime _DateSave;
        private string _cateIdList;
        #endregion
        #region Public Properties  
        public int id { get { return _id; } set { _id = value; } }
        public string DoctorName { get { return _DoctorName; } set { _DoctorName = value; } }
        public int DoctorCate { get { return _DoctorCate; } set { _DoctorCate = value; } }
        public string DoctorAddress { get { return _DoctorAddress; } set { _DoctorAddress = value; } }
        public string DoctorImage { get { return _DoctorImage; } set { _DoctorImage = value; } }
        public string DoctorEmail { get { return _DoctorEmail; } set { _DoctorEmail = value; } }
        public string DoctorMobile { get { return _DoctorMobile; } set { _DoctorMobile = value; } }
        public string DoctorWorkPlace { get { return _DoctorWorkPlace; } set { _DoctorWorkPlace = value; } }
        public string DoctorInfo { get { return _DoctorInfo; } set { _DoctorInfo = value; } }
        public string DoctorPosition { get { return _DoctorPosition; } set { _DoctorPosition = value; } }
        public string DoctorExperience { get { return _DoctorExperience; } set { _DoctorExperience = value; } }
        public string DoctorLever { get { return _DoctorLever; } set { _DoctorLever = value; } }
        public string DoctorAwards { get { return _DoctorAwards; } set { _DoctorAwards = value; } }
        public string DoctorResearchWorks { get { return _DoctorResearchWorks; } set { _DoctorResearchWorks = value; } }
        public string DoctorServices { get { return _DoctorServices; } set { _DoctorServices = value; } }
        public string DoctorTraining { get { return _DoctorTraining; } set { _DoctorTraining = value; } }
        public string DoctorOrganization { get { return _DoctorOrganization; } set { _DoctorOrganization = value; } }
        public string DoctorLanguage { get { return _DoctorLanguage; } set { _DoctorLanguage = value; } }
        public int DoctorOrd { get { return _DoctorOrd; } set { _DoctorOrd = value; } }
        public bool Active { get { return _Active; } set { _Active = value; } }
        public DateTime DateSave { get { return _DateSave; } set { _DateSave = value; } }

        public string cateIdList { get { return _cateIdList; } set { _cateIdList = value; } }
        public HttpPostedFileBase ImageFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.DoctorName))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên bác sỹ"));
            }
            else if (this.DoctorName.Length > 200)
            {
                results.Add(new ValidationResult("Tên bác sỹ lớn hơn 300 ký tự"));
            }

            if (this.DoctorCate == 0)
            {
                results.Add(new ValidationResult("Mời bạn chọn nhóm bác sỹ"));
            }


            if (!string.IsNullOrEmpty(DoctorAddress))
            {
                if (this.DoctorAddress.Length > 300)
                {
                    results.Add(new ValidationResult("Địa chỉ bác sỹ lớn hơn 300 ký tự"));
                }
            }

            if (!string.IsNullOrEmpty(DoctorEmail))
            {
                if (this.DoctorEmail.Length > 300)
                {
                    results.Add(new ValidationResult("Email bác sỹ lớn hơn 300 ký tự"));
                }
            }

            if (!string.IsNullOrEmpty(DoctorMobile))
            {
                if (this.DoctorMobile.Length > 20)
                {
                    results.Add(new ValidationResult("Số điện thoại bác sỹ lớn hơn 20 ký tự"));
                }

                else if (!Common.Common.IsNumeric(DoctorMobile))
                {
                    results.Add(new ValidationResult("Số điện thoại không đúng định dạng"));
                }
            }

            if (!string.IsNullOrEmpty(DoctorWorkPlace))
            {
                if (this.DoctorWorkPlace.Length > 300)
                {
                    results.Add(new ValidationResult("Nơi làm bác sỹ lớn hơn 300 ký tự"));
                }
            }

            if (!string.IsNullOrEmpty(DoctorLever))
            {
                if (this.DoctorInfo.Length > 200)
                {
                    results.Add(new ValidationResult("Trình độ bác sỹ lớn hơn 200 ký tự"));
                }
            }

            if (!string.IsNullOrEmpty(DoctorPosition))
            {
                if (this.DoctorPosition.Length > 300)
                {
                    results.Add(new ValidationResult("Vị trí bác sỹ lớn hơn 300 ký tự"));
                }
            }


            return results;
        }

        #endregion
    }
}

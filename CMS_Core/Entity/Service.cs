using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    [Serializable]

    public class Service : IValidatableObject
    {
        #region Constructors  
        public Service() { }
        #endregion
        #region Private Fields  
        private int _ServiceID;
        private int _LocationID;
        private string _ProvinceID;
        private string _ServiceName;
        private string _ServiceDes;
        private float _Cost;
        private string _ServiceContent;
        private string _Img;
        private float _Discount;
        private string _DesImg;
        private string _KeysearchName;
        private int _status;
        private float _CostFe;
        private float _DiscountFe;
        private int _LowAgeLimit;
        private int _HightAgeLimit;
        private int _IdGoi;
        private string _MaGoi;
        private int _Sex;
        private float _AverageRating;
        private int _CountRate;
        private string _ConditionDate;
        private DateTime _DateFrom;
        private DateTime _DateTo;
        private int _PersonNumService;
        private int _IsServey;
        private int _IsInNormal;
        private int _IsReExamination;
        private bool _IsHome;
        private int _TimeBefore;
        private int _ExtraDay;
        private int _DefaultNumber;
        private int _InWeb;
        private int _ServiceType;
        #endregion
        #region Public Properties  
        public int ServiceID { get { return _ServiceID; } set { _ServiceID = value; } }
        public int LocationID { get { return _LocationID; } set { _LocationID = value; } }
        public string ProvinceID { get { return _ProvinceID; } set { _ProvinceID = value; } }
        public string ServiceName { get { return _ServiceName; } set { _ServiceName = value; } }
        public string ServiceDes { get { return _ServiceDes; } set { _ServiceDes = value; } }
        public float Cost { get { return _Cost; } set { _Cost = value; } }
        public string ServiceContent { get { return _ServiceContent; } set { _ServiceContent = value; } }
        public string Img { get { return _Img; } set { _Img = value; } }
        public float Discount { get { return _Discount; } set { _Discount = value; } }
        public string DesImg { get { return _DesImg; } set { _DesImg = value; } }
        public string KeysearchName { get { return _KeysearchName; } set { _KeysearchName = value; } }
        public int status { get { return _status; } set { _status = value; } }
        public float CostFe { get { return _CostFe; } set { _CostFe = value; } }
        public float DiscountFe { get { return _DiscountFe; } set { _DiscountFe = value; } }
        public int LowAgeLimit { get { return _LowAgeLimit; } set { _LowAgeLimit = value; } }
        public int HightAgeLimit { get { return _HightAgeLimit; } set { _HightAgeLimit = value; } }
        public int IdGoi { get { return _IdGoi; } set { _IdGoi = value; } }
        public string MaGoi { get { return _MaGoi; } set { _MaGoi = value; } }
        public int Sex { get { return _Sex; } set { _Sex = value; } }
        public float AverageRating { get { return _AverageRating; } set { _AverageRating = value; } }
        public int CountRate { get { return _CountRate; } set { _CountRate = value; } }
        public string ConditionDate { get { return _ConditionDate; } set { _ConditionDate = value; } }
        public DateTime DateFrom { get { return _DateFrom; } set { _DateFrom = value; } }
        public DateTime DateTo { get { return _DateTo; } set { _DateTo = value; } }
        public int PersonNumService { get { return _PersonNumService; } set { _PersonNumService = value; } }
        public int IsServey { get { return _IsServey; } set { _IsServey = value; } }
        public int IsInNormal { get { return _IsInNormal; } set { _IsInNormal = value; } }
        public int IsReExamination { get { return _IsReExamination; } set { _IsReExamination = value; } }
        public bool IsHome { get { return _IsHome; } set { _IsHome = value; } }
        public int TimeBefore { get { return _TimeBefore; } set { _TimeBefore = value; } }
        public int ExtraDay { get { return _ExtraDay; } set { _ExtraDay = value; } }
        public int DefaultNumber { get { return _DefaultNumber; } set { _DefaultNumber = value; } }
        public int InWeb { get { return _InWeb; } set { _InWeb = value; } }
        public int ServiceType { get { return _ServiceType; } set { _ServiceType = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.ServiceName))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên dịch vụ"));
            }else if(this.ServiceName.Length > 256)
            {
                results.Add(new ValidationResult("Tên dịch vụ lớn hơn 256 ký tự"));
            }

            if (string.IsNullOrEmpty(this.ProvinceID))
            {
                results.Add(new ValidationResult("Mời bạn nhập vào tên dịch vụ"));
            }
            else if (this.ProvinceID.Length > 50)
            {
                results.Add(new ValidationResult("Tên dịch vụ lớn hơn 50 ký tự"));
            }


            return results;
        }
        #endregion  }
    }
}

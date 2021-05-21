using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entity
{
    [Serializable]
    public class Dashboard_Index
    {
        
        #region Private Fields  
        private Int64 _Total;
      //  private DateTime _create_date;
      
        private string _create_date_View;
        
        #endregion
        #region Public Properties  
        public Int64 Total { get { return _Total; } set { _Total = value; } }
     //   public DateTime create_date { get { return _create_date; } set { _create_date = value; _create_date_View = _create_date.ToString("ddMMyyyy"); } }         
        public string create_date_View { get { return _create_date_View; } set { _create_date_View = value; } }               
        #endregion

    }
}

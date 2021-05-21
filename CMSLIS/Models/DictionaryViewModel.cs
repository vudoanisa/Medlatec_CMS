using System.ComponentModel.DataAnnotations;

namespace CMSLIS.Models
{


    public class SearchDictionaryViewModel
    {
        [Required]
        [Display(Name = "Từ ngày")]
        public string tungay { get; set; }

        [Required]
        [Display(Name = "Đến ngày")]
        public string denngay { get; set; }

        [Required]
        [Display(Name = "Người tạo")]
        public int userId { get; set; }

        [Required]
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }

        [Required]
        [Display(Name = "ID bài viết")]
        public int newsId { get; set; }

        [Required]
        [Display(Name = "keyword")]
        public string keyword { get; set; }

    }

}
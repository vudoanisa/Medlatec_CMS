using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Models
{
    public class SearchNewsViewModel
    {
        [Required]
        [Display(Name = "Từ ngày")]
        public string tungay { get; set; }

        [Required]
        [Display(Name = "Đến ngày")]
        public string denngay { get; set; }

        [Required]
        [Display(Name = "Nguồn tin")]
        public int SourceId { get; set; }

        [Required]
        [Display(Name = "Chuyên mục")]
        public int ParrenId { get; set; }

        [Required]
        [Display(Name = "Tiểu mục")]
        public int cateId { get; set; }

        [Required]
        [Display(Name = "Loại tin")]
        public int newsTypeMessage { get; set; }

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

        [Required]
        [Display(Name = "Loại tìm kiếm")]
        public int TypeKeyword { get; set; }
        
    }
}

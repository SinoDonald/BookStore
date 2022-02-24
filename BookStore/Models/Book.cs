using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        [Display(Name = "序號")]
        [Range(1, 999999, ErrorMessage = "ID範圍錯誤")]
        public int ID { get; set; }

        [Display(Name = "書名")]
        [Required(ErrorMessage = "請輸入書名")]
        public string Author { get; set; }

        [Display(Name = "金額")]
        [Required(ErrorMessage = "請輸入金額")]
        public double Price { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Công Việc")]
        public int JobId { get; set; }

        [Required(ErrorMessage = "Chức danh công việc là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Chức danh công việc không được vượt quá 100 ký tự.")]
        [Display(Name = "Chức Danh Công Việc")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Mức lương là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Mức lương phải là một số dương.")]
        [Display(Name = "Mức Lương")]
        [Column(TypeName = "BIGINT")]
        public int Salary { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}



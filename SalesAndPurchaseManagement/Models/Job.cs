using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }

        [Required(ErrorMessage = "Chức danh công việc là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Chức danh công việc không được vượt quá 100 ký tự.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Mức lương là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Mức lương phải là một số dương.")]
        public decimal Salary { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Sản Phẩm")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Sản Phẩm")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Chiều Dài")]
        public int Length { get; set; }

        [Required]
        [Display(Name = "Chiều Rộng")]
        public int Width { get; set; }

        [Required]
        [Display(Name = "Chiều Cao")]
        public int Height { get; set; }

        [Required]
        [Display(Name = "Màu Sắc")]
        public string Color { get; set; }

        [Required]
        [Display(Name = "Mã Danh Mục")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Mã Hình Dạng")]
        public int ShapeId { get; set; }

        [Required]
        [Display(Name = "Mã Chất Liệu")]
        public int MaterialId { get; set; }

        [Required]
        [Display(Name = "Mã Xuất Xứ")]
        public int CountryOfOriginId { get; set; }

        [Required]
        [Display(Name = "Mã Nhà Sản Xuất")]
        public int ManufacturerId { get; set; }

        [Required]
        [Display(Name = "Mã Tính Năng")]
        public int FeatureId { get; set; }

        [Required]
        [Display(Name = "Mã Đặc Điểm")]
        public int CharacteristicId { get; set; }

        [Required]
        [Display(Name = "Số Lượng")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Giá Nhập")]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Giá Bán")]
        public decimal SellingPrice { get; set; }

        [Required]
        [Display(Name = "Thời Gian Bảo Hành")]
        public int WarrantyPeriod { get; set; }

        [Display(Name = "Ảnh")]
        public string Image { get; set; }

        [StringLength(500)]
        [Display(Name = "Ghi Chú")]
        public string Notes { get; set; }

        [ForeignKey("CategoryId")]
        [Display(Name = "Danh Mục")]
        public virtual Category Category { get; set; }

        [ForeignKey("ShapeId")]
        [Display(Name = "Hình Dạng")]
        public virtual Shape Shape { get; set; }

        [ForeignKey("MaterialId")]
        [Display(Name = "Chất Liệu")]
        public virtual Material Material { get; set; }

        [ForeignKey("CountryOfOriginId")]
        [Display(Name = "Xuất Xứ")]
        public virtual CountryOfOrigin Country { get; set; }

        [ForeignKey("ManufacturerId")]
        [Display(Name = "Nhà Sản Xuất")]
        public virtual Manufacturer Manufacturer { get; set; }

        [ForeignKey("FeatureId")]
        [Display(Name = "Tính Năng")]
        public virtual Feature Feature { get; set; }

        [ForeignKey("CharacteristicId")]
        [Display(Name = "Đặc Điểm")]
        public virtual Characteristic Characteristic { get; set; }

        public string GetSize()
        {
            return $"{Length} x {Width} x {Height}";
        }
    }
}

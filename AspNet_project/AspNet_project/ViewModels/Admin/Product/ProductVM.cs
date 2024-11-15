using AspNet_project.Models;

public class ProductVM : BaseEntity
{
    public string Title { get; set; }
    public string Info { get; set; }
    public string MoreInfo { get; set; }
    public double Price { get; set; }
    public int SalesCount { get; set; }
    public double DiscountPrice { get; set; }

    public string CategoryName { get; set; }
    public int? CategoryId { get; set; }
    public string MainImage { get; set; }
    public List<string> ImagePaths { get; set; }
    public List<IFormFile> ProductPhotos { get; set; }

    public List<ProductImage> ProductImages { get; set; } 
}

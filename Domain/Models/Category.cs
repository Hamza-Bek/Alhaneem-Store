namespace Domain.Models;

public class Category : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class ApplicationUser :  IdentityUser<Guid>
{
    public string Name { get; set; }
    
    //Foreign Keys & Navigation Properties
    public Guid? LocationId { get; set; }
    public virtual Location Location { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual Cart Cart { get; set; }
}
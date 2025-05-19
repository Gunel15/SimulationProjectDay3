using Microsoft.AspNetCore.Identity;

namespace MVCProjectDay3.Models
{
    public class Role:IdentityRole<Guid>
    {
        public Role()
        {
            
        }
        public Role(string name):base(name) 
        {
            
        }
    }
}

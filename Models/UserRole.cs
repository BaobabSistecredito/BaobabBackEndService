
using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public int IdMarketingUser { get; set; }
        public int IdRol { get; set; }
        public MarketingUser MarketingUsers { get; set; }
        public Rol Rol { get; set; }
    }
}
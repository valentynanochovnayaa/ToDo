using System.Data.Entity.Migrations.Model;

namespace ToDo.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
namespace eStore_Admin.Infrastructure.Identity
{
    public class JwtSettings
    {
        public const string JwtSetting = "JwtSettings";
        
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}
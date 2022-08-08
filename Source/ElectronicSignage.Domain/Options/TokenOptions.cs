namespace ElectronicSignage.Domain.Options
{
    public class TokenOptions
    {
        public string AllowedIssuer { get; set; }
        public string AllowedAudience { get; set; }
        public string SymmetricKey { get; set; }
    }
}
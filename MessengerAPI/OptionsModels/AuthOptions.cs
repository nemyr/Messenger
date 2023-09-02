namespace MessengerAPI.OptionsModels
{
    public class AuthOptions
    {
        public string ISSUER { get; set; } = string.Empty;
        public string AUDIENCE { get; set; } = string.Empty;
        public string KEY { get; set; } = string.Empty;
        public uint ExpireTime { get; set; } = 30;
        public string ClaimId { get; set; } = string.Empty;
    }
}

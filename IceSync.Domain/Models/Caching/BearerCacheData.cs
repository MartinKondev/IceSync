namespace IceSync.Domain.Models.Caching
{
    public class BearerCacheData
    {
        public string Token { get; set; }

        public DateTimeOffset ExpirationDate { get; set; }
    }
}

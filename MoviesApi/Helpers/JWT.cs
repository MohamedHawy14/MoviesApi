namespace MoviesApi.Helpers
{
    public class JWT
    {
        public  string SecurityKey { get; set; }
        public  string issuerIP { get; set; }
        public  string audienceIP { get; set; }
        public  double DuarationInMintues { get; set; }
    }
}

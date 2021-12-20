namespace Project_Ads.Core
{
    public class Connection
    {
        private const string Host = "server.evgfilim1.me";
        private const string User = "pisya";
        private const string DBname = "pis";
        private const string Password = "pisya";
        private const string Port = "5432";

        public static string ConnString =>
            $"Server={Host};Username={User};Database={DBname};Port={Port};Password={Password};SSLMode=Prefer";
    }
}
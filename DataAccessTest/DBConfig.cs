namespace Infrastructure.Tests
{
    public class DBConfig
    {

        public string DBName = "TestDB";
        public string Password = Guid.NewGuid().ToString();
        public string DBConnectionString { 
            get
            {
                return $"Server=127.0.0.1;Database={DBName};User Id=sa;Password={Password};TrustServerCertificate=True";
            }
        } 
    }
}
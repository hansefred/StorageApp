using Microsoft.SqlServer.Dac;
using Serilog;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Infrastructure.Tests.Helper
{
    public static class DatabaseHelper
    {
        public static void DeployDatabase(DBConfig dBConfig)
        {
            try
            {
                var solutionDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;
                var dacpacFile = Path.Combine(solutionDir ?? "", "StorageDB\\bin\\Debug\\StorageDB.dacpac");

                var instance = new DacServices(dBConfig.DBConnectionString);


                using (var dacpac = DacPackage.Load(dacpacFile))
                {
                    instance.Deploy(dacpac, dBConfig.DBName);
                }


            }
            catch (Exception ex)
            {
                Log.Logger.Error("Cannot deploy SQL Database to {DBName}: {ex}", dBConfig.DBName, ex);
            }
        }
        public static void WaitforSQLDB(string ConnectingString, ILogger logger, int Port = 1433, int Retry = 100)
        {
            int Start = ConnectingString.IndexOf("Server=") + 7;
            int End = ConnectingString.IndexOf(";", ConnectingString.IndexOf("Server=")) - (ConnectingString.IndexOf("Server=") + 7);

            string Server = ConnectingString.Substring(Start, End);

            int i = 1;

            while (i < Retry)
            {
                logger.Information($"Try to connect to Server: {Server}... ({i} - {Retry})");
                Ping PingSender = new Ping();
                try
                {
                    var Reply = PingSender.Send(Server);

                    if (Reply.Status == IPStatus.Success)
                    {
                        logger.Information($"Ping Sucess, try to connect to Port: {Port}... ({i})");
                        using (TcpClient tcpClient = new TcpClient())
                        {
                            try
                            {
                                tcpClient.Connect(Reply.Address.ToString(), Port);
                                logger.Information("Port open");
                                Thread.Sleep(TimeSpan.FromSeconds(15));
                                return;
                            }
                            catch
                            {
                                logger.Information("Port closed Wait and Try again... ");
                                Thread.Sleep(4000);
                                i++;
                            }
                        }
                    }
                }
                catch
                {
                    logger.Information("Wait and Try again... ");
                    Thread.Sleep(500);
                    i++;
                }


            }
        }


    }
}

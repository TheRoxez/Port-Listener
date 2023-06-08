using System;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;

namespace PLMSQL
{
    public partial class Service1 : ServiceBase
    {   
        private TcpListener listener;
        protected override void OnStart(string[] args)
        {
            LogWrite("Servis Başlatıldı. " + DateTime.Now);
            listener = new TcpListener(IPAddress.Any, 5555);
            listener.Start();
            
            listener.BeginAcceptTcpClient(HandleTcpClientConnected, null);
            LogWrite("Bağlantı Alındı. " + DateTime.Now);
        }

        protected override void OnStop()
        {
            listener.Stop();
            LogWrite("Servis Sonlandırıldı. " + DateTime.Now);
        }

        private void HandleTcpClientConnected(IAsyncResult result)
        {
            TcpClient client = listener.EndAcceptTcpClient(result);
            listener.BeginAcceptTcpClient(HandleTcpClientConnected, null);

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            string receivedData = "";

            while (true)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                string connectionString = "server=localhost;user=root;database=test;password=6660;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO new_table (nm) VALUES (@nm)";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nm", receivedData);

                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
        }
        public Service1()
        {
            InitializeComponent();
        }
        public void LogWrite(string message)
        {
            string dyol = AppDomain.CurrentDomain.BaseDirectory + "/Logs";
            if (!Directory.Exists(dyol))
            {
                Directory.CreateDirectory(dyol);
            }
            string txtyol = AppDomain.CurrentDomain.BaseDirectory + "/Logs/servis.txt";
            if (!File.Exists(txtyol))
            {
                using (StreamWriter sv = File.CreateText(txtyol))
                {
                    sv.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sv = File.AppendText(txtyol))
                {
                    sv.WriteLine(message);
                }
            }
        }     
    }
}

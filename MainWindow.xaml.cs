using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System;
using System.Net.Http;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int port = 8080;
        static string ip = "127.0.0.1";
        IPEndPoint endPoint;
        Socket socket;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void bt_messege_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_client.Text.Length == 0)
                {
                    throw new Exception("Message is empty");
                }
                if (!socket.Connected)
                {
                    throw new Exception("Connection error!");
                }

                string message = tb_client.Text;
                byte[] data2 = Encoding.UTF8.GetBytes(message);
                await socket.SendAsync(data2, SocketFlags.None);

                byte[] data = new byte[512];
                int bytes = await socket.ReceiveAsync(data, SocketFlags.None);
                message = Encoding.UTF8.GetString(data, 0, bytes);
                tb_server.Text = message;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void bt_conect_Click(object sender, RoutedEventArgs e)
        {
            ConnectAction();
        }
        private async void ConnectAction()
        {
            int port = Convert.ToInt32(tb_port.Text);
            string ip = tb_ip.Text;
            try
            {
                endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                await socket.ConnectAsync(endPoint);
                MessageBox.Show("Connected to the server");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

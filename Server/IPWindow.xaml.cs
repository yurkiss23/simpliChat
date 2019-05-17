using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Server
{
    /// <summary>
    /// Логика взаимодействия для IPWindow.xaml
    /// </summary>
    public partial class IPWindow : Window
    {
        //public string IpAddr { get; set; }
        public string EPoint { get; set; }
        public string RemEPoint { get; set; }
        public string RecMessage { get; set; }
        public IPWindow()
        {
            InitializeComponent();

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ep = new IPEndPoint(ip, 1098);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            s.Bind(ep);
            s.Listen(10);
            try
            {
                while (true)
                {
                    Socket ns = s.Accept();
                    string data = null;
                    byte[] bytes = new byte[1024];
                    int bytesRec = ns.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    RecMessage = data;
                    RemEPoint = ns.RemoteEndPoint.ToString();
                    ns.Send(Encoding.UTF8.GetBytes($"Server {DateTime.Now}"));
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Socket error: " + ex.Message);
            }
            EPoint = ep.ToString();
        }

        private void BtnConServ_Click(object sender, RoutedEventArgs e)
        {
            //IpAddr = txtIP.Text;
        }
    }
}

using simpliChat.Entities;
using simpliChat.Models;
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

namespace simpliChat.Windows
{
    /// <summary>
    /// Логика взаимодействия для SelectWindow.xaml
    /// </summary>
    public partial class SelectWindow : Window
    {
        private EFContext _context;
        public List<ReceiverModel> recList;
        public string EPoint { get; set; }
        public static string RecMessage { get; set; }
        public SelectWindow()
        {
            InitializeComponent();
            _context = new EFContext();
            Task SrvStart = new Task(ServerStart);
            SrvStart.Start();
        }

        public void ServerStart()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ep = new IPEndPoint(ip, 1098);
            EPoint = ep.ToString();
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
                    ns.Send(Encoding.UTF8.GetBytes($"\nsend in {DateTime.Now}"));
                    RecMessage = data + $"\nreceived in {DateTime.Now}\nfrom: {ns.RemoteEndPoint.ToString()}";
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Socket error: " + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            recList = new List<ReceiverModel>(
                _context.Receivers.Select(r => new ReceiverModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IPAdress = r.IPAdress,
                    History = r.History
                }).ToList());

            foreach (var item in recList)
            {
                cbSelName.Items.Add(item.Name);
            }
        }

        private void SelectedChsnged_Item(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mainDlg = new MainWindow();
            mainDlg.RecID = _context.Receivers.Where(r => r.Name == cbSelName.SelectedItem.ToString()).First().Id;
            mainDlg.EPoint = this.EPoint;
            mainDlg.ShowDialog();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

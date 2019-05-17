using simpliChat.Entities;
using simpliChat.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace simpliChat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EFContext _context;
        public int RecID { get; set; }
        public string EPoint { get; set; }
        //public string RecName { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _context = new EFContext();
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            Title += EPoint;
            //tabChat.Focus();
            txtSendMes.Focus();
            lblRecName.Content += _context.Receivers.Where(r => r.Id == RecID).First().Name;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse(_context.Receivers.Where(r => r.Id == RecID).First().IPAdress);
            IPEndPoint ep = new IPEndPoint(ip, 1098);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                s.Connect(ep);
                if (s.Connected)
                {
                    //MessageBox.Show(txtSendMes.Text,txtSendMes.Name);
                    //MessageBox.Show(txtReceiveMes.Text, txtReceiveMes.Name);
                    s.Send(Encoding.UTF8.GetBytes(txtSendMes.Text));
                    byte[] buffer = new byte[1024];
                    int l;
                    do
                    {
                        l = s.Receive(buffer);
                        txtSendMes.Text += Encoding.UTF8.GetString(buffer, 0, l);
                        txtReceiveMes.Text = SelectWindow.RecMessage;
                        //MessageBox.Show(txtSendMes.Text, txtSendMes.Name);
                        //MessageBox.Show(txtReceiveMes.Text, txtReceiveMes.Name);
                    } while (l > 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            finally
            {
                //s.Shutdown(SocketShutdown.Both);
                s.Close();
            }
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("delete messages history");
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

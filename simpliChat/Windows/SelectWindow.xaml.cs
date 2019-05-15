using simpliChat.Entities;
using simpliChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public SelectWindow()
        {
            InitializeComponent();
            _context = new EFContext();
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
            //MessageBox.Show(cbSelName.SelectedItem.ToString());
            MainWindow mainDlg = new MainWindow();
            mainDlg.ShowDialog();
        }
    }
}

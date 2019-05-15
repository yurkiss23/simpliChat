using simpliChat.Entities;
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
        public List<Receiver> recList;
        public SelectWindow()
        {
            InitializeComponent();
            _context = new EFContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            recList = new List<Receiver>(
                _context.Receivers.Select(r => new Receiver()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IPAdress = r.IPAdress,
                    History = r.History
                }).ToList());
        }
    }
}

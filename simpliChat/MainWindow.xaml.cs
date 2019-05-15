﻿using simpliChat.Entities;
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
        public int Receiver { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _context = new EFContext();
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            tabChat.Focus();
            lblRecName.Content = lblRecName.Content + _context.Receivers.Where(r => r.Id == Receiver).First().Name;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System.IO;
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

namespace WpfOrganizations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Organization> szervezetek = new List<Organization>();

        private void Betoltes(string filename)
        {
            foreach (var sor in File.ReadAllLines(filename).Skip(1))
            {
                szervezetek.Add(new Organization(sor.Split(';')));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Betoltes("Sources\\organizations-100000.csv");

            dgAdatok.ItemsSource = szervezetek;
            cbOrszag.ItemsSource = szervezetek.Select(x => x.Country).OrderBy(x => x).Distinct().ToList();
            cbEv.ItemsSource = szervezetek.Select(x => x.Founded).OrderBy(x => x).Distinct().ToList();
            lblAlkalmazottak.Content = szervezetek.Sum(x => x.EmployeesNumber);
        }

        private void cbOrszag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var szurtLista = szervezetek.Where(x => x.Country == cbOrszag.SelectedItem.ToString()).ToList();
            dgAdatok.ItemsSource = szurtLista;
            lblAlkalmazottak.Content = szurtLista.Sum(x => x.EmployeesNumber);
        }

        private void cbEv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var szurtLista = szervezetek.Where(x => x.Founded == int.Parse(cbEv.SelectedItem.ToString())).ToList();
            dgAdatok.ItemsSource = szurtLista;
            lblAlkalmazottak.Content = szurtLista.Sum(x => x.EmployeesNumber).ToString();
        }

        private void dgAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAdatok.SelectedItem is Organization)
            {
                Organization valasztott = dgAdatok.SelectedItem as Organization;
                lblAzonosito.Content = valasztott.Id.ToString();
                lblWebcim.Content = valasztott.Website;
                lblLeiras.Content = valasztott.Description;
            }
            else
            {
                MessageBox.Show("Hiba");
            }
        }
    }
}

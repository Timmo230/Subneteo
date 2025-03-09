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

namespace SubneteoDeRedesConInterfaz
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<IP> resultado = new List<IP>();

            resultado.Add(new IP { NumeroSubred="Subred 1", IPRed = "1.1.1.1", IPBroadcast = "23", IPPrimera = "primera", IPUltima = "Ultima", HostDesperdiciado = "42" });
            Receptor.ItemsSource = resultado;
        }
    }

    public class IP
    {
        public string NumeroSubred { get; set; }
        public string IPRed { get; set; }
        public string IPBroadcast { get; set; }
        public string IPPrimera { get; set; }
        public string IPUltima { get; set; }
        public string HostDesperdiciado { get; set; }
    }

    public class IPInicial
    {
        public string NumeroSubred { get; set; }
        public string IPIniciall { get; set; }
        public string Mascara { get; set; }
    }
}

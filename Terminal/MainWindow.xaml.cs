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
using TerminalAPI;

namespace Terminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            TerminalWriteLine("Test\nroot§damien> ");
        }

        void TerminalWriteLine(string s)
        {
            richTextBox.AppendText(s + "\n");
            richTextBox.SelectAll();
            
        }

        void TerminalWrite(string s)
        {

        }
    }
}

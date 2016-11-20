using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using MetroFramework.Components;
using System.Threading;
using System.Diagnostics;

namespace Terminal
{
    public partial class Form1 : MetroForm
    {
        string currentCommandString;

        string currentCommand;
        string currentCommandArgs;

        public Form1()
        {
            InitializeComponent();
            StyleManager = new MetroStyleManager();
            StyleManager.Theme = MetroThemeStyle.Dark;
            StyleManager.Style = MetroColorStyle.Black;

            TerminalWriteLine("Terminal v 1.0.1r2\nCopyright 2016 TheCoderPro\n", false);
            TerminalWrite("ROOT§" + Environment.MachineName + "> ");

            console.SelectionChanged += OnSelectionChange;
            console.KeyPress += OnKeyPressed;
            console.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                string[] data = currentCommandString.Split(new char[] { ' ' }, 2);
                currentCommand = data[0];
                currentCommandArgs = data[1];
                MessageBox.Show("Command: \"" + currentCommand + "\" with arguments: \"" + currentCommandArgs + "\"");
                currentCommandString = "";

                //if (currentCommand == "echo")
                //{
                //    TerminalWriteLine("Echo: " + currentCommandArgs, true);
                //    TerminalWrite("ROOT§" + Environment.MachineName + "> ");
                //}

                Process currentCommandProcess = Process.Start(new ProcessStartInfo(currentCommand, currentCommandArgs) { RedirectStandardInput = true, RedirectStandardOutput = true, UseShellExecute = false });

                //TODO: Add support for launching commands here...
            }
        }

        private void OnKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (currentCommandString == null) currentCommandString = "";
            if (e.KeyChar == '\b' && currentCommandString.Length > 0)
            {
                currentCommandString = currentCommandString.Remove(currentCommandString.Length - 1, 1);
                return;
            }
            currentCommandString += e.KeyChar;
        }

        private void OnSelectionChange(object sender, EventArgs e)
        {
            console.SelectionStart = console.TextLength;
            console.SelectionLength = 0;
            console.SelectionProtected = false;
        }

        private void OnCloseForm(object sender, FormClosingEventArgs e)
        {
            
        }

        void TerminalWriteLine(string toWrite, bool newLineBefore)
        {
            console.Text += (newLineBefore ? "\n" : "") + toWrite + "\n";
            ProtectAllConsoleText();
        }

        void TerminalWrite(string toWrite)
        {
            console.Text += toWrite;
            ProtectAllConsoleText();
        }

        void ProtectAllConsoleText()
        {
            console.SelectAll();
            console.SelectionLength = Math.Max(console.SelectionLength - 1, 0);
            console.SelectionProtected = true;
            console.SelectionLength = 0;
            console.SelectionStart = console.TextLength;
            console.SelectionProtected = false;
        }
    }
}

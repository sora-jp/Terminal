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
using TerminalAPI;
using Message = TerminalAPI.Message;

namespace Terminal
{
    public partial class Form1 : MetroForm
    {
        string currentCommandString;

        string currentCommand;
        string currentCommandArgs;
        
        //Thread workerThread;
        Thread commandWorkerThread;
        Process currentCommandProcess;

        public bool pendingRead;
        public bool cancelKeyPress;
        public bool pendingReadLine;
        public bool commandRunning;

        string currentReadLineString;

        public Form1()
        {
            InitializeComponent();
            
            CheckForIllegalCrossThreadCalls = true;
            
            StyleManager = new MetroStyleManager();
            StyleManager.Theme = MetroThemeStyle.Dark;
            StyleManager.Style = MetroColorStyle.Black;

            console.Focus();

            TerminalWriteLine("Terminal v 1.0.1r2\nCopyright 2016 TheCoderPro\n", false);
            TerminalWrite("ROOT§" + Environment.MachineName + "> ");

            console.SelectionChanged += OnSelectionChange;
            console.KeyPress += OnKeyPressed;
            console.KeyDown += OnKeyDown;
            GotFocus += OnRecievedFocus;
            Activated += OnRecievedFocus;        

            commandWorkerThread = new Thread(() => 
            {
                Debug.WriteLine("WorkerThread was started.");
                while (!currentCommandProcess.HasExited)
                {
                    Debug.WriteLine("WorkerThread is successfully running.");
                    Message m = PipeManager.RecieveMessage(currentCommandProcess);
                    switch (m.messageType)
                    {
                        case MessageType.Print:
                            TerminalWrite(m.data);
                            break;
                        case MessageType.PrintLn:
                            TerminalWriteLine(m.data, false);
                            break;
                        case MessageType.Read:
                            pendingRead = true;
                            cancelKeyPress = m.data == "0" ? false : true;
                            break;
                        case MessageType.ReadLine:
                            pendingReadLine = true;
                            break;
                        case MessageType.SetColorBG:
                            break;
                        case MessageType.SetColorFG:
                            break;
                        case MessageType.Clear:
                            Clear();
                            break;
                        default:
                            break;
                    }
                }
                TerminalWrite("\nROOT§" + Environment.MachineName + "> ");
                commandRunning = false;
            });
        }

        private void OnRecievedFocus(object sender, EventArgs e)
        {
            console.Focus();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (pendingRead)
            {
                PipeManager.SendMessage(new Message(MessageType.ReadResponse, ((int)e.KeyCode).ToString()), currentCommandProcess);
                pendingRead = false;
                e.Handled = true;
                e.SuppressKeyPress = cancelKeyPress;
                cancelKeyPress = false;
                return;
            }

            if (e.KeyCode == Keys.Enter && !commandRunning)
            {
                //e.Handled = true;
                string[] data = currentCommandString.Split(new char[] { ' ' }, 2);
                currentCommand = data[0];
                currentCommandArgs = data.Length > 1 ? data[1] : "";
                //MessageBox.Show("Command: \"" + currentCommand + "\" with arguments: \"" + currentCommandArgs + "\"");
                currentCommandString = "";
                currentCommandArgs = "";

                //if (currentCommand == "echo")
                //{
                //    TerminalWriteLine("Echo: " + currentCommandArgs, true);
                //    TerminalWrite("ROOT§" + Environment.MachineName + "> ");
                //}

                currentCommandProcess = Process.Start(new ProcessStartInfo(currentCommand, currentCommandArgs) { RedirectStandardInput = true, RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true });
                commandRunning = true;
                commandWorkerThread.Start();
            }

            //MessageBox.Show("Keys: " + e.KeyData + " | ConsoleKey: " + ((ConsoleKey)Enum.Parse(typeof(ConsoleKey), e.KeyValue.ToString())).ToString());
        }

        private void OnKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (pendingReadLine && currentCommandProcess != null)
            {
                if (!currentCommandProcess.HasExited)
                {
                    if (pendingReadLine)
                    {
                        if (e.KeyChar == '\n')
                        {
                            PipeManager.SendMessage(new Message(MessageType.ReadLineResponse, currentReadLineString), currentCommandProcess);
                            pendingReadLine = false;
                            currentReadLineString = "";
                        }
                        else
                        {
                            currentReadLineString += e.KeyChar.ToString();
                        }
                    }
                    //e.Handled = true;
                    return;
                }
            }
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
            //workerThread.Abort();
            commandWorkerThread?.Abort();

            currentCommandProcess?.Kill();
        }

        void TerminalWriteLine(string toWrite, bool newLineBefore)
        {
            if (!console.InvokeRequired) { terminalWriteLine(toWrite, newLineBefore); return; }
            Invoke((Action)(() => { TerminalWriteLine(toWrite, newLineBefore); }));
        }

        void terminalWriteLine(string toWrite, bool newLineBefore)
        {
            console.Text += (newLineBefore ? "\n" : "") + toWrite + "\n";
            ProtectAllConsoleText();
        }

        void TerminalWrite(string toWrite)
        {
            if (!console.InvokeRequired) { terminalWrite(toWrite); return; }
            Invoke((Action)(() => { TerminalWrite(toWrite); }));
        }

        void terminalWrite(string toWrite)
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

        void Clear()
        {
            Invoke((Action)(() => { clear(); }));
        }

        void clear()
        {
            console.Text = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Bluetooth_UI
{
    class SettingsManager
    {
        private static SettingsManager instance;

        public static SettingsManager Instance
        {
            get { return instance; }
        }

        private SettingsManager()
        { }

        private MainWindow mainWindow;

        public static void Initialize(MainWindow window)
        {
            if (instance != null)
                return;

            instance = new SettingsManager();
            instance.mainWindow = window;

            instance.commandmanager = new CommandManager(instance);

            instance.InitializeUI();
        }

        public MainWindow SettingPanel
        {
            get { return mainWindow; }
        }

        /*///////////////////////////////////////////////////////////////*/

        private void InitializeUI()
        {
            List<string> ports = new List<string>();
            string[] portlist = SerialPort.GetPortNames();
            //ToDo: find a better place for them #1 than a static function
            foreach (string port in portlist)
            {
                ports.Add(port);
            }
            mainWindow.cbPorts.SelectedItem = "COM18";
            mainWindow.cbPorts.ItemsSource = ports;
            FillCommandCommandBox();
        }

        /*///////////////////////////////////////////////////////////////*/

        List<Command> commands = new List<Command>();

        FileManager filemanager = new FileManager();

        CommandManager commandmanager;

        //switch between writing to file and to the textbox
        bool writeToFile = false;
        
        //ToDo: find a better place for them #1

        
        public void Connect()
        {
            //ToDo: after connection through wifi is possible, user should be able to choose between connection type
            //Connection through wifi: BT and system settings are available
            //Connection through BT: wifi and system settings are available
            string portname = mainWindow.cbPorts.SelectedItem.ToString();

            string connectionresult = commandmanager.OpenBlueToothConnection(portname);

            if (connectionresult == "ConnectionUp")
            {
                mainWindow.lbConnectionState.Text = "Connection: Up!";
                mainWindow.lbConnectionState.Foreground = Brushes.Green;
            }
            else
            {
                MessageBox.Show(connectionresult);
            }  
        }

        public void dataReceived(string incomingdata)
        {
            if (writeToFile == false)
            {
                mainWindow.tbOutput.Text += incomingdata;
                mainWindow.tbOutput.ScrollToEnd();
            }
            else
            {
                filemanager.WriteLine(incomingdata);
            }
        }

        public void SaveToFile()
        {
            //ToDo #2: Legyen választható az útvonal - pl OpenFileDialog 
            writeToFile = true;
        }

        public void FillCommandCommandBox()
        {
            //ToDo: lekezelni a checkboxokat
            List<string> commandNameList = new List<string>();
            commandNameList = commandmanager.GetCommandList("WifiCommands.xml");
            mainWindow.cbCommands.ItemsSource = commandNameList;
            mainWindow.cbCommands.SelectedIndex = 0;
        }

        public void Close()
        {
            commandmanager.CloseBlueToothConnection();
        }
    }   
}

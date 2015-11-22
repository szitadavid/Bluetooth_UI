using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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
            FillPortList();
            mainWindow.tbFilepath.Text = filemanager.FilePath;
            mainWindow.rbDestinySystem.IsChecked = true;
            string path = "Res/Wifi2.png";
            BitmapImage bitmap = new BitmapImage(new Uri(path, UriKind.Relative));
            mainWindow.image.Source = bitmap;
        }

        public void FillPortList()
        {
            List<string> ports = new List<string>();
            string[] portlist = SerialPort.GetPortNames();
            foreach (string port in portlist)
            {
                ports.Add(port);
            }
            mainWindow.cbPorts.SelectedItem = "COM18";
            mainWindow.cbPorts.ItemsSource = ports;
        }

        /*///////////////////////////////////////////////////////////////*/

        List<Command> commands = new List<Command>();

        FileManager filemanager = new FileManager();

        CommandManager commandmanager;

        DispatcherTimer timer = new DispatcherTimer();

        /*///////////////////////////////////////////////////////////////*/


        public enum CommandDestiny
        {
            Bluetooth,
            WiFi,
            System
        }

        //switch between writing to file and to the textbox
        bool writeToFile = false;

        public void Connect()
        {
            if (mainWindow.tabBluetooth.IsSelected)
            {
                if (mainWindow.cbPorts.SelectedItem != null)
                {
                    string portname = mainWindow.cbPorts.SelectedItem.ToString();
                    mainWindow.btConnect.IsEnabled = false;
                    commandmanager.OpenBlueToothConnection(portname);
                }
            }
        }

        public void WriteToOutput(string message)
        {
            if (writeToFile == false)
            {
                RefreshOutputTextBox(message);
            }
            else
            {
                filemanager.WriteLine(message);
            }
        }

        private void RefreshOutputTextBox(string message)
        {
            mainWindow.Dispatcher.Invoke((Action)(() =>
            {
                mainWindow.tbOutput.Text += message + "\r\n";
                mainWindow.tbOutput.ScrollToEnd();
            }));
        }

        public void SaveToFile()
        {
            if (mainWindow.cbSaveToFile.IsChecked == true)
            {
                //ToDo: parancsot küldeni, hogy mostantól csv fileformatban küldjön a mikrokontroller

                timer.Interval = TimeSpan.FromMilliseconds(100);
                //timer.Tick += sendPeriodicDutyCycle;
                //timer.Start();
                writeToFile = true;
            }
            else
            {
                //ToDo: parancsot küldeni, hogy mostantól olvasható formában küldjön a mikrokontroller
                timer.Stop();
                timer.Tick -= sendPeriodicDutyCycle;
                writeToFile = false;
            }
        }

        void sendPeriodicDutyCycle(object sender, EventArgs e)
        {
            string param1 = mainWindow.slValue.Value.ToString();
            
            if (commandmanager.SendCommand("Set speed (0% - 100%)", param1, null, null, CommandDestiny.System) == false)
            {
                RefreshOutputTextBox("Sending failed.");
            }
        }

        public void FillCommandCommandBox()
        {
            List<string> commandNameList = new List<string>();
            string fileName;
            if (mainWindow.rbDestinyBluetooth.IsChecked == true)
                fileName = "BluetoothCommands.xml";
            else if (mainWindow.rbDestinyWifi.IsChecked == true)
                fileName = "WifiCommands.xml";
            else
                fileName = "SystemCommands.xml";
            commandNameList = commandmanager.GetCommandList(fileName);
            mainWindow.cbCommands.ItemsSource = commandNameList;
            mainWindow.cbCommands.SelectedIndex = 0;
            FillParameterList();
        }

        public void FillParameterList()
        {
            if (mainWindow.cbCommands.SelectedItem != null)
            {
                string commandName = mainWindow.cbCommands.SelectedItem.ToString();
                string param1, param2, param3;

                int paramnum = commandmanager.GetParameterList(commandName, out param1, out param2, out param3);

                mainWindow.tbParam1.IsEnabled = true;
                mainWindow.tbParam2.IsEnabled = true;
                mainWindow.tbParam3.IsEnabled = true;

                mainWindow.tbParam1.Text = param1;
                mainWindow.tbParam2.Text = param2;
                mainWindow.tbParam3.Text = param3;

                if (paramnum < 3)
                    mainWindow.tbParam3.IsEnabled = false;
                if (paramnum < 2)
                    mainWindow.tbParam2.IsEnabled = false;
                if (paramnum < 1)
                    mainWindow.tbParam1.IsEnabled = false;
            }
        }

        public void SendCommand()
        {
            string commandName = mainWindow.cbCommands.SelectedItem.ToString();
            //ToDo: választás cb box alapján vagy eltárolni változóba az aktuálisat
            CommandDestiny commandDestiny = CommandDestiny.System;

            string param1=null, param2=null, param3=null;
            if (mainWindow.tbParam1.IsEnabled)
                param1 = mainWindow.tbParam1.Text;
            if (mainWindow.tbParam2.IsEnabled)
                param2 = mainWindow.tbParam2.Text;
            if (mainWindow.tbParam3.IsEnabled)
                param3 = mainWindow.tbParam3.Text;

            if(commandmanager.SendCommand(commandName, param1, param2, param3, commandDestiny) == false)
            {
                RefreshOutputTextBox("Sending failed.");
            }
        }

        public void SelectFile()
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "CSV file (*.csv)|*.csv";
            if(savefile.ShowDialog() == true)
                mainWindow.tbFilepath.Text = savefile.FileName;
            filemanager.FilePath = savefile.FileName;
            filemanager.OpenFile();
        }

        public void TabItemChanged()
        {
            if (mainWindow.tabBluetooth.IsSelected == true)
            {
                mainWindow.rbDestinyBluetooth.IsEnabled = false;
                mainWindow.rbDestinyWifi.IsEnabled = true;
                if(mainWindow.rbDestinyBluetooth.IsChecked == true)
                {
                    mainWindow.rbDestinyWifi.IsChecked = true;
                }
            }
            else
            {
                mainWindow.rbDestinyWifi.IsEnabled= false;
                mainWindow.rbDestinyBluetooth.IsEnabled = true;
                if (mainWindow.rbDestinyWifi.IsChecked == true)
                {
                    mainWindow.rbDestinyBluetooth.IsChecked = true;
                }
            }
        }

        public void SetConnectionState(string result)
        {
            if(result == "ConnectionUp")
            {
                mainWindow.lbConnectionState.Foreground = Brushes.Green;
                mainWindow.lbConnectionState.Text = "Up!";
            }
            else
            {
                mainWindow.btConnect.IsEnabled = true;
                MessageBox.Show(result);
            }
        }

        private double Kc=3.1;
        private double Ti=1.0;
        private double Td=0;
        public void PIDSettings()
        {
            PIDSettings pidSettings = new PIDSettings();
            pidSettings.Kc = Kc;
            pidSettings.Ti = Ti;
            pidSettings.Td = Td;
            if (pidSettings.ShowDialog() == true)
            {               
                //ToDO: PID paraméterek beállítása

                //commandmanager.SendCommand("Set PID parameters", null, null, null, CommandDestiny.System);
                Kc = pidSettings.Kc;
                Ti = pidSettings.Ti;
                Td = pidSettings.Td;
                string param1 = Kc.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
                string param2 = Ti.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
                string param3 = Td.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
                commandmanager.SendCommand("Set PID parameters", param1, param2, param3, CommandDestiny.System);
            }
        }

        public void Close()
        {
            commandmanager.CloseBlueToothConnection();
        }
    }   
}

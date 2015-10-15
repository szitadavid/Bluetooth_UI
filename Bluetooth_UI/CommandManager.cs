using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VentilatorComm;

namespace Bluetooth_UI
{
    class CommandManager
    {
        SettingsManager settingsmanager;

        BluetoothConnection bluetoothConnection;

        List<Command> commands = new List<Command>();
         
        public CommandManager(SettingsManager sm)
        {
            settingsmanager = sm;
        }
    
        public string OpenBlueToothConnection(string portname)
        {
            BluetoothConnection bluetoothConnection;
            bluetoothConnection = new BluetoothConnection(portname);
            bluetoothConnection.DataReceived += settingsmanager.dataReceived;
            bluetoothConnection.Connect();

            return bluetoothConnection.ConnectionState;
        }

        public List<string> GetCommandList(string xmlFilePath)
        {
            commands.Clear();

            List<string> commandNameList = new List<string>();
            XmlDocument xmldoc;

            xmldoc = new XmlDocument();
            xmldoc.Load(xmlFilePath);

            XmlNodeList xnList = xmldoc.SelectNodes("/Commands/Command");
            
            foreach (XmlNode node in xnList)
            {
                Command command = new Command();
                command.CommandName = node.SelectSingleNode("Commandname").InnerText;
                command.CommandText = node.SelectSingleNode("Commandtext").InnerText;
                int paramnum;
                if (Int32.TryParse(node.SelectSingleNode("Param_num").InnerText, out paramnum))
                    command.ParameterNumber = paramnum;
                else
                    continue;
                string param1 = node.SelectSingleNode("Param1_default").InnerText;
                string param2 = node.SelectSingleNode("Param2_default").InnerText;
                string param3 = node.SelectSingleNode("Param3_default").InnerText;
                if(param1 != "")
                {
                    command.Parameter1 = param1;
                }
                commands.Add(command);
                if(param2 != "")
                {
                    command.Parameter2 = param2;
                }
                commands.Add(command);
                if(param3 != "")
                {
                    command.Parameter3 = param3;
                }
                commands.Add(command);
                commandNameList.Add(command.CommandName);
            }

            return commandNameList;
        }

        public void CloseBlueToothConnection()
        {
            if (bluetoothConnection != null)
                bluetoothConnection.Close();
        }
    }
}

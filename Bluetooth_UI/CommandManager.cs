using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Xml;
using Ventilator_Connection;

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
    
        public void OpenBlueToothConnection(string portname)
        {
            bluetoothConnection = new BluetoothConnection(portname);
            bluetoothConnection.DataReceived += dataReceived;
            bluetoothConnection.ConnectionUp += ConnectionResult;
            bluetoothConnection.Connect();
        }

        private void ConnectionResult(bool status)
        {
            settingsmanager.SetConnectionState(status);
        }

        private void dataReceived(string incomingdata)
        {
            while (incomingdata.Length != 0)
            {
                int index = incomingdata.IndexOf("\n");
                if (index > 0)
                {
                    settingsmanager.WriteToOutput(incomingdata.Substring(0, index));
                    incomingdata = incomingdata.Substring(index + 1);
                }
                else incomingdata = "";
            }
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
            {
                bluetoothConnection.Close();
            }
        }

        public int GetParameterList(string commandName, out string param1, out string param2, out string param3)
        {
            Command command = commands.Find(x => x.CommandName == commandName);

            param1 = command.Parameter1;
            param2 = command.Parameter2;
            param3 = command.Parameter3;

            return command.ParameterNumber;
        }

        public bool SendCommand(string commandName, string param1, string param2, string param3, SettingsManager.CommandDestiny commandDestiny)
        {
            if (bluetoothConnection != null &&
                bluetoothConnection.ConnectionState == "ConnectionUp")
            {
                Command command = commands.Find(x => x.CommandName == commandName);
                string commandText = command.CommandText;

                if (command.ParameterNumber > 0)
                    commandText += "=" + param1;
                if (command.ParameterNumber > 1)
                    commandText += "," + param2;
                if (command.ParameterNumber > 2)
                    commandText += "," + param3;
                commandText += "\n";

                return bluetoothConnection.Send(commandText);
            }

            else
                return false;
        }
    }
}

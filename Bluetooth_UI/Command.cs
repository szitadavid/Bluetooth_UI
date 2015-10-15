using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluetooth_UI
{
    class Command
    {
        string commandName;
        string commandText;
        int parameterNumber;
        string parameter1;
        string parameter2;
        string parameter3;

        public string CommandName 
        {
            get { return commandName;}
            set { commandName = value;}
        }
        public string CommandText
        {
            get { return commandText; }
            set { commandText = value; }
        }
        public int ParameterNumber
        {
            get { return parameterNumber; }
            set { parameterNumber = value; }
        }
        public string Parameter1
        {
            get { return parameter1; }
            set { parameter1 = value; }
        }
        public string Parameter2
        {
            get { return parameter2; }
            set { parameter2 = value; }
        }
        public string Parameter3
        {
            get { return parameter3; }
            set { parameter3 = value; }
        }
    }
}

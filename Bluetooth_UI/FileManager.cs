using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bluetooth_UI
{
    class FileManager
    {
        string filePath = "C:\\Users\\Szita\\Desktop\\test.csv";

        public string FilePath 
        { 
            get {return filePath;}
            //no validation :(
            set {filePath=value;}
        }

        public bool OpenFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                { }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void WriteLine(string data)
        {
            //ToDo: jobb a használat során végig foglalva tartani a fájlt? -szerintem jobb lenne
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine(data);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

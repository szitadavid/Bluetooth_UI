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

        //ToDo #2: Legyen választható az útvonal - pl OpenFileDialog 
        string filePath = "C:\\Users\\Szita\\Desktop\\test.csv";

        public void WriteLine(string data)
        {
            //ToDo: jobb a használat során végig foglalva tartani a fájlt? -szerintem jobb lenne
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
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

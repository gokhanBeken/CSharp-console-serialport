using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using System.Threading;

namespace seriport_console
{
    class Program
    {
        static private System.IO.Ports.SerialPort serialPort1; //manuel ekliyoruz
        static void Main(string[] args)
        {
            //CheckForIllegalCrossThreadCalls = false; //çapraz çalışmaya izin veriyoruz
            serialPort1 = new SerialPort(); //manuel örnekliyoruz

            seri_port_baglan();


            serialPort1.Write("merhaba\r\n");
            Console.Read();

        }

        static void seri_port_baglan()
        {
            if (serialPort1.IsOpen) // Bağlantıyı açıyoruz.eğer önceden bağlan butonuna basmış isek yani bağlantıyı açmışsak aşağıdaki hata mesajını verecektir.
            {
                //MessageBox.Show("Port Açık Bulunmaktadır..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3);
                return;
            }
            else
            {
                try
                {
                    serialPort1.BaudRate = int.Parse("9600"); // Hız olarak 9600 verdik.
                    serialPort1.DataBits = int.Parse("8"); // Veri bit ini de 8 bit olarak verdik
                    serialPort1.StopBits = System.IO.Ports.StopBits.One; // Durma bitini tek sefer olarak verdik.
                    serialPort1.Parity = Parity.None; // eşlik bit ini vermedik.
                    serialPort1.PortName = "COM1"; // Port adlarını comboboxtan alıyoruz. 
                    //serialPort1.DataReceived+=serialPort1_DataReceived; //kesme fonksiyonunu belirtiyoruz
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                    serialPort1.Open(); // Bağlantıyı açıyoruz
                    //MessageBox.Show("Bağlantı Başarılı");
                }
                catch (Exception) // Herhangi bir hata anında alttaki hata mesajını alacağız..
                {
                    Console.WriteLine("TRACKPEDAL cihazınız bağlı değil...");
                }
            }

        }
        private static void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Console.WriteLine("dad");
            //SerialPort dene = (SerialPort)sender;
            //var mesaj = dene.ReadLine();

            string dene;
            dene = serialPort1.ReadExisting();

            Console.Write(dene);
            if (dene == "\r") { Console.Write("\r\n"); }
        }
    }
}

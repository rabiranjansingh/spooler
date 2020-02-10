using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
using static Spooler.PrintSpoolerApi;
using System.Reflection;
//http://192.168.1.15:80/WebServices/Device
namespace Spooler
{
    class Program
    {

       static void Main(string[] args)
        {

            Thread thr = new Thread(mythread);
          //  thr.Start();


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            // using System.Management;
            string printerName = "Brother DCP-1610NW series Printer";
            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", printerName);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection coll = searcher.Get();

            foreach (ManagementObject printer in coll)
            {
                foreach (PropertyData property in printer.Properties)
                {
                    Console.WriteLine(string.Format("{0}: {1}", property.Name, property.Value));
                }
                //
            }


            Console.ReadLine();
            

        }//main

        [DllImport("winspool.drv", EntryPoint = "OpenPrinter", SetLastError = true)]
        internal static extern bool OpenPrinter(string pPrinterName, ref IntPtr phPrinter, PRINTER_DEFAULTS pDefault);

        [DllImport("winspool.drv", EntryPoint = "ClosePrinter", SetLastError = true)]
        internal static extern int ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "GetPrinter", SetLastError = true)]
        public static extern bool GetPrinter(IntPtr printerHandle,int level,IntPtr printerData,int bufferSize,ref int printerDataSize);


        // add printer function 

       

        PrinterInfo2 obj = new PrinterInfo2();
        PrinterInfo2 p;





        //[DllImport("winspool.drv", EntryPoint = "AddPrinter", SetLastError = true)]
        //public static extern bool AddPrinter(string pPrinterName, obj, p);
 




        [StructLayout(LayoutKind.Sequential)]
        public class PRINTER_DEFAULTS
        {
            public string pDatatype;
            public IntPtr pDevMode;
            public int DesiredAccess;
        }

        public struct OpenPrinterAccessCodes
        {
            public const int DELETE = 0x10000; // DELETE - Allowed to delete printers
            public const int READ_CONTROL = 0x20000; // READ_CONTROL - Allowed to read printer information
            public const int WRITE_DAC = 0x40000; // WRITE_DAC - Allowed to write device access control info
            public const int WRITE_OWNER = 0x80000; // WRITE_OWNER - Allowed to change the object owner
            public const int SERVER_ACCESS_ADMINISTER = 0x1;
            public const int SERVER_ACCESS_ENUMERATE = 0x2;
            public const int PRINTER_ACCESS_ADMINISTER = 0x4;
            public const int PRINTER_ACCESS_USE = 0x8;
            public const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
            public const int PRINTER_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | PRINTER_ACCESS_ADMINISTER | PRINTER_ACCESS_USE);
            public const int SERVER_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SERVER_ACCESS_ADMINISTER | SERVER_ACCESS_ENUMERATE);

            public const int MAX_PORTNAME_LEN = 64;
            public const int MAX_NETWORKNAME_LEN = 49;
            public const int MAX_SNMP_COMMUNITY_STR_LEN = 33;
            public const int MAX_QUEUENAME_LEN = 33;
            public const int MAX_IPADDR_STR_LEN = 16;

            public const int ERROR_INSUFFICIENT_BUFFER = 122;
            public const int ERROR_INVALID_FLAGS = 1004;
        }

        public IntPtr OpenPrinterHandle(string printerName)
        {
            var def = new PRINTER_DEFAULTS { pDatatype = null, pDevMode = IntPtr.Zero, DesiredAccess = OpenPrinterAccessCodes.PRINTER_ALL_ACCESS };
            var hPrinter = IntPtr.Zero;
            if (!OpenPrinter(printerName, ref hPrinter, def))
            {
                var lastWin32Error = new Win32Exception(Marshal.GetLastWin32Error());
              Console.WriteLine("Failed open Printer: " + lastWin32Error.Message);
                throw lastWin32Error;
            }
            return hPrinter;
        }

        public void ClosePrinterHandle(IntPtr hPrinter)
        {
            ClosePrinter(hPrinter);
        }

        // Static method 
        static void mythread()
        {
            var printerInfo2 = new PrinterInfo2();

            var pHandle = new IntPtr();
            //If the function succeeds, the return value is a nonzero value
            Console.WriteLine(OpenPrinter("Brother DCP-1610NW series Printer".Normalize(), ref pHandle, null));

            int actualDataSize = 0;
            GetPrinter(pHandle, 2, IntPtr.Zero, 0, ref actualDataSize);

            Console.WriteLine(pHandle);

            int err = Marshal.GetLastWin32Error();

            if (err == 122)
            {
                if (actualDataSize > 0)
                {
                    //Allocate memory to the size of the data requested
                    IntPtr printerData = Marshal.AllocHGlobal(actualDataSize);
                    //Retrieve the actual information this time
                    GetPrinter(pHandle, 2, printerData, actualDataSize, ref actualDataSize);

                    //Marshal to our structure
                    printerInfo2 = (PrinterInfo2)Marshal.PtrToStructure(printerData, typeof(PrinterInfo2));
                    //We've made the conversion, now free up that memory
                    Marshal.FreeHGlobal(printerData);
                }
            }
            else
            {
                Console.WriteLine(" printer is not connected  ");
            }


            Console.WriteLine("  MY LOCAL NETWORK PRINTER DATA : ");

            foreach (var field in typeof(PrinterInfo2).GetFields(BindingFlags.Instance |
                                                 BindingFlags.NonPublic |
                                                 BindingFlags.Public))
            {
                Console.WriteLine("{0} = {1}", field.Name, field.GetValue(printerInfo2));
            }

            




        }
    }

    

}

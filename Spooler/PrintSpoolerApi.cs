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

namespace Spooler
{
    class PrintSpoolerApi
    {


        [StructLayout(LayoutKind.Sequential)]
        public struct PrinterDefaults
        {
            public IntPtr pDatatype;
            public IntPtr pDevMode;
            public int DesiredAccess;
        }

        public enum PrinterProperty
        {
            ServerName,
            PrinterName,
            ShareName,
            PortName,
            DriverName,
            Comment,
            Location,
            PrintProcessor,
            Datatype,
            Parameters,
            Attributes,
            Priority,
            DefaultPriority,
            StartTime,
            UntilTime,
            Status,
            Jobs,
            AveragePpm
        };

        public struct PrinterInfo2
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            private string serverName;
            [MarshalAs(UnmanagedType.LPTStr)]
            private string printerName;
            [MarshalAs(UnmanagedType.LPTStr)]
            private string shareName;
            [MarshalAs(UnmanagedType.LPTStr)]
            private string portName;
            [MarshalAs(UnmanagedType.LPTStr)]
            private string driverName;
            [MarshalAs(UnmanagedType.LPTStr)]
            private string comment;
            [MarshalAs(UnmanagedType.LPTStr)]
            private string location;
            private IntPtr devMode;
            [MarshalAs(UnmanagedType.LPTStr)]
            private string sepFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            private string printProcessor;
            [MarshalAs(UnmanagedType.LPTStr)]
            private string datatype;
            [MarshalAs(UnmanagedType.LPTStr)]
            private string parameters;
            private IntPtr securityDescriptor;
            private uint attributes;
            private uint priority;
            private uint defaultPriority;
            private uint startTime;
            private uint untilTime;
            private uint status;
            private uint jobs;
            private uint averagePpm;

            public string ServerName
            {
                get
                {
                    return serverName;
                }

                set
                {
                    serverName = value;
                }
            }

            public string PrinterName
            {
                get
                {
                    return printerName;
                }

                set
                {
                    printerName = value;
                }
            }

            public string ShareName
            {
                get
                {
                    return shareName;
                }

                set
                {
                    shareName = value;
                }
            }

            public string PortName
            {
                get
                {
                    return portName;
                }

                set
                {
                    portName = value;
                }
            }

            public string DriverName
            {
                get
                {
                    return driverName;
                }

                set
                {
                    driverName = value;
                }
            }

            public string Comment
            {
                get
                {
                    return comment;
                }

                set
                {
                    comment = value;
                }
            }

            public string Location
            {
                get
                {
                    return location;
                }

                set
                {
                    location = value;
                }
            }

            public IntPtr DevMode
            {
                get
                {
                    return devMode;
                }

                set
                {
                    devMode = value;
                }
            }

            public string SepFile
            {
                get
                {
                    return sepFile;
                }

                set
                {
                    sepFile = value;
                }
            }

            public string PrintProcessor
            {
                get
                {
                    return printProcessor;
                }

                set
                {
                    printProcessor = value;
                }
            }

            public string Datatype
            {
                get
                {
                    return datatype;
                }

                set
                {
                    datatype = value;
                }
            }

            public string Parameters
            {
                get
                {
                    return parameters;
                }

                set
                {
                    parameters = value;
                }
            }

            public IntPtr SecurityDescriptor
            {
                get
                {
                    return securityDescriptor;
                }

                set
                {
                    securityDescriptor = value;
                }
            }

            public uint Attributes
            {
                get
                {
                    return attributes;
                }

                set
                {
                    attributes = value;
                }
            }

            public uint Priority
            {
                get
                {
                    return priority;
                }

                set
                {
                    priority = value;
                }
            }

            public uint DefaultPriority
            {
                get
                {
                    return defaultPriority;
                }

                set
                {
                    defaultPriority = value;
                }
            }

            public uint StartTime
            {
                get
                {
                    return startTime;
                }

                set
                {
                    startTime = value;
                }
            }

            public uint UntilTime
            {
                get
                {
                    return untilTime;
                }

                set
                {
                    untilTime = value;
                }
            }

            public uint Status
            {
                get
                {
                    return status;
                }

                set
                {
                    status = value;
                }
            }

            public uint Jobs
            {
                get
                {
                    return jobs;
                }

                set
                {
                    jobs = value;
                }
            }

            public uint AveragePpm
            {
                get
                {
                    return averagePpm;
                }

                set
                {
                    averagePpm = value;
                }
            }
        }

    }
}

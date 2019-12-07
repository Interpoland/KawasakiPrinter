using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmClient
{
    /// <summary>
    /// extruder class. Commands that need the extruder are generally kept here
    /// </summary>
    public class extruder
    {
        /// <summary>
        /// port - port used to communicate with the extruder
        /// origin - point labeled 0,0,0;
        /// currentExtrusion - how muhc is currently being extruded
        /// reference - absolute or incremental steps
        /// units - inches or millimeters
        /// feedrate - extrusion rate
        /// </summary>
        System.IO.Ports.SerialPort port;
        double origin; //deprecated
        double currentExtrusion; // Current Extrusion Rate Based on Motor
        unit units;
        double feedrate;
        public Queue<String> output;
        public reference coordinateMode;
        System.Windows.Forms.Timer timer;
        public double ePosition;

        /// <summary>
        /// creates and extruder object when the portname is known I.E. COM1, COM2
        /// </summary>
        /// <param name="portName">port to use for communication with the robot arm.</param>
        public extruder(string portName)
        {
            output = new Queue<string>();
            units = unit.millimeters;
            coordinateMode = reference.incremental;
            configureExtruderPort(portName);
            currentExtrusion = 0;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Running every 1000 ms
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            port.WriteLine("M114");
        }

        void configureExtruderPort(string portName)
        {
            port = new System.IO.Ports.SerialPort();
            port.BaudRate = 115200;
            port.PortName = portName;
            port.DataReceived += Port_DataReceived;//when data is sent from the extruder on the port, run this function
            port.Open();
        }
        /// <summary>
        /// todo - read data and handle properly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string MPtext = port.ReadLine();
            output.Enqueue(MPtext);
            // Example: "X:0.00 Y:0.00 Z:0.00 E:0.00 Count X: 0.00 Y:0.00 Z:0.00"
            if (MPtext.StartsWith("X:"))
            {
                MPtext = MPtext.Substring(MPtext.IndexOf('E')+2);
                MPtext = MPtext.Substring(0, MPtext.IndexOf(' '));
                this.ePosition = Convert.ToDouble(MPtext);
            }
        }
        public void G1(List<string> parameters)
        {
            double value = 0;
            for (int i = 1; i < parameters.Count(); i++)
            {
                if (parameters[i].ToUpper()[0] == 'E')
                {
                     value = double.Parse(parameters[i].Substring(1));
                     // this.port.WriteLine("E" + value.ToString()); // Just wanted to try extruder (11/14/2018)
                }
            }

            this.port.WriteLine("G1 E" + value.ToString());
        }
        public void M104(int temperature, int toolNumber)
        {
            this.port.WriteLine("M104 S" + temperature.ToString() + " T" + toolNumber.ToString());
        }
        public void M105()
        {
            throw new NotImplementedException();

        }
        public void M106(int fanIndex, int fanSpeed)
        {
            this.port.WriteLine("M106 P" + fanIndex.ToString() + " S" + fanSpeed.ToString());
        }
        public void M109(int targetTemp)
        {
            this.port.WriteLine("M109 S" + targetTemp.ToString());

        }
        public void M140()
        {
            throw new NotImplementedException();

        }
        public void M190()
        {
            throw new NotImplementedException();

        }
        public void M221(int feedPercent)
        {
            this.port.WriteLine("M221 S" + feedPercent.ToString());

        }
        public void M82()
        {
            coordinateMode = reference.absolute;
            port.WriteLine("M82");

        }
        public void M83()
        {
            coordinateMode = reference.incremental;
            port.WriteLine("M83");
        }
    }
}

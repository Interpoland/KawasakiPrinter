using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmClient
{
    /// <summary>
    /// Extruder class. Commands that need the extruder are generally kept here
    /// </summary>
    public class Extruder
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
        //readonly double origin; //deprecated
        double currentExtrusion; // Current Extrusion Rate Based on Motor
        Unit units;
        double feedrate;
        public Queue<String> output;
        public Reference coordinateMode;
        readonly System.Windows.Forms.Timer timer;
        public double ePosition;

        /// <summary>
        /// creates an extruder object when the portname is known I.E. COM1, COM2
        /// </summary>
        /// <param name="portName">port to use for communication with the robot arm.</param>
        public Extruder(string portName)
        {
            output = new Queue<string>();
            units = Unit.millimeters;
            coordinateMode = Reference.incremental;
            configureExtruderPort(portName);
            currentExtrusion = 0;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Running every 1000 ms
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
        }

        /// <summary>
        /// gets the current position of the extruder.
        /// M114 - get current position
        /// </summary>
        /// <param name="sender">the object that creates an event. required for function signature</param>
        /// <param name="e">an event created by the sender object</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            port.WriteLine("M114");
        }

        /// <summary>
        /// configures the extruder port as a serial port
        /// </summary>
        /// <param name="portName">port to use for communication with the 3D printer board</param>
        void configureExtruderPort(string portName)
        {
            port = new System.IO.Ports.SerialPort();
            port.BaudRate = 115200; //rate of data transfer (bits per second)
            port.PortName = portName;
            port.DataReceived += Port_DataReceived; //when data is sent from the extruder on the port, run this function
            port.Open();
        }

        /// <summary>
        /// todo - read data and handle properly **this function seems incomplete**
        /// </summary>
        /// <param name="sender">object that creates an event (e.g. button)</param>
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
        /// <summary>
        /// G1 - Linear Move (w/ extruder)
        /// performs a linear move while extruding filament
        /// </summary>
        /// <param name="parameters">a list</param>
        public void G1(List<string> parameters)
        {
            double value = 0;
            foreach (string param in parameters)
            {
                if (param[0] == 'E')
                {
                     value = double.Parse(param.Substring(1));
                     // this.port.WriteLine("E" + value.ToString()); // Just wanted to try extruder (11/14/2018)
                }
            }

            this.port.WriteLine("G1 E" + value.ToString());
        }

        /// <summary>
        /// M104 - sets hotend temperature without waiting
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="toolNumber"></param>        
        public void M104(int temperature, int toolNumber)
        {
            this.port.WriteLine("M104 S" + temperature.ToString() + " T" + toolNumber.ToString());
        }

        /// <summary>
        /// M105 - requests a report of the hotend temperature
        /// </summary>
        public void M105()
        {
            this.port.WriteLine("M105");
            //port.DataReceived += Port_DataReceived; //run this to recieve reported data
        }

        /// <summary>
        /// M106 - used to turn on a fan and set the fan speed
        /// </summary>
        /// <param name="fanIndex"> indicates which fan to select </param>
        /// <param name="fanSpeed"> sets fan speed using the range 0-255 (0%-100%)</param>
        public void M106(int fanIndex, int fanSpeed)
        {
            this.port.WriteLine("M106 P" + fanIndex.ToString() + " S" + fanSpeed.ToString());
        }
        /// <summary>
        /// M109 - wait for hotend temperature
        /// sets the hotend temperature and waits for it to be achieved.
        /// </summary>
        /// <param name="targetTemp"></param>
        public void M109(int targetTemp)
        {
            this.port.WriteLine("M109 S" + targetTemp.ToString());

        }

        /// <summary>
        /// M140 - set bed temperature
        /// sets the bed temperature without waiting
        /// </summary>
        public void M140(int temp)
        {
            this.port.WriteLine("M140 S" + temp.ToString());
        }

        /// <summary>
        /// M190 - wait for bed temperature
        /// wait for bed temperature
        /// </summary>
        public void M190(int temp)
        {
            this.port.WriteLine("M140 S" + temp.ToString());

        }

        /// <summary>
        /// M221 - set flow percentage
        /// sets the flow rate percentage for extruder E
        /// </summary>
        /// <param name="feedPercent"></param>
        public void M221(int feedPercent)
        {
            this.port.WriteLine("M221 S" + feedPercent.ToString());

        }

        /// <summary>
        /// M82 - E absolute
        /// overrides G91 to put E axis in absolute coordinates,
        /// regardless of other axes
        /// </summary>
        public void M82()
        {
            coordinateMode = Reference.absolute;
            port.WriteLine("M82");

        }

        /// <summary>
        /// M83 - E relative
        /// overrides G90 to put E axis in relative (incremental) coordinates,
        /// regardless of other axes
        /// </summary>
        public void M83()
        {
            coordinateMode = Reference.incremental;
            port.WriteLine("M83");
        }
    }
}

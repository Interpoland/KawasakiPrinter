using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RobotArmClient
{
    /// <summary>
    /// robot arm class. Commands that need the robot arm are generally kept here
    /// </summary>
    public class robotArm
    {
        /// <summary>
        /// port - the serial port used for communication
        /// origin - the point labeled 0,0,0 to the gcode
        /// currentPosition- the current position of the robot
        /// reference -  absolute or incremental mode for points
        /// units - inches or millimeters for points
        /// speed - current speed of the robot arm. 
        /// 
        /// todo - find relationship between speed and feedrate
        /// </summary>
        System.IO.Ports.SerialPort port;
        System.Windows.Forms.Timer timer; // excuting function every few seconds
        point origin;
        public point currentPosition;
        public point programmedPosition; // used for absolute coordinates
        public Reference coordinateMode;
        Unit units;
        double speed;
        public Queue<string> outputs;
        public string empty = "";
        public bool running = false;
        public double feedRate = -1; //units are mm/min

            /// <summary>
            /// creates a robotarm class when the portname is known.
            /// </summary>
            /// <param name="portName">name of the port to use I.E. COM1, COM2</param>
        public robotArm(string portName)
        {
            origin = new point(-94.986, 1103.26, -20.854, Unit.millimeters, Reference.absolute);
            programmedPosition = new point (0, 0, 0, Unit.millimeters, Reference.absolute);
            outputs = new Queue<string>();
            currentPosition = new point(0, 0, 0, Unit.millimeters, Reference.absolute);
            port = new System.IO.Ports.SerialPort();
            units = Unit.millimeters;
            coordinateMode = Reference.incremental;
            configureRobotArmPort(portName);
            getPosition();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Running every 1000 ms
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            getPosition();
        }

        /// <summary>
        /// configuration for the robot arm.
        /// </summary>
        /// <param name="portName"></param>
        void configureRobotArmPort(string portName)
        {
            port.BaudRate = 9600;
            port.PortName = portName;
            port.DataReceived += Port_DataReceived;  //when data is sent from the robot arm on the port, run this function
            port.Open();
        }
        /// <summary>
        /// todo: read data and handle properly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string temp = port.ReadLine();
            outputs.Enqueue(temp);
            if (temp.Contains("Illegal input data."))
            {
                return;
            }
            if (temp.Contains("X[mm]     Y[mm]     Z[mm]     O[deg]    A[deg]    T[deg]\r"))
            {
                char[] delimiters = { '\t', ' ' };
                var header = temp.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                string values = port.ReadLine();
                outputs.Enqueue(values);
                port.WriteLine("");
                var positionValues = values.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                this.currentPosition.x = Convert.ToDouble(positionValues[0]);
                this.currentPosition.y = Convert.ToDouble(positionValues[1]);
                this.currentPosition.z = Convert.ToDouble(positionValues[2]);
                this.programmedPosition.x = Convert.ToDouble(positionValues[0]) - origin.x;
                this.programmedPosition.y = Convert.ToDouble(positionValues[1]) - origin.y;
                this.programmedPosition.z = Convert.ToDouble(positionValues[2]) - origin.z;
                string useless = port.ReadLine();
                port.Write("\r\n");
            }
            else if (temp.Contains("Program completed.No = 1"))
            {
                running = false;
                return;
            }
            
        }

        /// <summary>
        /// update the robot's current position
        /// todo - read current position from robot arm
        /// </summary>
        public void getPosition()
        {
            //throw new NotImplementedException();
            port.WriteLine("HERE pose"); 
        }

        public void SetRobotOrigin()
        {
            this.origin = this.currentPosition;
        }

        /// <summary>
        /// rapid movement.
        /// paramters - X Y Z
        /// moves to X,Y,Z
        /// </summary>
        /// <param name="parameters"></param>
        public void G0(List<string> parameters)
        {
            double x = programmedPosition.x, y = programmedPosition.y, z = programmedPosition.z;

            foreach (string param in parameters)
            {
                if (param[0] == 'x' || param[0] == 'X')
                {
                    double value = Convert.ToDouble(param.Substring(1));
                    x = value;
                }
                else if (param[0] == 'y' || param[0] == 'Y')
                {
                    double value = Convert.ToDouble(param.Substring(1));
                    y = value;
                }
                else if (param[0] == 'z' || param[0] == 'Z')
                {
                    double value = Convert.ToDouble(param.Substring(1));
                    z = value;
                }
            }

            var point = new point(x, y, z, this.units, this.coordinateMode);

            if (point.Ref == Reference.absolute)
            {
                point absoluteDestination = origin + point;
                point increment = absoluteDestination - currentPosition;
                this.port.WriteLine("X = " + (increment.x));
                this.port.WriteLine("Y = " + (increment.y));
                this.port.WriteLine("Z = " + (increment.z));


            }
            else
            {
                this.currentPosition= currentPosition + point;
                this.port.WriteLine("X = " + (point.x));
                this.port.WriteLine("Y = " + (point.y));
                this.port.WriteLine("Z = " + (point.z));
            }

            this.port.WriteLine("Execute PrintMove");
        }
        public Status G1(List<string> parameters)
        {
            double x = programmedPosition.x, y = programmedPosition.y, z = programmedPosition.z;
            if (this.coordinateMode == Reference.incremental)
            {
                x = 0;
                y = 0;
                z = 0;
            }

            foreach (string param in parameters)
            {
                if (param[0] == 'x' || param[0] == 'X')
                {
                    double value = Convert.ToDouble(param.Substring(1));
                    x = value;
                }
                else if (param[0] == 'y' || param[0] == 'Y')
                {
                    double value = Convert.ToDouble(param.Substring(1));
                    y = value;
                }
                else if (param[0] == 'z' || param[0] == 'Z')
                {
                    double value = Convert.ToDouble(param.Substring(1));
                    z = value;
                }
                // add F 
            }

            
            var point = new point(x, y, z, this.units, this.coordinateMode);

            if (point.Ref == Reference.absolute)
            {
                point absoluteDestination = origin + point;
                point increment = absoluteDestination - currentPosition;
                this.port.WriteLine("X = " + (increment.x));
                this.port.WriteLine("Y = " + (increment.y));
                this.port.WriteLine("Z = " + (increment.z));
            }
            else
            {
                this.currentPosition = currentPosition + point;
                this.port.WriteLine("X = " + (point.x));
                this.port.WriteLine("Y = " + (point.y));
                this.port.WriteLine("Z = " + (point.z));
            }

            this.port.WriteLine("Execute PrintMove");
            this.port.WriteLine("HERE pose");
            return new Status(new point(x, y, z, units, coordinateMode), feedRate);

        }

        /// <summary>
        /// G4 - dwell
        /// </summary>
        public void G4(List<string> parameters)
        {
            int s = -1, p = -1;
            foreach (string param in parameters)
            {
                if (param[0] == 's' || param[0] == 'S')
                {
                    s = Convert.ToInt32(param.Substring(1));
                }
                else if (param[0] == 'p' || param[0] == 'P')
                {
                    p = Convert.ToInt32(param.Substring(1));
                }
            }

            if (s == -1)
            {
                if (p == -1)
                {
                    return;
                }
                else
                {
                    Thread.Sleep(p);
                }
            }
            else
            {
                if (p == -1)
                { 
                    return;
                }
                else
                {
                    Thread.Sleep(s * 1000);
                }
                
            }
        }
        public void G20()
        {
            this.units = Unit.inches;
        }
        public void G21()
        {
            this.units = Unit.millimeters;
        }
        public void G28()
        {
            this.port.WriteLine("Execute printhome");
            this.currentPosition.x = 321.482;
            this.currentPosition.y = 873.663;
            this.currentPosition.z = 103.753;
        }
        public void G90()
        {
            //throw new NotImplementedException();
            this.coordinateMode = Reference.absolute;
        }
        public void G91()
        {
            this.coordinateMode = Reference.incremental;
        }
        public void G92()
        {
            throw new NotImplementedException();
        }

    }
}

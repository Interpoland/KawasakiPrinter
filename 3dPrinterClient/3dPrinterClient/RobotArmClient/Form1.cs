using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace RobotArmClient
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// wait - tells the code not to proceed to the next line of gcode if there is one.
        /// enables pause functionality.
        /// 
        /// fileLoaded - signifies that the user picked a file.
        /// 
        /// fileLine - the current line of GCode in the file that the program should execute next
        /// 
        /// arm - the robot arm itself
        /// 
        /// extruder the extruder itself.
        /// </summary>
        /// 
        bool wait = true;
        bool fileLoaded = false;
        int fileLine = 0;
        List<string> names;
        robotArm arm;
        extruder extruder;
        public bool armStarted = false;
        public bool extruderStarted = false;

        //todo - add an extruder wait and robot arm wait.

        /// <summary>
        /// this code executes first and runs setup on the form.
        /// there is nothing to mess with here.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// takes in a line of gcode and formats it to a list.
        /// the rest will be parameters
        /// </summary>
        /// <param name="GCode"> The GCode string to parse</param>
        /// <returns>formatted GCode List, and index 0 will be the function "G0", "M109" while the rest
        /// will be parameters if they were passed in "X0" "Y32.331"</returns>
        private List<string> parseGCode(string GCode)
        {
            var parameters = new List<string>();//this will be the returned result. initialize to empty list
            string param = "";// this will store each word. initialize to empty string.
            foreach(char c in GCode)
            {
                //when a letter is encountered, add param to parameters and then empty param again
                // a letter is not encounter (a number) add it to param. this way, we can group parameters
                //with their values.
                if((c >=65 && c <= 90) || (c >= 97 && c <= 122)) //uppercase or lower case character in ascii
                {
                    if(param != "")//add the parameter to the list if it isnt empty.
                    {
                        param = param.Replace(" ", "");//remove white spaces
                        parameters.Add(param);
                        //reset param and add the new character
                        param = "";
                        param += c;
                        continue;
                    }
                }
                param += c;
            }
            param = param.Replace(" ", "");
            parameters.Add(param);
            parameters[0] = removeLeading(parameters[0]); //
            return parameters;
        }

        /// <summary>
        /// removes leading 0's. this makes G00 look like G0, G01 look like G1 etc.
        /// </summary>
        /// <param name="function">the gcode function. this will be parameter[0]. G0, G28, G92, M104, etc.</param>
        /// <returns>returns the function without the leading 0's</returns>
        private string removeLeading(string function)
        {
            string temp = function;
            int count = 0;
            for(int i = 1; i < temp.Length; i++)
            {
                if(function[i] != '0')
                {
                    break;
                }
                count++;
            }
            function = function.Remove(1, count);
            //if all the is left is "M" or "G", it was G0 or M0
            if (function.Length == 1 && function.ToUpper() == "G")
                return "G0";
            if (function.Length == 1 && function.ToUpper() == "M")//not sure if this is valid GCode.
                return "M0";
            return function; // if this statement is reached, it was not G0 or M0. return the fixed function string.
        }
        
        /// <summary>
        /// actuates a GCode if it has been implemented, else, it throws an exception and the program exits.
        /// </summary>
        /// <param name="gcode"></param>
        private void runGcode(string gcode)
        {
            var parameters = parseGCode(gcode);
            if (parameters[0] == "G0")
            {
                arm.running = true;
                arm.G0(parameters);
            }
            else if (parameters[0] == "G1")
            {
                if (arm != null)
                {
                    arm.running = true;
                    var status = arm.G1(parameters);
                    feedrateArm.Text = " Arm Feedrate = " + status.getFeedRate();
                    setX.Text = " programmed X = " + status.getSetPosition().x;
                    setY.Text = " programmed Y = " + status.getSetPosition().y;
                    setZ.Text = " programmed Z = " + status.getSetPosition().z;
                }
                if (extruder != null)
                {
                    extruder.G1(parameters);

                }

            }
            else if (parameters[0] == "G20")
            {
                arm.G20();
            }
            else if (parameters[0] == "G21")
            {
                arm.G21();
            }
            else if (parameters[0] == "G28")
            {
                arm.G28();
            }
            else if (parameters[0] == "G90")
            {
                arm.G90();
            }
            else if (parameters[0] == "G91")
            {
                arm.G91();
            }
            else if (parameters[0] == "M104")
            {
                int temp = 0;
                int tool = 0;
                foreach(string param in parameters)
                {
                    if (param.ToUpper()[0] == 'S')
                    {
                        temp = int.Parse(param.Substring(1));
                    }
                    else if (param.ToUpper()[0] == 'T')
                    {
                        tool = int.Parse(param.Substring(1));
                    }
                }
                extruder.M104(temp,tool);
            }
            else if (parameters[0] == "M106")
            {
                int fanIndex = 0;
                int fanSpeed = 0;
                foreach (string param in parameters)
                {
                    if (param.ToUpper()[0] == 'P')
                    {
                        fanIndex = int.Parse(param.Substring(1));
                    }
                    else if (param.ToUpper()[0] == 'S')
                    {
                        fanSpeed = int.Parse(param.Substring(1));
                    }
                }
                extruder.M106(fanIndex, fanSpeed);
            }
            else if (parameters[0] == "M109")
            {
                int targetTemp = 0;
                foreach (string param in parameters)
                {
                    if (param.ToUpper()[0] == 'S')
                    {
                        targetTemp = int.Parse(param.Substring(1));
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                extruder.M109(targetTemp);
            }
            else if (parameters[0] == "M82")
            {
                extruder.M82();
            }
            else if (parameters[0] == "M83")
            {
                extruder.M83();
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        // ---------------- EVENTS ----------------

        /// <summary>
        /// when the form loads, get a list of all connected COM ports and add each on to the combo boxes
        /// labeled "Robot Arm" and "Extruder" on the display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            names = System.IO.Ports.SerialPort.GetPortNames().ToList();
            foreach (string name in names)
            {
                robotArmPort.Items.Add(name);
                extruderPort.Items.Add(name);
            }

            // Test cases 10-19-2018

            /*
            var o = new point(80, -50, 20, unit.millimeters, reference.absolute);
            var c = new point(100, -20, 2, unit.millimeters, reference.absolute);
            var x = new point(5, 5, 5, unit.millimeters, reference.incremental);
            var y = new point(5, 5, 5, unit.millimeters, reference.incremental);
            var z = x + y;
            x.toAbsolute(c);
            x.toIncremental(c);
            x.toInches();
            x.toMillimeters();
            arm = new robotArm("Hi");
            */

        }

        /// <summary>
        /// iterate through the entire file adding each non-comment line to the a listbox display.
        /// Comments start with ; and can start midway through the line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var reader = new System.IO.StreamReader(openFileDialog1.FileName); //reads files
            while (!reader.EndOfStream)// loop until the whole file is read.
            {
                string temp = reader.ReadLine();// read line
                if (temp.Contains(";")) // if there is a comment, remove the semicolon and everything after it.
                    temp = temp.Substring(0, temp.IndexOf(";"));
                if (temp != "")// if the string isnt empty, add it to listbox. If the whole line was a comment, this will be true.
                    listBox1.Items.Add(temp);
            }
            fileLoaded = true;//global variable. signifies that the file has finished loading.
        }

        /// <summary>
        /// lets the user pick a file to load Gcode from
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pickAFile(object sender, EventArgs e)
        {
            fileLoaded = false;
            listBox1.Items.Clear();//clear the Gcode
            openFileDialog1.ShowDialog(); // let the user pick a file
        }



        /// <summary>
        /// stops running through the gcode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pause_Click(object sender, EventArgs e)
        {
            wait = true;
            string x = arm.empty;
        }

        /// <summary>
        /// starts running theough the gcode line by line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_Click(object sender, EventArgs e)
        {
            if (!fileLoaded)
            {
                return;
            }
            wait = false;

        }

        /// <summary>
        /// ticks on when the "Execute Timer" finished its interval.
        /// this will need to be comved later and is kept for testing purposes.
        /// 
        ///             DO NOT RUN A FILE WHILE THIS IS ENABLED
        ///             
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteFile_Tick(object sender, EventArgs e)
        {
            if(arm != null)
                while (arm.outputs.Count > 0)
                {
                    listBox2.Items.Add(arm.outputs.Dequeue());
                    listBox2.SelectedIndex = listBox2.Items.Count - 1;
                }
            if (extruder != null)
                while (extruder.output.Count > 0)
                {
                    listBox2.Items.Add(extruder.output.Dequeue());
                    listBox2.SelectedIndex = listBox2.Items.Count - 1;
                }
            if (arm == null)
                return;
            actualX.Text = "actual X = " + arm.currentPosition.x;
            actualY.Text = "actual Y = " + arm.currentPosition.y;
            actualZ.Text = "actual Z = " + arm.currentPosition.z;

            if (!arm.running)
            {
                if (fileLine == listBox1.Items.Count)
                {
                    wait = true;
                    return;
                }
                listBox1.SelectedIndex = fileLine;
                currentCommand.Text = "Current Command: " + listBox1.SelectedItem.ToString();
                runGcode(listBox1.SelectedItem.ToString());
                fileLine++;
            }
        }

        /// <summary>
        /// activates when someone selects the robot arm port from the drop down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void robotArmPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            arm = new robotArm(robotArmPort.SelectedItem.ToString());
            armStarted = true;
            if (arm.coordinateMode == reference.absolute)
                mode.Text = "absolute";
            else
                mode.Text = "incremental";
        }

        // same as robotArmPort... but for extruder

        private void extruderPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            extruder = new extruder(extruderPort.SelectedItem.ToString());
            extruderStarted = true;
        }

        /// <summary>
        /// this function executes when the timer named "updatePorts" on the main pages "ticks".
        /// if you click on the timer, you'll see options to the bottom right for time interval
        /// and an option to enable or disable it. A "tick" is one full time interval.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updatePorts_Tick(object sender, EventArgs e)
        {
            var newNames = System.IO.Ports.SerialPort.GetPortNames().ToList<string>();
            //check if there have been any new port connections
            foreach (string name in newNames)
            {
                if (!names.Contains(name))
                {
                    names.Add(name);
                    robotArmPort.Items.Add(name);
                    extruderPort.Items.Add(name);
                }
            }
            //check if any ports have been disconnected
            for (int i = 0; i < names.Count; i++)
            {
                if (!newNames.Contains(names[i]))
                {
                    names.Remove(names[i]);
                    robotArmPort.Items.Remove(names[i]);
                    extruderPort.Items.Remove(names[i]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            runGcode(textBox1.Text);
            textBox1.Text = "";
            if (arm != null)
            {
                if (arm.coordinateMode == reference.absolute)
                    mode.Text = "absolute";
                else
                    mode.Text = "incremental";
            }
        }

        private void actualX_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

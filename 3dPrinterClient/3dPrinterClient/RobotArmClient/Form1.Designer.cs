namespace RobotArmClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RobotArm = new System.IO.Ports.SerialPort(this.components);
            this.Extruder = new System.IO.Ports.SerialPort(this.components);
            this.robotArmPort = new System.Windows.Forms.ComboBox();
            this.extruderPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.updatePorts = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pickFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pause = new System.Windows.Forms.Button();
            this.play = new System.Windows.Forms.Button();
            this.setX = new System.Windows.Forms.Label();
            this.setY = new System.Windows.Forms.Label();
            this.setZ = new System.Windows.Forms.Label();
            this.feedrateArm = new System.Windows.Forms.Label();
            this.TempBed = new System.Windows.Forms.Label();
            this.TempExt = new System.Windows.Forms.Label();
            this.currentCommand = new System.Windows.Forms.Label();
            this.actualZ = new System.Windows.Forms.Label();
            this.actualY = new System.Windows.Forms.Label();
            this.actualX = new System.Windows.Forms.Label();
            this.feedrateExt = new System.Windows.Forms.Label();
            this.ExecuteFile = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.mode = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.actualE = new System.Windows.Forms.Label();
            this.setE = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Extruder
            // 
            this.Extruder.BaudRate = 115200;
            // 
            // robotArmPort
            // 
            this.robotArmPort.FormattingEnabled = true;
            this.robotArmPort.Location = new System.Drawing.Point(750, 15);
            this.robotArmPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.robotArmPort.Name = "robotArmPort";
            this.robotArmPort.Size = new System.Drawing.Size(136, 28);
            this.robotArmPort.TabIndex = 0;
            this.robotArmPort.SelectedIndexChanged += new System.EventHandler(this.robotArmPort_SelectedIndexChanged);
            // 
            // extruderPort
            // 
            this.extruderPort.FormattingEnabled = true;
            this.extruderPort.Location = new System.Drawing.Point(750, 52);
            this.extruderPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.extruderPort.Name = "extruderPort";
            this.extruderPort.Size = new System.Drawing.Size(136, 28);
            this.extruderPort.TabIndex = 1;
            this.extruderPort.SelectedIndexChanged += new System.EventHandler(this.extruderPort_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(659, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Robot Arm";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(659, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Extruder";
            // 
            // updatePorts
            // 
            this.updatePorts.Enabled = true;
            this.updatePorts.Interval = 3000;
            this.updatePorts.Tick += new System.EventHandler(this.updatePorts_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(14, 356);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(398, 164);
            this.listBox1.TabIndex = 4;
            // 
            // pickFile
            // 
            this.pickFile.Location = new System.Drawing.Point(155, 324);
            this.pickFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pickFile.Name = "pickFile";
            this.pickFile.Size = new System.Drawing.Size(111, 29);
            this.pickFile.TabIndex = 5;
            this.pickFile.Text = "Select a File";
            this.pickFile.UseVisualStyleBackColor = true;
            this.pickFile.Click += new System.EventHandler(this.pickAFile);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Run From File";
            // 
            // pause
            // 
            this.pause.Location = new System.Drawing.Point(14, 528);
            this.pause.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(111, 29);
            this.pause.TabIndex = 7;
            this.pause.Text = "Pause";
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // play
            // 
            this.play.Location = new System.Drawing.Point(301, 528);
            this.play.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(111, 29);
            this.play.TabIndex = 8;
            this.play.Text = "Play";
            this.play.UseVisualStyleBackColor = true;
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // setX
            // 
            this.setX.AutoSize = true;
            this.setX.Location = new System.Drawing.Point(597, 232);
            this.setX.Name = "setX";
            this.setX.Size = new System.Drawing.Size(131, 20);
            this.setX.TabIndex = 9;
            this.setX.Text = "programmed X = ";
            this.setX.Click += new System.EventHandler(this.setX_Click);
            // 
            // setY
            // 
            this.setY.AutoSize = true;
            this.setY.Location = new System.Drawing.Point(596, 252);
            this.setY.Name = "setY";
            this.setY.Size = new System.Drawing.Size(131, 20);
            this.setY.TabIndex = 10;
            this.setY.Text = "programmed Y = ";
            this.setY.Click += new System.EventHandler(this.setY_Click);
            // 
            // setZ
            // 
            this.setZ.AutoSize = true;
            this.setZ.Location = new System.Drawing.Point(597, 272);
            this.setZ.Name = "setZ";
            this.setZ.Size = new System.Drawing.Size(130, 20);
            this.setZ.TabIndex = 11;
            this.setZ.Text = "programmed Z = ";
            this.setZ.Click += new System.EventHandler(this.setZ_Click);
            // 
            // feedrateArm
            // 
            this.feedrateArm.AutoSize = true;
            this.feedrateArm.Location = new System.Drawing.Point(604, 353);
            this.feedrateArm.Name = "feedrateArm";
            this.feedrateArm.Size = new System.Drawing.Size(124, 20);
            this.feedrateArm.TabIndex = 12;
            this.feedrateArm.Text = "Arm Feedrate = ";
            // 
            // TempBed
            // 
            this.TempBed.AutoSize = true;
            this.TempBed.Location = new System.Drawing.Point(578, 376);
            this.TempBed.Name = "TempBed";
            this.TempBed.Size = new System.Drawing.Size(150, 20);
            this.TempBed.TabIndex = 13;
            this.TempBed.Text = "Bed Temperature = ";
            // 
            // TempExt
            // 
            this.TempExt.AutoSize = true;
            this.TempExt.Location = new System.Drawing.Point(578, 396);
            this.TempExt.Name = "TempExt";
            this.TempExt.Size = new System.Drawing.Size(148, 20);
            this.TempExt.TabIndex = 14;
            this.TempExt.Text = "Ext. Temperature = ";
            // 
            // currentCommand
            // 
            this.currentCommand.AutoSize = true;
            this.currentCommand.Location = new System.Drawing.Point(571, 418);
            this.currentCommand.Name = "currentCommand";
            this.currentCommand.Size = new System.Drawing.Size(156, 20);
            this.currentCommand.TabIndex = 15;
            this.currentCommand.Text = "Current Command = ";
            this.currentCommand.Click += new System.EventHandler(this.currentCommand_Click);
            // 
            // actualZ
            // 
            this.actualZ.AutoSize = true;
            this.actualZ.Location = new System.Drawing.Point(646, 178);
            this.actualZ.Name = "actualZ";
            this.actualZ.Size = new System.Drawing.Size(83, 20);
            this.actualZ.TabIndex = 18;
            this.actualZ.Text = "actual Z = ";
            this.actualZ.Click += new System.EventHandler(this.actualZ_Click);
            // 
            // actualY
            // 
            this.actualY.AutoSize = true;
            this.actualY.Location = new System.Drawing.Point(645, 156);
            this.actualY.Name = "actualY";
            this.actualY.Size = new System.Drawing.Size(84, 20);
            this.actualY.TabIndex = 17;
            this.actualY.Text = "actual Y = ";
            // 
            // actualX
            // 
            this.actualX.AutoSize = true;
            this.actualX.Location = new System.Drawing.Point(646, 136);
            this.actualX.Name = "actualX";
            this.actualX.Size = new System.Drawing.Size(84, 20);
            this.actualX.TabIndex = 16;
            this.actualX.Text = "actual X = ";
            this.actualX.Click += new System.EventHandler(this.actualX_Click);
            // 
            // feedrateExt
            // 
            this.feedrateExt.AutoSize = true;
            this.feedrateExt.Location = new System.Drawing.Point(605, 333);
            this.feedrateExt.Name = "feedrateExt";
            this.feedrateExt.Size = new System.Drawing.Size(122, 20);
            this.feedrateExt.TabIndex = 19;
            this.feedrateExt.Text = "Ext. Feedrate = ";
            // 
            // ExecuteFile
            // 
            this.ExecuteFile.Enabled = true;
            this.ExecuteFile.Tick += new System.EventHandler(this.ExecuteFile_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(398, 26);
            this.textBox1.TabIndex = 20;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(113, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 89);
            this.button1.TabIndex = 21;
            this.button1.Text = "Send G-Code";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mode
            // 
            this.mode.AutoSize = true;
            this.mode.Location = new System.Drawing.Point(474, 97);
            this.mode.Name = "mode";
            this.mode.Size = new System.Drawing.Size(0, 20);
            this.mode.TabIndex = 22;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(892, 15);
            this.listBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(422, 564);
            this.listBox2.TabIndex = 23;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(515, 151);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 29);
            this.button2.TabIndex = 24;
            this.button2.Text = "Set Origin";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // actualE
            // 
            this.actualE.AutoSize = true;
            this.actualE.Location = new System.Drawing.Point(646, 200);
            this.actualE.Name = "actualE";
            this.actualE.Size = new System.Drawing.Size(84, 20);
            this.actualE.TabIndex = 25;
            this.actualE.Text = "actual E = ";
            this.actualE.Click += new System.EventHandler(this.actualE_Click);
            // 
            // setE
            // 
            this.setE.AutoSize = true;
            this.setE.Location = new System.Drawing.Point(596, 292);
            this.setE.Name = "setE";
            this.setE.Size = new System.Drawing.Size(131, 20);
            this.setE.TabIndex = 26;
            this.setE.Text = "programmed E = ";
            this.setE.Click += new System.EventHandler(this.setE_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(926, 587);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 29);
            this.button3.TabIndex = 27;
            this.button3.Text = "auto-scroll";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1119, 587);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(165, 29);
            this.button4.TabIndex = 28;
            this.button4.Text = "disable auto-scroll";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 694);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.setE);
            this.Controls.Add(this.actualE);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.mode);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.feedrateExt);
            this.Controls.Add(this.actualZ);
            this.Controls.Add(this.actualY);
            this.Controls.Add(this.actualX);
            this.Controls.Add(this.currentCommand);
            this.Controls.Add(this.TempExt);
            this.Controls.Add(this.TempBed);
            this.Controls.Add(this.feedrateArm);
            this.Controls.Add(this.setZ);
            this.Controls.Add(this.setY);
            this.Controls.Add(this.setX);
            this.Controls.Add(this.play);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pickFile);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.extruderPort);
            this.Controls.Add(this.robotArmPort);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort RobotArm;
        private System.IO.Ports.SerialPort Extruder;
        private System.Windows.Forms.ComboBox robotArmPort;
        private System.Windows.Forms.ComboBox extruderPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer updatePorts;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button pickFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button pause;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Label setX;
        private System.Windows.Forms.Label setY;
        private System.Windows.Forms.Label setZ;
        private System.Windows.Forms.Label feedrateArm;
        private System.Windows.Forms.Label TempBed;
        private System.Windows.Forms.Label TempExt;
        private System.Windows.Forms.Label currentCommand;
        private System.Windows.Forms.Label actualZ;
        private System.Windows.Forms.Label actualY;
        private System.Windows.Forms.Label actualX;
        private System.Windows.Forms.Label feedrateExt;
        private System.Windows.Forms.Timer ExecuteFile;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label mode;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label actualE;
        private System.Windows.Forms.Label setE;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}


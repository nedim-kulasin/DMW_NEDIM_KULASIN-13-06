using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace DMV_GUI
{
    public partial class Form1 : Form
    {
        //changed to List for easier input/output of motorvehicles
        List<MotorVehicle> vehicles = new List<MotorVehicle> { };
        //String lastUsedFileName = "";
        //int count = 0;       
        public static string fileName = "log_"+(int)(DateTime.Today.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)+".txt";
        public static string backup = "C:\\DVM\\BACKUP";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(fileName))  
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write); 
                fileStream.Close(); 
            }
            else if (File.Exists(fileName))
            {
                if (Directory.Exists(backup))
                {
                    File.Move(fileName, backup + "/" + "backup-" + DateTime.Now.ToString("ss.mm.hh.dd") + "-" + fileName);
                    FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write); 
                    fileStream.Close(); 

                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Model_Click(object sender, EventArgs e)
        {

        }

        // Called when truck is selected as vehicle type
        private void radioButtonTruck_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = true;
            label1.Text = "maximum weight";
            textBox1.Visible = true;

            ComboBoxMake.Items.Clear();
            ComboBoxMake.Items.Add("Truck 1");
            ComboBoxMake.Items.Add("Truck 2");
            ComboBoxMake.Items.Add("Truck 3");
            ComboBoxMake.Items.Add("Truck 4");
            ComboBoxMake.Items.Add("Truck 5");
            ComboBoxMake.Sorted = true;

            ComboBoxMake.SelectedIndex = 0;
        }

        // Called when bus is selected as vehicle type
        private void radioButtonBus_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = true;
            label1.Text = "Company name";
            textBox1.Visible = true;

            ComboBoxMake.Items.Clear();
            ComboBoxMake.Items.Add("Bus 1");
            ComboBoxMake.Items.Add("Bus 2");
            ComboBoxMake.Items.Add("Bus 3");
            ComboBoxMake.Items.Add("Bus 4");
            ComboBoxMake.Items.Add("Bus 5");

            ComboBoxMake.Sorted = true;
            ComboBoxMake.SelectedIndex = 0;
        }

        // Called when register button is clicked
        private void buttonRegVeh_Click(object sender, EventArgs e)
        {
            MotorVehicle mv = null;
            if(radioButtonTruck.Checked)
            {                                                                         //cast                    //cast
                mv = new Truck(textBoxVIN.Text, ComboBoxMake.Text, textBoxModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, dateTimePicker1.Value, Convert.ToDouble(textBox1.Text));
            }
            if (radioButtonBus.Checked)
            {                                                                         //cast                    //cast
                mv = new Bus(textBoxVIN.Text, ComboBoxMake.Text, textBoxModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, dateTimePicker1.Value, textBox1.Text);
            }
            if (radioButtonCar.Checked)
            {                                                                         //cast                    //cast
                mv = new Car(textBoxVIN.Text, ComboBoxMake.Text, textBoxModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, dateTimePicker1.Value, textBox1.Text, radioButtonYes.Checked, Convert.ToInt32(textBox2.Text));
            }


            vehicles.Add(mv);
            richTextBox1.Clear();
            foreach (MotorVehicle mV in vehicles) 
            {
                if (mV != null)
                {
                    richTextBox1.AppendText(mv.show() + "\n\n");
                    FileStream file = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(file);
                    writer.WriteLine(mv.show());
                    writer.Close();
                    file.Close();
                }
            }

            
        }

        // Called when car is selected as vehicle type
        private void radioButtonCar_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = true;
            label1.Text = "Car Color";
            textBox1.Visible = true;
            label2.Visible = true;
            label2.Text = "Number of airbags";
            textBox2.Visible = true;
            label3.Visible = true;
            label3.Text = "Does the car have AC?";
            radioButtonYes.Visible = true;
            radioButtonNo.Visible = true;


            ComboBoxMake.Items.Clear();
            ComboBoxMake.Items.Add("Car 1");
            ComboBoxMake.Items.Add("Car 2");
            ComboBoxMake.Items.Add("Car 3");
            ComboBoxMake.Items.Add("Car 4");
            ComboBoxMake.Items.Add("Car 5");
            ComboBoxMake.Sorted = true;
            ComboBoxMake.SelectedIndex = 0;
        }

        // Called when taxi is selected as vehicle type
        private void radioButtonTaxi_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = true;
            label1.Text = "Car Color";
            textBox1.Visible = true;
            label2.Visible = true;
            label2.Text = "Number of airbags";
            textBox2.Visible = true;
            label3.Visible = true;
            label3.Text = "Driver has licence?";
            radioButtonYes.Visible = true;
            radioButtonNo.Visible = true;


            ComboBoxMake.Items.Clear();
            ComboBoxMake.Items.Add("Taxi 1");
            ComboBoxMake.Items.Add("Taxi 2");
            ComboBoxMake.Items.Add("Taxi 3");
            ComboBoxMake.Items.Add("Taxi 4");

            ComboBoxMake.Sorted = true;
            ComboBoxMake.SelectedIndex = 0;
        }
        



        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Called when load last vehicle is called
        private void LastVehicleButton_Click(object sender, EventArgs e)
        {

            var lines = File.ReadLines(fileName);
            string line = lines.Last();
            richTextBox1.AppendText("\n"+line+"\n");
        }

        

        private void VINLabel_Click(object sender, EventArgs e)
        {

        }

        //shows every vehicle
        private void button1_Click(object sender, EventArgs e)
        {

            foreach (var m in vehicles)
            {
                richTextBox1.AppendText(m.show());
            }

            
        }

    }
}

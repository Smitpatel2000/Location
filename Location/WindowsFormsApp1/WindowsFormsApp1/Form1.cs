using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device.Location;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        GeoCoordinateWatcher geo = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }                                  

        private void Log(string message)
        {
            listBox1.Items.Add(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            geo = new GeoCoordinateWatcher();

            geo.TryStart(false, TimeSpan.FromSeconds(2));                        
            while(geo.Status != GeoPositionStatus.Ready)
            {              
                geo.TryStart(false, TimeSpan.FromSeconds(2));
                label2.Text = "Finding ...";
            }
            label2.Text = "Location found";
            if (geo.Status == GeoPositionStatus.Ready)
            {
                timer1.Interval = 1000;
                timer1.Start();
            }            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            GeoCoordinate coord = geo.Position.Location;
            Log("Latitude: "  +coord.Latitude.ToString()  + ", Longitude: " +coord.Longitude.ToString());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("ms-settings:privacy-location");
        }
    }
}

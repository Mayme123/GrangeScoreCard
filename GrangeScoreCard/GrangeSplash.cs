// The splash screen for the Grange employee score card app
// 10/26/2015
// Written by: Matt Ayme

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrangeScoreCard
{
    public partial class GrangeSplash : Form
    {
        //used to count down time left to display the splash
        public int timeLeft { get; set; }

        public GrangeSplash()
        {
            InitializeComponent();
        }

        private void GrangeSplash_Load(object sender, EventArgs e)
        {
            // set the amount of time to display and start the count down
            timeLeft = 6;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // if there is still time on the timer, decrease timeLeft by one
            if (timeLeft > 0)
            {
                timeLeft -= 1;
            }
            // otherwise stop the timer and show the first form of the application
            else
            {
                timer1.Stop();
                new GrangeForm().Show();
                this.Hide();
            }
        }
    }
}

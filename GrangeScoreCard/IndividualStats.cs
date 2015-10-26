// This form displays the metrics stats for an individual employee
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
using GrangeClasses;

namespace GrangeScoreCard
{
    public partial class IndividualStats : Form
    {
        //this employee object is passed from the previous form and used to 
        //get the information to display
        Employee currentEmployee;

        //constructor takes an employee object for use in the form
        public IndividualStats(Employee employee)
        {
            this.currentEmployee = employee;
            InitializeComponent();
        }

        private void IndividualStats_Load(object sender, EventArgs e)
        {
            // set titles to include the name of the current employee
            WelcomLabel.Text = "Hello " + currentEmployee.getName() +"!";
            this.Text = currentEmployee.getName() + "'s Stats";

            // Set the labels to show the information from the employee object
            QualityScoreLabel.Text = currentEmployee.getQuality().ToString() + "%";
            AhtScoreLabel.Text = currentEmployee.getAht().ToString() + " seconds";
            AdherenceScoreLabel.Text = currentEmployee.getAdherence().ToString() + "%";
            KnowledgeScoreLabel.Text = currentEmployee.getKnowledge().ToString() + "%";

            totalScoreLabel.Text = "Your total score is "+ (currentEmployee.getTotalScore()).ToString();
        }

        private void WelcomLabel_Click(object sender, EventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            new GrangeForm().Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

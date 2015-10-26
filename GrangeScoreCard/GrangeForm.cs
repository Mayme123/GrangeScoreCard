// This is the main form of the application. It displays employee ranks by their total scores and
// has a login section where employees can get more information about their stats
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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using GrangeClasses;

namespace GrangeScoreCard
{
    public partial class GrangeForm : Form
    {
        // List used to rank employees and validate login
        List<Employee> employeeList;

        // Orders the employeeList by total score
        TotalScoreRanking totalScoreRanking;

        // Reads an excel spreadsheet to populate the employee list
        ExcelParser excelParser;
        
        public GrangeForm()
        {
            InitializeComponent();
        }

        private void GrangeForm_Load(object sender, EventArgs e)
        {
            // initiate the parser to read from the current user profile directory
            excelParser = new ExcelParser(System.IO.Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), "Metrics.xlsx"));

            // read the spreadsheet data into the employee list
            employeeList = excelParser.getEmployeeList();

            totalScoreRanking = new TotalScoreRanking();

            // add the employees from the list into the ranking object
            foreach(Employee employee in employeeList)
            {
                totalScoreRanking.addEmployee(employee);
            }

            // add the ranked employees to the listbox
            foreach(Employee employee in totalScoreRanking.getRanks())
            {
                RanksListBox.Items.Add((totalScoreRanking.getRanks().IndexOf(employee) + 1).ToString() + "          " +employee.getName());
               
            }
            
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // validate that the employee username and password is correct
            // if the employee is an admin, open the special admin view form
            if (UsernameTextBox.Text.Equals("admin") && PasswordTextBox.Text.Equals("adminpassword"))
            {
                new AdminView(employeeList).Show();
                this.Hide();
            }
            else if(PasswordTextBox.Text.Equals("password"))
            {
                Employee loginEmployee = employeeList.Find(em => em.getName().Equals(UsernameTextBox.Text));
                if (loginEmployee != null)
                {
                    new IndividualStats(loginEmployee).Show();
                    this.Hide(); 
                }
            }
            else
            {
                MessageBox.Show("Please enter valid username and password.");// shown if info is not valid
            }
        }

        private void UsernameLabel_Click(object sender, EventArgs e)
        {

        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

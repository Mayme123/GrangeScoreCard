// This form is a special view for admins to get specific metric
// and ranking info about every employee
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
    public partial class AdminView : Form
    {
        // Employee list used to populate the listboxes and labels
        List<Employee> employeeList;

        // Ranking objects used to order the employee list by certain properties
        QualityRanking qualityRanking;
        AhtRanking ahtRanking;
        AdherenceRanking adherenceRanking;
        KnowledgeRanking knowledgeRanking;
        TotalScoreRanking totalScoreRanking;

        // Constructor takes a list of employees for use in the form
        public AdminView(List<Employee> employees)
        {
            this.employeeList = employees;
            InitializeComponent();
        }

        // on load, fill the listboxes and labels with the first employee in the list
        private void AdminView_Load(object sender, EventArgs e)
        {
            qualityRanking = new QualityRanking();
            ahtRanking = new AhtRanking();
            adherenceRanking = new AdherenceRanking();
            knowledgeRanking = new KnowledgeRanking();
            totalScoreRanking = new TotalScoreRanking();

            // add employees to each ranking object
            foreach(Employee employee in employeeList)
            {
                EmployeeListBox.Items.Add(employee.getName());
                qualityRanking.addEmployee(employee);
                ahtRanking.addEmployee(employee);
                adherenceRanking.addEmployee(employee);
                knowledgeRanking.addEmployee(employee);
                totalScoreRanking.addEmployee(employee);
            }

            // the next foreach loops add to the listboxes based on the ranking objects
            foreach(Employee employee in qualityRanking.getRanks())
            {
                QualityListBox.Items.Add(employee.getName() + "   " + employee.getQuality() + "%");
            }

            foreach (Employee employee in ahtRanking.getRanks())
            {
                AhtListBox.Items.Add(employee.getName() + "   " + employee.getAht() + " seconds");
            }

            foreach (Employee employee in adherenceRanking.getRanks())
            {
                AdherenceListBox.Items.Add(employee.getName() + "   " + employee.getAdherence() + "%");
            }

            foreach (Employee employee in knowledgeRanking.getRanks())
            {
                KnowledgeListBox.Items.Add(employee.getName() + "   " + employee.getKnowledge() + "%");
            }

            foreach (Employee employee in totalScoreRanking.getRanks())
            {
                TotalScoreListBox.Items.Add(employee.getName() + "   " + employee.getTotalScore());
            }

            //set first employee to be the selected one
            EmployeeListBox.SetSelected(0,true);
        }

        private void EmployeeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //when an employee is selected, fill labels with the individual stats and 
            //highlight them in the ranks 
            Employee employee = employeeList.Find(em => em.getName() == (string)EmployeeListBox.SelectedItem);

            // set labels of individuals metric info
            AhtScoreLabel.Text = employee.getAht().ToString()+ " seconds";
            AdherenceScoreLabel.Text = employee.getAdherence().ToString()+ "%";
            KnowledgeScoreLabel.Text = employee.getKnowledge().ToString() + "%";
            QualityScoreLabel.Text = employee.getQuality().ToString() + "%";
            TotalScoreLabel.Text = employee.getTotalScore().ToString();

            // highlight the ranks of the selected employee
            TotalScoreListBox.SetSelected(totalScoreRanking.getRanks().IndexOf(employee), true);
            QualityListBox.SetSelected(qualityRanking.getRanks().IndexOf(employee), true);
            AhtListBox.SetSelected(ahtRanking.getRanks().IndexOf(employee), true);
            AdherenceListBox.SetSelected(adherenceRanking.getRanks().IndexOf(employee), true);
            KnowledgeListBox.SetSelected(knowledgeRanking.getRanks().IndexOf(employee), true);
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            new GrangeForm().Show();
            this.Hide();
        }
    }
}

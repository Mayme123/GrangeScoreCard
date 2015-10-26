// This class is used to make a list of employee objects made from information
// read from an excel spreadsheet
// 10/26/2015
// Written by: Matt Ayme

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using GrangeClasses;

namespace GrangeScoreCard
{
    public class ExcelParser
    {
        //File path of the spreadsheet
        string path;

        //Constructor takes a string thats represents the path of the file to be read
        public ExcelParser(string filePath)
        {
            this.path = filePath;
        }

        // Method returns the list of employees gotten from reading the spreadsheet
        public List<Employee> getEmployeeList()
        {
            return getListFromSpreadSheet();
        }

        //Read spreadsheet and return a list
        private List<Employee> getListFromSpreadSheet()
        {
            //open an excel process, then a workbook from the file path
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);

            //get a worksheet from the workbook and define the range as every cell
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            List<Employee> employeeList = new List<Employee>();

            Employee employeeToAdd = new Employee();//temp employee object used to populate the list

            /* Used for testing
            try
            {
                for(int i = 1; i <= xlRange.Rows.Count; ++i)
                {
                    for(int j = 1; j <= xlRange.Columns.Count; ++j)
                    {
                        Console.WriteLine(xlRange.Cells[i, j].Value2.ToString());
                    }
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
            */

            try
            {
             
                // iterate through each cell in a row
                for(int i = 2; i <= xlRange.Rows.Count; ++i)
                {
                    employeeToAdd = new Employee();
                    for(int j = 1; j <= xlRange.Columns.Count; ++j)
                    {
                        // each if here checks the first row for a column name
                        // if the name matches an employee property, it adds
                        // the value of the current row to the respective property
                        if(xlRange.Cells[1,j].Value2.ToString().Equals("Name"))
                        {
                            employeeToAdd.setName(xlRange.Cells[i, j].Value2.ToString());
                        }

                        if (xlRange.Cells[1, j].Value2.ToString().Equals("Aht"))
                        {
                            employeeToAdd.setAht((int)xlRange.Cells[i, j].Value2);
                        }

                        if (xlRange.Cells[1, j].Value2.ToString().Equals("Adherence"))
                        {
                            employeeToAdd.setAdherence((Decimal)xlRange.Cells[i, j].Value2);
                        }

                        if (xlRange.Cells[1, j].Value2.ToString().Equals("Knowledge"))
                        {
                            employeeToAdd.setKnowledge((Decimal)xlRange.Cells[i, j].Value2);
                        }

                        if (xlRange.Cells[1, j].Value2.ToString().Equals("Quality"))
                        {
                            employeeToAdd.setQuality((Decimal)xlRange.Cells[i, j].Value2);
                        }

                    }

                    //add completed employee to the list
                    employeeList.Add(employeeToAdd);
                }

            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }

            // Close everything up and kill excel process
            xlWorkbook.Close(0);
            xlApp.Quit();
            killExcel();

            return employeeList;
        }

        // This makes sure the excel process gets killed
        private void killExcel()
        {
            System.Diagnostics.Process[] PROC = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process PK in PROC)
            {
                if (PK.MainWindowTitle.Length == 0)
                {
                    PK.Kill();
                }
            }
        }
    }
}

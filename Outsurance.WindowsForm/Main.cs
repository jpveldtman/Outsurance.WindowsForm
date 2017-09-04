using Outsurance.WindowsForm.Classes;
using Outsurance.WindowsForm.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Outsurance.WindowsForm
{
    public partial class Main : Form
    {
        List<Person_Class> _peoplelist = new List<Person_Class>();
        private string _file1 = "Count.txt";
        private string _file2 = "Alphabetically.txt";

        public Main()
        {
            InitializeComponent();
        }

        //Import the data from csv file chosen
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var fileName = openFileDialog.FileName;
                    string error = "";
                    if (Helpers.FileAccess.ValidateFormat(fileName))
                    {
                        _peoplelist = People_Class.ReadAllPeople(fileName, out error);
                        if (string.IsNullOrEmpty(error))//File imported succesfully
                        {
                            toolStripStatusLabel.Text = "File Imported Succesfully";
                            dataGridView1.DataSource = _peoplelist;
                            SetVisibility();
                        }
                        else
                            toolStripStatusLabel.Text = error;
                    }
                    else
                        toolStripStatusLabel.Text = "File format incorrect, [Name, Surname, Address]";

                }
                catch (Exception ex)
                {
                    toolStripStatusLabel.Text = ex.Message;
                }
            }
        }

        //show the action buttons
        private void SetVisibility()
        {
            bool actionvisible = _peoplelist.Count > 0;

            btnAlphabetically.Visible = actionvisible;
            btnPopularity.Visible = actionvisible;
            btnClear.Visible = actionvisible;
            btnImport.Visible = !actionvisible;
        }

        //Populate the count of the items passed from the csv file, populate to grid and ask to open file
        private void btnPopularity_Click(object sender, EventArgs e)
        {
            var firstNameSurnameCount = Sorter.GetWordCountDescendingOrder(People_Class.GetFullNameList());
            var names = (from row in firstNameSurnameCount select new { Name = row.Key, Count = row.Value });
            dataGridView1.DataSource = names.ToList();

            string error = null;
            Helpers.FileAccess.WriteAllLines(_file1, names.Select(x => x.Name + "\t" + x.Count), out error);

            if (string.IsNullOrEmpty(error))
            {
                toolStripStatusLabel.Text = "File with Names Count generated";
                if (MessageBox.Show("File generated succesfully, would you like to open the file now?", "Open File", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    Process.Start("notepad.exe", _file1);
            }
            else
                toolStripStatusLabel.Text = error;
        }

        //Populate the addresses alphabetically of the items passed from the csv file, populate to grid and ask to open file
        private void btnAlphabetically_Click(object sender, EventArgs e)
        {
            var addresses = Sorter.GetAsendingListIgnoringNumbers(People_Class.GetAddressList());
            dataGridView1.DataSource = addresses.Select(x => new { Address = x }).ToList();

            string error = null;
            Helpers.FileAccess.WriteAllLines(_file2, addresses.ToArray(), out error);

            if (string.IsNullOrEmpty(error))
            {
                if (MessageBox.Show("File generated succesfully, would you like to open the file now?", "Open File", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    Process.Start("notepad.exe", _file2);
            }
            else
                toolStripStatusLabel.Text = error;
        }

        //Reset the file input
        private void btnClear_Click(object sender, EventArgs e)
        {
            _peoplelist = new List<Person_Class>();
            dataGridView1.DataSource = null;
            toolStripStatusLabel.Text = "Ready";
            SetVisibility();
        }
    }
}

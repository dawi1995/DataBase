using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DisplayPeople();
        }
        private void DisplayPeople()
        {
            try
            {
                using (var dp = new Model())
                {
                    var people = from p in dp.People
                                 select new
                                 {
                                     p.Id,
                                     p.FirstName,
                                     p.LastName
                                 };
                    dgvPeople.DataSource = people.ToList();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("There is no connection to the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddPerson(int id, string firstName, string lastName)
        {
            try
            {
                using (var dp = new Model())
                {
                    Person person = new Person();
                    person.Id = id;
                    person.FirstName = firstName;
                    person.LastName = lastName;

                    dp.People.Add(person);
                    dp.SaveChanges();
                    MessageBox.Show("Successfully added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("There is no connection to the database ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayPeople();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(tbId.Text);
                string fn = tbFn.Text;
                string ln = tbLn.Text;
                AddPerson(id, fn, ln);
            }
            catch(Exception)
            {
                MessageBox.Show("Incorrect input data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

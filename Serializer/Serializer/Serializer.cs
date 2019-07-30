using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serializer
{
    public partial class Serializer : Form
    {

        private int currentSerialNum;

        public Serializer()
        {
            InitializeComponent();
        }

        private void RefreshForm()
        {
            Person p = Person.DeSerialize();
            txtName.Text = p.Name;
            txtAddress.Text = p.Address;
            txtPhone.Text = p.PhoneNumber;
            currentSerialNum = Person.GetLatestSerialNum();
        }

        private void RefreshForm(int serialNum)
        {
            Person p = Person.DeSerialize(serialNum);
            txtName.Text = p.Name;
            txtAddress.Text = p.Address;
            txtPhone.Text = p.PhoneNumber;
            currentSerialNum = serialNum;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtAddress.Text) && !string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                Person p = new Person(txtName.Text, txtAddress.Text, txtPhone.Text, Person.GetLatestSerialNum() + 1);
                p.Serialize();
                MessageBox.Show("Serialization completed.");
            }
        }

        private void Serializer_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            List<string> fileNames = new List<string>(Directory.GetFiles(@"C:\Users\Tulajdonos\Desktop\Codecool\Serializer\Serializer"));
            if (fileNames.Contains(@"C:\Users\Tulajdonos\Desktop\Codecool\Serializer\Serializer\person" + (currentSerialNum - 1) + ".dat"))
            {
                RefreshForm(currentSerialNum - 1);
            }
            else
            {
                MessageBox.Show("First entry reached.");
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            List<string> fileNames = new List<string>(Directory.GetFiles(@"C:\Users\Tulajdonos\Desktop\Codecool\Serializer\Serializer"));
            if (fileNames.Contains(@"C:\Users\Tulajdonos\Desktop\Codecool\Serializer\Serializer\person" + (currentSerialNum + 1) + ".dat"))
            {
                RefreshForm(currentSerialNum + 1);
            }
            else
            {
                MessageBox.Show("Last entry reached.");
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            RefreshForm(1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            RefreshForm(Person.GetLatestSerialNum());
        }
    }
}

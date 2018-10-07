using System;
using System.Windows.Forms;

namespace GreetingsSolution
{
    public partial class GreetingForm : Form
    {
        public GreetingForm()
        {
            InitializeComponent();
        }

        private void GreetingButton_Click(object sender, EventArgs e)
        {
            var userName = this.EnterNameTextbox.Text;

            if (isNameEntered(userName))
            {
                MessageBox.Show($"Hello, {userName}!");
            }
            else
            {
                MessageBox.Show($"Please enter your name!");
            }
        }

        private static bool isNameEntered(string userName)
        {
            return !string.IsNullOrEmpty(userName);
        }
    }
}

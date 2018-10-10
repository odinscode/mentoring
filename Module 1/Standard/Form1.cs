using System;
using System.Windows.Forms;
using GreetingsLibrary;

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
            MessageBox.Show(GreetingService.GreetPerson(userName));
        }
    }
}

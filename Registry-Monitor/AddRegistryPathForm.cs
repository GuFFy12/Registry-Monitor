using System;
using System.Linq;
using System.Windows.Forms;
using Registry_Monitor.RegistryUtils;

namespace Registry_Monitor
{
    public partial class AddRegistryPath : Form
    {
        public RegistryPath RegistryPath;

        public AddRegistryPath()
        {
            InitializeComponent();

            registryEventComboBox.Items.AddRange(Enum.GetValues(typeof(WmiRegistryEventListener.RegistryEvent)).Cast<object>().ToArray());
            registryEventComboBox.Text = WmiRegistryEventListener.RegistryEvent.RegistryKeyChangeEvent.ToString();
        }

        private void trackTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            registryValueTextBox.ReadOnly = registryEventComboBox.Text != WmiRegistryEventListener.RegistryEvent.RegistryValueChangeEvent.ToString();
        }

        private void addRegistryPathButton_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryPath = new RegistryPath((WmiRegistryEventListener.RegistryEvent)registryEventComboBox.SelectedIndex, registryPathTextBox.Text, registryValueTextBox.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                RegistryPath = null;
                return;
            }

            Close();
        }
    }
}
using System;
using System.Linq;
using System.Windows.Forms;
using Registry_Monitor.RegistryUtils;

namespace Registry_Monitor
{
    public partial class AddWmiRegistryEventListener : Form
    {
        public WmiRegistryEventListener WmiRegistryEventListener;

        public AddWmiRegistryEventListener()
        {
            InitializeComponent();

            registryEventComboBox.Items.AddRange(Enum.GetValues(typeof(WmiRegistryEventListener.RegistryEvent)).Cast<object>().ToArray());
            registryEventComboBox.Text = WmiRegistryEventListener.RegistryEvent.RegistryKeyChangeEvent.ToString();
        }

        private void registryEventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            registryValueTextBox.ReadOnly = registryEventComboBox.Text != WmiRegistryEventListener.RegistryEvent.RegistryValueChangeEvent.ToString();
        }

        private void addWmiRegistryEventListenerButton_Click(object sender, EventArgs e)
        {
            try
            {
                var registryPath = new RegistryPath((WmiRegistryEventListener.RegistryEvent)registryEventComboBox.SelectedIndex, registryPathTextBox.Text,
                    registryValueTextBox.Text);
                WmiRegistryEventListener = new WmiRegistryEventListener(registryPath);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Close();
        }
    }
}
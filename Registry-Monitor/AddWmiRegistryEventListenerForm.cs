using System;
using System.Linq;
using System.Windows.Forms;
using Registry_Monitor.RegistryUtils;

namespace Registry_Monitor
{
    public partial class AddWmiRegistryEventListener : Form
    {
        // Property to store the WmiRegistryEventListener created in this form
        public WmiRegistryEventListener WmiRegistryEventListener;

        // Constructor for the AddWmiRegistryEventListener form
        public AddWmiRegistryEventListener()
        {
            InitializeComponent();

            // Populate the registryEventComboBox with values from the RegistryEvent enumeration
            registryEventTypeComboBox.Items.AddRange(Enum.GetValues(typeof(WmiRegistryEventListener.RegistryEvent)).Cast<object>().ToArray());
            registryEventTypeComboBox.Text = WmiRegistryEventListener.RegistryEvent.RegistryKeyChangeEvent.ToString();
        }

        /**
         * Event handler for the registryEventComboBox's SelectedIndexChanged event.
         * Disables the value text box if the selected registry event is not RegistryValueChangeEvent.
         */
        private void registryEventTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            registryValueTextBox.ReadOnly = registryEventTypeComboBox.Text != WmiRegistryEventListener.RegistryEvent.RegistryValueChangeEvent.ToString();
        }

        /**
         * Event handler for the addWmiRegistryEventListenerButton's Click event.
         * Tries to parse the registry path, and if successful, creates a new WmiRegistryEventListener.
         */
        private void addWmiRegistryEventListenerButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Attempt to create a new RegistryPath using the selected registry event and input values
                var registryPath = new RegistryPath(
                    (WmiRegistryEventListener.RegistryEvent)registryEventTypeComboBox.SelectedIndex,
                    registryPathTextBox.Text,
                    registryValueTextBox.Text
                );

                // Create a new WmiRegistryEventListener with the parsed registry path
                WmiRegistryEventListener = new WmiRegistryEventListener(registryPath);
            }
            catch (Exception exception)
            {
                // Show a message box with the exception details if parsing fails
                MessageBox.Show(exception.Message, exception.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Close the form after successfully creating the WmiRegistryEventListener
            Close();
        }
    }
}
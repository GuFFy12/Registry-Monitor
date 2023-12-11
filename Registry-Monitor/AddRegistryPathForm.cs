using System;
using System.Linq;
using System.Windows.Forms;
using Registry_Monitor.RegistryUtils;

namespace Registry_Monitor
{
    public partial class AddRegistryPath : Form
    {
        private readonly MainForm.LoggerDelegate _logger;

        public RegistryPath RegistryPath;

        public AddRegistryPath(MainForm.LoggerDelegate loggerDelegate)
        {
            _logger = loggerDelegate;

            InitializeComponent();

            trackTypeComboBox.Items.AddRange(Enum.GetValues(typeof(WmiRegistryEventListener.TrackTypes)).Cast<object>().ToArray());
            trackTypeComboBox.Text = WmiRegistryEventListener.TrackTypes.RegistryKeyChangeEvent.ToString();
        }

        private void trackTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            registryValueTextBox.ReadOnly = trackTypeComboBox.Text != WmiRegistryEventListener.TrackTypes.RegistryValueChangeEvent.ToString();
        }

        private void addRegistryPathButton_Click(object sender, EventArgs e)
        {
            RegistryPath = new RegistryPath((WmiRegistryEventListener.TrackTypes)trackTypeComboBox.SelectedIndex,
                registryPathTextBox.Text, registryValueTextBox.Text, _logger);

            if (RegistryPath.Error)
            {
                RegistryPath = null;
                return;
            }

            Close();
        }
    }
}
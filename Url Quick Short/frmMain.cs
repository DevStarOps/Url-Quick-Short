using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Url_Quick_Short.Integrate;
using Url_Quick_Short.Integrate.Fields;

namespace Url_Quick_Short
{
    public partial class frmMain : Form
    {
        public Settings SystemSettings { get; private set; }

        public static frmMain Instance { get; private set; }

        public frmMain()
        {
            Instance = this;
            InitializeComponent();
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            SystemSettings = await Settings.Load();

            SystemSettings.StartKeyLogger();

            await LoadProviders();

            await LoadTrigger();
        }

        private async Task LoadTrigger()
        {
            cbxSupportedKeys.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(Keylogger.KeysWeCareAbout)))
            {
                cbxSupportedKeys.Items.Add(item.ToString());
            }
            cbxSupportedKeys.Text = SystemSettings.TriggerKey;
            chkUseAltKey.Checked = SystemSettings.TriggerUseAlt;
            chkUseCtrlKey.Checked = SystemSettings.TriggerUseCtrl;
            chkUseShiftKey.Checked = SystemSettings.TriggerUseShift;
        }

        private async Task LoadProviders()
        {
            cbxProviders.DisplayMember = "Name";
            cbxProviders.ValueMember = "Id";
            cbxProviders.DataSource = CommonIntegrationHelper.GetProviders();

            if (SystemSettings.CurrentProvider != null)
            {
                SetProviderSelectedIndex();
            }
        }

        private async Task SwitchAuthenticationProvider()
        {
            if (cbxProviders.SelectedValue != null)
            {
                SystemSettings.CurrentProviderId = (Guid)cbxProviders.SelectedValue;
                SetProviderSelectedIndex();
                await SystemSettings.Save();
                LoadAuthenticationFields();
                LoadOtherSettings();
            }
        }

        private void LoadOtherSettings()
        {
            enableKeyLogDebugToolStripMenuItem.Checked = SystemSettings.LogKeysDebug;
        }

        private void SetProviderSelectedIndex()
        {
            cbxProviders.SelectedIndex = cbxProviders.Items.IndexOf(SystemSettings.CurrentProvider);
        }

        private void LoadAuthenticationFields()
        {
            splitContainerAuthentication.Panel1.Controls.Clear();
            List<Control> controls = new List<Control>();
            foreach (var field in SystemSettings.CurrentProvider.AuthenticationFields)
            {
                controls.Add(GetLabel(field.DisplayName));
                var control = field.GetControl();
                control.Name = GetControlName(field);
                field.SetControlValue(control, SystemSettings.GetFieldValue(field));
                controls.Add(control);
            }
            AddAuthenticationControl(controls);
        }

        private static string GetControlName(FieldBase field)
        {
            return "control" + field.Id.ToString("N");
        }

        private void AddAuthenticationControl(List<Control> controls)
        {
            controls.Reverse();
            splitContainerAuthentication.Panel1.Invoke(new Action(() =>
            {
                int tabIndex = 0;
                foreach (var control in controls)
                {
                    control.TabIndex = tabIndex;
                    control.Dock = DockStyle.Top;
                    control.Margin = new Padding(5);
                    splitContainerAuthentication.Panel1.Controls.Add(control);
                    tabIndex++;
                }
            }));
        }

        private Label GetLabel(string text)
        {
            Label lbl = new Label();
            lbl.Text = text;
            return lbl;
        }

        private async void cbxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            await SwitchAuthenticationProvider();
        }

        private void btnTryAuthenticate_Click(object sender, EventArgs e)
        {
            if (SystemSettings.CurrentProvider != null)
            {
                Task.Run(TryAuthenticate);
            }
        }

        private async Task TryAuthenticate()
        {
            try
            {
                Dictionary<string, string> authenticationFieldsEncrypted = new Dictionary<string, string>();
                Dictionary<string, string> authenticationFieldsClear = new Dictionary<string, string>();
                foreach (Control control in splitContainerAuthentication.Panel1.Controls)
                {
                    foreach (var field in SystemSettings.CurrentProvider.AuthenticationFields)
                    {
                        if (GetControlName(field) == control.Name)
                        {
                            string value = field.GetControlValue(control);
                            authenticationFieldsClear.Add(field.DisplayName, value);
                            authenticationFieldsEncrypted.Add(field.DisplayName, field.EncryptValue(value));
                            break;
                        }
                    }
                }

                string authenticationData = await SystemSettings.CurrentProvider.GetAuthenticationData(authenticationFieldsClear);

                SystemSettings.AuthenticationFieldValues = authenticationFieldsEncrypted;
                SystemSettings.SetAuthenticationData(authenticationData);
                await SystemSettings.Save();
                MessageBox.Show("Authentication successful!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error authenticating", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openApplicationPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(Application.ExecutablePath));
        }

        private void openStoragePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Storage.StorageRootLocation);
        }

        private async void btnApplyTriggerKey_Click(object sender, EventArgs e)
        {
            try
            {
                SystemSettings.TriggerKey = cbxSupportedKeys.Text;
                SystemSettings.TriggerUseAlt = chkUseAltKey.Checked;
                SystemSettings.TriggerUseCtrl = chkUseCtrlKey.Checked;
                SystemSettings.TriggerUseShift = chkUseShiftKey.Checked;
                await SystemSettings.Save();                
                MessageBox.Show("New trigger applied!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error applying trigger", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void enableKeyLogDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemSettings.LogKeysDebug = !SystemSettings.LogKeysDebug;
            await SystemSettings.Save();
            enableKeyLogDebugToolStripMenuItem.Checked = SystemSettings.LogKeysDebug;
        }

        private void forceKeyFlushToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemSettings.logger.Flush2File(true);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
            else
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }
    }
}

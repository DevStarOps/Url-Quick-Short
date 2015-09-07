using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Url_Quick_Short.Integrate.Security;

namespace Url_Quick_Short.Integrate.Fields
{
    public class SecureTextField : FieldBase
    {
        public SecureTextField(string displayName)
        {
            DisplayName = displayName;
        }

        public override string DisplayName { get; }

        public override Guid Id => new Guid("{44CE3E45-EBE4-492E-8B81-D862B1F8CE13}");

        public virtual string SharedSecret => Id.ToString();

        public override Control GetControl()
        {
            return new TextBox() { UseSystemPasswordChar = true };
        }

        public override string GetControlValue(Control control)
        {
            if (control is TextBox)
            {
                return ((TextBox)control).Text;
            }
            return null;
        }

        public override void SetControlValue(Control control, string value)
        {
            if (control is TextBox)
            {
                ((TextBox)control).Text = (value ?? string.Empty);
            }
        }

        public override string EncryptValue(string value)
        {
            return Crypto.EncryptStringAES(value, SharedSecret);
        }

        public override string DecryptValue(string value)
        {
            return Crypto.DecryptStringAES(value, SharedSecret);
        }
    }
}

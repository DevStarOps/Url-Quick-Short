using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Url_Quick_Short.Integrate.Fields
{
    public class TextField : FieldBase
    {
        public TextField(string displayName)
        {
            DisplayName = displayName;
        }

        public override string DisplayName { get; }

        public override Guid Id => new Guid("{FD51078B-ABB8-4BCB-B96D-2BD24C8CC4BF}");

        public override Control GetControl()
        {
            return new TextBox();
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
    }
}

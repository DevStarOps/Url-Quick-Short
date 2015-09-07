using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Url_Quick_Short.Integrate.Fields
{
    public class BoolField : FieldBase
    {
        public BoolField(string displayName)
        {
            DisplayName = displayName;
        }

        public override string DisplayName { get; }

        public override Guid Id => new Guid("{07823960-B734-49CD-A257-8F5518CEBD2F}");

        public override Control GetControl()
        {
            return new CheckBox();
        }

        public override string GetControlValue(Control control)
        {
            if (control is CheckBox)
            {
                return ((CheckBox)control).Checked ? "1" : "0";
            }
            return null;
        }

        public override void SetControlValue(Control control, string value)
        {
            if (control is CheckBox)
            {
                ((CheckBox)control).Checked = (value ?? "0") == "1";
            }
        }
    }
}

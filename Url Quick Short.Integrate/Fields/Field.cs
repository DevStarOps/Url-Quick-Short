using System;
using System.Windows.Forms;

namespace Url_Quick_Short.Integrate.Fields
{
    public abstract class FieldBase
    {
        public abstract Guid Id { get; }
        public abstract string DisplayName { get; }
        public abstract Control GetControl();
        public abstract string GetControlValue(Control control);
        public abstract void SetControlValue(Control control, string value);
        public virtual string EncryptValue(string value)
        {
            return value;
        }
        public virtual string DecryptValue(string value)
        {
            return value;
        }
    }
}
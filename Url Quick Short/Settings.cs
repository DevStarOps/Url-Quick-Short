using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Url_Quick_Short.Integrate;
using Url_Quick_Short.Integrate.Dto;
using Url_Quick_Short.Integrate.Fields;
using Url_Quick_Short.Integrate.Security;

namespace Url_Quick_Short
{
    public class Settings
    {
        public static string[] DataContainer = new[] { "System" };
        public const string SettingsFileName = "settings.json";

        public Guid CurrentProviderId { get; set; }
        internal IIntegration CurrentProvider => CommonIntegrationHelper.GetProviders().FirstOrDefault(o => o.Id == CurrentProviderId);

        public Dictionary<string, string> AuthenticationFieldValues { get; set; } = new Dictionary<string, string>();
        public string AuthenticationData { get; private set; }

        public string TriggerKey { get; set; } = "+";
        public bool TriggerUseCtrl { get; set; } = true;
        public bool TriggerUseAlt { get; set; } = false;
        public bool TriggerUseShift { get; set; } = false;
        public bool LogKeysDebug { get; set; } = false;

        internal Keylogger logger { get; set; } = null;

        public async Task Save()
        {
            await Storage.WriteData(DataContainer, SettingsFileName, this);
        }

        public static async Task<Settings> Load()
        {
            var result = await Storage.ReadData<Settings>(DataContainer, SettingsFileName);
            await LoadSalt();
            return result;
        }

        public void StartKeyLogger()
        {
            logger = new Keylogger(Path.Combine(Storage.StorageRootLocation, "keys.log"), true);
            logger.FlushInterval = 60000;
            logger.Enabled = true;
        }

        private static async Task LoadSalt()
        {
            if (string.IsNullOrEmpty(Crypto.Salt))
            {
                var obj = await Storage.ReadData<StringValue>(null, "salt");
                if (string.IsNullOrEmpty(obj.Value))
                {
                    obj.Value = Guid.NewGuid().ToString("N");
                    await Storage.WriteData(null, "salt", obj);
                    Crypto.Salt = obj.Value;
                }
                else
                {
                    Crypto.Salt = obj.Value;
                }
            }
        }

        public string GetFieldValue(FieldBase field)
        {
            if (AuthenticationFieldValues.ContainsKey(field.DisplayName))
            {
                return field.DecryptValue(AuthenticationFieldValues[field.DisplayName]);
            }
            return string.Empty;
        }

        public void SetAuthenticationData(string authenticationData)
        {
            AuthenticationData = Crypto.EncryptStringAES(authenticationData, CurrentProvider.SharedSecret);
        }

        public string GetAuthenticationData(string authenticationData)
        {
            return Crypto.DecryptStringAES(AuthenticationData, CurrentProvider.SharedSecret);
        }
    }
}

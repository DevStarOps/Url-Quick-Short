using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Url_Quick_Short.Integrate
{
    public static class Storage
    {
        private readonly static FileSystemWatcher storageWatcher;

        public static string StorageRootLocation => Path.Combine((string.IsNullOrEmpty(ConfigurationManager.AppSettings["StoragePathRoot"]) ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : ConfigurationManager.AppSettings["StoragePathRoot"]), "Url Quick Short", "Storage");

        public delegate Task StorageUpdateDelegate(string[] datacontainer, string targetToken);

        public static event StorageUpdateDelegate StorageUpdate;

        static Storage()
        {
            if (!Directory.Exists(StorageRootLocation))
            {
                Directory.CreateDirectory(StorageRootLocation);
            }
            storageWatcher = new FileSystemWatcher(StorageRootLocation);
            storageWatcher.IncludeSubdirectories = true;
            storageWatcher.Created += storageWatcher_Created;
            storageWatcher.Changed += storageWatcher_Changed;
            storageWatcher.EnableRaisingEvents = true;
        }

        static void storageWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            RaiseStorageUpdated(e.FullPath);
        }

        private static void RaiseStorageUpdated(string fullPath)
        {
            if (StorageUpdate != null)
            {
                List<string> segments = new List<string>(fullPath.Remove(0, StorageRootLocation.Length).Trim('\\').Split('\\'));
                string dataToken = segments[segments.Count - 1];
                segments.RemoveAt(segments.Count - 1);
                StorageUpdate(segments.ToArray(), dataToken);
            }
        }

        static void storageWatcher_Created(object sender, FileSystemEventArgs e)
        {
            RaiseStorageUpdated(e.FullPath);
        }

        private static string GetFullPath(string[] dataContainer, string dataToken)
        {
            string result = StorageRootLocation;
            foreach (var segment in (dataContainer ?? new string[0]))
            {
                result = Path.Combine(result, segment);
            }
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }
            result = Path.Combine(result, dataToken);
            return result;
        }

        public static async Task WriteData(string[] dataContainer, string dataToken, string dataContents)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(GetFullPath(dataContainer, dataToken), FileMode.Create, FileAccess.Write, FileShare.Read);
                sw = new StreamWriter(fs);
                await sw.WriteAsync(dataContents);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                    sw = null;
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
            }
        }

        public static async Task WriteData(string[] dataContainer, string dataToken, object dataContents)
        {
            await WriteData(dataContainer, dataToken, JsonConvert.SerializeObject(dataContents, Formatting.Indented));
        }

        public static async Task<string> ReadData(string[] dataContainer, string dataToken)
        {
            string result = string.Empty;
            string path = GetFullPath(dataContainer, dataToken);
            if (File.Exists(path))
            {
                FileStream fs = null;
                StreamReader sr = null;
                try
                {
                    fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    sr = new StreamReader(fs);
                    result = await sr.ReadToEndAsync();
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();
                        sr.Dispose();
                        sr = null;
                    }
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                }
            }
            return result;
        }

        public static async Task<T> ReadData<T>(string[] dataContainer, string dataToken) where T : new()
        {
            string contents = await ReadData(dataContainer, dataToken);
            if (!string.IsNullOrEmpty(contents))
            {
                return JsonConvert.DeserializeObject<T>(contents);
            }
            return new T();
        }
    }
}

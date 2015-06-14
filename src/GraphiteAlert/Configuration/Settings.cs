using System.Configuration;
using GraphiteAlert.Extensions;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GraphiteAlert.Configuration
{
    public class Settings : ISettings
    {
        private const string SettingsFileName = @"Configuration\settings.json";
        private static readonly Lazy<ISettings> LazyInstance = new Lazy<ISettings>(GetInstance);

        public static ISettings Instance
        {
            get { return LazyInstance.Value; }
        }

        private static ISettings GetInstance()
        {
            var filePath = ConfigurationFilePath;
            if (null == filePath
                || !File.Exists(filePath))
            {
                throw new FileNotFoundException("Could not find a valid configuration file at the path " + SettingsFileName);
            }
            var fileContents = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Settings>(fileContents);
        }

        private static string ConfigurationFilePath
        {
            get
            {
                var candidateDirectories = new Func<string>[]
                {
                    () => ConfigurationManager.AppSettings["SettingsPath"],
                    () => HttpRuntime.AppDomainAppPath,
                    () => AppDomain.CurrentDomain.SetupInformation.ConfigurationFile,
                    () => Environment.CurrentDirectory,
                    () => Assembly.GetEntryAssembly().IfNotNull(x => x.GetDirectory()),
                    () => Assembly.GetCallingAssembly().IfNotNull(x => x.GetDirectory()),
                    () => Assembly.GetExecutingAssembly().IfNotNull(x => x.GetDirectory())
                }
                .Select(getDirectory =>
                {
                    try
                    {
                        return getDirectory();
                    }
                    catch (Exception)
                    {
                        // Perhaps wrong domain, largely for testing
                        return null;
                    }
                })
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
                var removeBinDirectories = candidateDirectories.Where(x => x.Contains("bin"))
                    .Select(x => x.ToLower().Replace(@"\bin", "").Replace(@"\debug", "").Replace(@"\release", ""));
                var directoriesWithoutFileNames = candidateDirectories.Select(Path.GetDirectoryName);
                var runningDirectories = candidateDirectories
                    .Concat(removeBinDirectories)
                    .Concat(directoriesWithoutFileNames)
                    .ToArray();
                var baseRunningDirectories = runningDirectories.Select(Path.GetDirectoryName);
                var directories = runningDirectories
                    .Concat(baseRunningDirectories)
                    .ToArray();
                var settingsPaths = directories
                    .Select(directory =>
                    {
                        if (!Directory.Exists(directory)) return null;
                        var filePath = Path.Combine(directory, SettingsFileName);
                        return !File.Exists(filePath) ? null : filePath;

                    })
                    .Where(x => x != null)
                    .ToArray();
                var path = settingsPaths
                    .Select(x => x.Trim())
                    .FirstOrDefault();
                if (null == path)
                {
                    var paths = string.Join(Environment.NewLine, directories.ToArray());
                    throw new FileNotFoundException("Could not find a config file after seeking the following paths: " + paths);
                }
                return path;
            }
        }

        public string GraphiteBaseUrl { get; set; }
        public void Foo() { }
    }
}
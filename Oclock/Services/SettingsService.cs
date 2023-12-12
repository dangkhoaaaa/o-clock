
using Oclock.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Oclock.Services
{
	public class SettingsService
	{
		private string FilePath;

		public SettingsService()
		{
			string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
			FilePath = Path.Combine(appDirectory, "WorldClockApp.settings");
		}

		public AppSettings LoadSettings()
		{
			if (!File.Exists(FilePath))
			{
				var defaultSettings = new AppSettings
				{
					IsDisplay = true,
					IsDarkTheme = false,
					ListWorldCurrent = new List<string>
			{
					"Tokyo Standard Time",
					"Central Standard Time",
					"SE Asia Standard Time"
			}
				};

				var defaultContent = JsonSerializer.Serialize(defaultSettings, new JsonSerializerOptions
				{
					WriteIndented = true
				});

				File.WriteAllText(FilePath, defaultContent);
				//Console.WriteLine("Created default settings: " + defaultContent);

				return defaultSettings;
			}

			var content = File.ReadAllText(FilePath);
		//	Console.WriteLine("has load" + content);
			return JsonSerializer.Deserialize<AppSettings>(content);
		}

		public void SaveSettings(AppSettings settings)
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};

			var content = JsonSerializer.Serialize(settings, options);
			File.WriteAllText(FilePath, content);
		//	Console.WriteLine("has save" + content);
		}
	}
}

using System;
using System.Configuration;
using emulator_launcher.Models;

namespace emulator_launcher.Services
{
	public class SettingsService
	{

		private LoggerService loggerService = new LoggerService();

		public SettingsService ()
		{
		}


		public Settings GetSettings() {
			Settings settings = new Settings();

			string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly ().Location);
			PlatformID CurrentPlatform = Environment.OSVersion.Platform;
			if (CurrentPlatform == PlatformID.MacOSX || CurrentPlatform == PlatformID.Unix) {
				path = path + "/";
			} else {
				path = path + "\\";
			}

			settings.DefaultEmulator = ConfigurationManager.AppSettings["DefaultEmulator"];
			settings.DefaultEmulatorArguments = ConfigurationManager.AppSettings ["DefaultEmulatorArguments"];

			settings.DefaultItemFolder = ConfigurationManager.AppSettings ["DefaultItemFolder"];

			settings.CurrentPlatform = CurrentPlatform;
			settings.ExecutingPath = path;

			PrintSettings (settings);

			return settings;
		}

		private void PrintSettings(Settings settings) {
			string settingsLog = string.Empty;
//			settingsLog += "AlwaysRedirectClipboard : " + settings.AlwaysRedirectClipboard + Environment.NewLine;
//			settingsLog += "AlwaysUseAero : " + settings.AlwaysUseAero + Environment.NewLine;
//			settingsLog += "AlwaysUseDefaultResolution : " + settings.AlwaysUseDefaultResolution + Environment.NewLine;
//			settingsLog += "AlwaysUseFonts : " + settings.AlwaysUseFonts + Environment.NewLine;
//			settingsLog += "AlwaysUseMenuAnims : " + settings.AlwaysUseMenuAnims + Environment.NewLine;
//			settingsLog += "AlwaysUseRFX : " + settings.AlwaysUseRFX + Environment.NewLine;
//			settingsLog += "AlwaysUseWindowDrag : " + settings.AlwaysUseWindowDrag + Environment.NewLine;
			settingsLog += "CurrentPlatform : " + settings.CurrentPlatform.ToString() + Environment.NewLine;
			settingsLog += "ExecutingPath : " + settings.ExecutingPath + Environment.NewLine;
//			settingsLog += "ResolutionHeight : " + settings.ResolutionHeight + Environment.NewLine;
//			settingsLog += "ResolutionWidth : " + settings.ResolutionWidth + Environment.NewLine;

			loggerService.Log (settingsLog, false);
		}

	}
}


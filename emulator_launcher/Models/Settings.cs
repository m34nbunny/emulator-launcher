using System;

namespace emulator_launcher.Models
{
	public class Settings
	{
		public Settings ()
		{
		}

		public string ExecutingPath { get;set; }
		public PlatformID CurrentPlatform { get;set; }

		public string DefaultEmulator { get; set; }
		public string DefaultEmulatorArguments { get; set; }
		public string DefaultItemFolder { get; set; }


	}
}


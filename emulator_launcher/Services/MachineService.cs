using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using emulator_launcher.Models;

namespace emulator_launcher.Services
{
	public class MachineService
	{
		private LoggerService loggerService = new LoggerService ();
		private Settings settings = new Settings();
		
		public MachineService ()
		{
			settings = new SettingsService ().GetSettings ();
		}

		public void LaunchRDP(Machine machine) {
			try {
			Process launcher = new Process();
			launcher.StartInfo.FileName = settings.DefaultEmulator;
			launcher.StartInfo.Arguments = settings.DefaultEmulatorArguments
				.Replace ("{avdname}", machine.AVDName);
			launcher.StartInfo.RedirectStandardError = true;
			launcher.StartInfo.RedirectStandardOutput = true;
			launcher.StartInfo.RedirectStandardInput = true;
			launcher.StartInfo.UseShellExecute = false;
			loggerService.Log (launcher.StartInfo.FileName + " " + launcher.StartInfo.Arguments);
			launcher.Start();
			} catch (Exception ex) {
				loggerService.Log (ex.Message);
			}
		}


		public List<Machine> GetAllMachines() {
			List<Machine> machines = new List<Machine> ();

			string[] machineFiles = Directory.GetFiles (settings.ExecutingPath + settings.DefaultItemFolder);
			foreach (string machineFile in machineFiles) {
				string machineFilePath = System.IO.Path.GetFileName (machineFile);
				Machine machine = new Machine ();
				string[] lines = File.ReadAllLines (machineFile);
				foreach (string line in lines) {
					string[] split = line.Split ('=');
					if (split.Length == 2) {
						string param = split [0];
						string value = split [1];


						if (param == "avdname")
							machine.AVDName = value;
						if (param == "parameters")
							machine.Parameters = value;
						if (param == "group")
							machine.GroupName = value;
						machine.FileName = machineFilePath;
					}
				}
				machines.Add (machine);
			}
			return machines;
		}

	}
}


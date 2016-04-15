using Gtk;
using Gdk;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

using emulator_launcher.Models;
using emulator_launcher.Services;

namespace emulator_launcher
{
	class MainClass
	{

		private static StatusIcon trayIcon;
		private static emulator_launcher.Models.Settings settings;
		private static MachineService machineService = new MachineService();
		private static SettingsService settingsService = new SettingsService();
		private static PlatformService platformService = new PlatformService();
		private static LoggerService loggerService = new LoggerService();

		public static void Main (string[] args)
		{
			try {
				settings = settingsService.GetSettings ();

				if (platformService.IsWindows) {
					platformService.HideConsoleWindow ();
				}


				Application.Init ();

				string iconPath = settings.ExecutingPath + "white_android.png";

				trayIcon = new StatusIcon (new Pixbuf (iconPath));
				trayIcon.PopupMenu += OnTrayIconPopup;
				trayIcon.Visible = true;
				trayIcon.Tooltip = "Remote Access";


				if (Directory.Exists (settings.ExecutingPath + "machines") == false) {
					Directory.CreateDirectory (settings.ExecutingPath +"machines");
				}

				Application.Run ();
			} catch (Exception ex) {
				loggerService.Log (ex.Message);
			}
		}


		// Create the popup menu, on right click.
		static void OnTrayIconPopup (object o, EventArgs args) {
			Menu popupMenu = new Menu();
			ImageMenuItem menuItemQuit = new ImageMenuItem ("Quit");
			Gtk.Image appimg = new Gtk.Image(Stock.Quit, IconSize.Menu);
			menuItemQuit.Image = appimg;

			var machines = machineService.GetAllMachines ()
				.GroupBy (x => x.GroupName);
			foreach (var item in machines) {
				if (item.Key == null)
					continue;

				MenuItem parentItem = new MenuItem (item.Key);
				Menu subMenu = new Menu ();
				foreach (Machine machine in item) {
					MenuItem menuItem = new MenuItem (machine.FileName);
					menuItem.Activated += (object sender, EventArgs e) => {
						machineService.LaunchRDP(machine);
					};
					subMenu.Add (menuItem);
				}
				parentItem.Submenu = subMenu;
				popupMenu.Add (parentItem);
			}


			popupMenu.Add(menuItemQuit);

			menuItemQuit.Activated += delegate { Application.Quit(); };
			popupMenu.ShowAll();
			popupMenu.Popup();
		}


	}
}
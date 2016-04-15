using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using emulator_launcher.Models;

namespace emulator_launcher.Services
{
	public class PlatformService
	{

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		private Settings settings = new Settings();

		public PlatformService ()
		{
			settings = new SettingsService ().GetSettings ();
		}

		public bool IsWindows { 
			get {
				if (settings.CurrentPlatform == PlatformID.Win32NT ||
				    settings.CurrentPlatform == PlatformID.Win32S ||
				    settings.CurrentPlatform == PlatformID.Win32Windows ||
				    settings.CurrentPlatform == PlatformID.WinCE) {
					return true;
				} else {
					return false;
				}
			}
		}

		public bool IsLinux {
			get {
				if (settings.CurrentPlatform == PlatformID.Unix) {
					return true;
				} else {
					return false;
				}
			}
		}

		public bool IsMac {
			get {
				if (settings.CurrentPlatform == PlatformID.MacOSX) {
					return true;
				} else {
					return false;
				}
			}
		}

		public void HideConsoleWindow() {
			if (IsWindows) {
				IntPtr h = Process.GetCurrentProcess ().MainWindowHandle;
				ShowWindow (h, 0);
			}
		}
	}
}


using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Utilities
{
	public static class StartupRegistry
	{
		public static bool IsRegistered
		{
			get
			{
				var runKey = Registry.CurrentUser.CreateSubKey ( "Software" ).CreateSubKey ( "Microsoft" )
					.CreateSubKey ( "Windows" ).CreateSubKey ( "CurrentVersion" ).CreateSubKey ( "Run" );
				return runKey.GetValue ( "URLFinder" ) != null;
			}
		}

		public static void Register ( bool withStartupUtility )
		{
			var runKey = Registry.CurrentUser.CreateSubKey ( "Software" ).CreateSubKey ( "Microsoft" )
				.CreateSubKey ( "Windows" ).CreateSubKey ( "CurrentVersion" ).CreateSubKey ( "Run" );
			runKey.SetValue ( "URLFinder", $"\"{Process.GetCurrentProcess ().MainModule.FileName}\" {( withStartupUtility ? "--startuputil" : "" )}" );
		}

		public static void Unregister ()
		{
			var runKey = Registry.CurrentUser.CreateSubKey ( "Software" ).CreateSubKey ( "Microsoft" )
				.CreateSubKey ( "Windows" ).CreateSubKey ( "CurrentVersion" ).CreateSubKey ( "Run" );
			runKey.DeleteValue ( "URLFinder" );
		}
	}
}

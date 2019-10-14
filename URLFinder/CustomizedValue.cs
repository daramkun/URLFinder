using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URLFinder
{
	public static class CustomizedValue
	{
		public static string WorkerName { get; set; } = "진재연";
		public static string WorkingDirectory { get; set; } = @"D:\모니터링 일지";
		public static string TemplateDirectory { get; set; } = @"D:\모니터링 일지\일지 양식";

		public static string MondayKeyword { get; set; } = "";
		public static string TuesdayKeyword { get; set; } = "";
		public static string WednesdayKeyword { get; set; } = "";
		public static string ThursdayKeyword { get; set; } = "";
		public static string FridayKeyword { get; set; } = "";
		public static string AdditiveKeyword { get; set; } = "";

		private static readonly string SaveFilePath = "customized_value.txt";

		static CustomizedValue ()
		{
			string path = Path.Combine ( Program.ProgramPath, SaveFilePath );
			if ( File.Exists ( path ) )
			{
				string [] lines = File.ReadAllLines ( path );
				WorkerName = lines [ 0 ];
				WorkingDirectory = lines [ 1 ];
				TemplateDirectory = lines [ 2 ];
				if ( lines.Length > 3 )
				{
					MondayKeyword = Encoding.UTF8.GetString ( Convert.FromBase64String ( lines [ 3 ] ) );
					TuesdayKeyword = Encoding.UTF8.GetString ( Convert.FromBase64String ( lines [ 4 ] ) );
					WednesdayKeyword = Encoding.UTF8.GetString ( Convert.FromBase64String ( lines [ 5 ] ) );
					ThursdayKeyword = Encoding.UTF8.GetString ( Convert.FromBase64String ( lines [ 6 ] ) );
					FridayKeyword = Encoding.UTF8.GetString ( Convert.FromBase64String ( lines [ 7 ] ) );
					AdditiveKeyword = Encoding.UTF8.GetString ( Convert.FromBase64String ( lines [ 8 ] ) );
				}
			}
			else
			{
				if ( new NameEditWindow ().ShowDialog () == System.Windows.Forms.DialogResult.Cancel )
					Application.Exit ();
				Save ();
			}
		}

		public static void Save ()
		{
			string path = Path.Combine ( Program.ProgramPath, SaveFilePath );
			File.WriteAllLines ( path, new []
			{
				WorkerName, WorkingDirectory, TemplateDirectory,
				Convert.ToBase64String ( Encoding.UTF8.GetBytes ( MondayKeyword ) ),
				Convert.ToBase64String ( Encoding.UTF8.GetBytes ( TuesdayKeyword ) ),
				Convert.ToBase64String ( Encoding.UTF8.GetBytes ( WednesdayKeyword ) ),
				Convert.ToBase64String ( Encoding.UTF8.GetBytes ( ThursdayKeyword ) ),
				Convert.ToBase64String ( Encoding.UTF8.GetBytes ( FridayKeyword ) ),
				Convert.ToBase64String ( Encoding.UTF8.GetBytes ( AdditiveKeyword ) ),
			} );
		}
	}
}
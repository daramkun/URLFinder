using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder
{
	public static class CustomizedValue
	{
		public static string WorkerName { get; set; } = "진재연";
		public static string WorkingDirectory { get; set; } = @"D:\모니터링 일지";
		public static string TemplateDirectory { get; set; } = @"D:\모니터링 일지\일지 양식";

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
			}
		}

		public static void Save ()
		{
			string path = Path.Combine ( Program.ProgramPath, SaveFilePath );
			File.WriteAllLines ( path, new [] { WorkerName, WorkingDirectory, TemplateDirectory } );
		}
	}
}
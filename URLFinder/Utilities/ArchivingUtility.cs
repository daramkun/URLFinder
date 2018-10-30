using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Utilities
{
	public static class ArchivingUtility
	{
		public static void ArchivePdfs ( string target, params string [] pdfPaths )
		{
			using ( Stream fs = new FileStream ( target, FileMode.Create ) )
			{
				using ( ZipArchive archive = new ZipArchive ( fs, ZipArchiveMode.Create ) )
				{
					foreach ( var pdfPath in pdfPaths )
					{
						var entry = archive.CreateEntry ( Path.GetFileName ( pdfPath ), CompressionLevel.Optimal );
						using ( Stream entryStream = entry.Open () )
						{
							using ( Stream pdfStream = new FileStream ( pdfPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) )
							{
								pdfStream.CopyTo ( entryStream );
							}
						}
					}
				}
			}
		}

		public static void ArchiveDirectory ( string target, string dir )
		{
			using ( Stream fs = new FileStream ( target, FileMode.Create ) )
			{
				using ( ZipArchive archive = new ZipArchive ( fs, ZipArchiveMode.Create ) )
				{
					foreach ( var filePath in Directory.GetFiles ( dir ) )
					{
						if ( Path.GetFileName ( filePath ) [ 0 ] == '~' )
							continue;

						var entry = archive.CreateEntry ( Path.GetFileName ( filePath ), CompressionLevel.Optimal );
						using ( Stream entryStream = entry.Open () )
						{
							using ( Stream pdfStream = new FileStream ( filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) )
							{
								pdfStream.CopyTo ( entryStream );
							}
						}
					}
				}
			}
		}
	}
}

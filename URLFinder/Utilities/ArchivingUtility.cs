﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace URLFinder.Utilities
{
	public static class ArchivingUtility
	{
		public static void ArchivePdfs ( string target, Action<string> preprocess, Action<string> proceed, CancellationToken token, params string [] pdfPaths )
		{
			Directory.CreateDirectory ( Path.Combine ( Program.ProgramPath, "PdfTemp" ) );
			using ( Stream fs = new FileStream ( target, FileMode.Create ) )
			{
				using ( ZipArchive archive = new ZipArchive ( fs, ZipArchiveMode.Create ) )
				{
					foreach ( var pdfPath in pdfPaths )
					{
						string path = pdfPath;

						if ( token.IsCancellationRequested )
							break;

						if ( path.Contains ( '%' ) )
							File.Move ( path, path = path.Replace ( "%", "" ) );

						preprocess?.Invoke ( path );

						var entry = archive.CreateEntry ( Path.GetFileName ( path ), CompressionLevel.Optimal );
						using ( Stream entryStream = entry.Open () )
						{
							string newPdfPath = Path.Combine ( Program.ProgramPath, "PdfTemp", Path.GetFileNameWithoutExtension ( path ) + "compression.pdf" );
							Process.Start ( new ProcessStartInfo ( "GhostScript/gswin32c.exe", $"-sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/printer -dNOPAUSE -dQUIET -dBATCH -sOutputFile=\"{newPdfPath}\" \"{path}\"" )
							{
								UseShellExecute = false,
								CreateNoWindow = true,
							} ).WaitForExit ();

							using ( Stream pdfStream = new FileStream ( newPdfPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) )
							{
								pdfStream.CopyTo ( entryStream );
							}

							File.Delete ( newPdfPath );
						}

						proceed?.Invoke ( path );
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
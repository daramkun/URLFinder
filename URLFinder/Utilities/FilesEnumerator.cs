using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Utilities
{
	public static class FilesEnumerator
	{
		public static IEnumerable<string> EnumerateFiles ( string path, string pattern, bool topDirectoryOnly = true )
		{
			bool allFinding = false;
			IntPtr hFind = FindFirstFile ( Path.Combine ( path, pattern ), out WIN32_FIND_DATA findData );
			if ( hFind == new IntPtr ( -1 ) )
			{
				hFind = FindFirstFile ( Path.Combine ( path, "*" ), out findData );
				if ( hFind == new IntPtr ( -1 ) )
					yield break;
				allFinding = true;
			}

			do
			{
				if ( findData.dwFileAttributes.HasFlag ( FileAttributes.Directory ) )
				{
					if ( findData.cFileName == "." || findData.cFileName == ".." )
						continue;
					if ( topDirectoryOnly )
						continue;
					foreach ( var innerfile in EnumerateFiles ( Path.Combine ( path, findData.cFileName ), pattern, false ) )
						yield return innerfile;
				}
				string ret = Path.Combine ( path, findData.cFileName );
				if ( ret != path )
				{
					if ( ( allFinding && PathMatchSpec ( ret, pattern ) ) || !allFinding )
						yield return ret;
				}
			} while ( FindNextFile ( hFind, out findData ) );

			FindClose ( hFind );
		}

		[StructLayout ( LayoutKind.Sequential, CharSet = CharSet.Unicode )]
		struct WIN32_FIND_DATA
		{
			public FileAttributes dwFileAttributes;
			public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
			public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
			public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
			public uint nFileSizeHigh;
			public uint nFileSizeLow;
			public uint dwReserved0;
			public uint dwReserved1;
			[MarshalAs ( UnmanagedType.ByValTStr, SizeConst = 260 )]
			public string cFileName;
			[MarshalAs ( UnmanagedType.ByValTStr, SizeConst = 14 )]
			public string cAlternateFileName;
		}

		[DllImport ( "Kernel32", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode )]
		static extern IntPtr FindFirstFile ( string lpFileName, out WIN32_FIND_DATA lpFindFileData );
		[DllImport ( "Kernel32", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode )]
		static extern bool FindNextFile ( IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData );
		[DllImport ( "Kernel32", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode )]
		static extern bool FindClose ( IntPtr hFindFile );
		[DllImport ( "Shlwapi", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode )]
		static extern bool PathMatchSpec ( string pszFile, string pszSpec );
	}
}

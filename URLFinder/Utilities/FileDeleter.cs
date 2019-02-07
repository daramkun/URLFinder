using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace URLFinder.Utilities
{
	public static class FileDeleter
	{
		public static void Delete ( params string [] filenames )
		{
			IFileOperation fileOperation = Activator.CreateInstance ( Type.GetTypeFromCLSID ( new Guid ( "3ad05575-8857-4850-9277-11b85bdb8e09" ) ) ) as IFileOperation;
			fileOperation?.SetOperationFlags ( OperationFlag.AllowUndo | OperationFlag.NoUI | OperationFlag.FilesOnly );

			foreach ( var filename in filenames )
			{
				IShellItem fileItem = CreateItem ( filename );
				long result = fileOperation.DeleteItem ( fileItem, IntPtr.Zero );
				Marshal.ReleaseComObject ( fileItem );
			}

			fileOperation.PerformOperations ();
			Marshal.ReleaseComObject ( fileOperation );
		}

		private static IShellItem CreateItem ( string path )
		{
			SHCreateItemFromParsingName ( path, IntPtr.Zero,
				new Guid ( "43826D1E-E718-42EE-BC55-A1E261C37BFE" ),
				out IShellItem item );
			return item;
		}

		[Flags]
		enum OperationFlag : uint
		{
			None = 0x0000,
			MultiDestinationFiles = 0x0001,
			ConfirmMouse = 0x0002,
			Silent = 0x0004,
			RenameOnCollision = 0x0008,
			NoConfirmation = 0x0010,
			WantMappingHandle = 0x0020,
			AllowUndo = 0x0040,
			FilesOnly = 0x0080,
			SimpleProgress = 0x0100,
			NoConfirmMakeDirectory = 0x0200,
			NoErrorUI = 0x0400,
			NoCopySecurityAttributes = 0x0800,
			NoRecursion = 0x1000,
			NoConnectedElements = 0x2000,
			WantNukeWarning = 0x4000,
			NoRecurseReparse = 0x8000,
			NoUI = ( Silent | NoConfirmation | NoErrorUI | NoConfirmMakeDirectory ),
		}

		enum ShellItemGetDisplayName : uint
		{
			NormalDisplay = 0x00000000,
			ParentRelativeParsing = 0x80018001,
			DesktopAbsoluteParsing = 0x80028000,
			ParentRelativeEditing = 0x80031001,
			DesktopAbsoluteEditing = 0x8004c000,
			FileSysPath = 0x80058000,
			URL = 0x80068000,
			ParentRelativeForAddressBar = 0x8007c001,
			ParentRelative = 0x80080001
		}

		[ComImport]
		[Guid ( "43826D1E-E718-42EE-BC55-A1E261C37BFE" )]
		[InterfaceType ( ComInterfaceType.InterfaceIsIUnknown )]
		interface IShellItem
		{
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void BindToHandler ( [In, MarshalAs ( UnmanagedType.Interface )] IntPtr pbc, [In] ref Guid bhid, [In] ref Guid riid, out IntPtr ppv );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void GetParent ( [MarshalAs ( UnmanagedType.Interface )] out IShellItem ppsi );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void GetDisplayName ( [In] ShellItemGetDisplayName sigdnName, [MarshalAs ( UnmanagedType.LPWStr )] out string ppszName );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void GetAttributes ( [In] uint sfgaoMask, out uint psfgaoAttribs );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void Compare ( [In, MarshalAs ( UnmanagedType.Interface )] IShellItem psi, [In] uint hint, out int piOrder );
		}

		[StructLayout ( LayoutKind.Sequential, Pack = 4 )]
		struct PropertyKey
		{
			public Guid FormatId;
			public uint PropertyId;
		}

		enum ShellItemAttributeFlags
		{
			And = 0x00000001,
			Or = 0x00000002,
			AppCompat = 0x00000003,
		}

		[ComImport]
		[Guid ( "B63EA76D-1F85-456F-A19C-48159EFA858B" )]
		[InterfaceType ( ComInterfaceType.InterfaceIsIUnknown )]
		interface IShellItemArray
		{
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void BindToHandler ( [In, MarshalAs ( UnmanagedType.Interface )] IntPtr pbc, [In] ref Guid rbhid, [In] ref Guid riid, out IntPtr ppvOut );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void GetPropertyStore ( [In] int Flags, [In] ref Guid riid, out IntPtr ppv );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void GetPropertyDescriptionList ( [In] ref PropertyKey keyType, [In] ref Guid riid, out IntPtr ppv );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void GetAttributes ( [In] ShellItemAttributeFlags dwAttribFlags, [In] uint sfgaoMask, out uint psfgaoAttribs );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void GetCount ( out uint pdwNumItems );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void GetItemAt ( [In] uint dwIndex, [MarshalAs ( UnmanagedType.Interface )] out IShellItem ppsi );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void EnumItems ( [MarshalAs ( UnmanagedType.Interface )] out IntPtr ppenumShellItems );
		}

		[DllImport ( "shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false )]
		static extern void SHCreateItemFromParsingName (
		[In, MarshalAs ( UnmanagedType.LPWStr )] string pszPath,
		[In] IntPtr pbc,
		[In, MarshalAs ( UnmanagedType.LPStruct )] Guid iIdIShellItem,
		[Out, MarshalAs ( UnmanagedType.Interface, IidParameterIndex = 2 )] out IShellItem iShellItem );

		[ComImport,
			Guid ( "947aab5f-0a5c-4c13-b4d6-4bf7836fc9f8" ),
			InterfaceType ( ComInterfaceType.InterfaceIsIUnknown )]
		interface IFileOperation
		{
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void Advise ( IntPtr pfops, IntPtr pdwCookie );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void Unadvise ( uint dwCookie );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void SetOperationFlags ( OperationFlag dwOperationFlags );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void SetProgressMessage ( [MarshalAs ( UnmanagedType.LPWStr )]string pszMessage );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void SetProgressDialog ( IntPtr popd );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void SetProperties ( IntPtr pproparray );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void SetOwnerWindow ( IntPtr hwndOwner );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void ApplyPropertiesToItem ( IShellItem psiItem );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void ApplyPropertiesToItems ( IntPtr punkItems );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			long RenameItem ( IShellItem psiItem, [MarshalAs ( UnmanagedType.LPWStr )] string pszNewName, IntPtr pfopsItem );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			long RenameItems ( IntPtr pUnkItems, [MarshalAs ( UnmanagedType.LPWStr )] string pszNewName );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			long MoveItem ( IShellItem psiItem, IShellItem psiDestinationFolder,
				 [MarshalAs ( UnmanagedType.LPWStr )]string pszNewName, IntPtr pfopsItem );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			long MoveItems ( IntPtr punkItems, IShellItem psiDestinationFolder );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			long CopyItem ( IShellItem psiItem, IShellItem psiDestinationFolder,
				 [MarshalAs ( UnmanagedType.LPWStr )]string pszCopyName, IntPtr pfopsItem );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			long CopyItems ( IntPtr punkItems, IShellItem psiDestinationFolder );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			long DeleteItem ( IShellItem psiItem, IntPtr pfopsItem );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void DeleteItems ( IntPtr punkItems );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void NewItem ( IShellItem psiDestinationFolder, uint dwFileAttributes, [MarshalAs ( UnmanagedType.LPWStr )] string pszName,
				 [MarshalAs ( UnmanagedType.LPWStr )] string pszTemplateName, IntPtr pfopsItem );
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void PerformOperations ();
			[MethodImpl ( MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime )]
			void GetAnyOperationsAborted ( [MarshalAs ( UnmanagedType.Bool )] ref bool pfAnyOperationsAborted );
		}
	}
}
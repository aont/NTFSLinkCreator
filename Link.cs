using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace Aont
{
    static class Link
    {
        /// <summary>
        /// Establishes a hard link between an existing file and a new file. This function is only supported on the NTFS file system, and only for files, not directories.
        /// </summary>
        /// <param name="FileName">The name of the new file. This parameter cannot specify the name of a directory. </param>
        /// <param name="ExistingFileName">The name of the existing file. This parameter cannot specify the name of a directory.</param>
        /// <param name="lpSecurityAttributes">Reserved; must be NULL.</param>
        /// <returns>If the function succeeds, the return value is nonzero.bIf the function fails, the return value is zero (0). To get extended error information, call GetLastError.</returns>
        [DllImport("kernel32.dll")]
        private static extern bool CreateHardLink(string FileName, string ExistingFileName, object lpSecurityAttributes);

        /// <summary>
        /// Creates a symbolic link.
        /// </summary>
        /// <param name="SymlinkFileName">The symbolic link to be created.</param>
        /// <param name="TargetFileName">The name of the target for the symbolic link to be created.</param>
        /// <param name="Flags">
        /// Indicates whether the link target, lpTargetFileName, is a directory.
        ///  0:File,1:Directory
        /// </param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool CreateSymbolicLink(string SymlinkFileName, string TargetFileName, int Flags);

        public static void CreateFileLink(string LinkFileName, string TargetFileName, bool hard)
        {
            if (hard)
            {
                if (!CreateHardLink(LinkFileName, TargetFileName, null))
                    throw new Exception("CreateHardLink Failed!");
            }
            else
            {
                if (!CreateSymbolicLink(LinkFileName, TargetFileName, 0))
                    throw new Exception("CreateSymbolicLink Failed!");
            }
        }

        public static void CreateDirectoryLink(string LinkDirectory, string TargetDirectory, bool hard)
        {
            Directory.CreateDirectory(LinkDirectory);

            foreach (string targetfile in Directory.GetFiles(TargetDirectory))
            {
                string linkfile = Path.Combine(LinkDirectory, Path.GetFileName(targetfile));
                CreateFileLink(linkfile, targetfile, hard);
            }
            foreach (string targetdirectory in Directory.GetDirectories(TargetDirectory))
            {
                string linkdirectory = Path.Combine(LinkDirectory, Path.GetFileName(targetdirectory));
                CreateDirectoryLink(linkdirectory, targetdirectory, hard);
            }
        }

    }
}

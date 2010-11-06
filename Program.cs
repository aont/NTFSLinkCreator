using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Aont
{
    public class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length != 0)
            {
                bool Hard = false;
                {
                    DialogResult result = MessageBox.Show("Create HardLink?", "Question", MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Cancel)
                        return;
                    else if (result == DialogResult.Yes)
                        Hard = true;
                }

                if (args.Length == 1)
                {
                    string TargetFile = args[0];
                    SaveFileDialog MySaveFileDialog = new SaveFileDialog()
                    {
                        FileName = Path.GetFileName(args[0])
                    };
                    if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string LinkFile = MySaveFileDialog.FileName;
                        if (Directory.Exists(TargetFile))
                            Link.CreateDirectoryLink(LinkFile, TargetFile, Hard);
                        else
                            Link.CreateFileLink(LinkFile, TargetFile, Hard);
                    }

                }
                else if (args.Length > 1)
                {
                }
            }
        }
    }
}

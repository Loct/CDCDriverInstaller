using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;
using System.Diagnostics;
using System.IO;

public enum OemSourceMediaType
{
    SPOST_NONE = 0,
    //Only use the following if you have a pnf file as well
}

public enum OemCopyStyle
{
    SP_COPY_NEWER = 0x0000004,   // copy only if source newer than or same as target
    SP_COPY_NEWER_ONLY = 0x0010000,   // copy only if source file newer than target
    SP_COPY_OEMINF_CATALOG_ONLY = 0x0040000,   // (SetupCopyOEMInf only) don't copy INF--just catalog
}

namespace CDCDriverInstaller
{


    public partial class MainForm : Form
    {

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupCopyOEMInf(

            string SourceInfFileName,
            string OEMSourceMediaLocation,
            OemSourceMediaType OEMSourceMediaType,
            OemCopyStyle CopyStyle,
            string DestinationInfFileName,
            int DestinationInfFileNameSize,
            ref int RequiredSize,
            string DestinationInfFileNameComponent

        );


        public MainForm()
        {
            MessageBox.Show(this, "The Driver for your device was not found. \nInstalling missing driver. \nIf any devices are present, please remove the devices.", "Driver Warning", MessageBoxButtons.OK);
            string filepath = System.Environment.CurrentDirectory + "\\driver";
            DirectoryInfo d = new DirectoryInfo(filepath);
            Boolean successfull = true;
            foreach (var file in d.GetFiles("*.inf"))
            {
                successfull = installDrivers(filepath + "\\" + file.ToString());
                if (!successfull)
                {
                    Environment.Exit(0);
                }
            }

            if (successfull)
            {
                MessageBox.Show(this, "Drivers Installed successfully. \nYou can now insert your device.", "Success", MessageBoxButtons.OK);
                Environment.Exit(0);
            }
        }

        private Boolean installDrivers(string file)
        {
            int size = 0;
            bool success = SetupCopyOEMInf(file, "", OemSourceMediaType.SPOST_NONE, OemCopyStyle.SP_COPY_NEWER, null, 0,
                            ref size, null);
            if (!success)
            {
                var errorCode = Marshal.GetLastWin32Error();
                var errorString = new Win32Exception(errorCode).Message;
                MessageBox.Show(this, file + "Driver failed to install. Please try again. \nIf this problem occurs again, please contact 2Mel with the following error:\n" + errorString, "Error", MessageBoxButtons.OK);
            }
            return success;
        }
    }

}

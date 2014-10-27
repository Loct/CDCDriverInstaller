using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Diagnostics;

class driverChecker
{
    private Boolean driverFound = false;

    public driverChecker(string driverName)
    {
        List<string> DriverList = new List<string>();
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPSignedDriver");
        foreach (ManagementObject obj in searcher.Get())
        {
            try
            {
                if (driverName.Equals(obj.GetPropertyValue("DeviceName").ToString()))
                {
                    driverFound = true;
                    break;
                }

            }
            catch
            {

            }
        }
    }

    public Boolean getDriverFound()
    {
        return driverFound;
    }

    public int runDriverInstaller(string fileName)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        // Enter in the command line arguments, everything you would enter after the executable name itself
        // Enter the executable to run, including the complete path
        start.FileName = fileName;
        // Do you want to show a console window?
        start.WindowStyle = ProcessWindowStyle.Normal;
        start.CreateNoWindow = false;

        // Run the external process & wait for it to finish
        using (Process proc = Process.Start(start))
        {
            proc.WaitForExit();

            // Retrieve the app's exit code
            return proc.ExitCode;
        }
    }
}
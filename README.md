CDCDriverInstaller
==================

Driver Installer for the Atmel Xplained CDC driver

## Prerequisits ##
`Visual Studio 2010`
`.Net 3.5`

The project should be able to run in `Visual Studio 2013`.

## Functions ##
For convenience one can add the DriverChecker.cs class in the project.

Afterwards one can call the checker as follows:
* `DriverChecker checker = new DriverChecker("driver name)`
The driver name can be found in the Device Manager.

* `getDriverFound()`  is used to check whether a driver is installed

* `runDriverInstaller(path)` The DriverChecker project contains a program to install the Atmel CDC driver, this function can be run to open the external program

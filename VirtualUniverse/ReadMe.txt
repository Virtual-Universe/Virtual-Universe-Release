Virtual-Universe 1.0.2+ (git)
Rowan Deppeler <greythane@gmail.com>
June 2014
Fly-Man-
Jan 2015
Emperor Starfinder <emperor@secondgalaxy.com>
May 11, 2016
===============================================

** Simplified startup scripts **
================================
Easy to use startup scripts are provided to simplify running Virtual Universe.
Choose the appropriate one(s) for your system and configuration.

 *nix flavours (including Mac)
-------------------------------
All these scripts are intended to be executed from a terminal window.

The following are the general startup scripts for use in testing, maintenance etc...
sim_console.sh       			: Start only the Virtual Universe standalone server
grid_console.sh      			: Start only the Virtual Universe grid server

Note:
 These 'background' modes require the 'screen' program installed on your system.
   Linux (ubuntu variations) >> sudo apt-get install screen;
   Mac >> already available
  
run_standalonemode.sh       	: Run Virtual Universe standalone mode in background
run_gridmode.sh             	: Run Virtual Universe grid mode in background

GUI scripts for Windows and Mac

-------------------------------

The same commands are available as gui scripts, <double_click> to execute

sim_console.command				: Mac
sim_console.bat					: Windows
grid_console.command			: Mac
grid_console.bat				: Windows

** The default configuration **
===============================
The default configuration of Virtual Universe is setup to run in Standalone mode and
to use an SQLite database, with no pre-configured users or regions.
On initial startup, you will be asked to create your initial region, together with 
your first user.

** Quick Customizing **
=======================
You can quickly set a few options to customize your Virtual Universe installation.
To do this, modify the commented out settings in 'MyWorld.ini' located in the
 'Config' folder.  This will allow you to set the name of your Sim, configure your default
  region and specify an address to use if you do not want to use your external IP.

** Grid mode **
===============
This configuration has been setup to run as a standalone simulator. If you wish to re-configure
and use the Grid mode of operation, change the selected include mode in the 'Main.ini' file.

Edit Main.ini file in
Config > Sim > Main.ini

Comment the "Include-Standalone =" line.
Uncomment the "Include-Grid =" line.

Save and re-start.
Note:  You will need to use both the 'grid' and 'sim' startup scripts.

** Updating **
==============
Checkout the 'Build Your Own.txt' file in the 'UniverseDocs' folder for details
 if you want to build from source.
Re-compile and copy/paste the new 'VirtualUniverse/bin' subdirectory from your build environment.

Monthly 'Development' build snapshots are available at http://virtual-planets.org/downloads/

Download your desired update snapshot.
Delete or backup the existing 'VirtualUniverse/bin' subdirectory.
Extract the update package and copy the resulting 'bin' folder to your 'VirtualUniverse' folder.
re-start..

Questions?
==========
Checkout the #galaxyfutures irc channel on freenode,
or check into the Google+ Second Galaxy/ Virtual Universe group at 
https://plus.google.com/communities/106118101750197366605?cfem=1

Emperor Starfinder
<emperor@secondgalaxy.com>

May 11, 2016
=======================

For licensing information, please see the relevant licenses.
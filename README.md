# Attention all users
We are aware of an issue involving the latest versions of Third Party Viewers (TPVs) that causes avatars to go black when attaching or detaching items. The Second Galaxy Development Team is aware of this bug and is currently investigating it.

Viewers affected are: Singularity Viewer (1.8.7.6861), Firestorm (4.7.9), Alchemy Viewer (4.0.1)  Kokua Viewer is not yet known to be affected.

Until we get this fixed we advise all users to use the version of their viewer just before the current release version.  In this way you will avoid this bug while we investigate this issue further.

# Virtual Universe

- Current Version: 1.0.2 RC2
- Version Release Date: April 30, 2016

The Virtual Universe Development Team is proud to present Virtual Universe as a rolling release software.

The Virtual Universe server is an Aurora-Sim/ WhiteCore-Sim derived project with heavy emphasis on supporting all users, 

## About Virtual Universe

The Virtual Universe Software is a Virtual Universe derived project with heavy emphasis on supporting all users, increased technology focus, heavy emphasis on working with other developers, whether it be viewer based developers or server based developers, and a set of features around stability and simplified usability for users.

We aren’t just releasing new features, but a new outlook on Virtual Universe Virtual World Technology Development for the average human.

Virtual Universe is an experimental and innovative framework containing advanced tools and options for creating virtual world applications.

Virtual Universe is not a virtual world, nor a stand-alone application, it is a scalable and customizable platform containing some basic modules, based on some fundamental innovative pillars (peer-to-peer architecture, secure communication infrastructure, legal framework, powerful scripting language); additional modules, extensions and expansion packs can be built on top of it on demand.

The core of Virtual Universe is the innovative Virtual Universe Engine based on a hybrid peer-to-peer infrastructure that allows the sharing of computational load in experiencing the virtual environment obtaining infrastructural resource optimization and bandwidth reduction.

It enhances the platform in terms of robustness, availability, scalability, load balancing.

and much more....

## Virtual Universe Build Status

Windows .Net 4.5 [![Build status](https://ci.appveyor.com/api/projects/status/a90lejf562n9sxwy?svg=true)](https://ci.appveyor.com/project/emperorstarfinder/virtual-universe)

Linux 64 Bit [![Build Status](https://travis-ci.org/Virtual-Universe/Virtual-Universe.svg?branch=master)](https://travis-ci.org/Virtual-Universe/Virtual-Universe)

Pull requests [![Issue Stats](http://www.issuestats.com/github/Virtual-Universe/Virtual-Universe/badge/pr)](http://www.issuestats.com/github/Virtual-Universe/Virtual-Universe)

Issues closed [![Issue Stats](http://www.issuestats.com/github/Virtual-Universe/Virtual-Universe/badge/issue)](http://www.issuestats.com/github/Virtual-Universe/Virtual-Universe)


## Configuration
*To see how to configure Virtual Universe, look at "Setting up Universe.txt" in the UniverseDocs folder for more information*

Windows:
   Run the 'runprebuild.bat' file.
   This will check you current system configuration, compile the correct Visual Studio 2010 solution and project files and prompt you to build immediately (if desired)
   NOTE: Windows users will now find that runprebuild.bat will now automatically detect the operating system architecture version i.e. x86 or x64 as well as the correct .Net Framework version i.e. 4.0 or 4.5 and build accordingly.

*nix:      (Also OSX)
   Execute the 'runprebuild.sh' form a terminal or console shell.
   You will be prompted for your desired configuration, the appropriate solution and project files for Mono will be compiled and finally, prompt you to build immediately (if desired)
   
OSX:
   Run the 'runprebuild.command' shell command by 'double clicking' in Finder.
   You will be prompted for your desired configuration, the appropriate solution and project files for Mono will be compiled and finally, prompt you to build immediately (if desired)

## Compiling Virtual Universe

*To compile Virtual Universe, look at the Compiling.txt in the Docs folder for more information*

*NOTE: For Windows 7 and 8, when compiling, you may see some warnings indicating that the core library does not match what is specified. This is an issue with how Microsoft provides the Net 4.5 packages and can be safely ignored as Windows will actually use the correct library when Virtual Universe is run *

## Virtual Universe Configuration

*To see how to configure Virtual Universe, look at "Setting up Virtual Universe.txt" in the UniverseDocs folder for more information*

## Contribute to Virtual Universe
If you would like to contribute code to Virtual Universe please see BuildTools/How to contribute Code.txt for more information.

## Router issues
If you are having issues logging into your simulator, take a look at http://forums.osgrid.org/viewtopic.php?f=14&t=2082 in the Router Configuration section for more information on ways to resolve this issue.

## Virtual Universe Support
Support is available from various sources.

IRC channel #galaxyfutures on freenode (http://webchat.freenode.net?channels=%23galaxyfutures)
Check out http://virtual-planets.org for the latest developments, downloads and forum
Second Galaxy and Virtual Universe Google + community is for both Second Galaxy and Virtual Universe with a friendly bunch that is happy to answer questions. Find it at https://plus.google.com/communities/106118101750197366605?cfem=1


## NOTES

*NOTES:

*- As of March 22, 2017, the LibOMV libraries are included as a submodule of the Virtual Universe repositories. When cloning, ensure that the submodules are included.*

`git clone --recursive https://github.com/Virtual-Universe/Virtual-LibOMV.git`

To update an existing repository that does not have the LibOMV submodule

	cd <your Virtual Universe repository>
	git submodule init
	git submodule update

*If you do not know what submodules are, or you are not using git from the command line, PLEASE make sure to fetch the submodules too.*

**If you download the repo using the zip file option, you will also need to download the Virtual-LibOMV submodule and extract it in your local Virtual Universe repo.**
`https://github.com/Virtual-Universe/Virtual-LibOMV`

*NOTE:
 As of Version 1.0.2, it's advised to Linux users to use a Mono version higher then 4.2.3.4, following a report about  GC.Collect() not cleaning up memory correctly. The most current version of Mono is 4.6.1.3 (Released 1st August 2016)*
 Attention Arch Linux Users: by default your Mono version is 4.4.1.0 and is customized to work on Arch Linux.  YOu should not experience any problems.
 More information can be found here: http://www.mono-project.com/docs/getting-started/install/linux/

*NOTE:
 We are aware of cases where the grid_console.sh and sim_console.sh are not working properly on Ubuntu 14.04.4 LTS Trusty Tahr.
 This bug claims it cannot find files or permission or access denied.  This is due to an issue with the Version 4.2 of the Linux_Generics libraries distributed by Linux.
 This is not a Virtual Universe specific bug.  The solution here is to cd to VirtualUniverse directory and run the command:

Grid: sudo ./grid_console.sh 

Sim: sudo ./sim_console.sh

 This will give the necessary permissions to override this bug and allow the consoles to work correctly.

 
 We are not aware of issues being reported relating to this bug from users of Ubuntu derivatives such as XUbuntu, KUbuntu, Linux Mint, etc.  
 
 Additionally we have not received any reports to indicate this is happening with Debian (8.0 Jessie), Fedora, Opensuse, Arch Linux, Redhat, etc.

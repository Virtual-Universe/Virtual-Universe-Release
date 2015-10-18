# Virtual Universe

*NOTE:
 As of Version 1.0.1, it's advised to Linux users to use a Mono version higher then 4.0.1, following a report about  GC.Collect() not cleaning up memory correctly. The most current version of Mono is 4.0.1 (Released 28th April 2015)*

 More information can be found here: http://www.mono-project.com/docs/getting-started/install/linux/

*NOTE: 
 We are aware of issues relating to Linux Kernal 3.16.0 and Mono 3.10.0 causing Linux systems to crash.  
 Currently there are no reported issues being caused by Kernal 3.16.0 and Mono 3.10.0 being reported with Virtual Universe.

The Virtual Universe Development Team is proud to present Virtual Universe as a rolling release software.

The Universe server is an Universe/Aurora-Sim derived project with heavy emphasis on supporting all users, 

## About Virtual Universe

The Virtual Universe Software is an Virtual Universe derived project with heavy emphasis on supporting all users, increased technology focus, heavy emphasis on working with other developers, whether it be viewer based developers or server based developers, and a set of features around stability and simplified usability for users.

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

*NOTE: For Windows 7 and 8, when compiling, you may see some warnings indicating that the core library does not match what is specified. This is an issue with how Microsoft provides the Net 4.5 packages and can be safely ignored as Windows will actually use the correct library when WhiteCore is run *

## Virtual Universe Configuration

*To see how to configure Virtual Universe, look at "Setting up Virtual Universe.txt" in the UniverseDocs folder for more information*

+## Contribute to Virtual Universe
+If you would like to contribute code to Virtual Universe please see BuildTools/How to contribute Code.txt for more information.

## Router issues
If you are having issues logging into your simulator, take a look at http://forums.osgrid.org/viewtopic.php?f=14&t=2082 in the Router Configuration section for more information on ways to resolve this issue.

## Virtual Universe Support
Support is available from various sources.

IRC channel #galaxyfutures on freenode (http://webchat.freenode.net?channels=%23galaxyfutures)
Check out http://virtual-planets.org for the latest developments, downloads and forum
Second Galaxy and Virtual Universe Google + community is for aih Second Galaxy and Virtual Universe with a friendly bunch that is happy to answer questions. Find it at https://plus.google.com/communities/106118101750197366605?cfem=1

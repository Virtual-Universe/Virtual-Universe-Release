#Virtual-Universe-Optional-Modules
==================================

This is the Optional Modules Repository for Virtual Universe

*NOTE:  As of Version 1.0.2, the Virtual Universe repository format has changed so ensure that you are using the correct format for your repo.
This should not be a problem if you use the latest commits of the Virtual-Universe or a release version >= 1.0.2*

# Installation

There are two ways to install these modules, auto-installation, or manual compilation

## Automated
1. Start Universe.exe or Universe.Server.exe
2. Type 'compile module <path to the build.am of the module that you want>' into the console and it will install the module for you and tell you how to use or configure it.

## Manual Compilation and installation:
Copy the selected directory to the addon-modules of the main source code directory.
Each module should be in it's own tree and the root of the tree should contain a file named "prebuild.xml".

*The prebuild.xml should only contain <Project> and associated child tags. 
The <xml>, <Prebuild>, <Solution> and <Configuration> tags should not be included since the add-on modules prebuild.xml will be inserted directly into the main prebuild.xml*

The module source code will be included in the main Universe solution when the "runprebuild" script is executed for your system.



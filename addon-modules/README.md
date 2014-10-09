These are optional modules that can be used with VirtualUniverse

# Installation

To install, there are three ways to install, auto-installation, or manual compilation

## Automated
1. Start Planets.exe or Galaxy.exe
2. Type 'compile module <path to the build.am of the module that you want>' into the console and it will install the module for you and tell you how to use or configure it.

## Manual Compilation and installation:
Copy the selected directory to the addon-modules of the main source code directory.
Each module should be in it's own tree and the root of the tree should contain a file named "prebuild.xml", which will be included in the main prebuild file.

The prebuild.xml should only contain <Project> and associated child tags. 
The <?xml>, <Prebuild>, <Solution> and <Configuration> tags should not be included since the add-on modules prebuild.xml will be inserted directly into the main prebuild.xml

## Integrated into Core VirtualUniverse
You can also drop the module into VirtualUniverse/Modules and then run runprebuild.bat (for Windows) or runprebuild.sh (for Mac and Linux) and then build using your favorite
method of building the sln file (Visual Studios, Xamarin Studio, xbuild, etc.) and configure the module using the ini files for the module.

# Known Issues
There are no known issues at the moment.
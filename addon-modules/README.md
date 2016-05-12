#Optional-Modules

This is the Optional Modules Repository for Virtual Universe.  
These modules add additional functionality to your Virtual Universe simulator installation.


## Installation

There are two ways to install these modules, auto-installation, or manual compilation

### Automated
1. Start Universe.exe or Universe.Server.exe
2. Type 'compile module <path to the build.am of the module that you want>' into the console and it will install the module for you and tell you how to use or configure it.

### Manual Compilation and installation:
Copy the selected directory to the addon-modules of the main source code directory.
Each module should be in it's own tree and the root of the tree should contain a file named "prebuild.xml".

*The prebuild.xml should only contain <Project> and associated child tags. 
The <?xml>, <Prebuild>, <Solution> and <Configuration> tags should not be included since the add-on modules prebuild.xml will be inserted directly into the main prebuild.xml*

The module source code will be included in the main Virtual Universe solution when the "runprebuild" script is executed for your system.
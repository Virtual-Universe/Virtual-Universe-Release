# This installer is to allow a fast setup of
# Virtual Universe on both debian and Ubuntu
# The Idea came from WhiteCore-Sim which we give many thanks
# Emperor Starfinder <emperor@secondgalaxy.com>
# July 9, 2016

# Install Mono 4.4
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list
sudo apt-get update
# Install mono-develop for bare essentials of mono
sudo apt-get install mono-devel
sudo apt-get update
# Install mono-complete for the entire set of required mono tools
sudo apt-get install mono-complete
sudo apt-get update
# Install mono reference assemblies to help suppress unnecessary errors with 
# reference assemblies during building
sudo apt-get install referenceassemblies-pcl
sudo apt-get update
#Install mono certificates
sudo apt-get install mono-certificates-ca
sudo apt-get update
# Install MySQL Server
sudo apt-get install mysql-server
sudo apt-get update
# Install Git
sudo apt-get install git-core
sudo apt-get update
# Setting up Virtual Universe
cd /
git clone https://github.com/Virtual-Universe/Virtual-Universe.git
cd Virtual-Universe/
./runprebuild.sh
xbuild
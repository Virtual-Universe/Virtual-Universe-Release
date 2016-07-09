# This installer is to allow a fast update of
# Virtual Universe on both debian and Ubuntu
# The Idea came from WhiteCore-Sim which we give many thanks
# Emperor Starfinder <emperor@secondgalaxy.com>
# July 9, 2016

# Check for updates for your Linux flavor
sudo apt-get update
# Getting updates for Virtual Universe
cd /
git pull https://github.com/Virtual-Universe/Virtual-Universe.git
cd Virtual-Universe/
./runprebuild.sh
xbuild
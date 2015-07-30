#!/bin/bash
# Start the Universe-Sim Standalone server
# Version 0.9.2+
#
# May 2014
# greythane @ gmail.com

WOASDIR="${0%/*}"
cd $WOASDIR
echo $WOASDIR

cd ./bin
echo Starting Universe Standalone Simulator...
mono Universe.exe -skipconfig
wait

exit


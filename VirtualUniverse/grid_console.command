#!/bin/bash
# Start the Virtual-Universe Grid server
# Version 0.9.2+
#
# May 2014
# greythane @ gmail.com

WOASDIR="${0%/*}"
cd $WOASDIR
echo $WOASDIR

cd ./bin
echo Starting Universe Grid server...
mono Universe.Server.exe -skipconfig
wait

exit


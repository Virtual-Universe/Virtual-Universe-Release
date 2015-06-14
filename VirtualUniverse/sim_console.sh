#!/bin/bash
# Start the Virtual-Universe server only
# Versions 0.9.2+
#
# May 2014
# greythane @ gmail.com

cd ./bin
sleep 1
echo Starting Standalone Region Simulator...
mono Universe.exe -skipconfig
wait
exit


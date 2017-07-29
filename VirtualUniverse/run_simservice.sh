#!/bin/bash
# Startup script for the Virtual Universe Region server service
# Versions 1.0.4+
#
# July 2017
# Emperor Starfinder <emperor@secondgalaxy.com>
#

cd ./bin
wait
echo Starting Virtual Universe Region Simulator...
screen -S Sim -d -m mono Universe.exe -skipconfig
sleep 3
screen -list
echo "To view the Sim server console,  use the command : screen -r Sim"
echo "To detach fron the console use the command : ctrl+a d  ..ctrl+a > command mode,  d > detach.."
echo

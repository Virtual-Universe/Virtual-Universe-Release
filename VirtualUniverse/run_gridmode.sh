#!/bin/bash
# Startup script for Universe-Sim in full Grid mode
# Versions 0.9.2+
#
# June 2014
# greythane @ gmail.com
#

cd ./bin
wait
echo Starting Universe GridServer...
screen -S Grid -d -m mono Universe.Server.exe -skipconfig
sleep 3
echo Starting Universe Region Simulator...
screen -S Sim -d -m mono Universe.exe -skipconfig
sleep 3
screen -list
echo "To view the Grid server console, use the command : screen -r Grid"
echo "To view the Sim server console,  use the command : screen -r Sim"
echo "To detach fron the console use the command : ctrl+a d  ..ctrl+a > command mode,  d > detach.."
echo

#!/bin/bash
# Startup script for Virtual Universe grid server service
# Versions 1.0.4+
#
# July 2017
# Emperor Starfinder <emperor@secondgalaxy.com>
#

cd ./bin
wait
echo Starting Virtual Universe GridServer...
screen -S Grid -d -m mono Universe.Server.exe -skipconfig
sleep 3
screen -list
echo "To view the Grid server console, use the command : screen -r Grid"
echo "To detach fron the console use the command : ctrl+a d  ..ctrl+a > command mode,  d > detach.."
echo

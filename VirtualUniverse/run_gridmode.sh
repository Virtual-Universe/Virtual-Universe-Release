#!/bin/bash
# Startup script for Virtual Universe in full Grid mode
# Versions 1.0.2+
#
# Emperor Starfinder<emperor@secondgalaxy.com>: For Virtual Universe - May 11, 2016
# Greythane<greythane@gmail.com>: For WhiteCore-Sim - May 2015

cd ./bin
wait
echo Starting Virtual Universe GridServer...
screen -S Grid -d -m mono Universe.Server.exe -skipconfig
sleep 3
echo Starting Virtual Universe Region Simulator...
screen -S Sim -d -m mono Universe.exe -skipconfig
sleep 3
screen -list
echo "To view the Grid server console, use the command : screen -r Grid"
echo "To view the Sim server console,  use the command : screen -r Sim"
echo "To detach fron the console use the command : ctrl+a d  ..ctrl+a > command mode,  d > detach.."
echo
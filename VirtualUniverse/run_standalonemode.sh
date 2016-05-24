#!/bin/bash
# startup script for Virtual Universe standalone
# Versions 1.0.2+
#
# Emperor Starfinder<emperor@secondgalaxy.com>: For Virtual Universe - May 11, 2016
# Greythane<greythane@gmail.com>: For WhiteCore-Sim - May 2015

cd ./bin
wait
echo Starting Virtual Universe Standalone Region Simulator...
screen -S Sim -d -m mono Universe.exe -skipconfig
sleep 3
screen -list
echo "To view the Sim console, use the command : screen -r Sims"
echo "To detach fron the console use the command : ctrl+a d  ...ctrl+a [command mode],  d [detach]"
echo
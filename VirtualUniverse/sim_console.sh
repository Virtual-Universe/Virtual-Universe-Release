#!/bin/bash
# Start the Virtual Universe server only
# Versions 1.0.2+
#
# Emperor Starfinder<emperor@secondgalaxy.com>: For Virtual Universe - May 11, 2016
# Greythane<greythane@gmail.com>: For WhiteCore-Sim - May 2015

cd ./bin
sleep 1
echo Starting Virtual Universe Standalone Region Simulator...
mono Universe.exe -skipconfig
wait
exit
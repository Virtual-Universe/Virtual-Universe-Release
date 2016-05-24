#!/bin/bash
# Start the Virtual Universe Grid server
# Version 1.0.2+
#
# Emperor Starfinder<emperor@secondgalaxy.com>: For Virtual Universe - May 11, 2016
# Greythane<greythane@gmail.com>: For WhiteCore-Sim - May 2015

VUOASDIR="${0%/*}"
cd $VUOASDIR
echo $VUOASDIR

cd ./bin
echo Starting Virtual Universe Grid server...
mono Universe.Server.exe -skipconfig
wait

exit
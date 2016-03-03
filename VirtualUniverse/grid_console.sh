#!/bin/bash
# Start Virtual Universe grid server only
# Versions 0.9.2+
#
# May 2014
# greythane @ gmail.com

cd ./bin
wait
echo Starting Universe Grid Server...
mono Universe.Server.exe -skipconfig


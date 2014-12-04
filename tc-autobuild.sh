#!/bin/bash

mono ./Prebuild.exe /target vs2010 /targetframework v4_0 /conditionals "LINUX;NET_4_0"

if [ -d ".git" ]; then git log --pretty=format:"VirtualUniverse (%cd.%h)" --date=short -n 1 > VirtualUniverse/bin/.version; fi

unset makebuild
unset makedist

while [ "$1" != "" ]; do
    case $1 in
	build )       makebuild=yes
                      ;;
	dist )        makedist=yes
                      ;;
    esac
    shift
done

if [ "$makebuild" = "yes" ]; then
    xbuild VirtualUniverse.sln
    res=$?

    if [ "$res" != "0" ]; then
	exit $res
    fi

    if [ "$makedist" = "yes" ]; then
	rm -f VirtualUniverse-autobuild.tar.bz2
	tar cjf VirtualUniverse-autobuild.tar.bz2 VirtualUniverse/bin
    fi
fi

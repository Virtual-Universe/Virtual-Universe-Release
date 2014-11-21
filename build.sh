#!/bin/bash
ARCH="x64"
CONFIG="Debug"
BUILD=true

USAGE="[-c <config>] -a <arch>"
LONG_USAGE="Configuration options to pass to prebuild environment

Options:
  -c|--config Build configuration Debug(default) or Release
  -a|--arch Architecture to target x86(default), x64
"

while case "$#" in 0) break ;; esac
do
  case "$1" in
    -c|--config)
      shift
      CONFIG="$1"
      ;;
    -a|--arch)
      shift
      ARCH="$1"
      ;;
    -b|--build)
      BUILD=true
      ;;
    -h|--help)
      echo "$USAGE"
      echo "$LONG_USAGE"
      exit
      ;;
    *)
      echo "Illegal option!"
      echo "$USAGE"
      echo "$LONG_USAGE"
      exit
      ;;
  esac
  shift
done

echo Configuring Virtual Universe

mono packages/Prebuild.exe /target vs2010 /targetframework v4_5 /conditionals "LINUX;NET_4_5"
if [ -d ".svn" ]; then svn log --pretty=format:"Virtual Universe:%h" -n 1 > packages/.version; fi

if ${BUILD:=true} ; then
  echo Building Virtual Universe
  xbuild /property:Configuration="$CONFIG" /property:Platform="$ARCH"
  echo Finished Building Virtual Universe
  echo Thank you for choosing Virtual Universe
  echo Please report any errors to our mantis at http://www.mantis.virtualplanets.org
fi

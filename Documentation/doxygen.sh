#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd $DIR/../
mkdir -p VirtualUniverse/UniverseDocs/doxygen
rm -fr VirtualUnivere/UniverseDocs/doxygen/*
doxygen Documentation/doxygen.conf

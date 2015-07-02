DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd ${DIR}/../
mkdir -p VirtualUniverse/UniverseDocs/doxygen
rm -fr VirtualUniverse/UniverseDocs/doxygen/*
doxygen Documentation/doxygen.conf
@ECHO OFF

ECHO ============================================
ECHO = Starting Virtual Universe Standalone Sim =
ECHO ============================================

chdir /D  %~dp0
cd .\bin
.\Universe.exe -skipconfig
cd ..
Echo.
Echo Virtual Universe stopped . . .

set /p nothing= Enter to continue
exit
@ECHO OFF


ECHO =======================================
ECHO Starting Universe Grid servers . . .
ECHO =======================================

chdir /D  %~dp0
cd bin
Universe.Server.exe -skipconfig
cd ..
Echo.
Echo Universe Grid servers stopped . . .

set /p nothing= Enter to continue
exit

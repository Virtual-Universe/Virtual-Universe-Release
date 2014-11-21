@ECHO OFF

echo ====================================
echo ==== VIRTUAL REALITY ========================
echo ====================================
echo.

rem ## Default Course of Action (VirtualWorld,VirtualServer,Setup,Exit)
set choice=VirtualReality

rem ## Auto-restart on exit/crash (y,n)
set auto_restart=y

rem ## Pause on crash/exit (y,n)
set auto_pause=y

echo Welcome to the Virtual Reality launcher.
if %auto_restart%==y echo I am configured to automatically restart on exit.
if %auto_pause%==y echo I am configured to automatically pause on exit.
echo You can edit this batch file to change your default choices.
echo.
echo You have the following choices:
echo	- VirtualWorld: Launches Virtual World
echo	- VirtualServer: Launches Virtual Reality services
echo	- Exit: Quits
echo.

:action
set /p choice="What would you like to do? (VirtualWorld, VirtualServer, Setup, Exit) [%choice%]: "
if %choice%==VirtualWorld (
	set app="VirtualWorld.exe"
	goto launchcycle
)
if %choice%==VirtualServer (
	set app="VirtualServer.exe"
	goto launchcycle
)
if %choice%==Exit goto eof
if %choice%==Quit goto eof
if %choice%==Abort goto eof

echo "%choice%" isn't a valid choice!
goto action


:launchcycle
echo.
echo Launching %app%...
%app%
if %auto_pause%==y pause
if %auto_restart%==y goto launchcycle

:eof
pause

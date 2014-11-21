@ECHO OFF

/***************************************************************************
 *	                VIRTUAL UNIVERSE PUBLIC SOURCE LICENSE
 * 
 * Date				: Sun January 1, 2006
 * Copyright		: (c) 2006-2014 by Virtual Reality Development Team. 
 *                    All Rights Reserved.
 * Website			: http://www.syndarveruleiki.is
 *
 * Product Name		: Virtual Reality
 * License Text     : packages/docs/VRLICENSE.txt
 * 
 * Planetary Info   : Information about the Planetary code
 * 
 * Copyright        : (c) 2014-2024 by Second Galaxy Development Team
 *                    All Rights Reserved.
 * 
 * Website          : http://www.secondgalaxy.com
 * 
 * Product Name     : Virtual Reality
 * License Text     : packages/docs/SGLICENSE.txt
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the WhiteCore-Sim Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
***************************************************************************/
echo ====================================
echo ==== BUILDING VIRTUAL UNIVERSE =====
echo ====================================
echo.

rem ## Default architecture (86 (for 32bit), 64)
set bits=64

rem ## Whether or not to add the .net4.5 flag
set framework=4_5

rem ## Default "configuration" choice ((r)elease, (d)ebug)
set configuration=d

rem ## Default "run compile batch" choice (y(es),n(o))
set compile_at_end=y

echo I will now ask you four questions regarding your build.
echo However, if you wish to build for:
echo        %bits% Architecture
echo        .NET %framework%
if %compile_at_end%==y echo And you would like to compile straight after prebuild...
echo.
echo Simply tap [ENTER] three times.
echo.
echo Note that you can change these defaults by opening this
echo batch file in a text editor.
echo.

:bits
set /p bits="Choose your architecture (x86, x64) [%bits%]: "
if %bits%==86 goto configuration
if %bits%==x86 goto configuration
if %bits%==64 goto configuration
if %bits%==x64 goto configuration
echo "%bits%" isn't a valid choice!
goto bits

:configuration
set /p configuration="Choose your configuration ((r)elease or (d)ebug)? [%configuration%]: "
if %configuration%==r goto framework
if %configuration%==d goto framework
if %configuration%==release goto framework
if %configuration%==debug goto framework
echo "%configuration%" isn't a valid choice!
goto configuration

:framework
set /p framework="Choose your .NET framework (4_5)? [%framework%]: "
if %framework%==4_0 goto final
if %framework%==4_5 goto final
echo "%framework%" isn't a valid choice!
goto framework

:final
echo.
echo.

if exist Compile.*.bat (
    echo Deleting previous compile batch file...
    echo.
    del Compile.*.bat
)

echo Calling Prebuild for target %vstudio% with framework %framework%...
packages\Prebuild.exe /target vs2010 /targetframework v%framework% /conditionals ISWIN;NET_%framework%

echo.
echo Creating compile batch file for your convinence...
set fpath=C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\msbuild
if %bits%==x64 set args=/p:Platform=x64
if %bits%==x86 set args=/p:Platform=x86
if %configuration%==r  (
    set cfg=/p:Configuration=Release
    set configuration=release
)
if %configuration%==d  (
set cfg=/p:Configuration=Debug
set configuration=debug
)
if %configuration%==release set cfg=/p:Configuration=Release
if %configuration%==debug set cfg=/p:Configuration=Debug
set filename=Compile.VS2010.net%framework%.%bits%.%configuration%.bat

echo %fpath% VirtualUniverse.sln %args% %cfg% > %filename% /p:DefineConstants="ISWIN;NET_%framework%"

echo.
set /p compile_at_end="Done, %filename% created. Compile now? (y,n) [%compile_at_end%]"
if %compile_at_end%==y (
    %filename%
    pause
)
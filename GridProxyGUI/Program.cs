/***************************************************************************
 *	                VIRTUAL REALITY PUBLIC SOURCE LICENSE
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

using System;
using System.IO;
using System.Diagnostics;
using Gtk;

namespace GridProxyGUI
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                InitLogging();
                StartGtkApp();
            }
            catch (Exception ex)
            {
                if (ex is TypeInitializationException || ex is TypeLoadException || ex is System.IO.FileNotFoundException)
                {
                    NativeApi.ExitWithMessage("Failed to start", ex.Message + "\n\nMake sure tha application install isn't missing accompanied files and that Gtk# is installed.", 1);
                }
                throw;
            }
        }

        static void StartGtkApp()
        {
                Gtk.Application.Init();
                MainWindow win = new MainWindow();
                win.Show();
                Application.Run();
        }

        static bool InitLogging()
        {
            try
            {
                string userDir = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "GridProxyGUI");

                if (!Directory.Exists(userDir))
                {
                    Directory.CreateDirectory(userDir);
                }

                string settingsFile = Path.Combine(userDir, "Settings.xml");
                Options.CreateInstance(settingsFile);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public static class NativeApi
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, int options);

        public static void LinuxMessageBox(string title, string msg, string type)
        {
            try
            {
                ProcessStartInfo p = new ProcessStartInfo("zenity", string.Format("--{0} --title=\"{1}\" --text=\"{2}\"", type, title.Replace("\"", "\\\""), msg.Replace("\"", "\\\"")));
                p.CreateNoWindow = true;
                p.ErrorDialog = false;
                p.UseShellExecute = true;
                var process = Process.Start(p);
                process.WaitForExit();
            }
            catch { }
        }

        public static void ExitWithMessage(string title, string msg, int exitCode)
        {
            Console.Error.WriteLine(title + ": " + msg);
            if (PlatformDetection.IsWindows)
            {
                MessageBox(IntPtr.Zero, msg, title, 0x10);
            }
            else if (PlatformDetection.IsMac)
            {
            }
            else
            {
                LinuxMessageBox(title, msg, "error");
            }

            Environment.Exit(exitCode);
        }
    }
}

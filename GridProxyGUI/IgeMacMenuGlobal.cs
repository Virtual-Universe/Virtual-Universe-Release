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
using System.Collections;
using System.Runtime.InteropServices;

namespace IgeMacIntegration
{

    public class IgeMacMenu
    {

        [DllImport("libigemacintegration.dylib")]
        static extern void ige_mac_menu_connect_window_key_handler(IntPtr window);

        public static void ConnectWindowKeyHandler(Gtk.Window window)
        {
            ige_mac_menu_connect_window_key_handler(window.Handle);
        }

        [DllImport("libigemacintegration.dylib")]
        static extern void ige_mac_menu_set_global_key_handler_enabled(bool enabled);

        public static bool GlobalKeyHandlerEnabled
        {
            set
            {
                ige_mac_menu_set_global_key_handler_enabled(value);
            }
        }

        [DllImport("libigemacintegration.dylib")]
        static extern void ige_mac_menu_set_menu_bar(IntPtr menu_shell);

        public static Gtk.MenuShell MenuBar
        {
            set
            {
                ige_mac_menu_set_menu_bar(value == null ? IntPtr.Zero : value.Handle);
            }
        }

        [DllImport("libigemacintegration.dylib")]
        static extern void ige_mac_menu_set_quit_menu_item(IntPtr quit_item);

        public static Gtk.MenuItem QuitMenuItem
        {
            set
            {
                ige_mac_menu_set_quit_menu_item(value == null ? IntPtr.Zero : value.Handle);
            }
        }

        [DllImport("libigemacintegration.dylib")]
        static extern IntPtr ige_mac_menu_add_app_menu_group();

        public static IgeMacIntegration.IgeMacMenuGroup AddAppMenuGroup()
        {
            IntPtr raw_ret = ige_mac_menu_add_app_menu_group();
            IgeMacIntegration.IgeMacMenuGroup ret = raw_ret == IntPtr.Zero ? null : (IgeMacIntegration.IgeMacMenuGroup)GLib.Opaque.GetOpaque(raw_ret, typeof(IgeMacIntegration.IgeMacMenuGroup), false);
            return ret;
        }
    }

    public class IgeMacMenuGroup : GLib.Opaque
    {

        [DllImport("libigemacintegration.dylib")]
        static extern void ige_mac_menu_add_app_menu_item(IntPtr raw, IntPtr menu_item, IntPtr label);

        public void AddMenuItem(Gtk.MenuItem menu_item, string label)
        {
            IntPtr native_label = GLib.Marshaller.StringToPtrGStrdup(label);
            ige_mac_menu_add_app_menu_item(Handle, menu_item == null ? IntPtr.Zero : menu_item.Handle, native_label);
            GLib.Marshaller.Free(native_label);
        }

        public IgeMacMenuGroup(IntPtr raw) : base(raw) { }
    }
}

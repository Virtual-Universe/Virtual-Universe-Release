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
using System.Collections.Generic;
using Gtk;
using GridProxyGUI;

namespace GridProxyGUI
{
    public class FilterScroller : ScrolledWindow
    {
        ListStore store;

        public FilterScroller(Container parent, ListStore store)
        {
            this.store = store;

            TreeView tvFilterUDP = new TreeView();
            TreeViewColumn cbCol = new TreeViewColumn();
            TreeViewColumn udpCol = new TreeViewColumn();

            CellRendererToggle cbCell = new CellRendererToggle();
            cbCell.Toggled += new ToggledHandler(cbCell_Toggled);
            cbCell.Activatable = true;
            cbCol.PackStart(cbCell, true);
            cbCol.SetCellDataFunc(cbCell, renderToggleCell);
            tvFilterUDP.AppendColumn(cbCol);

            CellRendererText cell = new CellRendererText();
            udpCol.PackStart(cell, true);
            udpCol.SetCellDataFunc(cell, renderTextCell);
            tvFilterUDP.AppendColumn(udpCol);

            tvFilterUDP.Model = store;
            tvFilterUDP.HeadersVisible = false;
            tvFilterUDP.Selection.Mode = SelectionMode.Single;

            foreach (var child in new List<Widget>(parent.Children))
            {
                parent.Remove(child);
            }

            Add(tvFilterUDP);
            ShadowType = ShadowType.EtchedIn;
            parent.Add(this);
            parent.ShowAll();
        }

        void cbCell_Toggled(object o, ToggledArgs args)
        {
            TreeIter iter;
            if (store.GetIterFromString(out iter, args.Path))
            {
                FilterItem item = store.GetValue(iter, 0) as FilterItem;
                if (null != item)
                {
                    item.Enabled = !item.Enabled;
                    store.SetValue(iter, 0, item);
                }
            }
        }

        void renderTextCell(TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
        {
            var item = model.GetValue(iter, 0) as FilterItem;
            if (item != null)
            {
                ((CellRendererText)cell).Text = item.Name;
            }
        }

        void renderToggleCell(TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
        {
            var item = model.GetValue(iter, 0) as FilterItem;
            if (item != null)
            {
                ((CellRendererToggle)cell).Active = item.Enabled;
            }
        }

    }
}

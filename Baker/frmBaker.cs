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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OpenMetaverse.Imaging;

namespace Baker
{
    public partial class frmBaker : Form
    {
        Bitmap AlphaMask;

        public frmBaker()
        {
            InitializeComponent();
        }

        private void frmBaker_Load(object sender, EventArgs e)
        {
            cboMask.SelectedIndex = 0;
            DisplayResource(cboMask.Text);
        }

        private void DisplayResource(string resource)
        {
            Stream stream = OpenMetaverse.Helpers.GetResourceStream(resource + ".tga");

            if (stream != null)
            {
                AlphaMask = LoadTGAClass.LoadTGA(stream);
                stream.Close();

                //ManagedImage managedImage = new ManagedImage(AlphaMask);

                // FIXME: Operate on ManagedImage instead of Bitmap
                pic1.Image = Oven.ModifyAlphaMask(AlphaMask, (byte)scrollWeight.Value, 0.0f);
            }
            else
            {
                MessageBox.Show("Failed to load embedded resource \"" + resource + "\"", "Baker",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void scrollWeight_Scroll(object sender, ScrollEventArgs e)
        {
            pic1.Image = Oven.ModifyAlphaMask(AlphaMask, (byte)scrollWeight.Value, 0.0f);
        }

        private void frmBaker_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void cmdLoadSkin_Click(object sender, EventArgs e)
        {

        }

        private void cboMask_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayResource(cboMask.Text);
        }
    }
}

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
using System.Reflection;
using System.Xml;
using System.Windows.Forms;
using Nini.Config;
using Aurora.Framework;
using Aurora.Framework.Services;
using Aurora.Framework.Modules;
using Aurora.Framework.ConsoleFramework;
using Aurora.Framework.Utilities;

namespace Aurora.UpdateManager
{
    public class UpdaterPlugin : IService, 
	#region Private Functions
	#endregion
	System.IDisposable
	{
		const string m_urlToCheckForUpdates = "http://www.virtualreality.is/updates.xml";
		static bool Compare (string givenVersion, string CurrentVersion)
		{
			string[] given = givenVersion.Split ('.');
			string[] current = CurrentVersion.Split ('.');
			for (int i = 0; i < (int)Math.Max (given.Length, current.Length); i++) {
				if (i == given.Length || i == current.Length)
					break;
				if (int.Parse (given [i]) > int.Parse (current [i]))
					return true;
			}
			return false;
		}
		public void Start (IConfigSource config, IRegistryCore registry)
		{
			try {
				//Check whether this is enabled
				IConfig updateConfig = config.Configs ["Updater"];
				if (updateConfig.GetString ("Plugin", "") != Name) {
					MainConsole.Instance.Info ("[Updater]: Updater Plugin not set");
					return;
				}
				if (!updateConfig.GetBoolean ("Enabled", false)) {
					MainConsole.Instance.Info ("[Updater]: Updater Plugin not enabled");
					return;
				}
				// Everything is working, let's start checking for an update
				MainConsole.Instance.Info ("[Updater]: Checking for updates...");
				const string CurrentVersion = VersionInfo.VERSION_NUMBER;
				string LastestVersionToBlock = updateConfig.GetString ("LatestRelease", VersionInfo.VERSION_NUMBER);
				string WebSite = updateConfig.GetString ("URLToCheckForUpdates", m_urlToCheckForUpdates);
				//Pull the xml from the website
				string XmlData = Utilities.ReadExternalWebsite (WebSite);
				if (string.IsNullOrEmpty (XmlData)) {
					MainConsole.Instance.ErrorFormat ("[Updater]: Unable to reach {0} due to network error", m_urlToCheckForUpdates);
					return;
				}
				XmlDocument doc = new XmlDocument ();
				doc.LoadXml (XmlData);
				XmlNodeList parts = doc.GetElementsByTagName ("Updater");
				XmlNode UpdaterNode = parts [0];
				//[0] - Minimum supported release #
				//[1] - Minimum supported release date
				//[2] - Newest version #
				//[3] - Date released
				//[4] - Release notes
				//[5] - Download link for Windows .Net
				//[6] - Download link for Mono 32 Bit
				//[7] - Download link for Mono 64 Bit
				//Read the newest version [2] and see if it is higher than the current version and less than the version the user last told us to block
				if (Compare (UpdaterNode.ChildNodes [2].InnerText, CurrentVersion) && Compare (UpdaterNode.ChildNodes [2].InnerText, LastestVersionToBlock)) {
					//Ask if they would like to update
					DialogResult result = MessageBox.Show ("A new version has been released, version " + UpdaterNode.ChildNodes [2].InnerText + " released " + UpdaterNode.ChildNodes [3].InnerText + ". Release notes: " + UpdaterNode.ChildNodes [4].InnerText + ", do you want to download the update?", "Updater", MessageBoxButtons.YesNo);
					//If so, download the new version
					if (result == DialogResult.Yes) {
						string updateLink = "";
						// Do a check if they are running on Windows .Net or Mono x86/x64
						if (!Utilities.IsLinuxOs) {
							updateLink = UpdaterNode.ChildNodes [5].InnerText;
						}
						else {
							if (!Utilities.Is64BitOs) {
								updateLink = UpdaterNode.ChildNodes [6].InnerText;
							}
							else {
								updateLink = UpdaterNode.ChildNodes [7].InnerText;
							}
						}
						// UpdaterNode.ChildNodes[5].InnerText = Windows (x86/x64)
						// UpdaterNode.ChildNodes[6].InnerText = Mono x86
						// UpdaterNode.ChildNodes[7].InnerText = Mono x64
						Utilities.DownloadFile (updateLink, "VirtualReality" + UpdaterNode.ChildNodes [2].InnerText + ".zip");
						MessageBox.Show (string.Format ("Downloaded to {0}, exiting for user to upgrade.", "VirtualReality" + UpdaterNode.ChildNodes [2].InnerText + ".zip"), "Updater");
						Environment.Exit (0);
					}
					//Update the config so that we do not ask again
					updateConfig.Set ("LatestRelease", UpdaterNode.ChildNodes [2].InnerText);
					updateConfig.ConfigSource.Save ();
				}
				else
					if (Compare (UpdaterNode.ChildNodes [0].InnerText, CurrentVersion) && Compare (UpdaterNode.ChildNodes [2].InnerText, LastestVersionToBlock)) {
						//This version is not supported anymore
						MainConsole.Instance.FatalFormat ("[Updater]: Your version of Virtual Reality ({0}, Released {1}) is not supported anymore.", CurrentVersion, UpdaterNode.ChildNodes [1].InnerText);
					}
					else {
						MainConsole.Instance.Info ("[Updater]: You are currently running the latest released version");
					}
			}
			catch {
			}
		}
		public void FinishedStartup ()
		{
		}
		public void Initialize (IConfigSource config, IRegistryCore registry)
		{
		}
		public string Name {
			get {
				return "Updater";
			}
		}
		public void Dispose ()
		{
		}
		public void Close ()
		{
		}
	}
}
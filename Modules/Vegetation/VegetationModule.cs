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
using System.Timers;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenMetaverse;
using Nini.Config;
using Aurora.Framework.Modules;
using Aurora.Framework.SceneInfo;
using Aurora.Framework.SceneInfo.Entities;
using Aurora.Framework.Utilities;
using Aurora.Framework.ConsoleFramework;
using Aurora.Framework.PresenceInfo;
using Aurora.Region;

namespace Aurora.Modules.Vegetation
{
    /// <summary>
    /// Version 1.0 (Trees)
    /// </summary>
    public class VegetationModule : INonSharedRegionModule, IVegetationModule
    {
        //private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IScene m_scene;
		private bool m_enabled = true;

        [XmlRootAttribute(ElementName = "Copse", IsNullable = false)]
        public class Copse
        {
            public string m_name;
            public Boolean m_frozen;
            public Tree m_tree_type;
            public int m_tree_quantity; 
            public float m_treeline_low;
            public float m_treeline_high;
            public Vector3 m_seed_point;
            public double m_range;
            public Vector3 m_initial_scale;
            public Vector3 m_maximum_scale;
            public Vector3 m_rate;

            [XmlIgnore]
            public Boolean m_planted;
            [XmlIgnore]
            public List<UUID> m_trees;

            public Copse()
            {
            }

			/// <summary>
			/// Initializes a new instance of the Copse when loaded from a definition file"/> class.
			/// </summary>
			/// <param name="fileName">File name.</param>
			/// <param name="planted">If set to <c>true</c> planted.</param>
			public Copse(string fileName, Boolean planted) 
            {
                Copse cp = (Copse)DeserializeObject(fileName);

                this.m_name = cp.m_name;
                this.m_frozen = cp.m_frozen;
                this.m_tree_quantity = cp.m_tree_quantity;
                this.m_treeline_high = cp.m_treeline_high;
                this.m_treeline_low = cp.m_treeline_low;
                this.m_range = cp.m_range;
                this.m_tree_type = cp.m_tree_type;
                this.m_seed_point = cp.m_seed_point;
                this.m_initial_scale = cp.m_initial_scale;
                this.m_maximum_scale = cp.m_maximum_scale;
                this.m_initial_scale = cp.m_initial_scale;
                this.m_rate = cp.m_rate;
                this.m_planted = planted;
                this.m_trees = new List<UUID>();
            }

            public Copse(string copsedef)
            {
				char[] delimiterChars = { ':', ';' };
                string[] field = copsedef.Split(delimiterChars);

                this.m_name = field[1].Trim();
                this.m_frozen = copsedef[0] == 'F';
                this.m_tree_quantity = int.Parse(field[2]);
                this.m_treeline_high = float.Parse(field[3], Culture.NumberFormatInfo);
                this.m_treeline_low = float.Parse(field[4], Culture.NumberFormatInfo);
                this.m_range = double.Parse(field[5], Culture.NumberFormatInfo);
				this.m_tree_type = (Tree) Enum.Parse(typeof(Tree),field[6]);
                this.m_seed_point = Vector3.Parse(field[7]);
                this.m_initial_scale = Vector3.Parse(field[8]);
                this.m_maximum_scale = Vector3.Parse(field[9]);
                this.m_rate = Vector3.Parse(field[10]);
                this.m_planted = true;
                this.m_trees = new List<UUID>();
            }

            public Copse(string name, int quantity, float high, float low, double range, Vector3 point, Tree type, Vector3 scale, Vector3 max_scale, Vector3 rate, List<UUID> trees)
            {
                this.m_name = name;
                this.m_frozen = false;
                this.m_tree_quantity = quantity;
                this.m_treeline_high = high;
                this.m_treeline_low = low;
                this.m_range = range;
                this.m_tree_type = type;
                this.m_seed_point = point;
                this.m_initial_scale = scale;
                this.m_maximum_scale = max_scale;
                this.m_rate = rate;
                this.m_planted = false;
                this.m_trees = trees;
            }

            public override string ToString()
            {
                string frozen = m_frozen ? "F" : "A";

                return string.Format("{0}TPM: {1}; {2}; {3:0.0}; {4:0.0}; {5:0.0}; {6}; {7:0.0}; {8:0.0}; {9:0.0}; {10:0.00};", 
                    frozen,
                    m_name,
                    m_tree_quantity,
                    m_treeline_high,
                    m_treeline_low,
                    m_range,
                    m_tree_type,
                    m_seed_point,
                    m_initial_scale,
                    m_maximum_scale,
                    m_rate);
            }
        }

        private List<Copse> m_copse;

		private IConfig treeConfig;
		private double m_update_ms = 2000.0; 	// msec between updates 
		private bool m_active_trees = false;

        Timer CalculateTrees;

        #region IRegionModule Members

        public void Initialise( IConfigSource config )
        {
			treeConfig = config.Configs["Trees"];

            if ( treeConfig != null )
			{
                m_enabled = treeConfig.GetBoolean ( "enabled", false );

                if ( m_enabled )
				{
                    m_active_trees = config.Configs ["Trees"].GetBoolean ( "active_trees", false );
                    m_update_ms = config.Configs ["Trees"].GetDouble ( "update_rate", 2000 );
				}
			}
        }

		public void AddRegion ( IScene scene )
        {
			m_scene = scene;
            if ( m_enabled )
            {
                //m_scene = scene;
				m_scene.RegisterModuleInterface<IVegetationModule>(this);
				m_scene.SceneGraph.RegisterEntityCreatorModule(this);
                AddConsoleCommands();
            }
        }

		public void RemoveRegion ( IScene scene )
        {
        }

		public void RegionLoaded ( IScene scene )
        {
			if ( m_enabled )
            {
				ReloadCopse();
                if ( m_copse.Count > 0 )
				{
                    MainConsole.Instance.Info ( "[TREES]: Copse load complete" );
				}
				// are we actively growing trees?
				if ( m_active_trees )
				{
					activeizeTreeze (true);
				}
            }
        }

        public Type ReplaceableInterface
        {
            get { return null; }
        }

        /// <summary>
        /// Posts the initialise.
        /// </summary>
        public void PostInitialise()
        {
        }

        /// <summary>
        /// Close this instance. (not used)
        /// </summary>
        public void Close()
        {
        }

        /// <value>The name of the module</value>
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "Vegetation Module"; }
        }

        #endregion
        //--------------------------------------------------------------

        #region ICommandableModule Members

		/// <summary>
		/// Trees check foa a valid region.
		/// </summary>
		/// <returns><c>true</c>, if check valid region, <c>false</c> otherwise.</returns>	
		/// <param name="scene">Scene.</param>
		private Boolean TreeCheckValidRegion( IScene scene )
		{
			IScene selectedScene = MainConsole.Instance.ConsoleScene;
             
            if ( selectedScene == null )
			{
				MainConsole.Instance.Info ("[TREES]: The 'tree' commands only operate on a region" +
				" \n          Please change to a region first");
            } else if ( selectedScene == scene )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Activate/de-activate tree growth
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
		private void HandleTreeActive(IScene scene, string[] cmd)
        {
            if ( !TreeCheckValidRegion( m_scene ) )
            {
				return;
			}
			
            if ( cmd.Count() < 3 ) 
            {
                string curState = m_active_trees ? "Active" : "Disabled";
                MainConsole.Instance.Info ( "[TREES]: Currently "+ curState );
				//				MainConsole.Instance.Info("[TREES]: Currently "+ (m_active_trees ? "Active" : "Disabled"));
				return;
			}
			
            bool activeState = Boolean.Parse(cmd[2]);

            if ( activeState && ! this.m_active_trees )
            {
                MainConsole.Instance.Info ( "[TREES]: Activating Trees" );
				activeizeTreeze( true );
            }
            else if ( !activeState && this.m_active_trees )
            {
                MainConsole.Instance.Info ( "[TREES]: Disabling Trees" );
				activeizeTreeze( false );
            }
            else
            {
                MainConsole.Instance.Info ( "[TREES]: Already "+ ( m_active_trees ? "Active" : "Disabled" ) );
            }
        }

		/// <summary>
		/// Freeze the growth (update) of a copse
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
		private void HandleTreeFreeze(IScene scene, string[] cmd)
		{
            string copseName = string.Empty;
            if ( !TreeCheckValidRegion ( m_scene ) )
				return;

            if ( cmd.Count () > 2 )
                copseName = cmd [2].Trim ();

            while ( copseName == String.Empty )
            {
                copseName = ReadLine ("Which copse would you like to Freeze?", "?");
                if ( copseName[0] == '?' )
                {
                    ListExistingCopse();
                    copseName = string.Empty;
                    continue;
                }

                // provide an "out"
                if (copseName.ToLower() == "quit")
                    return;
            }

            HandleTreeFreezeState ( copseName, true );
		}

		/// <summary>
		/// Unfreeze the tree growth of a copse
		/// </summary>
		/// <param name="scene">Scene.</param>
	    /// <param name="cmd">Cmd.</param>
		private void HandleTreeUnFreeze(IScene scene, string[] cmd)
		{
            string copseName = string.Empty;
            if ( !TreeCheckValidRegion ( m_scene ) )
				return;

            if ( cmd.Count () > 2 )
			    copseName = cmd [2].Trim ();

            while ( copseName == String.Empty )
            {
                copseName = ReadLine ("Which copse would you like to UnFreeze?", "?");
                if ( copseName[0] == '?' )
                {
                    ListExistingCopse();
                    copseName = string.Empty;
                    continue;
                }

                // provide an "out"
                if (copseName.ToLower() == "quit")
                    return;
            }
            this.HandleTreeFreezeState ( copseName, false );
		}

		/// <summary>
		/// Handles the copse freeze state.
		/// </summary>
		/// <param name="copseName">Copse name.</param>
		/// <param name="freezeState">If set to <c>true</c> freeze state.</param>
		private void HandleTreeFreezeState(string copseName, Boolean freezeState)
		{
            foreach ( Copse cp in m_copse )
            {
                if ( ( cp.m_name == copseName) && ( (!cp.m_frozen && freezeState) || (cp.m_frozen && !freezeState) ) )
                {
                    cp.m_frozen = freezeState;
                    foreach ( UUID tree in cp.m_trees )
                    {
                        IEntity ent;
                        if ( m_scene.Entities.TryGetValue( tree, out ent ) && ent is ISceneEntity )
                        {
                            ISceneChildEntity sop = ( (ISceneEntity) ent ).RootChild;
                            sop.Name = ( freezeState ? sop.Name.Replace ("ATPM", "FTPM") : sop.Name.Replace ( "FTPM", "ATPM" ) );
                            sop.ParentEntity.HasGroupChanged = true;
                        }
                    }

                    MainConsole.Instance.Info ( "[TREES]: Activity for copse "+copseName+" is now "+ (freezeState ? "frozen" : "unfrozen"));
                    return;
                }
                else if (( cp.m_name == copseName ) && ( ( cp.m_frozen && freezeState ) || ( !cp.m_frozen && !freezeState ) ) )
                {
                    MainConsole.Instance.Info ( "[TREES]: Copse '" + copseName + " is already in the requested freeze state" );
                    return; 
                }
            }
            MainConsole.Instance.Info ( "[TREES]: Copse " + copseName + " was not found" );
        }

		/// <summary>
		/// Load a copse definition from file
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
	    private void HandleTreeLoad(IScene scene, string[] cmd)
        {
            if ( !TreeCheckValidRegion( m_scene ) )
            {
				return;
			}

            if ( cmd.Count() < 3 )
			{
                MainConsole.Instance.Info ( "[TREES]: You need to specify the filename to load" );
				return;
			}

			string fileName = cmd [2];
            string extension = Path.GetExtension( fileName );

            if ( extension == string.Empty)
            {
				fileName = fileName+".copse";
			}

            if ( !File.Exists( fileName ) )
            {
                MainConsole.Instance.Info ( "[TREES]: Copse definition file '" + fileName + "' not found." );
				return;
			}

            MainConsole.Instance.Info ( "[TREES]: Loading copse definition...." );
            Copse newCopse = new Copse( cmd[2], false );

            foreach ( Copse cp in m_copse )
            {
                if ( cp.m_name == newCopse.m_name )
                {
                    MainConsole.Instance.Info ( "[TREES]: Copse: " + newCopse.m_name + " is already defined - load ignored" );
                    return;
                }
            }

            this.m_copse.Add( newCopse );
            MainConsole.Instance.Info ( "[TREES]: Loaded copse: " + newCopse );
        }

		/// <summary>
		/// Save copse definition
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
		private void HandleTreeSave(IScene scene, string[] cmd)
		{
            if ( !TreeCheckValidRegion( m_scene ) ) {
				return;
			}

            if ( cmd.Count() < 4 )
			{
                MainConsole.Instance.Info ( "[TREES]: You need to specify the copse and filename to save" );
				return;
		    }

		    string copseName = cmd [2];
			string fileName = cmd [3];

            // some file sanity checks
            string extension = Path.GetExtension( fileName );

            if (extension == string.Empty)
            {
                fileName = fileName + ".copse";
		    }

            string fileDir = Path.GetDirectoryName( fileName );
            if ( fileDir == "" ) { fileDir = "Settings/Trees"; }
            if ( !Directory.Exists( fileDir ) )
			{
                MainConsole.Instance.Info ( "[TREES]: The folder specified, '" + fileDir + "' does not exist!" );
				return;
			}

            if ( File.Exists( fileName )) {
                if ( MainConsole.Instance.Prompt ( "[TREES]: Copse definition file '"+fileName+"' exists. Overwrite?", "yes" ) != "yes" )
		 		return;

                File.Delete ( fileName );
			}

			//MainConsole.Instance.Info("[TREES]: Saving copse definition....");
            foreach ( Copse cp in this.m_copse )
		    {
                if ( cp.m_name == copseName )
				{
                    SerializeObject( fileName, cp );
				}
			}

            MainConsole.Instance.Info ( "[TREES]: Saved copse definition for : " + copseName );
		}

		/// <summary>
		/// Plant an existing copse of trees.
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
        private void HandleTreePlant( IScene scene, string[] cmd)
        {
            string copseName = string.Empty;
			if ( !TreeCheckValidRegion(m_scene) ) {
				return;
			}

            if (cmd.Count() > 2)
                copseName = cmd[2].Trim();

            while ( copseName == String.Empty )
            {
                copseName = ReadLine ("Which copse would you like to plant?", "?");
                if ( copseName[0] == '?' )
                {
                    ListExistingCopse();
                    copseName = string.Empty;
                    continue;
                }

                // provide an "out"
                if (copseName.ToLower() == "quit")
                    return;
            }

            PlantTreeCopse (copseName);

        }

		/// <summary>
		/// Modify the tree growth rate (update rate)
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
        private void HandleTreeRate( IScene scene, string[] cmd )
        {
            if ( !TreeCheckValidRegion( m_scene ) ) {
				return;
			}

            if ( cmd.Count() < 3 )
			{
                MainConsole.Instance.Info ( "[TREES]: You need to specify the update rate for the region" );
				return;
			}

			double updateRate = double.Parse(cmd[2]);
            if ( updateRate >= 1.0 )
            {
                m_update_ms = updateRate * 1000;
                
				// currently growing?
				if ( m_active_trees )
                {
					// toggle to reset update times
                    activeizeTreeze(false);
                    activeizeTreeze(true);
                }
                MainConsole.Instance.Info ( "[TREES]: Update rate set to "+ updateRate.ToString("0.00")+" Sec" );
            }
            else
            {
                MainConsole.Instance.Info ( "[TREES]: minimum update rate is 1 Sec" );
            }
        }

		/// <summary>
		/// reload a copse of trees.
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
		private void HandleTreeReload(IScene scene, string[] cmd)
        {
            if ( !TreeCheckValidRegion( m_scene ) ) {
				return;
			}

            if ( m_active_trees )
            {
                CalculateTrees.Stop();
            }

            ReloadCopse();

            if ( m_active_trees )
            {
                CalculateTrees.Start();
            } else
            {
                string resp = "yes";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.ResetColor();
                resp = ReadLine( "Do you want to activate tree growth now?", resp ).ToLower();

                if ( resp[0] == 'y' ) 
                {
                    MainConsole.Instance.Info ( "[TREES]: Activating Trees" );
                    this.activeizeTreeze( true );
                }
            }

        }

		/// <summary>
		/// remove a copse of trees.
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
        private void HandleTreeRemove( IScene scene, string[] cmd )
        {
            string copseName = string.Empty;

			if ( !TreeCheckValidRegion( m_scene ) ) 
				return;

            if ( cmd.Count() > 2 )
                copseName = cmd[2].Trim();

            while ( copseName == String.Empty )
			{
                copseName = ReadLine ("Which copse would you like to remove?", "?");
                if ( copseName[0] == '?' )
				{
					ListExistingCopse();
                    copseName = string.Empty;
					continue;
				}

			    // provide an "out"
                if (copseName.ToLower() == "quit")
    				return;

            }

			Copse copseIdentity = null;

            foreach ( Copse cp in this.m_copse )
            {
                if ( cp.m_name == copseName )
                {
                    copseIdentity = cp;
                }
            }

            if (copseIdentity != null)
            {
                List<ISceneEntity> groups = new List<ISceneEntity>();
                foreach ( UUID tree in copseIdentity.m_trees)
                {
                    IEntity entity;
                    if ( m_scene.Entities.TryGetValue ( tree, out entity  ))
                    {
                        if ( entity is ISceneEntity )
                            groups.Add( (ISceneEntity) entity );
                    }
                }
                IBackupModule backup = m_scene.RequestModuleInterface<IBackupModule>();
                if ( backup != null )
                {
                    backup.DeleteSceneObjects(groups.ToArray(), true, true );
                }
                copseIdentity.m_trees = new List<UUID>();
                m_copse.Remove(copseIdentity);
                MainConsole.Instance.Info ( "[TREES]: Copse "+copseName+" has been removed" );
            }
            else
            {
                MainConsole.Instance.Info ( "[TREES]: Copse "+copseName+" was not found" );
            }
        }

		/// <summary>
		/// tree statistics.
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
        private void HandleTreeStatistics( IScene scene, string[] cmd )
        {
			if ( !TreeCheckValidRegion(m_scene) ) {
				return;
		     }

            MainConsole.Instance.Info( "[TREES]: Active: " +  (m_active_trees ? "Yes" : "Disabled") + 
                ";  Update Rate: " + ( m_update_ms/1000 ).ToString("0.00")+" Sec" );

            ListExistingCopse();

        }

        /// <summary>
        /// Reads a response frome the console.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="infoMsg">information message.</param>
        /// <param name="defaultReturn">Default return.</param>
        private static string ReadLine( string infoMsg, string defaultReturn )
		{
            Console.Write( infoMsg + ": [" + defaultReturn + "] > " );
			string mode = Console.ReadLine();
            if ( mode == string.Empty )
				mode = defaultReturn;
            if ( mode != null )
				mode = mode.Trim();
			return mode;
		}

		/// <summary>
		/// Gets/updates the copse definitions.
		/// </summary>
		/// <returns>The copse definitions.</returns>
		/// <param name="cp">Cp.</param>
		/// <param name="advancedOptions">If set to <c>true</c> advanced options.</param>
		private static Copse GetCopseDefinitions(Copse cp, bool advancedOptions)
		{
			int treeType = GetTreeType ((int) cp.m_tree_type);
			cp.m_tree_type = (Tree)treeType; 

			cp.m_tree_quantity = int.Parse(ReadLine("How many trees do you want?", cp.m_tree_quantity.ToString()));
			cp.m_treeline_high = int.Parse(ReadLine("The highest elevation to grow a tree?", cp.m_treeline_high.ToString()));
			cp.m_treeline_low = int.Parse(ReadLine("The lowest elevation to grow a tree (may be below the waterline)?", cp.m_treeline_low.ToString()));
			cp.m_range = double.Parse(ReadLine("The maximum distance away from the initial seed point to grow trees?", cp.m_range.ToString()));

			// allow entry of seed point only if the initial plant has not happened
			if (!cp.m_planted)
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine ("Enter the position where to plant the first tree\n" +
					"e.g. to start in the middle of a 256x256 region enter position x = 128 and position y = 128");
				Console.ResetColor ();
				cp.m_seed_point.X = int.Parse (ReadLine ("Plant the first tree at X location ", cp.m_seed_point.X.ToString ()));
				cp.m_seed_point.Y = int.Parse (ReadLine ("Plant the first tree at Y location ", cp.m_seed_point.Y.ToString ()));
			}

			if ( advancedOptions )
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine ("Enter the initial size of the tree when it is planted <size> &  <height>\n");
				Console.ResetColor();
				cp.m_initial_scale.X = int.Parse(ReadLine("Initial tree size ", cp.m_initial_scale.X.ToString()));
				cp.m_initial_scale.Z = int.Parse(ReadLine("Initial tree height ", cp.m_initial_scale.Z.ToString()));
				cp.m_initial_scale.Y = cp.m_initial_scale.X;

				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine ("Enter the maximum size of the tree when it is fully grown <size> &  <height>\n");
				Console.ResetColor();
				cp.m_maximum_scale.X = int.Parse(ReadLine("Maximum tree size ", cp.m_maximum_scale.X.ToString()));
				cp.m_maximum_scale.Z = int.Parse(ReadLine("Maximum tree height ", cp.m_maximum_scale.Z.ToString()));
				cp.m_maximum_scale.Y = cp.m_maximum_scale.X;

				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine ("Enter the growth rate of the tree <horizontal> & <vertical>\n");
				Console.ResetColor();
				cp.m_rate.X = float.Parse(ReadLine("Horizontal growth rate of the tree ", cp.m_rate.X.ToString()));
				cp.m_rate.Z = int.Parse(ReadLine("Vertical growth rate ot the tree ", cp.m_rate.Z.ToString()));
				cp.m_rate.Y = cp.m_rate.X;
			}

			return cp;
		}

		/// <summary>
		/// Gets the type of the tree.
		/// </summary>
		/// <returns>The tree type.</returns>
		/// <param name="treeType">Tree type.</param>
		private static int GetTreeType(int treeType)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("The available trees are...\n"+
				"[1]  Pine 1,      [2]  Oak,             [3]  Tropical Bush, [4]  Palm,\n"+
				"[5]  Dogwood,     [6]  Tropical Bush 2, [7]  Palm 2,        [8]  Cypress 1,\n"+
				"[9]  Cypress 2,   [10] Pine 2,          [11] Plumeria,      [12] WinterPine 1,\n"+
				"[13] WinterAspen, [14] WinterPine 2,    [15] Eucalyptus,    [16] Fern,\n"+
				"[17] Eelgrass,    [18] SeaSword,        [19] Kelp 1,        [20] BeachGrass,\n"+
				"[21] Kelp 2");
			Console.ResetColor();
			string treeSelection = ReadLine("Tree type", (treeType+1).ToString());
			return (int.Parse(treeSelection)-1);
		}

		/// <summary>
		/// Gets the name of the tree.
		/// </summary>
		/// <returns>The tree name.</returns>
		/// <param name="treeType">Tree type.</param>
		private static string GetTreeName(int treeType)
		{
			switch (treeType)
			{
			case 0:	return "Pine 1";
			case 1:	return "Oak";
			case 2: return "Tropical Bush";
			case 3: return "Palm";
			case 4: return "Dogwood";
			case 5: return "Tropical Bush 2";
			case 6: return "Palm 2";
			case 7: return "Cypress 1";
			case 8: return "Cypress 2";
			case 9: return "Pine 2";
			case 10: return "Plumeria";
			case 11: return "WinterPine 1";
			case 12: return "WinterAspen";
			case 13: return "WinterPine 2";
			case 14: return "Eucalyptus";
			case 15: return "Fern";
			case 16: return "Eelgrass";
			case 17: return "SeaSword";
			case 18: return "Kelp 1";
			case 19: return "BeachGrass";
			case 20: return "Kelp 2";
			default:
				return "Unknown";
			}
		}

		/// <summary>
		/// Plants the initial tree of a copse.
		/// </summary>
		/// <param name="copseName">Copse name.</param>
		private void PlantTreeCopse(string copseName)
		{
			MainConsole.Instance.Info ( "[TREES]: New tree planting for copse " + copseName );
			UUID uuid = m_scene.RegionInfo.EstateSettings.EstateOwner;

			foreach ( Copse copse in m_copse )
			{
				if ( copse.m_name == copseName )
				{
					if ( !copse.m_planted )
					{
						// check position height
						Vector3 position = copse.m_seed_point;
						position.Z = m_scene.RequestModuleInterface<ITerrainChannel>()[(int)position.X, (int)position.Y];
						if (position.Z >= copse.m_treeline_low && position.Z <= copse.m_treeline_high)
						{
							// All good... Plant the first tree for the copse
							CreateTree (uuid, copse, copse.m_seed_point);
							copse.m_planted = true;

							Console.ForegroundColor = ConsoleColor.Cyan;
							Console.WriteLine ("\n=================================================\n");
							Console.WriteLine ("You new copse of trees, " + copseName + ", has been planted");
							Console.WriteLine ("\n=================================================\n");
							Console.ResetColor ();
						} else
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine ("Unable to plant copse because the height of the seed point\n is not within the specified height limits!");
							Console.ResetColor ();
						}
					} 
					else
					{
						MainConsole.Instance.Info ( "[TREES]: Copse " + copseName + " has already been planted?" );
					}
				}
			}
		}

		private void ListExistingCopse()
		{
			foreach ( Copse cp in m_copse )
			{
				MainConsole.Instance.Info (
					string.Format ("[TREES]: Copse {0}: {1} {2} trees at {3}, {4} ;  {5}", cp.m_name, cp.m_trees.Count, GetTreeName ((int) cp.m_tree_type), cp.m_seed_point.X, cp.m_seed_point.Y, (cp.m_frozen ? "Frozen" : "Growing")));
			}
		}

		/// <summary>
		/// Interactive tree configuration.
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
		private void HandleTreeConfigure(IScene scene, string[] cmd)
		{
            if ( !TreeCheckValidRegion(m_scene) ) {
				return;
			}

            bool advancedOptions = false;
            if ( cmd.Count() == 3 ) {
                if (cmd[2].ToLower() == "true") 
                    advancedOptions = true;
			}
            			
			string resp = "no";
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\n\n************* Tree Setup *************");
			Console.WriteLine (
				"\n   This setup will interactively generate a new tree copse" +
				"\n     definition for this region.\n");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.ResetColor();
			resp = ReadLine("Do you want to setup a new copse of trees now?", resp).ToLower();

			if ( resp[0] != 'y' )
			{
				return;
			}

			// here we go... set some defaults for ease of use
			Copse newCopse = new Copse();

			newCopse.m_name = "Mycopse";
			newCopse.m_frozen = false;
			newCopse.m_tree_quantity = 25;
			newCopse.m_treeline_high = 35;
			newCopse.m_treeline_low = 20;
			newCopse.m_range = 50;
			newCopse.m_tree_type = Tree.Pine1;
			newCopse.m_seed_point = new Vector3(128,128,0);
			newCopse.m_initial_scale = new Vector3(4,4,4);
			newCopse.m_maximum_scale = new Vector3(15,15,15);
			newCopse.m_rate = new Vector3((float) 0.01,(float) 0.01,(float)0.01); 
			newCopse.m_planted = false;
			newCopse.m_trees = new List<UUID>();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("\n\n");
			Console.WriteLine("====================================================================");
			Console.WriteLine("================== Tree Ssettings =====================");
			Console.WriteLine("====================================================================");
			Console.WriteLine("\n\n");
			Console.ResetColor();

            bool nameCheckOk = true;
			do
			{
                newCopse.m_name = ReadLine ("What will you call this copse of trees ('quit' to abort) ?", newCopse.m_name);
                nameCheckOk = true;         // assume name is ok

                foreach ( Copse cp in m_copse )
				{
					if ( cp.m_name == newCopse.m_name )
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine ("This copse name (" + newCopse.m_name + ") already exists");
			 			Console.ResetColor ();
						nameCheckOk = false;
					}
				}
			} while (!nameCheckOk);
			
			if ( newCopse.m_name.ToLower() == "quit" )		// provide an "out"
			{
				return;
			}

			GetCopseDefinitions (newCopse, advancedOptions);
            m_copse.Add(newCopse);

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Your new copse of trees has been successfully configured");
			Console.ResetColor();

			// plant them now?
			string plantNow;
			plantNow = ReadLine("Do you want to plant this copse now?", "no");
			if( plantNow[0] == 'y' )
			{
				PlantTreeCopse (newCopse.m_name);
			}	

            if ( !m_active_trees )
			{
				resp = "yes";
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.ResetColor();
                resp = ReadLine( "Do you want to activate tree growth now?", resp ).ToLower();

                if ( resp[0] == 'y' ) 
				{
                    MainConsole.Instance.Info ( "[TREES]: Activating Trees" );
					activeizeTreeze( true );
				}
			}
		}


		/// <summary>
		/// Re-configure a copse definition.
		/// </summary>
		/// <param name="scene">Scene.</param>
		/// <param name="cmd">Cmd.</param>
		private void HandleTreeUpdate(IScene scene, string[] cmd)
		{
			if ( !TreeCheckValidRegion(m_scene) ) {
				return;
			}

			bool advancedOptions = false;
			if ( cmd.Count() == 3 ) {
				advancedOptions = Boolean.Parse( cmd[2] );
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\n\n************* Tree Updater *************");
			Console.WriteLine (
				"\n   This will update an existing copse definition for this region.\n Type 'quit' to exit");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.ResetColor();

			var updateCopse = new Copse();
			updateCopse.m_name = "?";

			bool nameCheckOk = false;
			do
			{
			updateCopse.m_name = ReadLine ("Which copse would you like to update?", updateCopse.m_name);
			if ( updateCopse.m_name[0] == '?' )
				{
					ListExistingCopse();
					continue;
				}

				// provide an "out"
				if (updateCopse.m_name.ToLower () == "quit")
					return;

				// check for definition
				foreach ( Copse cp in m_copse )
				{
				if ( cp.m_name == updateCopse.m_name )
					{
                        // found it.. grab details
						nameCheckOk = true;
						updateCopse = cp;

						break;
					}
				}

				// not found ?
				if ( !nameCheckOk )
				{
					Console.ForegroundColor = ConsoleColor.Red;
				    Console.WriteLine ("The copse name (" + updateCopse.m_name + ") does not exist. ('?' for list)");
					Console.ResetColor ();
				}

			} while (!nameCheckOk);


			// disable activity if necessary
			bool oldState = m_active_trees;
			if ( oldState )
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine( "Disabling growth whilst re-configuring");
				Console.ResetColor();

				this.activeizeTreeze( false );
			}

			GetCopseDefinitions (updateCopse, advancedOptions);

			// update this definition
			foreach (Copse cp in m_copse)
			{
			if (cp.m_name == updateCopse.m_name)
				{

					// cp.m_name = updateCopse.m_name;
					// cp.m_frozen = false;
					cp.m_tree_quantity = updateCopse.m_tree_quantity;
					cp.m_treeline_high = updateCopse.m_treeline_high;
					cp.m_treeline_low = updateCopse.m_treeline_low;
					cp.m_range = updateCopse.m_range;
					cp.m_tree_type = updateCopse.m_tree_type;
					cp.m_seed_point = updateCopse.m_seed_point;
					cp.m_initial_scale = updateCopse.m_initial_scale;
					cp.m_maximum_scale = updateCopse.m_maximum_scale;
					cp.m_rate = updateCopse.m_rate; 
					//cp.m_planted = false;
					//cp.m_trees = new List<UUID>();

					break;
				}
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Your new copse of trees has been successfully configured");
			Console.ResetColor();

			// need to plant them now?
			if (!updateCopse.m_planted )
			{
				string plantNow;
				plantNow = ReadLine("Do you want to plant this copse now?", "no");
				if(plantNow[0] == 'y')
				{
				PlantTreeCopse (updateCopse.m_name);
				}
			}	

			if ( oldState )
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine( "Re-activiating growth");
				Console.ResetColor();

				activeizeTreeze( true );
			}
		}

		/// <summary>
		/// Handles the tree help command.
		/// </summary>
		/// <param name="scene">Not used</param>
		/// <param name="cmd">Not used</param>
        private void HandleTreeHelp( IScene scene, string[] cmd )
        {
            if ( MainConsole.Instance.ConsoleScene != m_scene )
            {
                return;
            }

			MainConsole.Instance.Info(
				"tree active <state> - Change active growing state of the trees module."+
				"\n <state>: The required activity state");
			MainConsole.Instance.Info(
				"tree freeze <copse> - Freeze activity for a defined copse." +
				"\n <copse>: The name of the required copse");
			MainConsole.Instance.Info(
				"tree unfreeze <copse> - Resume activity for a defined copse." +
				"\n <copse>: The name of the required copse");
			MainConsole.Instance.Info(
				"tree load <filename> - Load a copse definition from an xml file." +
				"\n <filename>: The <xml definition> file you wish to load");
			MainConsole.Instance.Info (
				"tree save <copse> <filename> - Save a copse definition to an xml file." +
				"\n <copse> : The name of the required copse" +
				"\n <filename>: The (xml) file you wish to save");
			MainConsole.Instance.Info(
				"tree plant <copse> - Start the planting of a copse (name)." +
				"\n <copse>: The required copse (name)");
			MainConsole.Instance.Info(
				"tree rate <updateRate> - Reset the tree growing update rate (Sec)." +
				"\n <updateRate>: The required update rate (minimum 1 Sec)");
			MainConsole.Instance.Info(
				"tree reload - Reload copse definitions from the in-scene trees.");
			MainConsole.Instance.Info(
				"tree remove <copse> - Remove a copse definition and all its in-scene trees." +
				"\n <copse>: The name of the required copse");
			MainConsole.Instance.Info(
				"tree stats - Log statistics about the trees.");
			MainConsole.Instance.Info(
					"tree configure - Interactive configuration of trees.");
			MainConsole.Instance.Info(
				"tree update - Update an existing copse configuration.");
        }

		/// <summary>
		/// Adds the console commands.
		/// </summary>
		private void AddConsoleCommands()
        {
            if (MainConsole.Instance != null)
            {
                MainConsole.Instance.Commands.AddCommand (
                    "tree active",
					"tree active <state>", 
					"Change active growing state of the trees module.\n <state>: The required activity state",
					HandleTreeActive,
                    true,
                    false);
                MainConsole.Instance.Commands.AddCommand (
                    "tree freeze",
                    "tree freeze <copse> <freeze>",
					"Freeze growing activity for a defined copse.\n <copse> : The name of the required copse",
					HandleTreeFreeze,
                    true,
                    false);
				MainConsole.Instance.Commands.AddCommand (
                    "tree unfreeze",
					"tree unfreeze <copse>",
					"Resume growing activity for a defined copse.\n <copse> : The name of the required copse",
					HandleTreeUnFreeze,
                    true,
                    false);
                MainConsole.Instance.Commands.AddCommand (
                    "tree load",
                    "tree load <filename>", 
					"Load a copse definition file.\n <filename>: The (.copse) file you wish to load",
					HandleTreeLoad,
                    true,
                    false);
				MainConsole.Instance.Commands.AddCommand (
                    "tree save",
					"tree save <copse> <filename>", 
					"Save a copse definition to a file.\n <copse> : The name of the required copse\n <filename>: The (.copse) file you wish to save",
					HandleTreeSave,
                    true,
                    false);
                MainConsole.Instance.Commands.AddCommand (
                    "tree plant",
                    "tree plant [copse]", 
					"Start the planting on a copse.\n <copse>: The name of the required copse",
					HandleTreePlant,
                    true,
                    false);
                MainConsole.Instance.Commands.AddCommand (
                    "tree rate",
                    "tree rate <updateRate>",
					"Reset the tree growing update rate (Sec).\n <updateRate>: The required update rate (minimum 1 Sec)",
					HandleTreeRate,
                    true,
                    false);
                MainConsole.Instance.Commands.AddCommand (
                    "tree reload",
					"tree reload", 
					"Reload copse definitions from the in-scene trees.",
					HandleTreeReload,
                    true,
                    false);
				MainConsole.Instance.Commands.AddCommand (
                    "tree remove",
                    "tree remove [copse]",
					"Remove a copse definition and all its in-scene trees.\n <copse>: The name of the required copse",
					HandleTreeRemove,
                    true,
                    false);
				MainConsole.Instance.Commands.AddCommand (
                    "tree stats",
					"tree stats",
					"Log statistics about the trees.",
					HandleTreeStatistics,
                    true,
                    false);
                MainConsole.Instance.Commands.AddCommand (
                    "tree help",
					"tree help",
					"Help about the trees command.",
					HandleTreeHelp,
                    true,
                    false);
				MainConsole.Instance.Commands.AddCommand (
                    "tree configure",
					"tree configure [advanced]",
                    "Interactive configuration of trees for the region.\n [advanced]: optional - allows entry of initial size, maximum size and growth rate",
					HandleTreeConfigure,
                    true,
                    false);
				MainConsole.Instance.Commands.AddCommand (
					"tree update",
					"tree update [advanced]",
					"Interactive update and existing copse configuration for the region.\n [advanced]: optional - allows entry of initial size, maximum size and growth rate",
					HandleTreeUpdate,
					true,
					false);
            }
        }

        #endregion

        #region IVegetationModule Members

        /// <summary>
        /// Add a new tree to the scene. Used by other modules.
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="groupID"></param>
        /// <param name="scale"></param>
        /// <param name="rotation"></param>
        /// <param name="position"></param>
        /// <param name="treeType"></param>
        /// <param name="newTree"></param>
        /// <returns></returns>
        public ISceneEntity AddTree (
            UUID uuid, UUID groupID, Vector3 scale, Quaternion rotation, Vector3 position, Tree treeType, bool newTree )
        {
            PrimitiveBaseShape treeShape = new PrimitiveBaseShape();
            treeShape.PathCurve = 16;
            treeShape.PathEnd = 49900;
            treeShape.PCode = newTree ? (byte)PCode.NewTree : (byte)PCode.Tree;
            treeShape.Scale = scale;
            treeShape.State = (byte)treeType;

            return m_scene.SceneGraph.AddNewPrim( uuid, groupID, position, rotation, treeShape );
        }

        #endregion

        #region IEntityCreator Members

        /// <summary>
        /// The creation capabilities.
        /// </summary>
        protected static readonly PCode[] creationCapabilities = new PCode[] { PCode.NewTree, PCode.Tree };

        /// <summary>
        /// The entities that this class is capable of creating. These match the PCode format.
        /// </summary>
        /// <returns></returns>
        /// <value>The creation capabilities.</value>
        public PCode[] CreationCapabilities { get { return creationCapabilities; } }

        /// <summary>
        /// Create a tree entity
        /// </summary>
        /// <param name="baseEntity"></param>
        /// <param name="ownerID"></param>
        /// <param name="groupID"></param>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="shape"></param>
        /// <returns>The tree entity created, or null if the creation failed</returns>
        /// <param name="sceneObject">Scene object.</param>
        public ISceneEntity CreateEntity(
            ISceneEntity sceneObject, UUID ownerID, UUID groupID, Vector3 pos, Quaternion rot, PrimitiveBaseShape shape)
        {
            if ( Array.IndexOf( creationCapabilities, ( PCode)shape.PCode ) < 0 )
            {
                MainConsole.Instance.Debug ( "[VEGETATION]: PCode " + shape.PCode + " not handled by "+ Name );
                return null;
            }

            ISceneChildEntity rootPart = sceneObject.GetChildPart(sceneObject.UUID);

            rootPart.AddFlag(PrimFlags.Phantom);
            if ( rootPart.Shape.PCode != (byte)PCode.Grass )
            {
                // Tree size has to be adapted depending on its type
                switch ( (Tree)rootPart.Shape.State )
                {
                    case Tree.Cypress1:
                    case Tree.Cypress2:
                    case Tree.Palm1:
                    case Tree.Palm2:
                    case Tree.WinterAspen:
                        rootPart.Scale = new Vector3(4, 4, 10);
                        break;
                    case Tree.WinterPine1:
                    case Tree.WinterPine2:
                        rootPart.Scale = new Vector3(4, 4, 20);
                        break;

                    case Tree.Dogwood:
                        rootPart.Scale = new Vector3(6.5f, 6.5f, 6.5f);
                        break;

                    // case... other tree types
                    // tree.Scale = new Vector3(?, ?, ?);
                    // break;
                    default:
                        rootPart.Scale = new Vector3(4, 4, 4);
                        break;
                }
            }

            sceneObject.SetGroup( groupID, UUID.Zero, false );
            m_scene.SceneGraph.AddPrimToScene( sceneObject );
            sceneObject.ScheduleGroupUpdate( PrimUpdateFlags.ForcedFullUpdate );

            return sceneObject;
        }

        #endregion
        //--------------------------------------------------------------

        #region Tree Utilities
        /// <summary>
        /// Serializes a copse definition.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="obj">Object.</param>
        static public void SerializeObject(string fileName, Object obj)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer( typeof( Copse ) );

                using ( XmlTextWriter writer = new XmlTextWriter( fileName, Util.UTF8 ) )
                {
                    writer.Formatting = Formatting.Indented;
                    xs.Serialize(writer, obj);
                }
            }
            catch ( SystemException ex )
            {
                throw new ApplicationException( "Unexpected failure in Tree serialization", ex );
            }
        }

        /// <summary>
        /// Deserializes the copse definition from the supplied filename.
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="fileName">File name.</param>
        static public object DeserializeObject( string fileName )
        {
            try
            {
                XmlSerializer xs = new XmlSerializer( typeof( Copse ) );

                using ( FileStream fs = new FileStream( fileName, FileMode.Open, FileAccess.Read ) )
                {
                    return xs.Deserialize(fs);
                }
            }
            catch (SystemException ex)
            {
                throw new ApplicationException( "Unexpected failure in Tree de-serialization", ex );
            }
        }

        /// <summary>
        /// Reloads the copse definitions.
        /// </summary>
        private void ReloadCopse()
        {
            this.m_copse = new List<Copse>();

            ISceneEntity[] objs = m_scene.Entities.GetEntities ();
            foreach( ISceneEntity grp in objs )
            {
                if( grp.Name.Length > 5 && ( grp.Name.Substring(0, 5) == "ATPM:" || grp.Name.Substring(0, 5) == "FTPM:" ) )
                {
                    // Create a new copse definition or add uuid to an existing definition
                    try
                    {
                        bool copsefound = false;
                        Copse copse = new Copse( grp.Name );

                        foreach( Copse cp in m_copse )
                        {
                            if( cp.m_name == copse.m_name )
                            {
                                // MainConsole.Instance.Info ("[TREES]: Found tree "+ grp.UUID);
                                copsefound = true;
                                cp.m_trees.Add( grp.UUID );
                            }
                        }

                        if( !copsefound )
                        {
                            m_copse.Add( copse );
                            copse.m_trees.Add(grp.UUID);
                            MainConsole.Instance.Info ( "[TREES]: Found copse "+ copse.m_name + " at "+
                                copse.m_seed_point.X+", "+copse.m_seed_point.Y );
						}
                    }
                    catch
                    {
                        MainConsole.Instance.Info ( "[TREES]: Ill formed copse definition "+grp.Name+" - ignoring" );
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Start or stop growing trees.
        /// </summary>
        /// <param name="activeYN">If set to <c>true</c> active.</param>
        private void activeizeTreeze( bool activeYN )
        {
            if ( activeYN )
            {
                CalculateTrees = new Timer(m_update_ms);
                CalculateTrees.Elapsed += CalculateTrees_Elapsed;
                CalculateTrees.Start();
            }
            else 
            {
                CalculateTrees.Stop();
            }
			// save curent state
			m_active_trees = activeYN;
        } 

        /// <summary>
        /// Grows the trees in a copse.
        /// </summary>
        private void growTrees()
        {
            foreach ( Copse copse in m_copse )
            {
                if ( !copse.m_frozen )
                {
                    foreach ( UUID tree in copse.m_trees )
                    {
                        IEntity ent;
                        if ( m_scene.Entities.TryGetValue(tree, out ent) )
                        {
                            ISceneChildEntity s_tree = ( (ISceneEntity)ent ).RootChild;

                            if ( s_tree.Scale.X < copse.m_maximum_scale.X && s_tree.Scale.Y < copse.m_maximum_scale.Y && s_tree.Scale.Z < copse.m_maximum_scale.Z )
                            {
                                s_tree.Scale += copse.m_rate;
                                s_tree.ParentEntity.HasGroupChanged = true;
                                s_tree.ScheduleUpdate(PrimUpdateFlags.FindBest);
                            }
                        }
                        else
                        {
                            MainConsole.Instance.Debug ( "[TREES]: Tree not in scene "+ tree );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Seeds the trees in a copse.
        /// </summary>
        private void seedTrees()
        {
            foreach ( Copse copse in m_copse )
            {
                if ( !copse.m_frozen )
                {
                    foreach ( UUID tree in copse.m_trees )
                    {
                        IEntity entity;
                        if( m_scene.Entities.TryGetValue( tree, out entity ) && entity is ISceneEntity )
                        {
                            ISceneChildEntity s_tree = ((ISceneEntity)entity).RootChild;

							if (copse.m_trees.Count < copse.m_tree_quantity)
                            {
                                // Tree has grown enough to seed if it has grown by at least 25% of seeded to full grown height
                                if (s_tree.Scale.Z > ( copse.m_initial_scale.Z + (copse.m_maximum_scale.Z - copse.m_initial_scale.Z) / 4.0) ) 
                                {
                                    if ( Util.RandomClass.NextDouble() > 0.75 )
                                    {
                                        SpawnChild( copse, s_tree );
                                    }
                                }
                            }
                        }
                        else
                        {
                            MainConsole.Instance.Debug ( "[TREES]: Tree not in scene "+ tree );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Kills trees from a copse.
        /// </summary>
        private void killTrees()
        {
            foreach ( Copse copse in m_copse )
            {
                if ( !copse.m_frozen && copse.m_trees.Count >= copse.m_tree_quantity )
                {
                    List<ISceneEntity> groups = new List<ISceneEntity>();
                    foreach ( UUID tree in copse.m_trees )
                    {
                        double killLikelyhood = 0.0;

                        IEntity entity;
                        if( m_scene.Entities.TryGetValue( tree, out entity ) && entity is ISceneEntity )
                        {
                            ISceneChildEntity selectedTree = ( (ISceneEntity)entity ).RootChild;
                            double selectedTreeScale = Math.Sqrt(Math.Pow(selectedTree.Scale.X, 2) +
                                                                 Math.Pow(selectedTree.Scale.Y, 2) +
                                                                 Math.Pow(selectedTree.Scale.Z, 2));

                            foreach ( UUID picktree in copse.m_trees )
                            {
                                if ( picktree != tree )
                                {
                                    IEntity ent;
                                    if( m_scene.Entities.TryGetValue(tree, out ent) && ent is ISceneEntity )
                                    {
                                        ISceneChildEntity pickedTree = ((ISceneEntity)ent).RootChild;

                                        double pickedTreeScale = Math.Sqrt (Math.Pow (pickedTree.Scale.X, 2) +
                                                                           Math.Pow (pickedTree.Scale.Y, 2) +
                                                                           Math.Pow (pickedTree.Scale.Z, 2));

                                        double pickedTreeDistance = Vector3.Distance (pickedTree.AbsolutePosition, selectedTree.AbsolutePosition);

                                        killLikelyhood += (selectedTreeScale / (pickedTreeScale * pickedTreeDistance)) * 0.1;
                                    }
                                }
                            }

                            if (Util.RandomClass.NextDouble() < killLikelyhood)
                            {
                                groups.Add(selectedTree.ParentEntity);
                                copse.m_trees.Remove(selectedTree.ParentEntity.UUID);

                                break;
                            }
                        }
                        else
                        {
							MainConsole.Instance.Debug ("[TREES]: Tree not in scene "+ tree);
                        }
                    }

                    IBackupModule backup = m_scene.RequestModuleInterface<IBackupModule>();
                    if ( backup != null )
                    {
                        backup.DeleteSceneObjects( groups.ToArray(), true, true );
                    }
                }
            }
        }

		/// <summary>
		/// Spawns a new tree.
		/// </summary>
		/// <param name="copse">Copse.</param>
		/// <param name="s_tree">S_tree.</param>
		private void SpawnChild ( Copse copse, ISceneChildEntity s_tree )
        {
            Vector3 position = new Vector3();

            double randX = ((Util.RandomClass.NextDouble() * 2.0) - 1.0) * (s_tree.Scale.X * 3);
            double randY = ((Util.RandomClass.NextDouble() * 2.0) - 1.0) * (s_tree.Scale.X * 3);

            position.X = s_tree.AbsolutePosition.X + (float)randX;
            position.Y = s_tree.AbsolutePosition.Y + (float)randY;

            if (!(position.X < 0f || position.Y < 0f ||
                position.X > m_scene.RegionInfo.RegionSizeX || position.Y > m_scene.RegionInfo.RegionSizeY) &&
                Util.GetDistanceTo(position, copse.m_seed_point) <= copse.m_range)
            {
                UUID uuid = m_scene.RegionInfo.EstateSettings.EstateOwner;

                CreateTree(uuid, copse, position);
            }
        }

		/// <summary>
		/// Creats (plants) a tree
		/// </summary>
		/// <param name="uuid">UUID.</param>
		/// <param name="copse">Copse.</param>
		/// <param name="position">Position.</param>
		private void CreateTree( UUID uuid, Copse copse, Vector3 position )
        {
            position.Z = m_scene.RequestModuleInterface<ITerrainChannel>()[(int)position.X, (int)position.Y];
            if (position.Z >= copse.m_treeline_low && position.Z <= copse.m_treeline_high)
            {
                SceneObjectGroup tree = (SceneObjectGroup) AddTree(uuid, UUID.Zero, copse.m_initial_scale, Quaternion.Identity, position, copse.m_tree_type, false);

                tree.Name = copse.ToString();
                copse.m_trees.Add(tree.UUID);
                tree.ScheduleGroupUpdate(PrimUpdateFlags.FindBest);
            }
        }

		/// <summary>
		/// Timer call to grow trees.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void CalculateTrees_Elapsed( object sender, ElapsedEventArgs e )
        {
            growTrees();
            seedTrees();
            killTrees();
        }
    }
}
/*
 * Copyright (c) Contributors, http://virtual-planets.org/, http://whitecore-sim.org/, http://aurora-sim.org/, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyrightD
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Virtual Universe Project nor the
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
 */

using System;
using Nini.Config;
using OpenMetaverse;
using Universe.Framework.ConsoleFramework;
using Universe.Framework.Modules;
using Universe.Framework.SceneInfo;
using Universe.Region;

namespace Universe.Physics.BulletSPlugin
{

    public class ExtendedPhysics : INonSharedRegionModule
    {
        static string LogHeader = "[Extended Physics]";

        // =============================================================
        // Since BulletSim is a plugin, this these values aren't defined easily in one place.
        // This table must correspond to an identical table in BSScene.

        // Per scene functions. See BSScene.

        // Per avatar functions. See BSCharacter.

        // Per prim functions. See BSPrim.
        public const string PhysFunctGetLinksetType = "BulletSim.GetLinksetType";
        public const string PhysFunctSetLinksetType = "BulletSim.SetLinksetType";
        public const string PhysFunctChangeLinkFixed = "BulletSim.ChangeLinkFixed";
        public const string PhysFunctChangeLinkType = "BulletSim.ChangeLinkType";
        public const string PhysFunctGetLinkType = "BulletSim.GetLinkType";
        public const string PhysFunctChangeLinkParams = "BulletSim.ChangeLinkParams";
        public const string PhysFunctAxisLockLimits = "BulletSim.AxisLockLimits";

        // =============================================================

        IConfig Configuration { get; set; }
        bool Enabled { get; set; }
        IScene BaseScene { get; set; }
        IScriptModuleComms Comms { get; set; }

        #region INonSharedRegionModule

        public string Name { get { return GetType().Name; } }

        public void Initialize(IConfigSource config)
        {
            BaseScene = null;
            Enabled = false;
            Configuration = null;
            Comms = null;

            try
            {
                if ((Configuration = config.Configs["ExtendedPhysics"]) != null)
                {
                    Enabled = Configuration.GetBoolean("Enabled", Enabled);
                }
            }
            catch (Exception e)
            {
                MainConsole.Instance.ErrorFormat("{0} Initialization error: {0}", LogHeader, e);
            }

            MainConsole.Instance.InfoFormat("{0} module {1} enabled", LogHeader, (Enabled ? "is" : "is not"));
        }

        public void Close()
        {
            if (BaseScene != null)
            {
                // Not implemented yet!
                /*
                BaseScene.EventManager.OnObjectAddedToScene -= EventManager_OnObjectAddedToScene;
                BaseScene.EventManager.OnSceneObjectPartUpdated -= EventManager_OnSceneObjectPartUpdated;
                */
                BaseScene = null;
            }
        }

        public void AddRegion(IScene scene)
        {
        }

        public void RemoveRegion(IScene scene)
        {
            if (BaseScene != null && BaseScene == scene)
            {
                Close();
            }
        }

        public void RegionLoaded(IScene scene)
        {
            if (!Enabled) return;
            BaseScene = scene;
            Comms = BaseScene.RequestModuleInterface<IScriptModuleComms>();
            if (Comms == null)
            {
                MainConsole.Instance.WarnFormat("{0} ScriptModuleComms interface not defined", LogHeader);
                Enabled = false;
                return;
            }

            // Not implemented Yet!
            // Register as LSL functions all the [ScriptInvocation] marked methods.
            //Comms.RegisterScriptInvocations(this);
            //Comms.RegisterConstants(this);

            // Not implemented Yet!
            // When an object is modifed, we might need to update its extended physics Parameters
            //BaseScene.EventManager.OnObjectBeingAddedToScene += Eventmanager_OnObjectAddedToScene;
            //BaseScene.EventManager.OnSceneObjectPartUpdated += EventManager_OnSceneObjectPartUpdated;
        }

        public Type ReplaceableInterface { get { return null; } }

        #endregion // INonSharedRegionModule

        void EventManager_OnObjectAddedToScene(SceneObjectGroup obj)
        {
        }

        // Event generated when some property of a prim changes.
        void EventManager_OnSceneObjectPartUpdated(SceneObjectPart sop, bool isFullUpdate)
        {
        }

        [ScriptConstant]
        public const int PHYS_CENTER_OF_MASS =     1 << 0;

        [ScriptInvocation]
        public string physGetEngineType(UUID hostID, UUID scriptID)
        {
            string ret = string.Empty;

            if (BaseScene.PhysicsScene != null)
            {
                ret = BaseScene.PhysicsScene.EngineType;
            }

            return ret;
        }

        // Code for specifying params.
        // The choice if 14700 is arbitrary and only serves to catch parameter code misuse.
        [ScriptConstant]
        public const int PHYS_AXIS_LOCK_LINEAR     = 14700;
        [ScriptConstant]
        public const int PHYS_AXIS_LOCK_LINEAR_X   = 14701;
        [ScriptConstant]
        public const int PHYS_AXIS_LIMIT_LINEAR_X  = 14702;
        [ScriptConstant]
        public const int PHYS_AXIS_LOCK_LINEAR_Y   = 14703;
        [ScriptConstant]
        public const int PHYS_AXIS_LIMIT_LINEAR_Y  = 14704;
        [ScriptConstant]
        public const int PHYS_AXIS_LOCK_LINEAR_Z   = 14705;
        [ScriptConstant]
        public const int PHYS_AXIS_LIMIT_LINEAR_Z  = 14706;
        [ScriptConstant]
        public const int PHYS_AXIS_LOCK_ANGULAR    = 14707;
        [ScriptConstant]
        public const int PHYS_AXIS_LOCK_ANGULAR_X  = 14708;
        [ScriptConstant]
        public const int PHYS_AXIS_LIMIT_ANGULAR_X = 14709;
        [ScriptConstant]
        public const int PHYS_AXIS_LOCK_ANGULAR_Y  = 14710;
        [ScriptConstant]
        public const int PHYS_AXIS_LIMIT_ANGULAR_Y = 14711;
        [ScriptConstant]
        public const int PHYS_AXIS_LOCK_ANGULAR_Z  = 14712;
        [ScriptConstant]
        public const int PHYS_AXIS_LIMIT_ANGULAR_Z = 14713;
        [ScriptConstant]
        public const int PHYS_AXIS_UNLOCK_LINEAR   = 14714;
        [ScriptConstant]
        public const int PHYS_AXIS_UNLOCK_LINEAR_X = 14715;
        [ScriptConstant]
        public const int PHYS_AXIS_UNLOCK_LINEAR_Y = 14716;
        [ScriptConstant]
        public const int PHYS_AXIS_UNLOCK_LINEAR_Z = 14717;
        [ScriptConstant]
        public const int PHYS_AXIS_UNLOCK_ANGULAR  = 14718;
        [ScriptConstant]
        public const int PHYS_AXIS_UNLOCK_ANGULAR_X = 14719;
        [ScriptConstant]
        public const int PHYS_AXIS_UNLOCK_ANGULAR_Y = 14720;
        [ScriptConstant]
        public const int PHYS_AXIS_UNLOCK_ANGULAR_Z = 14721;
        [ScriptConstant]
        public const int PHYS_AXIS_UNLOCK           = 14722;
        [ScriptConstant]
        public const int PHYS_LINK_TYPE_FIXED  = 1234;
        [ScriptConstant]
        public const int PHYS_LINK_TYPE_HINGE  = 4;
        [ScriptConstant]
        public const int PHYS_LINK_TYPE_SPRING = 9;
        [ScriptConstant]
        public const int PHYS_LINK_TYPE_6DOF   = 6;
        [ScriptConstant]
        public const int PHYS_LINK_TYPE_SLIDER = 7;

        // Code for specifying params.
        // The choice if 14400 is arbitrary and only serves to catch parameter code misuse.
        public const int PHYS_PARAM_MIN                    = 14401;

        [ScriptConstant]
        public const int PHYS_PARAM_FRAMEINA_LOC           = 14401;
        [ScriptConstant]
        public const int PHYS_PARAM_FRAMEINA_ROT           = 14402;
        [ScriptConstant]
        public const int PHYS_PARAM_FRAMEINB_LOC           = 14403;
        [ScriptConstant]
        public const int PHYS_PARAM_FRAMEINB_ROT           = 14404;
        [ScriptConstant]
        public const int PHYS_PARAM_LINEAR_LIMIT_LOW       = 14405;
        [ScriptConstant]
        public const int PHYS_PARAM_LINEAR_LIMIT_HIGH      = 14406;
        [ScriptConstant]
        public const int PHYS_PARAM_ANGULAR_LIMIT_LOW      = 14407;
        [ScriptConstant]
        public const int PHYS_PARAM_ANGULAR_LIMIT_HIGH     = 14408;
        [ScriptConstant]
        public const int PHYS_PARAM_USE_FRAME_OFFSET       = 14409;
        [ScriptConstant]
        public const int PHYS_PARAM_ENABLE_TRANSMOTOR      = 14410;
        [ScriptConstant]
        public const int PHYS_PARAM_TRANSMOTOR_MAXVEL      = 14411;
        [ScriptConstant]
        public const int PHYS_PARAM_TRANSMOTOR_MAXFORCE    = 14412;
        [ScriptConstant]
        public const int PHYS_PARAM_CFM                    = 14413;
        [ScriptConstant]
        public const int PHYS_PARAM_ERP                    = 14414;
        [ScriptConstant]
        public const int PHYS_PARAM_SOLVER_ITERATIONS      = 14415;
        [ScriptConstant]
        public const int PHYS_PARAM_SPRING_AXIS_ENABLE     = 14416;
        [ScriptConstant]
        public const int PHYS_PARAM_SPRING_DAMPING         = 14417;
        [ScriptConstant]
        public const int PHYS_PARAM_SPRING_STIFFNESS       = 14418;
        [ScriptConstant]
        public const int PHYS_PARAM_LINK_TYPE              = 14419;
        [ScriptConstant]
        public const int PHYS_PARAM_USE_LINEAR_FRAMEA      = 14420;
        [ScriptConstant]
        public const int PHYS_PARAM_SPRING_EQUILIBRIUM_POINT = 14421;

        public const int PHYS_PARAM_MAX                    = 14421;

        // Used when specifying a parameter that has settings for the three linear and three angular axis
        [ScriptConstant]
        public const int PHYS_AXIS_ALL = -1;
        [ScriptConstant]
        public const int PHYS_AXIS_LINEAR_ALL = -2;
        [ScriptConstant]
        public const int PHYS_AXIS_ANGULAR_ALL = -3;
        [ScriptConstant]
        public const int PHYS_AXIS_LINEAR_X  = 0;
        [ScriptConstant]
        public const int PHYS_AXIS_LINEAR_Y  = 1;
        [ScriptConstant]
        public const int PHYS_AXIS_LINEAR_Z  = 2;
        [ScriptConstant]
        public const int PHYS_AXIS_ANGULAR_X = 3;
        [ScriptConstant]
        public const int PHYS_AXIS_ANGULAR_Y = 4;
        [ScriptConstant]
        public const int PHYS_AXIS_ANGULAR_Z = 5;
    }
}
/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using VirtualUniverse.Framework;
using VirtualUniverse.Framework.ClientInterfaces;
using VirtualUniverse.Framework.SceneInfo;
using VirtualUniverse.Framework.SceneInfo.Entities;
using VirtualUniverse.Framework.Utilities;
using OpenMetaverse;
using OpenMetaverse.StructuredData;
using System.Collections.Generic;

namespace VirtualUniverse.ScriptEngine.VirtualScript
{
    public class ScriptStateSave
    {
        private ScriptEngine m_module;
        private object StateSaveLock = new object();

        public void Initialize(ScriptEngine module)
        {
            m_module = module;
        }

        public void AddScene(IScene scene)
        {
            scene.VirtualUniverseEventManager.RegisterEventHandler("DeleteToInventory", VirtualUniverseEventManager_OnGenericEvent);
        }

        public void Close()
        {
        }

        private object VirtualUniverseEventManager_OnGenericEvent(string FunctionName, object parameters)
        {
            if (FunctionName == "DeleteToInventory")
            {
                //Resave all the state saves for this object
                ISceneEntity entity = (ISceneEntity)parameters;
                foreach (ISceneChildEntity child in entity.ChildrenEntities())
                {
                    m_module.SaveStateSaves(child.UUID);
                }
            }
            return null;
        }

        public void SaveStateTo(ScriptData script)
        {
            SaveStateTo(script, false);
        }

        public void SaveStateTo(ScriptData script, bool forced)
        {
            SaveStateTo(script, forced, true);
        }

        public void SaveStateTo(ScriptData script, bool forced, bool saveBackup)
        {
            if (!forced)
            {
                if (script.Script == null)
                    return; //If it didn't compile correctly, this happens
                if (!script.Script.NeedsStateSaved)
                    return; //If it doesn't need a state save, don't save one
            }
            if (script.Script != null)
                script.Script.NeedsStateSaved = false;
            StateSave stateSave = new StateSave
            {
                State = script.State,
                ItemID = script.ItemID,
                Running = script.Running,
                MinEventDelay = script.EventDelayTicks,
                Disabled = script.Disabled,
                UserInventoryID = script.UserInventoryItemID,
                AssemblyName = script.AssemblyName,
                Compiled = script.Compiled,
                Source = script.Source == null ? "" : script.Source
            };
            //Allow for the full path to be put down, not just the assembly name itself
            if (script.InventoryItem != null)
            {
                stateSave.PermsGranter = script.InventoryItem.PermsGranter;
                stateSave.PermsMask = script.InventoryItem.PermsMask;
            }
            else
            {
                stateSave.PermsGranter = UUID.Zero;
                stateSave.PermsMask = 0;
            }
            stateSave.TargetOmegaWasSet = script.TargetOmegaWasSet;

            //Vars
            Dictionary<string, object> vars = new Dictionary<string, object>();
            if (script.Script != null)
                vars = script.Script.GetStoreVars();
            stateSave.Variables = XMLUtils.BuildXmlResponse(vars);

            //Plugins
            stateSave.Plugins =
                OSDParser.SerializeJsonString(m_module.GetSerializationData(script.ItemID, script.Part.UUID));

            lock (StateSaveLock)
                script.Part.StateSaves[script.ItemID] = stateSave;
            if (saveBackup)
                script.Part.ParentEntity.HasGroupChanged = true;
        }

        public void Deserialize(ScriptData instance, StateSave save)
        {
            instance.State = save.State;
            instance.Running = save.Running;
            instance.EventDelayTicks = (long)save.MinEventDelay;
            instance.AssemblyName = save.AssemblyName;
            instance.Disabled = save.Disabled;
            instance.UserInventoryItemID = save.UserInventoryID;
            if (save.Plugins != "")
                instance.PluginData = (OSDMap)OSDParser.DeserializeJson(save.Plugins);
            m_module.CreateFromData(instance.Part.UUID, instance.ItemID, instance.Part.UUID,
                                    instance.PluginData);
            instance.Source = save.Source;
            instance.InventoryItem.PermsGranter = save.PermsGranter;
            instance.InventoryItem.PermsMask = save.PermsMask;
            instance.TargetOmegaWasSet = save.TargetOmegaWasSet;

            if (save.Variables != null && instance.Script != null)
                instance.Script.SetStoreVars(XMLUtils.ParseXmlResponse(save.Variables));
        }

        public StateSave FindScriptStateSave(ScriptData script)
        {
            lock (StateSaveLock)
            {
                StateSave save;
                if (!script.Part.StateSaves.TryGetValue(script.ItemID, out save))
                {
                    if (!script.Part.StateSaves.TryGetValue(script.InventoryItem.OldItemID, out save))
                    {
                        if (!script.Part.StateSaves.TryGetValue(script.InventoryItem.ItemID, out save))
                        {
                            return null;
                        }
                    }
                }
                return save;
            }
        }

        public void DeleteFrom(ScriptData script)
        {
            bool changed = false;
            lock (StateSaveLock)
            {
                //if we did remove something, resave it
                if (script.Part.StateSaves.Remove(script.ItemID))
                    changed = true;
                if (script.Part.StateSaves.Remove(script.InventoryItem.OldItemID))
                    changed = true;
                if (script.Part.StateSaves.Remove(script.InventoryItem.ItemID))
                    changed = true;
            }
            if (changed)
                script.Part.ParentEntity.HasGroupChanged = true;
        }

        public void DeleteFrom(ISceneChildEntity Part, UUID ItemID)
        {
            lock (StateSaveLock)
            {
                //if we did remove something, resave it
                if (Part.StateSaves.Remove(ItemID))
                    Part.ParentEntity.HasGroupChanged = true;
            }
        }
    }
}
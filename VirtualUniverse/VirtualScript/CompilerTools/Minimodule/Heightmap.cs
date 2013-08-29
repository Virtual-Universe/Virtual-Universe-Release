/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using VirtualUniverse.Framework;
using VirtualUniverse.Framework.Modules;
using VirtualUniverse.Framework.SceneInfo;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    public class Heightmap : MarshalByRefObject, IHeightmap
    {
        private readonly IScene m_scene;

        public Heightmap(IScene scene)
        {
            m_scene = scene;
        }

        #region IHeightmap Members

        public float this[int x, int y]
        {
            get { return Get(x, y); }
            set { Set(x, y, value); }
        }

        public int Length
        {
            get { return m_scene.RequestModuleInterface<ITerrainChannel>().Height; }
        }

        public int Width
        {
            get { return m_scene.RequestModuleInterface<ITerrainChannel>().Width; }
        }

        #endregion

        protected float Get(int x, int y)
        {
            return m_scene.RequestModuleInterface<ITerrainChannel>()[x, y];
        }

        protected void Set(int x, int y, float val)
        {
            m_scene.RequestModuleInterface<ITerrainChannel>()[x, y] = val;
        }
    }
}
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
using VirtualUniverse.Framework.SceneInfo;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    internal class Host : MarshalByRefObject, IHost
    {
        private readonly IExtension m_extend;
        private readonly IGraphics m_graphics;
        private readonly IObject m_obj;
        private readonly MicroScheduler m_threader = new MicroScheduler();
        //private Scene m_scene;

        public Host(IObject m_obj, IScene m_scene, IExtension m_extend)
        {
            this.m_obj = m_obj;
            this.m_extend = m_extend;

            m_graphics = new Graphics(m_scene);

            m_scene.EventManager.OnFrame += EventManager_OnFrame;
        }

        #region IHost Members

        public IObject Object
        {
            get { return m_obj; }
        }

        public IGraphics Graphics
        {
            get { return m_graphics; }
        }

        public IExtension Extensions
        {
            get { return m_extend; }
        }

        public IMicrothreader Microthreads
        {
            get { return m_threader; }
        }

        #endregion

        private void EventManager_OnFrame()
        {
            m_threader.Tick(1000);
        }
    }
}

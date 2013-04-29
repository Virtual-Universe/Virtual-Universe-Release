using VirtualUniverse.Simulation.Base;

namespace VirtualUniverse
{
    /// <summary>
    /// Starting class for the VirtualUniverse Region
    /// </summary>
    public class Application
    {
        public static void Main(string[] args)
        {
            BaseApplication.BaseMain(args, "", new SimulationBase());
        }
    }
}
/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    /// <summary>
    ///     This implements an interface similar to that provided by physics engines to OpenSim internally.
    ///     Eg, PhysicsActor. It is capable of setting and getting properties related to the current
    ///     physics scene representation of this object.
    /// </summary>
    public interface IObjectPhysics
    {
        bool Enabled { get; set; }

        bool Phantom { get; set; }
        bool PhantomCollisions { get; set; }

        double Density { get; set; }
        double Mass { get; set; }
        double Buoyancy { get; set; }

        Vector3 GeometricCenter { get; }
        Vector3 CenterOfMass { get; }

        Vector3 RotationalVelocity { get; set; }
        Vector3 Velocity { get; set; }
        Vector3 Torque { get; set; }
        Vector3 Acceleration { get; }
        Vector3 Force { get; set; }

        bool FloatOnWater { set; }

        void AddForce(Vector3 force, bool pushforce);
        void AddAngularForce(Vector3 force, bool pushforce);
    }
}
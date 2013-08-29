/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    public enum SculptType
    {
        Default = 1,
        Sphere = 1,
        Torus = 2,
        Plane = 3,
        Cylinder = 4
    }

    public enum HoleShape
    {
        Default = 0x00,
        Circle = 0x10,
        Square = 0x20,
        Triangle = 0x30
    }

    public enum PrimType
    {
        NotPrimitive = 255,
        Box = 0,
        Cylinder = 1,
        Prism = 2,
        Sphere = 3,
        Torus = 4,
        Tube = 5,
        Ring = 6,
        Sculpt = 7
    }

    public interface IObjectShape
    {
        UUID SculptMap { get; set; }
        SculptType SculptType { get; set; }

        HoleShape HoleType { get; set; }
        Double HoleSize { get; set; }
        PrimType PrimType { get; set; }
    }
}
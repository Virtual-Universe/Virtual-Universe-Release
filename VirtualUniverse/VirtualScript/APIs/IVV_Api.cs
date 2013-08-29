/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using key = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLString;
using LSL_Float = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLFloat;
using LSL_Integer = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLInteger;
using LSL_Key = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLString;
using LSL_List = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.list;
using LSL_String = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLString;
using rotation = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.Quaternion;

namespace VirtualScript.ScriptEngine.VirtualScript.APIs.Interfaces
{
    public interface IVV_Api
    {
        void vvSetCloudDensity(LSL_Float density);

        void vvUpdateDatabase(LSL_String key, LSL_String value, LSL_String token);

        LSL_List vvQueryDatabase(LSL_String key, LSL_String token);

        LSL_List vvDeserializeXMLValues(key xmlFile);

        LSL_List vvDeserializeXMLKeys(key xmlFile);

        void vvSetConeOfSilence(LSL_Float radius);

        key vvSerializeXML(LSL_List keys, LSL_List values);

        LSL_String vvGetTeam(LSL_Key id);

        LSL_Float vvGetHealth(LSL_Key id);

        void vvJoinCombat(LSL_Key id);

        void vvLeaveCombat(LSL_Key id);

        void vvJoinCombatTeam(LSL_Key id, LSL_String team);

        void vvRequestCombatPermission(string ID);

        void vvThawAvatar(string ID);

        void vvFreezeAvatar(string ID);

        LSL_List vvGetTeamMembers(LSL_String team);

        LSL_String vvGetLastOwner();

        LSL_String vvGetLastOwner(LSL_String PrimID);

        void vvSayDistance(int channelID, LSL_Float Distance, string text);

        void vvSayTo(string userID, string text);

        LSL_Integer vvGetWalkDisabled(string userID);

        void vvSetWalkDisabled(string userID, bool Value);

        LSL_Integer vvGetFlyDisabled(string userID);

        void vvSetFlyDisabled(string userID, bool Value);

        key vvAvatarFullName2Key(string username);

        void vvRaiseError(string message);

        key vvGetText();

        rotation vvGetTextColor();

        void vvSetEnv(LSL_String name, LSL_List value);

        LSL_Integer vvGetIsInfiniteRegion();

        void vvAllRegionInstanceSay(LSL_Integer channelID, string text);

        LSL_Integer vvWindlightGetSceneIsStatic();

        LSL_Integer vvWindlightGetSceneDayCycleKeyFrameCount();
        LSL_List vvWindlightGetDayCycle();
        LSL_Integer vvWindlightAddDayCycleFrame(LSL_Float dayCyclePosition, int dayCycleFrameToCopy);
        LSL_Integer vvWindlightRemoveDayCycleFrame(int dayCycleFrame);

        LSL_List vvWindlightGetScene(LSL_List rules);
        LSL_List vvWindlightGetScene(int dayCycleKeyFrame, LSL_List rules);

        LSL_Integer vvWindlightSetScene(LSL_List list);
        LSL_Integer vvWindlightSetScene(int dayCycleIndex, LSL_List list);
    }
}

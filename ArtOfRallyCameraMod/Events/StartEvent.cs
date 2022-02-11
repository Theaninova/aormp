// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using HarmonyLib;

namespace ArtOfRallyMultiplayerMod.Events
{
    [HarmonyPatch(typeof(StageSceneManager), nameof(StageSceneManager.StartEvent))]
    public class StartEvent
    {
        public static void Postfix(StageSceneManager __instance)
        {
        }
    }
}
using HarmonyLib;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace ArtOfRallyMultiplayerMod.Control
{
    [HarmonyPatch(typeof(SettingsSelectable), "SetIndex")]
    public class DefaultGhost
    {
        public static void Postfix(SettingsSelectable __instance, string ___saveConstant)
        {
            if (!Main.Settings.EnableMultiplayer || __instance.settingsType != SettingsSelectable.SettingsType.Ghosts) return;

            var @int = SaveGame.GetInt(___saveConstant, 1);
            SaveGame.SetInt(___saveConstant, __instance.ghostList.IndexOf(@int));
            GhostManager.Instance.SetGhostType(@int);
        }
    }
}
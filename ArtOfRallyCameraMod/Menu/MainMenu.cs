using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace ArtOfRallyChampionshipMod.Menu
{
    public class MainMenu
    {
        
    }

    [HarmonyPatch(typeof(PanelManager), nameof(PanelManager.Start))]
    public class Start
    {
        public static void Postfix(PanelManager __instance)
        {
            var group = __instance.transform.Find("Main Menu/Race/VerticalLayoutGroup");
            var layout = group.GetComponent<VerticalLayoutGroup>();

            var button = DefaultControls.CreateButton(new DefaultControls.Resources());
            button.transform.parent = layout.transform;
            var customize = button.GetComponentInChildren<Text>();
            customize.text = "Multiplayer";
        }
    }
}
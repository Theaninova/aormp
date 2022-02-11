using HarmonyLib;
using UnityEngine;

namespace ArtOfRallyMultiplayerMod
{
    public static class MultiplayerHUD
    {
        private static readonly Rect LabelRect = new Rect(70, 40, 200, 200);
        public static string[] Players = {};

        public static void Draw()
        {
            GUI.Label(LabelRect, Players.Join(delimiter: "\n"));
        }
    }
}
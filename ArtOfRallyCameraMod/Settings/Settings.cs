﻿using UnityEngine;
using UnityModManagerNet;

// ReSharper disable ConvertToConstant.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace ArtOfRallyMultiplayerMod.Settings
{
    public class Settings : UnityModManager.ModSettings, IDrawable
    {
        [Draw("Server URL")]
        public string URL = "http://h2862963.stratoserver.net:4593";

        [Header("Multiplayer")]
        [Draw("Enable Multiplayer")] public bool EnableMultiplayer = false;

        [Draw("Server")] public bool MultiplayerServer = false;
        
        [Draw("Lobby")] public int MultiplayerLobby = 1045;
        
        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
        public void OnChange()
        {
            Main.Client.EmitAsync("leaveLobby");
            if (EnableMultiplayer)
            {
                Main.Client.EmitAsync("joinLobby", MultiplayerLobby);
            }
        }
    }
}
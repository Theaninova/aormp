﻿using System.Reflection;
using ArtOfRallyMultiplayerMod.Control;
using ArtOfRallyMultiplayerMod.Protocol;
using HarmonyLib;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using SocketIOClient.Transport;
using UnityModManagerNet;

namespace ArtOfRallyMultiplayerMod
{
    public static class Main
    {
        public static Settings.Settings Settings = null!;
        public static UnityModManager.ModEntry.ModLogger Logger = null!;
        public static SocketIO Client = null!;

        // ReSharper disable once UnusedMember.Local
        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;
            Settings = UnityModManager.ModSettings.Load<Settings.Settings>(modEntry);
            Client = new SocketIO($"{Settings.URL}/multiplayer", new SocketIOOptions
            {
                Transport = TransportProtocol.WebSocket,
                Reconnection = true
            });

            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            // modEntry.OnUpdate = CameraHandler.OnUpdate;
            // modEntry.OnFixedGUI = EditorGUI.OnFixedGUI;
            modEntry.OnGUI = entry => Settings.Draw(entry);
            modEntry.OnSaveGUI = entry => Settings.Save(entry);

            Connect();

            return true;
        }

        private static async void Connect()
        {
            Client.JsonSerializer = new NewtonsoftJsonSerializer();
            Client.OnError += (sender, error) => Logger.Error(error);
            Client.OnConnected += (sender, args) =>
            {
                Logger.Log("Connected to server!");
            };
            Client.OnDisconnected += (sender, s) => Logger.Warning("Got disconnected");
            Client.OnReconnectAttempt += (sender, i) => Logger.Log($"Trying to reconnect {i}x");

            Client.On("replayReceived", response =>
            {
                var data = response.GetValue<MultiplayerCar>();
                MultiplayerConnectionManager.LastCar = MultiplayerConnectionManager.CurrentCar;
                MultiplayerConnectionManager.CurrentCar = data.ToNative();
            });

            // TODO: sync map loading progress
            // Multiplayer
            /*Client.On(InitiateRally.MultiplayerLoadMap,
                response => InitiateRally.StartRallyRemotely(response.GetValue<LoadMapData>()));
            Client.On(InitiateRally.MultiplayerChangeMap,
                response => InitiateRally.ChangeMode(response.GetValue<GameModeManager.GAME_MODES>()));
            Client.On(InitiateRally.MultiplayerChangeRally,
                response => InitiateRally.ChangeRallyData(response.GetValue<string>()));*/
            await Client.ConnectAsync();
        }
    }
}
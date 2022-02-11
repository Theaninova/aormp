// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using HarmonyLib;
using UnityEngine;

namespace ArtOfRallyMultiplayerMod.Events
{
    public static class Data
    {
        public static readonly int GhostEnabledHandle = Shader.PropertyToID("_ghostEnabled");

        public static GhostManager.GhostData_native data = new GhostManager.GhostData_native
        {
            _version = 1,
            _map = "",
            _livery = new Livery("Car_Escort_V1"),
            _class = "Car_Escort_V1",
            _car = "Car_Escort_V1",
            _finishTime = 9999,
            _timeData = { },
            _positions = { },
            _rotations = { },
            _reset = { }
        };

        public static byte[] ObjectToByteArray(object obj)
        {
            using var serializationStream = new MemoryStream();
            new BinaryFormatter().Serialize(serializationStream, obj);
            return serializationStream.ToArray();
        }
    }

    [HarmonyPatch(typeof(StageSceneManager), nameof(StageSceneManager.StartEvent))]
    public class StartEvent
    {
        public static void Prefix(StageSceneManager __instance, bool __0)
        {
            Main.Client.EmitAsync("startEvent", __0);

            // Shader.SetGlobalInt(Data.GhostEnabledHandle, 1);
            // GhostManager.Instance.SetGlobalGhostData(Data.ObjectToByteArray(Data.data));
            // GhostManager.Instance.SetGhostType(3);
            // GhostManager.Instance.ToggleGhost(true);
        }
    }
}
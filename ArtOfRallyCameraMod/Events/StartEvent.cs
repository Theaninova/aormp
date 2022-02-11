// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using HarmonyLib;
using UnityEngine;

namespace ArtOfRallyMultiplayerMod.Events
{
    public static class Data
    {
        public static readonly int GhostEnabledHandle = Shader.PropertyToID("_ghostEnabled");

        public static bool HasStarted = false;

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

    [HarmonyPatch(typeof(PreStageScreen), nameof(PreStageScreen.StartStage))]
    public class StartEvent
    {
        public static void Prefix(PreStageScreen __instance)
        {
            if (!Main.Settings.EnableMultiplayer) return;
            
            Data.HasStarted = true;
            /*var index = __instance.GhostSelectable.ghostList.Count > 0 TODO
                ? __instance.GhostSelectable.ghostList.First()
                : 0;
            GhostManager.Instance.SetGhostType(index);*/
            Main.Client.EmitAsync("startEvent");

            // Shader.SetGlobalInt(Data.GhostEnabledHandle, 1);
            // GhostManager.Instance.SetGlobalGhostData(Data.ObjectToByteArray(Data.data));
            // GhostManager.Instance.SetGhostType(3);
            // GhostManager.Instance.ToggleGhost(true);
        }
    }

    [HarmonyPatch(typeof(PreStageScreen), "Start")]
    public class Start
    {
        public static void Postfix()
        {
            Data.HasStarted = false;
        }
    }
}
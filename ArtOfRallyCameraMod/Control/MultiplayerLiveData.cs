﻿using System.Reflection;
using System.Threading.Tasks;
using ArtOfRallyMultiplayerMod.Protocol;
using HarmonyLib;
using UnityEngine;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace ArtOfRallyMultiplayerMod.Control
{
    public static class MultiplayerConnectionManager
    {
        public static Task? Task;
        public static NativeMultiplayerCar? CurrentCar;

        public static NativeMultiplayerCar? LastCar;

        public static MethodInfo? FadeCarInfo =
            typeof(ReplayManager).GetMethod("FadeCar", BindingFlags.Instance | BindingFlags.NonPublic);
    }

    [HarmonyPatch(typeof(GhostManager), nameof(GhostManager.UpdateGhost))]
    public class UpdateGhost
    {
        public static bool Prefix(
            GhostManager __instance,
            Transform ____ghostTransform,
            ref bool ____fadedIn,
            bool ____fading,
            int ____carFadeHandle,
            float __0,
            bool __1,
            ref bool ____reloadAttempt,
            int ____playerPosition,
            GhostManager.GhostData? ____currentData,
            bool ____createdGhost)
        {
            if (!Main.Settings.EnableMultiplayer) return true;
            if (GameEntryPoint.EventManager.status != EventStatusEnums.EventStatus.UNDERWAY) return false;

            Shader.SetGlobalVector(____playerPosition,
                GameEntryPoint.EventManager.playerManager.PlayerObject.transform.position);
            if (____currentData == null || ____ghostTransform == null || MultiplayerConnectionManager.CurrentCar == null)
            {
                return false;
            }

            if (!____createdGhost && !__1)
            {
                if (!____reloadAttempt) return false;
                __instance.InitializeGhost();
                ____reloadAttempt = false;
                return false;
            }

            /*if (!____fadedIn && MultiplayerConnectionManager.FadeCarInfo != null) TODO
            {
                ____fadedIn = true;
                __instance.StartCoroutine(
                    (IEnumerator)MultiplayerConnectionManager.FadeCarInfo.Invoke(__instance,
                        new object[] { 1f, 0.5f, 0f, 0f }));
            }*/
            Shader.SetGlobalFloat(____carFadeHandle, 1f);

            var current = MultiplayerConnectionManager.CurrentCar.Value;
            var last = MultiplayerConnectionManager.LastCar ?? current;

            var t = Mathf.InverseLerp(last.time, current.time, __0);
            ____ghostTransform.position = Vector3.Lerp(last.position, current.position, t);
            ____ghostTransform.rotation = Quaternion.Slerp(last.rotation, current.rotation, t);
            ____ghostTransform.position -= ____ghostTransform.up * 0.08f;
            /*if (!____fading)
                {
                    int num = this.CurrentData._reset[this._currentIndex] ? 0 : 1;
                    int num2 = this.CurrentData._reset[this._currentIndex + 1] ? 0 : 1;
                    this.SetCarFade(Mathf.Lerp((float)num, (float)num2, t));
                }*/

            return false;
        }
    }

    [HarmonyPatch(typeof(CarController), "FixedUpdate")]
    public class RecordKeyframe
    {
        public static void Postfix(Rigidbody? ___body)
        {
            if (!Main.Settings.EnableMultiplayer || MultiplayerConnectionManager.Task is { IsCompleted: false }) return;

            if (___body == null) return;

            var stageSceneManager = GameEntryPoint.EventManager;
            var time = stageSceneManager is StageSceneManager
                ? stageSceneManager.stageTimerManager != null ? stageSceneManager.stageTimerManager.GetStageTimeMS() :
                0.0f
                : 0.0f;

            MultiplayerConnectionManager.Task =
                Main.Client.EmitAsync("replayUpdated", MultiplayerCar.FromBody(___body, time));
        }
    }
}
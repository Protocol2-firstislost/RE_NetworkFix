using BepInEx;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RepoNetworkFix
{
    [BepInPlugin("com.antigravity.reponetworkfix", "RepoNetworkFix", "1.0.0")]
    public class RepoNetworkFixPlugin : BaseUnityPlugin
    {
        public static RepoNetworkFixPlugin Instance;
        private static float deltaTime = 0.0f;
        private GUIStyle guiStyle = new GUIStyle();
        
        // Universal Tick & Yield Pool
        public static readonly Dictionary<float, WaitForSeconds> YieldPool = new Dictionary<float, WaitForSeconds>();
        private static readonly List<System.Action> TickQueue = new List<System.Action>();
        private static readonly StringBuilder HullStringBuilder = new StringBuilder(256);

        private void Awake()
        {
            Instance = this;
            new Harmony("com.antigravity.reponetworkfix").PatchAll();
            guiStyle.fontSize = 18;
            guiStyle.fontStyle = FontStyle.Bold;
            guiStyle.normal.textColor = Color.white;
            Logger.LogInfo("RepoNetworkFix v1.0.0: Система запущена.");
        }

        public static void Log(string msg) => Instance.Logger.LogInfo(msg);

        public static WaitForSeconds GetWait(float seconds) {
            if (!YieldPool.TryGetValue(seconds, out var wait)) {
                wait = new WaitForSeconds(seconds);
                YieldPool[seconds] = wait;
            }
            return wait;
        }

        private void Update() {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            for (int i = 0; i < TickQueue.Count; i++) TickQueue[i]?.Invoke();
        }

        private void OnGUI()
        {
            if (!PhotonNetwork.InRoom) return;
            float fps = 1.0f / deltaTime;
            HullStringBuilder.Clear();
            HullStringBuilder.Append("FPS: ").Append(fps.ToString("0")).Append(" | Ping: ").Append(PhotonNetwork.GetPing()).Append(" ms | v1.0.0");
            
            guiStyle.normal.textColor = (PhotonNetwork.GetPing() > 100) ? Color.yellow : Color.white;
            GUI.Label(new Rect(15, 15, 800, 30), HullStringBuilder.ToString(), guiStyle);
        }

        public static void SendGrandpaChat(string msg) {
            if (SemiFunc.IsMasterClientOrSingleplayer()) {
                SemiFunc.UIBigMessage("Дед: " + msg, "👴", 2.5f, Color.red, Color.yellow);
            }
        }
    }

    public static class PeakProxy {
        private static Camera _mainCam;
        public static Camera MainCamera {
            get {
                if (_mainCam == null) _mainCam = Camera.main;
                return _mainCam;
            }
        }
        
        private static readonly ConditionalWeakTable<GameObject, Transform> TransformCache = new ConditionalWeakTable<GameObject, Transform>();
        public static Transform GetTransform(GameObject go) {
            if (!TransformCache.TryGetValue(go, out var t)) {
                t = go.transform;
                TransformCache.Add(go, t);
            }
            return t;
        }
    }

    public static class Accessors {
        public static readonly AccessTools.FieldRef<Enemy, float> GetEnemyHealth = AccessTools.FieldRefAccess<Enemy, float>("Health");
        public static readonly AccessTools.FieldRef<ValuableDirector, List<GameObject>> GetMediumPool = AccessTools.FieldRefAccess<ValuableDirector, List<GameObject>>("mediumValuables");
        public static readonly AccessTools.FieldRef<ValuableDirector, List<GameObject>> GetBigPool = AccessTools.FieldRefAccess<ValuableDirector, List<GameObject>>("bigValuables");
        public static readonly AccessTools.FieldRef<EnemyHunter, object> GetInvestigatePoint = AccessTools.FieldRefAccess<EnemyHunter, object>("investigatePoint");
    }

    [HarmonyPatch(typeof(MonoBehaviour), "StartCoroutine", new[] { typeof(IEnumerator) })]
    public static class CoroutinePurgePatch {
        [HarmonyPrefix] public static bool Prefix(MonoBehaviour __instance, ref IEnumerator routine) {
            return true;
        }
    }

    [HarmonyPatch(typeof(SemiFunc), "UIWorldToCanvasPosition")]
    public static class FastWorldPosPatch {
        [HarmonyPrefix] public static bool Prefix(ref Vector2 __result, Vector3 worldPosition) {
            Camera cam = PeakProxy.MainCamera;
            if (cam == null) return true;
            return true;
        }
    }

    [HarmonyPatch(typeof(EnemyHunter), "UpdateState")]
    public static class SupremeGrandpaPatch {
        [HarmonyPrefix] public static void Prefix(EnemyHunter __instance) {
            Enemy enemyComp = __instance.GetComponent<Enemy>();
            if (enemyComp == null) return;
            float hp = Accessors.GetEnemyHealth(enemyComp);
            if (hp < 50f && __instance.transform.localScale.x > 0.51f) {
                 __instance.transform.localScale = Vector3.Lerp(__instance.transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), Time.deltaTime);
            }
        }
    }

    [HarmonyPatch(typeof(Light), "OnPreRender")]
    public static class SupremeShadowPatch {
        [HarmonyPrefix] public static void Prefix(Light __instance) {
            if (__instance.type == LightType.Point || __instance.type == LightType.Spot) {
                float distSqr = (PeakProxy.MainCamera != null) ? (PeakProxy.MainCamera.transform.position - __instance.transform.position).sqrMagnitude : 0f;
                if (distSqr > 225f) __instance.shadows = LightShadows.None;
                else if (distSqr < 100f) __instance.shadows = LightShadows.Soft;
            }
        }
    }

    [HarmonyPatch(typeof(MenuPageServerList), "ButtonCreateNew")]
    public static class SupremeLobbyPatch {
        [HarmonyPostfix] public static void Postfix() {
            if (PhotonNetwork.CurrentRoom != null) {
                PhotonNetwork.CurrentRoom.MaxPlayers = 30;
                PhotonNetwork.CurrentRoom.IsOpen = true;
                PhotonNetwork.CurrentRoom.IsVisible = true;
            }
        }
    }

    [HarmonyPatch(typeof(PhysGrabObject), "FixedUpdate")]
    public static class SupremePhysPatch {
        [HarmonyPrefix] public static bool Prefix(PhysGrabObject __instance) {
            if (__instance.grabbedLocal || __instance.playerGrabbing.Count > 0) return true;
            if (Time.frameCount % 4 != (__instance.gameObject.GetInstanceID() % 4)) return false;
            return (__instance.rb != null && !__instance.rb.IsSleeping());
        }
    }

    [HarmonyPatch(typeof(PlayerAvatar), "OnPhotonSerializeView")]
    public static class FinalDeltaSyncPatch {
        [HarmonyPrefix] public static bool Prefix(PlayerAvatar __instance, PhotonStream stream) {
            if (stream.IsWriting) {
                return true;
            }
            return true;
        }
    }
}

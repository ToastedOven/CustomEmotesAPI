using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using RoR2.UI;
using RiskOfOptions;
using UnityEngine.Rendering.PostProcessing;
using LeTai.Asset.TranslucentImage;
using R2API.Networking;

namespace EmotesAPI
{
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInPlugin("com.weliveinasociety.CustomEmotesAPI", "Custom Emotes API", VERSION)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [R2APISubmoduleDependency("PrefabAPI", "ResourcesAPI", "NetworkingAPI")]
    public class CustomEmotesAPI : BaseUnityPlugin
    {
        public static List<string> allClipNames = new List<string>();
        public static void LoadResource(string resource)
        {
            DebugClass.Log($"Loading {resource}");
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"CustomEmotesAPI.{resource}"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider($"@CustomEmotesAPI_{resource}", MainAssetBundle));
            }
        }
        public const string VERSION = "1.0.0";
        public void Awake()
        {
            DebugClass.SetLogger(base.Logger);
            Settings.RunAll();
            Register.Init();
            AnimationReplacements.RunAll();
        }
        public static void AddCustomAnimation(string name, string location, bool looping, string _wwiseEventName = "", string _wwiseStopEvent = "")
        {
            if (BoneMapper.animClips.ContainsKey(name))
            {
                DebugClass.Log($"Error #1: [{name}] is already defined as a custom emote but is trying to be added. Skipping");
                return;
            }
            CustomAnimationClip clip = new CustomAnimationClip(Resources.Load<AnimationClip>(location), looping, _wwiseEventName, _wwiseStopEvent);
            allClipNames.Add(name);
            BoneMapper.animClips.Add(name, clip);
        }
    }
}
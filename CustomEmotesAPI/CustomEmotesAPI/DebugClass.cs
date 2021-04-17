using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using System.Text;
using BepInEx.Logging;


    public static class DebugClass
    {
        private static ManualLogSource Logger;

        public static void SetLogger(ManualLogSource logSource)
        {
            Logger = logSource;
        }
        public static void Log(object message)
        {
            Logger.Log(LogLevel.Info, $"{message}");
        }
    }

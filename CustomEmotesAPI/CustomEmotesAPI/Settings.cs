using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using UnityEngine;
using UnityEngine.Events;

namespace EmotesAPI
{
    public static class Settings
    {
        public static ConfigEntry<KeyboardShortcut> EmoteWheel;
        public static ConfigEntry<KeyboardShortcut> Left;
        public static ConfigEntry<KeyboardShortcut> Right;
        public static ConfigEntry<float> DontTouchThis;

        public static void RunAll()
        {
            Setup();
            Yes();
        }
        private static void Setup()
        {
            ModSettingsManager.SetModDescription($"Made by Metrosexual Fruitcake#6969\n\nVersion {CustomEmotesAPI.VERSION}");

            //ModSettingsManager.CreateCategory("Controls");
        }
        private static void Yes()
        {
            EmoteWheel = CustomEmotesAPI.instance.Config.Bind<KeyboardShortcut>("Controls", "Emote Wheel", new KeyboardShortcut(KeyCode.C), "Displays the emote wheel");
            Left = CustomEmotesAPI.instance.Config.Bind<KeyboardShortcut>("Controls", "Cycle Wheel Left", new KeyboardShortcut(KeyCode.Mouse0), "Cycles the emote wheel left");
            Right = CustomEmotesAPI.instance.Config.Bind<KeyboardShortcut>("Controls", "Cycle Wheel Right", new KeyboardShortcut(KeyCode.Mouse1), "Cycles the emote wheel right");
            DontTouchThis = CustomEmotesAPI.instance.Config.Bind<float>("Data", "Dont Touch This", 69420, "But like actually dont touch this");

            ModSettingsManager.AddOption(new KeyBindOption(EmoteWheel));
            ModSettingsManager.AddOption(new KeyBindOption(Left));
            ModSettingsManager.AddOption(new KeyBindOption(Right));


        }
    }
}

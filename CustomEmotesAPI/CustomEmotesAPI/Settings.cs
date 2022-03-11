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

            //AddKeyBind("Emote Wheel", "Displays the emote wheel.", KeyCode.C, "Controls");

            //ModSettingsManager.AddListener(new UnityAction<KeyCode>(delegate (KeyCode keyCode) { EmoteWheel.emoteButton = keyCode; }), "Emote Wheel", "Controls");

            //AddKeyBind("left", "Displays the emote wheel.", KeyCode.Mouse0, "Controls");

            //ModSettingsManager.AddListener(new UnityAction<KeyCode>(delegate (KeyCode keyCode) { EmoteWheel.leftClick = keyCode; }), "left", "Controls");

            //AddKeyBind("right", "Displays the emote wheel.", KeyCode.Mouse1, "Controls");

            //ModSettingsManager.AddListener(new UnityAction<KeyCode>(delegate (KeyCode keyCode) { EmoteWheel.rightClick = keyCode; }), "right", "Controls");

            //AddSlider("Ligma Balls", "It's so sad that steve jobs died of ligma", 69420, 0, 69420, "Controls", false);
        }

        //private static void AddKeyBind(string name, string desc, KeyCode key, string category)
        //{
        //    var thing = new RiskOfOptions.OptionConstructors.KeyBind() { Name = name, Description = desc, DefaultValue = key, CategoryName = category };
        //    ModSettingsManager.AddOption(thing);
        //}
        //private static void AddSlider(string name, string desc, int starting, int min, int max, string category, bool visible)
        //{
        //    //ModSettingsManager.AddSlider("HitMarker Volume", "This sound is also tied to SFX, but has a separate slider if you want it to be less noisy", 100, 0, 100, "Audio", survivorsSkinsOnlySlider);
        //    var thing = new RiskOfOptions.OptionConstructors.Slider() { Name = name, Description = desc, CategoryName = category, DefaultValue = starting, Min = min, Max = max, IsVisible = visible };
        //    ModSettingsManager.AddOption(thing);
        //}
    }
}

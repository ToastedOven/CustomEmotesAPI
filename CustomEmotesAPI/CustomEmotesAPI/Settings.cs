using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using RiskOfOptions;
using RiskOfOptions.OptionOverrides;
using UnityEngine;
using UnityEngine.Events;

namespace EmotesAPI
{
    public static class Settings
    {
        public static void RunAll()
        {
            Setup();
            Yes();
        }
        private static void Setup()
        {
            ModSettingsManager.setPanelDescription($"Made by Metrosexual Fruitcake#6969\n\nVersion {CustomEmotesAPI.VERSION}");

            ModSettingsManager.setPanelTitle("Custom Emotes");
            ModSettingsManager.CreateCategory("Controls");
        }
        private static void Yes()
        {
            AddKeyBind("Emote Wheel", "Displays the emote wheel.", KeyCode.C, "Controls");

            ModSettingsManager.AddListener(new UnityAction<KeyCode>(delegate (KeyCode keyCode) { EmoteWheel.emoteButton = keyCode; }), "Emote Wheel", "Controls");

            AddKeyBind("left", "Displays the emote wheel.", KeyCode.Mouse0, "Controls");

            ModSettingsManager.AddListener(new UnityAction<KeyCode>(delegate (KeyCode keyCode) { EmoteWheel.leftClick = keyCode; }), "left", "Controls");

            AddKeyBind("right", "Displays the emote wheel.", KeyCode.Mouse1, "Controls");

            ModSettingsManager.AddListener(new UnityAction<KeyCode>(delegate (KeyCode keyCode) { EmoteWheel.rightClick = keyCode; }), "right", "Controls");
        }

        private static void AddKeyBind(string name, string desc, KeyCode key, string category)
        {
            var thing = new RiskOfOptions.OptionConstructors.KeyBind() { Name = name, Description = desc, DefaultValue = key, CategoryName = category };
            ModSettingsManager.AddOption(thing);
        }
    }
}

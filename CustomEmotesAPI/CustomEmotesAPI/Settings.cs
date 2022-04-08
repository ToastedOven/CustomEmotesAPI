using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using RoR2.UI;

namespace EmotesAPI
{
    public static class Settings
    {
        public static ConfigEntry<KeyboardShortcut> EmoteWheel;
        public static ConfigEntry<KeyboardShortcut> Left;
        public static ConfigEntry<KeyboardShortcut> Right;
        public static ConfigEntry<float> DontTouchThis;

        public static ConfigEntry<string> emote0;
        public static ConfigEntry<string> emote1;
        public static ConfigEntry<string> emote2;
        public static ConfigEntry<string> emote3;
        public static ConfigEntry<string> emote4;
        public static ConfigEntry<string> emote5;
        public static ConfigEntry<string> emote6;
        public static ConfigEntry<string> emote7;
        public static ConfigEntry<string> emote8;
        public static ConfigEntry<string> emote9;
        public static ConfigEntry<string> emote10;
        public static ConfigEntry<string> emote11;
        public static ConfigEntry<string> emote12;
        public static ConfigEntry<string> emote13;
        public static ConfigEntry<string> emote14;
        public static ConfigEntry<string> emote15;
        public static ConfigEntry<string> emote16;
        public static ConfigEntry<string> emote17;
        public static ConfigEntry<string> emote18;
        public static ConfigEntry<string> emote19;
        public static ConfigEntry<string> emote20;
        public static ConfigEntry<string> emote21;
        public static ConfigEntry<string> emote22;
        public static ConfigEntry<string> emote23;



        public static GameObject NakedButton = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/UI/NakedButton.prefab").WaitForCompletion();
        public static void RunAll()
        {
            UnityEngine.Object.DestroyImmediate(NakedButton.GetComponentInChildren<LanguageTextMeshController>());
            Setup();
            Yes();
        }
        private static void Setup()
        {
            ModSettingsManager.SetModDescription($"Made by Metrosexual Fruitcake#6969\n\nVersion {CustomEmotesAPI.VERSION}");
            ModSettingsManager.SetModIcon(Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/icon.png"));
            //ModSettingsManager.CreateCategory("Controls");
        }
        internal static GameObject picker;
        private static void Yes()
        {
            picker = GameObject.Instantiate(Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/emotepicker2.prefab"));
            GameObject.DontDestroyOnLoad(picker);
            picker.GetComponent<Canvas>().scaleFactor = 2;
            picker.transform.Find("emotepicker").Find("EmoteContainer").gameObject.AddComponent<ScrollManager>();
            var basedonwhatbasedonthehardwareinside = picker.transform.Find("emotepicker").Find("Wheels").transform.Find("Middle");
            for (int i = 0; i < 8; i++)
            {
                GameObject nut = GameObject.Instantiate(Settings.NakedButton);
                nut.transform.SetParent(basedonwhatbasedonthehardwareinside.Find($"Button ({i})"));
                nut.transform.localPosition = new Vector3(-80, -20, 0);
                nut.transform.localScale = new Vector3(.8f, .8f, .8f);
            }
            basedonwhatbasedonthehardwareinside = picker.transform.Find("emotepicker").Find("Wheels").transform.Find("Left");
            for (int i = 0; i < 8; i++)
            {
                GameObject nut = GameObject.Instantiate(Settings.NakedButton);
                nut.transform.SetParent(basedonwhatbasedonthehardwareinside.Find($"Button ({i})"));
                nut.transform.localPosition = new Vector3(-80, -20, 0);
                nut.transform.localScale = new Vector3(.8f, .8f, .8f);
            }
            basedonwhatbasedonthehardwareinside = picker.transform.Find("emotepicker").Find("Wheels").transform.Find("Right");
            for (int i = 0; i < 8; i++)
            {
                GameObject nut = GameObject.Instantiate(Settings.NakedButton);
                nut.transform.SetParent(basedonwhatbasedonthehardwareinside.Find($"Button ({i})"));
                nut.transform.localPosition = new Vector3(-80, -20, 0);
                nut.transform.localScale = new Vector3(.8f, .8f, .8f);
            }

            EmoteWheel = CustomEmotesAPI.instance.Config.Bind<KeyboardShortcut>("Controls", "Emote Wheel", new KeyboardShortcut(KeyCode.C), "Displays the emote wheel");
            Left = CustomEmotesAPI.instance.Config.Bind<KeyboardShortcut>("Controls", "Cycle Wheel Left", new KeyboardShortcut(KeyCode.Mouse0), "Cycles the emote wheel left");
            Right = CustomEmotesAPI.instance.Config.Bind<KeyboardShortcut>("Controls", "Cycle Wheel Right", new KeyboardShortcut(KeyCode.Mouse1), "Cycles the emote wheel right");

            emote0 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes0", "none", "Messing with this here is not reccomended, like at all");
            emote1 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes1", "none", "Messing with this here is not reccomended, like at all");
            emote2 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes2", "none", "Messing with this here is not reccomended, like at all");
            emote3 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes3", "none", "Messing with this here is not reccomended, like at all");
            emote4 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes4", "none", "Messing with this here is not reccomended, like at all");
            emote5 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes5", "none", "Messing with this here is not reccomended, like at all");
            emote6 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes6", "none", "Messing with this here is not reccomended, like at all");
            emote7 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes7", "none", "Messing with this here is not reccomended, like at all");
            emote8 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes8", "none", "Messing with this here is not reccomended, like at all");
            emote9 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes9", "none", "Messing with this here is not reccomended, like at all");
            emote10 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes10", "none", "Messing with this here is not reccomended, like at all");
            emote11 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes11", "none", "Messing with this here is not reccomended, like at all");
            emote12 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes12", "none", "Messing with this here is not reccomended, like at all");
            emote13 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes13", "none", "Messing with this here is not reccomended, like at all");
            emote14 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes14", "none", "Messing with this here is not reccomended, like at all");
            emote15 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes15", "none", "Messing with this here is not reccomended, like at all");
            emote16 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes16", "none", "Messing with this here is not reccomended, like at all");
            emote17 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes17", "none", "Messing with this here is not reccomended, like at all");
            emote18 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes18", "none", "Messing with this here is not reccomended, like at all");
            emote19 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes19", "none", "Messing with this here is not reccomended, like at all");
            emote20 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes20", "none", "Messing with this here is not reccomended, like at all");
            emote21 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes21", "none", "Messing with this here is not reccomended, like at all");
            emote22 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes22", "none", "Messing with this here is not reccomended, like at all");
            emote23 = CustomEmotesAPI.instance.Config.Bind<string>("Data", "Bind for emotes23", "none", "Messing with this here is not reccomended, like at all");

            DontTouchThis = CustomEmotesAPI.instance.Config.Bind<float>("Data", "Dont Touch This", 69420, "But like actually dont touch this");

            ModSettingsManager.AddOption(new GenericButtonOption("Customize Emote Wheel", "Controls", PressButton));
            ModSettingsManager.AddOption(new KeyBindOption(EmoteWheel));
            ModSettingsManager.AddOption(new KeyBindOption(Left));
            ModSettingsManager.AddOption(new KeyBindOption(Right));
        }
        internal static void PressButton()
        {
            picker.SetActive(false);
            picker.SetActive(true);
            picker.transform.Find("emotepicker").gameObject.SetActive(true);
            picker.transform.SetAsLastSibling();
            picker.GetComponent<Canvas>().sortingOrder = 5;
            
        }
    }
}

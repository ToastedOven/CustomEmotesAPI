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
using System.Collections.Generic;
using System.Text;
using RiskOfOptions;
using TMPro;
using R2API.Networking.Interfaces;
using EmotesAPI;
using System.Collections;
using UnityEngine.UI;
using RoR2.UI;

public class EmoteWheel : MonoBehaviour
{
    internal GameObject text;

    internal List<GameObject> gameObjects = new List<GameObject>();
    internal Image joy;

    internal RoR2.UI.MPInput input = GameObject.Find("MPEventSystem Player0").GetComponent<RoR2.UI.MPInput>();
    internal RoR2.UI.MPEventSystem events;

    internal string[] leftPage = new string[8];
    internal string[] middlePage = new string[8];
    internal string[] rightPage = new string[8];
    int activePage = 1; //0 left, 1 middle, 2 right

    GameObject selected;
    float XScale = 1, YScale = 1;
    void Start()
    {
        transform.localPosition = new Vector3(0, 2000, 0);
    }
    internal void RefreshWheels()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            middlePage[i] = ScrollManager.circularButtons[i].GetComponentInChildren<HGTextMeshProUGUI>().text;
            leftPage[i] = ScrollManager.circularButtons[i + 8].GetComponentInChildren<HGTextMeshProUGUI>().text;
            rightPage[i] = ScrollManager.circularButtons[i + 16].GetComponentInChildren<HGTextMeshProUGUI>().text;
        }
    }
    bool started = false;
    void Update()
    {
        if (!started)
        {
            try
            {
                foreach (var item in CustomEmotesAPI.nameTokenSpritePairs)
                {
                    //DebugClass.Log($"{NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().baseNameToken}         {item.nameToken}");
                    if (NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().baseNameToken == item.nameToken)
                    {
                        //DebugClass.Log($"{item.nameToken}         {item.sprite}");
                        joy.sprite = item.sprite;
                        break;
                    }
                }
                selected = gameObjects[0];
                events = input.GetFieldValue<RoR2.UI.MPEventSystem>("eventSystem");
                RefreshWheels();
                started = true;
            }
            catch (Exception)
            {
                return;
            }
        }
        if (RoR2.PauseManager.isPaused)
            return;

        Vector3 v = new Vector3(0, 0, 0);
        if (transform.localPosition == v)
        {
            XScale = Screen.width / 1980f;
            YScale = Screen.height / 1080f;
            if (!(Math.Abs(Input.mousePosition.x - (Screen.width / 2.0f)) < 30f * XScale && Math.Abs(Input.mousePosition.y - (Screen.height / 2.0f)) < 30f * YScale))
            {
                float dist = 99999;
                foreach (var item in gameObjects)
                {
                    if (dist > Vector2.Distance(new Vector2(item.GetComponent<RectTransform>().localPosition.x + (Screen.width / 2), item.GetComponent<RectTransform>().localPosition.y + (Screen.height / 2)), (Vector2)Input.mousePosition))
                    {
                        dist = Vector2.Distance(new Vector2(item.GetComponent<RectTransform>().localPosition.x + (Screen.width / 2), item.GetComponent<RectTransform>().localPosition.y + (Screen.height / 2)), (Vector2)Input.mousePosition);
                        selected = item;
                    }
                    item.GetComponent<RectTransform>().localScale = new Vector3(0.6771638f, 0.6771638f, 0.6771638f);
                }
                selected.GetComponent<RectTransform>().localScale = new Vector3(0.9771638f, 0.9771638f, 0.9771638f);
            }
            else
            {
                selected.GetComponent<RectTransform>().localScale = new Vector3(0.6771638f, 0.6771638f, 0.6771638f);
            }
        }
        //if (Input.GetKey(KeyCode.Mouse1) && !selected.GetComponent<RoR2.UI.MPDropdown>())
        //{
        //    DebugClass.Log($"----------doing the thing");
        //    if (transform.localPosition == v)
        //    {
        //        //UnityEngine.UI.Dropdown
        //        var drop = selected.AddComponent<RoR2.UI.MPDropdown>();
        //        drop.AddOptions(CustomEmotesAPI.allClipNames);
        //        //drop.template = GameObject.Instantiate(Resources.Load<GameObject>("prefabs/ui/PauseScreen")).transform.Find("Blur + Background Panel").Find("ValidScreenspacePanel").Find("SettingsPanel(Clone)").Find("SafeArea").Find("SubPanelArea").Find("SettingsSubPanel, Video").Find("Scroll View").Find("Viewport").Find("VerticalLayout").Find("Option, Resolution").Find("CarouselRect").Find("ResolutionDropdown").Find("Template").GetComponent<RectTransform>();
        //    }
        //}
        switch (activePage)
        {
            case 0:
                joy.color = Color.Lerp(joy.color, new Color(114f / 255f, 157f / 255f, 255f / 255f, .9f), Time.deltaTime * 4);
                break;
            case 1:
                joy.color = Color.Lerp(joy.color, new Color(114f / 255f, 255f / 255f, 157f / 255f, .9f), Time.deltaTime * 4);
                break;
            case 2:
                joy.color = Color.Lerp(joy.color, new Color(255f / 255f, 114f / 255f, 157f / 255f, .9f), Time.deltaTime * 4);
                break;
            default:
                break;
        }
        //DebugClass.Log($"----------{activePage} ---  {joy.color} ---  {joy.sprite.name}");
        if (CustomEmotesAPI.GetKeyPressed(Settings.Left))
        {
            if (transform.localPosition == v)
            {
                //Event.current.Use();
                if (activePage == 1)
                {
                    activePage = 0;
                    StartCoroutine(SwitchPage(leftPage));
                }
                if (activePage == 2)
                {
                    activePage = 1;
                    StartCoroutine(SwitchPage(middlePage));
                }
            }
        }
        if (CustomEmotesAPI.GetKeyPressed(Settings.Right))
        {
            if (transform.localPosition == v)
            {
                //Event.current.Use();
                if (activePage == 1)
                {
                    activePage = 2;
                    StartCoroutine(SwitchPage(rightPage));
                }
                if (activePage == 0)
                {
                    activePage = 1;
                    StartCoroutine(SwitchPage(middlePage));
                }
            }
        }
        if (CustomEmotesAPI.GetKey(Settings.EmoteWheel))
        {
            if (transform.localPosition != v)
            {
                events.cursorOpenerForGamepadCount += 1;
                events.cursorOpenerCount += 1;
            }
            transform.localPosition = v;
        }
        else
        {
            if (transform.localPosition == v)
            {
                try
                {
                    XScale = Screen.width / 1980f;
                    YScale = Screen.height / 1080f;
                    if (Math.Abs(Input.mousePosition.x - (Screen.width / 2.0f)) < 30f * XScale && Math.Abs(Input.mousePosition.y - (Screen.height / 2.0f)) < 30f * YScale)
                    {
                        CustomEmotesAPI.PlayAnimation("none");
                    }
                    else
                    {
                        CustomEmotesAPI.PlayAnimation(selected.GetComponentInChildren<TextMeshProUGUI>().text);
                    }
                }
                catch (Exception e)
                {
                    DebugClass.Log(e);
                }
                if (events.cursorOpenerForGamepadCount > 0)
                {
                    events.cursorOpenerForGamepadCount -= 1;
                    events.cursorOpenerCount -= 1;
                }
            }
            transform.localPosition = new Vector3(0, 2000, 0);
            StartCoroutine(SwitchPage(middlePage));
            activePage = 1;
        }
    }

    private IEnumerator SwitchPage(string[] newPage, bool instant = true)
    {
        if (instant)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = newPage[i];
            }
            yield break;
        }
        bool stay = true;
        float longestLength = 0;
        while (stay)
        {
            stay = false;
            foreach (var item in gameObjects)
            {
                if (item.GetComponentInChildren<TextMeshProUGUI>().text.Length != 0)
                {
                    if (item.GetComponentInChildren<TextMeshProUGUI>().text.Length > longestLength)
                        longestLength = item.GetComponentInChildren<TextMeshProUGUI>().text.Length;

                    item.GetComponentInChildren<TextMeshProUGUI>().text = item.GetComponentInChildren<TextMeshProUGUI>().text.Remove(0, 1);
                    stay = true;
                }
            }
            if (longestLength != 0)
            {
                yield return new WaitForSeconds(.05f / longestLength);
            }
        }
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = newPage[i];
        }

        yield break;
    }

    void OnDestroy()
    {
        if (CustomEmotesAPI.GetKey(Settings.EmoteWheel))
        {
            if (events.cursorOpenerForGamepadCount > 0)
            {
                events.cursorOpenerForGamepadCount -= 1;
                events.cursorOpenerCount -= 1;
            }
        }
    }
}

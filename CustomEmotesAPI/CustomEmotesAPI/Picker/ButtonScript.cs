using EmotesAPI;
using RoR2.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject emoteInQuestion;
    void Start()
    {

    }

    public void PickNewEmote()
    {
        emoteInQuestion.GetComponentInChildren<HGTextMeshProUGUI>().text = GetComponentInChildren<HGTextMeshProUGUI>().text;
        gameObject.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.Find("Wheels").gameObject.SetActive(true);
        gameObject.transform.parent.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
    }
    internal void SaveSettings()
    {
        Settings.emote0.Value = ScrollManager.circularButtons[0].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote1.Value = ScrollManager.circularButtons[1].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote2.Value = ScrollManager.circularButtons[2].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote3.Value = ScrollManager.circularButtons[3].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote4.Value = ScrollManager.circularButtons[4].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote5.Value = ScrollManager.circularButtons[5].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote6.Value = ScrollManager.circularButtons[6].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote7.Value = ScrollManager.circularButtons[7].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote8.Value = ScrollManager.circularButtons[8].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote9.Value = ScrollManager.circularButtons[9].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote10.Value = ScrollManager.circularButtons[10].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote11.Value = ScrollManager.circularButtons[11].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote12.Value = ScrollManager.circularButtons[12].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote13.Value = ScrollManager.circularButtons[13].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote14.Value = ScrollManager.circularButtons[14].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote15.Value = ScrollManager.circularButtons[15].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote16.Value = ScrollManager.circularButtons[16].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote17.Value = ScrollManager.circularButtons[17].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote18.Value = ScrollManager.circularButtons[18].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote19.Value = ScrollManager.circularButtons[19].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote20.Value = ScrollManager.circularButtons[20].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote21.Value = ScrollManager.circularButtons[21].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote22.Value = ScrollManager.circularButtons[22].GetComponentInChildren<HGTextMeshProUGUI>().text;
        Settings.emote23.Value = ScrollManager.circularButtons[23].GetComponentInChildren<HGTextMeshProUGUI>().text;
    }
    public void Finish()
    {
        SaveSettings();
        if (AnimationReplacements.g)
        {
            AnimationReplacements.g.GetComponent<EmoteWheel>().RefreshWheels();
        }
        gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Finish();
        }
    }
}

using EmotesAPI;
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
        emoteInQuestion.GetComponentInChildren<TextMeshProUGUI>().text = GetComponentInChildren<TextMeshProUGUI>().text;
        gameObject.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.Find("Wheels").gameObject.SetActive(true);
        gameObject.transform.parent.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
    }
    internal void SaveSettings()
    {
        Settings.emote0.Value = ScrollManager.circularButtons[0].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote1.Value = ScrollManager.circularButtons[1].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote2.Value = ScrollManager.circularButtons[2].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote3.Value = ScrollManager.circularButtons[3].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote4.Value = ScrollManager.circularButtons[4].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote5.Value = ScrollManager.circularButtons[5].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote6.Value = ScrollManager.circularButtons[6].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote7.Value = ScrollManager.circularButtons[7].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote8.Value = ScrollManager.circularButtons[8].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote9.Value = ScrollManager.circularButtons[9].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote10.Value = ScrollManager.circularButtons[10].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote11.Value = ScrollManager.circularButtons[11].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote12.Value = ScrollManager.circularButtons[12].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote13.Value = ScrollManager.circularButtons[13].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote14.Value = ScrollManager.circularButtons[14].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote15.Value = ScrollManager.circularButtons[15].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote16.Value = ScrollManager.circularButtons[16].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote17.Value = ScrollManager.circularButtons[17].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote18.Value = ScrollManager.circularButtons[18].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote19.Value = ScrollManager.circularButtons[19].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote20.Value = ScrollManager.circularButtons[20].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote21.Value = ScrollManager.circularButtons[21].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote22.Value = ScrollManager.circularButtons[22].GetComponentInChildren<TextMeshProUGUI>().text;
        Settings.emote23.Value = ScrollManager.circularButtons[23].GetComponentInChildren<TextMeshProUGUI>().text;
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

    }
}

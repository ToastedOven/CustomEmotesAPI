using EmotesAPI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    // Start is called before the first frame update
    internal GameObject content;
    internal static GameObject buttonTemplate;
    internal static List<GameObject> buttons = new List<GameObject>();
    internal static List<string> emoteNames = new List<string>();
    internal GameObject emoteInQuestion;
    internal static List<GameObject> circularButtons = new List<GameObject>();
    void Start()
    {
        gameObject.transform.parent.Find("Wheels").gameObject.SetActive(true);

        content = gameObject.transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;

        buttonTemplate = gameObject.transform.Find("Scroll View").Find("Viewport").Find("Content").Find("Button").gameObject;
        var butt = buttonTemplate.AddComponent<ButtonScript>();
        buttonTemplate.GetComponent<Button>().onClick.AddListener(butt.PickNewEmote);
        gameObject.transform.Find("Finish").GetComponent<Button>().onClick.AddListener(Cancel);

        TMP_InputField field = gameObject.transform.Find("InputField (TMP)").gameObject.GetComponent<TMP_InputField>();
        field.onValueChanged.AddListener(delegate { UpdateButtonVisibility(field.text); });
        GameObject wheels = transform.parent.Find("Wheels").gameObject;
        var basedonwhatbasedonthehardwareinside = wheels.transform.Find("Middle");
        for (int i = 0; i < 8; i++)
        {
            circularButtons.Add(basedonwhatbasedonthehardwareinside.Find($"Button ({i})").gameObject);
        }
        basedonwhatbasedonthehardwareinside = wheels.transform.Find("Left");
        for (int i = 0; i < 8; i++)
        {
            circularButtons.Add(basedonwhatbasedonthehardwareinside.Find($"Button ({i})").gameObject);
        }
        basedonwhatbasedonthehardwareinside = wheels.transform.Find("Right");
        for (int i = 0; i < 8; i++)
        {
            circularButtons.Add(basedonwhatbasedonthehardwareinside.Find($"Button ({i})").gameObject);
           
        }
        foreach (var item in circularButtons)
        {
            item.GetComponent<Button>().onClick.AddListener(delegate { Activate(item); });
        }
        LoadSettings();

        var script = wheels.transform.Find("Button").gameObject.AddComponent<ButtonScript>();
        wheels.transform.Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(wheels.transform.Find("Button").gameObject.GetComponent<ButtonScript>().Finish);
        gameObject.SetActive(false);
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    internal void LoadSettings()
    {
        ScrollManager.circularButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote0.Value;
        ScrollManager.circularButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote1.Value;
        ScrollManager.circularButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote2.Value;
        ScrollManager.circularButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote3.Value;
        ScrollManager.circularButtons[4].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote4.Value;
        ScrollManager.circularButtons[5].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote5.Value;
        ScrollManager.circularButtons[6].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote6.Value;
        ScrollManager.circularButtons[7].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote7.Value;
        ScrollManager.circularButtons[8].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote8.Value;
        ScrollManager.circularButtons[9].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote9.Value;
        ScrollManager.circularButtons[10].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote10.Value;
        ScrollManager.circularButtons[11].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote11.Value;
        ScrollManager.circularButtons[12].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote12.Value;
        ScrollManager.circularButtons[13].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote13.Value;
        ScrollManager.circularButtons[14].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote14.Value;
        ScrollManager.circularButtons[15].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote15.Value;
        ScrollManager.circularButtons[16].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote16.Value;
        ScrollManager.circularButtons[17].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote17.Value;
        ScrollManager.circularButtons[18].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote18.Value;
        ScrollManager.circularButtons[19].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote19.Value;
        ScrollManager.circularButtons[20].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote20.Value;
        ScrollManager.circularButtons[21].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote21.Value;
        ScrollManager.circularButtons[22].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote22.Value;
        ScrollManager.circularButtons[23].GetComponentInChildren<TextMeshProUGUI>().text = Settings.emote23.Value;
    }

    public static void SetupButtons(List<string> names)
    {
        if (!buttonTemplate)
        {
            return;
        }
        emoteNames = new List<string>();
        emoteNames.Clear();
        foreach (var item in names)
        {
            emoteNames.Add(item);
        }
        buttons.Clear();
        foreach (var item in emoteNames)
        {
            GameObject cum = GameObject.Instantiate(buttonTemplate);
            cum.name = item;
            cum.GetComponentInChildren<TextMeshProUGUI>().text = item;
            cum.transform.SetParent(buttonTemplate.transform.parent);
            cum.transform.transform.localScale = Vector3.one;
            buttons.Add(cum);
        }
        foreach (var item in buttons)
        {
            item.GetComponent<Button>().onClick.AddListener(item.GetComponent<ButtonScript>().PickNewEmote);
        }
    }
    void OnEnable()
    {
        UpdateButtonVisibility("");
    }

    public void SetEmoteInQuestion(GameObject e)
    {
        emoteInQuestion = e;
    }
    public void Activate(GameObject button)
    {
        //DebugClass.Log($"----------{button}");
        gameObject.transform.parent.Find("Wheels").gameObject.SetActive(false);
        emoteInQuestion = button;
        gameObject.SetActive(true);
    }

    public void Cancel()
    {
        gameObject.transform.parent.Find("Wheels").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void UpdateButtonVisibility(string filter)
    {
        List<GameObject> validButtons = new List<GameObject>();
        foreach (var item in buttons)
        {
            if (item.GetComponentInChildren<TextMeshProUGUI>().text.ToUpper().Contains(filter.ToUpper()))
            {
                validButtons.Add(item);
            }
            else
            {
                item.SetActive(false);
            }
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, validButtons.Count * 16);
        for (int i = 0; i < validButtons.Count; i++)
        {
            validButtons[i].SetActive(true);
            validButtons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -7.5f + (content.GetComponent<RectTransform>().sizeDelta.y * .5f) + (i * -16));
            validButtons[i].GetComponent<ButtonScript>().emoteInQuestion = emoteInQuestion;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}

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
using UnityEngine.Animations;
using UnityEngine.UI;
using EmotesAPI;
using UnityEngine.AddressableAssets;

internal static class AnimationReplacements
{
    internal static GameObject g;
    internal static void RunAll()
    {
        ChangeAnims();
        On.RoR2.UI.HUD.Awake += (orig, self) =>
        {
            orig(self);
            g = GameObject.Instantiate(Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/emotewheel.prefab"));
            foreach (var item in g.GetComponentsInChildren<TextMeshProUGUI>())
            {
                item.font = self.mainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("UpperLeftCluster").Find("MoneyRoot").Find("ValueText").GetComponent<TextMeshProUGUI>().font;
                item.fontMaterial = self.mainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("UpperLeftCluster").Find("MoneyRoot").Find("ValueText").GetComponent<TextMeshProUGUI>().fontMaterial;
                item.fontSharedMaterial = self.mainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("UpperLeftCluster").Find("MoneyRoot").Find("ValueText").GetComponent<TextMeshProUGUI>().fontSharedMaterial;
            }
            g.transform.SetParent(self.mainContainer.transform);
            g.transform.localPosition = new Vector3(0, 0, 0);
            var s = g.AddComponent<EmoteWheel>();
            foreach (var item in g.GetComponentsInChildren<Transform>())
            {
                if (item.gameObject.name.StartsWith("Emote"))
                {
                    s.gameObjects.Add(item.gameObject);
                }
                if (item.gameObject.name.StartsWith("MousePos"))
                {
                    s.text = item.gameObject;
                }
                if (item.gameObject.name == "Center")
                {
                    s.joy = item.gameObject.GetComponent<Image>();
                }
            }
        };
    }
    internal static bool setup = false;
    internal static void ChangeAnims()
    {
        On.RoR2.SurvivorCatalog.Init += (orig) =>
        {
            orig();
            if (!setup)
            {
                setup = true;
                ApplyAnimationStuff(RoR2Content.Survivors.Croco, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/acrid.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Mage, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/artificer.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Captain, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/captain.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Engi, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/engi.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Loader, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/loader.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Merc, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/merc.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Toolbot, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/mult.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Treebot, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/rex.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Commando, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/commando.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Huntress, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/huntress2022.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Bandit2, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/bandit.prefab");
                ApplyAnimationStuff(SurvivorCatalog.FindSurvivorDefFromBody(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidSurvivor/VoidSurvivorBody.prefab").WaitForCompletion()), "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/voidsurvivor.prefab");
                ApplyAnimationStuff(SurvivorCatalog.FindSurvivorDefFromBody(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Railgunner/RailgunnerBody.prefab").WaitForCompletion()), "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/railgunner.prefab");
                ApplyAnimationStuff(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Heretic/HereticBody.prefab").WaitForCompletion(), "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/heretic.prefab", 3);
            }
            //bodyPrefab = survivorDef.displayPrefab;
            //animcontroller = Resources.Load<GameObject>(resource);
            //animcontroller.transform.parent = bodyPrefab.GetComponent<ModelLocator>().modelTransform;
            //animcontroller.transform.localPosition = Vector3.zero;
            //animcontroller.transform.localEulerAngles = Vector3.zero;
            //smr1 = animcontroller.GetComponentInChildren<SkinnedMeshRenderer>();
            //smr2 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<SkinnedMeshRenderer>();
            //test = animcontroller.AddComponent<BoneMapper>();
            //test.smr1 = smr1;
            //test.smr2 = smr2;
            //test.a1 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<Animator>();
            //test.a2 = animcontroller.GetComponentInChildren<Animator>();
            //test.h = bodyPrefab.GetComponentInChildren<HealthComponent>();
        };
    }
    internal static void ApplyAnimationStuff(SurvivorDef index, string resource, int pos = 0)
    {
        ApplyAnimationStuff(index.bodyPrefab, resource, pos);
    }
    internal static void ApplyAnimationStuff(GameObject bodyPrefab, string resource, int pos = 0)
    {
        GameObject animcontroller = Assets.Load<GameObject>(resource);
        ApplyAnimationStuff(bodyPrefab, animcontroller, pos);
    }
    internal static void ApplyAnimationStuff(GameObject bodyPrefab, GameObject animcontroller, int pos = 0)
    {
        if (!animcontroller.GetComponentInChildren<Animator>().avatar.isHuman)
        {
            DebugClass.Log($"{animcontroller}'s avatar isn't humanoid, please fix it in unity!");
            return;
        }
        animcontroller.transform.parent = bodyPrefab.GetComponent<ModelLocator>().modelTransform;
        animcontroller.transform.localPosition = Vector3.zero;
        animcontroller.transform.localEulerAngles = Vector3.zero;
        animcontroller.transform.localScale = Vector3.one;
        SkinnedMeshRenderer smr1 = animcontroller.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
        SkinnedMeshRenderer smr2 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
        var test = animcontroller.AddComponent<BoneMapper>();
        test.smr1 = smr1;
        test.smr2 = smr2;
        for (int i = 0; i < smr1.bones.Length; i++)
        {
            if (smr1.bones[i].name != smr2.bones[i].name)
            {
                DebugClass.Log($"Fixing {bodyPrefab.name} bone order for emotes");
                List<Transform> trans = new List<Transform>();
                foreach (var item in smr2.bones)
                {
                    foreach (var item2 in smr1.bones)
                    {
                        if (item.name == item2.name)
                        {
                            trans.Add(item2);
                        }
                    }
                }
                smr1.bones = trans.ToArray();
                DebugClass.Log($"Done");
                break;
            }
        }
        test.a1 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<Animator>();
        test.a2 = animcontroller.GetComponentInChildren<Animator>();

        test.h = bodyPrefab.GetComponentInChildren<HealthComponent>();
        test.model = bodyPrefab.GetComponent<ModelLocator>().modelTransform.gameObject;
    }
}
public class CustomAnimationClip : MonoBehaviour
{
    public AnimationClip clip, secondaryClip; //DONT SUPPORT MULTI CLIP ANIMATIONS TO SYNC     //but why not? how hard could it be, I'm sure I left that note for a reason....
    internal bool looping;
    internal string wwiseEvent;
    internal bool syncronizeAudio;
    internal List<HumanBodyBones> soloIgnoredBones;
    internal List<HumanBodyBones> rootIgnoredBones;
    internal bool dimAudioWhenClose;
    internal bool stopOnAttack;
    internal bool stopOnMove;
    internal bool visibility;



    internal bool syncronizeAnimation;
    internal int syncPos;
    internal static List<float> syncTimer = new List<float>();
    internal static List<int> syncPlayerCount = new List<int>();
    internal CustomAnimationClip(AnimationClip _clip, bool _loop/*, bool _shouldSyncronize = false*/, string _wwiseEventName = "", string _wwiseStopEvent = "", HumanBodyBones[] rootBonesToIgnore = null, HumanBodyBones[] soloBonesToIgnore = null, AnimationClip _secondaryClip = null, bool dimWhenClose = false, bool stopWhenMove = false, bool stopWhenAttack = false, bool visible = true, bool syncAnim = false, bool syncAudio = false)
    {
        if (rootBonesToIgnore == null)
            rootBonesToIgnore = new HumanBodyBones[0];
        if (soloBonesToIgnore == null)
            soloBonesToIgnore = new HumanBodyBones[0];
        clip = _clip;
        if (_secondaryClip)
            secondaryClip = _secondaryClip;
        looping = _loop;
        //syncronizeAnimation = _shouldSyncronize;
        dimAudioWhenClose = dimWhenClose;
        stopOnAttack = stopWhenAttack;
        stopOnMove = stopWhenMove;
        visibility = visible;
        //int count = 0;
        //float timer = 0;
        if (_wwiseEventName != "" && _wwiseStopEvent == "")
        {
            //DebugClass.Log($"Error #2: wwiseEventName is declared but wwiseStopEvent isn't skipping sound implementation for [{clip.name}]");
        }
        else if (_wwiseEventName == "" && _wwiseStopEvent != "")
        {
            //DebugClass.Log($"Error #3: wwiseStopEvent is declared but wwiseEventName isn't skipping sound implementation for [{clip.name}]");
        }
        else if (_wwiseEventName != "")
        {
            //if (!_shouldSyncronize)
            //{
            BoneMapper.stopEvents.Add(_wwiseStopEvent);
            //}
            wwiseEvent = _wwiseEventName;
        }
        if (soloBonesToIgnore.Length != 0)
        {
            soloIgnoredBones = new List<HumanBodyBones>(soloBonesToIgnore);
        }
        else
        {
            soloIgnoredBones = new List<HumanBodyBones>();
        }

        if (rootBonesToIgnore.Length != 0)
        {
            rootIgnoredBones = new List<HumanBodyBones>(rootBonesToIgnore);
        }
        else
        {
            rootIgnoredBones = new List<HumanBodyBones>();
        }
        syncronizeAnimation = syncAnim;
        syncronizeAudio = syncAudio;
        syncPos = syncTimer.Count;
        syncTimer.Add(0);
        syncPlayerCount.Add(0);
    }
}
public class BoneMapper : MonoBehaviour
{
    public static List<string> stopEvents = new List<string>();
    public SkinnedMeshRenderer smr1, smr2;
    public Animator a1, a2;
    public HealthComponent h;
    public List<BonePair> pairs = new List<BonePair>();
    public float timer = 0;
    public static float caramellCount = 0;
    public static float caramellTimer = 0;
    public GameObject model;
    List<string> ignore = new List<string>();
    bool twopart = false;
    public static Dictionary<string, CustomAnimationClip> animClips = new Dictionary<string, CustomAnimationClip>();
    public CustomAnimationClip currentClip = null;
    internal static float Current_MSX = 69;
    internal static List<BoneMapper> allMappers = new List<BoneMapper>();
    private bool local = false;
    internal static bool moving = false;
    internal static bool attacking = false;
    public void PlayAnim(string s)
    {
        bool footL = false;
        bool footR = false;
        bool upperLegR = false;
        bool upperLegL = false;
        bool lowerLegR = false;
        bool lowerLegL = false;
        a2.enabled = true;
        List<string> dontAnimateUs = new List<string>();
        try
        {
            currentClip.clip.ToString();
            if (currentClip.syncronizeAnimation)
            {
                CustomAnimationClip.syncPlayerCount[currentClip.syncPos]--;
            }
        }
        catch (Exception)
        {
        }
        if (s != "none")
        {
            if (!animClips.ContainsKey(s))
            {
                DebugClass.Log($"No emote bound to the name [{s}]");
                return;
            }
            CustomEmotesAPI.Changed(s, this);
            currentClip = animClips[s];
            try
            {
                currentClip.clip.ToString();
            }
            catch (Exception)
            {
                return;
            }
            foreach (var item in currentClip.soloIgnoredBones)
            {
                if (item == HumanBodyBones.LeftFoot)
                {
                    footL = true;
                }
                if (item == HumanBodyBones.RightFoot)
                {
                    footR = true;
                }
                if (item == HumanBodyBones.LeftLowerLeg)
                {
                    lowerLegL = true;
                }
                if (item == HumanBodyBones.LeftUpperLeg)
                {
                    upperLegL = true;
                }
                if (item == HumanBodyBones.RightLowerLeg)
                {
                    lowerLegR = true;
                }
                if (item == HumanBodyBones.RightUpperLeg)
                {
                    upperLegR = true;
                }
                if (a2.GetBoneTransform(item))
                    dontAnimateUs.Add(a2.GetBoneTransform(item).name);
            }
            foreach (var item in currentClip.rootIgnoredBones)
            {

                if (item == HumanBodyBones.LeftUpperLeg || item == HumanBodyBones.Hips)
                {
                    upperLegL = true;
                    lowerLegL = true;
                    footL = true;
                }
                if (item == HumanBodyBones.RightUpperLeg || item == HumanBodyBones.Hips)
                {
                    upperLegR = true;
                    lowerLegR = true;
                    footR = true;
                }
                if (a2.GetBoneTransform(item))
                    dontAnimateUs.Add(a2.GetBoneTransform(item).name);
                foreach (var bone in a2.GetBoneTransform(item).GetComponentsInChildren<Transform>())
                {

                    dontAnimateUs.Add(bone.name);
                }
            }
        }
        else
        {
            CustomEmotesAPI.Changed(s, this);
        }
        bool left = upperLegL && lowerLegL && footL;
        bool right = upperLegR && lowerLegR && footR;
        Transform LeftLegIK = null;
        Transform RightLegIK = null;
        //DebugClass.Log($"----------{smr2.gameObject.ToString()}");
        for (int i = 0; i < smr2.bones.Length; i++)
        {
            try
            {
                if (right && (smr2.bones[i].gameObject.ToString() == "IKLegTarget.r (UnityEngine.GameObject)" || smr2.bones[i].gameObject.ToString() == "FootControl.r (UnityEngine.GameObject)"))
                {
                    RightLegIK = smr2.bones[i];
                }
                else if (left && (smr2.bones[i].gameObject.ToString() == "IKLegTarget.l (UnityEngine.GameObject)" || smr2.bones[i].gameObject.ToString() == "FootControl.l (UnityEngine.GameObject)"))
                {
                    LeftLegIK = smr2.bones[i];
                }
                if (smr2.bones[i].gameObject.GetComponent<ParentConstraint>() && !dontAnimateUs.Contains(smr2.bones[i].name))
                {
                    //DebugClass.Log($"-{i}---------{smr2.bones[i].gameObject}");
                    smr2.bones[i].gameObject.GetComponent<ParentConstraint>().constraintActive = true;
                }
                else if (dontAnimateUs.Contains(smr2.bones[i].name))
                {
                    //DebugClass.Log($"dontanimateme-{i}---------{smr2.bones[i].gameObject}");
                    smr2.bones[i].gameObject.GetComponent<ParentConstraint>().constraintActive = false;
                }
            }
            catch (Exception e)
            {
                DebugClass.Log($"{e}");
            }
        }
        if (left && LeftLegIK)//we can leave ik for the legs
        {
            if (LeftLegIK.gameObject.GetComponent<ParentConstraint>())
                LeftLegIK.gameObject.GetComponent<ParentConstraint>().constraintActive = false;
            foreach (var item in LeftLegIK.gameObject.GetComponentsInChildren<Transform>())
            {
                if (item.gameObject.GetComponent<ParentConstraint>())
                    item.gameObject.GetComponent<ParentConstraint>().constraintActive = false;
            }
        }
        if (right && RightLegIK)
        {
            if (RightLegIK.gameObject.GetComponent<ParentConstraint>())
                RightLegIK.gameObject.GetComponent<ParentConstraint>().constraintActive = false;
            foreach (var item in RightLegIK.gameObject.GetComponentsInChildren<Transform>())
            {
                if (item.gameObject.GetComponent<ParentConstraint>())
                    item.gameObject.GetComponent<ParentConstraint>().constraintActive = false;
            }
        }
        foreach (var item in stopEvents)
        {
            AkSoundEngine.PostEvent(item, gameObject);
        }
        if (s == "none")
        {
            a2.Play("none", -1, 0f);
            twopart = false;
            currentClip = null;
            return;
        }
        if (currentClip.wwiseEvent != "")
        {
            AkSoundEngine.PostEvent(currentClip.wwiseEvent, gameObject);
        }
        AnimatorOverrideController animController = new AnimatorOverrideController(a2.runtimeAnimatorController);


        if (currentClip.syncronizeAnimation)
        {
            CustomAnimationClip.syncPlayerCount[currentClip.syncPos]++;
            if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 1)
            {
                CustomAnimationClip.syncTimer[currentClip.syncPos] = 0;
            }
        }
        if (currentClip.secondaryClip)
        {
            if (CustomAnimationClip.syncTimer[currentClip.syncPos] > currentClip.clip.length)
            {
                animController["Floss"] = currentClip.secondaryClip;
                a2.runtimeAnimatorController = animController;
                a2.Play("Loop", -1, ((CustomAnimationClip.syncTimer[currentClip.syncPos] - currentClip.clip.length) % currentClip.secondaryClip.length) / currentClip.secondaryClip.length);
            }
            else
            {
                animController["Dab"] = currentClip.clip;
                animController["nobones"] = currentClip.secondaryClip;
                a2.runtimeAnimatorController = animController;
                a2.Play("PoopToLoop", -1, (CustomAnimationClip.syncTimer[currentClip.syncPos] % currentClip.clip.length) / currentClip.clip.length);
            }
        }
        else if (currentClip.looping)
        {
            animController["Floss"] = currentClip.clip;
            a2.runtimeAnimatorController = animController;
            if (currentClip.clip.length != 0)
            {
                a2.Play("Loop", -1, (CustomAnimationClip.syncTimer[currentClip.syncPos] % currentClip.clip.length) / currentClip.clip.length);
            }
            else
            {
                a2.Play("Loop", -1, 0);
            }
        }
        else
        {
            animController["Default Dance"] = currentClip.clip;
            a2.runtimeAnimatorController = animController;
            a2.Play("Poop", -1, (CustomAnimationClip.syncTimer[currentClip.syncPos] % currentClip.clip.length) / currentClip.clip.length);
        }
        twopart = false;
    }
    void AddIgnore(DynamicBone dynbone, Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            if (!dynbone.m_Exclusions.Contains(t.GetChild(i)))
            {
                ignore.Add(t.GetChild(i).name);
                AddIgnore(dynbone, t.GetChild(i));
            }
        }
    }
    void Start()
    {
        allMappers.Add(this);
        foreach (var item in allMappers)
        {
            //DebugClass.Log($"----------{item.a1.name}");
        }
        foreach (var item in model.GetComponents<DynamicBone>())
        {
            try
            {
                if (!item.m_Exclusions.Contains(item.m_Root))
                {
                    ignore.Add(item.m_Root.name);
                }
                AddIgnore(item, item.m_Root);
            }
            catch (Exception)
            {
            }
        }
        if (model.name.StartsWith("mdlLoader"))
        {
            Transform LClav = model.transform, RClav = model.transform;
            foreach (var item in model.GetComponentsInChildren<Transform>())
            {
                if (item.name == "clavicle.l")
                {
                    LClav = item;
                    ignore.Add(LClav.name);
                }
                if (item.name == "clavicle.r")
                {
                    RClav = item;
                    ignore.Add(RClav.name);
                }
            }
            foreach (var item in LClav.GetComponentsInChildren<Transform>())
            {
                ignore.Add(item.name);
            }
            foreach (var item in RClav.GetComponentsInChildren<Transform>())
            {
                ignore.Add(item.name);
            }
        }
        for (int i = 0; i < smr2.bones.Length; i++)
        {
            try
            {
                if (!ignore.Contains(smr2.bones[i].name))
                {
                    var s = new ConstraintSource();
                    s.sourceTransform = smr1.bones[i];
                    s.weight = 1;
                    smr2.bones[i].gameObject.AddComponent<ParentConstraint>().AddSource(s);
                }
            }
            catch (Exception)
            {
            }
        }
    }
    float interval = 0;
    void Update()
    {
        if (local)
        {
            for (int i = 0; i < CustomAnimationClip.syncPlayerCount.Count; i++)
            {
                if (CustomAnimationClip.syncPlayerCount[i] != 0)
                {
                    CustomAnimationClip.syncTimer[i] += Time.deltaTime;
                }
            }
            float closestDimmingSource = 20f;
            interval += Time.deltaTime;
            if (interval > 5f)
            {
                interval -= 5f;
            }
            foreach (var item in allMappers)
            {
                try
                {
                    if (item && item.a2.enabled && item.currentClip.dimAudioWhenClose)
                    {
                        if (Vector3.Distance(item.transform.parent.position, transform.position) < closestDimmingSource)
                        {
                            closestDimmingSource = Vector3.Distance(item.transform.position, transform.position);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            if (closestDimmingSource < 20f)
            {
                Current_MSX = Mathf.Lerp(Current_MSX, (closestDimmingSource / 20f) * CustomEmotesAPI.Actual_MSX, Time.deltaTime * 3);
                AkSoundEngine.SetRTPCValue("Volume_MSX", Current_MSX);
            }
            else if (Current_MSX != CustomEmotesAPI.Actual_MSX)
            {
                Current_MSX = Mathf.Lerp(Current_MSX, CustomEmotesAPI.Actual_MSX, Time.deltaTime * 3);
                AkSoundEngine.SetRTPCValue("Volume_MSX", Current_MSX);
            }
            try
            {
                if ((attacking && currentClip.stopOnAttack) || (moving && currentClip.stopOnMove))
                {
                    CustomEmotesAPI.PlayAnimation("none");
                }
            }
            catch (Exception)
            {
            }
        }
        else
        {
            try
            {
                var body = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                //if (body && Vector3.Distance(transform.parent.position, body.transform.position) < 4f)
                //{
                //    local = true;
                //}
                if (body.gameObject.GetComponent<ModelLocator>().modelTransform == transform.parent)
                {

                    local = true;
                }
            }
            catch (Exception)
            {
            }
        }
        if (a2.GetCurrentAnimatorStateInfo(0).IsName("none"))
        {
            if (!twopart)
            {
                twopart = true;
            }
            else
            {
                if (a2.enabled)
                {
                    AkSoundEngine.PostEvent("StopEmotes", gameObject);
                    AkSoundEngine.PostEvent("StopCaramell", gameObject);
                    if (smr2.transform.parent.gameObject.name == "mdlVoidSurvivor" || smr2.transform.parent.gameObject.name == "mdlMage")
                    {
                        smr2.transform.parent.gameObject.SetActive(false);
                        smr2.transform.parent.gameObject.SetActive(true);
                    }
                    for (int i = 0; i < smr2.bones.Length; i++)
                    {
                        try
                        {
                            if (smr2.bones[i].gameObject.GetComponent<ParentConstraint>())
                            {
                                smr2.bones[i].gameObject.GetComponent<ParentConstraint>().constraintActive = false;
                            }
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                }
                a1.enabled = true;
                a2.enabled = false;
            }
        }
        else
        {
            //a1.enabled = false;
            twopart = false;
        }
        if (h.health <= 0)
        {
            AkSoundEngine.PostEvent("StopEmotes", gameObject);
            AkSoundEngine.PostEvent("StopCaramell", gameObject);
            for (int i = 0; i < smr2.bones.Length; i++)
            {
                try
                {
                    if (smr2.bones[i].gameObject.GetComponent<ParentConstraint>())
                    {
                        smr2.bones[i].gameObject.GetComponent<ParentConstraint>().constraintActive = false;
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
            GameObject.Destroy(gameObject);
        }
    }
}
public class BonePair
{
    public Transform original, newiginal;
    public BonePair(Transform n, Transform o)
    {
        newiginal = n;
        original = o;
    }

    public void test()
    {

    }
}

internal static class Pain
{
    internal static Transform FindBone(SkinnedMeshRenderer mr, string name)
    {
        foreach (var item in mr.bones)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        DebugClass.Log($"couldnt find bone [{name}]");
        return mr.bones[0];
    }

    internal static Transform FindBone(List<Transform> bones, string name)
    {
        foreach (var item in bones)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        DebugClass.Log($"couldnt find bone [{name}]");
        return bones[0];
    }
}

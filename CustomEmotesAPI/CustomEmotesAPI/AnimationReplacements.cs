using RoR2;
using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Animations;
using UnityEngine.UI;
using EmotesAPI;
using UnityEngine.AddressableAssets;
using Generics.Dynamics;
using System.Security;
using System.Security.Permissions;

[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
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
                var money = self.moneyText.targetText;
                item.font = money.font;
                item.fontMaterial = money.fontMaterial;
                item.fontSharedMaterial = money.fontSharedMaterial;
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
    internal static void EnemyArmatures()
    {
        Import("RoR2/Base/Brother/BrotherBody.prefab", "@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/brother.prefab");
        Import("RoR2/Base/ClayBruiser/ClayBruiserBody.prefab", "@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/templar.prefab");

        Import("RoR2/DLC1/AcidLarva/AcidLarvaBody.prefab", "@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/enemies/AcidLarva1.prefab");
        Import("RoR2/Base/Titan/TitanGoldBody.prefab", "@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/enemies/Aurelionite.prefab");
        Import("RoR2/Base/Beetle/BeetleGuardBody.prefab", "@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/enemies/Beetle guard.prefab");
        Import("RoR2/Base/Beetle/BeetleQueen2Body.prefab", "@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/enemies/beetle queen1.prefab");

        Import("RoR2/DLC1/ClayGrenadier/ClayGrenadierBody.prefab", "@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/enemies/claygrenadier.prefab");
        Import("RoR2/Base/Bison/BisonBody.prefab", "@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/enemies/bison.prefab");
        //Import("RoR2/Base/Bell/BellBody.prefab", "@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/enemies/brass contraption2.prefab");
        Import("RoR2/DLC1/FlyingVermin/FlyingVerminBody.prefab", "@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/enemies/flyingvermin1.prefab");


        //CustomEmotesAPI.ImportArmature(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Beetle/BeetleBody.prefab").WaitForCompletion(), Assets.Load<GameObject>("@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/beetle.prefab"));
    }
    internal static void Import(string prefab, string skeleton)
    {
        CustomEmotesAPI.ImportArmature(Addressables.LoadAssetAsync<GameObject>(prefab).WaitForCompletion(), Assets.Load<GameObject>(skeleton));
    }
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
                ApplyAnimationStuff(RoR2Content.Survivors.Commando, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/commandoFixed.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Huntress, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/huntress2022.prefab");
                ApplyAnimationStuff(RoR2Content.Survivors.Bandit2, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/bandit.prefab");
                ApplyAnimationStuff(SurvivorCatalog.FindSurvivorDefFromBody(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidSurvivor/VoidSurvivorBody.prefab").WaitForCompletion()), "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/voidsurvivor.prefab");
                ApplyAnimationStuff(SurvivorCatalog.FindSurvivorDefFromBody(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Railgunner/RailgunnerBody.prefab").WaitForCompletion()), "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/railgunner.prefab");
                ApplyAnimationStuff(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Heretic/HereticBody.prefab").WaitForCompletion(), "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/heretic.prefab", 3);

                EnemyArmatures();

                //CustomEmotesAPI.ImportArmature(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Brother/BrotherBody.prefab").WaitForCompletion(), Assets.Load<GameObject>("@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/brother.prefab"));
                //CustomEmotesAPI.ImportArmature(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Brother/BrotherHurtBody.prefab").WaitForCompletion(), Assets.Load<GameObject>("@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/brother.prefab"));

                foreach (var item in SurvivorCatalog.allSurvivorDefs)
                {
                    if (item.bodyPrefab.name == "RobPaladinBody" && Settings.Paladin.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/animPaladin.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele, true);
                    }
                    else if (item.bodyPrefab.name == "EnforcerBody" && Settings.Enforcer.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/enforcer.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    else if (item.bodyPrefab.name == "NemesisEnforcerBody" && Settings.Enforcer.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/nemforcer.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    else if (item.bodyPrefab.name == "CHEF" && Settings.Chef.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/chef.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele, true);
                    }
                    else if (item.bodyPrefab.name == "HolomancerBody" && Settings.Holomancer.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/holomancer.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    else if (item.bodyPrefab.name == "SettBody" && Settings.Sett.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/Sett.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    else if (item.bodyPrefab.name == "TracerBody" && Settings.Tracer.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/imalreadytracer.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    else if (item.bodyPrefab.name == "JavangleHouse" && Settings.House.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/house.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    else if (item.bodyPrefab.name == "HenryBody" && Settings.Henry.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/henry.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    else if (item.bodyPrefab.name == "AurelionSolBody" && Settings.SolSupport.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/solreordered4.prefab");
                        //var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/soltest2.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    else if (item.bodyPrefab.name == "Katarina" && Settings.Katarina.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/katarina.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    else if (item.bodyPrefab.name == "MinerBody" && Settings.Miner.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/miner.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele, true);
                    }
                    else if (item.bodyPrefab.name == "PhoenixBody" && Settings.Phoenix.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/ppwright.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    else if (item.bodyPrefab.name == "ScoutBody" && Settings.Scout.Value)
                    {
                        var skele = Assets.Load<GameObject>("@CustomEmotesAPI_fineilldoitmyself:assets/fineilldoitmyself/scout.prefab");
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele);
                    }
                    //else
                    //{
                    //    DebugClass.Log($"----------{item.bodyPrefab.name}");
                    //    Settings.DebugBones(item.bodyPrefab);
                    //}
                }
            }
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
    internal static void ApplyAnimationStuff(GameObject bodyPrefab, GameObject animcontroller, int pos = 0, bool hidemeshes = true, bool jank = false)
    {
        if (!animcontroller.GetComponentInChildren<Animator>().avatar.isHuman)
        {
            DebugClass.Log($"{animcontroller}'s avatar isn't humanoid, please fix it in unity!");
            return;
        }
        if (hidemeshes)
        {
            foreach (var item in animcontroller.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.sharedMesh = null;
            }
            foreach (var item in animcontroller.GetComponentsInChildren<MeshFilter>())
            {
                item.sharedMesh = null;
            }
        }
        animcontroller.transform.parent = bodyPrefab.GetComponent<ModelLocator>().modelTransform;
        animcontroller.transform.localPosition = Vector3.zero;
        animcontroller.transform.localEulerAngles = Vector3.zero;
        animcontroller.transform.localScale = Vector3.one;


        var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Beetle/BeetleBody.prefab").WaitForCompletion();
        if (fab == bodyPrefab)
        {
            List<Transform> t = new List<Transform>();
            foreach (var item in bodyPrefab.GetComponentsInChildren<Transform>())
            {
                if (!item.name.Contains("Hurtbox") && !item.name.Contains("BeetleBody") && !item.name.Contains("Mesh") && !item.name.Contains("mdl"))
                {
                    t.Add(item);
                }
            }
            Transform temp = t[14];
            t[14] = t[11];
            t[11] = temp;
            temp = t[15];
            t[15] = t[12];
            t[12] = temp;
            temp = t[16];
            t[16] = t[13];
            t[13] = temp;
            foreach (var item in bodyPrefab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.bones = t.ToArray();
            }
        }


        SkinnedMeshRenderer smr1 = animcontroller.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
        SkinnedMeshRenderer smr2 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
        int matchingBones = 0;
        while (true)
        {
            foreach (var smr1bone in smr1.bones)
            {
                foreach (var smr2bone in smr2.bones)
                {
                    if (smr1bone.name == smr2bone.name)
                    {
                        matchingBones++;
                    }
                }
            }
            if (matchingBones < 5 && pos + 1 < bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>().Length)
            {
                pos++;
                smr2 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
                matchingBones = 0;
            }
            else
            {
                break;
            }
        }
        var test = animcontroller.AddComponent<BoneMapper>();
        test.jank = jank;
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

        var nuts = Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/animationreplacements/bandit.prefab");
        float banditScale = Vector3.Distance(nuts.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.Head).position, nuts.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).position);

        float currScale = Vector3.Distance(animcontroller.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.Head).position, animcontroller.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).position);

        test.scale = currScale / banditScale;

        test.h = bodyPrefab.GetComponentInChildren<HealthComponent>();
        test.model = bodyPrefab.GetComponent<ModelLocator>().modelTransform.gameObject;
    }
}
public struct JoinSpot
{
    public string name;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public JoinSpot(string _name, Vector3 _position, Vector3 _rotation, Vector3 _scale)
    {
        name = _name;
        position = _position;
        rotation = _rotation;
        scale = _scale;
    }
    public JoinSpot(string _name, Vector3 _position)
    {
        name = _name;
        position = _position;
        rotation = Vector3.zero;
        scale = Vector3.one;
    }
}
public class CustomAnimationClip : MonoBehaviour
{
    public AnimationClip[] clip, secondaryClip; //DONT SUPPORT MULTI CLIP ANIMATIONS TO SYNC     //but why not? how hard could it be, I'm sure I left that note for a reason....  //it was for a reason, but it works now
    internal bool looping;
    internal string wwiseEvent;
    internal bool syncronizeAudio;
    internal List<HumanBodyBones> soloIgnoredBones;
    internal List<HumanBodyBones> rootIgnoredBones;
    internal bool dimAudioWhenClose;
    internal bool stopOnAttack;
    internal bool stopOnMove;
    internal bool visibility;
    public int startPref, joinPref;
    public JoinSpot[] joinSpots;


    internal bool syncronizeAnimation;
    public int syncPos;
    public static List<float> syncTimer = new List<float>();
    public static List<int> syncPlayerCount = new List<int>();

    internal CustomAnimationClip(AnimationClip[] _clip, bool _loop/*, bool _shouldSyncronize = false*/, string[] _wwiseEventName = null, string[] _wwiseStopEvent = null, HumanBodyBones[] rootBonesToIgnore = null, HumanBodyBones[] soloBonesToIgnore = null, AnimationClip[] _secondaryClip = null, bool dimWhenClose = false, bool stopWhenMove = false, bool stopWhenAttack = false, bool visible = true, bool syncAnim = false, bool syncAudio = false, int startPreference = -1, int joinPreference = -1, JoinSpot[] _joinSpots = null)
    {
        if (rootBonesToIgnore == null)
            rootBonesToIgnore = new HumanBodyBones[0];
        if (soloBonesToIgnore == null)
            soloBonesToIgnore = new HumanBodyBones[0];
        clip = _clip;
        secondaryClip = _secondaryClip;
        looping = _loop;
        //syncronizeAnimation = _shouldSyncronize;
        dimAudioWhenClose = dimWhenClose;
        stopOnAttack = stopWhenAttack;
        stopOnMove = stopWhenMove;
        visibility = visible;
        joinPref = joinPreference;
        startPref = startPreference;
        //int count = 0;
        //float timer = 0;
        //if (_wwiseEventName != "" && _wwiseStopEvent == "")
        //{
        //    //DebugClass.Log($"Error #2: wwiseEventName is declared but wwiseStopEvent isn't skipping sound implementation for [{clip.name}]");
        //}
        //else if (_wwiseEventName == "" && _wwiseStopEvent != "")
        //{
        //    //DebugClass.Log($"Error #3: wwiseStopEvent is declared but wwiseEventName isn't skipping sound implementation for [{clip.name}]");
        //}
        //else if (_wwiseEventName != "")
        //{
        //    //if (!_shouldSyncronize)
        //    //{
        //    BoneMapper.stopEvents.Add(_wwiseStopEvent);
        //    //}
        //    wwiseEvent = _wwiseEventName;
        //}
        string[] wwiseEvents;
        string[] wwiseStopEvents;
        if (_wwiseEventName == null)
        {
            wwiseEvents = new string[] { "" };
            wwiseStopEvents = new string[] { "" };
        }
        else
        {
            wwiseEvents = _wwiseEventName;
            wwiseStopEvents = _wwiseStopEvent;
        }
        BoneMapper.stopEvents.Add(wwiseStopEvents);
        BoneMapper.startEvents.Add(wwiseEvents);
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

        if (_joinSpots == null)
            _joinSpots = new JoinSpot[0];
        joinSpots = _joinSpots;
    }
}
public class BoneMapper : MonoBehaviour
{
    public static List<string[]> stopEvents = new List<string[]>();
    public static List<string[]> startEvents = new List<string[]>();
    internal List<GameObject> audioObjects = new List<GameObject>();
    public SkinnedMeshRenderer smr1, smr2;
    public Animator a1, a2;
    public HealthComponent h;
    public List<BonePair> pairs = new List<BonePair>();
    public float timer = 0;
    public GameObject model;
    List<string> ignore = new List<string>();
    bool twopart = false;
    public static Dictionary<string, CustomAnimationClip> animClips = new Dictionary<string, CustomAnimationClip>();
    public CustomAnimationClip currentClip = null;
    internal static float Current_MSX = 69;
    internal static List<BoneMapper> allMappers = new List<BoneMapper>();
    public bool local = false;
    internal static bool moving = false;
    internal static bool attacking = false;
    public bool jank = false;
    public List<GameObject> props = new List<GameObject>();
    public float scale = 1.0f;
    int currEvent = 0;
    public float autoWalkSpeed = 0;
    public bool overrideMoveSpeed = false;
    public GameObject currentEmoteSpot = null;

    public void PlayAnim(string s, int pos)
    {
        if (s != "none")
        {
            if (!animClips.ContainsKey(s))
            {
                DebugClass.Log($"No emote bound to the name [{s}]");
                return;
            }
            try
            {
                animClips[s].ToString();
            }
            catch (Exception)
            {
                CustomEmotesAPI.Changed(s, this);
                return;
            }
        }
        bool footL = false;
        bool footR = false;
        bool upperLegR = false;
        bool upperLegL = false;
        bool lowerLegR = false;
        bool lowerLegL = false;
        a2.enabled = true;
        if (a2.transform.parent.GetComponent<InverseKinematics>() && a2.name != "loader")
        {
            a2.transform.parent.GetComponent<InverseKinematics>().enabled = false;
        }
        List<string> dontAnimateUs = new List<string>();
        try
        {
            currentClip.clip[0].ToString();
            if (currentClip.syncronizeAnimation || currentClip.syncronizeAudio)
            {
                CustomAnimationClip.syncPlayerCount[currentClip.syncPos]--;
            }
            if (stopEvents[currentClip.syncPos][currEvent] != "")
            {
                if (!currentClip.syncronizeAudio)
                {
                    AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], audioObjects[currentClip.syncPos]);
                }
                audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);

                if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 0 && currentClip.syncronizeAudio)
                {
                    foreach (var item in allMappers)
                    {
                        AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], item.audioObjects[currentClip.syncPos]);
                        item.audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);
                    }
                }
            }
        }
        catch (Exception)
        {
        }
        currEvent = 0;
        if (s != "none")
        {
            currentClip = animClips[s];
            try
            {
                currentClip.clip[0].ToString();
            }
            catch (Exception)
            {
                return;
            }
            if (pos == -2)
            {
                if (CustomAnimationClip.syncPlayerCount[animClips[s].syncPos] == 0)
                {
                    pos = animClips[s].startPref;
                }
                else
                {
                    pos = animClips[s].joinPref;
                }
            }
            if (pos == -1)
            {
                pos = UnityEngine.Random.Range(0, currentClip.clip.Length);
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
        bool left = upperLegL && lowerLegL && footL;
        bool right = upperLegR && lowerLegR && footR;
        Transform LeftLegIK = null;
        Transform RightLegIK = null;
        //DebugClass.Log($"----------{smr2.gameObject.ToString()}");
        if (!jank)
        {
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
        }
        else
        {
            a1.enabled = false;
        }
        //foreach (var item in stopEvents)
        //{
        //    AkSoundEngine.PostEvent(item, gameObject);
        //}
        if (s == "none")
        {
            a2.Play("none", -1, 0f);
            twopart = false;
            currentClip = null;

            NewAnimation(null);
            CustomEmotesAPI.Changed(s, this);

            return;
        }
        AnimatorOverrideController animController = new AnimatorOverrideController(a2.runtimeAnimatorController);
        if (currentClip.syncronizeAnimation || currentClip.syncronizeAudio)
        {
            CustomAnimationClip.syncPlayerCount[currentClip.syncPos]++;
        }
        if (currentClip.syncronizeAnimation && CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 1)
        {
            CustomAnimationClip.syncTimer[currentClip.syncPos] = 0;
        }
        if (startEvents[currentClip.syncPos][currEvent] != "")
        {
            if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 1 && currentClip.syncronizeAudio)
            {
                currEvent = UnityEngine.Random.Range(0, startEvents[currentClip.syncPos].Length);
                foreach (var item in allMappers)
                {
                    item.currEvent = currEvent;
                    AkSoundEngine.PostEvent(startEvents[currentClip.syncPos][currEvent], item.audioObjects[currentClip.syncPos]);
                }
            }
            else if (!currentClip.syncronizeAudio)
            {
                currEvent = UnityEngine.Random.Range(0, startEvents[currentClip.syncPos].Length);
                AkSoundEngine.PostEvent(startEvents[currentClip.syncPos][currEvent], audioObjects[currentClip.syncPos]);
            }
            audioObjects[currentClip.syncPos].transform.localPosition = Vector3.zero;
        }
        if (currentClip.secondaryClip != null && currentClip.secondaryClip.Length != 0)
        {
            if (true)
            {
                if (CustomAnimationClip.syncTimer[currentClip.syncPos] > currentClip.clip[pos].length)
                {
                    animController["Floss"] = currentClip.secondaryClip[pos];
                    a2.runtimeAnimatorController = animController;
                    a2.Play("Loop", -1, ((CustomAnimationClip.syncTimer[currentClip.syncPos] - currentClip.clip[pos].length) % currentClip.secondaryClip[pos].length) / currentClip.secondaryClip[pos].length);
                }
                else
                {
                    animController["Dab"] = currentClip.clip[pos];
                    animController["nobones"] = currentClip.secondaryClip[pos];
                    a2.runtimeAnimatorController = animController;
                    a2.Play("PoopToLoop", -1, (CustomAnimationClip.syncTimer[currentClip.syncPos] % currentClip.clip[pos].length) / currentClip.clip[pos].length);
                }
            }
        }
        else if (currentClip.looping)
        {
            animController["Floss"] = currentClip.clip[pos];
            a2.runtimeAnimatorController = animController;
            if (currentClip.clip[pos].length != 0)
            {
                a2.Play("Loop", -1, (CustomAnimationClip.syncTimer[currentClip.syncPos] % currentClip.clip[pos].length) / currentClip.clip[pos].length);
            }
            else
            {
                a2.Play("Loop", -1, 0);
            }
        }
        else
        {
            animController["Default Dance"] = currentClip.clip[pos];
            a2.runtimeAnimatorController = animController;
            a2.Play("Poop", -1, (CustomAnimationClip.syncTimer[currentClip.syncPos] % currentClip.clip[pos].length) / currentClip.clip[pos].length);
        }
        twopart = false;
        NewAnimation(currentClip.joinSpots);
        CustomEmotesAPI.Changed(s, this);
    }
    internal void NewAnimation(JoinSpot[] locations)
    {
        foreach (var item in props)
        {
            GameObject.Destroy(item);
        }
        props.Clear();
        if (locations != null)
        {
            for (int i = 0; i < locations.Length; i++)
            {
                props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/emotejoiner/emotespot1.prefab")));
                props[props.Count - 1].transform.SetParent(this.transform.parent);
                props[props.Count - 1].transform.localPosition = locations[i].position;
                props[props.Count - 1].transform.localEulerAngles = locations[i].rotation;
                props[props.Count - 1].transform.localScale = locations[i].scale;
                props[props.Count - 1].AddComponent<EmoteLocation>().owner = this;
                props[props.Count - 1].transform.parent = null;
            }
        }
        autoWalkSpeed = 0;
        overrideMoveSpeed = false;
        parentGameObject = null;
        transform.parent.GetComponent<CharacterModel>().body.GetComponent<ModelLocator>().modelBaseTransform.localEulerAngles = Vector3.zero;
        transform.parent.GetComponent<CharacterModel>().body.GetComponent<CharacterDirection>().enabled = true;
        transform.parent.localScale = ogScale;
    }
    public void ScaleProps()
    {
        Vector3 parentScale = this.transform.parent.transform.localScale;
        foreach (var item in props)
        {
            item.transform.localScale = new Vector3(scale / parentScale.x, scale / parentScale.y, scale / parentScale.z);
        }
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
        foreach (var item in startEvents)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.transform.localPosition = new Vector3(0, -10000, 0);
            audioObjects.Add(obj);
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
        int offset = 0;
        bool nuclear = true;
        //for (int i = 0; i + offset < smr2.bones.Length; i++)
        //{
        //    try
        //    {
        //        if (!ignore.Contains(smr2.bones[i].name))
        //        {
        //            while (smr2.bones[i + offset].name != smr1.bones[i].name)
        //            {
        //                offset++;
        //                if (i + offset > smr1.bones.Length - 1)
        //                {
        //                    nuclear = true;
        //                    DebugClass.Log($"----------ah fuck");
        //                    break;
        //                }
        //                else
        //                {
        //                    DebugClass.Log($"offset test {i + offset} [{smr2.bones[i + offset]}]   {i} [{smr1.bones[i]}]");
        //                }
        //            }
        //            var s = new ConstraintSource();
        //            s.sourceTransform = smr1.bones[i];
        //            s.weight = 1;
        //            smr2.bones[i + offset].gameObject.AddComponent<ParentConstraint>().AddSource(s);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        if (nuclear)
        {
            foreach (var smr1bone in smr1.bones)
            {
                foreach (var smr2bone in smr2.bones)
                {
                    if (smr1bone.name == smr2bone.name && !smr2bone.GetComponent<ParentConstraint>())
                    {
                        var s = new ConstraintSource();
                        s.sourceTransform = smr1bone;
                        s.weight = 1;
                        smr2bone.gameObject.AddComponent<ParentConstraint>().AddSource(s);
                    }
                }
            }
        }
        if (jank)
        {
            for (int i = 0; i < smr2.bones.Length; i++)
            {
                try
                {
                    if (smr2.bones[i].gameObject.GetComponent<ParentConstraint>())
                    {
                        //DebugClass.Log($"-{i}---------{smr2.bones[i].gameObject}");
                        smr2.bones[i].gameObject.GetComponent<ParentConstraint>().constraintActive = true;
                    }
                }
                catch (Exception e)
                {
                    DebugClass.Log($"{e}");
                }
            }
        }
    }
    GameObject parentGameObject;
    bool positionLock, rotationLock, scaleLock;
    public void AssignParentGameObject(GameObject youAreTheFather, bool lockPosition, bool lockRotation, bool lockScale)
    {
        ogScale = transform.parent.localScale;
        scaleDiff = ogScale - Vector3.one;
        parentGameObject = youAreTheFather;
        positionLock = lockPosition;
        rotationLock = lockRotation;
        scaleLock = lockScale;
    }
    float interval = 0;
    Vector3 ogScale = Vector3.one;
    Vector3 scaleDiff = Vector3.one;
    void Update()
    {
        if (parentGameObject)
        {
            if (positionLock)
            {
                transform.parent.GetComponent<CharacterModel>().body.GetComponentInChildren<KinematicCharacterController.KinematicCharacterMotor>().SetPosition(parentGameObject.transform.position);
                transform.parent.GetComponent<CharacterModel>().body.GetComponent<CharacterMotor>().velocity = Vector3.zero;
            }
            if (rotationLock)
            {
                transform.parent.GetComponent<CharacterModel>().body.GetComponent<CharacterDirection>().enabled = false;
                transform.parent.GetComponent<CharacterModel>().body.GetComponent<ModelLocator>().modelBaseTransform.rotation = parentGameObject.transform.rotation;
            }
            if (scaleLock)
            {
                transform.parent.localScale = parentGameObject.transform.localScale + scaleDiff;
            }
        }
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
            if (closestDimmingSource < 20f && Settings.DimmingSpheres.Value)
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
                if (body.gameObject.GetComponent<ModelLocator>().modelTransform == transform.parent)
                {
                    local = true;
                    CustomEmotesAPI.localMapper = this;
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
                    if (smr2.transform.parent.gameObject.name == "mdlVoidSurvivor" || smr2.transform.parent.gameObject.name == "mdlMage")
                    {
                        smr2.transform.parent.gameObject.SetActive(false);
                        smr2.transform.parent.gameObject.SetActive(true);
                    }
                    if (!jank)
                    {
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
                }
                //DebugClass.Log($"----------{a1}");
                a1.enabled = true;
                a2.enabled = false;
                if (a2.transform.parent.GetComponent<InverseKinematics>() && a2.name != "loader")
                {
                    a2.transform.parent.GetComponent<InverseKinematics>().enabled = true;
                }
                try
                {
                    currentClip.clip.ToString();
                    CustomEmotesAPI.Changed("none", this);
                    foreach (var item in props)
                    {
                        GameObject.Destroy(item);
                    }
                    props.Clear();
                    if (currentClip.syncronizeAnimation || currentClip.syncronizeAudio)
                    {
                        CustomAnimationClip.syncPlayerCount[currentClip.syncPos]--;
                    }
                    if (stopEvents[currentClip.syncPos][currEvent] != "")
                    {
                        if (!currentClip.syncronizeAudio)
                        {
                            AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], audioObjects[currentClip.syncPos]);
                        }
                        audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);

                        if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 0 && currentClip.syncronizeAudio)
                        {
                            foreach (var item in allMappers)
                            {
                                AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], item.audioObjects[currentClip.syncPos]);
                                item.audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);
                            }
                        }
                    }
                    currentClip = null;
                }
                catch (Exception)
                {
                }
            }
        }
        else
        {
            //a1.enabled = false;
            twopart = false;
        }
        if (autoWalkSpeed != 0)
        {
            if (overrideMoveSpeed)
            {
                transform.parent.GetComponent<CharacterModel>().body.moveSpeed = autoWalkSpeed;
            }
            transform.parent.GetComponent<CharacterModel>().body.GetComponent<CharacterMotor>().moveDirection = transform.parent.GetComponent<CharacterModel>().body.GetComponent<CharacterDirection>().forward * autoWalkSpeed;
        }
        if (h.health <= 0)
        {
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
    public void SetAutoWalk(float speed, bool overrideBaseMovement)
    {
        autoWalkSpeed = speed;
        overrideMoveSpeed = overrideBaseMovement;
    }
    void FixedUpdate()
    {
        //if (autoWalkSpeed != 0)
        //{
        //    if (overrideMoveSpeed)
        //    {
        //        transform.parent.GetComponent<CharacterModel>().body.GetComponent<InputBankTest>().moveVector = transform.parent.GetComponent<CharacterModel>().body.GetComponent<CharacterDirection>().forward * autoWalkSpeed;
        //    }
        //    else
        //    {
        //        transform.parent.GetComponent<CharacterModel>().body.GetComponent<CharacterMotor>().moveDirection = transform.parent.GetComponent<CharacterModel>().body.GetComponent<CharacterDirection>().forward * autoWalkSpeed;
        //    }
        //}
    }
    void OnDestroy()
    {
        //foreach (var item in allMappers)
        //{
        //    AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos], item.audioObjects[currentClip.syncPos]);
        //}
        try
        {
            currentClip.clip.ToString();
            if (currentClip.syncronizeAnimation || currentClip.syncronizeAudio)
            {
                if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] > 0)
                    CustomAnimationClip.syncPlayerCount[currentClip.syncPos]--;
            }
            if (stopEvents[currentClip.syncPos][currEvent] != "")
            {
                if (!currentClip.syncronizeAudio)
                {
                    AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], audioObjects[currentClip.syncPos]);
                }
                audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);

                if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 0 && currentClip.syncronizeAudio)
                {
                    foreach (var item in allMappers)
                    {
                        AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], item.audioObjects[currentClip.syncPos]);
                        item.audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);
                    }
                }
                AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], audioObjects[currentClip.syncPos]);
            }
            BoneMapper.allMappers.Remove(this);
            currentClip = null;
        }
        catch (Exception e)
        {
            BoneMapper.allMappers.Remove(this);
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

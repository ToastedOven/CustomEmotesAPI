using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using RoR2.UI;
using RiskOfOptions;
using UnityEngine.Rendering.PostProcessing;
using R2API.Networking;
using UnityEngine.Networking;
using R2API.Networking.Interfaces;
using System.Globalization;
using BepInEx.Configuration;
using UnityEngine.AddressableAssets;

namespace EmotesAPI
{
    [BepInDependency("com.gemumoddo.MoistureUpset", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInPlugin("com.weliveinasociety.CustomEmotesAPI", "Custom Emotes API", VERSION)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [R2APISubmoduleDependency("PrefabAPI", "ResourcesAPI", "NetworkingAPI")]
    public class CustomEmotesAPI : BaseUnityPlugin
    {
        public struct NameTokenWithSprite
        {
            public string nameToken;
            public Sprite sprite;
        }
        public static List<NameTokenWithSprite> nameTokenSpritePairs = new List<NameTokenWithSprite>();
        public static bool CreateNameTokenSpritePair(string nameToken, Sprite sprite)
        {
            NameTokenWithSprite temp = new NameTokenWithSprite();
            temp.nameToken = nameToken;
            temp.sprite = sprite;
            if (nameTokenSpritePairs.Contains(temp))
            {
                return false;
            }
            nameTokenSpritePairs.Add(temp);
            return true;
        }
        void CreateBaseNameTokenPairs()
        {
            CreateNameTokenSpritePair("CAPTAIN_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/captain.png"));
            CreateNameTokenSpritePair("COMMANDO_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/commando.png"));
            CreateNameTokenSpritePair("MERC_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/merc.png"));
            CreateNameTokenSpritePair("ENGI_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/engi.png"));
            CreateNameTokenSpritePair("HUNTRESS_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/huntress.png"));
            CreateNameTokenSpritePair("MAGE_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/artificer.png"));
            CreateNameTokenSpritePair("TOOLBOT_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/mult.png"));
            CreateNameTokenSpritePair("TREEBOT_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/rex.png"));
            CreateNameTokenSpritePair("LOADER_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/loader.png"));
            CreateNameTokenSpritePair("CROCO_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/acrid.png"));
            CreateNameTokenSpritePair("BANDIT2_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/bandit.png"));
            CreateNameTokenSpritePair("VOIDSURVIVOR_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/voidfiend.png"));
            CreateNameTokenSpritePair("RAILGUNNER_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/railgunner.png"));
            //CreateNameTokenSpritePair("HERETIC_BODY_NAME", Assets.Load<Sprite>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/heretic.png"));
        }
        internal static List<string> allClipNames = new List<string>();
        internal static void LoadResource(string resource)
        {
            Assets.AddBundle($"{resource}");
        }
        internal static bool GetKey(ConfigEntry<KeyboardShortcut> entry)
        {
            foreach (var item in entry.Value.Modifiers)
            {
                if (!Input.GetKey(item))
                {
                    return false;
                }
            }
            return Input.GetKey(entry.Value.MainKey);
        }
        internal static bool GetKeyPressed(ConfigEntry<KeyboardShortcut> entry)
        {
            foreach (var item in entry.Value.Modifiers)
            {
                if (!Input.GetKey(item))
                {
                    return false;
                }
            }
            return Input.GetKeyDown(entry.Value.MainKey);
        }
        public const string VERSION = "1.4.0";
        internal static float Actual_MSX = 69;
        public static CustomEmotesAPI instance;
        public void Awake()
        {
            instance = this;
            DebugClass.SetLogger(base.Logger);
            CustomEmotesAPI.LoadResource("customemotespackage");
            CustomEmotesAPI.LoadResource("fineilldoitmyself");
            CustomEmotesAPI.LoadResource("enemyskeletons");
            //if (!BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.gemumoddo.MoistureUpset"))
            //{
            //}
            CustomEmotesAPI.LoadResource("moisture_animationreplacements"); // I don't remember what's in here that makes importing emotes work, don't @ me
            Settings.RunAll();
            Register.Init();
            AnimationReplacements.RunAll();
            float WhosSteveJobs = 69420;
            CreateBaseNameTokenPairs();
            if (Settings.DontTouchThis.Value < 101)
            {
                WhosSteveJobs = Settings.DontTouchThis.Value;
            }
            On.RoR2.SceneCatalog.OnActiveSceneChanged += (orig, self, scene) =>
            {
                orig(self, scene);
                AkSoundEngine.SetRTPCValue("Volume_Emotes", Settings.EmotesVolume.Value);
                if (allClipNames != null)
                {
                    ScrollManager.SetupButtons(allClipNames);
                }
                AkSoundEngine.SetRTPCValue("Volume_MSX", Actual_MSX);
                for (int i = 0; i < CustomAnimationClip.syncPlayerCount.Count; i++)
                {
                    CustomAnimationClip.syncTimer[i] = 0;
                    CustomAnimationClip.syncPlayerCount[i] = 0;
                }
                if (scene.name == "title" && WhosSteveJobs < 101)
                {
                    AkSoundEngine.SetRTPCValue("Volume_MSX", WhosSteveJobs);
                    Actual_MSX = WhosSteveJobs;
                    WhosSteveJobs = 69420;
                }
                BoneMapper.allMappers.Clear();
                localMapper = null;
            };
            On.RoR2.AudioManager.VolumeConVar.SetString += (orig, self, newValue) =>
            {
                orig(self, newValue);
                //Volume_MSX
                if (self.GetFieldValue<string>("rtpcName") == "Volume_MSX" && WhosSteveJobs > 100)
                {
                    Actual_MSX = float.Parse(newValue, CultureInfo.InvariantCulture);
                    BoneMapper.Current_MSX = Actual_MSX;
                    Settings.DontTouchThis.Value = float.Parse(newValue, CultureInfo.InvariantCulture);
                }
            };
            On.RoR2.PlayerCharacterMasterController.FixedUpdate += (orig, self) =>
            {
                orig(self);
                if (CustomEmotesAPI.GetKey(Settings.EmoteWheel))
                {
                    if (self.hasEffectiveAuthority && self.GetFieldValue<InputBankTest>("bodyInputs"))
                    {
                        bool newState = false;
                        bool newState2 = false;
                        bool newState3 = false;
                        bool newState4 = false;
                        LocalUser localUser;
                        Rewired.Player player;
                        CameraRigController cameraRigController;
                        bool doIt = false;
                        if (!self.networkUser)
                        {
                            localUser = null;
                            player = null;
                            cameraRigController = null;
                            doIt = false;
                        }
                        else
                        {
                            localUser = self.networkUser.localUser;
                            player = self.networkUser.inputPlayer;
                            cameraRigController = self.networkUser.cameraRigController;
                            doIt = localUser != null && player != null && cameraRigController && !localUser.isUIFocused && cameraRigController.isControlAllowed;
                        }
                        if (doIt)
                        {
                            newState = player.GetButton(7) && !CustomEmotesAPI.GetKey(Settings.EmoteWheel); //left click
                            newState2 = player.GetButton(8) && !CustomEmotesAPI.GetKey(Settings.EmoteWheel); //right click
                            newState3 = player.GetButton(9);
                            newState4 = player.GetButton(10);
                            self.GetFieldValue<InputBankTest>("bodyInputs").skill1.PushState(newState);
                            self.GetFieldValue<InputBankTest>("bodyInputs").skill2.PushState(newState2);
                            BoneMapper.attacking = newState || newState2 || newState3 || newState4;
                            BoneMapper.moving = self.GetFieldValue<InputBankTest>("bodyInputs").moveVector != Vector3.zero || player.GetButton(4);
                        }
                    }
                }
                //bool didOrig = false;
                //try
                //{
                //    if (self.hasEffectiveAuthority && self.GetFieldValue<InputBankTest>("bodyInputs"))
                //    {
                //        LocalUser localUser;
                //        Rewired.Player player;
                //        CameraRigController cameraRigController;
                //        bool doIt = false;
                //        bool newState = false;
                //        bool newState2 = false;
                //        localUser = self.networkUser.localUser;
                //        player = self.networkUser.inputPlayer;
                //        cameraRigController = self.networkUser.cameraRigController;
                //        doIt = localUser != null && player != null && cameraRigController && !localUser.isUIFocused && cameraRigController.isControlAllowed;
                //        if (doIt)
                //        {
                //            player = self.networkUser.inputPlayer;
                //            newState = player.GetButton(7) && !CustomEmotesAPI.GetKey(Settings.EmoteWheel); //left click
                //            newState2 = player.GetButton(8) && !CustomEmotesAPI.GetKey(Settings.EmoteWheel); //right click
                //            bool newState3 = player.GetButton(9);
                //            bool newState4 = player.GetButton(10);
                //            BoneMapper.attacking = newState || newState2 || newState3 || newState4;
                //            orig(self);
                //            didOrig = true;
                //            BoneMapper.moving = self.GetFieldValue<InputBankTest>("bodyInputs").moveVector != Vector3.zero || player.GetButton(4);
                //            self.GetFieldValue<InputBankTest>("bodyInputs").skill1.PushState(newState);
                //            self.GetFieldValue<InputBankTest>("bodyInputs").skill2.PushState(newState2);
                //        }
                //        else
                //        {
                //            orig(self);
                //            didOrig = true;
                //        }
                //    }
                //    else
                //    {
                //        orig(self);
                //        didOrig = true;
                //    }
                //}
                //catch (System.Exception)
                //{
                //    if (!didOrig)
                //        orig(self);
                //}
                //if (self.hasEffectiveAuthority && self.GetFieldValue<InputBankTest>("bodyInputs"))
                //{
                //    bool newState = false;
                //    bool newState2 = false;
                //    bool newState3 = false;
                //    bool newState4 = false;
                //    bool newState5 = false;
                //    bool newState6 = false;
                //    bool newState7 = false;
                //    bool newState8 = false;
                //    bool newState9 = false;
                //    LocalUser localUser;
                //    Rewired.Player player;
                //    CameraRigController cameraRigController;
                //    bool doIt = false;
                //    if (!self.networkUser)
                //    {
                //        localUser = null;
                //        player = null;
                //        cameraRigController = null;
                //        doIt = false;
                //    }
                //    else
                //    {
                //        localUser = self.networkUser.localUser;
                //        player = self.networkUser.inputPlayer;
                //        cameraRigController = self.networkUser.cameraRigController;
                //        doIt = localUser != null && player != null && cameraRigController && !localUser.isUIFocused && cameraRigController.isControlAllowed;
                //    }
                //    if (doIt)
                //    {
                //        bool flag = self.GetFieldValue<CharacterBody>("body").isSprinting;
                //        if (self.GetFieldValue<bool>("sprintInputPressReceived"))
                //        {
                //            self.SetFieldValue("sprintInputPressReceived", false);
                //            flag = !flag;
                //        }
                //        if (flag)
                //        {
                //            Vector3 aimDirection = self.GetFieldValue<InputBankTest>("bodyInputs").aimDirection;
                //            aimDirection.y = 0f;
                //            aimDirection.Normalize();
                //            Vector3 moveVector = self.GetFieldValue<InputBankTest>("bodyInputs").moveVector;
                //            moveVector.y = 0f;
                //            moveVector.Normalize();
                //            if ((self.GetFieldValue<CharacterBody>("body").bodyFlags & CharacterBody.BodyFlags.SprintAnyDirection) == CharacterBody.BodyFlags.None && Vector3.Dot(aimDirection, moveVector) < self.GetFieldValue<float>("sprintMinAimMoveDot"))
                //            {
                //                flag = false;
                //            }
                //        }
                //        newState = player.GetButton(7) && !CustomEmotesAPI.GetKey(Settings.EmoteWheel); //left click
                //        newState2 = player.GetButton(8) && !CustomEmotesAPI.GetKey(Settings.EmoteWheel); //right click
                //        newState3 = player.GetButton(9);
                //        newState4 = player.GetButton(10);
                //        newState5 = player.GetButton(5);
                //        newState6 = player.GetButton(4);
                //        newState7 = flag;
                //        newState8 = player.GetButton(6);
                //        newState9 = player.GetButton(28);
                //    }
                //    BoneMapper.attacking = newState || newState2 || newState3 || newState4;
                //    BoneMapper.moving = self.GetFieldValue<InputBankTest>("bodyInputs").moveVector != Vector3.zero || player.GetButton(4);
                //    self.GetFieldValue<InputBankTest>("bodyInputs").skill1.PushState(newState);
                //    self.GetFieldValue<InputBankTest>("bodyInputs").skill2.PushState(newState2);
                //    self.GetFieldValue<InputBankTest>("bodyInputs").skill3.PushState(newState3);
                //    self.GetFieldValue<InputBankTest>("bodyInputs").skill4.PushState(newState4);
                //    self.GetFieldValue<InputBankTest>("bodyInputs").interact.PushState(newState5);
                //    self.GetFieldValue<InputBankTest>("bodyInputs").jump.PushState(newState6);
                //    self.GetFieldValue<InputBankTest>("bodyInputs").sprint.PushState(newState7);
                //    self.GetFieldValue<InputBankTest>("bodyInputs").activateEquipment.PushState(newState8);
                //    self.GetFieldValue<InputBankTest>("bodyInputs").ping.PushState(newState9);
                //    self.InvokeMethod("CheckPinging");
                //}
            };
            //On.RoR2.PlayerCharacterMasterController.Update += (orig, self) =>
            //{
            //    DebugClass.Log($"----------{self.transform.name}");
            //    if (self.transform.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>() && self.transform.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>().overrideMoveSpeed)
            //    {
            //        DebugClass.Log($"----------nut");
            //        DebugClass.Log($"----------{self.transform.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>().overrideMoveSpeed}");
            //        return;
            //    }
            //    orig(self);
            //};
            AddNonAnimatingEmote("none");
        }
        public static int RegisterWorldProp(GameObject worldProp, JoinSpot[] joinSpots)
        {
            worldProp.AddComponent<BoneMapper>().worldProp = true;
            BoneMapper.allWorldProps.Add(new WorldProp(worldProp, joinSpots));
            return BoneMapper.allWorldProps.Count - 1;
        }
        static Shader standardShader = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material.shader;
        public static GameObject SpawnWorldProp(int propPos)
        {
            GameObject prop = GameObject.Instantiate(BoneMapper.allWorldProps[propPos].prop);
            BoneMapper mapper = prop.GetComponent<BoneMapper>();
            foreach (var item in BoneMapper.allWorldProps[propPos].joinSpots)
            {
                mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/emotejoiner/emotespot1.prefab")));
                mapper.props[mapper.props.Count - 1].transform.SetParent(mapper.transform);
                mapper.props[mapper.props.Count - 1].transform.localPosition = item.position;
                mapper.props[mapper.props.Count - 1].transform.localEulerAngles = item.rotation;
                mapper.props[mapper.props.Count - 1].transform.localScale = item.scale;
                mapper.props[mapper.props.Count - 1].name = item.name;
                foreach (var rend in mapper.props[mapper.props.Count - 1].GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    rend.material.shader = standardShader;
                }
                mapper.props[mapper.props.Count - 1].AddComponent<EmoteLocation>().owner = mapper;
            }
            return prop;
        }

        public static void AddNonAnimatingEmote(string emoteName, bool visible = true)
        {
            if (visible)
                allClipNames.Add(emoteName);
            BoneMapper.animClips.Add(emoteName, null);
        }
        public static void AddCustomAnimation(AnimationClip[] animationClip, bool looping, string[] _wwiseEventName = null, string[] _wwiseStopEvent = null, HumanBodyBones[] rootBonesToIgnore = null, HumanBodyBones[] soloBonesToIgnore = null, AnimationClip[] secondaryAnimation = null, bool dimWhenClose = false, bool stopWhenMove = false, bool stopWhenAttack = false, bool visible = true, bool syncAnim = false, bool syncAudio = false, int startPref = -1, int joinPref = -1, JoinSpot[] joinSpots = null)
        {
            if (BoneMapper.animClips.ContainsKey(animationClip[0].name))
            {
                Debug.Log($"EmotesError: [{animationClip[0].name}] is already defined as a custom emote but is trying to be added. Skipping");
                return;
            }
            if (!animationClip[0].isHumanMotion)
            {
                Debug.Log($"EmotesError: [{animationClip[0].name}] is not a humanoid animation!");
                return;
            }
            if (rootBonesToIgnore == null)
                rootBonesToIgnore = new HumanBodyBones[0];
            if (soloBonesToIgnore == null)
                soloBonesToIgnore = new HumanBodyBones[0];
            if (_wwiseEventName == null)
                _wwiseEventName = new string[] { "" };
            if (_wwiseStopEvent == null)
                _wwiseStopEvent = new string[] { "" };
            if (joinSpots == null)
                joinSpots = new JoinSpot[0];
            CustomAnimationClip clip = new CustomAnimationClip(animationClip, looping, _wwiseEventName, _wwiseStopEvent, rootBonesToIgnore, soloBonesToIgnore, secondaryAnimation, dimWhenClose, stopWhenMove, stopWhenAttack, visible, syncAnim, syncAudio, startPref, joinPref, joinSpots);
            if (visible)
                allClipNames.Add(animationClip[0].name);
            BoneMapper.animClips.Add(animationClip[0].name, clip);
        }
        public static void AddCustomAnimation(AnimationClip[] animationClip, bool looping, string _wwiseEventName = "", string _wwiseStopEvent = "", HumanBodyBones[] rootBonesToIgnore = null, HumanBodyBones[] soloBonesToIgnore = null, AnimationClip[] secondaryAnimation = null, bool dimWhenClose = false, bool stopWhenMove = false, bool stopWhenAttack = false, bool visible = true, bool syncAnim = false, bool syncAudio = false, int startPref = -1, int joinPref = -1)
        {
            if (BoneMapper.animClips.ContainsKey(animationClip[0].name))
            {
                Debug.Log($"EmotesError: [{animationClip[0].name}] is already defined as a custom emote but is trying to be added. Skipping");
                return;
            }
            if (!animationClip[0].isHumanMotion)
            {
                Debug.Log($"EmotesError: [{animationClip[0].name}] is not a humanoid animation!");
                return;
            }
            if (rootBonesToIgnore == null)
                rootBonesToIgnore = new HumanBodyBones[0];
            if (soloBonesToIgnore == null)
                soloBonesToIgnore = new HumanBodyBones[0];
            string[] wwiseEvents = new string[] { _wwiseEventName };
            string[] wwiseStopEvents = new string[] { _wwiseStopEvent };
            CustomAnimationClip clip = new CustomAnimationClip(animationClip, looping, wwiseEvents, wwiseStopEvents, rootBonesToIgnore, soloBonesToIgnore, secondaryAnimation, dimWhenClose, stopWhenMove, stopWhenAttack, visible, syncAnim, syncAudio, startPref, joinPref);
            if (visible)
                allClipNames.Add(animationClip[0].name);
            BoneMapper.animClips.Add(animationClip[0].name, clip);
        }
        public static void AddCustomAnimation(AnimationClip animationClip, bool looping, string _wwiseEventName = "", string _wwiseStopEvent = "", HumanBodyBones[] rootBonesToIgnore = null, HumanBodyBones[] soloBonesToIgnore = null, AnimationClip secondaryAnimation = null, bool dimWhenClose = false, bool stopWhenMove = false, bool stopWhenAttack = false, bool visible = true, bool syncAnim = false, bool syncAudio = false)
        {
            if (BoneMapper.animClips.ContainsKey(animationClip.name))
            {
                Debug.Log($"EmotesError: [{animationClip.name}] is already defined as a custom emote but is trying to be added. Skipping");
                return;
            }
            if (!animationClip.isHumanMotion)
            {
                Debug.Log($"EmotesError: [{animationClip.name}] is not a humanoid animation!");
                return;
            }
            if (rootBonesToIgnore == null)
                rootBonesToIgnore = new HumanBodyBones[0];
            if (soloBonesToIgnore == null)
                soloBonesToIgnore = new HumanBodyBones[0];
            AnimationClip[] animationClips = new AnimationClip[] { animationClip };
            AnimationClip[] secondaryClips = null;
            if (secondaryAnimation)
            {
                secondaryClips = new AnimationClip[] { secondaryAnimation };
            }
            string[] wwiseEvents = new string[] { _wwiseEventName };
            string[] wwiseStopEvents = new string[] { _wwiseStopEvent };
            CustomAnimationClip clip = new CustomAnimationClip(animationClips, looping, wwiseEvents, wwiseStopEvents, rootBonesToIgnore, soloBonesToIgnore, secondaryClips, dimWhenClose, stopWhenMove, stopWhenAttack, visible, syncAnim, syncAudio);
            if (visible)
                allClipNames.Add(animationClip.name);
            BoneMapper.animClips.Add(animationClip.name, clip);
        }

        public static void ImportArmature(GameObject bodyPrefab, GameObject rigToAnimate, bool jank, int meshPos = 0, bool hideMeshes = true)
        {
            rigToAnimate.GetComponent<Animator>().runtimeAnimatorController = GameObject.Instantiate<GameObject>(Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/animationreplacements/commando.prefab")).GetComponent<Animator>().runtimeAnimatorController;
            AnimationReplacements.ApplyAnimationStuff(bodyPrefab, rigToAnimate, meshPos, hideMeshes, jank);
        }
        public static void ImportArmature(GameObject bodyPrefab, GameObject rigToAnimate, int meshPos = 0, bool hideMeshes = true)
        {
            rigToAnimate.GetComponent<Animator>().runtimeAnimatorController = GameObject.Instantiate<GameObject>(Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/animationreplacements/commando.prefab")).GetComponent<Animator>().runtimeAnimatorController;
            AnimationReplacements.ApplyAnimationStuff(bodyPrefab, rigToAnimate, meshPos, hideMeshes);
        }

        public static void PlayAnimation(string animationName, int pos = -2)
        {
            var identity = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject.GetComponent<NetworkIdentity>();

            if (!NetworkServer.active)
            {
                new SyncAnimationToServer(identity.netId, animationName, pos).Send(R2API.Networking.NetworkDestination.Server);
            }
            else
            {
                new SyncAnimationToClients(identity.netId, animationName, pos).Send(R2API.Networking.NetworkDestination.Clients);

                GameObject bodyObject = Util.FindNetworkObject(identity.netId);
                bodyObject.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>().PlayAnim(animationName, pos);
            }
        }
        public static void PlayAnimation(string animationName, BoneMapper mapper, int pos = -2)
        {
            foreach (var item in BoneMapper.allMappers)
            {
                if (item == mapper)
                {
                    var identity = mapper.transform.parent.GetComponent<CharacterModel>().body.GetComponent<NetworkIdentity>();

                    if (!NetworkServer.active)
                    {
                        new SyncAnimationToServer(identity.netId, animationName, pos).Send(R2API.Networking.NetworkDestination.Server);
                    }
                    else
                    {
                        new SyncAnimationToClients(identity.netId, animationName, pos).Send(R2API.Networking.NetworkDestination.Clients);

                        GameObject bodyObject = Util.FindNetworkObject(identity.netId);
                        bodyObject.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>().PlayAnim(animationName, pos);
                    }
                    return;
                }
            }
            DebugClass.Log($"BoneMapper of name {mapper.transform.name} was not found, L");
        }
        internal static BoneMapper localMapper = null;
        static BoneMapper nearestMapper = null;
        public static CustomAnimationClip GetLocalBodyCustomAnimationClip()
        {
            if (localMapper)
            {
                return localMapper.currentClip;
            }
            else
            {
                return null;
            }
        }
        public static BoneMapper[] GetAllBoneMappers()
        {
            return BoneMapper.allMappers.ToArray();
        }
        public delegate void AnimationChanged(string newAnimation, BoneMapper mapper);
        public static event AnimationChanged animChanged;
        internal static void Changed(string newAnimation, BoneMapper mapper) //is a neat game made by a developer who endorses nsfw content while calling it a fine game for kids
        {
            mapper.transform.parent.GetComponent<CharacterModel>().body.RecalculateStats();
            if (animChanged != null)
            {
                animChanged(newAnimation, mapper);
            }
            if (newAnimation != "none")
            {
                if (mapper.transform.name == "templar")
                {
                    mapper.transform.parent.Find("ClayBruiserCannonMesh").gameObject.SetActive(false);
                }
            }
            else
            {
                if (mapper.transform.name == "templar")
                {
                    mapper.transform.parent.Find("ClayBruiserCannonMesh").gameObject.SetActive(true);
                }
            }
        }
        public delegate void JoinedEmoteSpotBody(string emoteSpotName, string currentClipName, BoneMapper joiner, BoneMapper host);
        public static event JoinedEmoteSpotBody emoteSpotJoined_Body;
        internal static void JoinedBody(GameObject emoteSpot, BoneMapper joiner, BoneMapper host)
        {
            emoteSpotJoined_Body(emoteSpot.name, host.currentClip.name, joiner, host);
        }
        public delegate void JoinedEmoteSpotProp(string emoteSpotName, BoneMapper joiner, BoneMapper host);
        public static event JoinedEmoteSpotProp emoteSpotJoined_Prop;
        internal static void JoinedProp(GameObject emoteSpot, BoneMapper joiner, BoneMapper host)
        {
            emoteSpotJoined_Prop(emoteSpot.name, joiner, host);
        }

        void Update()
        {
            if (GetKeyPressed(Settings.RandomEmote))
            {
                int rand = UnityEngine.Random.Range(0, allClipNames.Count);
                //foreach (var item in BoneMapper.allMappers)
                //{
                //    PlayAnimation(allClipNames[rand], item);
                //}
                PlayAnimation(allClipNames[rand]);
            }
            if (GetKeyPressed(Settings.JoinEmote))
            {
                if (localMapper)
                {
                    if (localMapper.currentEmoteSpot)
                    {
                        if (localMapper.currentEmoteSpot.GetComponent<EmoteLocation>().owner.worldProp)
                        {
                            JoinedProp(localMapper.currentEmoteSpot, localMapper, localMapper.currentEmoteSpot.GetComponent<EmoteLocation>().owner);
                        }
                        else
                        {
                            JoinedBody(localMapper.currentEmoteSpot, localMapper, localMapper.currentEmoteSpot.GetComponent<EmoteLocation>().owner);
                        }
                    }
                    else
                    {
                        foreach (var mapper in BoneMapper.allMappers)
                        {
                            try
                            {
                                if (mapper != localMapper)
                                {
                                    if (!nearestMapper && (mapper.currentClip.syncronizeAnimation || mapper.currentClip.syncronizeAudio))
                                    {
                                        nearestMapper = mapper;
                                    }
                                    else if (nearestMapper)
                                    {
                                        if ((mapper.currentClip.syncronizeAnimation || mapper.currentClip.syncronizeAudio) && Vector3.Distance(localMapper.transform.position, mapper.transform.position) < Vector3.Distance(localMapper.transform.position, nearestMapper.transform.position))
                                        {
                                            nearestMapper = mapper;
                                        }
                                    }
                                }
                            }
                            catch (System.Exception)
                            {
                            }
                        }
                        if (nearestMapper)
                        {
                            PlayAnimation(nearestMapper.currentClip.clip[0].name);
                        }
                        nearestMapper = null;
                    }
                }
            }
        }
    }
}
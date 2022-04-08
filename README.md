# IMPORTANT

- This does nothing on without other mods. Please don't download this on it's own and complain to me unless you really want to.

# How to implement
First off, if you are at all confused about any of this, I made a repo with example implementations. https://github.com/ToastedOven/ExampleEmotePlugin

## General Overview
### Start here if you are lost
- Create a **humanoid** animation in unity and import it into your plugin project.
- In your project, Import the CustomEmotesAPI dll file and include `using EmotesAPI;` in your cs file.
- Place `CustomEmotesAPI.AddCustomAnimation(AnimationClip, false);` somewhere in your awake call. Where AnimationClip is the animation clip you imported from unity.
- If done correctly, this will load the animation into the list of available emotes. Feel free to change the bool to `true` and import a looping animation as well.

- You can also import custom survivors with
`CustomEmotesAPI.ImportArmature(bodyPrefab, underskeleton)`
Underskeleton is a copy of the bodyPrefab which is setup as a humanoid skeleton

## AddCustomAnimation Params
            AnimationClip animationClip                     //Default animation
			
            bool looping                                    //Whether or not animationClip loops
			
            string _wwiseEventName = ""                     //Event to post when animation starts
			
            string _wwiseStopEvent = ""                     //Event to post when animation stops
			
            HumanBodyBones[] rootBonesToIgnore = null       //All bones specified and any child bones will be ignored by the animation
			
            HumanBodyBones[] soloBonesToIgnore = null       //All bones specified will be ignored by the animation
			
            AnimationClip secondaryAnimation = null         //Animation to play after the primary animation. Use this if you have a non-looping-into-looping animation
			
            bool dimWhenClose = false                       //Create an audio dimming sphere around the emotee which will dim normal music when you approach them
			
            bool stopWhenMove = false                       //Stops the animation if moving
			
            bool stopWhenAttack = false                     //Stops the animation if attacking
			
            bool visible = true                             //Dictates if emote will show up in the normal list.


### Examples
1.  loops first anim, has a start and stop wwise event, dims audio when close

`CustomEmotesAPI.AddCustomAnimation(loserAnimClip, true, "Loser", "LoserStop", dimWhenClose: true);`


2. doesn't loop first anim, secondary anim which loops

`CustomEmotesAPI.AddCustomAnimation(spinStartAnimClip, false, secondaryAnimation: spinLoopAnimClip);`


3. Creates 2 HumanBodyBone lists for ignoring bones in the animation, doesn't loop first anim, has a start and stop wwise event, includes previously created HumanBodyBone lists

`HumanBodyBones[] upperLegs = new HumanBodyBones[] { HumanBodyBones.LeftUpperLeg, HumanBodyBones.RightUpperLeg };`

`HumanBodyBones[] hips = new HumanBodyBones[] { HumanBodyBones.Hips };`

`CustomEmotesAPI.AddCustomAnimation(dabAnimClip, false, "Dab", "DabStop", upperLegs, hips);`

If you're still lost, consider srolling up and cloning from the example repo or @ me on Discord @Metrosexual Fruitcake#6969

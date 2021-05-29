using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using CAMOWA.FBXRuntimeImporter;
using CAMOWA.FBXRuntimeImporter.AnimationRead;

namespace DIMOWA_Mod_Example
{
    public class FunnyAnimation : MonoBehaviour
    {
        AnimationClip clip;
        Animator animator;
        AnimationCurve positionXCurve;
        public void Start()//ficar parente do "RocketScientist"
        {
            FBXFileParser fBXFile = new FBXFileParser("heartianVibe.fbx");
            FBXAnimation[] animations = fBXFile.ReadAnimations();
            positionXCurve = FBXCurveToUnityCurve(animations[0].BonesAnimation[0].PositionCurves[0]);
            //AnimationCurve positionYCurve = FBXCurveToUnityCurve(animations[0].BonesAnimation[0].PositionCurves[1]);
            //AnimationCurve positionZCurve = FBXCurveToUnityCurve(animations[0].BonesAnimation[0].PositionCurves[2]);

            clip = new AnimationClip();
            clip.SetCurve(""/*FrontierVillager_rig:Root_JNT/FrontierVillager_rig:Spine1_JNT*/, typeof(Transform), "localPosition.x", positionXCurve);
            //clip.SetCurve("RocketScientist/FrontierVillager_default_idle/FrontierVillager_rig:Root_JNT/FrontierVillager_rig:Spine1_JNT", typeof(Transform), "localPosition.y", positionYCurve);
            //clip.SetCurve("RocketScientist/FrontierVillager_default_idle/FrontierVillager_rig:Root_JNT/FrontierVillager_rig:Spine1_JNT", typeof(Transform), "localPosition.z", positionZCurve);
            //clip.name = "Funny Animation";
            //clip.wrapMode = WrapMode.Loop;

            animator = gameObject.AddComponent<Animator>();//Como assim Animator não existe??????
            animator.animation.AddClip(clip, clip.name);
        }
        bool isItPlaying = false;
        public void Update()
        {
            //float f = positionXCurve.Evaluate(Time.time % positionXCurve[positionXCurve.length - 1].time);
            //transform.localPosition = new Vector3(f, 0f, 0f);
            if (!isItPlaying)
            {
                animator.animation[clip.name].layer = 123;
                bool b = animator.animation.Play(clip.name);
                Debug.Log($"is playing? {b}");
                isItPlaying = true;
                Debug.Log("length= " + clip.length);
                Debug.Log("name= " + clip.name);
            }
        }
        public static AnimationCurve FBXCurveToUnityCurve(FBXAnimationCurve fbxCurve)
        {
            FBXKeyFrame[] fbxKeyFrames = fbxCurve.KeyFrames;
            Keyframe[] keyframes = new Keyframe[fbxKeyFrames.Length + 1];
            keyframes[0] = new Keyframe(0f, fbxCurve.DefaultValue);
            for (int i = 1; i < keyframes.Length; i++)
                keyframes[i] = new Keyframe(fbxKeyFrames[i - 1].TimeInSeconds, fbxKeyFrames[i - 1].Value);

            return new AnimationCurve(keyframes);
        }

    }
}

using System;
using UnityEditor;
using UnityEngine;

namespace Humr.Editor
{
    public static class AnimationClipFactory
    {
        public static AnimationClip PopulateBoneRotationsClip(RecordingTake take, Animator animator)
        {
            var frameCount = take.Frames.Count;
            var totalCurves = 3 + HumanTrait.BoneName.Length * 4;

            var keyframes = InitializeKeyframeArrays(totalCurves, frameCount);
            for (var frameIdx = 0; frameIdx < frameCount; frameIdx++) 
                ProcessBoneRotationsKeyframes((BoneRotationsFrame)take.Frames[frameIdx], keyframes, frameIdx, animator);

            return CreateAndBindBoneRotationCurves(keyframes, animator);
        }

        public static AnimationClip PopulateObjectClip(RecordingTake take, Transform transform)
        {
            var frameCount = take.Frames.Count;
            var keyframes = InitializeKeyframeArrays(10, frameCount);
            for (var frameIdx = 0; frameIdx < frameCount; frameIdx++) 
                ProcessObjectKeyframes((ObjectFrame)take.Frames[frameIdx], keyframes, frameIdx);
            
            return CreateAndBindObjectCurves(keyframes, transform);
        }

        private static Keyframe[][] InitializeKeyframeArrays(int totalCurves, int frameCount)
        {
            var keyframes = new Keyframe[totalCurves][];
            for (var i = 0; i < totalCurves; i++) keyframes[i] = new Keyframe[frameCount];
            return keyframes;
        }

        private static void ProcessObjectKeyframes(ObjectFrame frame, Keyframe[][] keyframes, int frameIdx)
        {
            keyframes[0][frameIdx] = new Keyframe(frame.RecordTime, frame.Position.x);
            keyframes[1][frameIdx] = new Keyframe(frame.RecordTime, frame.Position.y);
            keyframes[2][frameIdx] = new Keyframe(frame.RecordTime, frame.Position.z);
            keyframes[3][frameIdx] = new Keyframe(frame.RecordTime, frame.Rotation.x);
            keyframes[4][frameIdx] = new Keyframe(frame.RecordTime, frame.Rotation.y);
            keyframes[5][frameIdx] = new Keyframe(frame.RecordTime, frame.Rotation.z);
            keyframes[6][frameIdx] = new Keyframe(frame.RecordTime, frame.Rotation.w);
            keyframes[7][frameIdx] = new Keyframe(frame.RecordTime, frame.LocalScale.x);
            keyframes[8][frameIdx] = new Keyframe(frame.RecordTime, frame.LocalScale.y);
            keyframes[9][frameIdx] = new Keyframe(frame.RecordTime, frame.LocalScale.z);
        }

        private static void ProcessBoneRotationsKeyframes(
            BoneRotationsFrame frame, Keyframe[][] keyframes, int frameIdx, Animator animator)
        {
            var localHipPos = ProcessHipPosition(frame.HipPosition, animator);
            keyframes[0][frameIdx] = new Keyframe(frame.RecordTime, localHipPos.x);
            keyframes[1][frameIdx] = new Keyframe(frame.RecordTime, localHipPos.y);
            keyframes[2][frameIdx] = new Keyframe(frame.RecordTime, localHipPos.z);

            ApplyWorldRotationsToAvatar(frame, animator);
            RecordLocalRotationsToKeyframes(keyframes, frameIdx, frame, animator);
        }

        private static Vector3 ProcessHipPosition(Vector3 rawHipPos, Animator animator)
        {
            var hipTransform = animator.GetBoneTransform(HumanBodyBones.Hips);
            if (hipTransform == null || hipTransform.parent == null) return rawHipPos;

            var armatureParent = hipTransform.parent;
            return armatureParent.InverseTransformPoint(rawHipPos);
        }

        private static void ApplyWorldRotationsToAvatar(BoneRotationsFrame frame, Animator animator)
        {
            for (var k = 0; k < HumanTrait.BoneName.Length; k++)
            {
                if (k >= frame.BoneRotations.Count) break;

                var boneTransform = animator.GetBoneTransform((HumanBodyBones)k);
                if (boneTransform == null) continue;

                boneTransform.rotation = frame.BoneRotations[k];
            }
        }

        private static void RecordLocalRotationsToKeyframes(
            Keyframe[][] keyframes, int frameIdx, BoneRotationsFrame frame, Animator animator)
        {
            for (var k = 0; k < HumanTrait.BoneName.Length; k++)
            {
                var boneTransform = animator.GetBoneTransform((HumanBodyBones)k);
                if (boneTransform == null) continue;

                var localRotation = boneTransform.localRotation;
                var curveBaseIndex = k * 4 + 3;

                keyframes[curveBaseIndex][frameIdx] = new Keyframe(frame.RecordTime, localRotation.x);
                keyframes[curveBaseIndex + 1][frameIdx] = new Keyframe(frame.RecordTime, localRotation.y);
                keyframes[curveBaseIndex + 2][frameIdx] = new Keyframe(frame.RecordTime, localRotation.z);
                keyframes[curveBaseIndex + 3][frameIdx] = new Keyframe(frame.RecordTime, localRotation.w);
            }
        }

        private static AnimationClip CreateAndBindBoneRotationCurves(Keyframe[][] keyframes, Animator animator)
        {
            var clip = new AnimationClip();
            var hipTransform = animator.GetBoneTransform(HumanBodyBones.Hips);
            var hipPath = AnimationUtility.CalculateTransformPath(hipTransform, animator.transform);

            clip.SetCurve(hipPath, typeof(Transform), "localPosition.x", new AnimationCurve(keyframes[0]));
            clip.SetCurve(hipPath, typeof(Transform), "localPosition.y", new AnimationCurve(keyframes[1]));
            clip.SetCurve(hipPath, typeof(Transform), "localPosition.z", new AnimationCurve(keyframes[2]));

            for (var m = 0; m < HumanTrait.BoneName.Length; m++)
            {
                var boneTransform = animator.GetBoneTransform((HumanBodyBones)m);
                if (boneTransform == null) continue;

                var bonePath = AnimationUtility.CalculateTransformPath(boneTransform, animator.transform);
                var curveBaseIndex = m * 4 + 3;

                clip.SetCurve(bonePath, typeof(Transform), "localRotation.x",
                    new AnimationCurve(keyframes[curveBaseIndex]));
                clip.SetCurve(bonePath, typeof(Transform), "localRotation.y",
                    new AnimationCurve(keyframes[curveBaseIndex + 1]));
                clip.SetCurve(bonePath, typeof(Transform), "localRotation.z",
                    new AnimationCurve(keyframes[curveBaseIndex + 2]));
                clip.SetCurve(bonePath, typeof(Transform), "localRotation.w",
                    new AnimationCurve(keyframes[curveBaseIndex + 3]));
            }

            clip.EnsureQuaternionContinuity();
            return clip;
        }

        private static AnimationClip CreateAndBindObjectCurves(Keyframe[][] keyframes, Transform transform)
        {
            var clip = new AnimationClip();
            // var transformPath = PathUtils.GetHierarchyPath(transform);
            var transformPath = "";

            clip.SetCurve(transformPath, typeof(Transform), 
                "localPosition.x", new AnimationCurve(keyframes[0]));
            clip.SetCurve(transformPath, typeof(Transform), 
                "localPosition.y", new AnimationCurve(keyframes[1]));
            clip.SetCurve(transformPath, typeof(Transform), 
                "localPosition.z", new AnimationCurve(keyframes[2]));
            clip.SetCurve(transformPath, typeof(Transform), 
                "localRotation.x", new AnimationCurve(keyframes[3]));
            clip.SetCurve(transformPath, typeof(Transform), 
                "localRotation.y", new AnimationCurve(keyframes[4]));
            clip.SetCurve(transformPath, typeof(Transform), 
                "localRotation.z", new AnimationCurve(keyframes[5]));
            clip.SetCurve(transformPath, typeof(Transform), 
                "localRotation.w", new AnimationCurve(keyframes[6]));
            clip.SetCurve(transformPath, typeof(Transform), 
                "localScale.x", new AnimationCurve(keyframes[7]));
            clip.SetCurve(transformPath, typeof(Transform), 
                "localScale.y", new AnimationCurve(keyframes[8]));
            clip.SetCurve(transformPath, typeof(Transform), 
                "localScale.z", new AnimationCurve(keyframes[9]));
            
            clip.EnsureQuaternionContinuity();
            return clip;
        }
    }
}
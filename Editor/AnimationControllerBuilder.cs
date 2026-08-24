using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Humr.Editor
{
    internal struct BoneSnapshot
    {
        public Transform Transform;
        public Vector3 LocalPosition;
        public Quaternion LocalRotation;
    }

    internal class AvatarPoseSnapshot
    {
        private readonly List<BoneSnapshot> _avatarSnapshot = new List<BoneSnapshot>();
        private Vector3 _savedRootPosition;
        private Quaternion _savedRootRotation;

        public void Take(Transform rootTransform, Animator animator)
        {
            if (animator == null) return;

            _savedRootPosition = rootTransform.position;
            _savedRootRotation = rootTransform.rotation;
            _avatarSnapshot.Clear();

            for (var i = 0; i < HumanTrait.BoneName.Length; i++)
            {
                var boneTransform = animator.GetBoneTransform((HumanBodyBones)i);
                if (boneTransform == null) continue;

                _avatarSnapshot.Add(new BoneSnapshot
                {
                    Transform = boneTransform,
                    LocalPosition = boneTransform.localPosition,
                    LocalRotation = boneTransform.localRotation
                });
            }
        }

        public void Restore(Transform rootTransform)
        {
            rootTransform.position = _savedRootPosition;
            rootTransform.rotation = _savedRootRotation;

            foreach (var snapshot in _avatarSnapshot)
            {
                if (snapshot.Transform == null) continue;
                snapshot.Transform.localPosition = snapshot.LocalPosition;
                snapshot.Transform.localRotation = snapshot.LocalRotation;
            }
        }
    }

    internal class AnimationControllerBuilder
    {
        public AnimatorController Controller { get; private set; }

        public void Setup(string humrPath)
        {
            var controllerFolderPath = $"{humrPath}/AnimationController";
            var controllerPath = $"{controllerFolderPath}/TmpAniCon.controller";

            if (Controller != null)
            {
                var clearAllStates = AssetDatabase.GetAssetPath(Controller) == controllerPath;
                CleanControllerStates(clearAllStates);
                return;
            }

            PathUtils.CreateDirectoryIfNotExist(controllerFolderPath);
            Controller = AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
        }

        public void CleanControllerStates(bool clearAll)
        {
            if (Controller == null) return;

            foreach (var layer in Controller.layers)
            {
                var states = layer.stateMachine.states;
                for (var i = states.Length - 1; i >= 0; i--)
                {
                    if (!clearAll && states[i].state.motion != null) continue;
                    layer.stateMachine.RemoveState(states[i].state);
                }
            }
        }

        public void AddClipToController(AnimationClip clip)
        {
            if (Controller == null || Controller.layers.Length == 0) return;
            Controller.layers[0].stateMachine.AddState(clip.name).motion = clip;
        }

        public static void SaveGenericAnimationAsset(AnimationClip clip, string animAssetPath)
        {
            if (File.Exists(animAssetPath))
            {
                AssetDatabase.DeleteAsset(animAssetPath);
                HumrLogger.Warning($"Overwrite target collision detected: Existing asset deleted at {animAssetPath}");
            }

            AssetDatabase.CreateAsset(clip, AssetDatabase.GenerateUniqueAssetPath(animAssetPath));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            EditorUtility.FocusProjectWindow();
            var createdAsset = AssetDatabase.LoadAssetAtPath<AnimationClip>(AssetDatabase.GetAssetPath(clip));
            Selection.activeObject = createdAsset;
            EditorGUIUtility.PingObject(createdAsset);
        }
    }
}
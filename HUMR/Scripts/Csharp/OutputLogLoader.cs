
/*******
 * OutputLogLoader.cs
 * 
 * メインの処理を行う。ログ出力時と同一のアバターをHierarchy上に置き、これをアタッチして使用することを想定している
 * PackageManagerからFBXExportorをインストールしておく必要あり
 * 
 * フォルダを構成して、OutputLog_xx_xx_xxからアニメーションを作成
 * そのアニメーションをアバターのアニメーターに入れてFBXとして出力
 * FBXをHumanoidにすることでHumanoidAnimationを得られるようにしている
 * 
 * *****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEditor;
using UnityEngine.EventSystems;

namespace HUMR
{
#if UNITY_EDITOR
    public interface OutputLogLoaderinterface : IEventSystemHandler
    {
        void LoadLogToExportAnim();
    }

    [RequireComponent(typeof(Animator))]
    public class OutputLogLoader : MonoBehaviour, OutputLogLoaderinterface
    {
        Animator animator;
        UnityEditor.Animations.AnimatorController controller;
        string[] files;
        [HideInInspector]
        public string OutputLogPath = "";
        [HideInInspector]
        public int index = 0;

        static int nHeaderStrNum = 19;//timestamp example/*2021.01.03 20:57:35*/
        static string strKeyWord = " Log        -  HUMR:";
        [TooltipAttribute("GenericAnimationを出力する場合はチェックを入れてください(チェックがないと複数のAnimationを出力できません)")]
        public bool ExportGenericAnimation = true;
        [TooltipAttribute("モーションを出力したいユーザーの名前を書いてください")]
        public string DisplayName = "";

        public void LoadLogToExportAnim()
        {
            if (DisplayName == "")
            {
                Debug.LogWarning("DisplayName is null");
                return;
            }
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
            string humrPath = @"Assets/HUMR";
            CreateDirectoryIfNotExist(humrPath);

            ControllerSetUp(humrPath);

            string[] files = Directory.GetFiles(OutputLogPath, "*.txt");

            string[] strOutputLogLines = File.ReadAllLines(files[index]);
            int nTargetCounter = 0;
            List<int> newTargetLines = new List<int>();//ファイルの中での新しく始まった対象の行を格納する
            newTargetLines.Add(0);
            List<int> newLogLines = new List<int>();//抽出したログの中で新しく始まった行を格納する
            newLogLines.Add(0);
            float beforetime = 0;
            for (int j = 0; j < strOutputLogLines.Length; j++)
            {
                //対象のログの行を抽出
                if (strOutputLogLines[j].Contains(strKeyWord + DisplayName))
                {
                    if (strOutputLogLines[j].Length > nHeaderStrNum + (strKeyWord + DisplayName).Length)
                    {
                        //記録終わりを検知
                        string strTmpOLL = strOutputLogLines[j].Substring(nHeaderStrNum + (strKeyWord + DisplayName).Length);
                        for (int k = 0; k < strTmpOLL.Length; k++)
                        {
                            if (strTmpOLL[k] == ',')
                            {
                                float currenttime = float.Parse(strTmpOLL.Substring(0, k));
                                if (currenttime < beforetime)
                                {
                                    newLogLines.Add(nTargetCounter);
                                    newTargetLines.Add(j);
                                }
                                beforetime = currenttime;
                                break;
                            }
                        }
                        nTargetCounter++;//目的の行が何行あるか。
                    }
                    else
                    {
                        Debug.LogWarning("Length is not correct");
                    }
                }
            }
            newLogLines.Add(nTargetCounter);
            newTargetLines.Add(strOutputLogLines.Length);
            // Keyframeの生成
            if (nTargetCounter == 0)
            {
                Debug.LogWarning("Not exist Motion Data with ["+ DisplayName + "] (Did you enter correct DisplayName ? or select correct log ?)");
                return;
            }

            for (int i =0; i<newLogLines.Count-1;i++)
            {
                int nLineNum = newLogLines[i + 1] - newLogLines[i];
                int nTargetLineNum = newTargetLines[i + 1] - newTargetLines[i];
                Keyframe[][] Keyframes = new Keyframe[4 * (HumanTrait.BoneName.Length + 1/*time + hip position*/) - 1/*time*/][];//[要素数]
                for (int j = 0; j < Keyframes.Length; j++)
                {
                    Keyframes[j] = new Keyframe[nLineNum];//[行数]
                }

                //Keyframeにログの値を入れていく
                {
                    string[] strDisplayNameOutputLogLines = new string[nLineNum];//目的の行の配列
                    int nTargetLineCounter = 0;
                    beforetime = 0;
                    for (int j = newTargetLines[i]; j < newTargetLines[i+1]; j++)
                    {
                        //対象のログの行を抽出
                        if (strOutputLogLines[j].Contains(strKeyWord + DisplayName))
                        {
                            if (strOutputLogLines[j].Length > nHeaderStrNum + (strKeyWord + DisplayName).Length)
                            {
                                strDisplayNameOutputLogLines[nTargetLineCounter] = strOutputLogLines[j].Substring(nHeaderStrNum + (strKeyWord + DisplayName).Length);//時間,position,rotation,rotation,…
                                for (int k = 0; k < strDisplayNameOutputLogLines[nTargetLineCounter].Length; k++)
                                {
                                    if (strDisplayNameOutputLogLines[nTargetLineCounter][k] == ',')
                                    {
                                        float currenttime = float.Parse(strDisplayNameOutputLogLines[nTargetLineCounter].Substring(0, k));
                                        if (currenttime < beforetime)
                                        {
                                            Debug.LogAssertion("new record line is contained");
                                        }
                                        beforetime = currenttime;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Debug.LogWarning("Log Length is not correct");
                            }
                            //Debug.Log(DisplayNameOutputLogLines[nTargetLineCounter]);
                            string[] strSplitedOutPutLog = strDisplayNameOutputLogLines[nTargetLineCounter].Split(',');
                            if (strSplitedOutPutLog.Length == 4 * (HumanTrait.BoneName.Length + 1/*time + hip position*/))
                            {
                                float key_time = float.Parse(strSplitedOutPutLog[0]);
                                Vector3 rootScale = animator.transform.localScale;
                                Vector3 armatureScale = animator.GetBoneTransform((HumanBodyBones)0).parent.localScale;
                                Vector3 hippos = new Vector3(float.Parse(strSplitedOutPutLog[1]), float.Parse(strSplitedOutPutLog[2]), float.Parse(strSplitedOutPutLog[3]));
                                transform.rotation = Quaternion.identity;//Avatarがrotation(0,0,0)でない可能性があるため
                                hippos = Quaternion.Inverse(animator.GetBoneTransform((HumanBodyBones)0).parent.localRotation) * hippos;//armatureがrotation(0,0,0)でない可能性があるため
                                hippos = new Vector3(hippos.x / rootScale.x/ armatureScale.x, hippos.y / rootScale.y/ armatureScale.y, hippos.z / rootScale.z/ armatureScale.z); //いる？
                                Keyframes[0][nTargetLineCounter] = new Keyframe(key_time, hippos.x);
                                Keyframes[1][nTargetLineCounter] = new Keyframe(key_time, hippos.y);
                                Keyframes[2][nTargetLineCounter] = new Keyframe(key_time, hippos.z);
                                Quaternion[] boneWorldRotation = new Quaternion[HumanTrait.BoneName.Length];
                                for (int k = 0; k < HumanTrait.BoneName.Length; k++)
                                {
                                    boneWorldRotation[k] = new Quaternion(float.Parse(strSplitedOutPutLog[k * 4 + 4]), float.Parse(strSplitedOutPutLog[k * 4 + 5]), float.Parse(strSplitedOutPutLog[k * 4 + 6]), float.Parse(strSplitedOutPutLog[k * 4 + 7]));
                                }
                                for (int k = 0; k < HumanTrait.BoneName.Length; k++)
                                {

                                    if (animator.GetBoneTransform((HumanBodyBones)k) == null)
                                    {
                                        continue;
                                    }
                                    animator.GetBoneTransform((HumanBodyBones)k).rotation = boneWorldRotation[k];
                                }

                                for (int k = 0; k < HumanTrait.BoneName.Length; k++)
                                {
                                    if (animator.GetBoneTransform((HumanBodyBones)k) == null)
                                    {
                                        continue;
                                    }
                                    Quaternion localrot = animator.GetBoneTransform((HumanBodyBones)k).localRotation;
                                    Keyframes[k * 4 + 3][nTargetLineCounter] = new Keyframe(key_time, localrot.x);
                                    Keyframes[k * 4 + 4][nTargetLineCounter] = new Keyframe(key_time, localrot.y);
                                    Keyframes[k * 4 + 5][nTargetLineCounter] = new Keyframe(key_time, localrot.z);
                                    Keyframes[k * 4 + 6][nTargetLineCounter] = new Keyframe(key_time, localrot.w);
                                }
                            }
                            else
                            {
                                Debug.Log(strSplitedOutPutLog.Length);//228
                                Debug.LogAssertion("Key value length is not correct");
                            }
                            nTargetLineCounter++;
                        }
                    }
                }

                //AnimationClipにAnimationCurveを設定
                AnimationClip clip = new AnimationClip();
                {
                    // AnimationCurveの生成
                    AnimationCurve[] AnimCurves = new AnimationCurve[Keyframes.Length];

                    for (int l = 0; l < AnimCurves.Length; l++)//[行数-1]
                    {
                        AnimCurves[l] = new AnimationCurve(Keyframes[l]);
                    }
                    // AnimationCurveの追加
                    clip.SetCurve(GetHierarchyPath(animator.GetBoneTransform((HumanBodyBones)0)), typeof(Transform), "localPosition.x", AnimCurves[0]);
                    clip.SetCurve(GetHierarchyPath(animator.GetBoneTransform((HumanBodyBones)0)), typeof(Transform), "localPosition.y", AnimCurves[1]);
                    clip.SetCurve(GetHierarchyPath(animator.GetBoneTransform((HumanBodyBones)0)), typeof(Transform), "localPosition.z", AnimCurves[2]);
                    for (int m = 0; m < (AnimCurves.Length - 3) / 4; m++)//[骨数]
                    {
                        if (animator.GetBoneTransform((HumanBodyBones)m) == null)
                        {
                            continue;
                        }
                        clip.SetCurve(GetHierarchyPath(animator.GetBoneTransform((HumanBodyBones)m)),
                            typeof(Transform), "localRotation.x", AnimCurves[m * 4 + 3]);
                        clip.SetCurve(GetHierarchyPath(animator.GetBoneTransform((HumanBodyBones)m)),
                            typeof(Transform), "localRotation.y", AnimCurves[m * 4 + 4]);
                        clip.SetCurve(GetHierarchyPath(animator.GetBoneTransform((HumanBodyBones)m)),
                            typeof(Transform), "localRotation.z", AnimCurves[m * 4 + 5]);
                        clip.SetCurve(GetHierarchyPath(animator.GetBoneTransform((HumanBodyBones)m)),
                            typeof(Transform), "localRotation.w", AnimCurves[m * 4 + 6]);
                    }
                    clip.EnsureQuaternionContinuity();//これをしないとQuaternion補間してくれない
                }

                //GenericAnimation出力
                {
                    string animFolderPath = humrPath + @"/GenericAnimations";
                    CreateDirectoryIfNotExist(animFolderPath);
                    string displaynameFolderPath = animFolderPath + "/" + DisplayName;
                    CreateDirectoryIfNotExist(displaynameFolderPath);

                    string animationName = files[index].Substring(files[index].Length - 13).Remove(9)+"_"+i.ToString();
                    string animPath = displaynameFolderPath + "/" + animationName + ".anim";
                    Debug.Log(animPath);

                    if (ExportGenericAnimation)
                    {
                        if (File.Exists(animPath))
                        {
                            AssetDatabase.DeleteAsset(animPath);
                            Debug.LogWarning("Same Name Generic Animation is existing. Overwritten!!");
                            foreach (var layer in controller.layers)//アニメーションを消したことにより空のアニメーションステートが出来てたら削除
                            {
                                foreach (var state in layer.stateMachine.states)
                                {
                                    if (state.state.motion == null)
                                    {
                                        layer.stateMachine.RemoveState(state.state);
                                    }
                                }
                            }
                        }
                        AssetDatabase.CreateAsset(clip, AssetDatabase.GenerateUniqueAssetPath(animPath));
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                }

                //アニメーションをアバターのアニメーターに入れる
                {
                    controller.layers[0].stateMachine.AddState(clip.name).motion = clip;
                }
            }
            //FBXとして出力
            {
                animator.runtimeAnimatorController = controller;
                string exportFolderPath = humrPath + @"/FBXs";
                CreateDirectoryIfNotExist(exportFolderPath);
                string displaynameFBXFolderPath = exportFolderPath + "/" + DisplayName;
                CreateDirectoryIfNotExist(displaynameFBXFolderPath);
                UnityEditor.Formats.Fbx.Exporter.ModelExporter.ExportObject(displaynameFBXFolderPath + "/" + files[index].Substring(files[index].Length - 13).Remove(9), this.gameObject);
            }
        }
        
        void ControllerSetUp(string humrPath)
        {
            string tmpAniConPath = humrPath + @"/AnimationController";
            if (controller == null)
            {
                CreateDirectoryIfNotExist(tmpAniConPath);
                controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(tmpAniConPath + "/TmpAniCon.controller");
            }
            else if (AssetDatabase.GetAssetPath(controller) == tmpAniConPath + "/TmpAniCon.controller")
            {
                foreach (var layer in controller.layers)
                {
                    foreach (var state in layer.stateMachine.states)
                    {
                        layer.stateMachine.RemoveState(state.state);
                    }
                }
            }
            else
            {
                foreach (var layer in controller.layers)
                {
                    foreach (var state in layer.stateMachine.states)
                    {
                        if (state.state.motion == null)
                        {
                            layer.stateMachine.RemoveState(state.state);
                        }
                    }
                }
            }
        }

        void CreateDirectoryIfNotExist(string path)
        {
            //存在するかどうか判定しなくても良いみたいだが気持ち悪いので
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        string GetHierarchyPath(Transform self)
        {
            string path = self.gameObject.name;
            Transform parent = self.parent;
            while (parent.parent != null)
            {
                path = parent.name + "/" + path;
                parent = parent.parent;
            }
            return path;
        }

    }
#endif
}

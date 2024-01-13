
/*******
 * InteractRecorder.cs
 * 
 * UdonSharp用。VRCSDK3-WorldとUdonSharpを導入すること
 * 
 * Interactするとプレイヤーの座標とボーンの回転値をログに出力する
 * 再度Interactすると記録を停止
 * 再再度Interactすると記録を開始する
 * 
 * *****/

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace HUMR
{
    public class InteractRecorder : UdonSharpBehaviour
    {
        [SerializeField, TooltipAttribute("チェックを入れるとワールド内の全ての人のモーションが記録されます（周知を推奨）")]
        bool recordAllPlayers = false;
        [TooltipAttribute("記録毎に最低何秒間の間隔を空けるかの設定")]
        public float SecondsPerRecord = 0.1f;

        Quaternion[] bonerotation;
        VRCPlayerApi[] players;
        VRCPlayerApi player;
        float time;
        float beforetime;

        bool isRecording = false;
        [TooltipAttribute("録画中であることを表示するオブジェクトを入れてください")]
        public GameObject DisplayRecording;
        [TooltipAttribute("録画停止中であることを表示するオブジェクトを入れてください")]
        public GameObject DisplayStopping;

        void Start()
        {
            players = new VRCPlayerApi[80];
            players[0] = Networking.LocalPlayer;
            bonerotation = new Quaternion[HumanTrait.BoneName.Length];
            time = 0;
            beforetime = time;
            isRecording = false;
            DisplayRecording.SetActive(isRecording);
            DisplayStopping.SetActive(!isRecording);
        }
        private void Update()
        {
            if (!isRecording)
            {
                return;
            }

            if (time - beforetime > SecondsPerRecord)
            {
                if (recordAllPlayers)
                {
                    VRCPlayerApi.GetPlayers(players);
                }
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i] == null)
                    {
                        continue;
                    }
                    player = players[i];

                    string strOutputLog = "HUMR:";
                    strOutputLog += player.displayName;
                    strOutputLog += time.ToString();
                    strOutputLog += ",";
                    //hipbone = root
                    strOutputLog += player.GetBonePosition(HumanBodyBones.Hips).x.ToString("F7");
                    strOutputLog += ",";
                    strOutputLog += player.GetBonePosition(HumanBodyBones.Hips).y.ToString("F7");
                    strOutputLog += ",";
                    strOutputLog += player.GetBonePosition(HumanBodyBones.Hips).z.ToString("F7");
                    for (int j = 0; j < bonerotation.Length; j++)
                    {
                        bonerotation[j] = player.GetBoneRotation((HumanBodyBones)j);
                        strOutputLog += ",";
                        strOutputLog += bonerotation[j].x.ToString("F7");
                        strOutputLog += ",";
                        strOutputLog += bonerotation[j].y.ToString("F7");
                        strOutputLog += ",";
                        strOutputLog += bonerotation[j].z.ToString("F7");
                        strOutputLog += ",";
                        strOutputLog += bonerotation[j].w.ToString("F7");
                    }
                    Debug.Log(strOutputLog);
                    beforetime = time;
                }
            }
            time += Time.deltaTime;
        }

        public override void Interact()
        {
            time = 0;
            beforetime = 0;
            isRecording = !isRecording;
            DisplayRecording.SetActive(isRecording);
            DisplayStopping.SetActive(!isRecording);
        }

    }
}
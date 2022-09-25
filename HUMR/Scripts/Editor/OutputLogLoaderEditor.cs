
/*******
 * OutputLogLoaderEditor.cs
 * 
 * Editor拡張。Editorフォルダの下に配置すること
 * 
 * OutputlogLoader.csをアタッチした際のInspectorに表示される項目を拡張している
 * 
 * FBXExporterの有無を確認し、存在するOutputLogをプルダウンに表示する
 * "LoadLogToExportAnim"のボタンを押すとOutputLog内の処理が実行される
 * 
 * *****/
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.EventSystems;

namespace HUMR
{
    [CustomEditor(typeof(OutputLogLoader))]
    public class OutputLogLoaderEditor : Editor
    {
        string path;
        bool foldout;

        public override void OnInspectorGUI()
        {

            //元のInspector部分を表示
            base.OnInspectorGUI();

            //targetを変換して対象を取得
            OutputLogLoader targetScript = target as OutputLogLoader;

            EditorGUI.BeginChangeCheck();

            foldout = EditorGUILayout.Foldout(foldout, "Advanced : CustomOutputLogPath");
            if (foldout)
            {
                EditorGUI.indentLevel++;
                path = EditorGUILayout.TextField("OutputLogPath", path);
                EditorGUI.indentLevel--;
            }
            else
            {
                path = System.Environment.GetEnvironmentVariable("USERPROFILE");
                path += @"\AppData\LocalLow\VRChat\VRChat";
            }
            targetScript.OutputLogPath = path;

            string[] files = Directory.GetFiles(path, "*.txt");
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Substring(files[i].Length - 13).Remove(9);
            }


            // ラベルの作成
            string label = "LoadOutputLog";
            // 初期値として表示する項目のインデックス番号
            int selectedIndex = targetScript.index;
            // プルダウンメニューの作成
            int index = files.Length > 0 ? EditorGUILayout.Popup(label, selectedIndex, files)
                : -1;

            if (EditorGUI.EndChangeCheck())
            {// 操作を Undo に登録
             // インデックス番号を登録
                targetScript.index = index;
            }

            GUILayout.Space(15);

            //PrivateMethodを実行する用のボタン
            if (GUILayout.Button("LoadLogToExportAnim"))
            {
                ExecuteEvents.Execute<OutputLogLoaderinterface>(
                target: targetScript.gameObject,
                eventData: null,
                functor: (recieveTarget, y) => recieveTarget.LoadLogToExportAnim());
            }

        }
    }
}
 #endif

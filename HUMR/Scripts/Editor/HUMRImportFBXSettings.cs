
/*******
 * HUMRImportFBXSettings.cs
 * 
 * Editor拡張。Editorフォルダの下に配置すること
 * 
 * FBXを読み込んだ際に自動で実行される
 * RigのAnimationTypeをHumanoidにして
 * AnimationのRotaionとPosition(Y)をBake into PoseにしUponをOriginalとしている
 * 
 * 上記の設定に関して不具合等が発生したらこのスクリプトを改造するか削除して対応のこと
 * 
 * *****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
namespace HUMR
{
    public class HUMRImportFBXSettings : AssetPostprocessor
    {
        void OnPreprocessModel()
        {

            if (assetPath.Contains("HUMR/FBXs"))
            {
                ModelImporter importer = (ModelImporter)assetImporter;
                string path = importer.assetPath;
                importer.animationType = ModelImporterAnimationType.Human;

            }
        }
        void OnPreprocessAnimation()
        {
            //このパス以下のAnimationは影響を受ける
            if (assetPath.Contains("HUMR/FBXs"))
            {
                ModelImporter importer = (ModelImporter)assetImporter;

                importer.animationType = ModelImporterAnimationType.Human;
                ModelImporterClipAnimation[] MIclips;
                if (importer.clipAnimations.Length == 0)
                {//初回ロード時
                    MIclips = importer.defaultClipAnimations;
                }
                else
                {
                    MIclips = importer.clipAnimations;
                }
                foreach (ModelImporterClipAnimation MIclip in MIclips)
                {
                    //回転とy軸はポーズに焼きこみ、xz座標は任意にしたほうが汎用性が高そう
                    MIclip.lockRootRotation = true;
                    MIclip.keepOriginalOrientation = true;
                    MIclip.lockRootHeightY = true;
                    MIclip.keepOriginalPositionY = true;
                    //MIclip.lockRootPositionXZ = true;
                    //MIclip.keepOriginalPositionXZ = true;
                }
                importer.clipAnimations = MIclips;

            }
        }

    }
}
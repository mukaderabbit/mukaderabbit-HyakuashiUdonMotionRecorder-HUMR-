# HyakuashiUdonMotionRecorder-HUMR-
VRChat上での動きをHumanoidAnimationに出力します

 

### 確認済み動作環境

* Unity 2018.4.20.f1
* VRCSDK3-WORLD-2020.12.09.04.44_Public
* UdonSharp_v0.19.0
* FBX Exporter Version 3.2.1
* VRChat 2020.1.2
 

### ダウンロード
[こちら](https://github.com/mukaderabbit/mukaderabbit-HyakuashiUdonMotionRecorder-HUMR-/releases)

内容物

- HUMR_Recorder
  - ReadMe(Recorder).txt
  - Recorder.prefab
  - SampleScene.scene
  - Recorder Udon C# Program Asset.asset
  - HUMR.Recorder.cs
- HUMR_OutputLogLoader
  - ReadMe(OutputLogLoader).txt
  - OutputLogLoader.cs
  - OutputLogLoaderEditor.cs
  - HUMRImportFBXSettings
  
### 導入の手順（動画解説）
   https://youtu.be/k_h762DME_U
  
### 導入の手順(Recorder)

VRCSDK3-WorldとUdonSharpをImportしたのちに、RecorderのunitypackageをImportしてください。

Assets\HUMR\Prefabs\Recorder.prefabをワールドSceneのHierarchyに設置してください。

そのワールドをアップロード(またはLocalTest)してワールドに入りログを残します。

VRChatを終了するとC:\Users\username\AppData\LocalLow\VRChat\VRChat\の下にoutput_log_xx-xx-xx.txtが作成されます。

これによってOutputLogLoaderを利用する準備が整いました。

 

### 導入の手順(OutputLogLoader)

PackageManagerからFBXExporterをInstallしたのちに、OutputLogLoaderのunitypackageをImportしてください。

レコードの際に使用したアバターprefabをHierarchyに移動させ、Assets\HUMR\Scripts\Csharp\OutputLogLoader.csをアタッチします。

アニメーションを作成したい人のDisplayNameを打ち込み、最新(レコード時)のoutput_log_xx-xx-xx.txtの”_xx-xx-xx”を選択します。

下にあるLoadLogToExportAnimと書かれたボタンを押します。

Assets\HUMR\FBXs\DisplayName\の下にHumanoidAnimationが出力されます。

 

### License

MIT License






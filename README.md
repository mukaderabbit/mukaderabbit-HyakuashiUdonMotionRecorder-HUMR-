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
  - InteractRecorder.prefab
  - SampleScene.scene
  - Recorder Udon C# Program Asset.asset
  - InteractRecorder Udon C# Program Asset.asset
  - HUMR.Recorder.cs
  - HUMR.InteractRecorder.cs
- HUMR_OutputLogLoader
  - ReadMe(OutputLogLoader).txt
  - OutputLogLoader.cs
  - OutputLogLoaderEditor.cs
  - HUMRImportFBXSettings
  
### 導入の手順（動画解説）
   https://youtu.be/Q1HrIqOT-io
  
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

### 更新履歴

v1.0(2021/02/07) リリース

v1.1(2021/02/08) Quaternion補間が行われていない不具合を修正 

v1.1.1(2021/02/09) TmpAniConへのパスの修正，clip名が空の時には適当な名前が付くように対応 

v1.2(2021/02/12) ログファイル内に複数のレコードログが有った場合に分けて出力するように対応 Interactで録画の停止・再開が行えるInteractRecorderを追加 

### License

MIT License






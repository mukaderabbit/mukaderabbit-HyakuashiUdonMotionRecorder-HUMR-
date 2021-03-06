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

v1.3(2021/04/27) ArmatureのScaleが(1,1,1)でないときに座標が正しく出力されない不具合を修正 ->OutputLogLoaderをv1.3に更新

### トラブルシューティング　Q&A

	Q.Importしたら'TMPro','Chinemachine','Formats'等が書かれたErrorが表示されます

	A.PackageManagerのパッケージが正しく認識されていないようです。
	　FBXExporter等がinstallされていることをご確認の後、
	　下記添付のトラブルシューティング ・認識されないパッケージ(パッケージが認識されない)の項目に記載されている内容を試してみていただけると幸いです。
	　https://docs.unity3d.com/ja/2019.4/Manual/upm-errors.html


	Q.FBXが出力されません

	A.C:\Users\username\AppData\LocalLow\VRChat\VRChat\の下にある出力しようとしたoutput_log_xx-xx-xx.txtを開き、
	　「2021.04.27 21:43:11 Log        -  HUMR:Hyakuashi…」
	　のようなログを見つけてください。
	　（[Hyakuashi]の部分がDisplayNameです）
	　このログのDisplayNameを使用して出力を行ってください。
	　ログがない場合は
	　　・VRChatを終了しているか
	　　・ワールドにRecorderが設置してあるか
	　　・別のログファイルに出力がされていないか
	　　・動作の記録から一週間が経過していないか(VRCは一週間でログを削除します)
	　を確認してみてください。


	Q.出力されたアニメーションがおかしいです

	A.OutputLogLoaderのLoadLogToExportを行うアバターについて、
	　ワールド内で使用したアバターを使ってくださいと案内していますが、
	　無改変のアバターにOutputLogLoaderを使用してアニメーションの出力(LoadLogToExportAnim)を試みてください。
	　(ワールドでのモーションレコードには改変アバターを使用していても問題は無いはずです) 
	　着せ替え等でArmatureの下に複数のHumanoidBone(Hips,Spine等)が存在しているアバターでアニメーションの出力を行うと
	　正しく出力されない場合があるということを認識しております。　

### License

MIT License






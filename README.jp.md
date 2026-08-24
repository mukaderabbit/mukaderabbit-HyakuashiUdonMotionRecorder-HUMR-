# Hyakuashi Udon Motion Recorder

[English](README.md)

HUMR(HyakuashiUdonMotionRecorder)はVRChat上での動きをHumanoidAnimationとして出力するためのunitypackageです。

MITライセンスでの提供ですのでご自由にお使いください。
VRChatの利用規約やモラルに反しない使用をお願いします。
制作者であるHyakuashi(mukaderabbit)はいかなる責任も取れません。


確認済み動作環境

HUMR_VCC_v1.1.1
* Unity 2022.3.22f1
* VRChat SDK - Worlds 3.10.3
* FBX Exporter Version 4.2.1
* VRChat 2026.2.2

### 導入の手順(Recorder)

VCCのVRCSDK3-WorldをAddしたのちに、RecorderのunitypackageをImportしてください。
Assets\HUMR\Prefabs\Recorder.prefabをワールドSceneのHierarchyに設置してください。
そのワールドをアップロード(またはLocalTest)してワールドに入りログを残します。
VRChatを終了するとC:\Users\username\AppData\LocalLow\VRChat\VRChat\の下にoutput_log_xx-xx-xx.txtが作成されます。
これによってOutputLogLoaderを利用する準備が整いました。

### 導入の手順(OutputLogLoader)

OutputLogLoaderのunitypackageをImportしてください。Packages以下に展開されます。
レコードの際に使用したアバターprefabをHierarchyに移動させてください。
Packages\HUMR_OutputLogLoader\Runtime\Scripts\Csharp\OutputLogLoader.csをアバターにアタッチします。
アニメーションにする人のDisplayName(ユーザー名)を打ち込み、最新(レコード時)のoutput_log_xx-xx-xx.txtの”_xx-xx-xx”を選択します。
下にあるLoadLogToExportAnimと書かれたボタンを押します。
Assets\HUMR\FBXs\DisplayName\の下にHumanoidAnimationが出力されます。


動きがOutputLogに出力されるデモワールド
https://vrchat.com/home/launch?worldId=wrld_5962f8a1-bc92-481e-b05a-7cb90eadce34&instanceId=0


更新履歴

v1.0(2021/02/07) リリース  
v1.1(2021/02/08) Quaternion補間が行われていない不具合を修正->OutputLogLoaderをv1.1に更新  
v1.1.1(2021/02/09) TmpAniConへのパスの修正，clip名が空の時には適当な名前が付くように対応 ->OutputLogLoaderをv1.1.1に更新  
v1.2(2021/02/12) VRCを落とさずに複数回記録した際に正常に出力されない不具合を修正 ->OutputLogLoaderをv1.2に更新　インタラクトでレコードの停止・再開を行えるInteractRecorderを追加 ->Recorderをv1.1に更新  
v1.3(2021/04/27) ArmatureのScaleが(1,1,1)でないときに座標が正しく出力されない不具合を修正 ->OutputLogLoaderをv1.3に更新  
v1.3.1(2021/09/12) v1.3でunitypackageに反映が漏れていた修正を反映 ->OutputLogLoaderをv1.3.1に更新  
v1.3.2(2021/11/27) DisplayNameにファイルパスに使用できない文字を使用していた場合に対応 ->OutputLogLoaderをv1.3.1に更新　InteractRecorder.prefabのU#参照が外れていたのを修正,誤字を修正 ->Recorderをv1.1.1に更新  

VCC_v1.0.0(2022/09/22) VCC向けに諸々を更新  
VCC_v1.0.0(2023/12/15) Unity2022.3.6f1で動作確認  
VCC_v1.0.0(2024/05/12) Q&Aを更新  
VCC_v1.1.0(2025/02/12) VRChat 2025.1.2 (OPEN BETA)以降のログファイル形式に対応 ->OutputLogLoaderをv1.1.0に更新　旧バージョンの説明書きとファイルを削除  
VCC_v1.1.1(2026/06/09) 地域による小数点表記の違いに対応。選択と出力に日時を含める変更 ->OutputLogLoaderをv1.1.1に更新  


トラブルシューティング　Q&A

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
	　　・VRChatでメニューを開き、メニュー下タブ右端の歯車マークを押します。開かれた「Settings」を下までスクロールし「Debug」の中の「Logging」が有効化されているか
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






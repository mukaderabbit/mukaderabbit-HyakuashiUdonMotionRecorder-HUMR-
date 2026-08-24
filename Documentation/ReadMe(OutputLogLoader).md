# 導入の手順(OutputLogLoader)

OutputLogLoaderのunitypackageをImportしてください。Packages以下に展開されます。
レコードの際に使用したアバターprefabをHierarchyに移動させてください。
Packages\HUMR_OutputLogLoader\Runtime\Scripts\Csharp\OutputLogLoader.csをアバターにアタッチします。
アニメーションにする人のDisplayNameを打ち込み、最新(レコード時)のoutput_log_xx-xx-xx.txtの”_xx-xx-xx”を選択します。
下にあるLoadLogToExportAnimと書かれたボタンを押します。
Assets\HUMR\FBXs\DisplayName\の下にHumanoidAnimationが出力されます。


トラブルシューティング　Q&A

	Q.Importしたら'TMPro','Chinemachine','Formats'等が書かれたErrorが表示されます

	A.PackageManagerのパッケージが正しく認識されていないようです。
	　Packages以下にFBXExporter等がinstallされていることをご確認の後、
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


### 導入の手順(OutputLogLoader)

PackageManagerからFBXExporterをInstallしたのちに、OutputLogLoaderのunitypackageをImportしてください。
レコードの際に使用したアバターprefabをHierarchyに移動させ、Assets\HUMR\Scripts\Csharp\OutputLogLoader.csをアタッチします。
アニメーションを作成したい人のDisplayNameを打ち込み、最新(レコード時)のoutput_log_xx-xx-xx.txtの”_xx-xx-xx”を選択します。
下にあるLoadLogToExportAnimと書かれたボタンを押します。
Assets\HUMR\FBXs\DisplayName\の下にHumanoidAnimationが出力されます。


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
	　を確認してみてください。


	Q.出力されたアニメーションがおかしいです

	A.OutputLogLoaderのLoadLogToExportを行うアバターについて、
	　ワールド内で使用したアバターを使ってくださいと案内していますが、
	　無改変のアバターにOutputLogLoaderを使用してアニメーションの出力(LoadLogToExportAnim)を試みてください。
	　(ワールドでのモーションレコードには改変アバターを使用していても問題は無いはずです) 
	　着せ替え等でArmatureの下に複数のHumanoidBone(Hips,Spine等)が存在しているアバターでアニメーションの出力を行うと
	　正しく出力されない場合があるということを認識しております。　

	
更新履歴

v1.0(2021/02/07) リリース
v1.1(2021/02/08) Quaternion補間が行われていない不具合を修正 
v1.1.1(2021/02/09) TmpAniConへのパスの修正，clip名が空の時には適当な名前が付くように対応 
v1.2(2021/02/12) ログファイル内に複数のレコードログが有った場合に分けて出力するように対応 
v1.3(2021/04/27) ArmatureのScaleが(1,1,1)でないときに座標が正しく出力されない不具合を修正


MIT License

Copyright (c) 2021 mukaderabbit

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

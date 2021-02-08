
### 導入の手順(OutputLogLoader)

PackageManagerからFBXExporterをInstallしたのちに、OutputLogLoaderのunitypackageをImportしてください。
レコードの際に使用したアバターprefabをHierarchyに移動させ、Assets\HUMR\Scripts\Csharp\OutputLogLoader.csをアタッチします。
アニメーションを作成したい人のDisplayNameを打ち込み、最新(レコード時)のoutput_log_xx-xx-xx.txtの”_xx-xx-xx”を選択します。
下にあるLoadLogToExportAnimと書かれたボタンを押します。
Assets\HUMR\FBXs\DisplayName\の下にHumanoidAnimationが出力されます。


更新履歴

v1.0(2021/02/07) リリース
v1.1(2021/02/08) Quaternion補間が行われていない不具合を修正 
v1.1.1(2021/02/09) TmpAniConへのパスの修正，clip名が空の時には適当な名前が付くように対応 


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

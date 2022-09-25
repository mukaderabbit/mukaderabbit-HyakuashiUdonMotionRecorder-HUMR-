
### 導入の手順(Recorder)

VCCのVRCSDK3-WorldとUdonSharpをAddしたのちに、RecorderのunitypackageをImportしてください。
Assets\HUMR\Prefabs\Recorder.prefabをワールドSceneのHierarchyに設置してください。
そのワールドをアップロード(またはLocalTest)してワールドに入りログを残します。
VRChatを終了するとC:\Users\username\AppData\LocalLow\VRChat\VRChat\の下にoutput_log_xx-xx-xx.txtが作成されます。
これによってOutputLogLoaderを利用する準備が整いました。

更新履歴

v1.0(2021/02/07) リリース
v1.1(2021/02/12) Interactで録画の停止・再開が行えるInteractRecorderを追加 
v1.1.1(2021/11/27) InteractRecorder.prefabのU#参照が外れていたのを修正,誤字を修正 

VCC_v1.0.0(2022/09/22) VCC(U#1.0以上)向けに対応


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

## Acknowledgments

- This project is based on [HyakuashiUdonMotionRecorder-HUMR](https://github.com/mukaderabbit/mukaderabbit-HyakuashiUdonMotionRecorder-HUMR-) by [mukaderabbit](https://github.com/mukaderabbit). Special thanks to them for their initial work and contributions to the open-source community.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details. The original HyakuashiUdonMotionRecorder-HUMR project by mukaderabbit is licensed under the MIT License.

# MotionCapture-FromVRChat-ToUnity

This Unity project is designed to record VR movement in VRChat, utilizing the latest VRChat SDK from the VRChat Creator Companion. It captures the user's movements within the VRChat environment, making it an essential tool for VR enthusiasts and content creators. Vive trackers or other compatible tracking devices for converting a Json file automatically generated from the world .

The intent of this Github page is to create awareness in the western world. 


## Requirements
Outputs movements in VRChat as Humanoid Animation.

Confirmed Operating Environment:
HUMR_VCC_v1.0.0 (New)

- Unity ver 2022.3.6f1 (Installed from VRChat Creator Companion): 2022 World Project template
- VRChat SDK - World version 3.5.0
- VRChat Package Resolver Tool version 0.1.27
- VRChat SDK - Base 3.5.0

# Contents:

- HUMR_Recorder
- ReadMe (Recorder).txt
- Recorder.prefab
- InteractRecorder.prefab
- SampleScene.scene
- Recorder Udon C# Program Asset.asset
- InteractRecorder Udon C# Program Asset.asset
- HUMR.Recorder.cs
- HUMR.InteractRecorder.cs
- HUMR_OutputLogLoader:

- ReadMe (OutputLogLoader).txt
- OutputLogLoader.cs
- OutputLogLoaderEditor.cs
- HUMRImportFBXSettings

Installation Procedure (Video Tutorial):

Unity 2019.4.31f1 (New)
Old Version
Installation Procedure (Recorder) – New:
After adding VRCSDK3-World and UdonSharp from VCC, import the Recorder's unitypackage. Place the Assets\HUMR\Prefabs\Recorder.prefab in the World Scene Hierarchy. Upload the world (or LocalTest) and enter the world to leave a log. When VRChat closes, output_log_xx-xx-xx.txt is created under C:\Users\username\AppData\LocalLow\VRChat\VRChat. This prepares you to use the OutputLogLoader.

Installation Procedure (OutputLogLoader) – New:
Import the OutputLogLoader unitypackage. It will expand under Packages. Move the avatar prefab used during recording to Hierarchy. Attach Packages\HUMR_OutputLogLoader\Runtime\Scripts\Csharp\OutputLogLoader.cs to the avatar. Enter the DisplayName of the person you want to animate, select the "_xx-xx-xx" from the latest output_log_xx-xx-xx.txt (recorded time). Press the LoadLogToExportAnim button below. HumanoidAnimation is output under Assets\HUMR\FBXs\DisplayName.

## Authors

- **mukaderabbit** - *Initial work on HyakuashiUdonMotionRecorder-HUMR* - [mukaderabbit-HyakuashiUdonMotionRecorder-HUMR](https://github.com/mukaderabbit/mukaderabbit-HyakuashiUdonMotionRecorder-HUMR-)

## Contributing

- **Fukuoka Digital Spinning** - *Updated to recent Unity 2022 version and English support* - [DavideRiccitiello](https://github.com/DavideRiccitiello)



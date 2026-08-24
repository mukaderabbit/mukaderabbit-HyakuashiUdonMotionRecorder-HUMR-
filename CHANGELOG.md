# Changelog

## [2.0.0-beta.2] - 2026-08-24

A complete rewrite of the codebase so new features can be implemented more easily. Uses a new log format, can read pre-2.0.0 logs but not write them.

> [!WARNING]
> Remove the old `HUMR OutputLogLoader` package and `Prefabs`, `ReadMe`, `Scenes` and `Scripts` in `Assets/HUMR` before importing.

### Added

- Load Object recordings
- Camera recorder
- Toggles to export .fbx or .anim or both
- Show help box on error
- Ping created assets

### Changed

- Keep original XZ position for .fbx import settings
- Select newest HUMR file automatically

### Fixed

- Export animations relative to Animator transform
- Restore original Animation Controller after export
- Explicit include .fbx extension to delete temp file
- Only change .fbx animationType on export

## [2.0.0-beta.1] - 2026-07-27

### Added

- HUMR Sample World

### Changed

- BaseRecorder: Use Unix timestamp as take number
- BaseRecorder: Start/stop recording on pickup use
- HumrRecordingLoader: Show target type in target dropdown
- New OutputLog format:

```txt
[HUMR] RECORDING;{TargetType};{TargetName};{TakeTimestamp};{RecordTime};{objects[]}
```

### Removed

- BaseRecorder: Remove UdonSharp dependency

## [2.0.0-beta.0] - 2026-07-16

### Added

- Button to explore log file path button in advanced foldout
- Add recording button to `PlayerRecorder.prefab`
- Load separate takes
- GitHub Action to build .unitypackage

### Changed

- Complete rewrite of the codebase
- Combine Recorder and OutputLogLoader `.unitypackage`s into `mukaderabbit.humr.unitypackage`
- New OutputLog format: Semicolons to separate different parameters, more metadata

```txt
[HUMR] RECORDING;{TargetName};{TakeNumber};{RecordingType};{RecordTime};{objects[]}
```

- Select newest log file automatically, sort in reverse order
- Show which log files have HUMR data
- Select player DisplayName from a dropdown

### Fixed

- Do not start recording until avatar loads
- Open log file as read-only
- Restore avatar pose after export

### Removed

- Record all players

## [VCC_1.1.1] - 2026-06-09

### 変更

- 選択と出力に日時を含める変更

### 修正

- `OutputLogLoader` を v1.1.1 に更新
- 地域による小数点表記の違いに対応。

## [VCC_1.1.0] - 2025-02-12

### 変更

- `OutputLogLoader` を v1.1.0 に更新
- VRChat 2025.1.2 (OPEN BETA)以降のログファイル形式に対応

### 削除

- 旧バージョンの説明書きとファイルを削除

## [VCC_1.0.0] - 2024-05-12

### 追加

- VCC向けに諸々を更新
- VCCに合わせて記載やフォルダ構造等を変更。
- VCC(U#1.0以上)向けに対応
- Unity2022.3.6f1で動作確認
- Q&Aを更新

## [1.3.2] - 2021-11-27

### 修正

- `Recorder` を v1.1.1 に更新
- DisplayNameにファイルパスに使用できない文字を使用していた場合に対応
- InteractRecorder.prefabのU#参照が外れていたのを修正
- 誤字を修正

## [1.3.1] - 2021-09-12

### 修正

- `OutputLogLoader` を v1.3.1 に更新
- v1.3でunitypackageに反映が漏れていた修正を反映

## [1.3] - 2021-04-27

### 変更

- `OutputLogLoader` を v1.3 に更新

### 修正

- ArmatureのScaleが(1,1,1)でないときに座標が正しく出力されない不具合を修正

## [1.2] - 2021-02-12

### 追加

- インタラクトでレコードの停止・再開を行えるInteractRecorderを追加

### 変更

- `Recorder` を v1.1 に更新
- `OutputLogLoader` を v1.2 に更新

### 修正

- VRCを落とさずに複数回記録した際に正常に出力されない不具合を修正
- ログファイル内に複数のレコードログが有った場合に分けて出力するように対応

## [1.1.1] - 2021-02-09

### 修正

- `OutputLogLoader` を v1.1.1 に更新
- TmpAniConへのパスの修正，clip名が空の時には適当な名前が付くように対応
- InteractRecorder.prefabのU#参照が外れていたのを修正,誤字を修正

## [1.1] - 2021-02-08

### 変更

- `OutputLogLoader` を v1.1 に更新

### 修正

- Quaternion補間が行われていない不具合を修正

## [1.0] - 2021-02-07

### 追加

- リリース

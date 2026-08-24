# TODO

- [ ] English and Japanese localization
- [ ] Make VPM package site
- [ ] Clean up Editor/Runtime classes
- [ ] Test with Unity 2019-6000
- [ ] Test with World SDK 3.9.0

## BaseRecorder.cs

- [ ] Option for world relative or start position relative recording
- [ ] Add take timestamp and framerate to start tags
- [ ] Add take timestamp, frame count and duration to end
- [ ] Analyze frametimes, did we drop frames?
- [ ] Set TargetName default to hierarchy path

## PlayerRecorder.cs

- [ ] Restart take on avatar change or eye height change
- [ ] T-pose on avatar change to calibrate hip height, save to start tags
- [ ] Record hand and feet positions for IK
- [ ] Record all players
- [ ] Hide `Target Name` in inspector

## PlayerRecorder.prefab

- [ ] Add recording origin marker
- [ ] Add countdown
- [ ] Add recording overlay with status and framerate
- [ ] Advanced options to change framerate
- [ ] Hold right stick up to show stop button

## HumrRecordingLoaderEditor.cs

- [ ] Test target selector actually select only takes belonging to target
- [ ] Test collecting Legacy and Humr targets in same file
- [ ] List takes with durations and checkmarks to include them
- [ ] Use `HumanPoseHandler` to write muscle values and hand and feet IK instead of raw rotations
- [ ] Include displayname and take number in exported animations
- [ ] Detect Avatar height mismatch, scale from calibrated start tags
- [ ] Fix toes rotation
- [ ] Update RecordingFiles if last write time is different
- [ ] Keyframe reduction for .anim files with `AnimationUtility.ReduceKeyframes`

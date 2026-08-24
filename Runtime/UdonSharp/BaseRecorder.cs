#if UDONSHARP
using System;
using System.ComponentModel;
using System.Globalization;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;

namespace Humr
{
    public class BaseRecorder : UdonSharpBehaviour
    {
        [SerializeField] [Tooltip("Target name for recording.")]
        protected string targetName = "Target";

        [SerializeField] [Tooltip("Frames per second for recording.")]
        protected float recordFramerate = 30;

        [SerializeField] [Tooltip("Start recording immediately on scene load.")]
        protected bool recordOnStart = true;

        [SerializeField] [Tooltip("Start recording button, connect onClick to StartRecording custom event.")]
        private Button startRecordButton;

        [SerializeField] [Tooltip("Stop recording button, connect onClick to StartRecording custom event.")]
        private Button stopRecordButton;
        
        [SerializeField] [Tooltip("Target mesh to that changes material to indicate recording state.")]
        private Renderer indicatorRenderer;
        
        [SerializeField] [Tooltip("The material to set the indicator when HUMR is recording.")]
        private Material recordingMaterial;

        private bool _isRecording;
        private float _nextRecordTime;
        private float _recordInterval;
        private float _recordTime;
        private long _takeTimestamp;
        private VRCPickup _pickup;
        private Material _indicatorDefaultMaterial;

        protected TargetType TargetType = TargetType.Object;
        protected object[] RecordingObjects;

        public virtual void Start()
        {
            if (recordOnStart) StartRecording();
            
            _pickup = GetComponent<VRCPickup>();
            if (_pickup != null) _pickup.UseText = "Record";
            
            if (indicatorRenderer != null && recordingMaterial != null)
            {
                _indicatorDefaultMaterial = indicatorRenderer.material;
            }
        }

        private void Update()
        {
            if (!_isRecording) return;

            _recordTime += Time.deltaTime;
            if (_recordTime < _nextRecordTime) return;
            _nextRecordTime = _recordTime + _recordInterval;

            OnRecordTick();
        }

        private void OnDestroy()
        {
            if (_isRecording) StopRecording();
        }

        public virtual void StartRecording()
        {
            _recordTime = 0f;
            _nextRecordTime = _recordTime;
            _recordInterval = 1f / recordFramerate;
            _takeTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _isRecording = true;
            RecordObjects();
            UpdateUI();
        }

        public virtual void StopRecording()
        {
            RecordObjects();
            _isRecording = false;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (startRecordButton != null) startRecordButton.gameObject.SetActive(!_isRecording);
            if (stopRecordButton != null) stopRecordButton.gameObject.SetActive(_isRecording);
            if (indicatorRenderer != null && recordingMaterial != null)
            {
                indicatorRenderer.material = _isRecording ? recordingMaterial : _indicatorDefaultMaterial;
            }
        }

        private void OnRecordTick()
        {
            RecordObjects();
        }

        protected virtual void UpdateRecordingObjects()
        {
        }

        public override void Interact()
        {
            if (_pickup != null) return;
            
            ToggleRecording();
        }

        public override void OnPickupUseDown()
        {
            ToggleRecording();
        }

        private void ToggleRecording()
        {
            if (_isRecording) StopRecording();
            else StartRecording();
        }

        private void RecordObjects()
        {
            var timeStr = _recordTime.ToString(HumrLogger.FloatFormat, CultureInfo.InvariantCulture);
            var typeStr = HumrLogger.TargetTypeToString(TargetType);
            var outputString = string.Join(
                HumrLogger.VariableDelimiter, HumrLogger.RecordingTag, typeStr, targetName, _takeTimestamp, timeStr);

            UpdateRecordingObjects();
            foreach (var recObj in RecordingObjects)
            {
                if (recObj == null) continue;

                switch (recObj.GetType().Name)
                {
                    case "Vector3":
                    {
                        var vector3Str = HumrLogger.FormatVector3Components((Vector3)recObj);
                        outputString = string.Join(HumrLogger.VariableDelimiter, outputString, vector3Str);
                        break;
                    }
                    case "Quaternion":
                    {
                        var quaternionStr = HumrLogger.FormatQuaternionComponents((Quaternion)recObj);
                        outputString = string.Join(HumrLogger.VariableDelimiter, outputString, quaternionStr);
                        break;
                    }
                    default:
                        outputString = string.Join(HumrLogger.VariableDelimiter, recObj.ToString());
                        break;
                }
            }

            HumrLogger.Log(outputString);
        }
    }
}
#endif
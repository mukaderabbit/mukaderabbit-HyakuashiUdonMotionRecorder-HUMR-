using UnityEngine;

namespace Humr
{
    [RequireComponent(typeof(Animator))]
    public class HumrRecordingLoader : MonoBehaviour
    {
        public Animator Animator => GetComponent<Animator>();
        public int fileIndex;
        public int targetIndex;
        public bool exportFbx = true;
        public bool exportAnim; 
        public bool showAdvanced;
        public bool blenderHipFix = true;
    }
}
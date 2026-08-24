#if UDONSHARP
namespace Humr
{
    public class ObjectRecorder : BaseRecorder
    {
        public override void Start()
        {
            TargetType = TargetType.Object;
            RecordingObjects = new object[3];
            base.Start();
        }

        protected override void UpdateRecordingObjects()
        {
            RecordingObjects[0] = transform.position;
            RecordingObjects[1] = transform.rotation;
            RecordingObjects[2] = transform.localScale;
        }
    }
}
#endif
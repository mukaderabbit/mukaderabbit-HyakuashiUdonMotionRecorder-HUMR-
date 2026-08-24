using UnityEngine;

namespace Humr
{
    public enum TargetType
    {
        Unknown,
        Legacy,
        BoneRotations,
        Object,
        BoneRotationsWithIK,
        HumanMuscles
    }
    
    public static class HumrLogger
    {
        private const string HumrTag = "[HUMR]";
        public const string RecordingTag = "RECORDING";
        public const char VariableDelimiter = ';';
        public const char ComponentDelimiter = ',';
        public const string FloatFormat = "F6";

        public static void Log(object message)
        {
            Debug.Log($"{HumrTag} {message}");
        }

        public static void Warning(object message)
        {
            Debug.LogWarning($"{HumrTag} {message}");
        }

        public static void Error(object message)
        {
            Debug.LogError($"{HumrTag} {message}");
        }

        public static void Assertion(object message)
        {
            Debug.LogAssertion($"{HumrTag} {message}");
        }

        public static string FormatVector3Components(Vector3 vector3, string format = FloatFormat)
        {
            var vector3XStr = vector3.x.ToString(format);
            var vector3YStr = vector3.y.ToString(format);
            var vector3ZStr = vector3.z.ToString(format);
            return string.Join(ComponentDelimiter, vector3XStr, vector3YStr, vector3ZStr);
        }

        public static string FormatQuaternionComponents(Quaternion quaternion, string format = FloatFormat)
        {
            var quaternionXStr = quaternion.x.ToString(format);
            var quaternionYStr = quaternion.y.ToString(format);
            var quaternionZStr = quaternion.z.ToString(format);
            var quaternionWStr = quaternion.w.ToString(format);
            return string.Join(ComponentDelimiter, quaternionXStr, quaternionYStr, quaternionZStr, quaternionWStr);
        }

        public static string TargetTypeToString(TargetType targetType)
        {
            switch (targetType)
            {
                case TargetType.Legacy:
                    return "Legacy";
                case TargetType.BoneRotations:
                    return "BoneRotations";
                case TargetType.Object:
                    return "Object";
                case TargetType.BoneRotationsWithIK:
                case TargetType.HumanMuscles:
                case TargetType.Unknown:
                default:
                    return "Unsupported";
            }
        }
    }
}
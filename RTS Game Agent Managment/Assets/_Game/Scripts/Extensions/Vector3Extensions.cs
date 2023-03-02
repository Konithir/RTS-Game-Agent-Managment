using UnityEngine;

namespace Extensions
{

    public static class Vector3Extensions
    {
        public static Vector3 UnsignedDifference(this Vector3 v1, Vector3 v2)
        {
            return new Vector3(Mathf.Abs(v1.x + v2.x), Mathf.Abs(v1.y + v2.y), Mathf.Abs(v1.z + v2.z));
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Extensions.Vector
{
    public static class Vector2Extension
    {
        public static Vector2 Abs(this Vector2 vector)
        {
            return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }

        public static Vector3 Abs(this Vector3 vector)
        {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }

        public static Vector2 Add(this Vector2 vector, float x = 0.0f, float y = 0.0f)
        {
            return new Vector2(vector.x + x, vector.y + y);
        }

        public static Vector3 Add(this Vector3 vector, float x = 0.0f, float y = 0.0f, float z = 0.0f)
        {
            return new Vector3(vector.x + x, vector.y + y, vector.z + z);
        }


    }
}

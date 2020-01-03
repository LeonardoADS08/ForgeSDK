using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Extensions.Vector.Angles
{
    public static class ForgeVectorExtension
    {
        /// <summary>
        /// Calculate the position before a rotation based on an axis
        /// </summary>
        /// <param name="AxisPosition">Axis point</param>
        /// <param name="ObjectPosition">Object point</param>
        /// <param name="AngleToRotate">Angle</param>
        /// <returns>Position before rotation</returns>
        public static Vector3 AxisRotate(this Vector3 AxisPosition, Vector3 ObjectPosition, Quaternion AngleToRotate)
        {
            var direction = ObjectPosition - AxisPosition;
            direction = AngleToRotate * direction;
            return AxisPosition + direction;
        }

        /// <summary>
        /// Calculate the angle (in Z) between two points, you should read the documentation about this:
        /// 
        /// </summary>
        /// <param name="start">Start point</param>
        /// <param name="end">End point</param>
        /// <param name="XtoY">Reference point to calculate angle</param>
        /// <returns>Angle between two objects</returns>
        public static float AngleBetween(this Vector2 start, Vector2 end, bool XtoY = true)
        {
            Vector2 firection = end - start;
            if (XtoY) return Mathf.Atan2(firection.x, firection.y) * Mathf.Rad2Deg;
            else return Mathf.Atan2(firection.y, firection.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        ///  Funcion que devuelve un punto en un circulo a partir de un centro, basado en un radio y un angulo (en grados)
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector3 PointOnCircle(this Vector3 center, float radius, float angle)
        {
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            pos.y = center.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            pos.z = center.z;
            return pos;
        }
    }
}

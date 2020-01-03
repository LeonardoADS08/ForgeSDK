/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : Extensions/Vector/ForgeVectorExtension.cs
Revision        : 1.0
Changelog       :   
*/

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
        /// Calculate the position before a rotation based on an axis <para></para>
        /// <see cref="https://github.com/LeonardoADS08/ForgeSDK/wiki/ForgeVectorExtension"/>
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
        /// Calculate the angle (in Z) between two points, you should read the documentation about this: <para></para>
        /// <see cref="https://github.com/LeonardoADS08/ForgeSDK/wiki/ForgeVectorExtension"/>
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
        /// Calculate position from a circle with a known radius and an angle <para></para>
        /// <see cref="https://github.com/LeonardoADS08/ForgeSDK/wiki/ForgeVectorExtension"/>
        /// </summary>
        /// <param name="Center">Center of the circle</param>
        /// <param name="Radius">Radius of the circle</param>
        /// <param name="Angle">Angle to move through the circle</param>
        /// <returns></returns>
        public static Vector3 PointOnCircle(this Vector3 Center, float Radius, float Angle)
        {
            Vector3 pos;
            pos.x = Center.x + Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
            pos.y = Center.y + Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
            pos.z = Center.z;
            return pos;
        }
    }
}

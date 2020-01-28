using ForgeSDK.Attributes;
using ForgeSDK.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Entities
{
    [RequireComponent(typeof(AttributeSystem))]
    public abstract class Entity : MonoBehaviour
    {
        protected virtual void OnValidate()
        {
            Attributes = GetComponent<AttributeSystem>();
        }

        public AttributeSystem Attributes;
    }
}

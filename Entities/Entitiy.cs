using Assets.ForgeSDK.Attributes;
using ForgeSDK.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Entities
{
    [RequireComponent(typeof(StatSystem), typeof(AttributeSystem))]
    public abstract class Entitiy : MonoBehaviour
    {
        private void OnValidate()
        {
            Stats = GetComponent<StatSystem>();
            Attributes = GetComponent<AttributeSystem>();
        }

        public StatSystem Stats;
        public AttributeSystem Attributes;
    }
}

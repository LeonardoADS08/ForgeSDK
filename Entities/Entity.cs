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
    [RequireComponent(typeof(AttributeSystem), typeof(Animator), typeof(SpriteRenderer))]
    public abstract class Entity : MonoBehaviour
    {
        [HideInInspector]
        public Animator Animator;
        [HideInInspector]
        public SpriteRenderer SpriteRenderer;
        public AttributeSystem Attributes;

        protected virtual void OnValidate()
        {
            // Caching components
            Attributes = GetComponent<AttributeSystem>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();

            // Default attributes
            Attributes.AddAttribute(DefaultAttributes.HEALTH);
            Attributes.AddAttribute(DefaultAttributes.INVULNERABLE);
        }

    }
}

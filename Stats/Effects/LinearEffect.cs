using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Stats.Effects
{
    public class LinearEffect : Effect
    {
        private float _startTime;
        private float _lastProportion = 0.0f;
        public LinearEffect(float time, float value, float totalTime) : base(value, totalTime) 
        {
            _startTime = time;
        }

        protected override float Variation(float time, float deltaTime)
        {
            float proportion = Mathf.Clamp01((time - _startTime) / Time);
            float result = Value * (proportion - _lastProportion);
            _lastProportion = proportion;
            return result;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Anim
    {

        public static float Sin_Time(float _rate = 1, float _min = 0.0f, float _max = 1.0f, float _timeOffset = 0)
        {
            float _HALF_RANGE = (_max - _min) * 0.5f;
        return (_min + _HALF_RANGE) + Mathf.Sin(Runtime(_rate, _timeOffset)) * _HALF_RANGE;
        }

        public static float Cos_Time(float _rate = 1, float _min = 0.0f, float _max = 1.0f, float _timeOffset = 0)
        {
            float _HALF_RANGE = (_max - _min) * 0.5f;
        return (_min + _HALF_RANGE) + Mathf.Cos(Runtime(_rate, _timeOffset)) * _HALF_RANGE;
        }

        public static float Runtime(float _rate = 1f, float _offset = 0)
        {
            return (Time.realtimeSinceStartup + _offset) * _rate;
        }
    }

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

    public static float PNoise(float _rateA = 1, float _rateB = 1, float _offsetA = 0, float _offsetB = 0)
    {
        return Mathf.PerlinNoise(Anim.Runtime(_rateA, _offsetA), Anim.Runtime(_rateB, _offsetB));
    }
    public static Color ColourOscillator(Color _A, Color _B, float _rate = 1, float _offset = 0){
        return Color.Lerp(_A, _B, Sin_Time(_rate: _rate, _timeOffset: +_offset));
    }
}

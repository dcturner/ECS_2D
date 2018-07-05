using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD
{
    public const float DEFAULT_GUTTER_RATIO = 0.95f;

    #region DATA
    public struct Histogram
    {
        public float[] values;
        public Histogram(float[] _values)
        {
            values = _values;
        }
        public float GetValue(int _index)
        {
            return values[_index];
        }
    }
    public struct Graph
    {
        public int binCount;
        public Color[] colours;
        public float[] values;

        public Graph(int _binCount)
        {
            binCount = _binCount;
            values = new float[_binCount];
            colours = new Color[_binCount];
        }
        public void Set_Colour(int _index, Color _col)
        {
            colours[_index] = _col;
        }
        public Color Get_Colour(int _index)
        {
            return colours[_index];
        }
        public void Set_Value(int _index, float _value)
        {
            values[_index] = _value;
        }
        public float Get_Value(int _index)
        {
            return values[_index];
        }
        public void UpdateRandom(float _rate, float _offset)
        {
            for (int i = 0; i < binCount; i++)
            {
                Set_Value(i, Mathf.PerlinNoise(Anim.Runtime(_rate, 0), Anim.Runtime(1f, _offset * i)));
            }
        }
    }
    public struct Spline
    {
        public Vector2[] points;
        public Color colour;
        public float tension;

        public Spline(Vector2[] _points, Color _colour, float _tension, bool _drawPoints = false, int _pointSides = 4)
        {
            points = _points;
            colour = _colour;
            tension = _tension;
        }
        public void Draw()
        {
            // draw points with tension
            // draw points if required
        }
    }
    #endregion

    #region DRAWING
    public static void Draw_BarGraph(Graph _graph, float _x, float _y, float _w, float _h, float _gutterRatio = DEFAULT_GUTTER_RATIO)
    {
        int _COUNT = _graph.binCount;
        float _BAR_SPACE = _w * _gutterRatio;
        float _BAR_THICKNESS = _BAR_SPACE / _COUNT;
        float _GUTTER = (_w - _BAR_SPACE) / (_COUNT - 1);

        for (int i = 0; i < _COUNT; i++)
        {
            Color _COL = _graph.Get_Colour(i);
            float _BIN_VALUE = _graph.Get_Value(i);
            GL_DRAW.Draw_RECT_FILL(
                _x + ((_BAR_THICKNESS * i) + (_GUTTER * i)),
                _y,
                _BAR_THICKNESS,
                _h * _BIN_VALUE,
                new Color(_COL.r, _COL.g, _COL.b, _BIN_VALUE));
        }
    }
    public static void Draw_ArcGraph(int _sides, Graph _graph, float _x, float _y, float _radius_START, float _radius_END, float _angle_MIN, float _angle_MAX, float _gutterRatio = DEFAULT_GUTTER_RATIO)
    {
        int _COUNT = _graph.binCount;
        float _DRAW_RANGE = _radius_END - _radius_START;
        float _BAR_SPACE = _DRAW_RANGE * _gutterRatio;
        float _BAR_THICKNESS = _BAR_SPACE / _COUNT;
        float _GUTTER = (_DRAW_RANGE - _BAR_SPACE) / (_COUNT - 1f);
        float _ANGLE_RANGE = _angle_MAX - _angle_MIN;

        for (int i = 0; i < _COUNT; i++)
        {
            float _BAR_START = _radius_START + ((i * _BAR_THICKNESS) + (i * _GUTTER));
            Color _COL = _graph.Get_Colour(i);
            GL_DRAW.Draw_ARC_FILL(
                _sides,
                _x,
                _y,
                _angle_MIN,
                (_angle_MIN + (_ANGLE_RANGE * _graph.Get_Value(i))),
                    _BAR_START,
                _BAR_START + _BAR_THICKNESS,
                new Color(_COL.r, _COL.g, _COL.b, _graph.Get_Value(i))
            );
        }
    }
    #endregion
}
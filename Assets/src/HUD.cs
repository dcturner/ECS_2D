﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DATA;
using COLOUR;

public class HUD
{
    public const float DEFAULT_GUTTER_RATIO = 0.95f;
    public const int DEFAULT_ARC_SIDES = 24;
    public const float DEFAULT_TEXT_MARGIN = 0.01f;
    public static Color DEFAULT_LINE_COLOUR = Color.white;

    #region DRAWING

    public static void Draw_BarGraph_X(Graph _graph, float _x, float _y, float _w, float _h, Color _col_MAX, Color _col_MIN, bool _alphaFade = false, float _gutterRatio = DEFAULT_GUTTER_RATIO)
    {
        int _COUNT = _graph.binCount;
        float _BAR_SPACE = _w * _gutterRatio;
        float _BAR_THICKNESS = _BAR_SPACE / _COUNT;
        float _GUTTER = (_w - _BAR_SPACE) / (_COUNT - 1);

        for (int i = 0; i < _COUNT; i++)
        {
            float _BIN_VALUE = _graph.Get_Value(i);
            Color _COL = Color.Lerp(_col_MIN, _col_MAX, _BIN_VALUE);
            GL_DRAW.Draw_RECT_FILL(
                _x + ((_BAR_THICKNESS * i) + (_GUTTER * i)),
                _y,
                _BAR_THICKNESS,
                _h * _BIN_VALUE,
                (_alphaFade) ? COL.Set_alphaStrength(_COL, _BIN_VALUE) : _COL);
        }
    }
    public static void Draw_BarGraph_Y(Graph _graph, float _x, float _y, float _w, float _h, Color _col_MIN, Color _col_MAX, bool _alphaFade = false, float _gutterRatio = DEFAULT_GUTTER_RATIO)
    {
        int _COUNT = _graph.binCount;
        float _BAR_SPACE = _h * _gutterRatio;
        float _BAR_THICKNESS = _BAR_SPACE / _COUNT;
        float _GUTTER = (_h - _BAR_SPACE) / (_COUNT - 1);

        for (int i = 0; i < _COUNT; i++)
        {
            float _BIN_VALUE = _graph.Get_Value(i);
            Color _COL = Color.Lerp(_col_MIN, _col_MAX, _BIN_VALUE);
            GL_DRAW.Draw_RECT_FILL(
                _x,
                _y + ((_BAR_THICKNESS * i) + (_GUTTER * i)),
                _w * _BIN_VALUE,
                _BAR_THICKNESS,
                (_alphaFade) ? COL.Set_alphaStrength(_COL, _BIN_VALUE) : _COL);
        }
    }
    public static void Draw_ArcGraph(Graph _graph, float _x, float _y, float _radius_START, float _radius_END, float _angle_MIN, float _angle_MAX, Color _col_MIN, Color _col_MAX, bool _alphaFade = false, int _sides = DEFAULT_ARC_SIDES, float _gutterRatio = DEFAULT_GUTTER_RATIO)
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
            float _BIN_VALUE = _graph.Get_Value(i);
            Color _COL = Color.Lerp(_col_MIN, _col_MAX, _BIN_VALUE);
            GL_DRAW.Draw_ARC_FILL(
                _sides,
                _x,
                _y,
                _angle_MIN,
                (_angle_MIN + (_ANGLE_RANGE * _BIN_VALUE)),
                    _BAR_START,
                _BAR_START + _BAR_THICKNESS,
                (_alphaFade) ? COL.Set_alphaStrength(_COL, _BIN_VALUE) : _COL
            );
        }
    }

    #region < _PARTITIONS


    public static void Draw_ARC_PARTITIONS(int _sides, float _x, float _y, float _radius, float _angleRange, float _thickness, float _gutterSize, float _rotation, params Partition[] _partitions)
    {
        int _TOTAL_PARTITIONS = _partitions.Length;
        float _PARTITION_AREA = (_angleRange * Get_CombinedPartitionArea(_partitions)) - (_gutterSize * (_TOTAL_PARTITIONS - 1));
        float _RADIUS_END = _radius + _thickness;

        float _CURRENT_ANGLE = 0;


        for (int i = 0; i < _TOTAL_PARTITIONS; i++)
        {
            Partition _PARTITION = _partitions[i];
            float _SHARE_AREA = _PARTITION_AREA * _PARTITION.share;
            GL_DRAW.Draw_ARC(_sides, _x, _y, _CURRENT_ANGLE, _CURRENT_ANGLE + _SHARE_AREA, _radius, _RADIUS_END, _PARTITION.colour, _rotation);
            _CURRENT_ANGLE += _gutterSize;
            _CURRENT_ANGLE += _SHARE_AREA;
        }
    }
    public static void Draw_ARC_PARTITIONS_FILL(int _sides, float _x, float _y, float _radius, float _angleRange, float _thickness, float _gutterSize, float _rotation, params Partition[] _partitions)
    {
        int _TOTAL_PARTITIONS = _partitions.Length;
        float _PARTITION_AREA = (_angleRange * Get_CombinedPartitionArea(_partitions)) - (_gutterSize * (_TOTAL_PARTITIONS - 1));
        float _RADIUS_END = _radius + _thickness;

        float _CURRENT_ANGLE = 0;


        for (int i = 0; i < _TOTAL_PARTITIONS; i++)
        {
            Partition _PARTITION = _partitions[i];
            float _SHARE_AREA = _PARTITION_AREA * _PARTITION.share;
            GL_DRAW.Draw_ARC_FILL(_sides, _x, _y, _CURRENT_ANGLE, _CURRENT_ANGLE + _SHARE_AREA, _radius, _RADIUS_END, _PARTITION.colour, _rotation);
            _CURRENT_ANGLE += _gutterSize;
            _CURRENT_ANGLE += _SHARE_AREA;
        }
    }
    public static void Draw_BAR_PARTITIONS_X(float _x, float _y, float _w, float _h, float _gutterSize, params Partition[] _partitions)
    {
        int _TOTAL_PARTITIONS = _partitions.Length;
        float _PARTITION_AREA = (_w * Get_CombinedPartitionArea(_partitions) - (_gutterSize * (_TOTAL_PARTITIONS - 1)));
        float _CURRENT_X = 0;

        for (int i = 0; i < _TOTAL_PARTITIONS; i++)
        {
            Partition _PARTITION = _partitions[i];
            float _SHARE_AREA = _PARTITION_AREA * _PARTITION.share;
            GL_DRAW.Draw_RECT(_x + _CURRENT_X, _y, _SHARE_AREA, _h, _PARTITION.colour);
            _CURRENT_X += _gutterSize;
            _CURRENT_X += _SHARE_AREA;
        }
    }
    public static void Draw_BAR_PARTITIONS_Y(float _x, float _y, float _w, float _h, float _gutterSize, params Partition[] _partitions)
    {
        int _TOTAL_PARTITIONS = _partitions.Length;
        float _PARTITION_AREA = (_h * Get_CombinedPartitionArea(_partitions) - (_gutterSize * (_TOTAL_PARTITIONS - 1)));
        float _CURRENT_Y = 0;

        for (int i = 0; i < _TOTAL_PARTITIONS; i++)
        {
            Partition _PARTITION = _partitions[i];
            float _SHARE_AREA = _PARTITION_AREA * _PARTITION.share;
            GL_DRAW.Draw_RECT(_x, _y + _CURRENT_Y, _w, _SHARE_AREA, _PARTITION.colour);
            _CURRENT_Y += _gutterSize;
            _CURRENT_Y += _SHARE_AREA;
        }
    }
    public static void Draw_BAR_PARTITIONS_FILL_X(float _x, float _y, float _w, float _h, float _gutterSize, params Partition[] _partitions)
    {
        int _TOTAL_PARTITIONS = _partitions.Length;
        float _PARTITION_AREA = (_w * Get_CombinedPartitionArea(_partitions) - (_gutterSize * (_TOTAL_PARTITIONS - 1)));
        float _CURRENT_X = 0;

        for (int i = 0; i < _TOTAL_PARTITIONS; i++)
        {
            Partition _PARTITION = _partitions[i];
            float _SHARE_AREA = _PARTITION_AREA * _PARTITION.share;
            GL_DRAW.Draw_RECT_FILL(_x + _CURRENT_X, _y, _SHARE_AREA, _h, _PARTITION.colour);
            _CURRENT_X += _gutterSize;
            _CURRENT_X += _SHARE_AREA;
        }
    }
    public static void Draw_BAR_PARTITIONS_FILL_Y(float _x, float _y, float _w, float _h, float _gutterSize, params Partition[] _partitions)
    {
        int _TOTAL_PARTITIONS = _partitions.Length;
        float _PARTITION_AREA = (_h * Get_CombinedPartitionArea(_partitions) - (_gutterSize * (_TOTAL_PARTITIONS - 1)));
        float _CURRENT_Y = 0;

        for (int i = 0; i < _TOTAL_PARTITIONS; i++)
        {
            Partition _PARTITION = _partitions[i];
            float _SHARE_AREA = _PARTITION_AREA * _PARTITION.share;
            GL_DRAW.Draw_RECT_FILL(_x, _y + _CURRENT_Y, _w, _SHARE_AREA, _PARTITION.colour);
            _CURRENT_Y += _gutterSize;
            _CURRENT_Y += _SHARE_AREA;
        }
    }
    public static float Get_CombinedPartitionArea(Partition[] _partitions)
    {
        int _TOTAL_PARTITIONS = _partitions.Length;
        float _AREA = 0;
        for (int i = 0; i < _TOTAL_PARTITIONS; i++)
        {
            _AREA += _partitions[i].share;
        }
        return _AREA;
    }
    #endregion _PARTITIONS >

    public static void Draw_LABEL_BOX(string _str, float _x, float _y, float _w, float _h, float _txt_height, float _txt_x, float _txt_y, Color _col_PANEL, Color _col_TXT)
    {
        GL_DRAW.Draw_RECT_FILL(_x, _y, _w, _h, _col_PANEL);
        GL_DRAW.Draw_RECT_FILL(_x + _w + (_h * 0.05f), _y, (_h * 0.05f), _h, _col_PANEL);
        GL_TXT.Txt(_str, _x + (_w * _txt_x), _y + (_h * _txt_y), _txt_height / 5, _col_TXT);

    }
    public static void Draw_LABEL_LINE_X(string _str, float _x, float _y, float _size, float _txt_height, Color _col_line_START, Color _col_line_END, Color _col_txt, float _txt_margin = DEFAULT_TEXT_MARGIN, float _rotation = 1)
    {
        GL.PushMatrix();
        GL_DRAW.TransformMatrix(_x, _y, _rotation);

        GL_DRAW.Draw_LINE(0, 0, 0 + _size, 0, _col_line_START, _col_line_END);
        GL_TXT.Txt("test", 0 + _size + _txt_margin, 0, _txt_height / 5, _col_txt);

        GL.PopMatrix();
    }
    public static void Draw_LABEL_LINE_Y(string _str, float _x, float _y, float _size, float _txt_height, Color _col_line_START, Color _col_line_END, Color _col_txt, float _txt_margin = DEFAULT_TEXT_MARGIN, float _rotation = 1)
    {
        GL.PushMatrix();
        GL_DRAW.TransformMatrix(_x, _y, _rotation);

        GL_DRAW.Draw_LINE(0, 0, 0, _size, _col_line_START, _col_line_END);
        GL_TXT.Txt("test", 0, _size + _txt_margin, _txt_height / 5, _col_txt);

        GL.PopMatrix();
    }
    #region < HISTOGRAMS


    public static void Draw_HISTOGRAM_LINE_X(float _x, float _y, float _w, float _h, Color _col_MIN, Color _col_MAX, bool _alphaFade, Histogram _histogram)
    {
        int _TOTAL_BINS = _histogram.binCount;
        float _DIV = _w / _TOTAL_BINS;

        for (int i = 0; i < _TOTAL_BINS; i++)
        {
            float _CURRENT = (_DIV * i);
            float _BIN_VALUE = _histogram.Get_Value(i);
            Color _COL = Color.Lerp(_col_MIN, _col_MAX, _BIN_VALUE);
            GL_DRAW.Draw_LINE(_CURRENT, _y, _CURRENT, _y + (_h * _histogram.Get_Value(i)), (_alphaFade) ? COL.Set_alphaStrength(_COL, _BIN_VALUE) : _COL);
        }
    }
    public static void Draw_HISTOGRAM_LINE_X(float _x, float _y, float _w, float _h, Color _col_MIN, Color _col_MAX, bool _alphaFade, params float[] _values)
    {
        int _TOTAL_BINS = _values.Length;
        float _DIV = _w / _TOTAL_BINS;

        for (int i = 0; i < _TOTAL_BINS; i++)
        {
            float _CURRENT = _x + (_DIV * i);
            float _BIN_VALUE = _values[i];
            Color _COL = Color.Lerp(_col_MIN, _col_MAX, _BIN_VALUE);
            GL_DRAW.Draw_LINE(_CURRENT, _y, _CURRENT, _y + (_h * _values[i]), (_alphaFade) ? COL.Set_alphaStrength(_COL, _BIN_VALUE) : _COL);
        }
    }
    public static void Draw_HISTOGRAM_POLY_X(float _x, float _y, float _w, float _h, Color _col_MIN, Color _col_MAX, bool _alphaFade, params float[] _values)
    {
        int _TOTAL_BINS = _values.Length;
        float _DIV = _w / _TOTAL_BINS;


        for (int i = 0; i < _TOTAL_BINS; i++)
        {
            float _CURRENT = _x + (_DIV * i);
            float _BIN_VALUE = _values[i];
            Color _COL = Color.Lerp(_col_MIN, _col_MAX, _BIN_VALUE);
            GL_DRAW.Draw_LINE(_CURRENT, _y, _CURRENT, _y + (_h * _values[i]), (_alphaFade) ? COL.Set_alphaStrength(_COL, _BIN_VALUE) : _COL);
        }
    }
    public static void Draw_HISTOGRAM_LINE_Y(float _x, float _y, float _w, float _h, Color _col_MIN, Color _col_MAX, bool _alphaFade, Histogram _histogram)
    {
        int _TOTAL_BINS = _histogram.binCount;
        float _DIV = _h / _TOTAL_BINS;

        for (int i = 0; i < _TOTAL_BINS; i++)
        {
            float _CURRENT = _y + (_DIV * i);
            float _BIN_VALUE = _histogram.Get_Value(i);
            Color _COL = Color.Lerp(_col_MIN, _col_MAX, _BIN_VALUE);
            GL_DRAW.Draw_LINE(_x, _CURRENT, _x + (_w * _histogram.Get_Value(i)), _CURRENT, (_alphaFade) ? COL.Set_alphaStrength(_COL, _BIN_VALUE) : _COL);
        }
    }
    public static void Draw_HISTOGRAM_LINE_Y(float _x, float _y, float _w, float _h, Color _col_MIN, Color _col_MAX, bool _alphaFade = false, params float[] _values)
    {
        int _TOTAL_BINS = _values.Length;
        float _DIV = _h / _TOTAL_BINS;

        for (int i = 0; i < _TOTAL_BINS; i++)
        {
            float _CURRENT = (_DIV * i);
            float _BIN_VALUE = _values[i];
            Color _COL = Color.Lerp(_col_MIN, _col_MAX, _BIN_VALUE);
            GL_DRAW.Draw_LINE(_x, _CURRENT, _x + (_w * _values[i]), _CURRENT, (_alphaFade) ? COL.Set_alphaStrength(_COL, _BIN_VALUE) : _COL);
        }
    }

    public static void Draw_HISTOGRAM_RADIAL(float[] _values, float _x, float _y, float _radius_start, float _radius_end, Color _col_MIN, Color _col_MAX, bool _alphaFade = false, float _angle_start = 0, float _angle_end = 1, float _rotation = 1)
    {
        GL.PushMatrix();
        GL_DRAW.TransformMatrix(_x, _y, _rotation);
        int _TOTAL_BINS = _values.Length;
        float _RANGE_RADIUS = _radius_end - _radius_start;
        float _RANGE_ANGLE = (_angle_end - _angle_start) * GL_DRAW.PI2;
        float _DIV_ANGLE = _RANGE_ANGLE / _TOTAL_BINS;

        for (int i = 0; i < _TOTAL_BINS; i++)
        {
            float _CURRENT_ANGLE = _angle_start + (i * _DIV_ANGLE);
            float _BIN_VALUE = _values[i];
            Vector2 _POS_START = GL_DRAW.PolarCoord(_CURRENT_ANGLE, _radius_start);
            Vector2 _POS_END = GL_DRAW.PolarCoord(_CURRENT_ANGLE, _radius_start + (_RANGE_RADIUS * _BIN_VALUE));
            Color _COL = Color.Lerp(_col_MIN, _col_MAX, _BIN_VALUE);
            GL_DRAW.Draw_LINE(_POS_START.x, GL_DRAW.LockAspect_Y(_POS_START.y), _POS_END.x, GL_DRAW.LockAspect_Y(_POS_END.y), (_alphaFade) ? COL.Set_alphaStrength(_COL, _BIN_VALUE) : _COL);
        }
        GL.PopMatrix();
    }
    public static void Draw_HISTOGRAM_RADIAL(Histogram _histogram, float _x, float _y, float _radius_start, float _radius_end, Color _col_MIN, Color _col_MAX, bool _alphaFade = false, float _angle_start = 0, float _angle_end = 1, float _rotation = 1)
    {
        Draw_HISTOGRAM_RADIAL(_histogram.values, _x, _y, _radius_start, _radius_end, _col_MIN, _col_MAX, _alphaFade, _angle_start, _angle_end, _rotation);
    }
    public static void Draw_HISTOGRAM_BAR_X(float _x, float _y, float _w, float _h, Color _col_MIN, Color _col_MAX, float _gutterRatio, bool _alphaFade, params float[] _values)
    {
        int _COUNT = _values.Length;
        float _BAR_SPACE = _w * _gutterRatio;
        float _BAR_THICKNESS = _BAR_SPACE / _COUNT;
        float _GUTTER = (_w - _BAR_SPACE) / (_COUNT - 1);

        for (int i = 0; i < _COUNT; i++)
        {
            float _BIN_VALUE = _values[i];
            Color _COL = Color.Lerp(_col_MIN, _col_MAX, _BIN_VALUE);
            GL_DRAW.Draw_RECT_FILL(
                _x + ((_BAR_THICKNESS * i) + (_GUTTER * i)),
                _y,
                _BAR_THICKNESS,
                _h * _BIN_VALUE,
                (_alphaFade) ? COL.Set_alphaStrength(_COL, _BIN_VALUE) : _COL);
        }

    }
    #endregion HISTOGRAMS >
    #endregion
}
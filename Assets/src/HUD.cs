using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DATA;
using COLOUR;

public class HUD
{
    public const float DEFAULT_GUTTER_RATIO = 0.95f;
    public const int DEFAULT_ARC_SIDES = 24;
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
    public static void Draw_HISTOGRAM_LINE_Y(float _x, float _y, float _w, float _h, Color _col_MIN, Color _col_MAX, bool _alphaFade, params float[] _values)
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
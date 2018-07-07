using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DATA
{
    public struct Histogram
    {
        public int binCount;
        public float[] values;
        public Histogram(int _binCount)
        {
            binCount = _binCount;
            values = new float[_binCount];
        }
        public Histogram(params float[] _values)
        {
            binCount = _values.Length;
            values = _values;
        }
        public void Set_Value(int _binIndex, float _newValue)
        {
            values[_binIndex] = _newValue;
        }
        public float Get_Value(int _binIndex)
        {
            return values[_binIndex];
        }
        public void UpdateNoise(float _rateA, float _offsetA, float _rateB, float _offsetB)
        {
            for (int i = 0; i < binCount; i++)
            {
                Set_Value(i, Anim.PNoise(_rateA, _offsetA, _rateB, _offsetB));
            }
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
        public void UpdateNoise(float _rateA, float _offsetA, float _rateB, float _offsetB)
        {
            for (int i = 0; i < binCount; i++)
            {
                Set_Value(i, Anim.PNoise(_rateA, _offsetA, _rateB, _offsetB));
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
    public struct Partition
    {
        public string name;
        public float share;
        public Color colour;
        public Partition(string _name, float _share, Color _col)
        {
            name = _name;
            share = _share;
            colour = _col;
        }
    }

    public struct DataSprawl
    {

        List<BitArray> rows;
        int rowLength, totalRows, currentRow, currentCell;
        float timer, minDuration, maxDuration, cellsPerSecond;
        public DataSprawl(int _totalRows, int _rowLength, float _minDuration = 0.1f, float _maxDuration = 1f, float _cellsPerSecond = 0.1f)
        {
            totalRows = _totalRows;
            rowLength = _rowLength;
            rows = new List<BitArray>(totalRows);

            minDuration = _minDuration;
            maxDuration = _maxDuration;
            cellsPerSecond = _cellsPerSecond;
            timer = 0;
            currentRow = 0;
            currentCell = 0;

            InitRows();
            ResetTimer();
        }
        private void ResetTimer()
        {
            timer = Random.Range(minDuration, maxDuration);
        }
        private void InitRows()
        {
            for (int i = 0; i < totalRows; i++)
            {
                rows[i] = new BitArray(rowLength, false);
            }
        }
        public void ClearRow(int _rowIndex)
        {
            BitArray _CELLS = rows[_rowIndex];
            for (int i = 0; i < rowLength; i++)
            {
                _CELLS.Set(i, false);
            }
        }
        public void Update(float _deltaTime)
        {
            timer -= _deltaTime;
            if (timer < 0)
            {
                ResetTimer();
            }
        }
    }
    public class VALUES
    {

        public static float[] RandomValues_01(int _count)
        {
            float[] _RESULT = new float[_count];
            for (int i = 0; i < _count; i++)
            {
                _RESULT[i] = Random.value;
            }
            return _RESULT;
        }
        public static float[] RandomValues(int _count, float _min, float _max)
        {
            float[] _RESULT = new float[_count];
            float _RANGE = _max - _min;
            for (int i = 0; i < _count; i++)
            {
                _RESULT[i] = _min + (Random.value * _RANGE);
            }
            return _RESULT;
        }
        public static float[] RandomValues_NOISE(int _count, float _rateA = 0.1f, float _rateB = 0.2f, float _offset_MAIN = 0)
        {
            float[] _RESULT = new float[_count];
            for (int i = 0; i < _count; i++)
            {
                _RESULT[i] = Mathf.PerlinNoise(_offset_MAIN + (i * _rateA), _offset_MAIN + (i * _rateB));
            }
            return _RESULT;
        }
        public static float[] RandomValues_NOISE_TIME(int _count, float _rateA = 1, float _rateB = 1, float _offsetA = 0, float _offsetB = 0)
        {
            float[] _RESULT = new float[_count];
            for (int i = 0; i < _count; i++)
            {
                _RESULT[i] = Anim.PNoise(_rateA, _rateB, _offsetA * i, _offsetB * i);
            }
            return _RESULT;
        }

        public static Color[] ColourGradient(int _stages, Color _colA, Color _colB)
        {
            Color[] _RESULT = new Color[_stages];
            for (int i = 0; i < _stages; i++)
            {
                _RESULT[i] = Color.Lerp(_colA, _colB, (float)i / _stages);
            }
            return _RESULT;
        }
    }
}

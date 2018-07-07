using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GL_DRAW
{

    static float PI2 = Mathf.PI * 2.0f;
    static float Z = 0f;

    public static float ScreenX(float _x)
    {
        return Screen.width * _x;
    }
    public static float ScreenY(float _y)
    {
        return Screen.height * _y;
    }
    public static float LockAspect_Y(float _y)
    {
        return _y * ((float)Screen.width / Screen.height);
    }
    public static float Angle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }
    public static Vector2 PolarCoord(float _theta, float _radius)
    {
        float _X = Mathf.Sin(_theta) * _radius;
        float _Y = Mathf.Cos(_theta) * _radius;
        return new Vector2(_X, _Y);
    }
    public static Vector2 PolarCoord2(float _theta, float _radiusX, float _radiusY)
    {
        float _X = Mathf.Sin(_theta) * _radiusX;
        float _Y = Mathf.Cos(_theta) * _radiusY;
        return new Vector2(_X, _Y);
    }

    public static void TransformMatrix(float _x, float _y, float _rotation = 0, float _scaleX = 1, float _scaleY = 1)
    {
        Matrix4x4 model = GL.modelview;
        Matrix4x4 m = Matrix4x4.TRS(new Vector3(ScreenX(_x), ScreenY(_y), Z), Quaternion.Euler(0, 0, _rotation * 360f), new Vector3(_scaleX, _scaleY, 1));

        GL.MultMatrix(m * model);

    }

    public static void Add_VERT(float _x, float _y, Color _col)
    {
        GL.Color(_col);
        GL.Vertex3(ScreenX(_x), ScreenY(_y), Z);
    }
    public static void Add_VERT_1to1(float _x, float _y, Color _col)
    {
        GL.Color(_col);
        GL.Vertex3(ScreenX(_x), ScreenX(_y), Z);
    }
    public static void Draw_LINE(float _startX, float _startY, float _endX, float _endY, Color _col)
    {
        GL.Begin(GL.LINES);
        Add_VERT(_startX, _startY, _col);
        Add_VERT(_endX, _endY, _col);
        GL.End();
    }
    public static void Draw_BG(Color _topLeft, Color _topRight, Color _bottomRight, Color _bottomLeft)
    {
        Draw_GRADIENT_RECT_4(0, 0, 1, 1, _topLeft, _topRight, _bottomRight, _bottomLeft);
    }
    public static void Draw_GRADIENT_RECT_4(float _x, float _y, float _w, float _h, Color _topLeft, Color _topRight, Color _bottomRight, Color _bottomLeft)
    {
        GL.Begin(GL.QUADS);
        Add_VERT(_x, _y, _bottomLeft);
        Add_VERT(_x + _w, _y, _bottomRight);
        Add_VERT(_x + _w, _y + _h, _topRight);
        Add_VERT(_x, _y + _h, _topLeft);
        GL.End();
    }
    public static void Draw_GRADIENT_RECT_X(float _x, float _y, float _w, float _h, Color _colA, Color _colB)
    {
        GL.Begin(GL.QUADS);
        Add_VERT(_x, _y, _colA);
        Add_VERT(_x + _w, _y, _colB);
        Add_VERT(_x + _w, _y + _h, _colB);
        Add_VERT(_x, _y + _h, _colA);
        GL.End();
    }
    public static void Draw_GRADIENT_RECT_Y(float _x, float _y, float _w, float _h, Color _colA, Color _colB)
    {
        GL.Begin(GL.QUADS);
        Add_VERT(_x, _y, _colA);
        Add_VERT(_x + _w, _y, _colA);
        Add_VERT(_x + _w, _y + _h, _colB);
        Add_VERT(_x, _y + _h, _colB);
        GL.End();
    }
    public static void Draw_POLY_LINE(Vector3[] _verts, Color[] _colours)
    {
        GL.Begin(GL.LINE_STRIP);

        for (int vertIndex = 0; vertIndex < _verts.Length; vertIndex++)
        {
            Vector3 _tempPos = _verts[vertIndex];
            Color _tempColour = _colours[vertIndex % _colours.Length];
            Add_VERT(_tempPos.x, _tempPos.y, _tempColour);
        }

        GL.End();
    }
    public static void Draw_POLY_LINE(Vector3[] _verts, Color _colour)
    {
        GL.Begin(GL.LINE_STRIP);

        for (int vertIndex = 0; vertIndex < _verts.Length; vertIndex++)
        {
            Vector3 _tempPos = _verts[vertIndex];
            Add_VERT(_tempPos.x, _tempPos.y, _colour);
        }

        GL.End();
    }

    public static void Draw_TRIANGLE(float _x, float _y, float _size, Color _col, float _rotation = 0)
    {
        float _SIZE2 = _size * 0.5f;

        GL.PushMatrix();
        TransformMatrix(_x, _y, _rotation);
        GL.Begin(GL.LINE_STRIP);

        Add_VERT(0, 0, _col);
        Add_VERT(-_SIZE2, -_size, _col);
        Add_VERT(_SIZE2, -_size, _col);
        Add_VERT(0, 0, _col);

        GL.End();
        GL.PopMatrix();
    }

    public static void Draw_TRIANGLE_FILL(float _x, float _y, float _size, Color _col, float _rotation)
    {
        float _SIZE2 = _size * 0.5f;

        GL.PushMatrix();
        TransformMatrix(_x, _y, _rotation);
        GL.Begin(GL.TRIANGLES);

        Add_VERT(0, 0, _col);
        Add_VERT(-_SIZE2, -_size, _col);
        Add_VERT(_SIZE2, -_size, _col);

        GL.End();
        GL.PopMatrix();
    }

    public static void Draw_RECT(float _x, float _y, float _w, float _h, Color _col)
    {

        GL.Begin(GL.LINE_STRIP);

        Add_VERT(_x, _y, _col);
        Add_VERT(_x + _w, _y, _col);
        Add_VERT(_x + _w, _y + _h, _col);
        Add_VERT(_x, _y + _h, _col);
        Add_VERT(_x, _y, _col);

        GL.End();
    }
    public static void Draw_RECT_FILL(float _x, float _y, float _w, float _h, Color _col)
    {
        GL.Begin(GL.QUADS);

        Add_VERT(_x, _y, _col);
        Add_VERT(_x + _w, _y, _col);
        Add_VERT(_x + _w, _y + _h, _col);
        Add_VERT(_x, _y + _h, _col);

        GL.End();
    }
    public static void Draw_NGON_LINE
    (int _sides, float _x, float _y, float _size, Color _col, float _rotation = 0)
    {
        _sides += 1;
        GL.PushMatrix();

        GL.Begin(GL.LINE_STRIP);
        TransformMatrix(_x, _y, _rotation);
        float _DIV = (Mathf.PI * 2) / _sides;
        Vector2 _START = PolarCoord(0, _size);

        Add_VERT_1to1(_START.x, _START.y, _col);
        for (int i = 1; i < _sides; i++)
        {
            Vector2 _POLAR = PolarCoord(i * _DIV, _size);
            Add_VERT_1to1(_POLAR.x, _POLAR.y, _col);
        }
        Add_VERT_1to1(_START.x, _START.y, _col);
        GL.End();

        GL.PopMatrix();
    }
    public static void Draw_NGON(int _sides, float _x, float _y, float _size, float _thickness, Color _col, float _rotation = 0)
    {
        Draw_ARC_FILL(_sides + 1, _x, _y, 0f, 1f, _size, _size + _thickness, _col, _rotation);
    }
    public static void Draw_NGON_FILL(int _sides, float _x, float _y, float _size, Color _col, float _rotation = 0)
    {
        Draw_ARC_FILL(_sides + 1, _x, _y, 0f, 1f, 0f, _size, _col, _rotation);
    }
    public static void Draw_ELLIPSE(int _segments, float _x, float _y, float _w, float _h, Color[] _colour)
    {


        GL.Begin(GL.LINE_STRIP);
        float _DIV = (Mathf.PI * 2) / _segments;
        Vector2 _START = PolarCoord2(0, _w, _h);

        Add_VERT(_START.x + _x, _START.y + _y, _colour[0]);
        for (int i = 1; i < _segments; i++)
        {
            Vector2 _POLAR = PolarCoord2(i * _DIV, _w, _h);
            Add_VERT(_POLAR.x + _x, _POLAR.y + _y, _colour[i % _colour.Length]);
        }
        Add_VERT(_START.x + _x, _START.y + _y, _colour[0]);
        GL.End();
    }

    public static void Draw_ELLIPSE_FILL(int _segments, float _x, float _y, float _w, float _h, Color[] _colour)
    {

        float _DIV = (Mathf.PI * 2) / _segments;


        for (int i = 0; i < _segments; i++)
        {
            Vector2 _V1 = PolarCoord2(i * _DIV, _w, _h);
            Vector2 _V2 = PolarCoord2((i + 1) * _DIV, _w, _h);
            GL.Begin(GL.TRIANGLES);
            Add_VERT(_V1.x + _x, _V1.y + _y, _colour[i % _colour.Length]);
            Add_VERT(_V2.x + _x, _V2.y + _y, _colour[i % _colour.Length]);
            Add_VERT(_x, _y, _colour[i % _colour.Length]);
            GL.End();
        }
    }

    public static void Draw_ARC(int _segments, float _x, float _y, float _start, float _end, float _radius_start, float _radius_end, Color _col, float _rotation = 0)
    {
        float _THETA_START = _start * PI2;
        float _THETA_END = _end * PI2;
        float _THETA_RANGE = _THETA_END - _THETA_START;
        float _THETA_DIV = _THETA_RANGE / (_segments - 1);

        GL.PushMatrix();
        TransformMatrix(_x, _y, _rotation);
        GL.Begin(GL.LINE_STRIP);

        // BOTTOM arc
        Vector2 _START_VEC = PolarCoord(_THETA_START, _radius_start);
        Add_VERT_1to1(_START_VEC.x, _START_VEC.y, _col);
        for (int i = 1; i < _segments; i++)
        {
            Vector2 _V = PolarCoord(_THETA_START + (i * _THETA_DIV), _radius_start);
            Add_VERT_1to1(_V.x, _V.y, _col);
        }
        // TOP arc
        for (int i = 0; i < _segments; i++)
        {
            Vector2 _V = PolarCoord(_THETA_END - (i * _THETA_DIV), _radius_end);
            Add_VERT_1to1(_V.x, _V.y, _col);
        }

        // end cap
        Add_VERT_1to1(_START_VEC.x, _START_VEC.y, _col);
        GL.End();

        GL.PopMatrix();
    }

    public static void Draw_ARC_FILL(int _segments, float _x, float _y, float _start, float _end, float _radius_start, float _radius_end, Color _col, float _rotation = 0)
    {
        float _THETA_START = _start * PI2;
        float _THETA_END = _end * PI2;
        float _THETA_RANGE = _THETA_END - _THETA_START;
        float _THETA_DIV = _THETA_RANGE / (_segments - 1);

        int _TOTAL_SEGMENTS = (_segments * 2) - 1;

        Vector2[] _VECS = new Vector2[_TOTAL_SEGMENTS + 1];

        // create vectors
        for (int i = 0; i < _segments; i++)
        {
            Vector2 _BTM = PolarCoord(_THETA_START + (i * _THETA_DIV), _radius_start);
            Vector2 _TOP = PolarCoord(_THETA_END - (i * _THETA_DIV), _radius_end);

            _VECS[i] = _BTM;
            _VECS[i + _segments] = _TOP;


        }
        GL.PushMatrix();
        TransformMatrix(_x, _y, _rotation);
        for (int i = 0; i < _TOTAL_SEGMENTS; i++)
        {
            Vector2 _VA, _VB, _VC;

            _VA = _VECS[_TOTAL_SEGMENTS - i];
            _VB = _VECS[i];
            _VC = _VECS[i + 1];

            GL.Begin(GL.TRIANGLES);
            Add_VERT_1to1(_VA.x, _VA.y, _col);
            Add_VERT_1to1(_VB.x, _VB.y, _col);
            Add_VERT_1to1(_VC.x, _VC.y, _col);
            GL.End();
        }
        GL.PopMatrix();
    }

    public static void Draw_CROSS(float _x, float _y, float _thickness, float _size, Color _col, float _rotation = 0)
    {
        GL.PushMatrix();
        _size *= 0.5f;
        _thickness *= 0.5f;

        TransformMatrix(_x, _y, _rotation);
        GL.Begin(GL.QUADS);
        // X rect
        Add_VERT_1to1(-_size, -_thickness, _col);
        Add_VERT_1to1(_size, -_thickness, _col);
        Add_VERT_1to1(_size, _thickness, _col);
        Add_VERT_1to1(-_size, _thickness, _col);

        // Y rect
        Add_VERT_1to1(-_thickness, -_size, _col);
        Add_VERT_1to1(_thickness, -_size, _col);
        Add_VERT_1to1(_thickness, _size, _col);
        Add_VERT_1to1(-_thickness, _size, _col);

        GL.End();
        GL.PopMatrix();
    }
    public static void Draw_CHEVRON(float _x, float _y, float _thickness, float _size, Color _col, float _rotation = 0)
    {
        GL.PushMatrix();
        _size *= 0.5f;
        _thickness *= 0.5f;

        TransformMatrix(_x, _y, _rotation);
        GL.Begin(GL.QUADS);

        // LEFT arm
        Add_VERT_1to1(0f, 0f, _col);
        Add_VERT_1to1(-_size, _size, _col);
        Add_VERT_1to1(-(_size - _thickness * 0.5f), _size + (_thickness * 0.5f), _col);
        Add_VERT_1to1(0f, _thickness, _col);

        // RIGHT arm
        Add_VERT_1to1(0f, 0f, _col);
        Add_VERT_1to1(_size, _size, _col);
        Add_VERT_1to1((_size - _thickness * 0.5f), _size + (_thickness * 0.5f), _col);
        Add_VERT_1to1(0f, _thickness, _col);

        GL.End();
        GL.PopMatrix();
    }

    public static void Draw_CHEVRON_FRAME(float _x, float _y, float _w, float _h, float _thickness, float _size, Color _col)
    {
        GL.PushMatrix();
        float _rot = -0.125f;

        // TOP LEFT
        Draw_CHEVRON(_x, _y, _thickness, _size, _col, _rot);

        // TOP RIGHT
        _rot += 0.25f;
        Draw_CHEVRON(_x + _w, _y, _thickness, _size, _col, _rot);

        // BTM RIGHT
        _rot += 0.25f;
        Draw_CHEVRON(_x + _w, _y + _h, _thickness, _size, _col, _rot);

        // BTM LEFT
        _rot += 0.25f;
        Draw_CHEVRON(_x, _y + _h, _thickness, _size, _col, _rot);
        GL.PopMatrix();
    }

    public static void Draw_AXIS(float _x, float _y, float _size, float _width_MAJOR, float _width_MINOR, int _divisions, int _subDiv, Color _col_MAJOR, Color _col_MINOR, float _rotation = 0, float _offset = 0, bool _includeStem = false, float _stemThickness = 0.01f)
    {
        GL.PushMatrix();
        float _DIV = _size / _divisions;

        TransformMatrix(_x, _y, _rotation);

        if (_includeStem)
        {
            Draw_RECT_FILL(0, 0, _size, _stemThickness, _col_MAJOR);
        }

        // draw bookends first
        Draw_LINE(_x, _y, _x + _width_MAJOR, _y, _col_MAJOR);
        Draw_LINE(_x, _y + _size, _x + _width_MAJOR, _y + _size, _col_MAJOR);

        for (int i = 0; i < _divisions; i++)
        {
            float _DIST = Mathf.Clamp((i * _DIV + _offset) % _size, _y, _y + _size);
            if (i % _subDiv == 0)
            {
                //MAJOR MARK
                Draw_LINE(0, _DIST, _width_MAJOR, _DIST, _col_MAJOR);
            }
            else
            {
                //    // MINOR MARK
                Draw_LINE(0, _DIST, _width_MINOR, _DIST, _col_MINOR);
            }
        }
        GL.PopMatrix();
    }
    public static void Draw_AXIS(float _startX, float _startY, float _endX, float _endY, float _width_MAJOR, float _width_MINOR, int _divisions, int _subDiv, Color _col_MAJOR, Color _col_MINOR, float _offset = 0, bool _includeStem = false, float _stemThickness = 0.01f)
    {
        Vector2 _VEC_START = new Vector2(_startX, _startY);
        Vector2 _VEC_END = new Vector2(_endX, _endY);
        float _SIZE = Vector2.Distance(_VEC_START, _VEC_END);
        float _ANGLE = Angle(_VEC_END - _VEC_START) / -360;

        Draw_AXIS(_startX, _startY, _SIZE, _width_MAJOR, _width_MINOR, _divisions, _subDiv, _col_MAJOR, _col_MINOR, _ANGLE, _offset, _includeStem, _stemThickness);
    }

    public static void Draw_GRID_LINE(float _x, float _y, float _w, float _h, int _divsX, int _divsY, Color _col, float _offset_x = 0f, float _offset_y = 0f)
    {
        float _DIV_X = _w / _divsX;
        float _DIV_Y = _h / _divsY;

        float _LEFT = _x;
        float _RIGHT = _x + _w;
        float _BTM = _y;
        float _TOP = _y + _h;

        float _OFFSET_X = (_offset_x * _w) % _DIV_X;
        float _OFFSET_Y = (_offset_y * _h) % _DIV_Y;

        // draw frame first
        Draw_RECT(_x, _y, _w, _h, _col);

        for (int x = 0; x < _divsX; x++)
        {
            GL.Begin(GL.LINES);
            Add_VERT(Mathf.Clamp01(_LEFT + (x * _DIV_X) + _OFFSET_X), _BTM, _col);
            Add_VERT(Mathf.Clamp01(_LEFT + (x * _DIV_X) + _OFFSET_X), _TOP, _col);
            GL.End();
            for (int y = 0; y < _divsY; y++)
            {
                GL.Begin(GL.LINES);
                Add_VERT(_LEFT, Mathf.Clamp01(_BTM + (y * _DIV_Y) + _OFFSET_Y), _col);
                Add_VERT(_RIGHT, Mathf.Clamp01(_BTM + (y * _DIV_Y) + _OFFSET_Y), _col);
                GL.End();
            }
        }
    }

    public static void Draw_GRID_NGON(float _x, float _y, float _w, float _h, int _divsX, int _divsY, int _sides, float _ngonSize, Color _col)
    {
        float _DIV_X = _w / _divsX;
        float _DIV_Y = _h / _divsY;

        for (int x = 0; x <= _divsX; x++)
        {
            for (int y = 0; y <= _divsY; y++)
            {
                Draw_NGON_FILL(_sides, _x + (x * _DIV_X), _y + (y * _DIV_Y), _ngonSize, _col);
            }
        }
    }

    public static void Draw_MATRIX_RECT(float _x, float _y, float _w, float _h, int _cellsX, int _cellsY, Color _col, BitArray _cells, float _rotation = 0)
    {
        GL.PushMatrix();
        TransformMatrix(_x, _y, _rotation);

        float _DIV_X = _w / _cellsX;
        float _DIV_Y = _h / _cellsY;

        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells.Get(i))
            {
                Draw_RECT_FILL(
                    (i % _cellsX) * _DIV_X,
                    Mathf.FloorToInt(i / _cellsX) * _DIV_Y,
                    _DIV_X,
                    _DIV_Y, _col
                );
            }
        }

        GL.PopMatrix();
    }
    public static void Draw_MATRIX_NGON(float _x, float _y, float _w, float _h, int _sides, float _ngonScaleFactor, int _cellsX, int _cellsY, Color _col, BitArray _cells, float _rotation = 0)
    {
        GL.PushMatrix();
        TransformMatrix(_x, _y, _rotation);

        float _DIV_X = _w / _cellsX;
        float _DIV_Y = _h / _cellsY;
        float _NGON_SIZE = _DIV_X * _ngonScaleFactor;

        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells.Get(i))
            {
                Draw_NGON_FILL(
                    _sides,
                    (i % _cellsX) * _DIV_X,
                    Mathf.FloorToInt(i / _cellsX) * _DIV_Y,
                    _NGON_SIZE,
                    _col);
            }
        }

        GL.PopMatrix();
    }
}

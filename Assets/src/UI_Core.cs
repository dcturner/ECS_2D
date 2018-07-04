using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ANIM;

public class UI_Core : MonoBehaviour
{
    static float PI2 = Mathf.PI * 2.0f;
    //static Camera CAM;
    static Material lineMaterial;
    static float Z = 0f;

    private void OnPostRender()
    {
        Color[] colours = new Color[1] { Color.red };
        GL.LoadPixelMatrix();

        Draw_CHEVRON_FRAME(0.25f, 0.75f, Anim.Sin_Time(3f, 0f, 0.15f, 0.3f), 0.25f, 0.05f, 0.05f, Color.cyan);
    }

    public static float ScreenX(float _x)
    {
        return Screen.width * _x;
    }
    public static float ScreenY(float _y)
    {
        return Screen.height * _y;
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
        Matrix4x4 m = Matrix4x4.TRS(new Vector3(ScreenX(_x), ScreenY(_y), Z), Quaternion.Euler(0, 0, _rotation * 360f), new Vector3(_scaleX, _scaleY, 1));
        GL.MultMatrix(m);
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
    }
    public static void Draw_CHEVRON(float _x, float _y, float _thickness, float _size, Color _col, float _rotation = 0)
    {
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
    }

    public static void Draw_CHEVRON_FRAME(float _x, float _y, float _w, float _h, float _thickness, float _size, Color _col)
    {
        float _rot = -0.375f;

        // TOP LEFT
        Draw_CHEVRON(_x, _y, _thickness, _size, _col, _rot);

        // TOP RIGHT
        _rot -= 0.25f;
        Draw_CHEVRON(_x + _w, _y, _thickness, _size, _col, _rot);

        // BTM RIGHT
        _rot -= 0.25f;
        Draw_CHEVRON(_x + _w, _y - _h, _thickness, _size, _col, _rot);

        // BTM LEFT
        _rot -= 0.25f;
        Draw_CHEVRON(_x, _y - _h, _thickness, _size, _col, _rot);
    }
}

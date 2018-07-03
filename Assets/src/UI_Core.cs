using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Core : MonoBehaviour {
    static float PI2 = Mathf.PI * 2.0f;
    //static Camera CAM;
    static Material lineMaterial;
    static float Z = 0f;

    private void OnPostRender()
    {
        Color[] colours = new Color[1] { Color.red };
        GL.LoadPixelMatrix();
        //Draw_ELLIPSE(36, 0.5f, 0.5f, 0.1f, 0.1f, colours);
        //Draw_ELLIPSE_FILL(36, 0.5f, 0.5f, 0.1f, 0.1f, colours);
        Draw_ARC_FILL(50, 0.5f, 0.5f, 0.25f, 0.75f, 0.2f, 0.4f, Color.cyan);
    }

    public static float ScreenX(float _x){
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

    public static void Add_VERT(float _x, float _y, Color _col)
    {
        GL.Color(_col);
        GL.Vertex3(ScreenX(_x), ScreenY(_y), Z);
    }

    public static void Draw_POLY(Vector3[] _verts, Color[] _colours){
        GL.Begin(GL.LINE_STRIP);

        for (int vertIndex = 0; vertIndex < _verts.Length; vertIndex++)
        {
            Vector3 _tempPos = _verts[vertIndex];
            Color _tempColour = _colours[vertIndex % _colours.Length];
            Add_VERT(_tempPos.x, _tempPos.y, _tempColour);
        }

        GL.End();
    }
    public static void Draw_POLY(Vector3[] _verts, Color _colour)
    {
        GL.Begin(GL.LINE_STRIP);

        for (int vertIndex = 0; vertIndex < _verts.Length; vertIndex++)
        {
            Vector3 _tempPos = _verts[vertIndex];
            Add_VERT(_tempPos.x, _tempPos.y, _colour);
        }

        GL.End();
    }


    public static void Draw_RECT(float _x, float _y, float _w, float _h, Color _col){
        
        GL.Begin(GL.LINE_STRIP);

        GL.Vertex3(_x, _y, Z);
        GL.Vertex3(_x + _w, _y, Z);
        GL.Vertex3(_x + _w, _y + _h, Z);
        GL.Vertex3(_x, _y + _h, Z);
        GL.Vertex3(_x, _y, Z);

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

    public static void Draw_ELLIPSE(int _segments, float _x, float _y, float _w, float _h, Color[] _colour){


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

    public static void Draw_ELLIPSE_FILL(int _segments, float _x, float _y, float _w, float _h, Color[] _colour){

        float _DIV = (Mathf.PI * 2) / _segments;


        for (int i = 0; i < _segments; i++)
        {
            Vector2 _V1 = PolarCoord2(i * _DIV, _w, _h);
            Vector2 _V2 = PolarCoord2((i+1) * _DIV, _w, _h);
            GL.Begin(GL.TRIANGLES);
            Add_VERT(_V1.x + _x, _V1.y + _y, _colour[i % _colour.Length]);
            Add_VERT(_V2.x + _x, _V2.y + _y, _colour[i % _colour.Length]);
            Add_VERT(_x, _y, _colour[i % _colour.Length]);
            GL.End();
        }
    }

    public static void Draw_ARC(int _segments, float _x, float _y, float _start, float _end, float _radius_start, float _radius_end, Color _col)
    {
        float _THETA_START = _start * PI2;
        float _THETA_END = _end * PI2;
        float _THETA_RANGE = _THETA_END - _THETA_START;
        float _THETA_DIV = _THETA_RANGE / (_segments-1);

        GL.Begin(GL.LINE_STRIP);

        // BOTTOM arc
        Vector2 _START_VEC = PolarCoord(_THETA_START, _radius_start);
        Add_VERT(_START_VEC.x + _x, _START_VEC.y + _y, _col);
        for (int i = 1; i < _segments; i++)
        {
            Vector2 _V = PolarCoord(_THETA_START + (i*_THETA_DIV), _radius_start);
            Add_VERT(_V.x + _x, _V.y + _y, _col);
        }
        // TOP arc
        for (int i = 0; i < _segments; i++)
        {
            Vector2 _V = PolarCoord(_THETA_END - (i * _THETA_DIV), _radius_end);
            Add_VERT(_V.x + _x, _V.y + _y, _col);
        }

        // end cap
        Add_VERT(_START_VEC.x + _x, _START_VEC.y + _y, _col);
        GL.End();
    }

    public static void Draw_ARC_FILL(int _segments, float _x, float _y, float _start, float _end, float _radius_start, float _radius_end, Color _col)
    {
        float _THETA_START = _start * PI2;
        float _THETA_END = _end * PI2;
        float _THETA_RANGE = _THETA_END - _THETA_START;
        float _THETA_DIV = _THETA_RANGE / (_segments-1);

        int _TOTAL_SEGMENTS = (_segments * 2)-1;

        Vector2[] _VECS = new Vector2[_TOTAL_SEGMENTS + 1];

        // create vectors
        for (int i = 0; i < _segments; i++)
        {
            Vector2 _BTM = PolarCoord(_THETA_START + (i * _THETA_DIV), _radius_start);
            Vector2 _TOP = PolarCoord(_THETA_END - (i * _THETA_DIV), _radius_end);

            _VECS[i] = _BTM;
            _VECS[i +_segments] = _TOP;


        }

        for (int i = 0; i < _TOTAL_SEGMENTS; i++)
        {
            Vector2 _VA, _VB, _VC;

                _VA = _VECS[_TOTAL_SEGMENTS - i];
                _VB = _VECS[i];
                _VC = _VECS[i + 1];

            GL.Begin(GL.TRIANGLES);
            Add_VERT(_VA.x + _x, _VA.y + _y, _col);
            Add_VERT(_VB.x + _x, _VB.y + _y, _col);
            Add_VERT(_VC.x + _x, _VC.y + _y, _col);
            GL.End();
        }
    }
}

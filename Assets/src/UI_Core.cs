using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DATA;
using COLOUR;
public class UI_Core : MonoBehaviour
{
    DATA.Histogram _HIST;
    DATA.DataSprawl _SPRAWL;
    Palette P;
    float MX, MY, MX_EASED, MY_EASED = 0f;
    float M_EASE = 20f;

    WINDOW_HISTOGRAMS wHist;
    private void Awake()
    {
        
        GL_FONT_3x5.Init();
        GL_MATRIX_ANIMS.Init();
        COL.INIT_PALETTES();
        P = COL.Get_Palette(0);
        _HIST = new DATA.Histogram(VALUES.RandomValues_01(20));
        _SPRAWL = new DataSprawl(30, 64, 10, 1, 30);

        wHist = new WINDOW_HISTOGRAMS(0.25f,0.25f,0.5f,0.5f);
        wHist.Init(_colour_A:2, _colour_B:0);
        wHist.transform_start = new GL_DRAW.GL_MATTRIX_TRANSFORM(_rotY:0.1f, _x: -0.1f, _y:-0.1f, _z: -0.1f);
        wHist.transform_end = new GL_DRAW.GL_MATTRIX_TRANSFORM(_rotY: 0.1f, _x: 0.1f, _y: 0.1f, _z: 0.1f);

    }
    private void OnPostRender()
    {
        GL_DRAW.RESET_SKEW();
        P = COL.Get_Palette(Anim.Runtime_int(0.1f) % 2);
        MX = Input.mousePosition.x / Screen.width;
        MY = Input.mousePosition.y / Screen.height;
        MX_EASED += (MX - MX_EASED) / M_EASE;
        MY_EASED += (MY - MY_EASED) / M_EASE;
        Update_HUD_Items();
        GL.LoadOrtho();

        GL_DRAW.Draw_BG_Y(P.Get(0), P.Get(1));
        Draw();
    }
    void Update_HUD_Items()
    {
        //wHist.Update();
    }
    void Draw(){

        // 121 DRAWING
        GL_DRAW.Draw_GRID_DOT(0, 0, 1, 1, 20, 20, P.Get(3));
        GL_DRAW.Draw_ZOOM_GRID(0.9f, .9f, 0.1f, 0.1f, P.Get(2), 15, 15, (0.9f + MX_EASED*0.1f), (0.9f + MY_EASED*0.1f),0.1f);
        GL_DRAW.Draw_ZOOM_GRID(0.95f, 0f, 0.05f, 0.7f, P.Get(4), 4, 20, (0.95f + MX_EASED * 0.05f), (MY_EASED * 0.7f), 0.1f);

        // START ISO DRAWING
        GL_DRAW.SKEW(0.5f, 0f);
        HUD.Draw_LABEL_LINE_X("test", 0.1f, 0.75f, 0.2f, 0.01f, P.Get(3), P.Get(4), P.Get(4));
        float _OFFSET_X = 0.001f + ((1f-MX_EASED) * 0.01f);
        float _OFFSET_Y = 0.002f + (MY_EASED * 0.02f);
        int _COUNT = 10;
        for (int i = 0; i < _COUNT; i++)
        {
            float[] _vals = VALUES.RandomValues_NOISE_TIME(_COUNT, _incrementA: 0.1f);
            HUD.Draw_HISTOGRAM_POLY(0.25f - (i * _OFFSET_X), 0.25f + (i * _OFFSET_Y), 0.55f, 0.2f, P.Get(1), _vals);
        }
        for (int i = 0; i < _COUNT; i++)
        {
            float[] _vals = VALUES.RandomValues_NOISE_TIME(_COUNT, _incrementA: 0.1f);
            HUD.Draw_HISTOGRAM_BAR_X(0.25f + (i * _OFFSET_X), 0.25f - (i * _OFFSET_Y), 0.5f, 0.2f, P.Get(1), P.Get(2), 1f, false, _vals);
        }
    }
}

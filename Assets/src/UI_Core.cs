using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DATA;
using COLOUR;
public class UI_Core : MonoBehaviour
{
    DATA.Histogram _HIST;
    DATA.DataSprawl _SPRAWL;

    WINDOW_HISTOGRAMS wHist;
    private void Awake()
    {
        GL_FONT_3x5.Init();
        GL_MATRIX_ANIMS.Init();
        COL.INIT_PALETTES();
        _HIST = new DATA.Histogram(VALUES.RandomValues_01(20));
        _SPRAWL = new DataSprawl(30, 64, 10, 1, 30);

        //wHist = new WINDOW_HISTOGRAMS(0.5f, 0.5f, 0.25f, 0.25f);
        //wHist.Init(_offsetA: 0.1f, _offsetB: 0.2f, _incrementA: 0.01f, _incrementB: 0.02f, _gutterRatio: 1, _colour_A: 3);

    }
    private void OnPostRender()
    {
        Update_HUD_Items();

        float _MX = Input.mousePosition.x / Screen.width;
        float _MY = Input.mousePosition.y / Screen.height;
        Palette _PAL = COL.Get_Palette(0);
        GL.LoadPixelMatrix();

        float[] _VALS = VALUES.RandomValues_NOISE_TIME(10, 0.5f, 1f, 0.1f, 0.01f);
        //HUD.Draw_HISTOGRAM_POLY_FILL(0.25f, 0.25f, 0.5f, 0.2f, Color.red, Color.white,_VALS);
        HUD.Draw_HISTOGRAM_POLY(0.25f, 0.25f, 0.5f, 0.3f, Color.white, _VALS);
    }

    void Update_HUD_Items()
    {
        //wHist.Update();
    }
}

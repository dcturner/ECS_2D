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

        wHist = new WINDOW_HISTOGRAMS(0.2f, 0.2f, 0.25f, 0.25f);
        wHist.Init(_colour_A:2, _colour_B:0);
        wHist.transform_start = new GL_DRAW.GL_MATTRIX_TRANSFORM(_rotY:0.1f, _z:-4);
        wHist.transform_end = new GL_DRAW.GL_MATTRIX_TRANSFORM(_rotY: 0.1f);
    }
    private void OnPostRender()
    {

        float _MX = Input.mousePosition.x / Screen.width;
        float _MY = Input.mousePosition.y / Screen.height;
        Palette _PAL = COL.Get_Palette(0);
        Update_HUD_Items();
        GL.LoadOrtho();

        wHist.Draw();
    }

    void Update_HUD_Items()
    {
        wHist.Update();
    }
}

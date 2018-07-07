using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DATA;
using COLOUR;
public class UI_Core : MonoBehaviour
{
    DATA.Histogram _HIST;
    private void Awake()
    {
        GL_FONT_3x5.Init();
        GL_MATRIX_ANIMS.Init();
        COL.INIT_PALETTES();
        _HIST = new DATA.Histogram(VALUES.RandomValues_01(20));
    }
    private void OnPostRender()
    {
        float _MX = Input.mousePosition.x / Screen.width;
        float _MY = Input.mousePosition.y / Screen.height;
        Palette _PAL = COL.Get_Palette(0);
        GL.LoadPixelMatrix();
        GL_DRAW.Draw_BG(_PAL.Get(1), _PAL.Get(1), _PAL.Get(2), _PAL.Get(2));
        GL_DRAW.Draw_GRADIENT_RECT_X(0.2f, 0.2f, 0.6f, 0.1f, Color.white, Color.red);
        //int _COUNT = 10;
        //for (int i = 0; i < _COUNT; i++)
        //{
        //    HUD.Draw_HISTOGRAM_BAR_X(0.25f - (i * 0.02f), 0.25f - (i * 0.02f), 0.5f, 0.3f - (i * 0.03f), _PAL.Get(0), _PAL.Get(3), 0.75f, false, VALUES.RandomValues_NOISE_TIME(40, 0.2f, 1f, i * 0.1f, 0.1f));
        //}

    }
}

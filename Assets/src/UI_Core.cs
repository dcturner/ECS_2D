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


    }
}

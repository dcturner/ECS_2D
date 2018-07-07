using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DATA;
using COLOUR;
public class UI_Core : MonoBehaviour
{
    DATA.Histogram _HIST;
    DATA.DataSprawl _SPRAWL;
    private void Awake()
    {
        GL_FONT_3x5.Init();
        GL_MATRIX_ANIMS.Init();
        COL.INIT_PALETTES();
        _HIST = new DATA.Histogram(VALUES.RandomValues_01(20));
        _SPRAWL = new DataSprawl(30, 64, 10, 1, 30);

    }
    private void OnPostRender()
    {
        Update_HUD_Items();

        float _MX = Input.mousePosition.x / Screen.width;
        float _MY = Input.mousePosition.y / Screen.height;
        Palette _PAL = COL.Get_Palette(0);
        GL.LoadPixelMatrix();

        _SPRAWL.Draw(0.25f, 0.2f, 0.2f, 0.4f, Color.white);
    }

    void Update_HUD_Items(){
        _SPRAWL.Update();
    }
}

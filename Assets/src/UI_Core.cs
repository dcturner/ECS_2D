using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UI_Core : MonoBehaviour
{
    private void Awake()
    {
        GL_FONT_3x5.Init();
    }
    private void OnPostRender()
    {
        float _MX = Input.mousePosition.x / Screen.width;
        float _MY = Input.mousePosition.y / Screen.height;
        GL.LoadPixelMatrix();

        int _COUNT = 10;
        float _DIV = 1f / _COUNT;
        for (int i = 0; i < _COUNT; i++)
        {
            HUD.Draw_LABEL(i.ToString(), _DIV * i, 0.75f, _DIV * 0.9f, 0.1f, 0.02f, 0.1f, 0.9f, Anim.ColourOscillator(Color.cyan, Color.white, 3f,i*0.1f), Color.red);
        }
    }
}

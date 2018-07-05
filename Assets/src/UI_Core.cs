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
        //GL_TXT.Txt(Anim.Runtime().ToString(), 0.5f, 0.5f, 0.01f, Color.cyan);
        GL_TXT.Txt_NGON(Anim.Runtime().ToString(), 0.5f, 0.5f, 0.01f, Anim.Sin_Time(_min: 0.25f, _max: 0.75f), 3, Color.cyan);
        //GL_DRAW.Draw_NGON_FILL(3, _MX + 0.01f, _MY, 0.2f, Color.red);
    }
}

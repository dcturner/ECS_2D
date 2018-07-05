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
        GL_TXT.Draw_TXT(Mathf.FloorToInt(Anim.Sin_Time(3f, 0,99)).ToString("##"), _MX + 0.1f, _MY, Anim.Sin_Time(10 * _MX,0.001f, 0.01f), Color.cyan);
    }
}

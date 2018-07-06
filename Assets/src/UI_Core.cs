using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UI_Core : MonoBehaviour
{
    private void Awake()
    {
        GL_FONT_3x5.Init();
        GL_MATRIX_ANIMS.Init();
    }
    private void OnPostRender()
    {
        float _MX = Input.mousePosition.x / Screen.width;
        float _MY = Input.mousePosition.y / Screen.height;
        GL.LoadPixelMatrix();
        GL_MATRIX_ANIMS.Draw(GL_MATRIX_ANIMS.NAME_INC_3X3, Mathf.FloorToInt(Anim.Runtime(2)), 0.25f, 0.25f, 0.1f, Anim.Colour_SWITCH(Color.red, Color.cyan,0.1f, 5f));
    }
}

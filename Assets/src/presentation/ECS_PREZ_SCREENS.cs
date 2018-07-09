using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COLOUR;

public class SCREEN_WELCOME: SCREEN{
    public SCREEN_WELCOME(){
        duration = 5f;
        title = "welcome";
        P = COL.Get_Palette(0);
    }

    public override void BG()
    {
        GL_DRAW.Draw_BG_Y(P.Get(0), P.Get(1));
    }

    public override void Draw()
    {
        base.Draw();

        TXT("hello world", 0.2f, 0.8f, P.Get(3));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COLOUR;

public class SCREEN  {
    public Palette P;
    public string title;
    public float duration;
    public virtual void BG(){}
    public virtual void Draw(){
        BG();
        GL_TXT.Txt(title, PRESENTATION.SCREEN_TITLE_X, PRESENTATION.SCREEN_TITLE_Y, PRESENTATION.DEFAULT_TXT_CELL_HEIGHT/5, P.Get(4));
    }
    public void TXT(string _str, float _x, float _y, Color _col, float _cellSize = PRESENTATION.DEFAULT_TXT_CELL_HEIGHT){
        GL_TXT.Txt(_str, _x, _y, _cellSize, _col);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COLOUR;
using DATA;

public class WINDOW {
    public const float DEFAULT_CONTENT_PADDING_X = 0.1f;
    public const float DEFAULT_CONTENT_PADDING_Y = 0.1f;
    public float x, y, w, h;
    public float pad_X, pad_Y, start_X, start_Y, active_X, active_Y;
    public Palette palette;

    public WINDOW(float _x = 0, float _y = 0, float _w = 1, float _h = 1){
        pad_X = DEFAULT_CONTENT_PADDING_X;
        pad_Y = DEFAULT_CONTENT_PADDING_X;
        Set_X(_x);
        Set_Y(_y);
        Set_W(_w);
        Set_H(_h);
        Set_Palette(0);
    }
    public void Set_Palette(int _index){
        palette = COL.Get_Palette(_index);
    }
    public Color GetColour(int _colourIndex){
        return palette.Get(_colourIndex);
    }
    public virtual void Update(){
        
    }
    public virtual void Draw(){
        
    }

    public void Set_X(float _x){
        
        x = _x;
        start_X = x + (w * pad_X);
    }
    public void Set_Y(float _y)
    {
        y = _y;
        start_Y = y + (h * pad_Y);
    }
    public void Set_W(float _w)
    {
        w = _w;
        active_X = w - (pad_X * 2);
    }
    public void Set_H(float _h)
    {
        h = _h;
        active_Y = h - (pad_Y * 2);
    }
}

public class WINDOW_HEADER{
}

public class WINDOW_HISTOGRAMS : WINDOW{

    public Histogram[] histograms;
    public int totalHistograms;
    // noise settings
    float rateA, rateB, offsetA, offsetB, incrementA, incrementB;
    // layout settings
    float spacing_X, spacing_Y, min_WIDTH, max_WIDTH, min_HEIGHT, max_HEIGHT, gutterRatio;
    float div_X, div_Y, div_WIDTH, div_HEIGHT;
    Color colour_A, colour_B;

    public WINDOW_HISTOGRAMS(float _x = 0, float _y = 0, float _w = 1, float _h = 1):base(_x, _y, _w, _h){
        
    }

    public void Init(
        int _totalHistograms = 10,
        int _bins_MIN = 10,
        int _bins_MAX = 10,

        // noise Settings
        float _rateA = 1,
        float _rateB = 1,
        float _startingOffset = 0.1f,
        float _offsetA = 0,
        float _offsetB = 0,
        float _incrementA = 0,
        float _incrementB = 0,
    
    // layout
        float _spacingX = 0.01f,
        float _spacingY = 0.01f,
        float _min_WIDTH = 0.1f,
        float _max_WIDTH = 1f,
        float _min_HEIGHT = 0.1f,
        float _max_HEIGHT = 0.5f,
        float _gutterRatio = HUD.DEFAULT_GUTTER_RATIO,

        // colour
        int _colour_A = 0,
        int _colour_B = 0
    ){

        totalHistograms = _totalHistograms;

        // noise settings
        rateA = _rateA;
        offsetA = _offsetA;
        incrementA = _incrementA;
        rateB = _rateB;
        offsetB = _offsetB;
        incrementB = _incrementB;

        // layout settings
        spacing_X = _spacingX;
        spacing_Y = _spacingY;
        min_WIDTH = _min_WIDTH;
        max_WIDTH = _max_WIDTH;
        min_HEIGHT = _min_HEIGHT;
        max_HEIGHT = _max_HEIGHT;
        gutterRatio = _gutterRatio;

        colour_A = GetColour(_colour_A);
        colour_B = GetColour(_colour_B);

        histograms = new Histogram[totalHistograms];
        for (int i = 0; i < _totalHistograms; i++)
        {
            histograms[i] = new Histogram(Random.Range(_bins_MIN, _bins_MAX));
        }
    }

    public override void Update()
    {
        div_X = active_X / totalHistograms;
        div_Y = active_Y / totalHistograms;

        for (int i = 0; i < totalHistograms; i++)
        {
            histograms[i].UpdateNoise(rateA, offsetA*i, incrementA, rateB, offsetB*i, incrementB);
        }
    }

    public override void Draw()
    {
     for (int i = 0; i < totalHistograms; i++)
        {
            HUD.Draw_HISTOGRAM_BAR_X(
                start_X + (i * div_X),
                start_Y + (i * div_Y),
                (min_WIDTH + (i * div_WIDTH)) * w, (min_HEIGHT + (i * div_HEIGHT)) * h, colour_A, colour_B, gutterRatio,false, histograms[i].values); 
        }   
    }
}

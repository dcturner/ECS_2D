using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COLOUR;

public class PRESENTATION
{
    public const float SCREEN_TITLE_X = 0.01f;
    public const float SCREEN_TITLE_Y = 0.98f;
    public const float DEFAULT_TXT_CELL_HEIGHT = 0.01f;

    float MX, MY, MX_EASED, MY_EASED = 0f;
    float M_EASE = 20f;

    List<SCREEN> screens;
    int currentScreen_index = 0;
    int totalScreens = 0;
    SCREEN currentScreen, previousScreen;
    float screenTimer;
    public PRESENTATION(params SCREEN[] _screens)
    {
        screens = new List<SCREEN>();
        for (int i = 0; i < _screens.Length; i++)
        {
            AddScreen(_screens[i]);
        }
    }
    public static void INIT(){
        GL_FONT_3x5.Init();
        GL_MATRIX_ANIMS.Init();
        COL.INIT_PALETTES();
    }
    public void AddScreen(SCREEN _screen)
    {
        screens.Add(_screen);
        totalScreens = screens.Count;
    }
    public void Goto(int _index)
    {
        currentScreen_index = _index % totalScreens;
        currentScreen = screens[currentScreen_index];
        screenTimer = currentScreen.duration;
    }
    public void GotoNextScreen(){
        Goto(currentScreen_index + 1);
    }
    public void GotoPreviousScreen()
    {
        Goto(currentScreen_index - 1);
    }
    public void Update()
    {
        GL_DRAW.RESET_SKEW();
        MX = Input.mousePosition.x / Screen.width;
        MY = Input.mousePosition.y / Screen.height;
        MX_EASED += (MX - MX_EASED) / M_EASE;
        MY_EASED += (MY - MY_EASED) / M_EASE;
        GL.LoadOrtho();

        screenTimer -= Time.deltaTime;
        if(screenTimer<0){
            GotoNextScreen();
        }

        Draw();
    }
    public void Draw()
    {
        currentScreen.Draw();
    }
}

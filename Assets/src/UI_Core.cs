using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DATA;
using COLOUR;
public class UI_Core : MonoBehaviour
{
    PRESENTATION PREZ;
    private void Awake()
    {
        PRESENTATION.INIT();
        PREZ = new PRESENTATION(new SCREEN_WELCOME());
    }
    private void OnPostRender()
    {
        PREZ.Update();
    }
}

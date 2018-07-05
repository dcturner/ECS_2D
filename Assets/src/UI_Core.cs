using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UI_Core : MonoBehaviour
{
    HUD.Graph graph;
    private void Awake()
    {
        GL_FONT_3x5.Init();

        graph = new HUD.Graph(20);
        for (int i = 0; i < 20; i++)
        {
            graph.Set_Colour(i, new Color((float)i/20, 1f, 1f));
        }
    }
    private void OnPostRender()
    {
        float _MX = Input.mousePosition.x / Screen.width;
        float _MY = Input.mousePosition.y / Screen.height;
        GL.LoadPixelMatrix();
        graph.UpdateRandom(1f, 0.1f);
        //HUD.Draw_ArcGraph(36, graph, 0.5f, 0.5f, 0.15f, 0.15f + (_MX * 0.2f), 0f, 1f, _MY);
        HUD.Draw_BarGraph(graph, 0.25f, 0.25f, 0.5f, 0.25f);
        GL_DRAW.Draw_AXIS(0.25f, 0.225f, 1f, 0.225f, 0.01f, 0.005f, 40, 5, Color.red, Color.white);
    }
}

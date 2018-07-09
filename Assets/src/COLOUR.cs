using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace COLOUR
{
    public struct Palette
    {
        public int totalColours;
        public List<Color> colours;
        public Palette(params Color[] _colours)
        {
            colours = _colours.ToList();
            totalColours = colours.Count;
        }
        public Color Get(int _index)
        {
            return colours[_index];
        }
        public Color Get(int _index, float _alpha)
        {
            return COL.Set_alphaStrength(colours[_index], _alpha);
        }
        public Color RandomColour()
        {
            return colours[Mathf.FloorToInt(Random.Range(0, totalColours))];
        }
        public Color RandomColour(float _alpha)
        {
            return COL.Set_alphaStrength(colours[Mathf.FloorToInt(Random.Range(0, totalColours))], _alpha);
        }
        public void Draw_Swatches(float _x, float _y, float _w, float _h){
            float _XDIV = _w / totalColours;
            for (int i = 0; i < totalColours; i++)
            {
                GL_DRAW.Draw_RECT_FILL(i * _XDIV, _y, _XDIV, _h, Get(i));
            }
        }
    }
    public class COL
    {
        public static List<Palette> palettes;
        public static void INIT_PALETTES()
        {
            palettes = new List<Palette>();
            palettes.Add(new Palette(
                new Color(0.145f,0.145f,0.145f),
                new Color(.2196f,.2039f,.2313f),
                new Color(0.6039f,0.7921f,0.9176f),
                new Color(0.8f, 0.5411f, 0.3803f),
                new Color(0.8470f, 0.9333f, 0.9727f)

            ));
            palettes.Add(new Palette(
                new Color(0.1294f, 0.1215f, 0.1254f),
                new Color(0.6470f, 0.2901f, 0.2941f),
                new Color(0.8039f, 0.5686f, 0.2431f),
                new Color(0.8431f, 0.6823f, 0.2784f),
                new Color(0.7647f, 0.8196f, 0.8509f)
            ));

        }
        public static Color HSV(float _hue, float _saturation, float _value, float _alpha = 1)
        {
            Color _C = Color.HSVToRGB(_hue, _value, _value);
            _C.a = _alpha;
            return _C;
        }
        public static Color Set_alphaStrength(Color _col, float _strength)
        {

            return new Color(_col.r, _col.g, _col.b, _col.a * _strength);
        }
        public static Palette Get_Palette(int _index)
        {
            return palettes[_index % palettes.Count];
        }
        public static Palette Get_RandomPalette()
        {
            return palettes[Mathf.FloorToInt(Random.Range(0, palettes.Count))];
        }
        public static Color Get_Colour(int _paletteIndex, int _colourIndex)
        {
            Palette _P = palettes[_paletteIndex % palettes.Count];
            return _P.colours[_colourIndex % _P.colours.Count];
        }
    }
}

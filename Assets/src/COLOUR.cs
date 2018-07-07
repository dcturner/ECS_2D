using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace COLOUR
{
    public struct Palette
    {
        public List<Color> colours;
        public Palette(params Color[] _colours)
        {
            colours = _colours.ToList();
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
            return colours[Mathf.FloorToInt(Random.Range(0, colours.Count))];
        }
        public Color RandomColour(float _alpha)
        {
            return COL.Set_alphaStrength(colours[Mathf.FloorToInt(Random.Range(0, colours.Count))], _alpha);
        }
    }
    public class COL
    {
        public static List<Palette> palettes;
        public static void INIT_PALETTES()
        {
            palettes = new List<Palette>();
            palettes.Add(new Palette(HSV(0.524f, 0.042f, 0.327f), HSV(0.000f, 0.000f, 0.149f), HSV(0.049f, 0.692f, 0.618f), HSV(0.533f, 0.633f, 0.690f), HSV(0.528f, 0.056f, 0.212f), HSV(0.534f, 0.815f, 0.894f)));
            palettes.Add(new Palette(HSV(0.944f, 0.046f, 0.127f), HSV(0.530f, 0.441f, 0.600f), HSV(0.123f, 0.683f, 0.555f), HSV(0.087f, 0.715f, 0.531f), HSV(0.022f, 0.472f, 0.378f), HSV(0.985f, 0.524f, 0.486f), HSV(0.943f, 0.489f, 0.461f), HSV(0.146f, 0.857f, 0.945f)));

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

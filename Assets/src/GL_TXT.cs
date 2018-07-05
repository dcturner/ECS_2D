using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GL_TXT
{

}

public class GL_FONT_3x5
{

    public static Dictionary<char, BitArray> glpyhs = new Dictionary<char, BitArray>();
    public static void Init()
    {
        glpyhs.Add('0', Set(new int[] { 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1 }));
        glpyhs.Add('1', Set(new int[] { 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }));
        glpyhs.Add('2', Set(new int[] { 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1 }));
        glpyhs.Add('3', Set(new int[] { 1, 1, 1, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 1, 1 }));

    }
    private static BitArray Set(int[] _cells)
    {
        BitArray result = new BitArray(_cells.Length);
        for (int i = 0; i < _cells.Length; i++)
        {
            result[i] = (_cells[i] == 1) ? true : false;
        }
        return result;
    }
}

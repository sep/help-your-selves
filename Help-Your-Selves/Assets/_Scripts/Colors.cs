using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Colors{
    public static Color Green = new Color(52f/255,223f/255,28f/255);
    public static Color Red = new Color(233f/255,26f/255,38f/255);
    public static Color Blue = new Color(28f/255,114f/255,233f/255, 1f);
    public static Color Gray = new Color(56f/255,56f/255,56f/255);
    public static Color LightGray = new Color(93f/255,93f/255,93f/255);
    public static Color White = new Color(255f/255,255f/255,255f/255);

    public static Color getColorById(int i){
        switch(i){
            case 0: return Colors.LightGray; break;
            case 1: return Colors.Blue; break;
            case 2: return Colors.Red; break;
            default: return Colors.Gray; break;
        }
    }
}
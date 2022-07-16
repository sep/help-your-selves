using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap
{
    private static GameMap instance;
    //private ArrayList<ArrayList<IBlock>> map;

    private GameMap() 
    {
        
    }

    public GameMap getInstance()
    {
        if(instance == null) 
        {
            instance = new GameMap();
        }
        return instance;
    }

    // public IBlock getBlock(int x, int y)
    // {

    // }

}
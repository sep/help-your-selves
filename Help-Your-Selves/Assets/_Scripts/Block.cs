using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBlock
{
    Sprite sprite;
    GameMap map;
    int color = 0;

    private void Awake() {
        this.map = GameMap.getInstance();
    }

    public bool move(Player p) {
        return true;
    }

    public int getX(){
        return (int) this.transform.position.x;
    }

    public int getY(){
        return (int) this.transform.position.y;
    }

    public void die() {
        
    }

}
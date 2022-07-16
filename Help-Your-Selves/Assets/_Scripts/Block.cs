using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBlock
{
    Sprite sprite;
    GameMap map;
    [SerializeField] int color = 0;

    private void Awake() {
        this.map = GameMap.getInstance();
        this.map.registerBlock(this);
    }

    public bool move(Player p) {
        if (p.color != this.color) return false;
        int px = p.getX();
        int py = p.getY();
        int dx = (this.getX() - px);
        int dy = (this.getY() - py);
        int x = this.getX() + dx;
        int y = this.getY() + dy;
        if(this.map.isEmpty(x, y)){
            this.map.unregisterPosition(this.getX(), this.getY());
            this.transform.position += new Vector3(dx,dy, 0);
            this.map.registerBlock(this);
            return true;
        }
        return false;
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
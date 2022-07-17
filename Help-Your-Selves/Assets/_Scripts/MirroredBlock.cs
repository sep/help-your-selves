using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirroredBlock : MonoBehaviour, IBlock
{
    GameMap map;
    [SerializeField] int color = 0;
    private static int xOffset = 16;

    private void Awake() {
        this.map = GameMap.getInstance();
        this.map.registerBlock(this);
        this.map.registerBlock(this, this.getX() + xOffset, this.getY());
        

    }

    public bool move(Player p) {
        if (p.color == this.color || this.color == 0) {
            int px = p.getX();
            if(px > xOffset) px -= xOffset;
            int py = p.getY();
            int dx = (this.getX() - px);
            int dy = (this.getY() - py);
            int x = this.getX() + dx;
            int y = this.getY() + dy;
            if(this.map.isEmpty(x, y) && this.map.isEmpty(x + xOffset, y)){
                this.map.unregisterPosition(this.getX(), this.getY());
                this.map.unregisterPosition(this.getX() + xOffset, this.getY());
                this.transform.position += new Vector3(dx,dy, 0);
                this.map.registerBlock(this);
                this.map.registerBlock(this, this.getX() + xOffset, this.getY());
                return true;
            }
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

    public void setColor(int color){
        this.color = color;
        SpriteRenderer[] sprites = this.GetComponentsInChildren<SpriteRenderer>();
        Color c = Colors.getColorById(color);
        sprites[1].color = c;
        sprites[2].color = c;
    }

}
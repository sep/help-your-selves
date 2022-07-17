using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int playerID = 0;
    private GameMap map;
    private Keys keys;
    
    public int color;
    
    private void Awake()
    {
        this.map = GameMap.getInstance();
        this.map.registerPlayer(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(playerID == 0){
            this.keys = new Keys(){
                up = "w",
                down = "s",
                left = "a",
                right = "d"
            };
        }
        else{
            this.keys = new Keys(){
                up = "up",
                down = "down",
                left = "left",
                right = "right"
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec = this.transform.position;
        bool moving = false;
        if (Input.GetKeyDown(keys.right))
        {
            vec.x += 1f;
            moving = true;
        }
        else if (Input.GetKeyDown(keys.left))
        {
            vec.x += -1f;
            moving = true;
        }
        else if (Input.GetKeyDown(keys.up))
        {
            vec.y += 1f;
            moving = true;    
        }
        else if (Input.GetKeyDown(keys.down))
        {
            vec.y += -1f;
            moving = true;
        }
        if(!moving) return;
        
        IBlock block = this.map.getBlock((int) vec.x, (int) vec.y);
        if(block == null || block.move(this)){
            if(playerID == 0){
                if(vec.x > map.getMiddle() + 1) return;
                this.transform.position = vec;
            }
            else{
                if(vec.x < map.getMiddle()) return;
                this.transform.position = vec;
            }
        }

    }

    public void setColor(int color){
        this.color = color;
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = Colors.getColorById(color);
    }

    public int getX(){
        return (int) this.transform.position.x;
    }

    public int getY(){
        return (int) this.transform.position.y;
    }

    private class Keys{
        public string up,down,left,right;
    }
}

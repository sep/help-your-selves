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
        Debug.Log(block);
        if(block == null || block.move(this)){
            this.transform.position = vec;
        }
    }

    public void changeColor(int i){
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        switch(i){
            case 1: sprite.color = new Color(52,223,28); break;
            case 2: sprite.color = new Color(233,26,38); break;
            default: sprite.color = new Color(255,255,255); break;
        }
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

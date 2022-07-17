using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int playerID = 0;
    private GameMap map;
    
    public int color;
    
    private void Awake()
    {
        this.map = GameMap.getInstance();
        this.map.registerPlayer(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec = this.transform.position;
        if(this.playerID == 0){
            if (Input.GetKeyDown("d"))
            {
                vec.x += 1f;
            }
            else if (Input.GetKeyDown("a"))
            {
            vec.x += -1f;
            }
            else if (Input.GetKeyDown("w"))
            {
                vec.y += 1f;      
            }
            else if (Input.GetKeyDown("s"))
            {
                vec.y += -1f;
            }
        }else{
            if (Input.GetKeyDown("right"))
            {
                vec.x += 1f;
            }
            else if (Input.GetKeyDown("left"))
            {
            vec.x += -1f;
            }
            else if (Input.GetKeyDown("up"))
            {
                vec.y += 1f;      
            }
            else if (Input.GetKeyDown("down"))
            {
                vec.y += -1f;
            }
        }
        
        IBlock block = this.map.getBlock((int) vec.x, (int) vec.y);
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
}

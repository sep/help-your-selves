using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameMap map;
    
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
            vec.y += 1f;      }

        else if (Input.GetKeyDown("down"))
        {
            vec.y += -1f;
        }
        IBlock block = this.map.getBlock((int) vec.x, (int) vec.y);
        if(block == null || block.move(this)){
            this.transform.position = vec;
        }
    }

    public int getX(){
        return (int) this.transform.position.x;
    }

    public int getY(){
        return (int) this.transform.position.y;
    }
}

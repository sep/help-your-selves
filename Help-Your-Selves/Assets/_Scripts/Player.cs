using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update

    private void Awake() {
        this.rb = GetComponent<Rigidbody2D>();
        // this.rb.velocity = new Vector2(-1 , this.rb.velocity.y);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int x = (int) this.rb.velocity.x;
        int y = (int) this.rb.velocity.y;
        if(Input.GetKeyDown("left")) x = -1;
        else if(Input.GetKeyDown("right")) x = 1;
        if(Input.GetKeyDown("down")) y = -1;
        else if(Input.GetKeyDown("up")) y = 1;
        
        this.rb.velocity = new Vector2(x , y);
        
        
    }
}

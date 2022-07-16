using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(0f, 0f, 0f);
        if (Input.GetKeyDown("right"))
        {
            vec.x = 1f;
        }
        else if (Input.GetKeyDown("left"))
        {
           vec.x = -1f;
        }
        else if (Input.GetKeyDown("up"))
        {
            vec.y = 1f;      }

        else if (Input.GetKeyDown("down"))
        {
            vec.y = -1f;
        }
        this.transform.position += vec;
    }
}

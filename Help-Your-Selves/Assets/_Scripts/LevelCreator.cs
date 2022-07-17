using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class LevelCreator : MonoBehaviour
{
    public GameObject wall;
    public GameObject block;
    public GameObject mirror;

    public Player player;
    // Start is called before the first frame update
    void Start(){
        createPerimeter();
        createPlayer(2,16,0,1);
        createPlayer(18,16,1,2);
        JSON thing = objectFromJson("./Assets/LevelFiles/level1.json");
        Debug.Log(thing);
        foreach(JSON i in thing.Blocks){
            Debug.Log($"{i.x}, {i.y}");
            createBlock(block, i.x, i.y);
        }
    }

    void createBlock(GameObject obj, int x, int y){
        Instantiate(obj, new Vector3(x + .5f, y + .5f, 0), Quaternion.identity);
    }

    void createPlayer(int x, int y, int id, int color){
        Player p = Instantiate(player, new Vector3(x + .5f, y + .5f, 0), Quaternion.identity);
        p.playerID = id;
        p.color = color;
        p.changeColor(color);
    }

    void createPerimeter(){
        for(int i = 0; i < 33; i++){
            createBlock(wall, i, 0);
            createBlock(wall, i, 18);
        }
        for(int i = 1; i < 18; i++){
            createBlock(wall, 0, i);
            if(i != 3) createBlock(wall, 16, i);
            createBlock(wall, 32, i);
        }
    }

    private (int x, int y)[] perimeter = new (int x, int y)[] {

    };

    private JSON objectFromJson(string filename){
        JObject obj;
        Debug.Log("Here");
        using (StreamReader r = new StreamReader(filename)){
            string json = r.ReadToEnd();
            Debug.Log(json);
            obj = JToken.ReadFrom(json);
            Debug.Log($"Here \n{obj.player1.x}");
            return obj;
        }
    }

    JsonSerializer serializer = new JsonSerializer();

    private class JSON{
        public JSON[] Blocks;
        public JSON player1;
        object player2;
        public int x,y,color;
    }

    public class Item{
        public int x,y,color;
    }
}

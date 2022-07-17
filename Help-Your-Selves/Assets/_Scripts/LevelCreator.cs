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
    private GameMap map;

    public Player player;
    // Start is called before the first frame update
    void Start(){
        this.map = GameMap.getInstance();
        createLevel(1);
    }

    private void Update(){
        if(map.isPlayerOnGoal(0) && map.isPlayerOnGoal(1)){
            Debug.Log("You won");
        }
    }

    void createLevel(int levelNum){
        JSON level = objectFromJson($"./Assets/LevelFiles/level{levelNum}.json");
        createPerimeter(level.goal.y);
        map.registerGoal(level.goal.x, level.goal.y);
        foreach(Item i in level.Mirrors){
            createBlock(mirror, i.x, i.y);
        }
        Item p1 = level.player1;
        Item p2 = level.player2;
        createPlayer(p1.x,p1.y,p1.id,p1.color);
        createPlayer(p2.x,p2.y,p2.id,p2.color);
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

    void createPerimeter(int goal){
        for(int i = 0; i < 33; i++){
            createBlock(wall, i, 0);
            createBlock(wall, i, 18);
        }
        for(int i = 1; i < 18; i++){
            createBlock(wall, 0, i);
            if(i != goal) createBlock(wall, 16, i);
            createBlock(wall, 32, i);
        }
    }

    private JSON objectFromJson(string filename){
        JSON obj;
        using (StreamReader r = new StreamReader(filename)){
            JsonSerializer serializer = new JsonSerializer();
            obj = (JSON)serializer.Deserialize(r, typeof(JSON));
            return obj;
        }
    }

    private class JSON{
        public Item[] Mirrors;
        public Item[] Blocks;
        public Item player1;
        public Item player2;
        public Item goal;
    }

    public class Item{
        public int x,y,color,id;
    }
}

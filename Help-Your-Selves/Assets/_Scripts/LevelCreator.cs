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
    private int currentLevel;

    private ArrayList objectList;

    public Player player;
    // Start is called before the first frame update
    void Start(){
        this.map = GameMap.getInstance();
        this.objectList = new ArrayList();
        this.currentLevel = 3;
        createLevel(currentLevel);
    }

    private void Update(){
        if(Input.GetKeyDown("r")){
            restart();
        }
        if(map.isPlayerOnGoal(0) && map.isPlayerOnGoal(1)){
            nextLevel();
        }
    }

    void createLevel(int levelNum){
        JSON level = objectFromJson($"./Assets/LevelFiles/level{levelNum}.json");
        createPerimeter(level.goal.y);
        map.registerGoal(level.goal.x, level.goal.y);
        foreach(Item i in level.Mirrors){
            createMirror(i.x, i.y, i.color);
        }
        foreach(Item i in level.Blocks){
            createBlock(this.block, i.x, i.y, i.color);
        }
        Item p1 = level.player1;
        Item p2 = level.player2;
        createPlayer(p1.x,p1.y,p1.id,p1.color);
        createPlayer(p2.x,p2.y,p2.id,p2.color);
    }

    void createBlock(GameObject obj, int x, int y, int color){
        GameObject b = Instantiate(obj, new Vector3(x + .5f, y + .5f, 0), Quaternion.identity);
        Block m = b.GetComponent<Block>();
        m.setColor(color);
        this.objectList.Add(b);
    }

    void createMirror(int x, int y, int color){
        GameObject block = Instantiate(mirror, new Vector3(x + .5f, y + .5f, 0), Quaternion.identity);
        MirroredBlock m = block.GetComponent<MirroredBlock>();
        m.changeColor(color);
        m.setColor(color);
        this.objectList.Add(block);
    }

    void createPlayer(int x, int y, int id, int color){
        Player p = Instantiate(player, new Vector3(x + .5f, y + .5f, 0), Quaternion.identity);
        p.playerID = id;
        p.color = color;
        p.changeColor(color);
        this.objectList.Add(p.gameObject);
    }

    void createPerimeter(int goal){
        for(int i = 0; i < 33; i++){
            createBlock(wall, i, 0, -1);
            createBlock(wall, i, 18, -1);
        }
        for(int i = 1; i < 18; i++){
            createBlock(wall, 0, i, -1);
            if(i != goal) createBlock(wall, 16, i, -1);
            createBlock(wall, 32, i, -1);
        }
    }

    public void nextLevel(){
        this.currentLevel += 1;
        clear();
        createLevel(currentLevel);
    }

    private void restart(){
        clear();
        createLevel(currentLevel);
    }
    
    private void clear(){
        map.reset();
        foreach(GameObject g in objectList){
            GameObject.Destroy(g);
        }
        objectList = new ArrayList();
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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

public class LevelCreator : MonoBehaviour
{
    public GameObject wall;
    public GameObject block;
    public GameObject mirror;
    public GameObject victoryMenuUI;
    public GameObject wonUI;
    public GameObject goal;
    private GameMap map;
    private int currentLevel;
    private int maxLevel = 8;

    private ArrayList objectList;

    public Player player;
    // Start is called before the first frame update
    void Start(){
        this.map = GameMap.getInstance();
        this.objectList = new ArrayList();
        this.currentLevel = 1;
        createLevel(currentLevel);
        victoryMenuUI.SetActive(false);
        wonUI.SetActive(false);
    }

    private void Update(){
        if(Input.GetKeyDown("r")){
            restart();
        }
        if(Input.GetKeyDown("[")){
            prevLevel();
        }
        if(Input.GetKeyDown("]")){
            nextLevel();
        }
        if(map.isPlayerOnGoal(0) && map.isPlayerOnGoal(1)){
            if(currentLevel >= maxLevel) showWon();
            showVictory();
        }
    }

    public void showVictory(){
        victoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    private void hideAll(){
        victoryMenuUI.SetActive(false);
        wonUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void showWon(){
        wonUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void createLevel(int levelNum){
        XML level = ConvertXmlToObject($"./Assets/LevelFiles/level{levelNum}.xml");
        createPerimeter(level.goal.y);
        map.registerGoal(level.goal.x, level.goal.y);
        foreach(Item i in level.MirroredBlocks){
            createMirror(i.x, i.y, i.color);
        }
        foreach(Item i in level.Block){
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
        m.setColor(color);
        this.objectList.Add(block);
    }

    void createPlayer(int x, int y, int id, int color){
        Player p = Instantiate(player, new Vector3(x + .5f, y + .5f, 0), Quaternion.identity);
        p.playerID = id;
        p.color = color;
        p.setColor(color);
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
            else objectList.Add(Instantiate(this.goal, new Vector3(16.5f, i + .5f, 0), Quaternion.identity));
            createBlock(wall, 32, i, -1);
        }
    }

    public void nextLevel(){
        if (this.currentLevel == this.maxLevel) return;
        this.currentLevel += 1;
        Debug.Log(currentLevel);
        if(currentLevel > maxLevel) showWon();
        clear();
        createLevel(currentLevel);
        hideAll();
    }

    public void prevLevel(){
        if (this.currentLevel == 1) return;
        this.currentLevel -= 1;
        clear();
        createLevel(currentLevel);
        hideAll();
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

        public XML ConvertXmlToObject(string filename){
        XML obj;
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "level";
        xRoot.IsNullable = true;
        using (StreamReader r = new StreamReader(filename)){
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(XML),xRoot);
            obj = (XML)serializer.Deserialize(r);
            return obj;
        }
    }

public class Item {
    [XmlElement]
    public int x;
    [XmlElement]
    public int y;
    [XmlElement]
    public int color;
    [XmlElement]
    public int id;
}
public class XML {
    [XmlArray("MirroredBlocks")]
    [XmlArrayItem("Mirrors", typeof(Item))]
    public Item[] MirroredBlocks { get; set; }
    [XmlArray("Block")]
    [XmlArrayItem("Blocks", typeof(Item))]
    public Item[] Block { get; set; }
    [XmlElement]
    public Item player1;
    [XmlElement]
    public Item player2;
    [XmlElement]
    public Item goal;
}
}

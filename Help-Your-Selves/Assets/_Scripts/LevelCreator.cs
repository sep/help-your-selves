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
    public GameObject victoryMenuUI;
    public GameObject wonUI;
    public GameObject goal;
    private GameMap map;
    private Level[] levels;
    private int currentLevel;
    private int maxLevel = 8;

    private ArrayList objectList;

    public Player player;
    // Start is called before the first frame update
    void Start(){
        loadLevelObjects();
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
        Level level = levels[levelNum - 1];
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

    private void loadLevelObjects()
    {
        this.levels = new Level[this.maxLevel];
        this.levels[0] = new Level1();
        this.levels[1] = new Level2();
        this.levels[2] = new Level3();
        this.levels[3] = new Level4();
        this.levels[4] = new Level5();
        this.levels[5] = new Level6();
        this.levels[6] = new Level7();
        this.levels[7] = new Level8();
    }
}

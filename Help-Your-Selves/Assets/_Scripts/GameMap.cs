using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap
{
    private static GameMap instance;
    //private ArrayList<ArrayList<IBlock>> map;
    private IBlock[,] mapLeft;
    private IBlock[,] mapRight;
    private Player playerLeft;
    private Player playerRight;
    private int verticalOffset;
    private static int leftOffset = 16;
    private Vector2 goal;

    private GameMap() 
    {
        mapLeft = new IBlock[17,19];
        mapRight = new IBlock[17,19];
        verticalOffset = 0;
    }

    public static GameMap getInstance()
    {
        if(instance == null) 
        {
            instance = new GameMap();
        }
        return instance;
    }

    public void registerGoal(int x, int y){
        this.goal = new Vector2(x,y);
    }

    public void registerBlock(IBlock block, int x = -1, int y = -1){
        x = x == -1 ? block.getX() : x;
        y = y == -1 ? block.getY() : y;

        if(!isEmpty(x, y)) Debug.LogError($"Cannot register block at position ({x}, {y}) occupied by {this.getBlock(x, y)}");
        if(x <= leftOffset){
            mapLeft[x, y - verticalOffset] = block;
        }
        else{
            mapRight[x - leftOffset, y - verticalOffset] = block;
        }
    }

    public void registerPlayer(Player p){
        int x = p.getX();

        if(!isEmpty(x, p.getY())) Debug.LogError("Register player on taken position");

        if(x < leftOffset){
            playerLeft = p;
        }
        else{
            playerRight = p;
        }
    }

    public void unregisterPosition(int x, int y){
        if(x == leftOffset){
            mapRight[x, y - verticalOffset] = null;
        }
        if(x < leftOffset){
            mapLeft[x, y - verticalOffset] = null;
        }
        else{
            mapRight[x - leftOffset, y - verticalOffset] = null;
        }
    }

    public IBlock getBlock(int x, int y)
    {
        if(x == leftOffset){
            return mapLeft[x, y - verticalOffset];
        }
        if(x < leftOffset){
            return mapLeft[x, y - verticalOffset];
        }
        else{
            return mapRight[x - leftOffset, y - verticalOffset];
        }
    }

    public int getMiddle(){ return leftOffset; }

    public bool isEmpty(int x, int y){
        if(x == leftOffset) return mapLeft[x, y - verticalOffset] == null;
        if(x < leftOffset){
            return mapLeft[x, y - verticalOffset] == null && (playerLeft == null || !(x == playerLeft.getX() && y == playerLeft.getY()));
        }
        else{
            return mapRight[x - leftOffset, y - verticalOffset] == null  && (playerRight == null || !(x == playerRight.getX() && y == playerRight.getY()));
        }
    }

    public bool isPlayerOnGoal(int player){
        if(player == 0){ return playerLeft != null && playerLeft.getX() == goal.x && playerLeft.getY() == goal.y; }
        else { return  playerRight != null && playerRight.getX() == goal.x && playerRight.getY() == goal.y; }
    }

    public void reset(){
        mapLeft = new IBlock[17,19];
        mapRight = new IBlock[17,19];
        verticalOffset = 0;
        playerLeft = null;
        playerRight = null;
        // goal = null;
    }

}
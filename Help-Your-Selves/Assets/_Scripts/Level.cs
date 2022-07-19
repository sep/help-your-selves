public class Level
{   
    public Item[] Mirrors;
    public Item[] Blocks;
    public Item player1;
    public Item player2;
    public Item goal;
}

public class Item
{
    public int x, y, color, id;
    public Item(int x, int y, int color = 0, int id = 0)
    {
        this.x = x;
        this.y = y;
        this.color = color;
        this.id = id;
    }
}
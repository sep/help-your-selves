import json
import sys, os

def load_json(infile):
    with open(infile) as f:
        data = json.load(f)
    return data

def print_level_class(data, class_name="LevelX", outfile=sys.stdout):
    Mirrors = data["Mirrors"]
    Blocks = data["Blocks"]
    player1 = data["player1"]
    player2 = data["player2"]
    goal = data["goal"]
    print(f'public class {class_name} : Level\n{{\n    public {class_name}()\n    {{', file=outfile)
    print(f'        this.Mirrors = new Item[{len(Mirrors)}];', file=outfile)
    for i in range(len(Mirrors)):
        mirror = Mirrors[i]
        print(f'        this.Mirrors[{i}] = new Item({mirror["x"]}, {mirror["y"]}, {mirror["color"]});', file=outfile)
    print(f'        this.Blocks = new Item[{len(Blocks)}];', file=outfile)    
    for i in range(len(Blocks)):
        block = Blocks[i]
        print(f'        this.Blocks[{i}] = new Item({block["x"]}, {block["y"]}, {block["color"]});', file=outfile)
    print(f'        this.player1 = new Item({player1["x"]}, {player1["y"]}, {player1["color"]}, {player1["id"]});', file=outfile)
    print(f'        this.player2 = new Item({player2["x"]}, {player2["y"]}, {player2["color"]}, {player2["id"]});', file=outfile)
    print(f'        this.goal = new Item({goal["x"]}, {goal["y"]});', file=outfile)
    print('    }\n}', file=outfile)

def main():
    if len(sys.argv) < 2 or len(sys.argv) > 3:
        print(f"Usage: python {os.path.basename(sys.argv[0])} infile [outfile]")
        exit(1)

    infile = sys.argv[1]
    data = load_json(infile)
    if len(sys.argv) == 3:
        outfile = sys.argv[2]
        with open(outfile, "x") as f:
            print_level_class(data, os.path.splitext(os.path.basename(outfile))[0], f)
    else:
        print_level_class(data)
    

if __name__ == "__main__":
    main()
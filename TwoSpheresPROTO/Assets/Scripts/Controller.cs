using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public WorldControler wC;
    public World world;

    List<KeyCode> komande;
    [SerializeField]
    KeyCode up, down, left, right;

    [SerializeField]
    TileState myState;

    [SerializeField]
    TileState myEnemy;
    public TileState MYenemy
    {
        get
        {
            return myEnemy;
        }
    }
    public Controller myEnemyGO;    //  public??

    public int x;
    public int y;
    public int oldX, oldY;  //  mozda ne treba

    #region - X Y - get seteri region
    public KeyCode Up
    {
        get { return up; }
        set { up = value; }
    }
    public KeyCode Down
    {
        get { return down; }
        set { down = value; }
    }
    public KeyCode Left
    {
        get { return left; }
        set { left = value; }
    }
    public KeyCode Right
    {
        get { return right; }
        set { right = value; }
    }

    public int X
    {
        get { return x; }
        set { x = value; }
    }
    public int Y
    {
        get { return y; }
        set { y = value; }
    }
    #endregion 
    [SerializeField]
    bool move;
    public int kills;
    public int deaths;

    public bool gotTail;
    public List<Tile> tail;   //  prepraviti sve?
    public TileState tailState;
    public int tailMax = 4; //  vrati na nulu !!!


    Text killsText;

    // dodaj score UI (kako to napraviti bez dodavanja UnityEngine.UI), //  nikak valjda

    public Color myColor;

    LightScript light;

    private void Awake()
    {
        tail = new List<Tile>();
        move = true;
        gotTail = true;
        kills = 0;
        deaths = 0;

    }

    private void Start()
    {
        world = wC.world;
        

        //  odredjuje koji si player
        if (this.gameObject.name == "PlayerOne")
        {
            x = 1; y = 1;
            myState = TileState.One;
            myEnemy = TileState.Two;
            myColor = Color.blue;
            tailState = TileState.TailOne;
            killsText = GameObject.Find("Text_KillsOne").GetComponent<Text>();
        }
        else if (this.gameObject.name == "PlayerTwo")
        {
            myColor = Color.red;
            x = wC.world.Tile.GetLength(0)  - 2; y = wC.world.Tile.GetLength(1) - 2;
            myState = TileState.Two;
            myEnemy = TileState.One;
            tailState = TileState.TailTwo;
            killsText = GameObject.Find("Text_KillsTwo").GetComponent<Text>();
        }

        for (int i = 0; i < world.Tile.GetLength(0); i++)
        {
            for (int j = 0; j < world.Tile.GetLength(1); j++)
            {
                if(world.Tile[i, j].State == myState)
                {
                    myColor = world.Tile[i, j].mat.color;
                }
            }
        }

        light = new LightScript(new GameObject(), this.gameObject.name, new Vector3(x, y, -1), new Light(), myColor);

        if (this.gameObject.name == "PlayerOne")
        {
            myEnemyGO = GameObject.Find("PlayerTwo").GetComponent<Controller>();
        }
        else if (this.gameObject.name == "PlayerTwo")
        {
            myEnemyGO = GameObject.Find("PlayerOne").GetComponent<Controller>();
        }

        NamjestiKomande();  //  print("komande " + komande[0] + komande[1]);

        //ObrniKomande();
        NormalneKomande();

        killsText.text = kills.ToString();
        world.Tile[x, y].State = myState;
    }


    private void Update()
    {
        if(Input.GetKeyDown(up) && move == true)
        {
            world.PlayerMoving(0, 1, myState, this);
        }
        if (Input.GetKeyDown(down) && move == true)
        {
            world.PlayerMoving(0, -1, myState, this);
        }
        if (Input.GetKeyDown(left) && move == true)
        {
            world.PlayerMoving(-1, 0, myState, this);
        }
        if (Input.GetKeyDown(right) && move == true)
        {
            world.PlayerMoving(1, 0, myState, this);
        }
    }


    //ORIGINAL
    public IEnumerator MoveTimer()  //  parametar null za svjetlost?
    {
        move = false;
        float timerMax = 0.5f;
        float timer = 0.0f;
        Vector3 targetScale = world.Tile[x, y].GO.transform.localScale;
        Vector3 zeroScale = new Vector3(0, 0, 0);

        while (timer < 0.5f)
        {
            world.Tile[x, y].GO.transform.localScale = Vector3.Lerp(zeroScale, targetScale, timer / timerMax);
            timer += Time.deltaTime;
            yield return null;
        }
        world.Tile[x, y].GO.transform.localScale = targetScale;
        move = true;
    }

    public void MoveLight(Vector3 _oldV3, Vector3 _newV3)
    {
        StartCoroutine(MoveLightIE(_oldV3, _newV3));
    }

    public IEnumerator MoveLightIE(Vector3 _oldV3, Vector3 _newV3)
    {

        print("Light coroutine starts");
        float timerMax = 0.5f;
        float timer = 0.0f;
        while(timer < 0.5f)
        {
            light.go.transform.position = Vector3.Lerp(_oldV3, _newV3, timer / timerMax);
            timer += Time.deltaTime;
            yield return null;
        }
        light.go.transform.position = new Vector3(x, y, -1);
    }

    




    public void NamjestiKomande()
    {
        if (this.gameObject.name == "PlayerOne")
        {
            komande = new List<KeyCode>() { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
        }
        else if (this.gameObject.name == "PlayerTwo")
        {
            komande = new List<KeyCode>() { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
        }
    }


    public void ObrniKomande()
    {
        List<KeyCode> kopijaKomandi = komande;

        int randomInt()
        {
            int integer = Random.Range(0, kopijaKomandi.Count);
            return integer;
        }

        int randy = randomInt();    //  randy kao random number
        up = kopijaKomandi[randy];
        kopijaKomandi.RemoveAt(randy);

        randy = randomInt();
        down= kopijaKomandi[randy];
        kopijaKomandi.RemoveAt(randy);

        randy = randomInt();
        left = kopijaKomandi[randy];
        kopijaKomandi.RemoveAt(randy);

        randy = randomInt();
        right = kopijaKomandi[0];
        kopijaKomandi.RemoveAt(0);
    }

    public void NormalneKomande()
    {
        up = komande[0];
        down = komande[1];
        left = komande[2];
        right = komande[3];
    }


    public void PozderanSam()
    {
        Vector3 lightVector = light.go.transform.position;

        deaths++;

        int podjelaX = (int)world.WorldX / 2;
        int podjelaY = (int)world.WorldY / 2;

        if(x <= podjelaX)
        {
            x = Random.Range(world.WorldX - 3, world.WorldX - 1);
        }
        else if ( x >= podjelaX)
        {
            x = Random.Range(1, 3);
        }

        if(y <= podjelaY)
        {
            y = Random.Range(world.WorldY - 3, world.WorldY - 1);
        }
        else if(y >= podjelaY)
        {
            y = Random.Range(1, 3);
        }

        world.Tile[x, y].State = myState;
        MoveLight(lightVector, new Vector3(x, y, -1));

        // TAIL

        //foreach(int[] item in tail)
        //{
        //    world.Tile[item[0], item[1]].State = TileState.Empty;
        //}
        foreach (Tile item in tail)
        {
            item.State = TileState.Empty;
        }
        tail.Clear();

        gotTail = true;
        tailMax++;
    }

    public void PozderaoSam()
    {
        kills++;
        killsText.text = kills.ToString();

        //foreach (int[] item in tail)
        //{
        //    world.Tile[item[0], item[1]].State = TileState.Empty;
        //}
        foreach (Tile item in tail)
        {
            //world.Tile[x, y].State = TileState.Empty;
            item.State = TileState.Empty;
        }
        tail.Clear();
        gotTail = false;
    }
}

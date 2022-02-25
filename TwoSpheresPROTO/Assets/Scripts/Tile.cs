using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// prebacio sam u "GLOBAL", dakle van klase i sada radi u switch caseu
public enum TileState { Empty, One, Two, PowerUp, Hole, Border, Fire, Ice, TailOne, TailTwo, SpeedUp};
                    //      0       1   2       3       4       5   6       7


public class Tile
{
    TileState state = TileState.Empty;
    public TileState State
    {
        get {
            return state;
        }
        set {
            state = value;
            TileSwitchCase();
        }
    }

    World world;
    GameColors gc = GameObject.FindObjectOfType<GameColors>();
    int x;  //  izgleda da ovo nigdje ne koristim
    int y;

    GameObject go;
    public GameObject GO
    {
        get { return go; }
        set { this.go = value; }
    }
    public Transform trans;
    public MeshRenderer rend;
    public Material mat;
    public Color color;

    public Tile(World _world, int _x, int _y, GameObject _go, MeshRenderer _rend, Material _mat, Vector3 _pos, Vector3 _scale)
    {
        this.world = _world;
        this.x = _x;
        this.y = _y;
        this.go = _go;
        this.go.transform.position = _pos;  //  sta ne bi tu trebalo bit ovaj njegov x i y? sredi
        this.go.transform.localScale = _scale;
        this.rend = _rend;
        this.mat = _mat;
        //this.mat.EnableKeyword("_EMISSION");      ¸// ovo ovdje palimo
        //mat.SetColor("_EmissionColor", color);    tu ne radi, mora biti u tile switch caseu ()
    }

    public void TileSwitchCase()
    {

        //Vector3 newV3 = new Vector3(1, 1, 1);
        switch (State)
        {
            case TileState.Empty:
                //mat.color = Color.green;
                //mat.color = Color.grey;
                mat.color = gc.empty;
                color = Color.green;
              //  mat.SetColor("_EmissionColor", color);
                GO.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                //Color color = Color.green;
                //newV3 = new Vector3(0.3f, 0.3f, 0.3f);    //  TEST, VRATI AKO TREBA
                // start coro parametri
                //StateIE(newV3);
                break;
            case TileState.One:
                //mat.color = Color.blue;
                mat.color = gc.plOne;
                color = gc.plOne;
              //  mat.SetColor("_EmissionColor", color);
                GO.transform.localScale = new Vector3(1, 1, 1);
        //        newV3 = new Vector3(1, 1, 1);

                //this.mat.EnableKeyword("_EMISSION");
                break;
            case TileState.Two:
                //mat.color = Color.red;
                mat.color = gc.plTwo;
                color = Color.red;
            //    mat.SetColor("_EmissionColor", color);
                GO.transform.localScale = new Vector3(1, 1, 1);
          //      newV3 = new Vector3(1, 1, 1);
                break;
            case TileState.PowerUp:
                break;
            case TileState.Hole:
                break;
            case TileState.Border:
                //mat.color = Color.black;
                mat.color = gc.border;
                //mat.SetColor("_EmissionColor", color);
                color = Color.black;
                GO.transform.localScale = new Vector3(0.80f, 0.80f, 0.80f);
                break;
            case TileState.Fire:
                break;
            case TileState.Ice:
                break;
            case TileState.TailOne:
                mat.color = gc.plOne;
                //mat.SetColor("_EmissionColor", color);
                GO.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                break;
            case TileState.TailTwo:
                mat.color = gc.plTwo;
                //mat.SetColor("_EmissionColor", color);
                GO.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                break;
            default:        // nije pomoglo u odklanjanja buga. odklanjanja, jel to rijec uopste?
                go.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                break;
        }

        //Debug.Log("jel radi ovo?");

    }

    //IEnumerator StateIE(Vector3 _new)
    //{
    //    Debug.Log("coro");
    //    Vector3 old = GO.transform.localScale;
    //    Vector3 newV3 = _new;
    //    float currentTime = 0;
    //    float maxTime = 1.0f;

    //    while(currentTime <= maxTime)
    //    {
    //        go.transform.localScale = Vector3.Lerp(old, newV3, currentTime / maxTime);
    //        currentTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    go.transform.localScale = _new;
    //    Debug.Log("coro gotov");
    //}
}
    //class OldTile
    //{
    //    public Color c;
    //    public Vector3 v;
    //    public OldTile(Color _c, Vector3 _v)
    //    {
    //        this.c = _c;
    //        this.v = _v;
    //    }
    //}

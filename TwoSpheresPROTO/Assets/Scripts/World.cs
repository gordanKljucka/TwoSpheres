using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    Tile[,] tiles;
    public Tile[,] Tile //  jel ovo uopce funkcionira?  DA
    {
        get { return tiles; }   
    }
    int width;
    public int WorldX
    {
        get { return width; }
    }
    int height;
    public int WorldY
    {
        get { return height; }
    }

    // new brija
    private int worldSize;
    


    public World(int _x = 15, int _y = 15)
    {
        this.width = _x;
        this.height = _y;
        tiles = new Tile[_x, _y];   // KAKO SAM GLUP!

        // stvaranje grida od gameObjectsa
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject goo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Vector3 pos = new Vector3(i, j, 0);
                Vector3 scale = new Vector3();  //  TileSwitchCase odredjuje scale
                MeshRenderer rend = goo.GetComponent<MeshRenderer>();
                Material mat = new Material(Shader.Find("Standard"));
                rend.material = mat;
                

                tiles[i, j] = new Tile(this, i, j, goo, rend, mat, pos, scale);
                tiles[i, j].State = TileState.Empty;
                if(i == 0 || i >= width - 1 || j <= 0 || j >= height - 1)
                {
                    tiles[i, j].State = TileState.Border;
                }
                //  //  //  //tiles[i, j] = TileSwitchCase(i, j);
            }
        }
    }



    public void PlayerMoving(int _x, int _y, TileState _state, Controller _controller)
    {
        int posX = _controller.x;
        int posY = _controller.y;
        int newX = _controller.x + _x;
        int newY = _controller.y + _y;
        Tile targetTile = tiles[newX, newY];    //  POLA PROBLEMA BI SE RJESILO da sam u "tail" mehanici koristio ovakvu varijablu
        Tile oldTile = tiles[posX, posY];              //  radi i ovako kako sam napis'o pa nije hitno prepravljati

        if (targetTile.State != TileState.Border)
        {

            // rep
            if(_controller.gotTail == true) //  rep     //  BUG on zadnji baca, 
            {
                _controller.tail.Insert(0, oldTile);
                // ako je zadnji index u repu, postavlja Empty iza sebe
                if(_controller.tail.Count >= _controller.tailMax)
                {
                    int i = _controller.tail.Count - 1; //  jel ovo ispravno? -1? DA
                    //int ix = _controller.tail[i][0];
                    //int iy = _controller.tail[i][1];
                    _controller.tail[i].State = TileState.Empty;
                    _controller.tail.RemoveAt(i);
                }
                tiles[posX, posY].State = _controller.tailState;    //  postavlja state na rep
                for (int i = 0; i < _controller.tail.Count - 1 ; i++)    //  MOGUCI BUG WARNING
                {
                    _controller.tail[i].State = _controller.tailState;
                }
            }

            else if(_controller.gotTail == false)
            {
                tiles[posX, posY].State = TileState.Empty;    //  empty state
            }

            if(targetTile.State == _controller.tailState)
            {
                targetTile.State = _controller.tailState;
            }

            // neprijatelj
            if (targetTile.State == _controller.MYenemy)
            {
                _controller.myEnemyGO.PozderanSam();
                _controller.PozderaoSam();
            }

            
            // pomicanje playera opcenito
            targetTile.State = _state;
            _controller.x = newX;
            _controller.y = newY;

            //_controller.MoveLight(oldTile.GO.transform.position, targetTile.GO.transform.position);
            _controller.MoveLight(new Vector3(posX, posY, -1), new Vector3(newX, newY, -1));
        }

        _controller.StartCoroutine("MoveTimer");
    }






    // napravi ovo !!!
    public void StvoriPowerup()
    {

    }







    //  WORK IN PROGRESS
    // OK, OVO ZA SADA RADI
    //public IEnumerator ScaleAnimation(int _x, int _y, Vector3 _newVec, Color _newColor)    //  mjenjaj scale, boju
    //{
    //    float timeElapsed = 0.0f;
    //    float maxTime = 0.5f;

    //    while(timeElapsed < maxTime)
    //    {
    //        timeElapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    Debug.Log("Gotova Coroutina!");
    //}

    //public Ienumerator 

    //public void Animation(int x, int y)
    //{
    //    GameObject go = tiles[x, y].GO;
    //    go.transform.localScale = Vector3.Lerp()
    //}
    //  OVO ODKOMENTIRAJ
    //public void NamjestiState()
    //{
    //    for (int i = 0; i < width; i++)
    //    {
    //        for (int j = 0; j < height; j++)
    //        {
    //            tiles[width, height].State = Tile.TileState.Empty; //  vrati na getter
    //        }
    //    }
    //}

}


//public Tile TileSwitchCase(int _x, int _y)
//{
//    Tile t = tiles[_x, _y];
//    switch (t.State)
//    {
//        case TileState.Empty:
//            t.mat.color = Color.green;
//            t.GO.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
//            break;
//        case TileState.One:
//            t.mat.color = Color.blue;
//            t.GO.transform.localScale = new Vector3(1, 1, 1);
//            break;
//        case TileState.Two:
//            t.mat.color = Color.red;
//            t.GO.transform.localScale = new Vector3(1, 1, 1);
//            break;
//        case TileState.PowerUp:
//            break;
//        case TileState.Hole:
//            break;
//        case TileState.Border:
//            t.mat.color = Color.black;
//            t.GO.transform.localScale = new Vector3(0.80f, 0.80f, 0.80f);
//            break;
//        case TileState.Fire:
//            break;
//        case TileState.Ice:
//            break;
//        case TileState.TailOne:
//            t.mat.color = Color.blue;
//            t.GO.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
//            break;
//        case TileState.TailTwo:
//            t.mat.color = Color.red;
//            t.GO.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
//            break;
//    }

//    return Tile[_x,_y];
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldControler : MonoBehaviour
{
    public World world;

    public Text scoreOne, scoreTwo;

    private void Awake()
    {
        world = new World();
    }
    //  Start
    void Start()
    {
        for (int x = 0; x < world.WorldX; x++)
        {
            for (int y = 0; y < world.WorldY; y++)
            {
                GameObject go = world.Tile[x, y].GO;
                go.name = "Tile " + x + "_" + y;
                go.transform.SetParent(this.gameObject.transform);
                Destroy(go.GetComponent<SphereCollider>());
            }
        }
        for (int x = 0; x < world.WorldX; x++)
        {
            for (int y = 0; y < world.WorldY; y++)
            {
                world.Tile[x, y].TileSwitchCase();
            }
        }

        CreatePlayers();

        Camera.main.transform.position = new Vector3(world.WorldX / 2, world.WorldY / 2, world.WorldY * -1 * 1.5f);    // z * 2 cak i nije lose

        //Camera.main.GetComponent<Camera>().fieldOfView = 2;
        //Camera.main.GetComponent<Camera>().orthographic = true;
        //Camera.main.GetComponent<Camera>().orthographicSize = 9.9f;
        //  sad namjesti if camera perspective ili octographic
        //  i provjeri jel z mora biti + ili -




    }
    // </Start>

    //  stvaramo playera
    void CreatePlayers()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject go = new GameObject();

            if(i == 0)
            {
                go.name = "PlayerOne";
            }
            else if (i == 1)
            {
                go.name = "PlayerTwo";
            }
            else
            {
                Debug.LogError("Player three doesn't exist");
            }
            go.AddComponent<Controller>();
            go.GetComponent<Controller>().wC = this;
        }
    }

}

        #region STVARANJE CANVASA REGION
        ////  CANVAS - dodavanje Cavas-a na Scenu


        //    //  bila bi brija to napraviti nekakvom klasom
        //    // return Canvas - ovo je mozda mali hint
        //GameObject canvas = new GameObject();
        //canvas.name = "myNewCanvas";
        //canvas.AddComponent<Canvas>();
        //canvas.AddComponent<CanvasScaler>();
        //canvas.AddComponent<GraphicRaycaster>();
        //Canvas can = canvas.GetComponent<Canvas>();
        //can.worldCamera = Camera.main;


        ////  VRATI

        ///*
        // * for number of players
        // * textGO.name = "Score " + i, jbg kako dodati one? u varijablu dodati if uvjet pa return string?
        // * */
        //GameObject textGO = new GameObject(); //  pretpostavljam da bi ovo trebao prebaciti u global variablu, member vatijablu ili sta vac
        //textGO.name = "ScoreOne";
        //textGO.transform.SetParent(canvas.transform, false);    //  dodaj bool kao drugi property

        //Text myText = textGO.AddComponent<Text>();
        //myText.text = "My text one";
        ////textGO.transform.position.x
        ////textGO.


        ////-sto se tice dodavanja texta skriptom
        //// - https://stackoverflow.com/questions/347342

        #endregion

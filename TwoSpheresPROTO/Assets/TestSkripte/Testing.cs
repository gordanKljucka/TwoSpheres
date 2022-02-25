using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    World world;
    Controller ctr;
    TestingNMB nmb;
    public GameObject objekt;

    public GameObject[] go = new GameObject[2];
    void Start()
    {
        //world = new World();
        //go[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        nmb = new TestingNMB();
        //nmb.Stvori();
        //objekt = nmb.go[1, 2];
        //objekt = world.tileGO[2, 4];

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
          //  objekt.transform.position = new Vector3(4, 4, 4);
        }
        //if(Input.GetKeyDown(KeyCode.T))
        //{
        //    print("test");
        //    ctr = FindObjectOfType<Controller>();
        //    world.ObrniKomande(ctr);
        //}
    }


}

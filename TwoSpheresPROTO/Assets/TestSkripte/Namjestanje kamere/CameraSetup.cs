using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    private void Start()
    {
        World width= FindObjectOfType<WorldControler>().world/*.WorldX*/;
       // float height = FindObjectOfType<WorldControler>().world.WorldY;
        //transform.position = new Vector3(width / 2, height / 2, width * 2 * -1);
    }
}

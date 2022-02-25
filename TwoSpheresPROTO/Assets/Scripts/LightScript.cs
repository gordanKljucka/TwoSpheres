using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript
{
    public GameObject go;
    Light light;
    Color color;

    public LightScript(GameObject _go, string _name, Vector3 _vec3, Light _light, Color _color)
    {
        this.go = _go;
        this.go.name = _name + " light";
        this.go.transform.position = _vec3;
        this.light = _light;
        // this.light.color = _color;
        this.color = _color;
        SetUp();
    }

    public void SetUp()
    {
        light = go.AddComponent<Light>();
        light.type = LightType.Point;
        light.color = color;
        light.range = 3;
        light.intensity = 60;
    }

    //  vrati i nastavi
    public void MoveLight(Vector3 _vec3)
    {
        go.transform.position = _vec3;
    }
}

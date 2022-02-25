using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BojaKoro : MonoBehaviour
{
    Renderer rend;
    public Material mat;
    Color col = Color.black;


    Vector3 vec = new Vector3(2, 6, 2); 

    private void Start()
    {
        //rend = GetComponent<Renderer>();
        //mat = rend.material;

        //mat.color = col;
        ////mat.color = Color.red;
        ////float col = mat.color.r;
        ////mat.SetColor(rend, 150)

        transform.localScale = new Vector3(0.5f, 1, 0.5f);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Scale());
        }
    }

    IEnumerator Scale()
    {
        Vector3 original = transform.localScale;
        Vector3 newScale = new Vector3(2, 6, 2);



        float timeCurrent = 0;
        float timeMax = 1;

        while(timeCurrent <= timeMax)
        {
            transform.localScale = Vector3.Lerp(original, newScale, timeCurrent / timeMax);
            timeCurrent += Time.deltaTime;
            yield return null;
        }

        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;

        if (transform.localScale.x < newScale.x)
        {
            print("dakle nije dobro");
            x = Mathf.FloorToInt(x);
            print("" + Mathf.FloorToInt(transform.localScale.x));
            print("x, " + x);
            //  dakle uzima x, izracuna/floora ali ne namjesta pod objektov scale jbg
        }
        print(transform.localScale.x + "");

        transform.localScale = newScale;
    }

    /*
     * mjenjanje boje
     * 
     * https://www.youtube.com/watch?v=5OxmXaeAx2g
     * https://answers.unity.com/questions/209573/how-to-change-material-color-of-an-object.html
     * https://answers.unity.com/questions/982576/change-material-color-of-an-object-c.html
     * https://docs.unity3d.com/ScriptReference/Material-color.html
     * https://docs.unity3d.com/ScriptReference/Material.SetColor.html
     * */
}

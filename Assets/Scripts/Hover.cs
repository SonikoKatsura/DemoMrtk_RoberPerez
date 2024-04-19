using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    //Attributes
    public float hoverDistance;
    public float hoverSpeed;

    void Update()
    {
        /*
         * Para que suba y baje el objeto -> Seno del tiempo desde que empez� a ejecutar
         * Sin(devuleve valores entre -1 y 1)
        */
        //Lo m�s correcto es hacerlo con la Pos, sino puede haber errores de reondeo
        float localHeight = (float)Mathf.Sin(Time.time * hoverSpeed) * hoverDistance;
        //transform.localPosition.y = localHeight;

        Vector3 newPosition = transform.position;
        newPosition.y = localHeight; 
        transform.position = newPosition;
    }
}

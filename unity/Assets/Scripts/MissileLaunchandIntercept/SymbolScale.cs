using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolScale : MonoBehaviour
{
    public GameObject ThalesScale;
    public GameObject[] Symbols;
    private float scale;
    private float symbolScale;
    private float tempScale;

    // Start is called before the first frame update
    void Start()
    {
        Symbols = GameObject.FindGameObjectsWithTag("Symbol");
        symbolScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(scale);
        scale = ThalesScale.transform.localScale.x;       
        foreach (GameObject symbol in Symbols)
        {
            
            
            if(scale>300)
            {
                symbol.SetActive(false);
                symbol.transform.localScale = new Vector3(100f, 100f, 100f);
            }            
            else if (scale < 300)
            {
                symbol.SetActive(true);                
            }            
            if (scale < 150)
            {
                symbol.transform.localScale = new Vector3(200, 200, 200);
            }           
            if (scale==100)
            {
                symbol.transform.localScale = new Vector3(500, 500, 500);
            }
        }
    }
            
}

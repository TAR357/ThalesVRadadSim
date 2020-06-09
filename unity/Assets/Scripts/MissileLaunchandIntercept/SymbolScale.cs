using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolScale : MonoBehaviour
{
    public GameObject ThalesScale;
    public GameObject[] Symbols;
    public GameObject[] MSymbols;

    private Vector3 defaultScale = new Vector3(100f, 100f, 100f);
    private Vector3 baseScale = new Vector3(300f, 300f, 300f);
    private float scale;
    private float symbolScale;
    private float tempScale;

    // Start is called before the first frame update
    void Start()
    {
        tempScale = 300f;
        Symbols = GameObject.FindGameObjectsWithTag("Symbol");
        MSymbols = GameObject.FindGameObjectsWithTag("MSymbol");
        symbolScale = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        scale = ThalesScale.transform.localScale.x;        
        tempScale = scale;
        //Debug.Log(scale);
        foreach (GameObject symbol in Symbols)
        {
            if (scale > 300)
            {
                symbol.SetActive(false);
                scaledSymbol();
            }
            else if(scale<=300)
            {
                symbol.SetActive(true);
                scaledSymbol();
            }
        }
        
        
            
    }

    /*foreach (GameObject symbol in Symbols)
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
    }*/
    public void scaledSymbol()
    {
        foreach (GameObject symbol in Symbols)
        {
            symbol.transform.localScale = defaultScale + (baseScale - ThalesScale.transform.localScale)*2;
        }
    }
}
            
    


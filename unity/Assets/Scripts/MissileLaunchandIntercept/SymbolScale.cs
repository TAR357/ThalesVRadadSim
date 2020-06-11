using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolScale : MonoBehaviour
{
    public GameObject ThalesScale;
    public GameObject[] Symbols;
    public GameObject[] MSymbols;

    private Vector3 defaultScale = new Vector3(100f, 100f, 100f);
    private Vector3 MSymbolScale = new Vector3(200f, 200f, 200f);
    private Vector3 baseScale = new Vector3(400f, 400f, 400f);
    private float scale;
    private float symbolScale;
    private float tempScale;

    // Start is called before the first frame update
    void Start()
    {        
        Symbols = GameObject.FindGameObjectsWithTag("Symbol");
        MSymbols = GameObject.FindGameObjectsWithTag("MSymbol");        
    }

    // Update is called once per frame
    void Update()
    {
        scale = ThalesScale.transform.localScale.x;        
        tempScale = scale;
        //Debug.Log(scale);
        foreach (GameObject symbol in Symbols)
        {
            if (scale > 400)
            {
                symbol.SetActive(false);
                scaledSymbol();
            }
            else if(scale<=400)
            {
                symbol.SetActive(true);
                scaledSymbol();
            }
        }
        
        
            
    }
        
    public void scaledSymbol()
    {
        foreach (GameObject symbol in Symbols)
        {
            symbol.transform.localScale = defaultScale + (baseScale - ThalesScale.transform.localScale)*2;
        }
    }

    public void scaledMSymbol()
    {
        foreach (GameObject msymbol in MSymbols)
        {
            msymbol.transform.localScale = MSymbolScale + (baseScale - ThalesScale.transform.localScale) * 2;
        }
    }
}
            
    


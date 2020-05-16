using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countCoint : MonoBehaviour
{
    private Text txt;

    void Update()
    {
        txt = GetComponent<Text>();
        txt.text = PlayerPrefs.GetInt("coin").ToString(); 
         
    }
}

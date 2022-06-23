using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogManager : MonoBehaviour
{
    
    public Text dialog;

    float setTime;
    int i;
    List<Dictionary<string, object>> data;

    void ReadDialog()
    {
        data = CSVReader.Read("Dialog");
    }

    void Start()
    {
        setTime = 0;
        //i = 87;
        i = 101;
        ReadDialog();
    }

    void Update()
    {
        //dialog.text = data[50]["Korean"].ToString();
        
        setTime += Time.deltaTime;
        if (setTime > 3)
        {
            i += 1;
            dialog.text = (data[i]["Korean"].ToString());
            setTime = 0;
            if(i == 103)
            {
                ObjectManager.instance.isWatchout = true;
            }
        }
    }
}
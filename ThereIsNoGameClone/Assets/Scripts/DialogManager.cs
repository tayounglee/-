using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogManager : MonoBehaviour
{
    public Text dialog;

    public static float setTime;
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
        setTime += Time.deltaTime;
        if (setTime > 3)
        {
            if (BrickoutController.isBrickout && BrickoutController.isMyTitle)
            {
                i = 114;
                setTime = 4;
                BrickoutController.isMyTitle = false;
            }

            i += 1;
            dialog.text = (data[i]["Korean"].ToString());
            setTime = 0;
            switch (i)
            {
                case 103:
                    ObjectManager.instance.isWatchout = true;
                    break;
                case 108:
                    setTime = -20;
                    break;
                case 113:
                    i = 108;
                    setTime = -20;
                    break;
                case 129:
                    setTime = -9999999999999999;
                    break;
            }
            
            
        }
    }
}
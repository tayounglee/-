using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    float setTime;
    int i;
    List<Dictionary<string, object>> data;

    private void ReadDialog()
    {
        //대사집 불러오기
        data = CSVReader.Read("Dialog");
        //필요한 만큼!
        /*
        for (i = 0; i < data.Count; i++)
        {
            Debug.Log(data[i]["Korean"].ToString());
        }
        */
    }

    void Start()
    {
        setTime = 0;
        i = 0;
        ReadDialog();
    }

    void Update()
    {
        for(i = 0; i < data.Count; i++)
        {
            Debug.Log(data[i]["Korean"].ToString());
        }
        
        /*
        setTime += Time.deltaTime;
        if (setTime > 3)
        {
            
            setTime = 0;
        }
        */
    }
}
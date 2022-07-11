using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    public int life;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        life = 0;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(count == 13)
        {

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        life -= 1;

        if(life == 0)
        {
            Destroy(gameObject);
            if(gameObject.name == "TitleCubeLetter_E3")
            {
                DialogManager.setTime = 4;
            }
            count += 1;
        }
    }
}
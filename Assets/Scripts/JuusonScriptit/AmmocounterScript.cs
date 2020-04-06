using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmocounterScript : MonoBehaviour
{
        public GameObject[] pistolBullets;
    //    public gameobject player;
    //    private int bulletsleft;

    //    void update()
    //    {



    //        if (input.getbutton("fire1"))
    //        {
    //            changecolor();
    //<<<<<<< head

    //=======
    //>>>>>>> juusonbranch2
    //        }
    //    }

    //    void start()
    //    {



    //    }
    //    public void getpistolbullets(int ammocapacity)
    //    {
    //<<<<<<< head
    //        //gameobject pistolbullets[ammocapacity];
    //=======
    //>>>>>>> juusonbranch2
    //    }


    //    public void changecolor()
    //    {
    //<<<<<<< head
    //        //bool emptymagazine = player.getcomponent<weapons>().emptymagazine;
    //        int bulletsleft = player.getcomponent<weapons>().ammoleft;

    //        //if (emptymagazine)
    //        //{
    //        //    returncolor();
    //        //}

    //        color used = new color32(75, 75, 75, 255);
    //        pistolbullets[bulletsleft].getcomponent<image>().color = used;
    //=======
    //        int bulletsleft = player.getcomponent<weapons>().ammoleft;

    //        color used = new color32(75, 75, 75, 255);
    //        pistolbullets[bulletsleft].getcomponent<image>().color = used;
    //>>>>>>> juusonbranch2
    //    }

    public void ReturnColor(int ammocapacity)
    {
        for (int i = 0; i < ammocapacity; i++)
        {
            Color used = new Color32(255, 255, 255, 255);

            pistolBullets[i].GetComponent<Image>().color = used;
        }
    }
}

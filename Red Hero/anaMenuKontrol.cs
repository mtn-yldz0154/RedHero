﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class anaMenuKontrol : MonoBehaviour
{

    GameObject leveller;
    GameObject kilitler;
    void Start()
    {

        // PlayerPrefs.DeleteAll();

        leveller = GameObject.Find("leveller");
        kilitler = GameObject.Find("kilitler");


        for (int i = 0; i < leveller.transform.childCount; i++)
        {
            leveller.transform.GetChild(i).gameObject.SetActive(false);


        }
        for (int i = 0; i < kilitler.transform.childCount; i++)
        {
            kilitler.transform.GetChild(i).gameObject.SetActive(false);


        }


       

        for (int i=0;i<PlayerPrefs.GetInt("kacincilevel");i++)
        {
            
            leveller.transform.GetChild(i).GetComponent<Button>().interactable=true;
        }

    }

    public void buttonSec(int gelenButon)
    {

        if (gelenButon==1)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("kacincilevel"));
        }

        else if(gelenButon == 2)
        {
            for (int i = 0; i < kilitler.transform.childCount; i++)
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(true);


            }
            for (int i = 0; i < leveller.transform.childCount; i++)
            {
                leveller.transform.GetChild(i).gameObject.SetActive(true);


            }
            for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
       else if(gelenButon == 3)
        {
            Application.Quit();
        }

    }

    public void levellerButon (int gelenLevel)
    {
        SceneManager.LoadScene(gelenLevel);
    }

}

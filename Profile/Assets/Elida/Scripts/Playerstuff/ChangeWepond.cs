using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWepond : MonoBehaviour
{
    public GameObject[] weponds;
    public int currentwepondIndex = 0;


    void Start()
    {
        SwitchWeapond(currentwepondIndex);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentwepondIndex++;

            if(currentwepondIndex >= weponds.Length)
            {
                currentwepondIndex = 0;
            }

            SwitchWeapond(currentwepondIndex);
        }
    }

    void SwitchWeapond(int Indexs)
    {
        foreach(GameObject weapond in weponds)
        {
            weapond.SetActive(false);
        }

        if (weponds.Length < 0)
        {
            weponds[Indexs].SetActive(true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobyDataManager : Singleton<LobyDataManager>
{

    public GameObject EquipParent;
    public ChildReference1[] reference1;
    public textType[] SetText;
    //[System.NonSerialized]
    public ChildReference1 EquipInfo;

    public void ReferenceLoby()
    {
        reference1 = EquipParent.GetComponentsInChildren<ChildReference1>();

        for (int i = 0; i < reference1.Length; i++)
        {
            reference1[i].name = i.ToString();
        }

        SetText = EquipParent.GetComponentsInChildren<textType>();

        
    }


    private void Awake()
    {
        ReferenceLoby();
    }


    

    public void sceneChange()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }





}

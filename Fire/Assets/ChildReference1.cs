using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChildReference1 : MonoBehaviour
{
    public int ArrNumber;

    public Text Name = null;
    public Text ability = null;
    public Text ability2 = null;
    public Button GoldButton = null;
    public Text GoldButtonText = null;

   
    private panelOnoff panel;
    private CsvEquipPanel csvEquip;


    private void Awake()
    {
        panel = FindObjectOfType<panelOnoff>();
        csvEquip = FindObjectOfType<CsvEquipPanel>();
    }

    private void Start()
    {
        GoldButton.onClick.AddListener(DataEquip);
        ArrNumber = int.Parse(transform.name);
        if (panel.panelName == PanelName.Equip)
        {
            //로드할게 있다면 여기~
        }
    }

    
    public void DataEquip()
    {
        csvEquip.ChangeModel.sharedMesh = csvEquip.meshRenderer[ArrNumber].sharedMesh;
        //스킨장착 후 데이터 변동을 해야하나?....
    }
}
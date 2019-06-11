using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenDataKM
{
    public void FirstSetting()
    {
    }

    public bool IsHaveItem(InvenIndex invenIndex, int num)
    {
        InvenSaveData invenSaveData = LoadData(invenIndex);

        if (invenSaveData == null)
            return false;

        if (invenSaveData.num < num)
            return false;

        return true;
    }

    public InvenSaveData LoadData(InvenIndex invenIndex)
    {
        InvenSaveData invenSaveData = new InvenSaveData();
        invenSaveData.num = 100000;
        Debug.Log(invenSaveData.ivenIndex + "  확인");
        return invenSaveData;
    }
}
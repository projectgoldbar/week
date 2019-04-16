using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameItemContainer : MonoBehaviour
{
    public Queue<Item> veiledItemlist = new Queue<Item>();
    private Queue<Item> unVeiledItemlist = new Queue<Item>();

    /// <summary>
    /// 펼치기 안한 모든 아이템을 펼치고 unVeiledItemlist에 집어 넣는다.
    /// </summary>
    public void OpenVeilAll()
    {
        for (int i = 0; i < veiledItemlist.Count; i++)
        {
            var unVeiledItem = veiledItemlist.Dequeue();
            unVeiledItem.Open();
            unVeiledItemlist.Enqueue(unVeiledItem);
        }
    }

    public void OpenVeilAll(List<Item> something)
    {
        for (int i = 0; i < veiledItemlist.Count; i++)
        {
            var unVeiledItem = veiledItemlist.Dequeue();
            unVeiledItem.Open();
            something.Add(unVeiledItem);
        }
    }

    /// <summary>
    /// 펼치기 안한 아이템중 첫번째 아이템을 펼치기 한다.
    /// </summary>
    /// <returns></returns>
    public Item OpenVeilOnce()
    {
        var unVeiledItem = veiledItemlist.Dequeue();
        unVeiledItem.Open();
        return unVeiledItem;
    }
}
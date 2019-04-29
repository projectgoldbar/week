using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Boxing : MonoBehaviour
{
    public ItemList ItemList;

    public ItemType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("a");
            if (type == ItemType.KeyItem)
            {
                GameLevelManager.instance.stage++;
                GameLevelManager.instance.StageUp();
            }

            var b = ItemList.equipItemList[Random.Range(0, ItemList.Instance.equipItemList.Count)];
            var a = new Item_Equip(b.type, b.name, b.description, Random.Range(b.minStat, b.maxStat));
            GameLevelManager.instance.itemContainer.veiledItemlist.Enqueue(a);
            Destroy(this.gameObject);
        }
    }
}
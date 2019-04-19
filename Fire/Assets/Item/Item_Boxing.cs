using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Boxing : Item
{
    public ItemList ItemList;

    public ItemType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (type == ItemType.KeyItem)
            {
                GameLevelManager.instance.stage++;
                GameLevelManager.instance.StageUp();
            }
            var a = Instantiate(ItemList.equipItemList[Random.Range(0, ItemList.equipItemList.Count)]);
            GameLevelManager.instance.itemContainer.veiledItemlist.Enqueue(a);
            Destroy(this.gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public InGameItemContainer container;

    public int hp;

    //public string name;
    public Sprite itemSprite;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            container.veiledItemlist.Enqueue(this);
            TargetPointer a = GameObject.Find("UI").GetComponent<TargetPointer>();
            a.pointerRectTransform.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    public void Open()
    {
        hp = Random.Range(0, 10);
    }
}
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
            MarkerSystem.instance.targetChange(new Vector3(167f, 1.5f, -2f));
            GameManager.instance.goal = new Vector3(167f, 1.5f, -2f);
            Destroy(this.gameObject);
        }
    }

    public void Open()
    {
        hp = Random.Range(0, 10);
    }
}
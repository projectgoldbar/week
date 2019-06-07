using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Fance : Interaction
{
    public List<Transform> zombies;

    public override void Use()
    {
        StartCoroutine(Zmove());
        gameObject.layer = LayerMask.NameToLayer("UsedInteraction");
    }

    private IEnumerator Zmove()
    {
        for (int i = 0; i < 20; i++)
        {
            transform.Translate(0, 0, 1f);
            yield return null;
        }
        aa();
        yield break;
    }

    private void aa()
    {
        List<Transform> zombie = new List<Transform>();
        for (int i = 0, j = 0; i < zombies.Count; i++)
        {
            if (Vector3.Distance(transform.position, zombies[i].position) < 20f)
            {
                j++;
                if (j > zombieCount)
                {
                    break;
                }
                zombie.Add(zombies[i]);
            }
        }
        for (int i = 0; i < zombie.Count; i++)
        {
            zombie[i].GetComponent<ZombieState.ZombiesComponent>().player = pivot.transform;
        }

        StartCoroutine(Test(zombie));
    }

    private IEnumerator Test(List<Transform> zombie)
    {
        for (int i = 0; i < zombie.Count;)
        {
            if (Vector3.Distance(transform.position, zombie[i].position) < 2f)
            {
                Debug.Log(i);
                i++;
            }
            yield return null;
        }
        for (int i = 0; i < zombie.Count; i++)
        {
            var a = zombie[i].GetComponent<ZombieState.ZombiesComponent>().player;
            a = GameObject.FindObjectOfType<Player>().gameObject.transform;
        }
        Destroy(this.gameObject);
        yield return null;
    }
}
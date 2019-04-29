using UnityEngine;

public class Interaction_Health : Interaction
{
    public override void Somthing()
    {
        GameObject.FindObjectOfType<Player>().Hp += 2;
        Destroy(gameObject);
    }
}
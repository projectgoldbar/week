using System;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu]
public class Item_Equip : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite itemImage;

    public int hp;
    public Action specialmove;
}
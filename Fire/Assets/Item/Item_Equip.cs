using System;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Item_Equip : Item
{
    public EquipType type;
    public string name;
    public string description;
    public int hp;
    public Sprite itemImage;

    public Item_Equip(EquipType _type, string _name, string _description, int _hp)
    {
        type = _type;
        name = _name;
        description = _description;
        hp = _hp;
        itemImage = Resources.Load("ItemSprite/" + name.ToString(), typeof(Sprite)) as Sprite;
    }
}
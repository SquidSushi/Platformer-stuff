using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "ScriptableObjects/Weapon", order = 7)]
public class Weapon : ScriptableObject
{
    public int baseDamage;
    public int baseDurability;
    public int weight;
    public int value;
    public int level;
    public int rarity;
    public int damageType;
    public int weaponType;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

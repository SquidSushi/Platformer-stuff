using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MonsterBreed", menuName = "ScriptableObjects/MonsterBreed", order = 2)]
public class Monsterbreed : ScriptableObject
{
    public string monsterName = "defaultName";
    public Image FrontSprite;
    public Image BackSprite;
    [Tooltip("Primary Element should never be \"none\"")]
    public Element PrimaryElement;
    [Tooltip("Secondary Element can be \"none\"; there is no priority between those two")]
    public Element SecondaryElement;
    public float BaseHealth = 100f;
    public float BasePhysicalAttack = 100f;
    public float BaseMagicalAttack = 100f;
    public float BasePhysicalDefense = 100f;
    public float BaseMagicalDefense = 100f;
    public float BaseSpeed = 100f;
    public List<LevelUpMove> LevelUpMoves = new List<LevelUpMove>();
}

public enum Element
{
    None = 0,
    Normal,
    Fighting,
    Flying,
    Poison,
    Ground,
    Rock,
    Bug,
    Ghost,
    Steel,
    Fire,
    Water,
    Grass,
    Electric,
    Psychic,
    Ice,
    Dragon,
    Dark,
    Fairy
}
[System.Serializable]
public struct Move
{
    public Element element;
    public string name;
    public float power;
    public float accuracy;
}
[System.Serializable]
public struct LevelUpMove
{
   
    public int level;
    public Move move;
}
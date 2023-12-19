using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BeamData", menuName = "ScriptableObjects/BeamData", order = 2)]
public class BeamData : ScriptableObject
{
    [Tooltip("Image is normalized to face right")]
    public Image projectileSprite;
    [Tooltip("Speed in units per second")]
    public float projectileSpeed = 10f;
    [Tooltip("Damage dealt per projectile to enemies hit. \nIf the projectile is piercing it can't hit an enemy more than once every quarter second")]
    public float projectileDamage = 10f;
    [Tooltip("How many hits can be dealt by a single projectile")]
    public int maxHits = 1;
    public int projectileCount = 1;
    [Tooltip("How far the projectile flies before it is destroyed")]
    public float maxRange = 10f;
    public float InitialLifetime() {return maxRange / projectileSpeed;}
    public bool piercesWalls = false;

    [Tooltip("The angle in degrees inbetween projectiles if there are multiple")]
    public float perProjectileSpread = 0f;
    public float RotationOffsetOfProjectile(int i)
    {
        return 0; //TODO
    }
    [Tooltip("The amount of units projectiles are offset vertically if there are multiple")]
    public float perProjectileOffset = 0f;
    public float PositionOffsetOfProjectile(int i)
    {
        return 0; //TODO
    }
    public GameObject getProjectileObject() //returns a GameObject with the projectile script attached, needs to be instantiated
        //the returned object does not account for the projectile count, that needs to be done in shoot script.
    {
        return null; //TODO
    }

}

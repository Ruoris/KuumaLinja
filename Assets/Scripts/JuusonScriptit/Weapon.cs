using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New weapon", menuName = "Weapon/Weapon")]
public class Weapon : ScriptableObject
{
    public new string name;
    public Sprite worldModel;
    public Sprite UIModel;
    public GameObject model;

    public float fireRate;
    public int ammoCapacity;
    public float weight;
    public float reach;
    public GameObject ammoModel;
    //public int cost;
}
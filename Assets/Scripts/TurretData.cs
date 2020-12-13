using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData  {
    public GameObject walker;
    public int cost;
    public GameObject Cavalry;
    public int costUpgraded;
    public TurretType type;
}
public enum TurretType
{
    Mage,
    Archer,

}
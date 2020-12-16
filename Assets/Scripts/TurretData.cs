using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData  {
    public GameObject L1;
    public int cost;
    public GameObject L2;
    public GameObject L3;
    public int costUpgraded;
    public TurretType type;
}
public enum TurretType
{
    primary,
    middiem,
    highest,

}
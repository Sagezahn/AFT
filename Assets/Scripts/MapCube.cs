using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
    [HideInInspector]
    public GameObject turretGo;//保存当前cube身上的炮台
    [HideInInspector]
    public TurretData turretData;
    [HideInInspector]
    public bool isUpgraded = false;

    public GameObject buildEffect;


    void Start()
    {
    }

    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgraded = false;
        turretGo = GameObject.Instantiate(turretData.L1, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    public void UpgradeTurret()
    {
        if(isUpgraded==true)return;
        string turretLevelName = turretGo.name;
        Destroy(turretGo);
        if (turretLevelName.Contains("L1")) {
            turretGo = GameObject.Instantiate(turretData.L2, transform.position, Quaternion.identity);
        } else if (turretLevelName.Contains("L2")) {
            turretGo = GameObject.Instantiate(turretData.L3, transform.position, Quaternion.identity);
        } else {
            isUpgraded = true;
        }
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
    
    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgraded = false;
        turretGo = null;
        turretData=null;
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    void OnMouseEnter()
    {

        if (turretGo == null && EventSystem.current.IsPointerOverGameObject()==false)
        {
        }
    }
    void OnMouseExit()
    {
    }
}

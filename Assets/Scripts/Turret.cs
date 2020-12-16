using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private List<GameObject> enemys = new List<GameObject>();

    public float attackRateTime = 1; //多少秒攻击一次
    private float timer = 0;

    public GameObject bulletPrefab;//子弹
    public Transform firePosition;
    public Transform head;

    public bool useLaser = false;

    public float damageRate = 70;

    public LineRenderer laserRenderer;

    public GameObject laserEffect;

    void Start()
    {
        laserEffect.SetActive(false);
        timer = attackRateTime;
        string currentTurretName = gameObject.name;
        damageRate = damageRate * currentTurret(currentTurretName);
        attackRateTime = attackRateTime * currentTurret(currentTurretName);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("Trigger Exit");
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        if (useLaser == false)
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
        else if(enemys.Count>0)
        {
            if (laserRenderer.enabled == false)
                laserRenderer.enabled = true;
            
            Debug.Log("true");
            laserEffect.SetActive(true);
            if (enemys[0] == null)
            {
                UpdateEnemys();
            }
            if (enemys.Count > 0)
            {
                laserRenderer.SetPositions(new Vector3[]{firePosition.position, enemys[0].transform.position});
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate *Time.deltaTime );
                laserEffect.transform.position = enemys[0].transform.position;
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        }
        else
        {
            Debug.Log("false");
            laserEffect.SetActive(false);
            laserRenderer.enabled = false;
        }
    }

    // different level have different speed
    private float currentLevel(string name) {
        if (name.Contains("L1")) {
            return 1f;
        } else if (name.Contains("L2")) {
            return 0.7f;
        } else if (name.Contains("L3")) {
            return 0.5f;
        }
        return 1f;
    }

    // different turret have different damage
    private float currentTurret(string name) {

        if (name.Contains("Primary")) {
            return 1f;
        } else if (name.Contains("Middiem")) {
            return 1.3f;
        } else if (name.Contains("Heighest")) {
            return 1.5f;
        }
        return 1f;
    }

    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }

    void UpdateEnemys()
    {
        //enemys.RemoveAll(null);
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }

        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i]-i);
        }
    }
}

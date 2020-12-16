using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    private NavMeshAgent agent ;
    public float speed = 3;
    public float hp = 150;
    private float totalHp;
    public GameObject explosionEffect;
    private Slider hpSlider;
    private GameObject Destination;  
    private Animator animator;


	// Use this for initialization
	void Start () {
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
        agent = GetComponent<NavMeshAgent>();
        Destination =  GameObject.FindGameObjectWithTag("Destination");
	}
	
	// Update is called once per frame
	void Update () {
        animator = gameObject.GetComponent<Animator>();
        Move();
	}


    void Move()
    {
        agent.speed = speed;
        agent.SetDestination(Destination.transform.position);
        animator.SetBool("Walking", true);
        if(transform.position == Destination.transform.transform.position ){
            ReachDestination();
        }
    }
    //达到终点
    void ReachDestination()
    {
        GameManager.Instance.Failed();
        GameObject.Destroy(this.gameObject);
    }


    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    public void TakeDamage(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.Play("Die");
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }

}

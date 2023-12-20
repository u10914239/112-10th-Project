using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    public GameObject attackPoint;
    public Animator anim;
    public float radius;
    public LayerMask enemies;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            anim.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        
        Collider[] enemy = Physics.OverlapSphere(attackPoint.transform.position, radius, enemies);
        foreach (Collider enemyGameObject in enemy)
        {
            enemyGameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}

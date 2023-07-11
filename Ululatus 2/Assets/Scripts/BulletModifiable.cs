using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModifiable : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

    //Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    //Damage - only if explosive
    public int explosionDamage;
    public float explosionRange;

    //Lifetime of bullet
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        //When to explode:
        if (collisions > maxCollisions)
        {
            Explode();
        }

        //Count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        //Instantiate explosion
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);

        for (int i = 0; i < enemies.Length; i ++)
        {
            //Get component of enemy and call Take Damage

            //enemies[i].GetComponent<EnemyDamage>().TakeDamage(explosionDamage);
            enemies[i].GetComponent<EnemyAi>().health = 0;
        }

        //A little delay is added to make sure everything works
        Invoke("Delay", 0.001f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {

        // Dont count collisions with other bullets
        if (collision.collider.CompareTag("Bullet"))
        {
            return;
        }

        //Count up collisions
        collisions++;

        //Explode if bullet hits an enemy directly and explodeOnTouch is On
        if (collision.collider.CompareTag("Monster"))
        {
            Explode();
        }
    }
    private void Setup()
    {
        //Create a new Physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        //Assign material to collider
        GetComponent<SphereCollider>().material = physics_mat;

        //Set Gravity
        rb.useGravity = useGravity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}

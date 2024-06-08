using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D rb;
    Vector2 position;

    public float changeTime = 3.0f;
    float timer;
    public int xDirection = 0;
    public int yDirection = 0;

    Animator animator;

    public float damage;

    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        position = rb.position;
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
               
                timer = changeTime;
                xDirection = Random.Range(-1, 2);
                yDirection = Random.Range(-1, 2);
            }

        }
       
        position.x = position.x + moveSpeed * Time.deltaTime * xDirection;
        position.y = position.y + moveSpeed * Time.deltaTime * yDirection;
        rb.MovePosition(position);
        animator.SetFloat("MoveX", xDirection);
        animator.SetFloat("MoveY", yDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ChangeHealth(damage);
        }
    }


    public void Fix()
    {
        rb.simulated = false;
        particle.Stop();
        animator.SetTrigger("Fixed");

    }

}

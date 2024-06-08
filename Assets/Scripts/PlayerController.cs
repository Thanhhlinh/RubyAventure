using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }
    public float moveSpeed = 3f;  
    private Rigidbody2D rb;
    private Vector2 movement;
    Vector2 moveDirection = new Vector2(1, 0);
    Animator animator;

    public GameObject projectileObjected;
    public GameObject dialogScreen;

    public float currentHealth;
    public float maxHealth;

    public bool isInvisible =false;


    private void Awake()
    {    
            instance = this;
       
    }
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        
    }

    void Update()
    {

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f))
        {
            moveDirection.Set(movement.x, movement.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);

        animator.SetFloat("Speed", movement.magnitude);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectileObject = Instantiate(projectileObjected, rb.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(moveDirection, 300);
            animator.SetTrigger("Launch");

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                dialogScreen.SetActive(true);
                StartCoroutine(CloseDialog());
            }
        }


       



    }

    IEnumerator CloseDialog()
    {
        yield return new WaitForSeconds(2f);
        dialogScreen.SetActive(false);
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);      
    }


    public void ChangeHealth(float countHealth)
    {
        if(countHealth < 0 )
        {
            if (isInvisible)
            {
                return;
            }  
            animator.SetTrigger("Hit");
            isInvisible = true;
            
            StartCoroutine(CloseIsInvisible());
        }
        if(countHealth>0 && HealthZone.Instance.isZone)
        {
            StartCoroutine(UpHealthZone(countHealth));
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth + countHealth, 0, maxHealth);
    }
    IEnumerator CloseIsInvisible()
    {
        yield return new WaitForSeconds(2f);
        isInvisible = false;
    }

    IEnumerator UpHealthZone(float countHealth)
    {
        yield return new WaitForSeconds(2f);
        currentHealth = Mathf.Clamp(currentHealth + countHealth, 0, maxHealth);
    }

}

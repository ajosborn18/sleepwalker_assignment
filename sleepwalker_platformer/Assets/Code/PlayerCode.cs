using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCode : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    // Animator _animator;

    public float speed = 40.0f;
    public float jump_force = 1500.0f;
    float bullet_force = 400.0f;

    public GameObject bullet_prefab;
    public Transform spawn_pos;

    public LayerMask ground_layer;
    public Transform feet_trans;
    bool grounded = false;
    float ground_check_dist = 0.3f;

    bool isAlive = true;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        // _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(feet_trans.position, ground_check_dist, ground_layer);
        //_animator.SetBool("Grounded", grounded);
        if(isAlive && transform.position.y < -20)
        {
            isAlive = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        //_animator.SetFloat("Speed", Mathf.Abs(xSpeed));

        if(grounded && Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(new Vector2(0, jump_force));
        }

        if((xSpeed > 0 && transform.localScale.x < 0) || (xSpeed < 0 && transform.localScale.x > 0))
        {
            transform.localScale *= new Vector2(-1, 1); // might have to change the y value depending on the scale of the player
            
            
        }

        if(Input.GetButtonDown("Fire1"))
        {
            //_animator.SetTrigger("Shoot");
            GameObject new_bullet = Instantiate(bullet_prefab, spawn_pos.position, Quaternion.identity);
            new_bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bullet_force * transform.localScale.x, 0));
        }
    }
}

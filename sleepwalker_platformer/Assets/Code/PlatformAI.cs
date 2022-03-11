using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAI : MonoBehaviour
{
    public float speed = 5.0f;
    public float look_dist = 4.0f;
    public LayerMask GroundWallLayer;
    Rigidbody2D _rigidbody;
    Transform player;
    public Transform cast_point;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            if(Vector2.Distance(transform.position, player.position) < look_dist)
            {
                if(player.position.x > transform.position.x && transform.localScale.x < 0 ||
                player.position.x < transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
            }
            else if(Physics2D.Raycast(cast_point.position, transform.forward, 1, GroundWallLayer) ||
            !Physics2D.Raycast(cast_point.position, -transform.up, 1, GroundWallLayer))
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _rigidbody.velocity = new Vector2(speed * transform.localScale.x, _rigidbody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

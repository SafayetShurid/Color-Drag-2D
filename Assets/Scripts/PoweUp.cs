using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PowerUpType
{
    RainbowBall
}

public class PoweUp : MonoBehaviour
{

    public PowerUpType powerUpType;
    public Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f,0f,90f)*2f*Time.deltaTime,Space.Self);
        rb.velocity = Vector2.down;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            PowerUpManager.instance.RainbowBallActive();
            SoundManager.instance.PlayWithPowerUpSource(SoundManager.instance.rainbowClip);
            Destroy(this.gameObject);
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyDestroyer"))
        {           
            Destroy(this.gameObject);
        }
    }


}

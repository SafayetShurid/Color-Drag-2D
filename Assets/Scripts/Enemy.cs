using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public ColorType colorType;
    private Color colorRGB;
    public Sprite[] enemySprites;
    public float speed;

    [SerializeField]
    private SpriteRenderer baseSpriteRenderer, faceSpriteRenderer;
    private Rigidbody2D rb;

    private ParticleEffect particleEffect;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        colorType = (ColorType)Random.Range(0, 4);
        colorRGB = ColorPicker.instance.GetColorRGBValue(colorType);
        baseSpriteRenderer.color = colorRGB;
        //faceSpriteRenderer.sprite = enemySprites[Random.Range(0, enemySprites.Length)];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.down * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if(ball.colorType.Equals(this.colorType) || ball.colorType.Equals(ColorType.Rainbow))
            {
                ParticleEffect.instance.SpawnExplosion(transform.position, colorType);
                GameManager.instance.ScoreUp();
                //GameManager.instance.IncreaseCash(cashReward);
                this.gameObject.transform.position = SpawnManager.instance.spawnPoints[Random.Range(0,2)].position;
                EnemyPoolManager.instance.AddNewEnemyToIdlePool(this.gameObject);
                EnemyPoolManager.instance.RemoveEnemyFromPool(this.gameObject);
               
                this.gameObject.SetActive(false);
               
            }
        }
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyDestroyer"))
        {
            GameManager.instance.ReduceHealth();
            SoundManager.instance.PlayWithEnemyDestroyerSource(SoundManager.instance.zap);
            EnemyPoolManager.instance.AddNewEnemyToIdlePool(this.gameObject);
            EnemyPoolManager.instance.RemoveEnemyFromPool(this.gameObject);
            ParticleEffect.instance.SpawnExplosion(transform.position, colorType);
            this.gameObject.SetActive(false);
        }
    }
}

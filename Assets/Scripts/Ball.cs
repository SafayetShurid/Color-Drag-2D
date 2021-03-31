using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Ball : MonoBehaviour
{

    private bool isSelected;
    private Rigidbody2D rb;
    private Rigidbody2D hookRb;
    private SpringJoint2D sj;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float maxDragDistance;

    [SerializeField]
    private float releaseDelay;

    [SerializeField]
    public ColorType colorType;
    private ColorType initialColorType;

    private GameObject parentObject;
    private SlingShot parentSlingShot;

    private CircleCollider2D circleCollider2D;

    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    [SerializeField]
    private Gradient rainBowColor;
    [SerializeField]
    private Gradient basicGradientColor;



    void Start()
    {
        initialColorType = colorType;
        parentObject = transform.parent.gameObject;
        parentSlingShot = parentObject.GetComponent<SlingShot>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();



        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        sj.connectedAnchor = parentSlingShot.hook.localPosition;

        sj.connectedBody = parentObject.GetComponent<Rigidbody2D>();
        hookRb = sj.connectedBody;

        releaseDelay = 1 / (sj.frequency * 4);
        circleCollider2D.isTrigger = true;

        PowerUpManager.instance.OnRainbowBallActive += TurnOnRainbowBall;
        PowerUpManager.instance.OnRainbowBallDeactive += TurnOffRainbowBall;


        if (PowerUpManager.instance.isRainbowBallActive)
        {
            TurnOnRainbowBall();
            colorType = ColorType.Rainbow;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isSelected)
        {
            Drag();
        }
    }

    private void Drag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float distance = Vector2.Distance(mousePosition, hookRb.position);

        if (distance > maxDragDistance)
        {
            Vector2 direction = (mousePosition - hookRb.position).normalized;
            rb.position = hookRb.position + direction * maxDragDistance;
        }
        else
        {
            rb.position = mousePosition;
        }



    }

    private void OnMouseDown()
    {
        isSelected = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isSelected = false;
        rb.isKinematic = false;


        Release();
    }

    public void Release()
    {
        Debug.Log(Vector2.Distance(sj.connectedBody.position, rb.position));
        if (Vector2.Distance(sj.connectedBody.position, rb.position) > 0.7f)
        {
            StartCoroutine(ReleaseRoutine());
        }
    }

    IEnumerator ReleaseRoutine()
    {

        SoundManager.instance.PlayWithBallSource(colorType);
        circleCollider2D.radius = 0.5f;
        yield return new WaitForSeconds(releaseDelay);
        sj.enabled = false;
        circleCollider2D.isTrigger = false;
        rb.velocity = rb.velocity * speed;
        parentSlingShot.SpawnNewBall();


    }

    public void TurnOnRainbowBall()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = PowerUpManager.instance.rainbowBallSprite;
            spriteRenderer.color = Color.white;

        }
        colorType = ColorType.Rainbow;

        if (trailRenderer != null)
        {
            trailRenderer.colorGradient = rainBowColor;

        }

    }

    public void TurnOffRainbowBall()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = PowerUpManager.instance.defaultBallSprite;
            spriteRenderer.color = ColorPicker.instance.GetColorRGBValue(initialColorType);

        }
        colorType = initialColorType;
        if (trailRenderer != null)
        {
            trailRenderer.endColor = ColorPicker.instance.GetColorRGBValue(initialColorType);
            trailRenderer.startColor = ColorPicker.instance.GetColorRGBValue(initialColorType);
            trailRenderer.colorGradient = basicGradientColor;
        }
    }

    private void OnDisable()
    {
        PowerUpManager.instance.OnRainbowBallActive -= TurnOnRainbowBall;
        PowerUpManager.instance.OnRainbowBallDeactive -= TurnOffRainbowBall;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyDestroyer"))
        {

            Destroy(this.gameObject);
        }

    }
}

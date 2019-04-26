using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{

    Vector2 movementVector;
    Rigidbody2D rb;
    bool thrusting = false;
    bool occupied = false;
    [SerializeField] ParticleSystem ps;
    [SerializeField] ParticleSystem explosionPrefab;
    [SerializeField] ParticleSystem rebounceEffectPrefab;
    [SerializeField] AudioClip explosionAudioClip;
    [SerializeField] AudioClip rebounceAudioClip;
    [SerializeField] float rebounceForce = 5f;
    [SerializeField] int maxLifes = 3;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float invulnerableTime = 1.2f;

    [SerializeField] float blinkingCycleTime = 0.25f;

    float Tstart;
    int lifes;
    bool invulnerable = false;
    AudioSource audioSource;

    SpriteRenderer sr;

    public bool Occupied
    {
        get { return occupied; }
        set
        {
            occupied = value;
            UIController.Instance.SetPlayerOccupied(value);
        }
    }

    public int Lifes
    {
        get { return lifes; }
        set
        {
            lifes = value;
            UIController.Instance.SetLifes(value);
        }
    }
    public Vector2 MovementVector
    {
        get
        {
            return movementVector;
        }

        set
        {
            if (value.y > 0)
            {
                movementVector = value;
                if (movementVector.magnitude > maxSpeed)
                {
                    movementVector.Normalize();
                    movementVector *= maxSpeed;
                }

                float ratio = movementVector.magnitude / maxSpeed;
                var main = ps.main;
                var emission = ps.emission;
                main.startLifetime = 0.25f + ratio * 0.75f;
                emission.rateOverTime = 15f + 15f * ratio;

                if (!thrusting)
                {
                    ps.Play();
                    thrusting = true;
                }
            }
            else
            {
                movementVector = Vector2.zero;
                if (thrusting)
                {
                    ps.Stop();
                    thrusting = false;
                }
            }
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(rb);
        Assert.IsNotNull(ps);
        Assert.IsNotNull(explosionPrefab);
        Assert.IsNotNull(explosionAudioClip);
        Assert.IsNotNull(rebounceAudioClip);
        Assert.IsNotNull(audioSource);
        Lifes = maxLifes;

        ps.Stop();
    }
    private void FixedUpdate()
    {
        rb.AddForce(movementVector);
    }

    private void Update()
    {
        if (invulnerable)
        {
            BlinkingEffect();
        }
    }

    void BlinkingEffect()
    {
        float deltaT = Time.time - Tstart;
        float blinkingValue = 0.5f * Mathf.Sin(deltaT / blinkingCycleTime * 2 * Mathf.PI) + 0.5f;
        Color newColor = sr.color;
        newColor.a = blinkingValue;
        sr.color = newColor;
    }

    void StartBlinking()
    {
        Tstart = Time.time;
    }

    void StopBlinking()
    {
        Color newColor = sr.color;
        newColor.a = 1f;
        sr.color = newColor;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BadTerrain") && invulnerable == false)
        {
            Lifes--;
            if (Lifes > 0)
            {
                StartCoroutine(DamageTakenCoroutine());
                Rebounce(other);
                Debug.Log("Rebounce 1");
            }
            else
            {
                ParticleSystem exp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(exp.gameObject, 1f);
                Destroy(gameObject);
                GameController.Instance.audioSource.PlayOneShot(explosionAudioClip);
                //game over?
                GameController.instance.OnGameOver();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BadTerrain"))
        {
            Rebounce(other);
            Debug.Log("Rebounce 2");
            // Debug.Break ();
        }
    }

    void Rebounce(Collision2D other)
    {
        Vector2 dir = (Vector2)(transform.position) - other.GetContact(0).point;
        dir.Normalize();
        rb.AddForce(dir * rebounceForce, ForceMode2D.Impulse);
        ParticleSystem exp = Instantiate(rebounceEffectPrefab, transform.position, Quaternion.identity);
        Destroy(exp.gameObject, 1f);
        if (audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(rebounceAudioClip);
        }
    }

    IEnumerator DamageTakenCoroutine()
    {
        invulnerable = true;
        StartBlinking();
        Debug.Log("invulnerable = true;");
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
        StopBlinking();
        Debug.Log("invulnerable = false;");
        yield return null;
    }
}
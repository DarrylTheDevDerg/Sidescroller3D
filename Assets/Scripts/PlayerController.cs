using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    private Rigidbody rb;
    public GameObject childObject; // Objeto hijo cuyo MeshRenderer o SkinnedMeshRenderer se modificará (en caso de usar Skinned Mesh)
    public float health = 10f;
    public float damageTakenLow;
    public float damageTakenHigh;
    public float invperiod = 1.5f;
    public float flashDuration = 0.2f;         // Duration of the flash effect
    public Color flashColor = Color.red;       // Color to flash
    private Renderer objectRenderer;           // Reference to the object's renderer
    private Color originalColor;               // Original color of the object
    private bool isFlashing = false;           // Flag to track if object is currently flashing

    private bool isBlinking = false;           // Flag to track if object is currently blinking

    public Color blinkColor;
    private float blinkDuration;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;

        blinkDuration = invperiod / 3;
    }

    void Update()
    {
        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * moveSpeed;
        transform.Translate(movement, Space.World);

        // Rotación del objeto hijo (cambio de dirección)
        if (horizontalInput != 0 || verticalInput != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
            childObject.transform.rotation = Quaternion.Slerp(childObject.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Salto
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            TakeFlatDamage(25f);
        }

        if (invperiod > 0)
        {
            StartBlinking();
            invperiod -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    public void TakeFlatDamage(float damageTaken)
    {
        if (invperiod <= 0)
        {
            health -= damageTaken;
            StartFlashEffect();
            invperiod = 1.2f;
        }
        
    }

    public void SetDamageLow(float newvalue)
    {
        damageTakenLow = newvalue;
    }

    public void SetDamageHigh(float newvalue)
    {
        damageTakenHigh = newvalue;
    }

    public void TakeRandomDamage()
    {
        if (invperiod <= 0)
        {
            health -= Random.Range(damageTakenLow, damageTakenHigh);
            StartFlashEffect();
            invperiod = 1.2f;
        }
    }

    private void StartFlashEffect()
    {
        if (!isFlashing && !isBlinking)
        {
            StartCoroutine(FlashEffect());
        }
    }

    private IEnumerator FlashEffect()
    {
        isFlashing = true;
        Color originalColor = objectRenderer.material.color;

        objectRenderer.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        objectRenderer.material.color = originalColor;

        isFlashing = false;
    }

    private IEnumerator BlinkingColorEffect()
    {
        isBlinking = true;
        Color originalColor = objectRenderer.material.color;

        // Blinking loop
        while (isBlinking && invperiod > 0)
        {
            objectRenderer.material.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);

            objectRenderer.material.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }

        if (invperiod <= 0)
        {
            isBlinking = false;
        }

        objectRenderer.material.color = originalColor;
    }

    private void StartBlinking()
    {
        if (!isBlinking && !isFlashing)
        {
            StartCoroutine(BlinkingColorEffect());
        }
    }
}

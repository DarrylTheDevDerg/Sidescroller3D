using UnityEngine;

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
    public float invperiod = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            invperiod = 1.2f;
        }
    }
}

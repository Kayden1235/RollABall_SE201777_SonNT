using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    public int targetCount = 12;

    private Rigidbody rb;
    private Vector2 moveInput;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winTextObject.SetActive(false);
        UpdateCountText();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(moveInput.x, 0f, moveInput.y) * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Items"))
        {
            other.gameObject.SetActive(false);
            count++;
            UpdateCountText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject); 
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    void UpdateCountText()
    {
        scoreText.text = "Count: " + count;

        if (count >= targetCount)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy")); 
        }
    }
}
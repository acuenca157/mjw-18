using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MinigamesController minigamesController;
    public Vector2 MovementSpeed = new Vector2(100.0f, 100.0f); // 2D Movement speed to have independant axis speed
    private new Rigidbody2D rigidbody2D; // Local rigidbody variable to hold a reference to the attached Rigidbody2D component
    private Vector2 inputVector = new Vector2(0.0f, 0.0f);
    public float rotationSpeed = 5.0f;
    public Transform draw;


    void Awake()
    {
        rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        
        rigidbody2D.angularDrag = 0.0f;
        rigidbody2D.gravityScale = 0.0f;
    }

    void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (inputVector.x != 0 || inputVector.y != 0) {
            makeLladosDance();
        }
        UpdateRotation(inputVector.x);
    }

    void UpdateRotation(float x) {
        // DERECHA
        if (0 < x)
        {
            draw.DOLocalRotate(new Vector3(0, 0, 0), 0.35f);
        }
        // IZQUIEDA
        if (0 > x) {
            draw.DOLocalRotate(new Vector3(0, -180, 0), 0.35f);
        }

    }

    void FixedUpdate()
    {
        if(!minigamesController.IsOnMinigame)
        {
            rigidbody2D.MovePosition(rigidbody2D.position + (inputVector * MovementSpeed * Time.fixedDeltaTime));
        }
    }

    void makeLladosDance() {
        Vector3 sinoidalMovement = new Vector3(
                Mathf.Sin(Time.realtimeSinceStartup * (1.0f / 0.1f)) * 0.3f,
                -Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * (1.0f / 0.1f))) * 0.3f,
                this.transform.localPosition.z);
        draw.localPosition = sinoidalMovement;
    }
}

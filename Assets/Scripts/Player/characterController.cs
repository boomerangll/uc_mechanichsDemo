using UnityEngine;

public class characterController : MonoBehaviour {

    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float moveForce = 40f;

    [SerializeField] private bool airControl = false;
    [SerializeField] private bool canDoubleJump = false;

    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    private new Rigidbody2D rigidbody2D;

    const float groundedRadius = .2f;
    const float ceilingRadius = .2f;

    private bool grounded;
    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;

    private float direction;
    private bool isJumping = false;
    private bool hasDoubleJump = false;

    private GameObject playerNormal;
    private GameObject playerDex;
    
    public void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        changeToNormal();
    }

    void Update() {
        direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")) {
            isJumping = true;
        }

        if (Input.GetButtonDown("changeToDex")) {
            changeToDex();
        }

        if (Input.GetButtonDown("changeToNormal")) {
            changeToNormal();
        }
    }

    private void FixedUpdate() {
        move();

        isJumping = false;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject)
                grounded = true;
        }
    }

    public void move() {
        float move = Time.deltaTime * direction * moveForce;

        if (canDoubleJump && !hasDoubleJump && grounded) {
            hasDoubleJump = true;
        }

        if (grounded || airControl) {
            Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
            rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            if (move > 0 && !facingRight) {
                Flip();
            } else if (move < 0 && facingRight) {
                Flip();
            }
        }

        if (isJumping) {
            if (grounded) {
                grounded = false;
                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            } else if (canDoubleJump && hasDoubleJump) {
                hasDoubleJump = false;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }
        }
    }


    private void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void changeToNormal() {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("player");
        for (int i = 0; i < playerObjects.Length; i++) {
            if (playerObjects[i].name.Contains("Normal")) {
                playerObjects[i].SetActive(true);
            } else {
                playerObjects[i].SetActive(false);
            }
        }

        jumpForce = 400f;
        moveForce = 40f;

        canDoubleJump = false;
        hasDoubleJump = false;
    }

    private void changeToDex() {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("player");
        for (int i = 0; i < playerObjects.Length; i++) {
            if (playerObjects[i].name.Contains("Dex")) {
                playerObjects[i].SetActive(true);
            } else {
                playerObjects[i].SetActive(false);
            }
        }

        jumpForce = 680f;
        moveForce = 60f;

        canDoubleJump = true;
        hasDoubleJump = true;
    }
}

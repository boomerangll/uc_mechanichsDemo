using UnityEngine;

public class characterController : MonoBehaviour {
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    const float groundedRadius = .2f;

    private float direction;
    private bool facingRight = true;
    private Vector3 zeroVector = Vector3.zero;

    Player currentPlayer;
    GameObject[] playerObjects;
    new Rigidbody2D rigidbody2D;

    readonly PlayerNormal playerNormal = new PlayerNormal();
    readonly PlayerDex playerDex = new PlayerDex();
    readonly PlayerStr playerStr = new PlayerStr();

    public void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerObjects = GameObject.FindGameObjectsWithTag("player");
        changeToNormal();
    }

    private bool gotHere = false;

    void Update () {
        direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")) {
            currentPlayer.setIsJumping(true);
        }

        if (Input.GetButtonDown("changeToNormal")) {
            changeToNormal();
        }

        if (Input.GetButtonDown("changeToDex")) {
            changeToDex();
        }

        if (Input.GetButtonDown("changeToStr")) {
            changeToStr();
        }
    }

    private void FixedUpdate () {
        move();

        if (currentPlayer.isJumping) {
            currentPlayer.setIsJumping(false);
        }

        if (!currentPlayer.isGrounded) {
            Debug.Log("gotHerere");
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].gameObject != gameObject) {
                    if (gotHere) {
                        currentPlayer.setIsGrounded(true);
                        gotHere = false;
                    }

                    Debug.Log('1');
                    gotHere = true;
                }
            }
        }
    }

    public void move () {
        float move = Time.deltaTime * direction * currentPlayer.move();

        Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref zeroVector, movementSmoothing);

        if (move > 0 && !facingRight) {
            Flip();
        } else if (move < 0 && facingRight) {
            Flip();
        }

        //Debug.Log("isJumping: " + currentPlayer.isJumping);
        //Debug.Log("can jump : " + currentPlayer.canJump());
        //Debug.Log("is grounded : " + currentPlayer.isGrounded);

        if (currentPlayer.isJumping && currentPlayer.canJump()) {
            currentPlayer.setIsGrounded(false);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            rigidbody2D.AddForce(new Vector2(0f, currentPlayer.jump()));
        }
    }


    private void Flip () {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void changeToNormal () {
        changePlayer("playerNormal");
        currentPlayer = playerNormal;
    }

    private void changeToDex () {
        changePlayer("playerDex");
        currentPlayer = playerDex;
    }

    private void changeToStr () {
        changePlayer("playerStr");
        currentPlayer = playerStr;
    }

    private void changePlayer (string playerName) {
        foreach (GameObject playerObject in playerObjects) {
            if (playerObject.name.Equals(playerName)) {
                playerObject.SetActive(true);
            } else {
                playerObject.SetActive(false);
            }
        }
    }
}



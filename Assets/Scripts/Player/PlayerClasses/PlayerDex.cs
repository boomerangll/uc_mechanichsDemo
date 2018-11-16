public class PlayerDex : Player {
    private int jumpsAmount = 2;
    private int currentJumpCount = 0;

    public PlayerDex () : base(680f, 60f) {}

    public override void setIsGrounded (bool isGrounded) {
        base.setIsGrounded(isGrounded);

        if(isGrounded) {
            currentJumpCount = 0;
        }
    }

    public override void setIsJumping (bool isJumping) {
        base.setIsJumping(isJumping);

        if(isJumping) {
            currentJumpCount++;
        }
    }

    public override bool canJump () {
        return currentJumpCount < jumpsAmount;
    }
}
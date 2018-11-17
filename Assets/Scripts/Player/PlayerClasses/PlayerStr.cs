using System.Collections;
using UnityEngine;

public class PlayerStr : Player {
    private float timeBetweenJumps = 2f;
    private float currentTime      = 0f;

    private bool jumpReady = true;

    public PlayerStr() : base(400f, 25f) {
    }

    public override bool canJump() {
        bool returnValue = base.canJump() && jumpReady;
        if (returnValue) {
            jumpReady = false;
            MonoBehasviourInstance.instance.StartCoroutine(startJumpCountdime());
        }

        return returnValue;
    }

    private IEnumerator startJumpCountdime() {
        currentTime = 0f;

        while (currentTime < timeBetweenJumps) {
            yield return new WaitForSeconds(1.0f);
            currentTime++;
        }

        jumpReady = true;
    }
}
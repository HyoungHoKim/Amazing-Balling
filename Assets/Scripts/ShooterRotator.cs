using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    private enum SR_State{
        Idle,
        Vertical,
        Horizontal,
        Ready
    }

    private SR_State state = SR_State.Idle; // 처음 시작은 Idle로
    public float verticalRotateSpeed = 360f; // 수직 방향 회전값 초기값 360
    public float horizontalRotateSpeed = 360f; // 수평 방향 회전값 초기값 360

    public BallShooter ballShooter;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SR_State.Idle:
                if (Input.GetButtonDown("Fire1")) // 누르는 순간
                    state = SR_State.Horizontal;
                break;
            case SR_State.Horizontal:
                if (Input.GetButton("Fire1")) // 누르는 동안 
                    transform.Rotate(new Vector3(0, horizontalRotateSpeed * Time.deltaTime, 0)); // 수평 방향으로 회전시키기
                else if (Input.GetButtonUp("Fire1")) // 떼는 순간
                    state = SR_State.Vertical;
                break;
            case SR_State.Vertical:
                if (Input.GetButton("Fire1")) // 누르는 동안
                    transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0)); // 수직 방향으로 회전시키기
                else if (Input.GetButtonUp("Fire1")) // 떼는 순간
                {
                    state = SR_State.Ready;
                    ballShooter.enabled = true;
                } 
                break;
            default:
                break;
        }
    }

    private void OnEnable() {
        transform.rotation = Quaternion.identity;
        state = SR_State.Idle;
        ballShooter.enabled = false;
    }
}

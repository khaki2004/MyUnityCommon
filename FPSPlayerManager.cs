using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 10f;
    private float cameraSpeed = 3f;
    private float jumpPower = 4f;
    private bool isJump = false;
    // クラス内に変数を追加（カメラ上下用）
    private float verticalRotation = 0f;  // 上下視点の角度（X軸回転）



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player移動の処理
        //前に進む
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * moveSpeed);
        }
        //後ろに進む
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * moveSpeed);
        }
        //右に進む
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * moveSpeed);
        }
        //左に進む
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * moveSpeed);
        }
        //スペースキーでジャンプ
        if (Input.GetKey(KeyCode.Space) && !isJump)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            isJump = true;

        }
    }

    void Update()
    {
        //移動をぴたっと止める
        if (Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.A))
        {
            rb.linearVelocity = Vector3.zero;
        }

        //右クリック、マウスドラッグでカメラの視点を操作する
        float x = Input.GetAxis("Mouse X") * cameraSpeed;


        //視点の上下操作
        float y = Input.GetAxis("Mouse Y") * cameraSpeed; // 縦のマウス移動量

        if (Input.GetMouseButton(1))
        {
            if (Mathf.Abs(x) > 0.1f)
            {
                transform.RotateAround(transform.position, Vector3.up, x);
            }

            verticalRotation -= y; // 縦回転の角度に反映（上向きはマイナス）

            // 真上（+90度）、真下（-90度）までの範囲に制限
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f); // カメラを上下に回転
                                                                                              // ↑↑上下設定ここまで ↑↑↑
        }
    }

    //Groundに着地したらisJumpをfalseにする
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }
}

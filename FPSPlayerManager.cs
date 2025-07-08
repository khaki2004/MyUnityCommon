using UnityEngine;

public class FPSPlayerManager : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 10f;
    private float cameraSpeed = 3f;
    private float jumpPower = 4f;
    private bool isJump = false;



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
        float y = Input.GetAxis("Mouse Y") * cameraSpeed;
        if (Input.GetMouseButton(1))
        {
            if (Mathf.Abs(x) > 0.1f)
            {
                transform.RotateAround(transform.position, Vector3.up, x);
            }
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

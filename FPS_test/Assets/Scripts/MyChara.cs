using UnityEngine;
using System.Collections;

public class MyChara : MonoBehaviour
{

    //キャラクターコントローラー
    private CharacterController cCon;
    //　キャラクターの速度
    private Vector3 velocity;
    //　Animator
    private Animator animator;
    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 1.5f;
    //　走るスピード
    [SerializeField]
    private float runSpeed = 2.5f;
    //　走っているかどうか
    private bool runFlag = false;

    void Start()
    {
        //キャラクターコントローラの取得
        cCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //　キャラクターコントローラのコライダが地面と接触してるかどうか
        if (cCon.isGrounded)
        {
            velocity = Vector3.zero;

            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            //　走るか歩くかでスピードを変更する
            float speed = 0f;

            if (Input.GetButton("Run"))
            {
                runFlag = true;
                speed = runSpeed;
            }
            else
            {
                runFlag = false;
                speed = walkSpeed;
            }
            velocity *= speed;

            if (velocity.magnitude > 0f)
            {
                if (runFlag)
                {
                    animator.SetFloat("Speed", 2.1f);
                }
                else
                {
                    animator.SetFloat("Speed", 1f);
                }
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }

        }
        velocity.y += Physics.gravity.y * Time.deltaTime; //　重力値を計算
        cCon.Move(velocity * Time.deltaTime); //　キャラクターコントローラのMoveを使ってキャラクターを移動させる
    }
}

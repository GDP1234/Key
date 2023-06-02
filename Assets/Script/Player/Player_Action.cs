using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 BugList
1) �ڷ�ƾ���� ���߾��µ� Update���� Ǯ� �����̴� �� ����
   ��ź ���¿��� Ű ��� ������ ������ �̼��ϰ� ������.
 */


// ���� ��� ��
static class Constants
{
    public const short DR = 1;
    public const short DL = 2;
    public const short DU = 3;
    public const short DD = 4;

}

public enum PlayerState
{
    MoveOn =0,
    MoveOff
}

public class Player_Action : MonoBehaviour
{
    #region Singleton
    public static Player_Action instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    private PlayerState playerState;

    public float speed;

    // ĳ���� ����
    short direction;
    Vector3 dirVec;
    bool isCharacterMove;
    float isCharacterTime;

    // h : horizontal , v : vertical
    float h;
    float v;

    bool isHorizonMove;

    /* �� �������� */
    private Rigidbody2D rigid;

    /*�ִϸ��̼� */
    Animator anim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        direction = Constants.DD;
        dirVec = Vector3.down;
        isCharacterTime = 0f;
        isCharacterMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�ӽù������� ���ӿ��� Ȱ��ȭ �� ������ �Ұ���.
        if (GameManager.instance.isGameover)
            return;

        //Ű���� �Է� �޴� �޼ҵ�
        Player_Move();
        //���� ���� velocity �ִ� �޼ҵ�
        Player_velocity();
    }

    void FixedUpdate()
    {
        if (isCharacterMove)
        {
            isCharacterTime += Time.deltaTime;
            if (isCharacterTime >= 0.7f)
            {
                isCharacterMove = false;
                isCharacterTime = 0f;
            }
        }
    }

    void Player_Move()
    {

        if (isCharacterMove)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

            anim.SetInteger("hRaw", 0);
            anim.SetInteger("vRaw", 0);

        }
        else
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

            /* <- ���� Value -1 , -> ���� Value 1 */
            h = Input.GetAxisRaw("Horizontal");
            /* �Ʒ� ���� Value -1, �� ���� Value 1 */
            v = Input.GetAxisRaw("Vertical");

            bool hDown = Input.GetButtonDown("Horizontal");
            bool hUp = Input.GetButtonUp("Horizontal");
            bool vDown = Input.GetButtonDown("Vertical");
            bool vUp = Input.GetButtonUp("Vertical");

            // ������ȯ ����
            if (hDown)
            {
                isHorizonMove = true;

            }
            else if (vDown)
            {
                isHorizonMove = false;

            }
            else if (hUp || vUp)
            {
                isHorizonMove = h != 0;
            }

            // �ִϸ��̼� [ h�� ���� !! v�� ���� !! ]
            if (anim.GetInteger("hRaw") != h)
            {
                anim.SetBool("isMoveDirection", true);
                anim.SetInteger("hRaw", (int)h);
            }
            else if (anim.GetInteger("vRaw") != v)
            {
                anim.SetBool("isMoveDirection", true);
                anim.SetInteger("vRaw", (int)v);
            }
            else
            {
                anim.SetBool("isMoveDirection", false);
            }


        }
    }

    void Player_velocity()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        if (moveVec == Vector2.right)
        {
            direction = Constants.DR;
            dirVec = Vector3.right;
        }
        else if (moveVec == Vector2.left)
        {
            direction = Constants.DL;
            dirVec = Vector3.left;
        }
        else if (moveVec == Vector2.down)
        {
            direction = Constants.DD;
            dirVec = Vector3.down;
        }
        else if (moveVec == Vector2.up)
        {
            direction = Constants.DU;
            dirVec = Vector3.up;
        }

        rigid.velocity = moveVec * speed;
    }

    //ĳ���� ���� getter
    public short get_s_dir()
    {
        return direction;
    }

    public Vector3 get_v_dir()
    {
        return dirVec;
    }

    public void isCharacterSetter(bool isCharacter)
    {
        isCharacterMove = isCharacter;
    }

    // �浹
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Box")
        {
            Debug.Log("�ڽ��� �浹�� !! ");

        }
    }


    //�÷��̾� ������ ���� OnStop(PlayerState.MoveOff) ���� ȣ���Ͽ�����.
    //���谡 ��¦ �߸��Ǿ� ����.
    //�Ű������� 2�� �ް� ���ߴ� �ð����� �޴°� ���ƺ��� ������ ��
    public void OnStop(PlayerState state)
    {
        playerState = state;

        switch (playerState)
        {
            case PlayerState.MoveOn:
                //StartCoroutine(Stop());
                break;
            case PlayerState.MoveOff:
                StartCoroutine(MoveStop());
                break;
        
        }

    }


    private IEnumerator MoveStop()
    {
        while (true)
        {
            yield return StartCoroutine(Stop());

            yield return StartCoroutine(Move());

            if (playerState == PlayerState.MoveOff)
            {
                break;
            }
        }
    }

    // 
    private IEnumerator Stop()
    {
        float currentTime = 0.0f;

        while (currentTime < 2)
        {
            currentTime += Time.deltaTime;
            rigid.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

            anim.SetInteger("hRaw", 0);
            anim.SetInteger("vRaw", 0);
            // �Լ� �ݺ� ��
            yield return null;
        }
        
    }

    private IEnumerator Move()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return null;
    }

}

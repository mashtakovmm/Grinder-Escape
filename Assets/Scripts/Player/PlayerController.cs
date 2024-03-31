using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input Reader")]
    [SerializeField] InputReader inputReader;

    [Header("Controls Vars")]
    [SerializeField] float dragSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpCoolDownTime;
    [SerializeField] float jumpTime;

    [Header("Belts")]
    [SerializeField] GameObject[] beltObjects;
    [Header("Managers")]
    [SerializeField] GameManager gameManager;

    [SerializeField] GameObject blood;

    Dictionary<int, Vector2> beltPositions = new Dictionary<int, Vector2>();
    Rigidbody2D rb;
    Vector2 dir;
    BoxCollider2D collider2d;
    int currentBelt;
    int maxBelts;
    float diffScale;
    bool canMove = true;
    bool canJump = true;
    bool isDead = false;
    public bool IsDead => isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        int i = 0;
        foreach (GameObject belt in beltObjects)
        {
            beltPositions.Add(i, belt.transform.position);
            i++;
        }

        transform.position = beltPositions[1];
        currentBelt = 1;
        maxBelts = beltObjects.Length - 1;
    }

    private void Update()
    {
        Move();

        diffScale = gameManager.DiffScale;
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += GetMove;
        inputReader.JumpEvent += Jump;
        inputReader.SwitchLeftEvent += SwitchLeft;
        inputReader.SwitchRightEvent += SwitchRight;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= GetMove;
        inputReader.JumpEvent -= Jump;
        inputReader.SwitchLeftEvent -= SwitchLeft;
        inputReader.SwitchRightEvent -= SwitchRight;
    }

    private void Move()
    {
        float diff = diffScale;
        if (diff > 5)
        {
            diff = 5;
        }
        if (canMove)
        {
            rb.velocity = dir * moveSpeed + new Vector2(0, -1 - diff);
        }
        else
        {
            rb.velocity = new Vector2(0, -1 - diff);
        }
        Debug.Log(rb.velocity);
    }

    private void GetMove(Vector2 vector2)
    {
        dir = vector2;
    }

    private void Jump()
    {
        if (canJump && canMove)
        {
            StartCoroutine(JumpProcess());
        }
    }

    private void SwitchLeft()
    {
        if (currentBelt != 0 && canMove)
        {
            float newX = beltPositions[currentBelt - 1].x;
            transform.position = new Vector2(newX, transform.position.y);
            currentBelt--;
        }
    }

    private void SwitchRight()
    {
        if (currentBelt < maxBelts && canMove)
        {
            float newX = beltPositions[currentBelt + 1].x;
            transform.position = new Vector2(newX, transform.position.y);
            currentBelt++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && canMove)
        {
            StartCoroutine(CantMove());
        }
        if (other.tag == "End")
        {
            Die();
        }
    }

    IEnumerator CantMove()
    {
        canMove = false;
        yield return new WaitForSeconds(2f);
        canMove = true;
    }


    IEnumerator CantJump()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCoolDownTime);
        canJump = true;
    }

    IEnumerator JumpProcess()
    {
        float diff = diffScale;
        if (diff > 0.5)
        {
            diff = 0.5f;
        }
        StartCoroutine(CantJump());
        collider2d.enabled = false;
        transform.localScale = new Vector3(2, 2, 2);
        yield return new WaitForSeconds(jumpTime - diffScale);
        transform.localScale = new Vector3(1, 1, 1);
        collider2d.enabled = true;
    }

    void Die()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        canMove = false;
        isDead = true;
        GameObject bloodInstance = Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(bloodInstance, 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    //enables the player to move and defines public uses like lives and gameobjects that need to be connected
    Rigidbody2D rb;
    public int lives = 3;
    public GameObject Player;
    public GameObject RespawnPoint;
    //why is nothing working
    public int DeathTime;

    public Collider2D objectCollider;
    public Collider2D anotherCollider;
    private Points points;
    private Text itemText;
    private GameObject itemOutline;
    private List<GameObject> itemList = new List<GameObject>();
    public GameObject explosionAttack;
    public GameObject healEffect;
    public float itemCooldown = 2f;
    private float currentItemCooldown = 0;
    public HealthBar healthbar;
    public LayerMask groundLayer;
    public Sprite checkedCheckPoint;
    public Sprite unCheckedCheckPoint;
    private Animator animator;


    //edited values for making the respawn mechanic work properly
    public float movementSpeed;
        public float movementJump;

    //Movement
        float speed;
        float jump;
    void Start()
    {
        healthbar.SetMaxHealth(lives);
        animator = GetComponent<Animator>();
        itemText = GameObject.FindWithTag("ItemText").GetComponent<Text>();
        points = GameObject.FindWithTag("PointCounter").GetComponent<Points>();
        itemOutline = GameObject.FindWithTag("ItemOutline");
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        speed = movementSpeed;
        jump = movementJump;
    }
   

        
    void Update()
    {

        if (currentItemCooldown >= 0.1)
        {
            currentItemCooldown -= Time.deltaTime;
        }

        //Jumping
        if (Input.GetButton("Jump") && IsGrounded())
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
        }

        //Left Right Movement
        if (Input.GetButton("Left"))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        if (Input.GetButton("Right"))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //nick: cycles forward in itemList
        if (Input.GetButtonDown("Item forw") && itemList.Count > 0)
        {
            itemList.Add(itemList[0]);
            itemList.RemoveAt(0);
            UpdateItem();
        }

        //nick: cycles backward in itemList
        if (Input.GetButtonDown("Item backw") && itemList.Count > 0)
        {
            itemList.Insert(0, itemList[itemList.Count - 1]);
            itemList.RemoveAt(itemList.Count - 1);
            UpdateItem();
        }

        if (Input.GetButtonDown("Item use")  && itemList.Count > 0 && currentItemCooldown <= 0.1f)
        {
            if (itemList[0].name == "EXPLOSION")
            {
                Instantiate(itemList[0].GetComponent<ItemScript>().effect, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("Weapon"), 0.25f);
            }

            if (itemList[0].name == "HEAL" && points.currentPoints >= 100)
            {
                Instantiate(itemList[0].GetComponent<ItemScript>().effect, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                TakeDamage(-1);
                Destroy(GameObject.FindGameObjectWithTag("Heal"), 0.5f);
                points.GetPoints(-50);
            } 
            currentItemCooldown += itemCooldown;
        }

        float dirX = Input.GetAxisRaw("Horizontal");
        
        animator.SetFloat("speed", dirX * dirX);

        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
    }

    bool IsGrounded() {
    Vector2 position = transform.position;
    Vector2 direction = Vector2.down;
    float distance = 1.0f;
    
    RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
    if (hit.collider != null) {
        return true;
    }
    
    return false;
}
    void OnTriggerEnter2D(Collider2D collision)
    {
        //nick: adds item to itemList when touching an item
        if (collision.gameObject.tag == "Item")
        {
            itemList.Insert(0, collision.gameObject);
            collision.gameObject.transform.position = new Vector2(-300, -300);
            UpdateItem();
        }

        if (collision.gameObject.tag == "CheckPoint")
        {
            RespawnPoint.GetComponent<SpriteRenderer>().sprite = unCheckedCheckPoint;
            RespawnPoint = collision.gameObject;
            RespawnPoint.GetComponent<SpriteRenderer>().sprite = checkedCheckPoint;
        }
    }

    void UpdateItem()
    {
        Destroy(GameObject.FindWithTag("ItemUI"));
        var equippedItem = Instantiate(itemList[0].GetComponent<ItemScript>().UI, new Vector2(itemOutline.transform.position.x, itemOutline.transform.position.y), Quaternion.identity);
        equippedItem.transform.SetParent(itemOutline.transform);
        itemText.text = itemList[0].name;
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        
        if (lives <= 0)
        {
            StartCoroutine(Dead());
        }
        else if (lives > 3)
        {
            lives = 3;
        }

        points.GetPoints(-50);
        healthbar.SetHealth(lives);



        IEnumerator Dead()
            {
            Debug.Log("dead");
            points.GetPoints(-150);
            speed = 0;
            jump = 0;
            GetComponent<Renderer>().enabled = false;
            Player.transform.position = RespawnPoint.transform.position;
            lives = 3;
            yield return new WaitForSeconds(DeathTime);
            speed = movementSpeed;
            jump = movementJump;
            Debug.Log("respawn");
            GetComponent<Renderer>().enabled = true;
            
        }
    }
}
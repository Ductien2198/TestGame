using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ColorObject
{

    [SerializeField] private float speed = 5;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask stepLayer;

    public Transform brickHolder;
    
    public Transform playerSkin;

    [HideInInspector] public Brick stage;

    public int BrickCount => playerBricks.Count;

    [SerializeField] private PlayerBrick playerBrickPrefab;
    private List<PlayerBrick> playerBricks = new List<PlayerBrick>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ChangeColor(colorType);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 nextPoint = JoystickControl.direct * speed * Time.deltaTime + transform.position;

            if (CanMove(nextPoint))
            {
                transform.position = CheckGround(nextPoint);
            }
            if (JoystickControl.direct != Vector3.zero)
            {
                playerSkin.forward = JoystickControl.direct;
            }

        }
    }

    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit,2f, groundLayer))
        {
            return hit.point + Vector3.up*1.1f;
        }

        return transform.position;
        
    }

    public bool CanMove(Vector3 nextPoint)
    {
        bool isCanmove = true;

        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, stepLayer))
        {
            Set_Step step = hit.collider.GetComponent<Set_Step>();
            if(step.colorType != colorType && playerBricks.Count > 0)
            {
                step.ChangeColor(colorType);
                RemoveBrick();
            }
            if(step.colorType != colorType && playerBricks.Count == 0 && playerSkin.forward.z > 0)
            {
                isCanmove = false;
            }
        }

        return isCanmove;
    }

    public void AddBrick()
    {
        int index = playerBricks.Count;

        PlayerBrick playerBrick = Instantiate(playerBrickPrefab, brickHolder);
        playerBrick.ChangeColor(colorType);
        playerBrick.transform.localPosition = index * 0.25f * Vector3.up;
        playerBricks.Add(playerBrick);




    }

    public void RemoveBrick()
    {
        int index = playerBricks.Count - 1;

        if (index >= 0)
        {
            PlayerBrick playerBrick = playerBricks[index];
            playerBricks.Remove(playerBrick);
            Destroy(playerBrick.gameObject);
        }
        
    }

    public void ClearBrick()
    {
        for (int i = 0;i < playerBricks.Count; i++)
        {
            Destroy(playerBricks[i]);
        }

        playerBricks.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Set_Brick brick = other.GetComponent<Set_Brick>();
            if (brick.colorType == colorType)
            {
                AddBrick();
            }
        }
    }

    //private void SpwamBrick()
    //{
    //    //ChangeColor((ColorType)Random.Range(1, 9));
    //    gameObject.SetActive(true);
    //}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;



public class DragAndShoot : MonoBehaviour
{
    public static DragAndShoot Instance;
    private UIManager uiManager;
    private bool dragging = false;
    private float camDistance;
    //private Vector2 lastClickPosition;
    private Rigidbody2D _rb;
    
    [SerializeField] private float force;
    [SerializeField] private float _maxDistance = 4f; // radius
    [SerializeField] private float _safeArea = 1.65f;
    public GameObject attachedObstacle;

    [SerializeField] private LineRenderer leftArm;
    [SerializeField] private LineRenderer rightArm;
    [SerializeField] private Transform leftArmPos;
    [SerializeField] private Transform rightArmPos;
    
    [SerializeField] private float _grav = 15f;

    public int coin;
     
     private void Awake()
     {
         Instance = this;
         _rb = GetComponent<Rigidbody2D>();
         
     }

     private void Start()
     {
         uiManager = UIManager.Instance;
     }

     void OnMouseDown()
    {
        camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        //gameObject.GetComponent<SpringJoint2D>().frequency = 0f;
        
    }
 
    void OnMouseUp()
    {

        dragging = false;
        // aradaki distanca göre
        if (Vector2.Distance(GetMousePosition(), attachedObstacle.transform.position) >= _safeArea && attachedObstacle != null)
        {
            Shoot();
            StartCoroutine(CloseColliderDelay(attachedObstacle));
            transform.localScale = Vector3.one;
            attachedObstacle = null;
        }
        

        
    }
 
    void Update()
    {
        if (attachedObstacle != null)
        {
            LookAtAttachedObstacle();
            CustomGravity();
            //transform.Rotate(0f, 0f, transform.rotation.z);
            //transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z);
            transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
            
            if (!dragging)
            {
                transform.localScale = Vector3.one;
                //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
                //transform.Rotate(0f, 0f, transform.rotation.z);
                transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);

            }
                


        }


        // if (Input.GetMouseButtonDown(0))
        // {
        //     lastClickPosition = GetMousePosition();
        // }
        if (dragging)
        {
            _rb.isKinematic = true;
            _rb.velocity = Vector2.zero;
            if (Vector2.Distance(GetMousePosition(), attachedObstacle.transform.position) <= _maxDistance && attachedObstacle != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 rayPoint = ray.GetPoint(camDistance);
                transform.position = rayPoint;
                ScaleDown();
                transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);

            }
            else
            {
                // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Vector3 rayPoint = ray.GetPoint(camDistance);
                // rayPoint += Vector3.ClampMagnitude(rayPoint, _maxDistance); 
                // transform.position += rayPoint;}}

                Vector2 mousePosition = GetMousePosition();
                Vector2 center = attachedObstacle.transform.position;
 
                Vector2 direction = mousePosition - center; //direction from Center to Cursor
                Vector2 normalizedDirection = direction.normalized;
 
                transform.position = center + (normalizedDirection * _maxDistance);
                transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
            }

        }
        
        if (attachedObstacle != null)
        {
            leftArm.SetPosition(0, leftArmPos.transform.position);
            leftArm.SetPosition(1, attachedObstacle.transform.position);
            rightArm.SetPosition(0, rightArmPos.transform.position);
            rightArm.SetPosition(1, attachedObstacle.transform.position);
        }else
        {
            leftArm.SetPosition(0, rightArmPos.transform.position);
            leftArm.SetPosition(1, gameObject.transform.position);
            rightArm.SetPosition(0, rightArmPos.transform.position);
            rightArm.SetPosition(1, gameObject.transform.position);
        }
        
        
        
        
    }

    private void ScaleDown()
    {
        Vector3 dir = transform.position - attachedObstacle.transform.position;
        float scaleValue = dir.y;
        //Debug.Log(scaleValue);

        transform.localScale = new Vector3(Mathf.Clamp(scaleValue, -0.5f, 1), transform.localScale.y, transform.localScale.z);
        
    }

    public void Shoot()
    {
        //gameObject.GetComponent<SpringJoint2D>().frequency = 5f;

        GetComponent<SpringJoint2D>().enabled = false;
        
        //GetComponent<WheelJoint2D>().enabled = false;
        //GetComponent<PolygonCollider2D>().enabled = false;
        Vector3 playerForce = (transform.position - attachedObstacle.transform.position) * force * -1;
        _rb.velocity = playerForce;
        //_rb.AddForce(playerForce, ForceMode2D.Impulse);
        _rb.isKinematic = false;

    }


    private Vector2 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);
        return pos;
    }

    public void LookAtAttachedObstacle()
    {
        Vector3 dir = transform.position - attachedObstacle.transform.position;
        
        transform.up = -dir.normalized;
        //transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z);
        transform.Rotate(0f, 0f, transform.rotation.z);
        // Vector3 dir = transform.position - attachedObstacle.transform.position;
        // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(angle, Vector2.up);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("attachable"))
        {
            
            
            
            if (attachedObstacle != col.gameObject && attachedObstacle == null)
            {
                uiManager.score += 25;
                attachedObstacle = col.gameObject;
            
                GetComponent<SpringJoint2D>().enabled = true;
                _rb.isKinematic = false;

                //GetComponent<WheelJoint2D>().enabled = true;

                GetComponent<SpringJoint2D>().connectedBody = col.gameObject.GetComponent<Rigidbody2D>();
                //GetComponent<WheelJoint2D>().connectedBody = col.gameObject.GetComponent<Rigidbody2D>();
            }

        }
    }


    IEnumerator CloseColliderDelay(GameObject lastAttachedObstacle)
    {
        lastAttachedObstacle.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(.1f);
        lastAttachedObstacle.GetComponent<Collider2D>().enabled = true;

    }


    private void CustomGravity()
    {
        if (transform.position.y > attachedObstacle.transform.position.y)
        {
            Vector3 vel = _rb.velocity;
            vel.y -= _grav * Time.deltaTime;
            _rb.velocity = vel;
        }

    }

    public void OnTakeCoin()
    {
        coin++;
    }
}

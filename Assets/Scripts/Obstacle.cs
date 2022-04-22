using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Obstacle : MonoBehaviour
{
    public ObstacleType obstacleType;
    private GameObject player;
    private GameObject explodingPoint;
   // [SerializeField] private GameObject plant;
    [SerializeField][Range(0,20)] private float rotarySawValue;
    [SerializeField][Range(0, 20)] private float fireMachineValue;

    public enum ObstacleType
    {
        RotarySaw,
        FireMachine,
        Diamond,
        MovingPoint,
        WallLeft,
        WallRight,
        RotaryPoint,
        Plant,
        ExplodingPoint,
        NormalPoint,
        RotaryFireMachine,
        HalfRotaryFireMachine
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        

        ObstacleMovement();
        transform.DOMove(player.transform.position, 1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear).Pause();
    }

    private void Update()
    { 
        if(obstacleType == ObstacleType.Plant)
        {
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 5)
            {
                Vector3 dir = player.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                StartCoroutine(WaitingPlant());
            }
        }

        
    }

    private void ObstacleMovement()
    {
        if (obstacleType == ObstacleType.RotarySaw)
        {
            transform.DOMoveX(gameObject.transform.position.x + 5, 0.8f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);            
            transform.DORotate(new Vector3(0, 0, transform.rotation.z + 360), rotarySawValue, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }

        else if (obstacleType == ObstacleType.RotaryFireMachine)
        {
            transform.DORotate(new Vector3(0, 0, transform.rotation.z + 360), fireMachineValue, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
        
        else if (obstacleType == ObstacleType.HalfRotaryFireMachine)
        {
            transform.DORotate(new Vector3(0, 0, transform.rotation.z + 180), fireMachineValue, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }

        else if(obstacleType == ObstacleType.WallLeft)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveX( transform.position.x + 3, 0.1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear))
                .Append(transform.DOMoveX(transform.position.x, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear)).SetLoops(-1);
            sequence.Play();  
        }
        else if(obstacleType == ObstacleType.WallRight)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveX( transform.position.x - 3, 0.1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear))
                .Append(transform.DOMoveX(transform.position.x, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear)).SetLoops(-1);
            sequence.Play();  
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ExplodingPoint();
    }

    IEnumerator ExplodingPoint()
    {
        if (obstacleType == ObstacleType.ExplodingPoint)
        {
            if (player.GetComponent<DragAndShoot>().attachedObstacle == gameObject)
            {
                yield return new WaitForSeconds(3);
                this.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator NormalPoint()
    {
        if (obstacleType == ObstacleType.NormalPoint)
        {
            yield return new WaitForSeconds(4);
            ExplodingPoint();
        }
    }

    IEnumerator WaitingPlant()
    {
        yield return new WaitForSeconds(2);
        transform.DOPlay();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRow : MonoBehaviour
{
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected float delay;
    [SerializeField] protected float speed = 0.8f;
    [SerializeField] protected float distance = 2.0f;
    [SerializeField]
    [Range(0, 1)]
    protected int curDirection;
	private List<float> directionalDesX = new List<float>() { 0.0f, 0.0f };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 curPos = transform.position;
		directionalDesX[0] = curPos.x + distance * Vector3.left.x;
		directionalDesX[1] = curPos.x + distance * Vector3.right.x;
		if (canMove) StartCoroutine(StartMoving(delay));
    }

    protected IEnumerator StartMoving(float inDelay)
    {
        while(0 == 0)
        {
			yield return new WaitForSeconds(inDelay);
			curDirection = Mathf.Abs(curDirection - 1);
		}
    }

    // Update is called once per frame
    void Update()
    {
        //if (canMove)
        //{
        //    Vector3 newPos = transform.position;
        //    newPos.x = directionalDesX[curDirection];
        //    transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
        //}
        if (canMove)
        {
			Vector3 newPos = transform.position;
			newPos.x = directionalDesX[curDirection];
			transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
		}
	}
}

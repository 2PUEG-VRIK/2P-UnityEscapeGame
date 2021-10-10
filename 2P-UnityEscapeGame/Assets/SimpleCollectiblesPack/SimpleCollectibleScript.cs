using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {Key, ClickItem, DragItem, Type4, Type5}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType; // this gameObject's type

	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	public AudioClip collectSound;

	public GameObject collectEffect;

	private Vector3 m_Offset;
	private float m_ZCoord;

	// 드래그해서 원하는 지점에 놓기 
	private Vector3 pre_position;
	public GameObject target;
	private bool inTarget;

	// Use this for initialization
	void Start () {
		pre_position = transform.position;
		inTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World); 
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
		}
		if( other == target)
        {
			inTarget = true;
        }
	}

	public void Collect()
	{
		if (collectSound)
			Debug.Log("collectSound 있긴 함");
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		//Below is space to add in your code for what happens based on the collectible type
		 
		if (CollectibleType == CollectibleTypes.Key) {

			Debug.Log("Do Key Command");
			
			//sample1에 있는 노란 열쇠. 먹으면 캐릭 머리위 활성화
			GameObject player = GameObject.Find("Man").gameObject;
			GameObject key = GameObject.Find("Man").transform.GetChild(2).gameObject;

			player.GetComponent<Man>().hasKey = true;
 			key.SetActive(true);
		}
		 
		Destroy (gameObject);
	}

	private void OnMouseDown()
	{
		if (CollectibleType == CollectibleTypes.ClickItem || CollectibleType == CollectibleTypes.DragItem)
        {
			Debug.Log("OnMouseDown");
			m_ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			m_Offset = gameObject.transform.position - GetMouseWorldPosition();
		}

	}

    private Vector3 GetMouseWorldPosition()
	{
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = m_ZCoord;

		return Camera.main.ScreenToWorldPoint(mousePoint);
	}

	private void OnMouseUp()
	{
		if (CollectibleType == CollectibleTypes.ClickItem)
		{
			Destroy(gameObject);
		}
		if (CollectibleType == CollectibleTypes.DragItem)
        {
            if (inTarget)
            {
				Debug.Log("inTarget");
				Destroy(gameObject);
			}
			else
            {
				Debug.Log("제자리로 ^^");
				transform.position = pre_position;
            }
		}
	}
	private void OnMouseEnter()
	{
	}

	private void OnMouseDrag()
	{
		if (CollectibleType == CollectibleTypes.DragItem)
		{
			transform.position = GetMouseWorldPosition() + m_Offset;
		}
	}
}

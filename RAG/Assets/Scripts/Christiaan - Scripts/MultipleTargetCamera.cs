﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour {
	public static MultipleTargetCamera _MultipleTargetCamera;
	public List<Transform> targets;

	public Vector3 offset;

	public float smoothTime = .5f;

	public float minZoom = 70f;
	public float maxZoom = 30f;
	public float zoomLimiter = 50f;

	private Vector3 velocity;
	private Camera cam;
	void Awake () {
		_MultipleTargetCamera = this;
	}
	void Start() 
	{
		cam = GetComponent<Camera>();
	}

	void LateUpdate() 
	{
		if (targets.Count == 0)
		return;

		Move();
		Zoom();	
	}

	void Zoom()
	{
		float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance()/zoomLimiter); //Change zoomLimiter to whatever our max distance between 2 players is (want value to go from 0 to 1)
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime) ;
	}

	void Move()
	{
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
	}

	float GetGreatestDistance()
	{
		var bounds = new Bounds(targets[0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++)
		{
			bounds.Encapsulate(targets[i].position);
		}

		return bounds.size.x;
	}

	Vector3 GetCenterPoint()
	{
		if (targets.Count == 1)
		{
			return targets[0].position;
		}

		var bounds = new Bounds(targets[0].position, Vector3.zero);
		for (int i = 0 ; i < targets.Count; i++)
		{
			bounds.Encapsulate(targets[i].position);
		}
		return bounds.center;
	}
}

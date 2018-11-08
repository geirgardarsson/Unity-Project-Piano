﻿using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class pianoKey : MonoBehaviour {

	private AudioSource note;
	private Transform t;
	private Vector3 halfLength;
	private Vector3 startPosition;
	private float pressRotation = 3f;

	void Start() {
		note = GetComponent<AudioSource>();
		t = GetComponent<Transform>();
		startPosition = t.position;
		halfLength = new Vector3(0f, t.localScale[1] * 2, 2f);
	}


	public void setAudioClip(AudioClip newClip) {
		this.note.clip = newClip;
	}


	public void setPitch(float p) {
		this.note.pitch = p;
	}


	private void RotateSelf(float dir) {
		t.RotateAround(
			t.position + halfLength,
			Vector3.right,
			dir * pressRotation
		);
	}


	void OnMouseDown() {
		RotateSelf(-1);
		note.Play();
		SignalCamera();
	}


	void OnMouseUp() {
		RotateSelf(1);
		t.position = startPosition;
	}
	


	void SignalCamera() {
		float xpos = GetComponent<Transform>().position[0];

		Camera cam = Camera.main;
		cam.GetComponent<CameraController>().Signal(xpos);
	}

}

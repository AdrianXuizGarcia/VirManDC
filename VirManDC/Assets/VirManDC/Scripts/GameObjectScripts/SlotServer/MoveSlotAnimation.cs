// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSlotAnimation : MonoBehaviour
{
    private GameObject auxCanvas;
	private GameObject mainCanvasPanel;
	
	private Vector3 startPos;
	
	void Start()
	{
		startPos = transform.position;
	}
	
	public void SetCanvasAnimation(GameObject mainPanel, GameObject canvas)
	{
		auxCanvas = canvas;
		mainCanvasPanel = mainPanel;
	}
	
	public void DoAnimation(bool isClosed)
	{
		if(isClosed) OpenAnimation(); else CloseAnimation();
	}
	
	public void OpenAnimation()
	{
		Vector3 goPos = startPos+(-transform.forward);
		StartCoroutine(MoveToSpot(goPos));

		DeactivatePanel();
		mainCanvasPanel.SetActive(true);
	}
	
	public void CloseAnimation()
	{
		StartCoroutine(MoveToSpot(startPos));
		//screenPanel.SetActive(false);
		DeactivatePanel();
		mainCanvasPanel.SetActive(false);
	}
	
	public void ActivatePanel()
	{
		//screenPanel.SetActive(true);
		auxCanvas.GetComponent<Canvas>().enabled=true;
	}
	
	public void DeactivatePanel()
	{
		//screenPanel.SetActive(true);
		auxCanvas.GetComponent<Canvas>().enabled=false;
	}
	
	IEnumerator MoveToSpot(Vector3 Gotoposition)
	{
		float elapsedTime = 0;
		float waitTime = 0.3f;
		Vector3 currentPos = transform.position;

		while (elapsedTime < waitTime)
		{
			transform.position = Vector3.Lerp(currentPos, Gotoposition, (elapsedTime / waitTime));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.position = Gotoposition;
		yield return null;
	}
}

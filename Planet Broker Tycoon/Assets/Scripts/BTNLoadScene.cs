﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BTNLoadScene : MonoBehaviour
{
	public string SceneToLoad;
	
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void TaskOnClick()
	{
		SceneManager.LoadScene(SceneToLoad);
		Debug.Log("Woosh !");
	}
	
	
}

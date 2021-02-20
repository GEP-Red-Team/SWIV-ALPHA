using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundController : MonoBehaviour
{
    public GameObject playObjects;
    
    [SerializeField] private BackgroundData data;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;

    [SerializeField] private List<GameObject> panels;
    [SerializeField] private Vector3 startPosition;

    private void Start()
    {
        panels = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            var temp = Instantiate(objectToPool, playObjects.transform, true);
            temp.SetActive(false);
            temp.GetComponent<Renderer>().material = data.materials[i];
            temp.transform.localScale = data.panelDimensions;
            panels.Add(temp);
        }

        startPosition = panels[0].transform.position;
        ActivateNewPanel();
    }

    // Randomly chooses next panel to activate (change to different condition?)
    public void ActivateNewPanel()
    {
        var panelChosen = false;
        var max = amountToPool - 1;
        while (!panelChosen)
        {
            var randIndex = Random.Range(0, max);
            if (panels[randIndex].activeSelf) continue;
            panelChosen = true;
            panels[randIndex].SetActive(true);
        }
    }

    private void ResetPanel(GameObject panel)
    {
        panel.SetActive(false);
        panel.transform.position = startPosition;
    }

    // Called in BackgroundKillBox script
    public void CollisionDetected(GameObject panel)
    {
        ResetPanel(panel);
        ActivateNewPanel();
    }
}
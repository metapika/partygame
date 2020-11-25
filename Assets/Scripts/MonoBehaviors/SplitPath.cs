using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitPath : MonoBehaviour
{
    public List<Route> splitPaths = new List<Route>();
    public GameObject PathPrompt;

    private void Start()
    {
        PathPrompt.SetActive(false);
    }
}
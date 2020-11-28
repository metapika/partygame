using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {
    
    namespace Player {
        
        public class PlayerMovement : MonoBehaviour
        {
            [SerializeField] private float speed = 20f;
            [SerializeField] private int routePosition;
            
            public Route currentRoute;
            public int steps;
            public int remainingSteps;
            public bool isMoving = false;
            
            private GameObject body = null;
            private Vector3 offset = new Vector3(0f, 1.255f, 0f);
            [SerializeField] private bool decidePath = false;
            [SerializeField] private SplitPath currentSplitBlock;
            [SerializeField] private GameObject currentPathPrompt;

#region Private Functions

            private void Start()
            {
                //routePosition = 1;
                body = transform.Find("body").gameObject;
                transform.position = currentRoute.childNodeList[0].position + offset;
            }
            private void Update()
            {
                if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
                {
                    steps = Random.Range(1, 6);
                    //Debug.Log("Rolled = " + steps);
                    StartCoroutine(Move());
                }
                
                if(Input.GetKeyDown(KeyCode.E) && !isMoving)
                {
                    steps = 2;
                    //Debug.Log("Rolled = " + steps);
                    StartCoroutine(Move());
                }


                if(Input.GetKeyDown(KeyCode.LeftArrow) && decidePath)
                {
                    //Chose the A path
                    SwitchPath(currentSplitBlock.splitPaths[0]);
                    decidePath = false;
                    steps = remainingSteps;
                    remainingSteps = 0;
                    currentPathPrompt.SetActive(false);
                    Debug.Log("Switched to path A");

                    StartCoroutine(Move());
                }
                else if(Input.GetKeyDown(KeyCode.UpArrow) && decidePath)
                {
                    //Chose the B path
                    SwitchPath(currentSplitBlock.splitPaths[1]);
                    decidePath = false;
                    steps = remainingSteps;
                    remainingSteps = 0;
                    currentPathPrompt.SetActive(false);
                    Debug.Log("Switched to path B");

                    StartCoroutine(Move());
                }
                else if(Input.GetKeyDown(KeyCode.RightArrow) && decidePath)
                {
                    //Chose the C path
                    SwitchPath(currentSplitBlock.splitPaths[2]);
                    decidePath = false;
                    steps = remainingSteps;
                    remainingSteps = 0;
                    currentPathPrompt.SetActive(false);
                    Debug.Log("Switched to path C");

                    StartCoroutine(Move());
                }
            }

            private IEnumerator Move()
            {
                isMoving = true;

                while(steps > 0)
                {
                    routePosition++;

                    if (routePosition >= currentRoute.childNodeList.Count) {
                        routePosition = 0;
                    }
                    
                    Vector3 nextPos = currentRoute.childNodeList[routePosition].position + offset;

                    while (MoveToNextNode(nextPos)) {
                        yield return null;
                    }
                    
                    yield return new WaitForSeconds(0.1f);
                    
                    if(steps != 0) {
                        steps--;
                        Debug.Log("minus one");
                    }
                    //body.transform.rotation = Quaternion.Slerp(body.transform.rotation, Quaternion.LookRotation(nextPos), 0.1f);
                }

                isMoving = false;
            }

            private bool MoveToNextNode(Vector3 goal)
            {
                return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime));
            }

            private void SwitchPath(Route splitPath)
            {
                currentRoute = splitPath;
                return;
            }
            
            private void OnTriggerEnter(Collider other)
            {
                if(other.GetComponent<SplitPath>())
                {
                    remainingSteps = steps;
                    steps = 0;
                    decidePath = true;
                    other.GetComponent<SplitPath>().PathPrompt.SetActive(true);
                    CurrentSplitBlock(other.GetComponent<SplitPath>());
                    CurrentPathPrompt(other.GetComponent<SplitPath>().PathPrompt);
                }
            }
            private void OnTriggerExit(Collider other)
            {
                decidePath = false;
            }
            private SplitPath CurrentSplitBlock(SplitPath splitPathNode)
            {
                return currentSplitBlock = splitPathNode;
            }
            private GameObject CurrentPathPrompt(GameObject splitPathPrompt)
            {
                return currentPathPrompt = splitPathPrompt;
            }
#endregion

        }
    }
}

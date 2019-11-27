using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Route fRoute;
    public List<Node> fullRoute = new List<Node>();
    public Node startNode;
    public Node currentNode;
    public Node goalNode;

    int routePosition;
    int startRouteIndex;

    int steps;
    int doneSteps;
    public bool isOut;
    bool isMoving;
    bool hasTurn;

    void Start()
    {
        startRouteIndex = fRoute.RequestPosition(startNode.gameObject.transform);
        CreateFullRoute();
    }

    void CreateFullRoute(){
        for(int i=0;i<fRoute.childNodeList.Count;i++){
            int tempPost = startRouteIndex +i;
            tempPost %= fRoute.childNodeList.Count;
            fullRoute.Add(fRoute.childNodeList[tempPost].GetComponent<Node>());
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving){
            steps = Random.Range(1,8);
            if(doneSteps + steps < fullRoute.Count){
                StartCoroutine(Move());
            }
            else{
                Debug.Log("Too High");
            }
        }
    }
    IEnumerator Move(){
        if(isMoving){
            yield break;
        }
        isMoving = true;

        while(steps>0){
            routePosition++;
            Vector3 nextPos = fullRoute[routePosition].gameObject.transform.position;
            while(MoveToNextNode(nextPos,8f)){yield return null;}
            yield return new WaitForSeconds(0.1f);
            steps--;
            doneSteps++;
        }
    }
    bool MoveToNextNode(Vector3 goalPos,float speed){
        return goalPos != (transform.position = Vector3.MoveTowards(transform.position,goalPos,speed * Time.deltaTime));
    }
}

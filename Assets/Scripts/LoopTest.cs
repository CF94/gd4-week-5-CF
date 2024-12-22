using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LoopTest : MonoBehaviour
{
    public GameObject[] gameObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void forLoop()
    {
        //for(initialisation; condition; incriment){  }

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
    }

    void foreachLoop()
    {
        //foreach(var variableName in collection {}

        foreach (GameObject obj in gameObjects)
        {
            obj.AddComponent<Rigidbody>();
        }

    }

    void whileLoop()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSection : MonoBehaviour
{
    public GameObject player;

    private float previousSectionX;         // posX of last section spawned
    private Queue<GameObject> sections;     // list of sections to be destroyed
    private int numPrefabs = 5;             // number of sections in folder

    // Use this for initialization
    void Start()
    {
        // spawn the first section and store it so we can spawn a new section next to it
        GameObject section = (GameObject)Instantiate(Resources.Load("Sections/Section0"), Vector3.zero, Quaternion.identity);

        previousSectionX = section.transform.position.x;

        // init queue and add first section to be destroyed
        sections = new Queue<GameObject>();
        sections.Enqueue(section);
    }

    // Update is called once per frame
    void Update()
    {
        // the distance between the player and the last section spawned, add a new random section
        if (previousSectionX - player.transform.position.x < 100)
        {
            // instantiate a random section from our assets and save it as previous
            GameObject section = (GameObject)Instantiate(
                Resources.Load("Sections/Section" + Random.Range(0, numPrefabs).ToString()),    // the object to load
                new Vector3(previousSectionX + 61.32f, 0, 0),                                   // position
                Quaternion.identity);                                                           // rotation

            previousSectionX = section.transform.position.x;

            // add to queue to be destroyed
            sections.Enqueue(section);
        }

        // the distance between the player and the section behind the player, destroy section
        if (player.transform.position.x - sections.Peek().transform.position.x > 100)
        {
            Destroy(sections.Dequeue());
        }
    }
}

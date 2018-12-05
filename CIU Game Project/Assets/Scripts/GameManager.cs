using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private int level1NumberOfCollectables;
    [SerializeField] private int level1Collected = 0;
    [SerializeField] private int level2NumberOfCollectables;
    [SerializeField] private int level2Collected = 0;
    [SerializeField] private int level3NumberOfCollectables;
    [SerializeField] private int level3Collected = 0;

    public float audioSpeed = 1f;

    public AudioSource level1AudioSource;
    public AudioSource level2AudioSource;
    public AudioSource level3AudioSource;

    [SerializeField] private Door level1Door;
    [SerializeField] private Door level2Door;
    [SerializeField] private Door level3Door;

    // Use this for initialization
    void Start () {
        GameObject[] Collectables = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (GameObject collectable in Collectables)
        {
            int levelCollectable;
            levelCollectable = collectable.GetComponent<Collectable>().levelCollectable;

            switch (levelCollectable)
            {
                case 1:
                    ++level1NumberOfCollectables;
                    break;
                case 2:
                    ++level2NumberOfCollectables;
                    break;
                case 3:
                    ++level3NumberOfCollectables;
                    break;
                default:
                    Debug.Log("Invalid level number!");
                    break;
            }
        }

        level1AudioSource.volume = 0f;
        level2AudioSource.volume = 0f;
        level3AudioSource.volume = 0f;
        level1AudioSource.loop = true;
        level2AudioSource.loop = true;
        level3AudioSource.loop = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MusicCollected (int levelCollectable)
    {
        switch (levelCollectable)
        {
            case 1:
                ++level1Collected;
                StartCoroutine(IncreaseVolume(level1AudioSource, level1Collected, level1NumberOfCollectables));
                break;
            case 2:
                ++level2Collected;
                StartCoroutine(IncreaseVolume(level2AudioSource, level2Collected, level2NumberOfCollectables));
                break;
            case 3:
                ++level3Collected;
                StartCoroutine(IncreaseVolume(level3AudioSource, level3Collected, level3NumberOfCollectables));
                break;
            default:
                Debug.Log("Invalid Level Number!");
                break;
        }

        if (level1NumberOfCollectables == level1Collected)
        {
            level1Door.open = true;
        }

        if (level2NumberOfCollectables == level2Collected)
        {
            level2Door.open = true;
        }

        if (level3NumberOfCollectables == level3Collected)
        {
            level3Door.open = true;
        }
    }

    IEnumerator IncreaseVolume(AudioSource audioRef, float collected, float total)
    {
        while (audioRef.volume < (collected / total))
        {
            yield return null;
            audioRef.volume += 0.1f * audioSpeed;
        }
        StopCoroutine("IncreaseVolume");
    }
}

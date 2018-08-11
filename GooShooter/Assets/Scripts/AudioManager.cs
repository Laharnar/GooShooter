using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager instance;

	[SerializeField] private AudioSource music = null;

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

	private void Awake()
	{
		instance = this;
	}
}

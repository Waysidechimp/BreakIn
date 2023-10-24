using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField] List<AudioClip> songs;
    Stack<AudioClip> songsStack;
    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        songs = RandomizeSongList(songs);
        songsStack = new Stack<AudioClip>(songs);

        GetNextSong(songsStack);
    }

    private void Update()
    {
        if(!audio.isPlaying)
        {
            GetNextSong(songsStack);
        }
    }

    //This plays the next song in the randomized stack. Also repopulates stack if it is empty.
    //The return is mainly for debug reasons.
    AudioClip GetNextSong(Stack<AudioClip> songStack)
    {
        AudioClip clip = songsStack.Pop();
        audio.clip = clip;
        audio.Play();
        if(songsStack.Count <= 0)
        {
            RandomizeSongList(songs);
        }
        return clip;
    }

    //Randomized the stack based on the songs put into the song list.
    //Should be able to repopulate stack since songs is never emptied
    List<AudioClip> RandomizeSongList(List<AudioClip> songs)
    {
        List<AudioClip> newList = new List<AudioClip>(songs);

        for(int i = 0; i < songs.Count; i++)
        {
            int k = Random.Range(0, newList.Count - 1);
            newList.Add(songs[k]);
            newList.RemoveAt(k);
        }
        return newList;
    }
}

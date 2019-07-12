using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechScript : MonoBehaviour
{
    public GameObject speechPoint;

    public AudioClip ganondorf;
    public AudioClip minutes;
    public AudioClip ngl;
    public AudioClip overwatch;
    public AudioClip turtles;
    public AudioClip devGang;
    public AudioClip sequel;
    public AudioClip feature;
    public AudioClip coronas;
    public AudioClip gtab;
    public AudioClip sunlight;
    public AudioClip nintendo;
    public AudioClip kojima;
    public AudioClip matty;
    public AudioClip olimar;
    public AudioClip sans;
    public AudioClip frames;
    public AudioClip runtime;
    public AudioClip paid;
    public AudioClip ligma;
    public AudioClip waifu;
    public AudioClip scout;
    public AudioClip gitgud;
    public AudioClip money;
    public AudioClip white;
    public AudioClip dad;
    public AudioClip ultraInstinct;


    public bool isSpawned;
    
    private List<string> quips;
    private List<AudioClip> voices;
    // Start is called before the first frame update
    void Start()
    {
        quips = new List<string>();
        voices = new List<AudioClip>();
        

        quips.Add("NGL this game lit!");
        voices.Add(ngl);

        quips.Add("Kevin's Ganondorf is trash.");
        voices.Add(ganondorf);

        quips.Add("I'm an unplanned feature!");
        voices.Add(feature);

        quips.Add("You think heaven got any Coronas?");
        voices.Add(coronas);

        quips.Add("This meme was brought to you by game dev gang.");
        voices.Add(devGang);

        quips.Add("I should be in the sequel.");
        voices.Add(sequel);

        quips.Add("You need some sunlight... just sayin.");
        voices.Add(sunlight);

        quips.Add("My uncle works for Nintendo.");
        voices.Add(nintendo);

        quips.Add("Directed by Hideo Kojima.");
        voices.Add(kojima);

        quips.Add("Heard rumors that I was gonna be in Matty Ice 1.5 Remix: Extended Cut.");
        voices.Add(matty);

        quips.Add("Olimar should be removed from smash. I think that's fair. - someone who got bodied by Henry");
        voices.Add(olimar);

        quips.Add("Big secret... I'm Sans Undertale.");
        voices.Add(sans);

        quips.Add("Running at 200 fps");
        voices.Add(frames);

        quips.Add("Tell me the runtime complexity of inserting an element into a doubly linked list.");
        voices.Add(runtime);

        quips.Add("Don't tell Anna I payed someone to be in this game please.");
        voices.Add(paid);

        quips.Add("You think Godhand isn't scary? GTAB!");
        voices.Add(gtab);

        quips.Add("I liek turtles");
        voices.Add(turtles);

        quips.Add("You think Overwatch is dead?");
        voices.Add(overwatch);

        quips.Add("I have ligma");
        voices.Add(ligma);

        quips.Add("So I got with Makoto but I think should have gone for Kawakami...");
        voices.Add(waifu);

        quips.Add("I could fly up and scout around... but I won't.");
        voices.Add(scout);

        quips.Add("Git gud");
        voices.Add(gitgud);

        quips.Add("26 MINUTES??!!");
        voices.Add(minutes);

        quips.Add("Jean looks like a lunch money dispenser.");
        voices.Add(money);

        quips.Add("The dude looks like Jean but skinnier and white.");
        voices.Add(white);

        quips.Add("Dad where are you?");
        voices.Add(dad);

        quips.Add("Like zoinks, I'm not even using 10% of my power.");
        voices.Add(ultraInstinct);

        

        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawned){
            transform.position = speechPoint.transform.position;
        }
        
    }

    public void setSpeech(){
        int rando = Random.Range(0, quips.Count);
        GetComponent<TextMesh>().text = quips[rando];
        GetComponent<AudioSource>().clip = voices[rando];
        GetComponent<AudioSource>().Play();
        //GetComponent<TextMesh>().text = "Yoooo";
    }


    public void setSpawn(bool spawn){
        isSpawned = spawn;
    }
}

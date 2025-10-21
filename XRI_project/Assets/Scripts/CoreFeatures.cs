using UnityEngine;

public enum FeatureUsage
{
    Once, // use once
    Toggle, // use multiple
}
public class CoreFeatures : MonoBehaviour
{
    /*
     * Property - Common Way to access code that exists from outside this class
     * public variables or or Properties
     * Properties ENCAPSULATES variables as fields.
     * GET Accessor (Read) - Returns encapsulated variable values
     * SET Accesspr (Write) - Allocates new values to the property fields
     * PROPERTY values use PascalCase
     * 
     * 
     */

    public bool AudioSFXSourceCreated { get; set; }

    [field: SerializeField]
    public AudioClip AudioClipOnStart { get; set; }
    [field: SerializeField]
    public AudioClip AudioClipOnEnd { get; set; }

    private AudioSource AudioSource;

    public FeatureUsage featureUsage;


    protected virtual void awake()
    {
        MakeSFXAudioSource();
    }
    public void MakeSFXAudioSource()
    {
        AudioSource = GetComponent<AudioSource>();

        //iF  THIS IS equal to null set it up
        if (AudioSource == null) { AudioSource = gameObject.GetComponent<AudioSource>(); }
        //whether null or not, still need to make sure this is true
        //on awake, create it
        AudioSFXSourceCreated = true;
    }

    public void Start()
    {

    }
}

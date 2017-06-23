using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]


public class AudioTestCodeForBoomrang : MonoBehaviour {

    [Range(-1f, 1f)]
    public float offset;
    
    public float cutoffOn = 800;
    public float cutoffOff = 100;

    [Range(-1f, 3f)]
    public float PitchMin = 0;
    
    [Range(-1f, 3f)]
    public float PitchMax = 0.3f;
    
    [Range(0f, 10f)]
    public float PitchVibrance = 0.3f;

    public float TimeInAir = 3.0f;
    
    public bool engineOn;
    
    
    System.Random rand = new System.Random();
    AudioLowPassFilter lowPassFilter;
    AudioSource audioSource;
    void Awake() {
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        audioSource = GetComponent<AudioSource>();
        Update();
    }
    
    void OnAudioFilterRead(float[] data, int channels) {
        for (int i = 0; i < data.Length; i++) {
            data[i] = (float)(rand.NextDouble() * 2.0 - 1.0 + offset);
        }
    }
    

    bool pitchGoingUp = true;
    void Update() {
        lowPassFilter.cutoffFrequency = engineOn ? cutoffOn : cutoffOff;

        print(pitchGoingUp +"&&"+ (audioSource.pitch < PitchMax));
        if(pitchGoingUp && audioSource.pitch < PitchMax)
        {
            print("         up");
            audioSource.pitch += PitchVibrance/60;
            if(audioSource.pitch >= PitchMax) pitchGoingUp = false;
        }else if(!pitchGoingUp && audioSource.pitch > PitchMin){
            print("         dwn");
            audioSource.pitch -= PitchVibrance/60;
            if(audioSource.pitch <= PitchMin) pitchGoingUp = true;
        }

    }
}

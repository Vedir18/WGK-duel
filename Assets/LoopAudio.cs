using UnityEngine;

public class LoopAudio : MonoBehaviour
{
    [SerializeField] private float loopAfter;
    [SerializeField] private AudioSource source1, source2;
    private bool playingfirst;
    private float runtime = 0;
    // Start is called before the first frame update
    void Start()
    {
        runtime = Mathf.Infinity;
    }

    // Update is called once per frame
    void Update()
    {
        runtime += Time.deltaTime;
        if(runtime >= loopAfter)
        {
            if (playingfirst) source2.Play();
            else source1.Play();
            playingfirst = !playingfirst;
            runtime = 0;
        }
    }
}

using UnityEngine;

using UnityEngine.Playables;

using UnityEngine.Animations;



[RequireComponent(typeof(Animator))]
public class CustomAnimationController : MonoBehaviour
{
    public AnimationClip[] clipsToPlay;
    public Rigidbody rigidbody;
    PlayableGraph m_Graph;

    void Start()

    {
        rigidbody = GetComponentInParent<Rigidbody>();
        m_Graph = PlayableGraph.Create();

        ScriptPlayable<CustomAnimationControllerPlayable> custPlayable = ScriptPlayable<CustomAnimationControllerPlayable>.Create(m_Graph);

        CustomAnimationControllerPlayable playableBehavior = custPlayable.GetBehaviour();

        playableBehavior.Initialize(clipsToPlay, custPlayable, m_Graph, transform);

        AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(m_Graph, "Animation", GetComponent<Animator>());

        playableOutput.SetSourcePlayable(custPlayable);

        

        m_Graph.Play();

    }

    void OnDisable()

    {

        // Destroys all Playables and Outputs created by the graph.

        m_Graph.Destroy();

    }

    private void OnAnimatorMove()
    {

        //rigidbody.position = rigidbody.position + new Vector3(0, 0, 0.01f);
        
        Vector3 deltaPosition = GetComponent<Animator>().deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / Time.deltaTime;
        rigidbody.velocity = velocity;
    }
}


public class CustomAnimationControllerPlayable : PlayableBehaviour
{

    private int m_CurrentClipIndex = -1;
    private float m_TimeToNextClip;
    private Playable mixer;
    private Transform transform;

    public void Initialize(AnimationClip[] clipsToPlay, Playable owner, PlayableGraph graph, Transform inTransform)
    {
        transform = inTransform;
        owner.SetInputCount(1);
        mixer = AnimationMixerPlayable.Create(graph, clipsToPlay.Length);
        graph.Connect(mixer, 0, owner, 0);
        owner.SetInputWeight(0, 1);
        for (int clipIndex = 0; clipIndex < mixer.GetInputCount(); ++clipIndex)
        {
            graph.Connect(AnimationClipPlayable.Create(graph, clipsToPlay[clipIndex]), 0, mixer, clipIndex);
            mixer.SetInputWeight(clipIndex, 1.0f);
        }
    }


    override public void PrepareFrame(Playable owner, FrameData info)
    {
        if (mixer.GetInputCount() == 0)
            return;
        // Advance to next clip if necessary
        m_TimeToNextClip -= (float)info.deltaTime;
        if (m_TimeToNextClip <= 0.0f)
        {
            m_CurrentClipIndex++;
            if (m_CurrentClipIndex >= mixer.GetInputCount())
                m_CurrentClipIndex = 0;
            var currentClip = (AnimationClipPlayable)mixer.GetInput(m_CurrentClipIndex);
            // Reset the time so that the next clip starts at the correct position
            currentClip.SetTime(0);
            m_TimeToNextClip = 1f;
            //m_TimeToNextClip = currentClip.GetAnimationClip().length;

            
            
        }
        // Adjust the weight of the inputs
        for (int clipIndex = 0; clipIndex < mixer.GetInputCount(); ++clipIndex)
        {
            if (clipIndex == m_CurrentClipIndex)
                mixer.SetInputWeight(clipIndex, 1.0f);
            else
                mixer.SetInputWeight(clipIndex, 0.0f);
        }
    }
}



/*
[RequireComponent(typeof(Animator))]

public class PlayAnimationSample : MonoBehaviour

{
    public AnimationClip clip0;
    public AnimationClip clip1;
    public float tranTime;
    private float leftTime = -1;
    private PlayableGraph m_Graph;
    private AnimationPlayableOutput m_Output;
    private AnimationMixerPlayable m_Mixer;
    public bool tri;


    void Start()
    {
        m_Graph = PlayableGraph.Create();
        m_Mixer = AnimationMixerPlayable.Create(m_Graph, 2);
        m_Output = AnimationPlayableOutput.Create(m_Graph, "Animation", GetComponent<Animator>());
        m_Output.SetSourcePlayable(m_Mixer);
        AnimationClipPlayable clipPlayable0 = AnimationClipPlayable.Create(m_Graph, clip0);
        AnimationClipPlayable clipPlayable1 = AnimationClipPlayable.Create(m_Graph, clip1);
        m_Graph.Connect(clipPlayable0, 0, m_Mixer, 0);
        m_Graph.Connect(clipPlayable1, 0, m_Mixer, 1);
        m_Mixer.SetInputWeight(0, 1);
        m_Mixer.SetInputWeight(1, 0);
        m_Graph.Play();
        

    }

    void OnDisable()

    {

        // Destroys all Playables and Outputs created by the graph.

        m_Graph.Destroy();

    }

    
    void Update()
    {
        if (tri)
        {
            tri = false;
            leftTime = tranTime;
        }

        if (leftTime > 0)
        {
            leftTime = leftTime - Time.deltaTime;
            float weight = leftTime / tranTime;
            m_Mixer.SetInputWeight(0, weight);
            m_Mixer.SetInputWeight(1, 1 - weight);
            Debug.Log(weight);
        }
    }


}

*/
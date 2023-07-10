using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

[RequireComponent(typeof(AudioSource))]

public class FootstepSoundPlayer : MonoBehaviour{
    [SerializeField]
    private LayerMask FloorLayer;
    [SerializeField]
    private TextureSound[] TextureSounds;
    [SerializeField]
    private TextureSound2[] TextureSounds_Sprint;
    [SerializeField]
    private bool BlendTerrainSounds;
    public int WalkSpeed;
    public int SprintSpeed;


    public CharacterController Controller;
    private AudioSource AudioSource;

    private void Awake ()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(CheckGround());
    }

    private IEnumerator CheckGround()
    {
        while(true)
        {
            //if (Controller.isGrounded && Controller.velocity != Vector3.zero && Physics.Raycast(transform.position - new Vector3(0, 0.5f * Controller.height + 0.5f * Controller.radius, 0),
            if (Controller.isGrounded && Controller.velocity != Vector3.zero && Controller.velocity.magnitude <= 4.9 && Physics.Raycast(transform.position - new Vector3(0, 0.5f * Controller.height, 0),
                Vector3.down,
                out RaycastHit hit,
                1f,
                
                FloorLayer)
                )
                {
                    
                    if(hit.collider.TryGetComponent<Terrain>(out Terrain terrain))
                    {
                        yield return StartCoroutine(PlayFootstepSoundFromTerrain(terrain, hit.point));
                    }
                    else if(hit.collider.TryGetComponent<Renderer>(out Renderer renderer))
                    {
                        yield return StartCoroutine(PlayFootstepSoundFromRenderer(renderer));
                    }
                }
            else if (Controller.isGrounded && Controller.velocity != Vector3.zero && Controller.velocity.magnitude >= 5 && Physics.Raycast(transform.position - new Vector3(0, 0.5f * Controller.height, 0),
                Vector3.down,
                out RaycastHit hit2,
                1f,
                FloorLayer)
                )
                {
                    
                    if (hit2.collider.TryGetComponent<Terrain>(out Terrain terrain))
                    {
                        yield return StartCoroutine(PlayFootstepSoundFromTerrainSprint(terrain, hit2.point));
                    }
                    else if(hit2.collider.TryGetComponent<Renderer>(out Renderer renderer))
                    {
                        yield return StartCoroutine(PlayFootstepSoundFromRendererSprint(renderer));
                    }
                }

            yield return null;
        }
    }

    private IEnumerator PlayFootstepSoundFromTerrain(Terrain terrain, Vector3 hitPoint)
    {
        Vector3 terrainPosition = hitPoint - terrain.transform.position;
        Vector3 splatMapPosition = new Vector3(
            terrainPosition.x / terrain.terrainData.size.x,
            0,
            terrainPosition.z / terrain.terrainData.size.z
        );
        
        int x = Mathf.FloorToInt(splatMapPosition.x * terrain.terrainData.alphamapWidth);
        int z = Mathf.FloorToInt(splatMapPosition.z * terrain.terrainData.alphamapHeight);

        float[,,] alphaMap = terrain.terrainData.GetAlphamaps(x, z, 1, 1);
        
        if (!BlendTerrainSounds)
        {
            int primaryIndex = 0;
            for (int i = 1; i < alphaMap.Length; i++)
            {
                if (alphaMap[0, 0, i] > alphaMap[0, 0, primaryIndex])
                {
                    primaryIndex = i;
                }
            }
            
            foreach (TextureSound textureSound in TextureSounds)
            {
                if (textureSound.Albedo == terrain.terrainData.terrainLayers[primaryIndex].diffuseTexture)
                {
                    AudioClip clip = GetClipFromTextureSound(textureSound);
                    AudioSource.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    break;
                }
            }
        }
        else
        {
            List<AudioClip> clips = new List<AudioClip>();
            int clipIndex = 0;
            
            for (int i = 0; i < alphaMap.Length; i++)
            {
                if (alphaMap[0, 0, i] > 0)
                {
                    foreach (TextureSound textureSound in TextureSounds)
                    {
                        if (textureSound.Albedo == terrain.terrainData.terrainLayers[i].diffuseTexture)
                        {
                            AudioClip clip = GetClipFromTextureSound(textureSound);
                            AudioSource.PlayOneShot(clip, alphaMap[0, 0, i]);
                            clips.Add(clip);
                            clipIndex++;
                            break;
                        }
                    }
            
                    float longestClip = clips.Max(clip => clip.length);
                    yield return new WaitForSeconds(longestClip);
                }
            }
        }
    }

    private IEnumerator PlayFootstepSoundFromRenderer(Renderer Renderer)
    {
        foreach(TextureSound textureSound in TextureSounds)
        {
            if (textureSound.Albedo == Renderer.material.GetTexture("_MainTex"))
            {
                AudioClip clip = GetClipFromTextureSound(textureSound);

                AudioSource.PlayOneShot(clip);
                yield return new WaitForSeconds(clip.length);
                break;
            }
        }
    }

    private IEnumerator PlayFootstepSoundFromTerrainSprint(Terrain terrain, Vector3 hitPoint)
    {
        Vector3 terrainPosition = hitPoint - terrain.transform.position;
        Vector3 splatMapPosition = new Vector3(
            terrainPosition.x / terrain.terrainData.size.x,
            0,
            terrainPosition.z / terrain.terrainData.size.z
        );
        
        int x = Mathf.FloorToInt(splatMapPosition.x * terrain.terrainData.alphamapWidth);
        int z = Mathf.FloorToInt(splatMapPosition.z * terrain.terrainData.alphamapHeight);

        float[,,] alphaMap = terrain.terrainData.GetAlphamaps(x, z, 1, 1);
        
        if (!BlendTerrainSounds)
        {
            int primaryIndex = 0;
            for (int i = 1; i < alphaMap.Length; i++)
            {
                if (alphaMap[0, 0, i] > alphaMap[0, 0, primaryIndex])
                {
                    primaryIndex = i;
                }
            }
            
            foreach (TextureSound2 textureSound in TextureSounds_Sprint)
            {
                if (textureSound.Albedo2 == terrain.terrainData.terrainLayers[primaryIndex].diffuseTexture)
                {
                    AudioClip clip = GetClipFromTextureSound2(textureSound);
                    AudioSource.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length/2);
                    break;
                }
            }
        }
        else
        {
            List<AudioClip> clips = new List<AudioClip>();
            int clipIndex = 0;
            
            for (int i = 0; i < alphaMap.Length; i++)
            {
                if (alphaMap[0, 0, i] > 0)
                {
                    foreach (TextureSound2 textureSound in TextureSounds_Sprint)
                    {
                        if (textureSound.Albedo2 == terrain.terrainData.terrainLayers[i].diffuseTexture)
                        {
                            AudioClip clip = GetClipFromTextureSound2(textureSound);
                            AudioSource.PlayOneShot(clip, alphaMap[0, 0, i]);
                            clips.Add(clip);
                            clipIndex++;
                            break;
                        }
                    }
            
                    float longestClip = clips.Max(clip => clip.length);
                    yield return new WaitForSeconds(longestClip/2);
                }
            }
        }
    }

    private IEnumerator PlayFootstepSoundFromRendererSprint(Renderer Renderer)
    {
        foreach(TextureSound2 textureSound in TextureSounds_Sprint)
        {
            if (textureSound.Albedo2 == Renderer.material.GetTexture("_MainTex"))
            {
                AudioClip clip = GetClipFromTextureSound2(textureSound);

                AudioSource.PlayOneShot(clip);
                yield return new WaitForSeconds(clip.length/2);
                break;
            }
        }
    }

    private AudioClip GetClipFromTextureSound(TextureSound TextureSound)
    {
        int clipIndex = Random.Range(0, TextureSound.Clips.Length);
        return TextureSound.Clips[clipIndex];
    }
    private AudioClip GetClipFromTextureSound2(TextureSound2 TextureSound)
    {
        int clipIndex = Random.Range(0, TextureSound.Clips2.Length);
        return TextureSound.Clips2[clipIndex];
    }

    [System.Serializable]
    private class TextureSound
    {
        public Texture Albedo;
        public AudioClip[] Clips;
    }
    [System.Serializable]
    private class TextureSound2
    {
        public Texture Albedo2;
        public AudioClip[] Clips2;
    }
}
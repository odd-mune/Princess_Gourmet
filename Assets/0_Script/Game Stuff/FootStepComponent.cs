using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FootStepComponent : MonoBehaviour
{
    //AudioManager 인스펙터 창에 추가
    [Tooltip("걷는 소리")]
    public string walkSound;

    private Tilemap currentTilemapOrNull;
    private string currentWalkSound;
    private AudioManager theAudio;
    private Vector3Int mPreviousTilePosition = Vector3Int.zero;
    private bool mbHasInitializedPreviousTilePosition = false;
    private TileMapManager mTileMapManager;
    private PlayerManager mPlayerManager;
    private AudioListener mAudioListener;

    // Start is called before the first frame update
    void Start()
    {
        mTileMapManager = FindObjectOfType<TileMapManager>();
        if (mTileMapManager == null)
        {
            Debug.LogError("Settings에 TileMapManager가 설정되어있지 않습니다!!");
            Debug.Break();
        }

        mPlayerManager = FindObjectOfType<PlayerManager>();
        if (mPlayerManager == null)
        {
            Debug.LogError("PlayerManager가 Scene에 없습니다!!");
            Debug.Break();
        }

        mAudioListener = mPlayerManager.GetComponentInChildren<AudioListener>();
        if (mAudioListener == null)
        {
            Debug.LogError("PlayerManager에 AudioListener가 없습니다!!");
            Debug.Break();
        }

        theAudio = FindObjectOfType<AudioManager>();
        if (theAudio == null)
        {
            Debug.LogError("Settings에 AudioManager가 없습니다!!");
            Debug.Break();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool bHasFoundTile = false;

        if (mTileMapManager.grassTilemaps.Count > 0)
        {
            foreach (var grassTilemap in mTileMapManager.grassTilemaps)
            {
                Vector3Int grassTilePosition = grassTilemap.WorldToCell(transform.position);
                if (mbHasInitializedPreviousTilePosition == false || mPreviousTilePosition != grassTilePosition)
                {
                    if (grassTilemap.HasTile(grassTilePosition))
                    {
                        mPreviousTilePosition = grassTilePosition;
                        mbHasInitializedPreviousTilePosition = true;
                        currentTilemapOrNull = grassTilemap;
                        currentWalkSound = walkSound + "_grass";
                        bHasFoundTile = true;
                        break;
                    }
                }
            }
        }

        if (mTileMapManager.rockTilemaps.Count > 0 && bHasFoundTile == false)
        {
            foreach (var rockTilemap in mTileMapManager.rockTilemaps)
            {
                Vector3Int rockTilePosition = rockTilemap.WorldToCell(transform.position);
                if (mbHasInitializedPreviousTilePosition == false || mPreviousTilePosition != rockTilePosition)
                {
                    if (rockTilemap.HasTile(rockTilePosition))
                    {
                        mPreviousTilePosition = rockTilePosition;
                        mbHasInitializedPreviousTilePosition = true;
                        currentTilemapOrNull = rockTilemap;
                        currentWalkSound = walkSound + "_rock";
                        bHasFoundTile = true;
                        break;
                    }
                }
            }
        }

        if (mTileMapManager.woodTilemaps.Count > 0 && bHasFoundTile == false)
        {
            foreach (var woodTilemap in mTileMapManager.woodTilemaps)
            {
                Vector3Int woodTilePosition = woodTilemap.WorldToCell(transform.position);
                if (mbHasInitializedPreviousTilePosition == false || mPreviousTilePosition != woodTilePosition)
                {
                    if (woodTilemap.HasTile(woodTilePosition))
                    {
                        mPreviousTilePosition = woodTilePosition;
                        mbHasInitializedPreviousTilePosition = true;
                        currentTilemapOrNull = woodTilemap;
                        currentWalkSound = walkSound + "_rock";
                        bHasFoundTile = true;
                        break;
                    }
                }
            }
        }
    }

    public void OnFootStep()
    {
        if (gameObject != mPlayerManager.gameObject)
        {
            bool isFootStepListenable = mAudioListener.IsListenable(this);
            if (isFootStepListenable == true)
            {
                float radius = mAudioListener.GetComponent<CircleCollider2D>().radius;
                float distance = Vector3.Distance(mPlayerManager.gameObject.transform.position, transform.position);

                float factor = 1.0f - Mathf.Min(radius, distance) / radius;

                float defaultVolume = theAudio.GetDefaultVolume(currentWalkSound);
                theAudio.SetVolumn(currentWalkSound, defaultVolume * factor);
                theAudio.Play(currentWalkSound);
            }
        }
        else
        {
            theAudio.Play(currentWalkSound);
        }
    }
}

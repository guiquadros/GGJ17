﻿using Assets.Scripts.So;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class CharacterSelectorManager : MonoBehaviour
    {
        [SerializeField] private Image[] spaceshipImages;
        [SerializeField] private SpaceshipsListSo spaceshipsListSo;
        [SerializeField] private AudioListSo audioListSo;

        private readonly int[] spaceshipsSelected = new [] {0, 0};

        private int Validate(int id)
        {
            if (spaceshipsSelected[id] < 0)
            {
                return spaceshipsListSo.spaceshipsSprites.Length - 1;
            }
            else if (spaceshipsSelected[id] >= spaceshipsListSo.spaceshipsSprites.Length)
            {
                return 0;
            }

            return spaceshipsSelected[id];
        }

        private void UpdateSpaceshipSprite(int id)
        {
            spaceshipImages[id].sprite = spaceshipsListSo.spaceshipsSprites[spaceshipsSelected[id]];
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey(Constants.KEY_PLAYER_ONE_ID))
            {
                PlayerPrefs.SetInt(Constants.KEY_PLAYER_ONE_ID, 0);
                PlayerPrefs.SetInt(Constants.KEY_PLAYER_TWO_ID, 0);
            }

            spaceshipsSelected[0] = PlayerPrefs.GetInt(Constants.KEY_PLAYER_ONE_ID);
            spaceshipsSelected[1] = PlayerPrefs.GetInt(Constants.KEY_PLAYER_TWO_ID);

            //Update the last selected ship on the HUD
            UpdateSpaceshipSprite(0);
            UpdateSpaceshipSprite(1);
        }

        private void Update()
        {
            //"Back" key on android
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(Constants.SCENE_ID_MAIN_MENU);
            }
        }

        #region UI Methods

        public void Previous(int id)
        {
            AudioSourceManager.Instance.audioSource.PlayOneShot(audioListSo.ClickClip);

            spaceshipsSelected[id]--;

            spaceshipsSelected[id] = Validate(id);

            UpdateSpaceshipSprite(id);
        }

        public void Next(int id)
        {
            AudioSourceManager.Instance.audioSource.PlayOneShot(audioListSo.ClickClip);

            spaceshipsSelected[id]++;

            spaceshipsSelected[id] = Validate(id);

            UpdateSpaceshipSprite(id);
        }

        public void StartGame()
        {
            AudioSourceManager.Instance.audioSource.PlayOneShot(audioListSo.ClickClip);

            //Save the spaceships' Ids for being used during the game
            PlayerPrefs.SetInt(Constants.KEY_PLAYER_ONE_ID, spaceshipsSelected[0]);
            PlayerPrefs.SetInt(Constants.KEY_PLAYER_TWO_ID, spaceshipsSelected[1]);

            //Change to the game scene
            SceneManager.LoadScene(Constants.SCENE_ID_GAME);
        }

        #endregion
    }
}
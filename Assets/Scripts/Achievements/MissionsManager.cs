﻿using UnityEngine;

namespace Assets.Scripts.Achievements
{
    public class MissionsManager : MonoBehaviour
    {
        [SerializeField]
        private MissionsSo missionsSo;

        [SerializeField]
        private MissionHolder[] MissionHolder;

        private void Start()
        {
            Mission[] missions = missionsSo.GetAllMissions();

            for (int i = 0; i < missions.Length; i++)
            {
                MissionHolder[i].presentationText.text = missions[i].ToString();

                if (missions[i].IsSucceded)
                    MissionHolder[i].SetCompleted();
            }
        }
    }
}
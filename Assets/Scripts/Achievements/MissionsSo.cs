﻿using System.Collections.Generic;
using UnityEngine;

namespace Achievements
{
    public enum MissionType
    {
        TOTAL_GAME_PLAYED = 0,
        TOTAL_GAME_PLAYED_IN_CURRENT_GAME = 1,
        CURRENT_SPACESHIPS_PLAYING = 2
    }

    [CreateAssetMenu(fileName = "newMissionsSo", menuName = "MissionsSo")]
    public class MissionsSo : ScriptableObject
    {
        public List<SwKeyValuePair> Database;

        [SerializeField]
        private Mission[] allMissions;

        public void ResetGameValues()
        {
            MissionType[] gameValues =
            {
                MissionType.TOTAL_GAME_PLAYED_IN_CURRENT_GAME,
                MissionType.CURRENT_SPACESHIPS_PLAYING
            };

            foreach (MissionType missionType in gameValues)
            {
                //Start all values with 0
                Database.Add(new SwKeyValuePair(missionType, 0));
            }
        }

        public bool FindCompletedMissions(ref List<Mission> missions)
        {
            missions.Clear();

            foreach (Mission mission in allMissions)
            {
                if (mission.IsSucceded)
                    continue;

                bool isMissionCompleted = true;

                for (int i = 0; i < mission.Values.Count; i++)
                {
                    if (GetValueByKey(mission.Values[i].Key).Value < mission.Values[i].Value)
                    {
                        isMissionCompleted = false;
                        break;
                    }
                }

                if (isMissionCompleted)
                {
                    mission.IsSucceded = true;
                    missions.Add(mission);
                }
            }

            return missions.Count > 0;
        }

        public void Persist(MissionType missionType, float newValue)
        {
            for (int i = 0; i < Database.Count; i++)
            {
                if (Database[i].Key == missionType)
                {
                    Database[i].Value = newValue;
                    return;
                }
            }
        }

        public void Increment(MissionType missionType, float value)
        {
            for (int i = 0; i < Database.Count; i++)
            {
                if (Database[i].Key == missionType)
                {
                    Database[i].Value += value;
                    return;
                }
            }
        }

        private SwKeyValuePair GetValueByKey(MissionType type)
        {
            for (int i = 0; i < Database.Count; i++)
            {
                if (Database[i].Key == type)
                    return Database[i];
            }

            return null;
        }
     }
}
﻿using System.Collections.Generic;
using System.Linq;
using Profiles;
using UnityEngine;

namespace Menu
{
    public class MainMenuManagerScript : MonoBehaviour
    {
        public Color completedLevelColor;
        public Color unlockedLevelColor;
        public Color lockedLevelColor;

        [SerializeField]
        private bool canPlayLockedLevels;

        private void Start()
        {
            var unlockedLevels = GameObject.Find("ProfileManager").GetComponent<ProfileManager>().selectedProfile.UnlockedLevels;
            EmergeAndExtendPipes(unlockedLevels);
            SetLevelStatuses(unlockedLevels);
            SetLevelButtonColors(unlockedLevels);
        }

        private void SetLevelStatuses(List<int> unlockedLevels)
        {
            var levels = GetLevelButtonScripts();
            foreach(var level in levels)
            {
                if (Config.Debug.CanPlayLockedLevels)
                {
                    level.IsUnlocked = true;
                }
                else
                {
                    level.IsUnlocked = IsLevelUnlocked(unlockedLevels, level.levelIndex);
                }                
            }
        }

        public List<LevelButtonScript> GetLevelButtonScripts()
        {
            var list = new List<LevelButtonScript>();
            var container = GameObject.Find("Cogs");
            for (var i = 0; i < container.transform.childCount; i++)
                list.Add(container.transform.GetChild(i).GetComponent<LevelButtonScript>());

            return list;
        }

        public void SetLevelButtonColors(List<int> unlockedLevels)
        {
            var levels = GetLevelButtonScripts();
            foreach (var level in levels) level.SetButtonColor(GetButtonColor(unlockedLevels, level.levelIndex));
        }

        public Color GetButtonColor(List<int> unlockedLevels, int levelIndex)
        {
            if (IsLevelCompleted(unlockedLevels, levelIndex)) return completedLevelColor;
            if (IsLevelUnlocked(unlockedLevels, levelIndex)) return unlockedLevelColor;
            return lockedLevelColor;
        }

        public bool IsLevelCompleted(List<int> unlockedLevels, int levelIndex)
        {
            if (unlockedLevels == null) return false;
            if (unlockedLevels.Count == 0) return false;
            return levelIndex < unlockedLevels.Max();
        }

        public bool IsLevelUnlocked(List<int> unlockedLevels, int levelIndex)
        {
            if (unlockedLevels == null) return false;
            if (unlockedLevels.Count == 0) return false;
            return unlockedLevels.Contains(levelIndex);
        }


        public void EmergeAndExtendPipes(List<int> unlockedLevels)
        {
            var pipeBasket = GameObject.Find("Pipes").transform;            

            for (var i = 0; i < pipeBasket.childCount; i++)
            {
                var childPipeScript = pipeBasket.GetChild(i).GetComponent<PipeScript>();

                if (unlockedLevels.Contains(childPipeScript.LevelFrom)) childPipeScript.Emerge();
            }

            for (var i = 0; i < pipeBasket.childCount; i++)
            {
                var childPipeScript = pipeBasket.GetChild(i).GetComponent<PipeScript>();

                if (unlockedLevels.Contains(childPipeScript.LevelTo)) childPipeScript.Extend();
            }            
        }
    }
}
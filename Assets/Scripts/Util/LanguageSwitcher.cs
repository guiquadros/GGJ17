﻿using System;
using SmartLocalization;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class LanguageSwitcher : MonoBehaviour
    {
        public bool showGUI = false;

        public enum Language
        {
            Portuguese,
            English
        }

        private static class LanguageHelper
        {
            public static string ParseLanguage(Language lang)
            {
                switch (lang)
                {
                    case Language.Portuguese:
                        return "pt-BR";
                        break;
                    case Language.English:
                        return "en";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("lang", lang, null);
                }
            }
        }

        public virtual void SwitchLanguage(Language newLanguage)
        {
            string languageString = LanguageHelper.ParseLanguage(newLanguage);

            LanguageManager.Instance.defaultLanguage = languageString;

            LanguageManager.Instance.ChangeLanguage(languageString);

            OnLanguageSwitched(languageString);
        }

        protected virtual void OnLanguageSwitched(string languageCode) { }

        protected virtual void Start()
        {
            LanguageManager.SetDontDestroyOnLoad();

#if ANDROID
            Destroy(this.gameObject);
#endif
        }

        private void OnGUI()
        {
            if (!showGUI)
                return;

            if (GUILayout.Button("English"))
                SwitchLanguage(Language.English);

            if (GUILayout.Button("Português"))
                SwitchLanguage(Language.Portuguese);
        }
    }
}

using System;
using TMPro;
using UnityEngine;

namespace Drafts.DataView
{
    public class PlayerPrefsEntryView : MonoBehaviour
    {
        public enum KeyType
        {
            String,
            Int,
            Float,
        }

        [SerializeField] private KeyType type;
        [SerializeField] private string key;
        [SerializeField] private string defaultValue;
        [SerializeField] private TMP_InputField text;
        [SerializeField] private TMP_InputField input;

        private void Awake()
        {
            if (!input) return;
            input.onValueChanged.AddListener(Save);

            input.contentType = type switch
            {
                KeyType.Int => TMP_InputField.ContentType.IntegerNumber,
                KeyType.Float => TMP_InputField.ContentType.DecimalNumber,
                _ => input.contentType
            };

            if (!string.IsNullOrEmpty(defaultValue) && !PlayerPrefs.HasKey(key))
                Save(defaultValue);
        }

        private void OnEnable()
        {
            object obj = type switch
            {
                KeyType.String => PlayerPrefs.GetString(key),
                KeyType.Int => PlayerPrefs.GetInt(key),
                KeyType.Float => PlayerPrefs.GetFloat(key),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (text) text.text = obj.ToString();
            if (input) input.text = obj.ToString();
        }

        private void Save(string value)
        {
            switch (type)
            {
                case KeyType.String: PlayerPrefs.SetString(key, value); break;
                case KeyType.Int: PlayerPrefs.SetInt(key, int.Parse(value)); break;
                case KeyType.Float: PlayerPrefs.SetFloat(key, float.Parse(value)); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
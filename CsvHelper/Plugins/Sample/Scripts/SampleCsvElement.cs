using System;
using CsvHelper.Configuration.Attributes;
using UnityEngine;

namespace CSVHelper
{
    namespace Sample
    {
        [Serializable]
        public struct SampleCsvElement
        {
            [SerializeField, Header("Sample1"), Range(0f, 100f)]
            private float _sample1;
            [SerializeField, Header("Sample2"), Range(0, 100)]
            private int _sample2;
            [SerializeField, Header("Sample3")]
            private string _sample3;


            [Name("SAMPLE1")]
            public float Sample1
            {
                get => _sample1;
                set => _sample1 = value;
            }

            [Name("SAMPLE2")]
            public int Sample2
            {
                get => _sample2;
                set => _sample2 = value;
            }

            [Name("SAMPLE3")]
            public string Sample3
            {
                get => _sample3;
                set => _sample3 = value;
            }

            public SampleCsvElement(float sample1,  int sample2, string sample3)
            {
                _sample1 = sample1;
                _sample2 = sample2;
                _sample3 = sample3;
            }
        }
    }
}
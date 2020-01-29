using System.Collections;
using System.Collections.Generic;
using CsvHelper.Configuration;
using CSVHelper.Support;

namespace CSVHelper
{
    namespace Sample
    {
        public sealed class SampleCsvEntity : ClassMap<SampleCsvEntity>, ICsvEntity<SampleCsvElement>
        {
            public string FileName { get; private set; }
            public List<SampleCsvElement> Elements { get; set; }

            public IEnumerator GetEnumerator()
            {
                return Elements.GetEnumerator();
            }

            public SampleCsvEntity()
            {
                Map(m => m.Elements).ConvertUsing(row =>
                {
                    List<SampleCsvElement> elements = new List<SampleCsvElement>
                    {
                        new SampleCsvElement(
                            row.GetField<float>(nameof(SampleCsvElement.Sample1)),
                            row.GetField<int>(nameof(SampleCsvElement.Sample2)),
                            row.GetField<string>(nameof(SampleCsvElement.Sample3)))
                    };
                    return elements;
                });
            }

            public SampleCsvEntity(string name, List<SampleCsvElement> elements)
            {
                FileName = name;
                Elements = elements;
            }
        }
    }
}
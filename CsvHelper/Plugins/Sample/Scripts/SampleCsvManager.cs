using System.Collections.Generic;
using System.IO;
using CSVHelper.Sample;
using CSVHelper.Support;
using UnityEngine;

public class SampleCsvManager : MonoBehaviour
{
    [SerializeField]
    private List<SampleCsvElement> _sample = new List<SampleCsvElement>();

    // Start is called before the first frame update
    void Start()
    {
        string fileName = "sample";
        var filePath = Path.Combine(Directory.GetParent(UnityEngine.Application.dataPath).FullName, fileName + ".csv");

        // save
        if (_sample.Count > 0)
        {
            var sampleCsvEntity = new SampleCsvEntity(fileName, _sample);
            CsvHelperSupport.SaveToCsv<SampleCsvEntity, SampleCsvElement>(sampleCsvEntity, filePath);
        }

        // load
        var loadCsvData = CsvHelperSupport.LoadFromCsv<SampleCsvEntity, SampleCsvElement>(filePath);
        if (loadCsvData != null) _sample = loadCsvData;
    }
}

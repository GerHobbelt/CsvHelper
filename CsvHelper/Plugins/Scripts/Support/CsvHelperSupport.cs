using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using UnityEngine;

namespace CSVHelper
{
    namespace Support
    {
        /// <summary>
        /// CSV Base Interface
        /// </summary>
        public interface ICsvEntity : IEnumerable
        {
            string FileName { get; }
        }

        /// <summary>
        /// CSV Base Interface
        /// </summary>
        /// <typeparam name="TElement">CSV One line element</typeparam>
        public interface ICsvEntity<TElement>: ICsvEntity
            where TElement : struct
        {
            List<TElement> Elements { get; set; }
        }

        /// <summary>
        /// CSV reading and writing support
        /// </summary>
        public static class CsvHelperSupport
        {
            #region Public Static Method

            /// <summary>
            /// Load CSV data
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <typeparam name="TElement"></typeparam>
            /// <param name="filePath"></param>
            /// <returns>Result</returns>
            public static List<TElement> LoadFromCsv<T, TElement>(string filePath)
                where T : ClassMap<T>, ICsvEntity<TElement>
                where TElement : struct
            {
                return LoadObjectFromCsv<T,TElement>(filePath);
            }

            /// <summary>
            /// Save CSV data
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <typeparam name="TElement"></typeparam>
            /// <typeparam name="TMapping"></typeparam>
            /// <param name="list"></param>
            /// <param name="filePath"></param>
            public static void SaveToCsv<T, TElement>(T list, string filePath)
                where T : ClassMap<T>, ICsvEntity<TElement>
                where TElement : struct
            {
                SaveObjectToCsv<T,TElement>(list, filePath);
            }


            #endregion

            #region Private Static Method

            private static List<TElement> LoadObjectFromCsv<T, TElement>(string path)
                where T : ClassMap<T>, ICsvEntity<TElement>
                where TElement : struct
            {
                List<TElement> result = null;
                try
                {
                    if (File.Exists(path))
                    {
                        using (var streamReader = File.OpenText(path))
                        {
                            using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                            {
                                // Header
                                csv.Configuration.HasHeaderRecord = true;
                                // Register mapping rules
                                csv.Configuration.RegisterClassMap<T>();
                                // Read data
                                var records = csv.GetRecords<TElement>();
                                result = new List<TElement>();
                                foreach (TElement record in records)
                                {
                                    result.Add(record);
                                }
                            }
                        }

                        Debug.Log("CSV Loaded = " + path);
                    }
                    else
                    {
                        Debug.Log("CSV file does not exist = " + path);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("CSV Load error " + e.Message + " " + e.StackTrace);
                    throw;
                }

                return result;
            }

            private static void SaveObjectToCsv<T, TElement>(T contents, string path)
                where T : ClassMap<T>, ICsvEntity<TElement>
                where TElement : struct
            {
                try
                {
                    using (var streamWriter = new StreamWriter(path))
                    {
                        using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                        {
                            // Header
                            csv.Configuration.HasHeaderRecord = true;
                            // Register mapping rules
                            csv.Configuration.RegisterClassMap<T>();
                            // Write data
                            csv.WriteRecords(contents);
                        }
                    }

                    Debug.Log("CSV Saved = " + path);
                }
                catch (Exception e)
                {
                    Debug.Log("CSV Load error " + e.Message + " " + e.StackTrace);
                    throw;
                }
            }

            #endregion
        }
    }
}

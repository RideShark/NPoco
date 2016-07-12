using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco.Tests.Common;
using NUnit.Framework;

namespace NPoco.Tests.Async
{
    public class AAATranslationStrings
    {
        public int ID { get; set; }
        public string TranslationKey { get; set; }
        public string EnglishText { get; set; }
        public string ImportedEnglishText { get; set; }
        public string FrenchText { get; set; }
        public string SpanishText { get; set; }
        public bool FrenchTranslationComplete { get; set; }
        public bool SpanishTranslationComplete { get; set; }
        public string TranslationNotes { get; set; }
        public string TranslationUsage { get; set; }
        public string ServerControlName { get; set; }
        public string PageName { get; set; }
    }


    public class RideSharkAsyncTests : RideSharkRealDBTest
    {

        /// <summary>
        /// Known IDs in the Database for AAATranslationStrings
        /// </summary>
        private int[] _testIds =
        {
            9562, 9528, 9066, 9104, 9450, 9073, 9067, 9116, 9102, 9074, 9255, 9103, 9101, 9100,
            9071, 9072, 7510, 7516, 7514, 8901, 8899, 8902, 8898, 8900, 6321, 4171, 9238, 9089, 8523, 7131, 4981, 4271,
            8481, 7661, 9535, 8238, 5307, 7596, 6322, 8985, 9057, 6323, 6324, 6325, 4611, 6326, 9126, 7783, 7990, 7996
        };

        [Test]
        public async Task QueryMultipleRecords()
        {
            int[] taksCounts = {1, 2, 3, 4, 5};
            var tasks = taksCounts.Select(i => DoWork()).ToList();

            var errors = 0;
            while (tasks.Count > 0)
            {
                var nextTask = await Task.WhenAny(tasks);
                tasks.Remove(nextTask);
                var record = await nextTask;
                errors += record;
            }


            Assert.AreEqual(0, errors);
        }

        public async Task<int> DoWork()
        {
            List<Task<List<AAATranslationStrings>>> tasks = _testIds.Select(id => Database.FetchAsync<AAATranslationStrings>(
                "Select TOP 1 * FROM AAATranslationStrings WHERE ID = @0",
                id
                )).ToList();

            var caughtExceptions = 0;
            var incorrectIds = _testIds.Length;
            try
            {
                while (tasks.Count > 0)
                {
                    var nextTask = await Task.WhenAny(tasks);
                    tasks.Remove(nextTask);
                    var record = await nextTask;
                    AAATranslationStrings translationString = record.FirstOrDefault();
                    if (translationString != null)
                    {
                        if (_testIds.Contains(translationString.ID))
                        {
                            incorrectIds--;
                        }
                    }
                    else
                    {
                        incorrectIds--;
                    }
                }

            }
            catch (Exception ex)
            {
                caughtExceptions += 1;
            }

            return caughtExceptions + incorrectIds;
        }
    }
}

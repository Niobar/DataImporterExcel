using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace MJDateImporterExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Record> listRecords = new List<Record>();
            //List<RecordDeutch> listRecordsDeutch = new List<RecordDeutch>();
            string dataFromFile = System.IO.File.ReadAllText(@"C:\Users\tjtj\Downloads\Love-Nature-EMEA-October-EPG-Affiliates-GMT2_GMT1_Dutch.1.txt");
            Regex exLine = new Regex(@"(?<day>\d{1,2})/(?<month>\d{1,2})/(?<year>\d{1,4})\t(?<hour>\d{1,2}):(?<min>\d{1,2})\t(?<localTitle>[^\t\r\n]*)\t(?:[^\t\r\n]*)\t(?<originalTitle>[^\t\r\n]*)\t(?:[^\t\r\n]*)\t(?:[^\t\r\n]*)\t(?<episodeNumber>\d*)\t(?<episodeYear>\d*)\t(?:[^\t\r\n]*)\t(?<synopsis>[^\t\r\n]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
            //Regex exLine = new Regex(@"(?<day>\d{1,2})/(?<month>\d{1,2})/(?<year>\d{1,4})\t(?<hour>\d{1,2}):(?<min>\d{1,2})\t(?<localTitle>[^\t\r\n]*)\t(?:[^\t\r\n]*)\t(?<originalTitle>[^\t\r\n]*)\t(?:[^\t\r\n]*)\t(?:[^\t\r\n]*)\t(?<episodeNumber>\d*)\t(?<episodeYear>\d*)\t(?<synopsis>[^\t\r\n]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
            MatchCollection mcLine = exLine.Matches(dataFromFile);
            foreach (Match mLine in mcLine)
            {
                Record rec = new Record();
                int tempHourUTC = int.Parse(mLine.Groups["hour"].Value) - 1;
                if (tempHourUTC < 0) tempHourUTC += 24;
                int tempMin = int.Parse(mLine.Groups["min"].Value);
                int tempYear = int.Parse(mLine.Groups["year"].Value);
                int tempMonth = int.Parse(mLine.Groups["month"].Value);
                int tempDay = int.Parse(mLine.Groups["day"].Value);
                rec.BroadcastDateAndTime = new System.DateTime(tempYear, tempMonth, tempDay, tempHourUTC, tempMin, 0);
                rec.SeriesTitle = mLine.Groups["localTitle"].Value;
                rec.EpisodeTitle = mLine.Groups["originalTitle"].Value;
                //rec.Year = int.Parse(mLine.Groups["episodeYear"].Value);
                rec.Year = mLine.Groups["episodeYear"].Value;
                rec.EpisodeNumber = short.Parse(mLine.Groups["episodeNumber"].Value);
                rec.Synopsis = mLine.Groups["synopsis"].Value;
                listRecords.Add(rec);
            }



            using (System.IO.StreamWriter records = new System.IO.StreamWriter(@"C:\Users\tjtj\Downloads\records.txt"))
            {
                foreach (var record in listRecords)
                {
                    records.WriteLine(record.ToString());
                    System.Console.WriteLine(record.ToString());
                }
                records.WriteLine("That's all folks");
            }
        }
    }
}

using System;

namespace MJDateImporterExcel
{
    public class Record
    {
        public DateTime? BroadcastDateAndTime;
        public string SeriesTitle;
        public string EpisodeTitle;
        public short EpisodeNumber;
        public string Year;
        public string Synopsis;

        public override string ToString()
        {
            return "Date: " + BroadcastDateAndTime + " ST: " + SeriesTitle + " ET: " + EpisodeTitle +" EN: "+ EpisodeNumber + " Y: " + Year + " S: " + Synopsis;
        }
    }
   
}

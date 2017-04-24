using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicBreakout
{
    public class ScoreData      
    {

        public ScoreData()
        {
            sortedScoreData = new SortedDictionary<int,string>();
            sortedScoreData.Add(0, "No Scores Recorded Yet");
        }

        public SortedDictionary<int, string> sortedScoreData;
    }



    public class ScoreRecord
    {
        public string Player { get; set; }
        public string Score { get; set; }
    }
}

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
            datalist = new List<KeyValuePair<int,string>>();
            datalist.Add(new KeyValuePair<int, string>(0, "No Scores Recorded Yet"));
        }

        public List<KeyValuePair<int, string>> datalist;
    }



    public class ScoreRecord
    {
        public string Player { get; set; }
        public string Score { get; set; }
    }
}

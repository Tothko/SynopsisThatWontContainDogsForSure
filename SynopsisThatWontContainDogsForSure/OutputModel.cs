using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynopsisThatWontContainDogsForSure
{
    public class OutputModel
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string ThreadName { get; set; }
        public string TaskName { get; set; }
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return "Id: " +Id +" "+ "Time: " + Time + " " + "Value: " + Value + " " + "ThreadName: " + ThreadName + " " + "TaskID: " + TaskName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooop365ML.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Prediction
    {
        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double confidence { get; set; }
        public string @class { get; set; }
    }

    public class Root
    {
        public List<Prediction> predictions { get; set; }
    }



}

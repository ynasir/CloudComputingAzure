using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortgageHelperClassLibrary
{
    public class MortgageSerializer
    {
        public string Serialize(MortgageModel model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public MortgageModel Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<MortgageModel>(data);
        }
    }
}

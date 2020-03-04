using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageHelperClassLibrary;
using Newtonsoft.Json;

namespace UnitTestProject
{
    [TestClass]
    public class MortgageHelperLibraryTest
    {
        [TestMethod]
        public void Test_Compute_Monthly_Payment()
        {
            double monthlypayment = MortgageCalcHelper.ComputeMonthlyPayment(2300, 5.2, 3.33);

            Assert.AreEqual(monthlypayment, 40.19);
        }

        [TestMethod]
        public void Test_Serialization()
        {
            MortgageModel mortgageModel = new MortgageModel
            {
                Principal = "2300",
                Interest = "5.2",
                Duration = "3.33"
            };

            MortgageSerializer mortgageSerializer = new MortgageSerializer();
            var serializedMortgageModel = mortgageSerializer.Serialize(mortgageModel);

            var jsonSerializedModel = JsonConvert.SerializeObject(mortgageModel);

            Assert.AreEqual(serializedMortgageModel, jsonSerializedModel);
        }

        [TestMethod]
        public void Test_Deserialization()
        {
            MortgageModel mortgageModel = new MortgageModel
            {
                Principal = "2300",
                Interest = "5.2",
                Duration = "3.33"
            };

            MortgageSerializer mortgageSerializer = new MortgageSerializer();
            var serializedMortgageModel = mortgageSerializer.Serialize(mortgageModel);
            MortgageModel deSerializeddata = JsonConvert.DeserializeObject<MortgageModel>(serializedMortgageModel);

            var jsonSerializedModel = JsonConvert.SerializeObject(mortgageModel);
            MortgageModel jsondata = JsonConvert.DeserializeObject<MortgageModel>(jsonSerializedModel);

            Assert.AreEqual(deSerializeddata.ToString(), jsondata.ToString());
        }
    }
}

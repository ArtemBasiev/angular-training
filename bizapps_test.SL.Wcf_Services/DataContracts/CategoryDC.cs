using System.Runtime.Serialization;

namespace bizapps_test.SL.Wcf_Services.DataContracts
{
    [DataContract]
    public class CategoryDC
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CategoryName { get; set; }
    }
}
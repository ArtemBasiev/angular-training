using System.Runtime.Serialization;

namespace bizapps_test.SL.Wcf_Services.DataContracts
{
    [DataContract]
    public class BlogUserDC
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string UserPassword { get; set; }

        [DataMember]
        public string BlogName { get; set; }
    }
}
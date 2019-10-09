using System;
using System.Runtime.Serialization;

namespace bizapps_test.SL.Wcf_Services.DataContracts
{
    [DataContract]
    public class PostDC
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public string PostImage { get; set; }
    }
}
using System;
using System.Runtime.Serialization;

namespace bizapps_test.SL.Wcf_Services.DataContracts
{
    [DataContract]
    public class CommentDC
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CommentText { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public int ParentId { get; set; }

        [DataMember]
        public int PostId { get; set; }
    }
}
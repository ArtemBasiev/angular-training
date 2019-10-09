using System;

namespace bizapps_test.SL.ASMX_Services.SoapEntities
{
    public class CommentSoap
    {
        public int Id { get; set; }

        public string CommentText { get; set; }

        public string UserName { get; set; }

        public DateTime CreationDate { get; set; }

        public int ParentId { get; set; }

        public int PostId { get; set; }
    }
}
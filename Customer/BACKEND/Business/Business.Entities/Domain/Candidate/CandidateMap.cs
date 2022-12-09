using System.Collections.Generic;

namespace Business.Entities
{
    public class CandidateMapAdd
    {
        public long CandidateId { get; set; }
        public string CandidateFullName { get; set; }
        public short Gender { get; set; }
        public string Email { get; set; } 
        public int PhoneNumber { get; set; }
        public string Introduction { get; set; }
        public string UserID { get; set; }
        public long RecruitId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentName { get; set; }
        public CandidateAttachmentMapAdd CandidateAttachmentMapAdd { get; set; }
    }

    public class CandidateAttachmentMapAdd
    {
        public long CandidateAttachmentId { get; set; }        
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public string UserID { get; set; }
        public long CandidateId { get; set; }
    }
}

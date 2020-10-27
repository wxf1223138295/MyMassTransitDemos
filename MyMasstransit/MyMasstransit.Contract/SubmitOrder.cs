using System;

namespace MyMasstransit.Contract
{
    public class SubmitOrder
    {
        public Guid OrderId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string CustomerNum { get; set; }
    }

    public class OrderSubmissionAccepted
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Num { get; set; }
    }
}
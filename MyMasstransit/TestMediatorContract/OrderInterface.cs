using System;

namespace TestMediatorContract
{
    public class CreateOrder
    {
        public Guid OrderId { get; set; }
        public DateTime TimeSpam { get; set; }
        public string CustomorNum { get; set; }
    }

    public class OrderSubmissionAccecpt
    {
        public Guid OrderId { get; set; }
        public DateTime TimeSpam { get; set; }
        public string CustomorNum { get; set; }
    }
}
using System;

namespace PubMsgContract
{
    public interface CreateOrder
    {
        Guid Id { get; }
        string OrderName { get; }
        DateTime CreateTime { get; }
        decimal Count { get; }
    }
}
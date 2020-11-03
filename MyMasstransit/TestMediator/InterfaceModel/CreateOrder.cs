using System;

namespace TestMediator.InterfaceModel
{
    public interface CreateOrder
    {
        Guid OrderId { get; }
        DateTime TimeSpam { get;  }
        string CustomorNum { get; }
    }

    public interface OrderSubmissionAccecpt
    {
        Guid OrderId { get; }
        DateTime TimeSpam { get;  }
        string CustomorNum { get; }
    }
}
﻿namespace Calendar.Domain.Common
{
    public interface IEventHandler<T>
    {
        void Handle(T @event);
    }
}

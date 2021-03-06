﻿using System;
using NServiceBus;

namespace HumanResources.Messages.Commands
{
    public class BookHoliday : IMessage
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End{ get; set; }
        public string Description { get; set; }
    }
}

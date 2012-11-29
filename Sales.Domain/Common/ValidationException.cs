﻿using System;

namespace Sales.Domain.Common
{
    public class ValidationException : Exception
    {
        public ValidationMessageCollection ValidationMessages { get; set; }

        public ValidationException(ValidationMessageCollection validationMessages)
            : base("A validation error occured.")
        {
            ValidationMessages = validationMessages;
        }
    }
}

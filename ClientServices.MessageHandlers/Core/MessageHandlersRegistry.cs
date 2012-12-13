﻿using ClientServices.Data.Core;
using StructureMap.Configuration.DSL;

namespace ClientServices.MessageHandlers.Core
{
    class MessageHandlersRegistry : Registry
    {
        public MessageHandlersRegistry()
        {
            Configure(x =>
            {
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}

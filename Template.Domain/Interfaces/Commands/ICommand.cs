﻿using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoManager.Domain.Interfaces.Commands
{
    public interface ICommand 
    {
        void Validate();
    }
}

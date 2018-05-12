﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHWebApplication.TasksRunAsync
{
    public interface ITaskInvoke
    {
        double PeriodMS { get; }

        Task Invoke();

    }
}

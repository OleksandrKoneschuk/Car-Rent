﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public interface IMessageService
    {
        void SendMessage(string phoneNumber, string message);
    }
}

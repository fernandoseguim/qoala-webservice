﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace QoalaWS.Exceptions
{
    [Serializable]
    public class CreateRecordException : Exception, ISerializable
    {
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    public abstract class ServiceResponseBase
    {
        public ServiceResponseBase()
        {
            this.Exception = null;
        }

        /// <summary>
        /// Save the exception thrown so that consumers can read it
        /// </summary>
        public Exception Exception { get; set; }
    }
}

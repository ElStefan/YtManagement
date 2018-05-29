using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YtManagement.Core
{
    public interface ISystemCore
    {
        int Value { get; set; }
    }
    public class SystemCore : ISystemCore
    {
        public int Value { get; set; }


        public SystemCore()
        {
        }
    }
}

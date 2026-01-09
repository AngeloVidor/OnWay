using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Services
{
    public sealed class InMemoryTrackingNumberGenerator : ITrackingNumberGenerator
    {
        private int _counter = 0;

        public int GetNextNumber()
        {
            return Interlocked.Increment(ref _counter);
        }
    }
}
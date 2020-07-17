using System;
using System.Collections.Generic;
using System.Text;

namespace DCWR.Event_Manager.Infrastructure
{
    public interface IGuidIdGenerator
    {
        Guid Generate();
    }

    public class GuidIdGenerator : IGuidIdGenerator
    {
        public static GuidIdGenerator Instance = new GuidIdGenerator();

        private GuidIdGenerator()
        {

        }

        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}

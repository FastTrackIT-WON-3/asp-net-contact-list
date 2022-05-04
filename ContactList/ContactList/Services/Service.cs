using System;

namespace ContactList.Services
{
    public abstract class Service : IService
    {
        public Service()
        {
            Identifier = Guid.NewGuid().ToString("D");
        }

        public string Identifier { get; }
    }
}

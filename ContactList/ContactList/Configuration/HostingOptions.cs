using System.Collections.Generic;

namespace ContactList.Configuration
{
    public class HostingOptions
    {
        public string ProviderName { get; set; }

        public string DeployUrl { get; set; }

        public List<int> OtherConfig { get; set; }

        public OtherComplexConfigOptions OtherComplexConfig { get; set; }
    }

    public class OtherComplexConfigOptions
    {
        public string SomeProperty { get; set; }
    }
}

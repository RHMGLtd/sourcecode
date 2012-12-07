using coolbunny.common.Extensions;

namespace blackpoolgigs.common.Contracts
{
    public class VenueName
    {
        public string Value { get; set; }

        public VenueName Tidy()
        {
            Value = Value.Tidy();
            return this;
        }
    }
}
namespace ZenStates
{
    public class CustomListItem
    {
        public uint Value { get; }

        public string Display { get; }

        public CustomListItem(uint value, string display)
        {
            Value = value;
            Display = display;
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
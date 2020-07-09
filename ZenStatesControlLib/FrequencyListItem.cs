namespace ZenStates
{
  public class FrequencyListItem
  {
    public double Multi { get; }

    public string Display { get; }

    public FrequencyListItem(double multi, string display)
    {
      Multi = multi;
      Display = display;
    }

    public override string ToString()
    {
      return Display;
    }
  }
}
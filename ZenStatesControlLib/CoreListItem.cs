namespace ZenStates
{
  public class CoreListItem
  {
    public int CCD { get; }

    public int CCX { get; }

    public int CORE { get; }

    public string Display { get; }

    public CoreListItem(int ccd, int ccx, int core, string display)
    {
      CCD = ccd;
      CCX = ccx;
      CORE = core;
      Display = display;
    }

    public override string ToString()
    {
        return Display;
    }
  }
}
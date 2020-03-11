namespace ZenStates
{
  public class CoreListItem
  {
    public int CCD { get; }

    public int CCX { get; }

    public int CORE { get; }

    public CoreListItem(int ccd, int ccx, int core)
    {
      CCD = ccd;
      CCX = ccx;
      CORE = core;
    }

    public override string ToString()
    {
      return string.Format("Core {0}", CORE);
    }
  }
}
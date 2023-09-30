namespace CPU.Emulator;

public interface ICpuInfo
{
    public string Stats();
}

public interface ICpu : ICpuInfo
{
    public void Start();
    public void Start(Action<ICpuInfo>? afterStep);
}

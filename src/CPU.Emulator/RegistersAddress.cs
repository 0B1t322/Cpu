namespace CPU.Emulator;

public enum RegistersAddress : ushort
{
    Flags,
    A,
    B,
    C,
    D,
    E,
    F
}

public static class RegistersAddressExtensions
{
    public static ushort Address(this RegistersAddress registersAddress) =>
        (ushort)registersAddress;
}

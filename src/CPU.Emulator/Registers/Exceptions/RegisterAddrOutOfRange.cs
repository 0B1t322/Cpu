namespace CPU.Emulator.Registers.Exceptions;

public class RegisterAddrOutOfRange : ArgumentOutOfRangeException
{
    public RegisterAddrOutOfRange(ushort providedAddr, ushort maxAddr) : base(
        $"Register addr={providedAddr} out of range. Max addr=${maxAddr}")
    {
    }
}

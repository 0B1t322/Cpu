namespace CPU.Emulator.Registers;

public class FlagRegister
{
    private readonly Int32Register _register;

    private const int CfMask = (0b1);
    private const int LZMask = CfMask << 1;
    private const int EZMask = LZMask << 1;
    private const int GZMask = EZMask << 1;

    public FlagRegister(Int32Register register)
    {
        _register = register;
    }

    private Int32 Value
    {
        get => _register.Value;
        set => _register.Value = value;
    }

    public bool CF
    {
        get => (Value & CfMask) > 0;
        set
        {
            if (value)
                Value |= CfMask;
            else
                Value &= ~CfMask;
        }
    }

    public bool GZ
    {
        get => (Value & GZMask) > 0;
        set
        {
            if (value)
                Value |= GZMask;
            else
                Value &= ~GZMask;
        }
    }
    
    public bool EZ
    {
        get => (Value & EZMask) > 0;
        set
        {
            if (value)
                Value |= EZMask;
            else
                Value &= ~EZMask;
        }
    }

    public bool LZ
    {
        get => (Value & LZMask) > 0;
        set
        {
            if (value)
                Value |= LZMask;
            else
                Value &= ~LZMask;
        }
    }
}

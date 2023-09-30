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
// public enum RegistersAddress : ushort
// {
//     /// <summary>
//     /// High 16 bits of accumulator
//     /// </summary>
//     AH,
//
//     /// <summary>
//     /// Low 16 bits of accumulator
//     /// </summary>
//     AL,
//
//     /// <summary>
//     /// Flags register
//     /// </summary>
//     Flags,
//
//     /// <summary>
//     /// High 16 bits of B register
//     /// </summary>
//     BH,
//
//     /// <summary>
//     /// Lower 16 bits of B register
//     /// </summary>
//     BL,
//
//     /// <summary>
//     /// High 16 bits of C register
//     /// </summary>
//     CH,
//
//     /// <summary>
//     /// Low 16 bits of C register
//     /// </summary>
//     CL,
//
//     /// <summary>
//     /// High 16 bits of D register 
//     /// </summary>
//     DH,
//
//     /// <summary>
//     /// Low 16 bits of D register
//     /// </summary>
//     DL,
//
//     /// <summary>
//     /// High 16 bits of E register
//     /// </summary>
//     EH,
//
//     /// <summary>
//     /// Low 16 bits of E register
//     /// </summary>
//     EL,
//
//     /// <summary>
//     /// High 16 bits of H register
//     /// </summary>
//     HH,
//
//     /// <summary>
//     /// Low 16 bits of H register
//     /// </summary>
//     HL,
//
//     /// <summary>
//     /// High 16 bits of Stack pointer register
//     /// </summary>
//     SPH,
//
//     /// <summary>
//     /// Low 16 bits of Stack pointer register
//     /// </summary>
//     SPL,
// }

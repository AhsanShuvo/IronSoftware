using IronSoftware.PhonePadLib;

IPhonePadService oldPhonePad = new OldPhonePadServiceImp();

Console.WriteLine(oldPhonePad.ProcessInput("33#"));
Console.WriteLine(oldPhonePad.ProcessInput("227*#"));
Console.WriteLine(oldPhonePad.ProcessInput("4433555 555666#"));
Console.WriteLine(oldPhonePad.ProcessInput("8 88777444666*664#"));
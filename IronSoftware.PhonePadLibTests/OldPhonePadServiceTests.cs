using IronSoftware.PhonePadLib;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace IronSoftware.PhonePadLibTests;

public class OldPhonePadServiceTests
{
    private readonly IPhonePadService _oldPhonePad = new OldPhonePadServiceImp();
    
    [Fact]
    public void ProcessInput_ShouldReturnCorrectOutput_ForSingleCharacter()
    {
        var result = _oldPhonePad.ProcessInput("3#");
        Assert.That(result, Is.EqualTo("D"));
    }

    [Fact]
    public void ProcessInput_ShouldReturnCorrectOutput_ForBackspace()
    {
        var result = _oldPhonePad.ProcessInput("227*#");
        Assert.That(result, Is.EqualTo("B"));
    }
    
    [Fact]
    public void ProcessInput_ShouldReturnCorrectOutput_ForMultipleCharacters()
    {
        var result = _oldPhonePad.ProcessInput("66655#");
        Assert.That(result , Is.EqualTo("OK"));
    }
    
    [Fact]
    public void ProcessInput_ShouldHandleBackspaceOnEmptyResult()
    {
        var result = _oldPhonePad.ProcessInput("*#");
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Fact]
    public void ProcessInput_ShouldHandleSpacesCorrectly()
    {
        var result = _oldPhonePad.ProcessInput("4433555 555666#");
        Assert.That(result, Is.EqualTo("HELLO"));
    }

    [Fact]
    public void ProcessInput_ShouldThrowExceptionForInvalidKey()
    {
        Assert.Throws<ArgumentException>(() => _oldPhonePad.ProcessInput("1#"));
    }
}
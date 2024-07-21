# OldPhonePadApp

A simple application that simulates an old phone keypad for input. This project provides a way to input text using a simulated phone keypad and includes functionality to handle input, process commands, and return the appropriate output.

### Prerequisites
.NET 8.0 SDK


## Table of Contents

### PhonePadLib
`PhonePadLib` is a .NET library that simulates an phone keypad for input. This library provides functionality to process sequences of key presses and convert them into the corresponding text output, handling special commands like backspace and end-of-input.

`PhonePadLib` contains an Interface `IPhonePadService` which exposes the public endpoint for this library. 

```
public interface IPhonePadService
{
    public string ProcessInput(string input);
}
``` 
`OldPhonePadServiceImp` is a concrete class that implements the interface. The reason behind adding an interface is to make the code loosely coupled and easy to extend for different type of keypad.

The concrete class has a key-mapping dictionary which maps numeric values to corresponding characters. 

```
private readonly Dictionary<char, string> _keyMappings = new Dictionary<char, string>
    {
        { '2', "ABC" },
        { '3', "DEF" },
        { '4', "GHI" },
        { '5', "JKL" },
        { '6', "MNO" },
        { '7', "PQRS" },
        { '8', "TUV" },
        { '9', "WXYZ" }
    };
```

The functionality of processing and converting them into corresponding text output is handled by using the follwoing strings - 
```
var result = new StringBuilder();
var currentSequence = new StringBuilder();
char? lastChar = null;
```
`result` - Store text output while processing the sequence.   
`currentSequence` - Store consecutive values.  
`lastChar` - Store last processed char to decide when to convert a sequence into text output.  

#### ProcessInput(string input)
It iterates over the input string and check for some conditions to perform some actions -  
```
if the current char is #(EOF):
  convert current sequence into text output if currentSequence has a length > 0 
  and append it to result
  ends the iteration
```

```
if the current char is *(BackSpace) :
  convert current sequence into text output if currentSequence has a length > 0 
  and append it to result
  clear the current sequence
  reduce the result length by 1 
```
```
if last processed char and current char is same:
  append the char to the currentSequence
```
```
if current char is a space:
  convert the current sequence into text output and append it to the result
  clear the current sequence
```
```
if current char is not same as last processed char:
  convert the current sequence into text output and append it to the result
  clear the current sequence and append new char to the current sequence
```

#### GetCharacterFromSequence(string sequence)
This method converts the text output from a sequence.  
It checks whether the key is valid and find the text output char.


### OldPhonePadApp 
A simple console application which has a dependency on `PhonePadLib`. 
We can use it like -  
```
IPhonePadService oldPhonePad = new OldPhonePadServiceImp();

Console.WriteLine(oldPhonePad.ProcessInput("33#"));
```

### PhonePadLibTest
This is a unit test application which tests publically accessable endpoint of the Library `ProcessInput(string input)`.


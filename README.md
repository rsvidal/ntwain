# NTwain
A minimal amount of code to "Just Scan" using Ntwain
This project has been developed using Visual Studio 2022 and C#.
The NTwain library uses internally the TwainDSM.dll (included in the last versions of Windows).
TwainDSM.dll works using Twain 2.x protocol, and it's compatible with x86 and x64 architecture
Note: However, Twain_32.dll (included in windows too) only is compatible with x86 arquitecture

You can test the NTwain library using three UI applications:
- Windows Console
- Windows Forms. You can get/set capabilities as PaperSize, Depth, Duplex page, Automatic size and more
- WPF

You can clone this project and test the NTwain Library, assigned different values in the scanner configuration.
For example, Depth can be 'B/W', 'Color' or 'Grayscale'

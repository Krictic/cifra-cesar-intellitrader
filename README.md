Cifra Cesar Intellitrader

Overview
This C# application is an implementation of the Caesar Cipher, an encryption method which involves replacing each letter in the plaintext by a letter some fixed number of positions down the alphabet. The application provides functionality to both encrypt and decrypt text using this cipher. You are allowed to define the shift amount for the encryption and decryption process.
The application is implemented in the Cifra_Cesar_Intellitrader.Program class. Its Main method is the entry point of the program and gives user the options to choose between encryption and decryption.

Usage
On initiating the running the program, it prompts if the action to be performed is an encryption or decryption.
If you wish to encrypt, type in 'e' or 'encrypt'.
Further, the console asks to input the text to be encrypted.
After the text input, define your shift amount. This is an integer that shifts letters in the text by the given amount.
The program then verifies the input string and the shift amount, and proceeds with the encryption. It displays the encrypted string.
If you wish to decrypt, type in 'd' or 'decrypt'.
The console then asks to input the text to be decrypted.
After the text input, you need to provide the shift amount used in the original encryption. If you do not know the shift value, type in '0'. The program will try all possibilities with respect to shift values (from 0 to 26).
The decrypted string will then be displayed.

Error Handling:

While providing the inputs, if you leave the text string empty or provide a shift value out of the range 1 - 26, then the corresponding error messages will be displayed. The range of the shift value is accepted between 1 and 26 because beyond this range, the letters in the text would just be rotated without actual shifting.
Implementation:

The Program class provides the following methods:

EncryptUi(): drives the encryption process

DecryptUi(): drives the decryption process

Encrypt(string text, int shift): does the actual encryption

Decrypt(string text, int shift = 0): does the actual decryption

ValidateString(string text, int shift): validates text string and shift

ProcessLetter(int shift, char letter, string operation): processes each character in the text
using System;
using System.IO.Ports;

class Program
{
    static SerialPort? serialPort; // Make the field nullable

    static void Main()
    {
        string portName = "COM1"; // Change this to the actual COM port your dongle is connected to.
        string phoneNumber = "RecipientPhoneNumber"; // Replace with the recipient's phone number
        string message = "Hello, this is a test message.";

        serialPort = new SerialPort(portName, 9600); // Set baud rate accordingly
        serialPort.Open();

        if (serialPort.IsOpen)
        {
            // Initialize the modem
            serialPort.WriteLine("ATZ\r\n");
            System.Threading.Thread.Sleep(1000);

            // Set SMS text mode
            serialPort.WriteLine("AT+CMGF=1\r\n");
            System.Threading.Thread.Sleep(500);

            // Send the SMS
            serialPort.WriteLine("AT+CMGS=\"" + phoneNumber + "\"\r\n");
            System.Threading.Thread.Sleep(500);
            serialPort.WriteLine(message + "\x1A"); // Message body and Ctrl+Z to send
            System.Threading.Thread.Sleep(1000);

            // Close the port
            serialPort.Close();
        }
        else
        {
            Console.WriteLine("Serial port is not open.");
        }
    }
}

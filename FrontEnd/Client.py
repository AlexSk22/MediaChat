import socket
import requests
import json

import Audio
import Listener 
import Writer

HOST = "localhost"
APIPORT = 5000 
PORT = 5050

def main():
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    try:
        s.connect((HOST, PORT))
        print("Connected!")

        listener = Listener.Listener(s)
        writer = Writer.Writer(s)
        
        Audio.list_audio_devices()

        input_device_index = int(input("\nEnter the device index number for INPUT (microphone): "))
        output_device_index = int(input("Enter the device index number for OUTPUT (speaker): "))

        aulist = Audio.AudioListener() 
        aulist.start(device_index=output_device_index)
        
        player = Audio.AudioPlayer(device_index=output_device_index)

        listener.start()
        writer.start()

        try:
            listener.start()
            player.start()
            print("\nStreaming audio in real-time. Press Ctrl+C to stop.\n")
            while True:
                data = listener.read()
                writer.sendMessage(data)
                player.play(data)
        except KeyboardInterrupt:
            print("\nStopping...")
        finally:
            listener.stop()
            player.stop()

        writer.join()                 # Wait for writer to finish
        listener.running = False      # Signal listener to stop
        s.shutdown(socket.SHUT_RDWR)  # Optional: shut down the connection
        s.close()                     # Close the socket
        listener.join()
        
    except Exception as e:
        print(f"Connection failed: {e}")
        s.close()

    #print(requests.get(f"http://{HOST}:{APIPORT}").text) # Hello World
    #print(requests.get(f"http://{HOST}:{APIPORT}/rooms").text) #Rooms List


if __name__ == "__main__":
    main()

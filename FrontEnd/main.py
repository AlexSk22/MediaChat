import requests
import json
import socket

import Writer
import Listener
# ask for server
# ask for rooms
# ask for name

def main():
    print("Welcome to mediachat client")   
    HOST = input("write host name: ") 
    APIPORT = input("write host api port (standart 5000): ")
  
    if not HOST:
        HOST = "localhost" 
    if not APIPORT:
        APIPORT = 5000 
   
    try:
        print("rooms")
        response = requests.get(f"http://{HOST}:{APIPORT}/rooms")
        response.raise_for_status()  # Raises HTTPError if the response was unsuccessful
        rooms = response.json()      # More efficient than using json.loads(response.text)
    except requests.RequestException as e:
        print(f"HTTP error occurred: {e}")
    except json.JSONDecodeError as e:
        print(f"JSON decode error: {e}")
    
    print(json.dumps(rooms,indent=4))
    for index,i in enumerate(rooms):
        print(f"{index}: RoomName {i["name"]}")
    roomIndex = int(input("Choose your room please"))
   
    VoicePORT = rooms[roomIndex]["voicePort"]
    TextPort = rooms[roomIndex]["textPort"] 
    
    print(f"Voice port: {VoicePORT}, Text port: {TextPort}") 

    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    try:
        s.connect((HOST, TextPort))
        print("Connected!")
        
        listener = Listener.Listener(s)
        writer = Writer.Writer(s)
    
        listener.start()
        writer.start()

        try:
            listener.start()
            print("\nStreaming audio in real-time. Press Ctrl+C to stop.\n")
            while True:
                data = listener.read()
                writer.sendMessage(data)
        except KeyboardInterrupt:
            print("\nStopping...")
        finally:
            listener.stop()

        writer.join()                 # Wait for writer to finish
        listener.running = False      # Signal listener to stop
        s.shutdown(socket.SHUT_RDWR)  # Optional: shut down the connection
        s.close()                     # Close the socket
        listener.join()
        
    except Exception as e:
        print(f"Connection failed: {e}")
        s.close()    

    
if __name__ == "__main__":
    main()
    
    

    
import requests
import json
import threading

import TextSocket
import AudioSocket
import Audio
# ask for server
# ask for rooms
# ask for name

def StartAudioSocket(HOST,VoicePORT,output_device_index,input_device_index):
    print("Creating Audio Socket")
    s = AudioSocket.AudioSocket(host=HOST,port=VoicePORT,output_device_index=output_device_index,input_device_index=input_device_index)
    s.start()

def StartTextSocket(HOST,TextPort):
    print("Creating Text Socket")
    s = TextSocket.TextSocket(HOST=HOST,TextPort=TextPort)
    s.start() 


def main():
    print("Welcome to mediachat client")   
    HOST = input("write host name: ").strip()

    APIPORT = input("write host api port (standart 5000): ")
  
    if not HOST:
        HOST = "localhost" 
    if not APIPORT:
        APIPORT = 5000 
   
    try:
        print("Rooms")
        response = requests.get(f"http://{HOST}:{APIPORT}/rooms")
        response.raise_for_status()  # Raises HTTPError if the response was unsuccessful
        rooms = response.json()      # More efficient than using json.loads(response.text)
    except requests.RequestException as e:
        print(f"HTTP error occurred: {e}")
    except json.JSONDecodeError as e:
        print(f"JSON decode error: {e}")
    
    for index,i in enumerate(rooms):
        print(f"{index}:{i["name"]}")
    roomIndex = int(input("Choose your room please"))
  
    Audio.list_audio_devices()
    input_device_index = int(input("\nEnter the device index number for INPUT (microphone): "))
    output_device_index = int(input("Enter the device index number for OUTPUT (speaker): "))
   
    VoicePORT = rooms[roomIndex]["voicePort"]
    TextPort = rooms[roomIndex]["textPort"] 
    
    print(f"Voice port: {VoicePORT}, Text port: {TextPort}") 

    args1 = [
        HOST,
        TextPort
    ]
    StartAudioSocket(HOST=HOST,VoicePORT=VoicePORT,output_device_index=output_device_index,input_device_index=input_device_index)
    #StartTextSocket(HOST,TextPort)
    
if __name__ == "__main__":
    main()
    
    

    
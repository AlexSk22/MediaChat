import socket
import threading

class Listener(threading.Thread):
    def __init__(self,socket : socket.socket):
        super().__init__()
        self.socket = socket
        self.running = True 
    
    def run(self):
        while self.running:
            data = self.socket.recv(1024)
            if not data:
                # Connection closed
                print("Connection closed by peer")
                break
            if data.strip() != b"":
                print(f"Received {data!r}")

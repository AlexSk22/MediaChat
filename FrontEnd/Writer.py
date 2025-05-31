import socket
import threading

class Writer(threading.Thread):
    def __init__(self,socket : socket.socket):
        super().__init__()
        self.socket = socket
        self.running = True
         
    def run(self):
        while self.running:
            data  = input()
            if data == "exit":
                self.running = False
                exit()
            self.socket.sendall(data.encode())
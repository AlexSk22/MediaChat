import socket

import Listener 
import Writer

HOST = "VanHost"  # The server's hostname or IP address
PORT = 12345  # The port used by the server

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    s.sendall(b"test request")
    data = s.recv(1024)
    if not data:
        print(f"can't connect")

    listener = Listener.Listener(s)
    writer = Writer.Writer(s)
   
    listener.start()
    writer.start() 
    
    writer.join()
    listener.running = False  # To stop listener if writer finished
    listener.join()
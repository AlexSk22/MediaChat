import sys
import socket
import threading

users = []

## change if needed but lets assume this is port for communication user-server
PORT = 12345
HOST = socket.gethostname()
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen()
    conn, addr = s.accept()
    with conn:
        print(f"Connected by {addr}")
        while True:
            data = conn.recv(1024)
            if not data:
                break
            conn.sendall(data)



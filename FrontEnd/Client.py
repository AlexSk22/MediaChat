import socket
import threading

HOST = "VanHost"  # The server's hostname or IP address
PORT = 12345  # The port used by the server

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    s.sendall(b"test request")
    data = s.recv(1024)
    if not data:
        print(f"can't connect")

    while True:
        data  = input()
        if data == "exit":
            break
        s.sendall(data.encode())
        data = s.recv(1024)
        print(f"Received {data!r}")
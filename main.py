import socket

serversocket = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
port =12345 

print(socket.gethostname())
serversocket.bind((socket.gethostname(),port))
serversocket.listen(5)

while True:
    (clientsocket,addres) = serversocket.accept()
    print("Got connection from ", addres)
    clientsocket.send("Hello World\n".encode())
    clientsocket.close()

serversocket.close()
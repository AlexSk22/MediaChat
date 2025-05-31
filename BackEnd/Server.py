import socket
import threading
import User

class Server(threading.Thread):
    def __init__(self,port : int,host : str,peoplecount : int):
        super().__init__()
        self.users = []
      
        self.HOST = host
        self.PORT = port
       
        self.serversocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.serversocket.bind((host, port))
        self.serversocket.listen(peoplecount)
        print(f"Server listening on {self.HOST}:{self.PORT}")

            
    def removeUser(self,user : User.User):
        if user in self.users:
            self.users.remove(user)

    def run(self):
        while True:
            conn, addr = self.serversocket.accept()
            user = User.User(conn, addr)
            
            for i in self.users:
                i.send("someone connected")
            self.users.append(user)
            user.start()

    def shutdown(self):
        for i in self.users:
            i.close()
        self.serversocket.close()
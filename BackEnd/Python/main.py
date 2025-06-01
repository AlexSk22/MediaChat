import socket
import Server

PORT = 12345
HOST = socket.gethostname()
PEOPLECOUNT = 5

def main():
    server = Server.Server(PORT,HOST,PEOPLECOUNT)
    server.start()
    input("enter to end")
    server.shutdown() 

if __name__ == "__main__":
    main()

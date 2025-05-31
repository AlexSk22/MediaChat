import socket
import User

users = []

## change if needed but lets assume this is port for communication user-server
PORT = 12345
HOST = socket.gethostname()

def main():
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.bind((HOST, PORT))
        s.listen(5)
        print(f"Server listening on {HOST}:{PORT}")

        while True:
            conn, addr = s.accept()
            user = User.User(conn, addr)
            for i in users:
                i.send("someone connected")
            users.append(user)
            user.start()

if __name__ == "__main__":
    main()

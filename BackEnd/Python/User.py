import threading

class User(threading.Thread):
    def __init__(self, conn, addr):
        super().__init__()
        self.conn = conn
        self.addr = addr
        self.running = True

    def OnSend(self,fun):
        fun()        

    def send(self, msg: str):
        try:
            if not isinstance(msg, bytes):
                self.conn.sendall(msg.encode())
            else:
                self.conn.sendall(msg)
        except Exception as e:
            print(f"Error with {self.addr}: {e}")

    def close(self):
        self.conn.close()

    def run(self):
        print(f"Connected by {self.addr}")
        try:
            while self.running:
                data = self.conn.recv(1024)
                if not data:
                    print(f"{self.addr} disconnected")
                    break

                # Check for exit command to stop thread
                if data == b"exit":
                    print(f"{self.addr} requested to exit")
                    break

                # Echo back the received data
                self.send(data)
        except Exception as e:
            print(f"Error with {self.addr}: {e}")
        finally:
            self.conn.close()
            print(f"Connection closed with {self.addr}")

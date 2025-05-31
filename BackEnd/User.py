import threading

class User(threading.Thread):
    def __init__(self, conn, addr):
        super().__init__()
        self.conn = conn
        self.addr = addr
        self.running = True

    def send(self,msg : str):
        try :
            self.conn.sendall(msg.encode())
        except Exception as e:
            print(f"Error with {self.addr}: {e}")
        finally:
            self.conn.close()

    def run(self):
        print(f"Connected by {self.addr}")
        try:
            while self.running:
                data = self.conn.recv(1024)
                if not data:
                    print(f"{self.addr} disconnected")
                    break
                # Echo back the received data
                self.conn.sendall(data)
        except Exception as e:
            print(f"Error with {self.addr}: {e}")
        finally:
            self.conn.close()

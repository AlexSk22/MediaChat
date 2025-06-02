import socket
import threading

class Listener(threading.Thread):
    def __init__(self, conn: socket.socket):
        super().__init__()
        self.conn = conn
        self.running = True
    
    def run(self):
        try:
            while self.running:
                data = self.conn.recv(1024)
                if not data:
                    print("Connection closed by peer")
                    self.running = False
                    break
                if data.strip():
                    print(data.decode().strip())
        except Exception as e:
            print(f"Error in listener thread: {e}")
        finally:
            self.conn.close()

    def stop(self):
        self.running = False
        self.conn.close()

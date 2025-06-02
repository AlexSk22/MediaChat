import threading
import socket

import Listener
import Writer

class TextSocket(threading.Thread):
    def __init__(self, HOST, TextPort):
        super().__init__()
        self.HOST = HOST
        self.TextPort = TextPort
        self.s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.listener = None
        self.writer = None
        try:
            self.s.connect((self.HOST, self.TextPort))
            print("Connected!")

            self.listener = Listener.Listener(self.s)
            self.writer = Writer.Writer(self.s)

            self.listener.start()
            self.writer.start()

            self.writer.join()                 # Wait for writer to finish
            self.listener.running = False      # Signal listener to stop (assumes Listener checks this)
            self.s.shutdown(socket.SHUT_RDWR)
            self.s.close()
            self.listener.join()

        except Exception as e:
            print(f"Connection failed: {e}")
            self.s.close()

    
    def stop(self):
        if self.writer:
            self.writer.join()
        if self.listener:
            self.listener.running = False
            self.listener.join()
        if self.s:
            try:
                self.s.shutdown(socket.SHUT_RDWR)
            except:
                pass
            self.s.close()

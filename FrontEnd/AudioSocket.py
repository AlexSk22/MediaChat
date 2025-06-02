import threading
import socket
import pyaudio

class AudioSocket(threading.Thread):
    def __init__(self, host, port, input_device_index, output_device_index):
        super().__init__()
        self.Host = host
        self.Port = port
        
        self.input_device_index = input_device_index
        self.output_device_index = output_device_index
        
        self.CHUNK = 1024
        self.FORMAT = pyaudio.paInt16
        self.CHANNELS = 1
        self.RATE = 44100
        self.running = True

    def run(self):
        try:
            s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            s.connect((self.Host, self.Port))
            print("AudioSocket init: ", self.Host, self.Port)
            print("AudioSocket started")

            p = pyaudio.PyAudio()

            input_stream = p.open(format=self.FORMAT, channels=self.CHANNELS, rate=self.RATE,
                                  input=True, input_device_index=self.input_device_index,
                                  frames_per_buffer=self.CHUNK)

            output_stream = p.open(format=self.FORMAT, channels=self.CHANNELS, rate=self.RATE,
                                   output=True, output_device_index=self.output_device_index,
                                   frames_per_buffer=self.CHUNK)

            while self.running:
                data = input_stream.read(self.CHUNK, exception_on_overflow=False)
                s.sendall(data)
                received = s.recv(self.CHUNK)
                if received:
                    output_stream.write(received, exception_on_underflow=False)
        except Exception as e:
            print(f"Error: {e}")
        finally:
            self.running = False
            try:
                input_stream.stop_stream()
                input_stream.close()
                output_stream.stop_stream()
                output_stream.close()
            except:
                pass
            s.close()

# Then in main
if __name__ == "__main__":
    host = "localhost"
    port = 45215 
    pyaudio.PyAudio()
    # print devices, ask user, etc.

    input_device_index = int(input("Input device index: "))
    output_device_index = int(input("Output device index: "))

    audio_socket = AudioSocket(host, port, input_device_index, output_device_index)
    audio_socket.start()

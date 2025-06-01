import pyaudio


class AudioListener:
    def __init__(self, rate=44100, channels=1, chunk=1024, format=pyaudio.paInt16, device_index=None):
        self.p = pyaudio.PyAudio()
        self.rate = rate
        self.channels = channels
        self.chunk = chunk
        self.format = format
        self.device_index = device_index
        self.stream = None

    def start(self):
        self.stream = self.p.open(format=self.format,
                                  channels=self.channels,
                                  rate=self.rate,
                                  input=True,
                                  frames_per_buffer=self.chunk,
                                  input_device_index=self.device_index)

    def read(self):
        return self.stream.read(self.chunk, exception_on_overflow=False)

    def stop(self):
        if self.stream is not None:
            self.stream.stop_stream()
            self.stream.close()
        self.p.terminate()


class AudioPlayer:
    def __init__(self, rate=44100, channels=1, chunk=1024, format=pyaudio.paInt16, device_index=None):
        self.p = pyaudio.PyAudio()
        self.rate = rate
        self.channels = channels
        self.chunk = chunk
        self.format = format
        self.device_index = device_index
        self.stream = None

    def start(self):
        self.stream = self.p.open(format=self.format,
                                  channels=self.channels,
                                  rate=self.rate,
                                  output=True,
                                  frames_per_buffer=self.chunk,
                                  output_device_index=self.device_index)

    def play(self, data):
        self.stream.write(data, exception_on_underflow=False)

    def stop(self):
        if self.stream is not None:
            self.stream.stop_stream()
            self.stream.close()
        self.p.terminate()


def list_audio_devices():
    p = pyaudio.PyAudio()
    print("Available audio devices:\n")
    for i in range(p.get_device_count()):
        info = p.get_device_info_by_index(i)
        print(f"[{i}] {info['name']} | Input Channels: {info['maxInputChannels']} | Output Channels: {info['maxOutputChannels']}")
    p.terminate()


def main():
    list_audio_devices()

    input_device_index = int(input("\nEnter the device index number for INPUT (microphone): "))
    output_device_index = int(input("Enter the device index number for OUTPUT (speaker): "))

    listener = AudioListener(device_index=input_device_index)
    player = AudioPlayer(device_index=output_device_index)

    try:
        listener.start()
        player.start()
        print("\nStreaming audio in real-time. Press Ctrl+C to stop.\n")
        while True:
            data = listener.read()
            player.play(data)
    except KeyboardInterrupt:
        print("\nStopping...")
    finally:
        listener.stop()
        player.stop()


if __name__ == "__main__":
    main()

import pyaudio

CHUNK = 512
FORMAT = pyaudio.paInt16
CHANNELS = 1
RATE = 48000
RECORD_SECONDS = 5

p = pyaudio.PyAudio()

# Step 1: List all available devices
print("Available audio devices:\n")
for i in range(p.get_device_count()):
    info = p.get_device_info_by_index(i)
    print(f"[{i}] {info['name']} | Input Channels: {info['maxInputChannels']} | Output Channels: {info['maxOutputChannels']}")

# Step 2: Ask user to select input and output device
input_device_index = int(input("\nEnter the device index number for INPUT (microphone): "))
output_device_index = int(input("Enter the device index number for OUTPUT (speaker): "))

# Step 3: Open selected input and output streams
InputStream = p.open(format=FORMAT,
                     channels=CHANNELS,
                     rate=RATE,
                     input=True,
                     input_device_index=input_device_index,
                     frames_per_buffer=CHUNK)

OutputStream = p.open(format=FORMAT,
                      channels=CHANNELS,
                      rate=RATE,
                      output=True,
                      output_device_index=output_device_index,
                      frames_per_buffer=CHUNK)

## Step 4: Record and play back simultaneously
#print("\nRecording and playing back in real-time...\n")
#for _ in range(0, int(RATE / CHUNK * RECORD_SECONDS)):
#    data = InputStream.read(CHUNK, exception_on_overflow=False)
#    OutputStream.write(data, exception_on_underflow=False)
#print("Done.")

while True:
    data = InputStream.read(CHUNK, exception_on_overflow=False)
    OutputStream.write(data, exception_on_underflow=False)
# Step 5: Cleanup
InputStream.stop_stream()
InputStream.close()
OutputStream.stop_stream()
OutputStream.close()
p.terminate()

import serial 
import time
import re



# heartbeat will check if device is on. 1 = On, 0 = Off.
def connect()
	try:
		ser = serial.Serial("COM10", timeout = None, baudrate = 115200)
		while (ser.isOpen() == True):
			print("Device is on. Port is open.")
			heartBeat = 1  # System is on.
			print (heartBeat)
	except serial.serialutil.SerialException:
			print ('Device is off.')
			heartBeat = 0 # System is off.
			print(heartBeat)



ser = serial.Serial("COM10", timeout = None, baudrate = 115200)


# Test to check if port is open. 

if(ser.isOpen() == False):
    ser.open()
else:
    print("Port is open.")

# Reading in data from Monoprice Printers.

while 1:

	# Reading in Temperature Data (Extruder/Bed)

	ser.write(b"M105;\n") # Converting to byte data.
	tempData = ser.readline()
	tempData = tempData.decode("utf-8") # Converting back to ASCII.
	sizeTempData = len(tempData.split())
	if sizeTempData == 12:
		print(tempData.split()[4]) # Printing Current Extruder Temperature.
		print(tempData.split()[5].strip("/")) # Printing the Set Extruder Temperature. 
		print(tempData.split()[6]) # Printing Current Bed Temperature.
		print(tempData.split()[7].strip("/")) # Printing the Set Bed Temperature. 
	else:
		continue # Continue the program to read the next data receiving until we have tempurature data.
	# If printer hasn't exported the entire line of data, continue to read. 
	while ser.inWaiting() > 0:
		tempData = ser.readline()
		tempData = tempData.decode("utf-8")
		sizeTempData = len(tempData.split())
		if sizeTempData == 12:
			print(tempData.split()[4]) # Printing Current Extruder Temperature.
			print(tempData.split()[5].strip("/")) # Printing the Set Exruder Temperature. 
			print(tempData.split()[6]) # Printing Current Bed Temperature.
			print(tempData.split()[7].strip("/")) # Printing the Set Bed Temperature. 
		else:
			continue # Continue the program to read the next data receiving until we have tempurature data.
	time.sleep(5)

	# Reading in Position Data (XYZ)

	ser.write(b"M114;\n")
	posData = ser.readline()
	posData = posData.decode("utf-8")
	# print(posData) #Understand the structure of posData
	sizePosData = len(posData.split())
	if sizePosData == 9:
		print(posData.split()[0]) # X Position
		print(posData.split()[1]) # Y Position
		print(posData.split()[2]) # Z Position
		print(posData.split()[3]) # E Extruder
	else:
		continue # Continue the program to read the next data receiving until we have position data.
	
	# If printer hasn't exported the entire line of data, continue to read.
	while ser.inWaiting() > 0:
		posData = ser.readline()
		posData = posData.decode("utf-8")
		sizePosData = len(posData.split())
		if sizePosData == 9:
			print(posData.split()[0]) # X Position
			print(posData.split()[1]) # Y Position
			print(posData.split()[2]) # Z Position
			print(posData.split()[3]) # E Extruder
		else:
			continue # Continue the program to read the next data receiving until we have position data.
	time.sleep(5)






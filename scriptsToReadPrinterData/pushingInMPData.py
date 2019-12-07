import VTDataRepository
import serial
import time
import threading

# This function will connect serial port to Monoprice Printer.
def connect():

	# machineState will check if device is on. 1 = On, 0 = Off.
	ser = serial.Serial("COM10", timeout=None, baudrate=115200)

	try:
		if (ser.isOpen() == True):
			print("Device is on. Port is open.")
		return ser

	except serial.serialutil.SerialException:
  		print ('Device is off.')
  		machineState = 0 # System is off.

# Function sends heartbeat every second.
def heartBeatSend(heartBeat):
	threading.Timer(1,heartBeatSend(heartBeat))
	heartBeat.pushPoint(1)
	heartBeatSend(heartBeat)

# Reading in data from Monoprice Printers.
def readMPData(ser):

	results = {} # Dictionary to process position indexing for main() function.

	while 1:

		# Reading in Temperature Data (Extruder/Bed)

		ser.write(b"M105;\n") # Converting to byte data.
		tempData = ser.readline()
		tempData = tempData.decode("utf-8") # Converting back to ASCII.
		sizeTempData = len(tempData.split())
		if sizeTempData == 12:
			results["extruderTemp"] = tempData.split()[4].strip("T:") # Current Extruder Temperature.
			results["setExtruderTemp"] = tempData.split()[5].strip("/") # Set Extruder Temperature. 
			results["bedTemp"] = (tempData.split()[6]).strip("B:") # Current Bed Temperature.
			results["setBedTemp"] = tempData.split()[7].strip("/") # Set Bed Temperature.

		else:
			continue # Continue the program to read the next data receiving until we have tempurature data.
		# If printer hasn't exported the entire line of data, continue to read. 
		while ser.inWaiting() > 0:
			tempData = ser.readline()
			tempData = tempData.decode("utf-8")
			sizeTempData = len(tempData.split())
			if sizeTempData == 12:
				results["extruderTemp"] = tempData.split()[4].strip("T:") # Current Extruder Temperature.
				results["setExtruderTemp"] = tempData.split()[5].strip("/") # Set Extruder Temperature. 
				results["bedTemp"] = (tempData.split()[6]).strip("B:") # Current Bed Temperature.
				results["setBedTemp"] = tempData.split()[7].strip("/") # Set Bed Temperature.
			else:
				continue # Continue the program to read the next data receiving until we have tempurature data.
		#time.sleep(1)

		# Reading in Position Data (XYZ)

		ser.write(b"M114;\n")
		posData = ser.readline()
		posData = posData.decode("utf-8")
		# print(posData) #Understand the structure of posData
		sizePosData = len(posData.split())
		if sizePosData == 9:
			results["xPosition"] = (posData.split()[0]).strip("X:") # X Position
			results["yPosition"] = (posData.split()[1]).strip("Y:") # Y Position
			results["zPosition"] = (posData.split()[2]).strip("Z:") # Z Position
			results["ePosition"] = (posData.split()[3]).strip("E:") # E Extruder
		else:
			continue # Continue the program to read the next data receiving until we have position data.
		
		# If printer hasn't exported the entire line of data, continue to read.
		while ser.inWaiting() > 0:
			posData = ser.readline()
			posData = posData.decode("utf-8")
			sizePosData = len(posData.split())
			if sizePosData == 9:
				results["xPosition"] = (posData.split()[0]).strip("X:") # X Position
				results["yPosition"] = (posData.split()[1]).strip("Y:") # Y Position
				results["zPosition"] = (posData.split()[2]).strip("Z:") # Z Position
				results["ePosition"] = (posData.split()[3]).strip("E:") # E Extruder
			else:
				continue # Continue the program to read the next data receiving until we have position data.
		#time.sleep(1)

		return results



#changes made to loaded items will not be saved to the repository!
def main():

	ser = connect() # Connecting printer to serial port.
	if (ser.isOpen() == False):
		return
	
	#initialize repository
	repo = VTDataRepository.VTDataRepository("http://10.42.0.18:8080/")


	#create a new physical asset
	physicalAsset1 = repo.createPhysicalAsset("mpPrinter01")	
	print(physicalAsset1.toString())

	#set connections string for an asset
	# res = physicalAsset1.setConnectionString("usb-FTDI_FT232R_USB_UART_A7049C0Z-if00-port0")
	# print(res)

	#add dataTag for Monoprice Printer data (float) to mpPrinter01
	machineState = physicalAsset1.createDataTag("machineState", VTDataRepository.RepositoryDataType.INTEGER)
	heartBeat = physicalAsset1.createDataTag("heartBeat", VTDataRepository.RepositoryDataType.INTEGER)
	xPosition = physicalAsset1.createDataTag("xPosition", VTDataRepository.RepositoryDataType.FLOAT)
	yPosition = physicalAsset1.createDataTag("yPosition", VTDataRepository.RepositoryDataType.FLOAT)
	zPosition = physicalAsset1.createDataTag("zPosition", VTDataRepository.RepositoryDataType.FLOAT)
	ePosition = physicalAsset1.createDataTag("ePosition", VTDataRepository.RepositoryDataType.FLOAT)
	extruderTemp = physicalAsset1.createDataTag("extruderTemp", VTDataRepository.RepositoryDataType.FLOAT)
	setExtruderTemp = physicalAsset1.createDataTag("setExtruderTemp", VTDataRepository.RepositoryDataType.FLOAT)
	bedTemp = physicalAsset1.createDataTag("bedTemp", VTDataRepository.RepositoryDataType.FLOAT)
	setBedTemp = physicalAsset1.createDataTag("setBedTemp", VTDataRepository.RepositoryDataType.FLOAT)
	heartBeatSend(heartBeat)

	machineState.pushPoint(1) # Indicating if the print is connected or not.

	#Checking if printer is on. If not, shut off program.
	while 1: 

		try:
			data = readMPData(ser) # Reading in data.
		except serial.serialutil.SerialException:
			machineState.pushPoint(0)  # Indicating if the print is connected or not.
			#heartBeat.pushPoint(0)
			return
		except IndexError: # Anticipating IndexError message if disconnected.
			machineState.pushPoint(0)  # Indicating if the print is connected or not.
			#heartBeat.pushPoint(0)
			return
		except KeyboardInterrupt:
			machineState.pushPoint(0)  # Indicating if the print is connected or not.
			#heartBeat.pushpoint(0)
			return


		#push data point to tag (auto generate time)
		xPosition.pushPoint(data["xPosition"]) # X Position
		yPosition.pushPoint(data["yPosition"]) # Y Position
		zPosition.pushPoint(data["zPosition"]) # Z Position
		ePosition.pushPoint(data["ePosition"]) # E Extruder
		extruderTemp.pushPoint(data["extruderTemp"]) # Extruder Temperature
		setExtruderTemp.pushPoint(data["setExtruderTemp"]) # Set Extruder Temperature
		bedTemp.pushPoint(data["bedTemp"]) # Bed Temperature
		setBedTemp.pushPoint(data["setBedTemp"]) # Set Bed Temperature
		heartBeat.pushPoint(1)

		time.sleep(1) # Useful for testing out data. 

main()

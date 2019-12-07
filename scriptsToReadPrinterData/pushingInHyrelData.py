import VTDataRepository
import serial
import time
import threading

# This function will connect serial port to Monoprice Printer.
def connect():
	ser = serial.Serial("COM13", timeout = None, baudrate = 38400)
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

# Reading in data from Hyrel Printers.
def readHyrelData(ser):

	results = {} # Dictionary to process position indexing for main() function.

	while 1:

		hyrelData = ser.readline().decode("utf-8") # Converting back to ASCII.
		extruderTempData = hyrelData.split()[2]
		bedTempData = hyrelData.split()[7]
		results["extruderTemp"] = extruderTempData
		results["bedTemp"] = bedTempData

		# hyrelDataEx = ["b'>RT", ':T13', '999', '0', '4094', '4094', ':T91', '23', '0', '23', '58']

		# Printing rest of the data.
		# data1 = hyrelData.split()[0]
		# tool1 = hyrelData.split()[1].strip(":")
		# data2 = hyrelData.split()[3]
		# data3 = hyrelData.split()[4]
		# data4 = hyrelData.split()[5]
		# tool2 = hyrelData.split()[6].strip(":")
		# data5 = hyrelData.split()[8]
		# data6 = hyrelData.split()[9]
		# data7 = hyrelData.split()[10]

		return results



#changes made to loaded items will not be saved to the repository!
def main():

	ser = connect() # Connecting printer to serial port. 
	if (ser.isOpen() == False):
		return
	
	#initialize repository
	repo = VTDataRepository.VTDataRepository("http://10.42.0.18:8080/")


	#create a new physical asset
	physicalAsset1 = repo.createPhysicalAsset("hyrelPrinter01")	
	print(physicalAsset1.toString())

	#set connections string for an asset
	# res = physicalAsset1.setConnectionString("usb-FTDI_FT232R_USB_UART_A7049C0Z-if00-port0")
	# print(res)

	#add dataTag for Monoprice Printer data (float) to mpPrinter01
	machineState = physicalAsset1.createDataTag("machineState", VTDataRepository.RepositoryDataType.INTEGER)
	heartBeat = physicalAsset1.createDataTag("heartBeat", VTDataRepository.RepositoryDataType.INTEGER)
	extruderTemp = physicalAsset1.createDataTag("extruderTemp", VTDataRepository.RepositoryDataType.FLOAT)
	bedTemp = physicalAsset1.createDataTag("bedTemp", VTDataRepository.RepositoryDataType.FLOAT)
	heartBeatSend(heartBeat)

	machineState.pushPoint(1)  # Indicating if the print is connected or not. Only need to do once.

	#Checking if printer is on. If not, shut off program.
	while 1:
		try:
			data = readHyrelData(ser) # Reading in data.
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
			#heartBeat.pushPoint(0)
			return

		#push data point to tag (auto generate time)
		extruderTemp.pushPoint(data["extruderTemp"]) # Extruder Temperature
		bedTemp.pushPoint(data["bedTemp"]) # Bed Temperature
		# heartBeat.pushPoint(1)

		time.sleep(1) # Useful for testing out data.

main()

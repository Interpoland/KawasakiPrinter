import serial 
import time
import re

def movement():
	print("work")

ser = serial.Serial("COM13", timeout = None, baudrate = 38400)


# Test to check if port is open. 

if(ser.isOpen() == False):
    ser.open()
else:
    print("Port is open.")

while 1:
	hyrelData = ser.readline().decode("utf-8") # Converting back to ASCII.
	extruderTempData = hyrelData.split()[2]
	bedTempData = hyrelData.split()[7]
	print("T:" + extruderTempData)
	print("B:" + bedTempData)

	hyrelDataEx = ["b'>RT", ':T13', '999', '0', '4094', '4094', ':T91', '23', '0', '23', '58']

	# Printing rest of the data.
	data1 = hyrelData.split()[0]
	tool1 = hyrelData.split()[1].strip(":")
	data2 = hyrelData.split()[3]
	data3 = hyrelData.split()[4]
	data4 = hyrelData.split()[5]
	tool2 = hyrelData.split()[6].strip(":")
	data5 = hyrelData.split()[8]
	data6 = hyrelData.split()[9]
	data7 = hyrelData.split()[10]




